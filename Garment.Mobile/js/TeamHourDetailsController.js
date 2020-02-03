module.controller("TeamHourDetailsController", function ($scope, $http) {
    var options = myNavigator.getCurrentPage().options;
    var teamId = options.teamId;
    var factoryId = options.factoryId;
    var date = options.date;
    $scope.to = date;
    var toDate = dateFromString(date);
    var fromDate = toDate.addMonths(-1).addDays(1);
    $scope.from = fromDate.toString();

    myNavigator.off('prepop');

    function loadData() {
        $http.jsonp(apiBaseUrl + "/Teams/GetById?teamId=" + teamId + "&callback=JSON_CALLBACK").success(function (data) {
            $scope.team = data;
        })

        $http.jsonp(apiBaseUrl + "/Analytics/HourDetailsChart?id=" + teamId + "&date=" + date + "&callback=JSON_CALLBACK").success(function (data) {
            $scope.hoursdata = data;
        })
        .error(function (data, status, headers, config) {
            console.log(data, status, headers, config);
        });
    }
    loadData();
    $http.jsonp(apiBaseUrl + "/Teams/GetAllByFactoryId?factoryId=" + factoryId + "&callback=JSON_CALLBACK").success(function (data) {
        $scope.teams = data;
    });
    $scope.goTo = function (step) {
        for (var i = 0; i < $scope.teams.length; i++) {
            if ($scope.teams[i].Id == $scope.team.Id) {
                teamId = $scope.teams[(i + step + $scope.teams.length) % $scope.teams.length].Id;
                break;
            };
        }
        loadData();
    }

    $scope.goToTeamOverview = function () {
        modal.hide('modal');

        var options = {
            animation: 'slide',
            teamId: $scope.team.Id,
            factoryId: factoryId,
            date: date
        };

        myNavigator.replacePage("teamdetails.html", options);
    }

    $scope.goToHoursChart = function () {
        modal.hide('modal');

    }

    $scope.goToTeamErrorChart = function () {
        modal.hide('modal');
        var options = {
            animation: 'slide',
            teamId: $scope.team.Id,
            factoryId: factoryId,
            date: date
        };

        myNavigator.replacePage("teamerrorchart.html", options);
    }

    $scope.goToTeamErrorChart2 = function () {
        modal.hide('modal');

        var options = {
            animation: 'slide',
            teamId: $scope.team.Id,
            factoryId: factoryId,
            date: date
        };

        myNavigator.replacePage("teamerrorchart2.html", options);
    }

    $scope.goToTeamEmployeeChart = function () {
        modal.hide('modal');
        var options = {
            animation: 'slide',
            teamId: $scope.team.Id,
            factoryId: factoryId,
            date: date
        };

        myNavigator.replacePage("teamemployeechart.html", options);
    }

    myNavigator.off('prepop');
    myNavigator.once('prepop', function (event) {
        //var page = event.currentPage; // Get current page object
        //console.log(event);
        //event.enterPage.options.date = date;
        myNavigator.replacePage('factorydetails.html', { animation: 'none', factoryId: factoryId, date: date });
        event.cancel();

    });
});

module.directive('hourschart', function () {
    return {
        restrict: 'A',
        scope: {
            hoursdata: "=",
        },
        link: function (scope, element, attrs) {
            var hoursdata = scope.hoursdata;
            //find max
            var maxY = 0;
            for (var i = 0; i < hoursdata.Series[0].data.length; i++) {
                var count = 0;
                for (var j = 0; j < hoursdata.Series.length - 1; j++) {
                    count += hoursdata.Series[j].data[i].y;
                }
                if (count > maxY) maxY = count;
            }
            var chart = new Highcharts.Chart({
                chart: {
                    renderTo: element[0],
                    type: 'column',
                },
                credits: {
                    enabled: false
                },
                title: {
                    text: 'Biểu đồ sản xuất theo giờ'
                },
                series: hoursdata.Series,
                //scrollbar: {
                //    enabled: true,
                //    liveRedraw: false
                //},
                xAxis:
                {
                    type: "category",
                    categories: hoursdata.Categories
                },
                //yAxis: {
                //    min: 0,
                //    max: maxY,
                //    title: {
                //        text: null
                //    },
                //    stackLabels: {
                //        enabled: true,
                //        style: {
                //            fontWeight: 'bold',
                //            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                //        }
                //    },
                //    opposite: true
                //},
                //legend:
                //    {
                //        itemWidth: 145,
                //        margin: 5,
                //        align: 'left',
                //        itemStyle: {
                //            fontSize: '10px',
                //        }
                //    },
                tooltip: {
                    shared: true,
                    //formatter: function () {
                    //    var points = this.points;
                    //    var pointsLength = points.length;

                    //    var tooltipMarkup = pointsLength ? '<span style="font-size: 10px">' + Highcharts.dateFormat('%d/%m/%Y', points[0].key) + '</span><br/>' : '';
                    //    var index;

                    //    for (index = 0; index < pointsLength; index += 1) {
                    //        tooltipMarkup += '<span style="color:' + points[index].series.color + '">\u25CF</span> ' + points[index].series.name + ': <b>' + points[index].y + '</b><br/>';
                    //    }

                    //    return tooltipMarkup;
                    //}
                },
                //plotOptions: {
                //    series:
                //    {
                //        pointPadding: 0,
                //        groupPadding: 0.01
                //    },
                //    column: {
                //        stacking: 'normal',
                //        dataLabels: {
                //            enabled: true,
                //            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
                //            style: {
                //                textShadow: '0 0 3px black'
                //            }
                //        }
                //    }
                //}
            });
            scope.$watch('hoursdata', function (newVal, oldVal) {
                for (var i = 0; i < newVal.length; i++) {
                    chart.series[i].update({
                        data: newVal[i].data
                    }, false);
                }
                chart.redraw();
            });
        }
    }
});