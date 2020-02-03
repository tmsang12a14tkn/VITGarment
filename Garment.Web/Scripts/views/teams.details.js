var chartOptions = {
        chart: { type: 'spline', animation: false },
        title: { text: 'Biểu đồ thống kê sản xuất' },
        tooltip:
        {
            valueDecimals: 2,
            formatter: function () {

                var s = 'Ngày ' + Highcharts.dateFormat('%d/%m/%Y', this.x) + '<br/><span style="font-weight:bold; color:' + this.series.color + '">' + this.series.name + '</span>: ' + this.point.y;
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
                            var teamid = this.series.options.id;
                            var $fixedTooltip = $("#fixed-tooltip");
                            var link = '#';

                            $fixedTooltip.show();
                            $fixedTooltip.find(".title").html(this.series.name + " - " + selectedDate);
                            $fixedTooltip.find(".content").html('Sản xuất: ' + this.y);
                            $fixedTooltip.find(".buttons").html('<a href="' + link + '" class="btn btn-default">Xem chi tiết</a>');
                        }
                    }
                }
            }
        },

    };
    var errorScrollStatus = null;
    var errorScrollStatus2 = null;
    var hrScrollStatus = null;

    var errorChartOptions = {
        chart: {
            type: 'column'
        },
        title: {
            text: 'Biểu đồ sự cố chi tiết'
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
                day: '%e/%m',
                week: '%e/%b',
                month: '%m/%y',
                year: '%Y'
            },
            range: 5184000000,
            events: {
                setExtremes: function (e) {
                    if (errorScrollStatus == null)
                        errorScrollStatus = 2;
                    if (errorScrollStatus == 2) {
                        var percent = (e.min - e.target.dataMin) * 100 / (e.target.dataMax - e.target.dataMin);
                        $("#table-error-wrapper").scrollLeft($("#table-error").width() * percent / 100);
                    }
                }
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Số lỗi'
            },
            stackLabels: {
                enabled: true,
                style: {
                    fontWeight: 'bold',
                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                }
            }
        },
        scrollbar: {
            enabled: true
        },

        legend: {
            align: 'right',
            x: -30,
            verticalAlign: 'top',
            y: 25,
            floating: true,
            backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
            borderColor: '#CCC',
            borderWidth: 1,
            shadow: false
        },
        tooltip: {
            shared: true,
            formatter: function () {
                var points = this.points;
                var pointsLength = points.length;

                var tooltipMarkup = pointsLength ? '<span style="font-size: 10px">' + Highcharts.dateFormat('%d/%m/%Y', points[0].key) + '</span><br/>' : '';
                var index;

                for (index = 0; index < pointsLength; index += 1) {
                    tooltipMarkup += '<span style="color:' + points[index].series.color + '">\u25CF</span> ' + points[index].series.name + ': <b>' + points[index].y + '</b><br/>';
                }

                return tooltipMarkup;
            }
        },
        plotOptions: {
            series:
            {
                //pointWidth: 60,
                pointPadding: 0,
                groupPadding: 0.01
            },
            column: {
                stacking: 'normal',
                dataLabels: {
                    enabled: true,
                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
                    style: {
                        textShadow: '0 0 3px black'
                    }
                }
            }
        }
    };

    var errorChartOptions2 = {
        chart: {
            type: 'column'
        },
        title: {
            text: 'Biểu đồ sự cố'
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
                day: '%e/%m',
                week: '%e/%b',
                month: '%m/%y',
                year: '%Y'
            },
            range: 5184000000,
            events: {
                setExtremes: function (e) {
                    if (errorScrollStatus2 == null)
                        errorScrollStatus2 = 2;
                    if (errorScrollStatus2 == 2) {
                        var percent = (e.min - e.target.dataMin) * 100 / (e.target.dataMax - e.target.dataMin);
                        $("#table-error-wrapper-2").scrollLeft($("#table-error-2").width() * percent / 100);
                    }
                }
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Số lỗi'
            },
            stackLabels: {
                enabled: true,
                style: {
                    fontWeight: 'bold',
                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                }
            }
        },
        scrollbar: {
            enabled: true
        },

        legend: {
            align: 'right',
            x: -30,
            verticalAlign: 'top',
            y: 25,
            floating: true,
            backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
            borderColor: '#CCC',
            borderWidth: 1,
            shadow: false
        },
        tooltip: {
            shared: true,
            formatter: function () {
                var points = this.points;
                var pointsLength = points.length;

                var tooltipMarkup = pointsLength ? '<span style="font-size: 10px">' + Highcharts.dateFormat('%d/%m/%Y', points[0].key) + '</span><br/>' : '';
                var index;

                for (index = 0; index < pointsLength; index += 1) {
                    tooltipMarkup += '<span style="color:' + points[index].series.color + '">\u25CF</span> ' + points[index].series.name + ': <b>' + points[index].y + '</b><br/>';
                }

                return tooltipMarkup;
            }
        },
        plotOptions: {
            series:
            {
                pointPadding: 0,
                groupPadding: 0.01
            },
            column: {
                stacking: 'normal',
                dataLabels: {
                    enabled: true,
                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
                    style: {
                        textShadow: '0 0 3px black'
                    }
                }
            }
        }
    };

    var hrChartOptions = {
        chart: {
            type: 'column'
        },
        title: {
            text: 'Biểu đồ nhân sự'
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
                day: '%e/%m',
                week: '%e/%b',
                month: '%m/%y',
                year: '%Y'
            },
            range: 5184000000,
            events: {
                setExtremes: function (e) {
                    if (hrScrollStatus == null)
                        hrScrollStatus = 2;
                    if (hrScrollStatus == 2) {
                        var percent = (e.min - e.target.dataMin) * 100 / (e.target.dataMax - e.target.dataMin);
                        $("#table-hr-wrapper").scrollLeft($("#table-hr").width() * percent / 100);
                    }
                }
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Số lỗi'
            },
            stackLabels: {
                enabled: true,
                style: {
                    fontWeight: 'bold',
                    color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                }
            }
        },
        scrollbar: {
            enabled: true
        },

        legend: {
            align: 'right',
            x: -30,
            verticalAlign: 'top',
            y: 25,
            floating: true,
            backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
            borderColor: '#CCC',
            borderWidth: 1,
            shadow: false
        },
        tooltip: {
            shared: true,
            formatter: function () {
                var points = this.points;
                var pointsLength = points.length;

                var tooltipMarkup = pointsLength ? '<span style="font-size: 10px">' + Highcharts.dateFormat('%d/%m/%Y', points[0].key) + '</span><br/>' : '';
                var index;

                for (index = 0; index < pointsLength; index += 1) {
                    tooltipMarkup += '<span style="color:' + points[index].series.color + '">\u25CF</span> ' + points[index].series.name + ': <b>' + points[index].y + ' (' + Math.round(points[index].percentage*10)/10 + '%)' + '</b><br/>';
                }

                return tooltipMarkup;
            }
        },
        plotOptions: {
            series:
            {
                //pointWidth: 60,
                pointPadding: 0,
                groupPadding: 0.01
            },
            column: {
                stacking: 'normal',
                dataLabels: {
                    enabled: true,
                    color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
                    style: {
                        textShadow: '0 0 3px black'
                    }
                }
            }
        }
    };
    var weekdayMap = ["CN", "T2", "T3", "T4", "T5", "T6", "T7"];
    var app = angular.module("garmentApp", []);
    app.controller("teamController", function ($scope, $http) {
        var today = moment(new Date());
        var toDate = today; //moment(new Date(today.year(), today.month(), 1));
        var fromDate = toDate.clone().add(-30, 'days');//fromDate.clone().add(1, 'months').add(-1, 'days');
        $scope.from = fromDate.format('DD/MM/YYYY');
        $scope.to = toDate.format('DD/MM/YYYY');

        //var errorChart;
        var pickerRange = $('#daterange').daterangepicker({
            locale: {
                format: 'DD/MM/YYYY'
            },
            startDate: $scope.from,
            endDate: $scope.to,
            opens: 'left',
        });

        $('.dropdownchild').on('click', '.selectpicker', function (e) {
            e.preventDefault();
            var selectpicker = this;
            $scope.$apply(function () {
                $scope.from = $(selectpicker).data('start');
                $scope.to = $(selectpicker).data('end');

                //fromDate = $('#daterange').data('daterangepicker').startDate;
                //$scope.from = fromDate.format('DD/MM/YYYY');
                //toDate = $('#daterange').data('daterangepicker').endDate;
                //$scope.to = toDate.format('DD/MM/YYYY');
            });

            $('#daterange').data('daterangepicker').setStartDate($scope.from);
            $('#daterange').data('daterangepicker').setEndDate($scope.to);
            fromDate = $('#daterange').data('daterangepicker').startDate;
            toDate = $('#daterange').data('daterangepicker').endDate;

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
            //$("#myModal").modal('show');
            drawTeamChart();
            drawErrorChart();
            drawHRChart();
        });
        $('#daterange').on("apply.daterangepicker", function (ev, picker) {
            fromDate = $('#daterange').data('daterangepicker').startDate;
            $scope.from = fromDate.format('DD/MM/YYYY');
            toDate = $('#daterange').data('daterangepicker').endDate;
            $scope.to = toDate.format('DD/MM/YYYY');
            $('.selectparent').each(function () {
                $(this).empty();
                $(this).html($(this).data('default') + ' <span class="caret"></span>');
                $(this).removeClass('active');
            });
            drawTeamChart();
            drawErrorChart();
            drawHRChart();

        });
        $('.btn-error-chart').on('shown.bs.tab', function (e) {
            window.dispatchEvent(new Event('resize'));
        });

        function drawTeamChart() {

        	$.getJSON("/api/Analytics/SingleTeamChart?from=" + $scope.from + "&to=" + $scope.to + "&id=" + teamId, function (data) {
                chartOptions.series = data;
                $("#chart").highcharts(chartOptions);
            });
        }
        function drawErrorChart() {
        	$http.jsonp("/api/Analytics/ReasonChart?from=" + $scope.from + "&to=" + $scope.to + "&id=" + teamId + "&callback=JSON_CALLBACK").success(function (data) {
                $scope.dates = [];
                $scope.months = [];
                for (var date = fromDate; date <= toDate; date.add(1, 'days')) {
                    var month = date.month() + 1; //month is based on zero
                    var year = date.year();
                    var monthGroup = $scope.months.find(function (monthGroup) {
                        return monthGroup.month == month && monthGroup.year == year;

                    });
                    if (monthGroup == null) {
                        monthGroup = {
                            month: month,
                            year: year,
                            value: 1,
                        };
                        $scope.months.push(monthGroup);
                    }
                    else
                        monthGroup.value += 1;

                    $scope.dates.push({ date: date.date(), weekday: weekdayMap[date.day()] });
                }
                $scope.errordata = data;
                $scope.errordata2 = [];
                $scope.errordata2[0] = data[0];
                $scope.errordata2[1] = unionError(data[1], data[3], "Thiếu nguyên phụ liệu", data[1].color);
                $scope.errordata2[2] = data[4];
                $scope.errordata2[3] = data[5];

                errorChartOptions.series = $scope.errordata;
                $("#error-chart").highcharts(errorChartOptions);

                errorChartOptions2.series = $scope.errordata2;
                $("#error-chart-2").highcharts(errorChartOptions2);
            })
            .error(function (data, status, headers, config) {
                console.log(data, status, headers, config);
            });
        };

        function unionError(error1, error2, newName, newColor) {
        	var error =
			{
				color: newColor,
				name: newName,
				data: [],
				cells:[]
			};
        	for (var i = 0; i < error1.data.length; i++) {
        		error.data[i] = { x: error1.data[i].x, y: error1.data[i].y != null && error2.data[i].y != null ? error1.data[i].y + error2.data[i].y : null };
        		error.cells[i] = error1.cells[i] + error2.cells[i];
        	}
        	console.log(error);
        	return error;
        }

        function drawHRChart()
        {
            $http.jsonp("/api/Analytics/EmployeeChart?from=" + $scope.from + "&to=" + $scope.to + "&id="+teamId+"&callback=JSON_CALLBACK").success(function (data) {
                $scope.hrdata = data;
                $scope.hrdata[0].name = "Vắng không lý do";
                $scope.hrdata[1].name = "Vắng có lý do";

                $scope.hrdata[0].legendIndex = 2;
                $scope.hrdata[1].legendIndex = 1;
                $scope.hrdata[2].legendIndex = 0;

                hrChartOptions.series = $scope.hrdata;
                $("#hr-chart").highcharts(hrChartOptions);
            })
            .error(function (data, status, headers, config) {
                console.log(data, status, headers, config);
            });
        }
        $scope.$on('scrollLeft', function (ngRepeatFinishedEvent) {
            $("#table-error-wrapper").scrollLeft($("#table-error").width());
        });
        $("#table-error-wrapper").scroll(function (evtObj)
        {
            console.log(evtObj);
            if (errorScrollStatus == null)
                errorScrollStatus = 1;
            if (errorScrollStatus == 1) {
                var errorChart = $("#error-chart").highcharts();
                var minC = errorChart.xAxis[0].dataMin;
                var maxC = errorChart.xAxis[0].dataMax;
                var percent = $("#table-error-wrapper").scrollLeft() * 100 / $("#table-error").width();
                var min = percent * (maxC - minC) / 100 + minC;
                var max = min + 2592000000;
                errorChart.xAxis[0].setExtremes(min, max);
            }
            errorScrollStatus = null;
        })
        $("#table-error-wrapper-2").scroll(function (evtObj) {
            console.log(evtObj);
            if (errorScrollStatus2 == null)
                errorScrollStatus2 = 1;
            if (errorScrollStatus2 == 1) {
                var errorChart = $("#error-chart-2").highcharts();
                var minC = errorChart.xAxis[0].dataMin;
                var maxC = errorChart.xAxis[0].dataMax;
                var percent = $("#table-error-wrapper-2").scrollLeft() * 100 / $("#table-error-2").width();
                var min = percent * (maxC - minC) / 100 + minC;
                var max = min + 2592000000;
                errorChart.xAxis[0].setExtremes(min, max);
            }
            errorScrollStatus2 = null;
        });
        $("#table-hr-wrapper").scroll(function (evtObj) {
            console.log(evtObj);
            if (hrScrollStatus == null)
                hrScrollStatus = 1;
            if (hrScrollStatus == 1) {
                var hrChart = $("#hr-chart").highcharts();
                var minC = hrChart.xAxis[0].dataMin;
                var maxC = hrChart.xAxis[0].dataMax;
                var percent = $("#table-hr-wrapper").scrollLeft() * 100 / $("#table-hr").width();
                var min = percent * (maxC - minC) / 100 + minC;
                var max = min + 2592000000;
                hrChart.xAxis[0].setExtremes(min, max);
            }
            hrScrollStatus = null;
        });
        drawTeamChart();
        drawErrorChart();
        drawHRChart();
    });
    app.directive('onFinishRender', function ($timeout) {
        return {
            restrict: 'A',
            link: function (scope, element, attr) {
                if (scope.$last === true) {
                    $timeout(function () {
                        scope.$emit(attr.onFinishRender);
                    });
                }
            }
        }
    });