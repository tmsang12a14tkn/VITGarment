module.controller("TeamDetailsController", function ($scope, $http) {
    var options = myNavigator.getCurrentPage().options;
    var factoryId = options.factoryId;
    $scope.teamId = options.teamId;
    
    $scope.datePickerOptions = {
        format: 'dd/mm/yyyy',
        today: 'Hôm nay',
        clear: '',
        close: 'Đóng',
        max: true,
        onSet: function (context) {
            if (context.select) {
                dateObj = new Date(context.select);
                date = dateObj.toString();
                loadData();
            }
        }
    }

    $scope.empPieChartOptions = {
        easing: 'easeOutBounce',
        barColor: ['#2BB673', 'orange'],
        trackColor: '#bd2212',
        scaleColor: false,
        lineWidth: 8,
        //lineCap: 'butt'
    };


    var date = options.date;
    var dateObj;
    if (date == null) {
        dateObj = new Date();
        date = dateObj.toString();
    }
    else
        var dateObj = dateFromString(date);
    
    var loadData = function () {
        $http.jsonp(apiBaseUrl + "/Goals/GoalFind?teamId=" + $scope.teamId + "&date=" + date + "&includeChart=true&callback=JSON_CALLBACK").success(function (data) {
            $scope.team = data;
            $scope.team.Date = date;
            $scope.team.DayOfWeek = dateObj.dayOfWeek();
            if ($scope.team.TimeLeft) {
                $scope.team.TimePass = $scope.team.TotalHour - $scope.team.TimeLeft.hourToNumber();
                $scope.team.PercentTime = Math.round($scope.team.TimePass * 100 / $scope.team.TotalHour);

            }
            else {
                $scope.team.TimeLeft = "0h0";
                $scope.team.TimePass = 0;
                $scope.team.PercentTime = 0;
            }
            $scope.team.AbsentWithReason = $scope.team.TotalEmployee - $scope.team.CurrentNoEmployee - $scope.team.AbsentWithoutReason;
            $scope.team.PercentEmployee = $scope.team.TotalEmployee == 0 ? 0 : Math.round(($scope.team.CurrentNoEmployee * 100) / $scope.team.TotalEmployee);
            $scope.team.PercentAbsentWithReason = $scope.team.TotalEmployee == 0 ? 0 : Math.round($scope.team.AbsentWithReason * 100 / $scope.team.TotalEmployee);
            $scope.team.PercentProcess = $scope.team.TotalQuantity == 0 ? 0 : Math.round(($scope.team.LastUpdatedProduceQuantity * 100) / $scope.team.TotalQuantity);
            var good = $scope.team.LastUpdatedProduceQuantity >= $scope.team.LastUpdatedQuantity;
            $scope.team.Color = good ? 'green' : 'red';
            $scope.team.PieChartOptions = {
                easing: 'easeOutBounce',
                //barColor: '#3498db',
                trackColor: '#f2f2f2',
                scaleColor: false,
                lineWidth: 8,
                barColor: good ? '#2BB673' : '#bd2212',
                //lineCap: 'square'
            };
            //$('[data-toggle="tooltip"]').tooltip({ trigger: 'manual' }).tooltip("show");
        });
    }
    loadData();
    $http.jsonp(apiBaseUrl + "/Teams/GetAllByFactoryId?factoryId=" + factoryId + "&callback=JSON_CALLBACK").success(function (data) {
        $scope.teams = data;
    });

    $scope.isToday = function () {
        return date == new Date().toString();
    }
    $scope.goTo = function (step) {
        console.log($scope.teams);
        for (var i = 0; i < $scope.teams.length; i++) {
            if ($scope.teams[i].Id == $scope.teamId) {
                $scope.teamId = $scope.teams[(i + step + $scope.teams.length) % $scope.teams.length].Id;
                break;
            };
        }
        loadData();
    }

    $scope.goToDate = function (step) {
        dateObj = dateObj.addDays(step);
        date = dateObj.toString();
        loadData();
    }

    $scope.haveComments = function (comments) {
        return comments != null && comments.some(function (element, index) {
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
    $scope.goToTeamOverview = function()
    {
        modal.hide('modal');
    }
    $scope.goToHoursChart = function () {
        modal.hide('modal');

        var options = {
            animation: 'slide',
            teamId: $scope.team.TeamId,
            factoryId: factoryId,
            date: date
        };

        myNavigator.replacePage("teamhourdetails.html", options);

    }
    
    $scope.goToTeamErrorChart = function () {
        modal.hide('modal');

        var options = {
            animation: 'slide',
            teamId: $scope.team.TeamId,
            factoryId : factoryId,
            date: date
        };

        myNavigator.replacePage("teamerrorchart.html", options);

    }
    $scope.goToTeamErrorChart2 = function () {
        modal.hide('modal');

        var options = {
            animation: 'slide',
            teamId: $scope.team.TeamId,
            factoryId: factoryId,
            date: date
        };

        myNavigator.replacePage("teamerrorchart2.html", options);

    }
    $scope.goToTeamEmployeeChart = function () {
        modal.hide('modal');

        var options = {
            animation: 'slide',
            teamId: $scope.team.TeamId,
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
})
