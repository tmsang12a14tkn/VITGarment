function registerEvent() {
    $('a.chart4-btn').on('shown.bs.tab', function (e) {
        var $tab = $(e.target);
        var teamId = $tab.data('teamid');
        var $tabContent = $($tab.attr('href'));
        $tabContent.load("/QCReport/_ChartForTeam?teamId=" + teamId + "&date=" + date);
    })
}


$('#viewQCReportModal').on('hidden.bs.modal', function (e) {
    $(this).find('.modal-content').empty();
})
$('#viewQCReportModal').on('show.bs.modal', function (e) {
    $(this).find('.modal-content').load($(e.relatedTarget).data('href'));
})

$("#btndatepicker").on("click", function () {
    $('#datepicker').datetimepicker('toggle');
});


Number.prototype.padLeft = function (base, chr) {
    var len = (String(base || 10).length - String(this).length) + 1;
    return len > 0 ? new Array(len).join(chr || '0') + this : this;
}

function drawchart(team) {
    team.GoalSessions.forEach(function (session, key) {
        var targetData = []; var achievementData = [];
        var timeData = [];
        var availableEmpData = []; var absentEmpData = [];
        var currentHour = new Date().getHours() + 'h';
        console.log(session);
        session.GoalDetails.forEach(function (detail, key) {
            var produceQuantity = detail.ProduceQuantity == 0 ? null : detail.ProduceQuantity;
            targetData.push(detail.Quantity);
            achievementData.push(produceQuantity);
            timeData.push(detail.EndCountTimeString);
            availableEmpData.push(detail.IsPass ? detail.NoEmployee : null);
            absentEmpData.push(detail.IsPass ? detail.TotalEmployee - detail.NoEmployee : null);
            
        })
        var currentHourIndex = timeData.indexOf(currentHour);
        
        $("#team_" + team.TeamId +"_order_"+ key +"_hourchart").highcharts({
            title: {
                text: 'Biểu đồ chi tiết công việc theo giờ',
                x: -20 //center
            },

            xAxis: {
                categories: timeData,
                plotLines: [{
                    color: 'red', // Color value
                    dashStyle: 'longdashdot', // Style of the plot line. Default to solid
                    value: currentHourIndex, // Value of where the line will appear
                    width: 2 // Width of the line
                }]
            },
            yAxis: {
                min: 0,
                minPadding: 1,
                title: {
                    text: 'Số sản phẩm'
                },
                //plotLines: [{
                //    value: 0,
                //    width: 1,
                //    color: '#808080'
                //}]
            },
            tooltip: {
                shared: true,
                crosshairs: true,
                formatter: function () {
                    var s = '<b>' + this.x + '</b>';

                    $.each(this.points, function () {
                        s += '<br/><span style="font-weight:bold; color:' + this.series.color + '">' + this.series.name + ': </span>' + this.point.y;
                    });
                    if (this.points.length == 2) {
                        var color = this.points[1].point.y >= this.points[0].point.y ? "green" : "red";
                        s += '<br/><b style="color:' + color + '">Lệch: ' + (this.points[1].point.y - this.points[0].point.y) + '</b>';
                    }
                    return s;
                }
            },
            series: [{
                name: 'Mục tiêu',
                data: targetData
            }, {
                name: 'Thực tế',
                data: achievementData
            }]
        });

        $("#team_" + team.TeamId  +"_order_"+ key + "_empchart").highcharts({
            chart:
            {
                type: 'column'
            },
            title: {
                text: 'Biểu đồ công nhân theo giờ',
                x: -20 //center
            },

            xAxis: {
                categories: timeData,
            },
            yAxis: {
                min: 0,
                minPadding: 1,
                title: {
                    text: 'Số công nhân'
                },
                stackLabels: {
                    enabled: true,
                },
            },
            plotOptions: {
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
            },
            series: [
                {
                    name: 'Vắng mặt',
                    data: absentEmpData,
                    color: 'red'
                },
                {
                    name: 'Có mặt',
                    data: availableEmpData,
                    color: 'green'
                },
            ]
        });
    });
    
}

var app = angular.module("garmentApp", []);
app.controller("goalscontroller", function ($scope, $interval, $http, $sce) {
    var update = function () {
        $scope.factoryId = factoryId;
        $scope.predate = predate;
        $scope.nextdate = nextdate;
        $scope.iscurrentdate = iscurrentdate;
        $http.get("/api/Goals/FindInFactory?factoryId=" + factoryId + "&date=" + date)
        .then(function (response) {
            $scope.teams = response.data.TeamDetails;
            $scope.factoryName = response.data.FactoryName;
            $scope.factoryPicture = response.data.FactoryPicture;
            $scope.TotalQuantity = response.data.TotalQuantity;
            $scope.TotalQuantityProduct = response.data.TotalQuantityProduct;
            $scope.PercentProcess = response.data.PercentProcess;
            $scope.TotalNoEmployeeDefault = response.data.TotalNoEmployeeDefault;
            $scope.TotalNoEmployeeAvailable = response.data.TotalNoEmployeeAvailable;
            $scope.TeamCount = response.data.TeamCount;
            $scope.date = response.data.Date;
            $scope.dayofweek = $sce.trustAsHtml(response.data.DayOfWeek);
            $scope.predate = predate;
            $scope.nextdate = nextdate;
            $scope.iscurrentdate = iscurrentdate;
            document.title = $scope.factoryName + " - VIT Garment";

            groupByProduct();

            setTimeout(function () {
                $('[data-toggle="tooltip"]').tooltip({ trigger: 'manual' }).tooltip("show");
                for (var i = 0; i < $scope.teams.length; i++) {
                    var team = $scope.teams[i];
                    drawchart(team);
                }
                $('.chart2-btn').on('shown.bs.tab', function (e) {
                    window.dispatchEvent(new Event('resize'));
                });

                $('[data-toggle="popover"]').popover();

                registerEvent();

            }, 100);

           

            jQuery('#datepicker').datetimepicker(
            {
                timepicker: false,
                format: 'd/m/Y',
                value: $scope.date,
                maxDate: '+1970/01/01',
                onChangeDateTime: function (dp, $i) {
                    window.location = "/Factories/Details?id=" + factoryId + "&date=" + $i.val();
                }
            });

            $http.get("/api/Factories/GetAll")
            .then(function (response) {
                var factories = response.data;
                for (var i = 0; i < factories.length; i++)
                {
                    if (factories[i].Id == factoryId)
                    {
                        $scope.nextFactoryId = factories[(i + 1) % factories.length].Id;
                        $scope.preFactoryId = factories[(i - 1 + factories.length) % factories.length].Id;
                        break;
                    };
                }
            });

        });
    }
    var groupByProduct = function () {
        for(var i = 0;i<$scope.teams.length;i++)
        {
            var team = $scope.teams[i];
            angular.forEach(team.GoalSessions, function (session, sIndex) {
                var dictProducts = {};
                angular.forEach(session.GoalDetails, function (goalDetail, dIndex) {
                    angular.forEach(goalDetail.ProductDetails, function (product, pIndex)
                    {
                        if (dictProducts[product.ProductId] == null)
                            dictProducts[product.ProductId] = {
                                ProduceQuantity : product.ProduceQuantity,
                                Quantity: product.Quantity
                            };
                        else
                        {
                            dictProducts[product.ProductId].ProduceQuantity += product.ProduceQuantity;
                            dictProducts[product.ProductId].Quantity += product.Quantity;
                        }
                    })

                });
                var products = [];
                angular.forEach(dictProducts, function (product, productId) {
                    this.push({
                        ProductId: productId,
                        ProduceQuantity: product.ProduceQuantity,
                        Quantity: product.Quantity
                    });
                }, products);
                session.Products = products;
            });
        }
    };
    update();
    $scope.haveComments = function (comments) {
        return comments!=null && comments.some(function (element, index) {
            return element.Comment;
        });
    }
    $scope.haveReasons = function (preasons) {
        return preasons != null && preasons.some(function (preason, index) {
            return preason.Reasons.some(function (reason, index) {
                return reason;
            });
        });
    }

    $scope.getReasons = function (goaldetail)
    {
        var reasons = "<table class='table table-responsive table-hover table-bordered'><tbody>";
        for (var i = 0 ; i < goaldetail.ProductDetails.length; i++)
        {
            var productDetail = goaldetail.ProductDetails[i];
            if (productDetail.Reasons.length > 0) {
                reasons += "<tr><td rowspan=" + productDetail.Reasons.length + ">" + productDetail.ProductId + "</td>";

                for (var j = 0; j < productDetail.Reasons.length; j++)
                    reasons += "<td>" + productDetail.Reasons[j].Name + "</td>";
                reasons += "</tr>";
            }
        }
        reasons += "</tbody></table>"
        return reasons;
    }

    $scope.getComments = function (goaldetail)
    {
        var comments = "<table class='table table-responsive table-hover table-bordered'><tbody>";
        for (var i = 0 ; i < goaldetail.ProductDetails.length; i++) {
            var productDetail = goaldetail.ProductDetails[i];
            if (productDetail.Comment)
            {
                comments += "<tr><td>" + productDetail.ProductId + "</td><td>" + productDetail.Comment + "</td></tr>";
            }
        }
        comments += "</tbody></table>"
        return comments;
    }
    $interval(update, 100000);
});


app.filter('abs', function () {
    return function (val) {
        return Math.abs(val);
    }
});

var selectedPoints = {};
var secondSelectedpoints = {};
var selectedHours = {};
app.directive('quantityTooltip', function () {
    return {
        restrict: 'A',
        scope: {
            hour: '&',
            teamId: '&'
        },
        link: function (scope, element, attrs) {
            var hour = scope.hour();
            var teamId = scope.teamId();

            if (hour.IsPass == false && hour.HasData) {
                $(element).click(function () {
                    //an di
                    if ($(element).attr('data-click-state') == 1) {
                        $(element).attr('data-click-state', 0);
                        $(element).removeClass("active");
                        $("#" + $(element).data('tooltiptextid')).hide();
                        selectedPoints[teamId] = null;
                    }
                        //hien len
                    else {
                        //Hide previous select
                        if (selectedHours[teamId] != null) {
                            $(selectedHours[teamId]).attr('data-click-state', 0);
                            selectedHours[teamId] = null;
                        }
                        if (selectedPoints[teamId] != null) {
                            $(selectedPoints[teamId]).attr('data-click-state', 0);
                            $(selectedPoints[teamId]).removeClass("active");

                            $("#" + $(selectedPoints[teamId]).data('tooltiptextid')).hide();

                            var $selectedRange = $("#pq-" + teamId + "-" + hour.EndTime);
                            $selectedRange.removeClass("selected");

                            var $rangeDif = $("#pt-header-" + teamId).find(".range-difference");
                            $rangeDif.hide();
                        }
                        if (secondSelectedpoints[teamId] != null) {
                            $(secondSelectedpoints[teamId]).attr('data-click-state', 0);
                            $(secondSelectedpoints[teamId]).removeClass("active");

                            $("#" + $(secondSelectedpoints[teamId]).data('tooltiptextid')).hide();
                        }
                        $("#pt-body-" + teamId + " .progress-bar").removeClass("selected");


                        $(element).attr('data-click-state', 1);
                        $(element).addClass("active");
                        $("#" + $(element).data('tooltiptextid')).show();

                        selectedPoints[teamId] = element;
                    }
                    return false;
                });

                $(element).hover(function () {
                    //neu hover len 1 diem chua dc click
                    if (selectedHours[teamId] == null && $(element).attr('data-click-state') != 1 && $(element).attr('data-click-state') != 2) {
                        $(element).addClass("active");
                        $("#" + $(element).data('tooltiptextid')).show();
                        if (selectedPoints[teamId] != null) {
                            var left1 = $("#" + $(selectedPoints[teamId]).data('tooltiptextid')).data("pos");
                            var value1 = $("#" + $(selectedPoints[teamId]).data('tooltiptextid')).text();
                            var left2 = $("#" + $(element).data('tooltiptextid')).data("pos");
                            var value2 = $("#" + $(element).data('tooltiptextid')).text();

                            var minLeft = Math.min(left1, left2);
                            var maxLeft = Math.max(left1, left2);

                            $("#pt-body-" + teamId + " .progress-bar-quantity").filter(function () {
                                var pos = $(this).data("pos");
                                return pos > minLeft && pos <= maxLeft;
                            }).addClass("selected");

                            var $rangeDif = $("#pt-header-" + teamId).find(".range-difference");
                            $rangeDif.css("left", "calc(" + ((left1 + left2) / 2) + "% - 15px)");
                            $rangeDif.text(Math.abs(value2 - value1));
                            $rangeDif.show();
                        }
                    }
                }, function () {
                    //neu roi khoi len 1 diem chua dc click
                    if ($(element).attr('data-click-state') != 1 && $(element).attr('data-click-state') != 2) {
                        $(element).removeClass("active");
                        $("#" + $(element).data('tooltiptextid')).hide();
                        if (selectedPoints[teamId] != null) {
                            var $selectedRange = $("#pq-" + teamId + "-" + hour.EndTime);
                            $selectedRange.removeClass("selected");

                            var $rangeDif = $("#pt-header-" + teamId).find(".range-difference");
                            $rangeDif.hide();
                        }
                    }
                });
            }
        }
    };
});

app.directive('hourTooltip', function () {
    return {
        restrict: 'A',
        scope: {
            hour: "&",
            teamId: "&",
            preHour: "&"
        },
        link: function (scope, element, attrs) {
            var hour = scope.hour();
            var teamId = scope.teamId();
            var preHour = scope.preHour();
            if (!hour.IsPass && hour.HasData) {
                $(element).on('click', function () {
                    //Hide
                    if ($(element).attr('data-click-state') == 1) {
                        $(element).attr('data-click-state', 0);
                        selectedHours[teamId] = null;

                        //disable tooltip text
                        $("#tooltiptext_" + teamId + "_hour_" + hour.EndTime).hide();
                        if (preHour) {
                            $("#tooltiptext_" + teamId + "_hour_" + preHour.EndTime).hide();
                        }
                        //disable tooltip
                        if (selectedPoints[teamId] != null) {
                            $(selectedPoints[teamId]).attr('data-click-state', 0);
                            $(selectedPoints[teamId]).removeClass("active");
                            selectedPoints[teamId] = null;
                        }
                        if (secondSelectedpoints[teamId] != null) {
                            $(secondSelectedpoints[teamId]).attr('data-click-state', 0);
                            $(secondSelectedpoints[teamId]).removeClass("active");
                            secondSelectedpoints[teamId] = null;
                        }
                        //hide range diff
                        var $rangeDif = $("#pt-header-" + teamId).find(".range-difference");
                        $rangeDif.hide();

                        //hide selected range
                        var $selectedRange = $("#pq-" + teamId + "-" + hour.EndTime);
                        $selectedRange.removeClass("selected");
                    }
                        //Show
                    else {
                        //Hide previous select
                        if (selectedHours[teamId]) {
                            $(selectedHours[teamId]).attr('data-click-state', 0);
                        }

                        if (selectedPoints[teamId] != null) {
                            $(selectedPoints[teamId]).attr('data-click-state', 0);
                            $(selectedPoints[teamId]).removeClass("active");

                            $("#" + $(selectedPoints[teamId]).data('tooltiptextid')).hide();
                        }
                        if (secondSelectedpoints[teamId] != null) {
                            $(secondSelectedpoints[teamId]).attr('data-click-state', 0);
                            $(secondSelectedpoints[teamId]).removeClass("active");

                            $("#" + $(secondSelectedpoints[teamId]).data('tooltiptextid')).hide();
                        }
                        $("#pt-body-" + teamId + " .progress-bar-quantity").removeClass("selected");

                        $(element).attr('data-click-state', 1);
                        selectedHours[teamId] = element;
                        //show tooltip and tootiptext for selected
                        selectedPoints[teamId] = $(element).siblings(".end-point")[0];
                        $(selectedPoints[teamId]).attr('data-click-state', 2);
                        $(selectedPoints[teamId]).addClass("active");
                        $("#" + $(selectedPoints[teamId]).data('tooltiptextid')).show();

                        //show tooltip and tootiptext for pre selected
                        if (preHour) {
                            //active tooltip
                            secondSelectedpoints[teamId] = document.getElementById("quantity_tooltip_" + teamId + "_" + preHour.EndTime);
                            $(secondSelectedpoints[teamId]).attr('data-click-state', 2);
                            $(secondSelectedpoints[teamId]).addClass("active");

                            //show tooltip text
                            $("#" + $(secondSelectedpoints[teamId]).data('tooltiptextid')).show();
                        }
                        else {
                            secondSelectedpoints[teamId] = null;
                        }

                        //show rang dif

                        var $rangeDif = $("#pt-header-" + teamId).find(".range-difference");
                        var left1 = $("#" + $(selectedPoints[teamId]).data('tooltiptextid')).data("pos");
                        var value1 = $("#" + $(selectedPoints[teamId]).data('tooltiptextid')).text();

                        if (preHour) {
                            var left2 = $("#" + $(secondSelectedpoints[teamId]).data('tooltiptextid')).data("pos");
                            var value2 = $("#" + $(secondSelectedpoints[teamId]).data('tooltiptextid')).text();

                            $rangeDif.css("left", "calc(" + ((left1 + left2) / 2) + "% - 15px)");
                            $rangeDif.text(Math.abs(value2 - value1));
                            $rangeDif.show();

                            var minLeft = Math.min(left1, left2);
                            var right = 100 - Math.max(left1, left2);

                            var $selectedRange = $("#pq-" + teamId + "-" + hour.EndTime);
                            $selectedRange.addClass("selected");
                        }
                        else {
                            $rangeDif.hide();

                            var $selectedRange = $("#pq-" + teamId + "-" + hour.EndTime);
                            $selectedRange.addClass("selected");
                        }
                    }
                });
            }
        }
    };
});
//function fixDiv() {
//    var $cache = $('.fixed-top');
//    var top = $(window).scrollTop();
//    if (top > 100)
//        $cache.css({
//            'position': 'absolute',
//            'top': (top-40) + 'px'
//        });
//    else
//        $cache.css({
//            'position': 'relative',
//            'top': 'auto'
//        });
//}

//$(function () {
//    $(window).scroll(fixDiv);
//    fixDiv();
//});


//function sticky_relocate() {
//    var window_top = $(window).scrollTop();
//    var div_top = $('#sticky-anchor').offset().top;
//    if (window_top > div_top) {
//        $('#sticky').addClass('stick');
//        $('#sticky-anchor').height($('#sticky').outerHeight());
//    } else {
//        $('#sticky').removeClass('stick');
//        $('#sticky-anchor').height(0);
//    }
//}

//$(function () {
//    $(window).scroll(sticky_relocate);
//    sticky_relocate();
//});