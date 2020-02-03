module.controller("TeamErrorChart2Controller", function ($scope, $http) {
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
            $scope.errordata = [];
            $scope.errordata[0] = data[0];
            $scope.errordata[1] = unionError(data[1], data[3], "Thiếu nguyên phụ liệu", data[1].color);
            $scope.errordata[2] = data[4];
            $scope.errordata[3] = data[5];
        })
        .error(function (data, status, headers, config) {
            console.log(data, status, headers, config);
        });
    }
    loadData();
    function unionError(error1, error2, newName, newColor)
    {
        var error =
        {
            color: newColor,
            name: newName,
            data: []
        };
        for (var i = 0; i < error1.data.length; i++)
        {
            error.data[i] = { x: error1.data[i].x, y: error1.data[i].y != null && error2.data[i].y !=null? error1.data[i].y + error2.data[i].y :null};
        }
        console.log(error);
        return error;
    }
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
