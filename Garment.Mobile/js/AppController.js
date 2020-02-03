
var apiBaseUrl = 'http://vitgarment.vitcorp.vn/api';
var module = ons.bootstrap(['easypiechart', 'angular-datepicker']);

module.controller('MainScreenController', function ($scope, $http) {
    var dateObj = new Date();
    if (myNavigator.getCurrentPage().options.date) {
        $scope.currentDate = myNavigator.getCurrentPage().options.date;
        dateObj = dateFromString($scope.currentDate);
    }
    else {
        $scope.currentDate = dateObj.toString();
    }
    $scope.dayOfWeek = dateObj.dayOfWeek();

    $scope.datePickerOptions = {
        format: 'dd/mm/yyyy',
        today: 'Hôm nay',
        clear: '',
        close: 'Đóng',
        max: true,
        onSet: function (context) {
            if (context.select) {
                dateObj = new Date(context.select);
                $scope.currentDate = dateObj.toString();
                $scope.loadData();
            }
        }
    }
    $scope.isToday = function () {
        return $scope.currentDate == new Date().toString();
    }
    $scope.loadData = function () {
        $http.jsonp(apiBaseUrl + "/goals/findallfactory?date=" + $scope.currentDate + "&callback=JSON_CALLBACK").success(function (data) {
            $scope.factories = data;
            for (var i = 0; i < $scope.factories.length; i++) {
                var factory = $scope.factories[i];
                var good = factory.TotalQuantityProduct >= factory.TotalQuantityTarget;
                var noData = factory.TotalQuantityProduct == 0 && factory.TotalQuantityTarget == 0;
                factory.Color = noData ? 'gray' : good ? 'green' : 'red';
                factory.pieChartOptions = {
                    barColor: noData ? '#808285' : good ? '#2BB673' : '#EF4541',
                    trackColor: '#f2f2f2',
                    scaleColor: false,
                    lineWidth: 8,
                };
            }
        });
    }
    $scope.goToDate = function (step) {
        dateObj = dateObj.addDays(step);
        $scope.dayOfWeek = dateObj.dayOfWeek();
        $scope.currentDate = dateObj.toString();
        $scope.loadData();
        //var date = 
    }

    //$scope.loadData();

    $scope.goToFactoryDetails = function (factoryId) {
        var options = {
            animation: 'slide', // What animation to use
            onTransitionEnd: function () { }, // Called when finishing transition animation
            factoryId: factoryId,
            date: $scope.currentDate,
            //factories: $scope.factories
        };

        myNavigator.pushPage("factorydetails.html", options);

    }
});

module.filter('abs', function () {
    return function (val) {
        return Math.abs(val);
    }
});

module.directive('highcharts', function () {
    return {
        restrict: 'A',
        scope: {
            team: "&",
        },
        link: function (scope, element, attrs) {
            var team = scope.team();
            var targetData = []; var achievementData = [];
            var timeData = [];
            var currentHour = new Date().getHours() + 'h';
            team.GoalDetails.forEach(function (detail, key) {
                var produceQuantity = detail.ProduceQuantity == 0 ? null : detail.ProduceQuantity;
                targetData.push(detail.Quantity);
                achievementData.push(produceQuantity);
                timeData.push(detail.EndCountTimeString);
            })
            var currentHourIndex = timeData.indexOf(currentHour);

            new Highcharts.Chart({
                chart: {
                    renderTo: element[0]
                },
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
                    labels: {
                        enabled: false
                    },
                    title: {
                        text: null
                    },
                    plotLines: [{
                        value: 0,
                        width: 1,
                        color: '#808080'
                    }]
                },
                tooltip: {
                    shared: true,
                    crosshairs: true,
                    formatter: function () {
                        var s = '<b>' + this.x + '</b>';

                        for (var i = 0; i < this.points.length; i++) {
                            var point = this.points[i];
                            s += '<br/><span style="font-weight:bold; color:' + point.series.color + '">' + point.series.name + ': </span>' + point.point.y;
                        };
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
        }
    }
});

