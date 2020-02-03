module.controller("FactoryDetailsController", function ($scope, $http) {
    var factoryId = myNavigator.getCurrentPage().options.factoryId;

    var date = myNavigator.getCurrentPage().options.date;
    var dateObj = dateFromString(date);


    $scope.reverse = false;
    $scope.options = [{
        label: "Tổ May",
        value: "TeamOrder"
    }, {
        label: "Tiến độ",
        value: "PercentProcess"
    }, {
        label: "Sản phẩm",
        value: "TotalProduceQuantity"
    }, {
        label: "Tình trạng lỗi",
        value: ["-LastHour.NoError", "TeamOrder"]
    }];
    $scope.sortBy = $scope.options[0];

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

    var loadData = function () {
        $http.jsonp(apiBaseUrl + "/Goals/FindInFactory?factoryId=" + factoryId + "&date=" + date + "&includeChart=false&callback=JSON_CALLBACK").success(function (data) {
            $scope.factory = data;
            var good = $scope.factory.TotalQuantityProduct >= $scope.factory.TotalQuantityTarget;
            $scope.factory.Color = good ? 'green' : 'red';
            
            for (var i = 0; i < $scope.factory.TeamDetails.length; i++) {
                var team = $scope.factory.TeamDetails[i];
                console.log(team);
                team.PercentProcess = team.TotalQuantity == 0 ? 0 : Math.round((team.TotalQuantityProduct * 100) / team.TotalQuantity);
                var good = team.LastUpdatedProduceQuantity >= team.LastUpdatedQuantity;
                team.Color = good ? 'green' : 'red';

                var currentSession = team.GoalSessions[team.CurrentGoalSessionId];
                if (currentSession) {
                    if (currentSession.LastHour)
                        currentSession.LastHour.NoError = 0;
                    currentSession.LastHour.ProductDetails.forEach(function (currentProduct, currentIndex, array) {
                        currentSession.LastHour.NoError += currentProduct.Reasons.length;
                    });
                    team.CurrentGoalSession = currentSession;
                }
               
            };

            if ($scope.factory.TeamDetails[0].TimeLeft) {
                $scope.factory.TimePass = $scope.factory.TeamDetails[0].CurrentGoalSession.TotalHour - $scope.factory.TeamDetails[0].TimeLeft.hourToNumber();
                $scope.factory.PercentTime = Math.round($scope.factory.TimePass * 100 / $scope.factory.TeamDetails[0].CurrentGoalSession.TotalHour);

            }
            else {
                $scope.factory.TimeLeft = "0h0";
                $scope.factory.TimePass = 0;
                $scope.factory.PercentTime = 0;
            }

        });
    }
    $scope.isToday = function()
    {
        return date == new Date().toString();
    }
    $scope.changeReverse = function () {
        $scope.reverse = !$scope.reverse;
    }
    $scope.goTo = function (step) {
        for (var i = 0; i < $scope.factories.length; i++) {
            if ($scope.factories[i].Id == factoryId) {
                factoryId = $scope.factories[(i + step + $scope.factories.length) % $scope.factories.length].Id;
                break;
            };
        }
        loadData();
    }
    $scope.goToDate = function (step) {
        dateObj = dateObj.addDays(step);
        date = dateObj.toString();
        loadData();
        //var date = 
    }

    $http.jsonp(apiBaseUrl + "/Factories/GetAll?callback=JSON_CALLBACK")
            .success(function (data) {
                $scope.factories = data;
            });

    loadData();

    $scope.goToTeamDetails = function (team) {
        var options = {
            animation: 'slide', // What animation to use
            onTransitionEnd: function () { }, // Called when finishing transition animation
            teamId: team.TeamId,
            factoryId: factoryId,
            date: date
        };

        myNavigator.pushPage("teamdetails.html", options);

    }

    myNavigator.once('prepop', function (event) {
        myNavigator.replacePage('allfactories.html', { animation: 'none', date: date });
        event.cancel();
    });

    $scope.$watch('sortBy', function (newOption, oldOption) {
        var newVal = newOption.value;
        if (newVal == 'TeamOrder' || newOption == $scope.options[3])
            $scope.reverse = false;
        else if (newVal == 'PercentProcess' || newVal == 'TotalProduceQuantity')
            $scope.reverse = true;

    });
})