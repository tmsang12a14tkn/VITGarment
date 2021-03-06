﻿module.controller("TeamErrorChartController", function ($scope, $http) {
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
        $http.jsonp(apiBaseUrl + "/Analytics/ReasonChart?from=" + $scope.from + "&to=" + $scope.to + "&id=" + teamId + "&callback=JSON_CALLBACK").success(function (data) {
            $scope.errordata = data;
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

        var options = {
            animation: 'slide',
            teamId: $scope.team.Id,
            factoryId: factoryId,
            date: date
        };

        myNavigator.replacePage("teamhourdetails.html", options);

    }

    $scope.goToTeamErrorChart = function () {
        modal.hide('modal');
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

module.directive('errorchart', function () {
    return {
        restrict: 'A',
        scope: {
            errordata: "=",
        },
        link: function (scope, element, attrs) {
            var errordata = scope.errordata;
            //find max
            var maxY = 0;
            for (var i = 0; i < errordata[0].data.length; i++)
            {
                var count = 0;
                for(var j = 0; j < errordata.length; j++)
                {
                    count += errordata[j].data[i].y;
                }
                if (count > maxY) maxY = count;
            }
            var chart = new Highcharts.Chart({
                chart: {
                    renderTo: element[0],
                    type: 'column',
                    zoomType: 'x'
                },
                title: {
                    text: 'Biểu đồ sự cố'
                },
                series : errordata,
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
                    //range: 604800000,
                },
                yAxis: {
                    min: 0,
                    max: maxY,
                    title: {
                        text: null
                    },
                    stackLabels: {
                        enabled: true,
                        style: {
                            fontWeight: 'bold',
                            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    },
                    opposite: true
                },

                //scrollbar: {
                //    enabled: true
                //},
                credits: {
                    enabled: false
                },
                legend:
                    {
                        itemWidth: 145,
                        margin: 5,
                        align: 'left',
                        itemStyle: {
                            fontSize:'10px',
                        }
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
            });
            scope.$watch('errordata', function (newVal, oldVal) {
                for (var i = 0; i < newVal.length; i++)
                {
                    chart.series[i].update({
                        data: newVal[i].data
                    }, false);
                }
                chart.redraw();
            });
        }
    }
});