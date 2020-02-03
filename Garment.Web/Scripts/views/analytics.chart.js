var app = angular.module("analyticsApp", []);


var selectedTeams = [];
var teams = getCookie("teams");
if (teams != "")
    selectedTeams = JSON.parse(teams);

var today = moment(new Date());
var dateTemp = moment(new Date(today.year(), today.month(), 1));

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}

app.controller("ChartController", function ($scope, $http)
{
    $scope.from = dateTemp.format('DD/MM/YYYY');
    $scope.to = dateTemp.add(1, 'months').add(-1, 'days').format('DD/MM/YYYY');
    $scope.timeRange = 'day';
    $scope.timeRangeValues = { "day": "Ngày", "week": "Tuần", "month": "Tháng", "quater": "Quý", "year": "Năm" };

    function reloadTable()
    {
        $http.get("/api/Analytics/TeamsTable?from=" + $scope.from + "&to=" + $scope.to).then(function (result) {
            $scope.factories = result.data.Factories;
        });
    }
    var chartOptions = {
        chart: { type: 'spline', animation: false },
        title: { text: 'Biểu đồ thống kê sản xuất' },
        legend: { enabled: false },
        tooltip:
        {
            valueDecimals: 2,
            formatter: function () {
                var s = 'Ngày ' + Highcharts.dateFormat('%d/%m/%Y', this.x) + '<br/><span style="font-weight:bold; color:' + this.series.color + '">' + this.series.name + '</span>: ' + this.point.y;
                if (this.point.products) {
                    s += "<br/>--------MÃ HÀNG--------";
                    var noProductDisplay = Math.min(this.point.products.length, 10);
                    for (var i = 0; i < noProductDisplay; i++) {
                        var product = this.point.products[i];
                        s += ("<br/><strong>" + product.ProductId + "</strong>: " + product.ProduceQuantity);
                    }
                    if (this.point.products.length > 10) {
                        s += "<br/>...<br/>Nhấn chuột trái để xem thêm"
                    }
                }
                return s;
            }
        },
        xAxis:
       {
           type: 'datetime',
           dateTimeLabelFormats:
           {
               millisecond: '%H:%M:%S.%L',
               second: '%H:%M:%S',
               minute: '%H:%M',
               hour: '%H:%M',
               day: '%e/%b',
               week: '%e/%b',
               month: '%b/%y',
               year: '%Y'
           }
       },
        yAxis: {
            min: 0,
            title: {
                text: 'Số lượng'
            }

        },
        plotOptions:
        {
            series:
            {
                point:
                {
                    events:
                    {
                        click: function (e) {
                            console.log("click", this);
                            var selectedDate = Highcharts.dateFormat('%d/%m/%Y', this.x);
                            var point = this;
                            $scope.$apply(function ()
                            {
                                $scope.point = {
                                    Date: selectedDate,
                                    ProduceQuantity: point.y,
                                    Products: point.products
                                };
                            })
                           
                            $("#pointModal").modal('show');
                        }
                    }
                }
            },
            spline: {
                animation: false,
                lineWidth: 2,
                states: {
                    hover: {
                        lineWidth: 3
                    }
                },
                marker: {
                    enabled: false
                }
            }
        },

    };

    reloadTable();

    var pickerRange = $('#daterange').daterangepicker({
        locale: {
            format: 'DD/MM/YYYY'
        },
        startDate: $scope.from,
        endDate: $scope.to,
        opens: 'left',
    });

    $("#btnday, #btnweek, #btnmonth, #btnquarter, #btnyear").on("click", function () {
        $('#selectDate').empty();
        $('#selectDate').append($(this).html() + ' <span class="caret"></span>');
        $(this).removeClass("active");
        $(this).addClass("active");
        $(this).siblings().removeClass("active");
        $("#myModal").modal('show');
        changeTimeRange($(this).data("value"));
        //drawChart();
    });
    
    $('.dropdownchild').on('click', '.selectpicker', function (e) {
        e.preventDefault();
        var selectpicker = this;
        $scope.$apply(function () {
            $scope.from = $(selectpicker).data('start');
            $scope.to = $(selectpicker).data('end');
        });
        
        $('#daterange').data('daterangepicker').setStartDate($scope.from);
        $('#daterange').data('daterangepicker').setEndDate($scope.to);
        var title = $(this).data('display');

        $('.selectparent').each(function () {
            $(this).empty();
            $(this).html($(this).data('default') + ' <span class="caret"></span>');
            $(this).removeClass('active');
        });
        var button = $(this).parent().parent().parent().parent().children('button');
        button.empty();
        button.html(title + ' <span class="caret"></span>');
        button.addClass('active');
        $("#myModal").modal('show');
        drawChart();
        reloadTable();
        //rerenderTable();
    });
    $('#daterange').on("apply.daterangepicker", function (ev, picker) {
        $scope.$apply(function () {
            $scope.from = $('#daterange').data('daterangepicker').startDate.format('DD/MM/YYYY');
            $scope.to = $('#daterange').data('daterangepicker').endDate.format('DD/MM/YYYY');
        });
        $('.selectparent').each(function () {
            $(this).empty();
            $(this).html($(this).data('default') + ' <span class="caret"></span>');
            $(this).removeClass('active');
        });
        drawChart();
        reloadTable();
    });
    $(".toggle-team").on("click", function () {
        var toggleBtn = this;
        var factoryId = $(this).data("factoryid");
        var teamId = $(this).data("id").toString();
        var checked = !$(this).data("checked");
        $(this).attr("data-checked", checked);
        $(this).data("checked", checked);



        //add to or remove from selectedTeams 
        var teamIndex = selectedTeams.indexOf(teamId);
        if (checked) {
            if (teamIndex == -1)
                selectedTeams.push(teamId);
        }
        else if (teamIndex > -1)
            selectedTeams.splice(teamIndex, 1);
        setCookie("teams", JSON.stringify(selectedTeams), 1);//save

        var chart = $('#chart').highcharts();
        var found = false;
        $.each(chart.series, function (i, s) {
            if (s.options.id == teamId) {
                if (!checked)
                    s.hide();
                else
                    s.show();
            }
        });
    });

    $('input:radio[name="radiocharttype"]').change(function () {
        var charttype = $(this).val();
        chartOptions.chart.type = charttype;
        $('#chart').highcharts(chartOptions);
    });
    $("#cbToggleMarker").on("change", function () {
        var enabled = chartOptions.plotOptions.spline.marker.enabled;
        chartOptions.plotOptions.spline.marker.enabled = !enabled;
        $('#chart').highcharts(chartOptions);
    })

    $(function () {
        drawChart();

        $('#selectDate').addClass('active');
        $('#selectDate').empty();
        $('#selectDate').html('Theo Ngày <span class="caret"></span>');

        $('#initMonth').empty();
        $('#initMonth').html(dateTemp.month() + 1 + ' / ' + dateTemp.year() + ' <span class="caret"></span>');
        $('#initMonth').addClass('active')
    })
    

    function changeTimeRange(val) {
        if (val != $scope.timeRange) {
            $scope.$apply(function () {
                $scope.timeRange = val;
            });
            drawChart();
        }
    }

    function drawChart() {
        $.getJSON("/api/Analytics/TeamsChart?from=" + $scope.from + "&to=" + $scope.to + "&groupBy=" + $scope.timeRange, function (data) {
            $.each(data, function (i, team) {
                //bật lại các mục đã chọn
                if (selectedTeams.indexOf(team.id) == -1)
                    team.visible = false;
                else {
                    $(".toggle-team[data-id=" + team.id + "]").attr("data-checked", true);
                    $(".toggle-team[data-id=" + team.id + "]").data("checked", true);
                }
            })
            chartOptions.series = data;
            $("#chart").highcharts(chartOptions);

            $("#myModal").modal('hide');
        })
    }
});
app.directive("popover", function ($compile, $templateCache, $timeout) {
   
    return {
        restrict: 'A',
        scope: {
            team : '&'
        },
        link: function (scope, element, attrs) {
            var team = scope.team();

            var content = '<div>'
                + '<table class="table table-responsive table-hover table-bordered"><tbody>';

            for (var i = 0; i < team.Products.length; i++)
            {
                
                var product = team.Products[i];
                var percent = product.ProduceQuantity * 100.0 / team.ProduceQuantity;
                content += '<tr><td>' + product.ProductId + '</td><td>' + product.ProduceQuantity + '</td><td><div class="progress" style="width: 200px">'
                    + '<div class="progress-bar progress-bar-success" role="progressbar" style="width: '+percent+'%;">'
                    + '</div>'
                + '</div></td><td>' + Math.round(percent * 100) / 100 + ' %</td></tr>';
            }

            content += '</tbody></table>'
            + '</div>';

            var options = { placement: 'bottom', trigger: 'hover', html:true, title : team.Name, content: content };
            $(element).popover(options);
        }
    };
});

