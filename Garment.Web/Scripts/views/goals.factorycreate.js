var app = angular.module("garmentApp", [], function () {
});
//var now = new Date();
//var dow = now.getDay();

var timeFromString = function(str)
{
    var peices = split(':');
    var housr = minutes = seconds = 0;
    if (pieces.length === 3) {
        hours = parseInt(pieces[0], 10);
        minutes = parseInt(pieces[1], 10);
        seconds = parseInt(pieces[2], 10);
    }
    return { Hours: hour, Minutes: minutes, Seconds: senconds };
}

jQuery('#datepicker').datetimepicker(
    {
        timepicker: false,
        format: 'd/m/Y',
        value: date,
        onChangeDateTime: function (dp, $i) {
            window.location = "/Goals/FactoryCreate?factoryId=" + factoryId + "&date=" + $i.val();
        }
    });
$("#btndatepicker").on("click", function () {
    $('#datepicker').datetimepicker('toggle');
});


//angular
app.controller("goalscontroller", function ($scope, $http, $filter) {

    $scope.initialgroups = null;
    $scope.isLoadFirst = true;
    $scope.factoryId = factoryId;
    $scope.date = date;
    $scope.datatemp = [
          { Team: null, ProductDetail: null, Position: null, X: null, Y: null }
    ];
    $scope.sessionDayOptions =
        [
            { value : 0, text: "Hôm nay"},
            { value: 1, text: "Ngày mai" },
        ]



    var checkValidProduct = function (goaldetail) {
        if (goaldetail.ManualProduceQuantity == 0) {
            goaldetail.ValidProduct = true;
            return;
        }

        for (var i = 0; i < $scope.products.length; i++) {
            if ($scope.products[i].ProductId == goaldetail.ProductId) {
                goaldetail.ValidProduct = true;
                return;
            }

        }
        goaldetail.ValidProduct = false;
    }

    $http.get("/api/Products/GetAll").then(function (response) {
        $scope.products = response.data;
    });
    $http.get("/api/Reasons/GetAll").then(function (response) {
        $scope.reasons = response.data;
    });
    var reloadTeams = function (complete) {
        $http.get("/api/Teams/GetAllByFactoryId?factoryId=" + factoryId).then(function (response) {
            $scope.teams = response.data;
            if (complete)
                complete();
        });
    }
    var reloadFactorySessions = function (complete) {
        $http.get("/api/Goals/GetSessions?factoryId=" + factoryId + "&date=" + date).then(function (response) {
            $scope.factorySessions = response.data;
            $scope.numSessions = $scope.factorySessions.length > 1 ? 3 : 2;
            //tinh toan otherSessionGroups
            console.log("factorySessions: " + $scope.factorySessions[0].StartStr);
            console.log("factorySessions: " + $scope.factorySessions[0].EndStr);
            calculateOtherSessionGroups();
            
            initFactorySessionNew();
            
            if (complete)
                complete();
        });
    }
    var calculateOtherSessionGroups = function()
    {
        $scope.otherSessionGroups = {};
        for (var i = 1; i < $scope.factorySessions.length; i++) {
            var teamSessions = $scope.factorySessions[i].TeamSessions;
            for (j = 0; j < teamSessions.length; j++) {
                var teamSession = teamSessions[j];
                if ($scope.otherSessionGroups[teamSession.TeamName] == null) {
                    $scope.otherSessionGroups[teamSession.TeamName] = [teamSession];
                }
                else
                    $scope.otherSessionGroups[teamSession.TeamName].push(teamSession);
            }
        }
    }
    var initFactorySessionNew = function()
    {
        if ($scope.teams == null)
            reloadTeams(initFactorySessionNew);
        else {
            $scope.fSessionNew =
                    {
                        Type: 0,
                        Start: { Days: 0, Hours: 0, Minutes: 0 },
                        End: { Days: 0, Hours: 0, Minutes: 0 },
                        Date: $scope.factorySessions[0].Date,
                        FactoryId: factoryId,
                        //Order: $scope.factorySessions.length + 1,
                    }
            $scope.fSessionNew.TeamSessions = [];
            for (var i = 0; i < $scope.teams.length; i++) {
                var team = $scope.teams[i];
                $scope.fSessionNew.TeamSessions.push({
                    TeamId: team.Id,
                    TeamName: team.Name,
                    Start: {Days : 0},
                    End: { Days: 0},
                });
            }
        }
    }
    var reloadFactoryQuantity = function () {
        $http.get("/api/Goals/FactoryQuantity?factoryId=" + factoryId + "&date=" + date).then(function (response) {
            $scope.factoryQuantity = response.data;
            console.log("factoryQuantity");
            console.log($scope.factoryQuantity);
            $scope.oriFactoryQuantity = angular.copy(response.data);

            calculateNoHours_Sessions();
        });
    }
    var calculateNoHours_Sessions = function()
    {

        for (var i = 0; i < $scope.factorySessions.length; i++) {
            var session = $scope.factorySessions[i];
            session.NoHours = 0;
            for (var j = 0; j < $scope.factoryQuantity.QuantityHours.length; j++) {
                var hour = $scope.factoryQuantity.QuantityHours[j];
                if (hour.Start.Days * 24 + hour.Start.Hours >= session.Start.Days * 24 + session.Start.Hours
                    && hour.End.Days * 24 + hour.End.Hours <= session.End.Days * 24 + session.End.Hours)
                    session.NoHours++;
            }
            console.log("test" + i + ": " + $scope.factorySessions[i].NoHours);
        }
        
    }
    //$http.get("/api/Goals/FactoryNoEmployee?factoryId=" + factoryId + "&date=" + date).then(function (response) {
    //    $scope.factoryNoEmployee = response.data;
    //    $scope.oriFactoryNoEmployee = angular.copy(response.data);
    //    //console.log(response.data);
    //});

    var reloadFactoryHours = function (complete) {
        $http.get("/api/Goals/FactoryFind?factoryId=" + factoryId + "&date=" + date).then(function (response) {

            $scope.groups = angular.copy(response.data.SessionGoals);
            $scope.initialgroups = angular.copy(response.data.SessionGoals);
            $scope.nextgroup = response.data.NextSessionGoal;
            $scope.pregroup = response.data.PreSessionGoal;
          
            if (complete) complete();
        });
    }


    reloadFactoryHours();
    reloadFactorySessions(reloadFactoryQuantity);
    

    //var choseTeam = function()
    //{
    //    $http.get("/api/Goals/IsChoseTeams?factoryId=" + factoryId + "&date=" + date).then(function (response) {
    //        //da chon team roi
    //        if(response.data == true)
    //        {
    //            //cho phep hien thi cac nut
    //            $scope.isLoadFirst = true;
    //            reloadFactoryQuantity();
    //            reloadFactorySessions();
    //        }else
    //        {
    //            $scope.isLoadFirst = false;
    //            reloadChoseTeams();
    //        }
    //    });
    //}

    //choseTeam();

    //$scope.submitChoseTeam = function()
    //{
    //    console.log($scope.teams);
    //    console.log(teamdate);
    //    var obj = {
    //        'teams': $scope.teams,
    //        'date': teamdate
    //    };
    //    $http({
    //        method: 'POST',
    //        url: "/api/Goals/CreateTeamByDate",
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        data: JSON.stringify(obj),
    //    })
    //    .success(function (data, status, headers, config) {
    //        if(data == true)
    //        {
    //            $scope.isLoadFirst = true;
    //            reloadFactoryQuantity();
    //            reloadFactorySessions();
    //        }
    //    });
    //}
    $scope.addNewFactorySession = function()
    {
        var newSession = angular.copy($scope.fSessionNew);
        var lastSession =  $scope.factorySessions[$scope.factorySessions.length - 1];
        //check valid new session
        if (newSession.Start.Days * 24 + newSession.Start.Hours < lastSession.End.Days * 24 + lastSession.End.Hours)
        {
            alert("Giờ bắt đầu ca sau phải lơn hơn hoặc bằng giờ kết thúc của ca trước");
            return;
        }

        newSession.Order = $scope.factorySessions.length + 1;
        $scope.factorySessions.push(newSession);

        initFactorySessionNew();
    }
    $scope.removeFactorySession = function (fSession)
    {
        var index = $scope.factorySessions.indexOf(fSession);
        $scope.factorySessions.splice(index, 1);
    }

    $scope.saveSessionGoal = function (group) {

        $http({
            method: 'POST',
            url: "/api/Goals/UpdateFactoryHour",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(group),
        })
        .success(function (data, status, headers, config) {
            $('#collapse_' + data.StartTime).collapse('hide');

            $.each($scope.groups, function (index, value) {
                if (value.StartTime == data.StartTime) {
                    $scope.groups[index] = angular.copy(data);
                    return;
                }
            });

            $scope.initialgroups = angular.copy($scope.groups);

            var target = $('#collapcontainer_' + data.StartTime).offset();
            if (target != null) {
                $('html, body').animate({
                    scrollTop: target.top
                }, 500);
            }
            reloadFactoryQuantity();
            alert("Đã lưu");
        });
    }

    $scope.saveFactorySessions = function () {
        $http({
            method: 'POST',
            url: "/api/Goals/UpdateFactorySessions",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify($scope.factorySessions),
        })
        .success(function (data, status, headers, config) {
            $scope.factorySessions = data;
            calculateOtherSessionGroups();

            reloadFactoryHours();
            reloadFactoryQuantity();
            //calculateNoHours_Sessions();
            alert("Lưu thành công");
        });
    }

    $scope.cancelSessionGoal = function (starttime) {
        $.each($scope.groups, function (index, value) {
            if (value.StartTime == starttime) {
                var result = $filter('filter')($scope.initialgroups, { StartTime: starttime })[0];
                $scope.groups[index] = angular.copy(result);
                return;
            }
        });
    }

    $scope.hideSessionGoal = function (group) {
        $http({
            method: 'POST',
            url: "/api/Goals/HideSessionFactory",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(group),
        })
        .success(function (data, status, headers, config) {
            $('#collapse_' + data.StartTime).collapse('hide');
            var id = $scope.groups.indexOf(group);
            if (id > -1) {
                $scope.groups.splice(id, 1);
            }
            reloadFactoryHours();
            reloadFactoryQuantity();
        });
    }

    $scope.saveNextGoal = function () {
        $http({
            method: 'POST',
            url: "/api/Goals/CreateNextSession",
            dataType: "json",
            data: JSON.stringify($scope.nextgroup)
        })
      .success(function (data, status, headers, config) {
          console.log(data);
          $scope.groups.push(data.Session);
          $scope.nextgroup = data.NewSession;

          reloadFactoryQuantity();

          alert("Đã lưu");

      });
    }
    $scope.savePreGoal = function () {
        $http({
            method: 'POST',
            url: "/api/Goals/CreatePreSession",
            dataType: "json",
            data: JSON.stringify($scope.pregroup)
        })
       .success(function (data, status, headers, config) {
           console.log(data);
           $scope.groups.unshift(data.Session);
           $scope.pregroup = data.NewSession;

           reloadFactoryQuantity();
           alert("Đã lưu");
       });
    }

    $scope.updateFactoryQuantity = function () {
        $http({
            method: 'POST',
            url: "/api/Goals/UpdateFactoryQuantity",
            dataType: "json",
            data: JSON.stringify($scope.factoryQuantity.QuantitiesTeams)
        })
       .success(function (data, status, headers, config) {
           $scope.oriFactoryQuantity = angular.copy($scope.factoryQuantity);
           //$scope.factoryQuantity.QuantitiesTeams = data;
           reloadFactoryQuantity();
           reloadFactoryHours(function () {
               alert("Đã lưu");
           });

       })
        .error(function (data, status, headers, config) {
            console.log(data, status, headers, config);
        });
    }

    $scope.updateFactoryEmployee = function () {
        $http({
            method: 'POST',
            url: "/api/Goals/UpdateFactoryEmployee",
            dataType: "json",
            data: JSON.stringify($scope.factorySessions)
        })
       .success(function (data, status, headers, config) {
           //$scope.oriFactoryNoEmployee = angular.copy($scope.factoryNoEmployee);
           reloadFactoryHours(function () {
               alert("Đã lưu");
           });

       })
        .error(function (data, status, headers, config) {
            console.log(data, status, headers, config);
        });
    }

    $scope.getProductDetail = function (goalProductDetail, index) {
        var id = 0;
        for (var i = 0; i < goalProductDetail.length; i++) {
            if (goalProductDetail[i].IsDelete == false) {
                if (id == index) {
                    return goalProductDetail[i];
                }
                id++;
            }

        }
    }

    $scope.addNewProduct = function (goalProductDetail) {
        if (confirm('Bạn có thực sự muốn thêm một Mã sản phẩm khác?')) {
            var newProduct = {
                GoalDetailId: goalProductDetail[0].GoalDetailId,
                IsDelete: false
            }
            goalProductDetail.push(newProduct);
            setTimeout(function () {
                initFixedTable();

            }, 100);
        }
        return false;
    }
    $scope.removeProduct = function (productDetail) {
        if (confirm('Bạn có thực sự muốn xóa Mã sản phẩm này?')) {
            $scope.$apply(function () {
                productDetail.IsDelete = true;
            });

        }
    }

    $scope.NoRow = function (array) {
        var result = 0;
        for (var i = 0; i < array.length; i++) {
            var count = array[i].length;
            for (var j = 0; j < array[i].length; j++) {
                if (array[i][j].IsDelete == true)
                    count -= 1;
            }

            if (result < count)
                result = count;
        }
        return result;
    }

    $scope.range = function (min, max, step) {
        step = step || 1;
        var input = [];
        for (var i = min; i <= max; i += step) {
            input.push(i);
        }
        return input;
    };
    $scope.noDelete = function (item) {
        return item.IsDelete === false;
    }

    $scope.cloneValue = function (team, x, y, value, type) {
        var ischeck = false;
        for (var j = 0; j < team.GoalProductDetailList.length; j++) {
            for (var k = 0; k < team.GoalProductDetailList[j].length; k++) {
                if (ischeck == true) {
                    if (type == "productid") {
                        $scope.$apply(function () {
                            team.GoalProductDetailList[j][k].ProductId = value;
                        });
                    }
                    else if (type == "quantity") {
                        $scope.$apply(function () {
                            team.GoalProductDetailList[j][k].Quantity = value;
                        });
                    }

                }

                if (ischeck == false && j == x && k == y) {
                    ischeck = true;
                }
            }
        }
    }

    $scope.setproductdetail = function (team, productdetail, position, x, y) {
        $scope.datatemp.Team = team;
        $scope.datatemp.ProductDetail = productdetail;
        $scope.datatemp.Position = position;
        $scope.datatemp.X = x;
        $scope.datatemp.Y = y;
    }

    var initFixedTable = function () {
        $fixedTable.fxdHdrCol({
            fixedCols: 2,
            width: "100%",
            height: 400,
            colModal: [
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 0.1, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
           { width: 100, align: 'center' },
            ]
        });
    }

    $('#modal-target').on('show.bs.modal', function (e) {
        setTimeout(function () {
            $('.planloading').addClass('hidetool');
            $('#table_input').removeClass('hidetool');
            $('#table_input_footer').removeClass('hidetool');
            initFixedTable();
            
        }, 300);

    });

    $('.container-div').on('mouseenter', '[data-toggle="tooltip"]', function () {
        $(this).tooltipster({
            multiple: true,
            theme: 'tooltipster-shadow',
            contentAsHTML: true,
            autoClose: true,
            trigger: 'click',
            interactive: true,
            animation: 'fade',
            delay: 0,
            maxWidth: 600,
            speed: 100,
            functionBefore: function (origin, continueTooltip) {
                continueTooltip();
                var content = '';
                if ($scope.datatemp.ProductDetail != null && $scope.datatemp.Position != 0) {
                    content = '<div class="col-lg-12">' +
                            '<div class="row" id="containeryoutube">' +
                                '<div class="col-lg-5">' +
                                    '<input type="button" id="copyproductid" name="saveyoutubeplay" class="btn btn-primary" value="Sao chép mã hàng">' +
                                '</div>' +
                                '<div class="col-lg-5">' +
                                    '<input type="button" id="copyquantity" class="btn btn-primary" value="Sao chép mục tiêu">' +
                                '</div>' +
                                '<div class="col-lg-2">' +
                                    '<input type="button" id="deleteproduct" name="saveyoutubeplay" class="btn btn-danger" value="Xoá">' +
                                '</div>' +
                                    '</div>' +
                            '</div>';
                } else {
                    content = '<div class="col-lg-12">' +
                            '<div class="row" id="containeryoutube">' +
                                '<div class="col-lg-6">' +
                                    '<input type="button" id="copyproductid" name="saveyoutubeplay" class="btn btn-primary" value="Sao chép mã hàng">' +
                                '</div>' +
                                '<div class="col-lg-6">' +
                                    '<input type="button" id="copyquantity" class="btn btn-primary" value="Sao chép mục tiêu">' +
                                '</div>' +
                                '</div>' +
                        '</div>';
                }
                origin.tooltipster('content', content);

            },
            functionReady: function (origin, tooltipEl) {
                $(tooltipEl).on('click', '#deleteproduct', function () {
                    $scope.removeProduct($scope.datatemp.ProductDetail);

                });

                $(tooltipEl).on('click', '#copyquantity', function () {
                    if ($scope.datatemp.ProductDetail.Quantity != null) {
                        $scope.cloneValue($scope.datatemp.Team, $scope.datatemp.X, $scope.datatemp.Y, $scope.datatemp.ProductDetail.Quantity, 'quantity');
                    }
                    $(tooltipEl).tooltipster('hide');
                });

                $(tooltipEl).on('click', '#copyproductid', function () {
                    if ($scope.datatemp.ProductDetail.ProductId != null) {
                        $scope.cloneValue($scope.datatemp.Team, $scope.datatemp.X, $scope.datatemp.Y, $scope.datatemp.ProductDetail.ProductId, 'productid');
                    }
                    $(tooltipEl).tooltipster('hide');
                });
            }
        });
    });
});

//app.directive('ngEsc', function () {
//    return function (scope, element, attrs) {
//        element.bind("keydown keypress keyup", function (event) {
//            if (event.which === 27) {
//                alert("asdasd");

//                event.preventDefault();
//            }
//        });
//    };
//});

app.directive('hideZero', [function () {
    return {
        require: '?ngModel', // get a hold of NgModelController
        link: function (scope, element, attrs, ngModel) {

            // Specify how UI should be updated
            ngModel.$formatters.push(function (value) {
                if (value === 0) {
                    // If the value is zero, return a blank string.
                    return '';
                }

                return value;
            });
        }
    };
}]);

app.directive('modalRevisions', function () {
    return {
        template: '<div class="modal fade">' +
            '<div class="modal-dialog modal-lg">' +
              '<div class="modal-content">' +
                '<div class="modal-header">' +
                  '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
                  '<h4 class="modal-title">{{ title }}</h4>' +
                '</div>' +
                '<div class="modal-body" ng-transclude></div>' +
                '<div class="modal-footer">' +
                    '<button type="button" class="btn btn-success" data-dismiss="modal"><i class="fa fa-check"></i>OK</button>' +
                '</div>' +
              '</div>' +
            '</div>' +
          '</div>',
        restrict: 'E',
        transclude: true,
        replace: true,
        scope: {
        },
        link: function postLink(scope, element, attrs) {
            scope.title = attrs.title;
            scope.id = attrs.id;
        }
    };
});

app.directive('convertToNumber', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            ngModel.$parsers.push(function (val) {
                return parseInt(val, 10);
            });
            ngModel.$formatters.push(function (val) {
                return '' + val;
            });
        }
    };
});

app.directive('onlyDigits', function () {
    return {
        require: 'ngModel',
        restrict: 'A',
        link: function (scope, element, attr, ctrl) {
            function inputValue(val) {
                if (val) {
                    var digits = val.replace(/[^0-9.]/g, '');

                    if (digits.split('.').length > 2) {
                        digits = digits.substring(0, digits.length - 1);
                    }

                    if (digits !== val) {
                        ctrl.$setViewValue(digits);
                        ctrl.$render();
                    }

                    return parseFloat(digits);
                }
                return undefined;
            }
            ctrl.$parsers.push(inputValue);
        }
    };
});

app.directive('onlyDigits60', function () {
    return {
        require: 'ngModel',
        restrict: 'A',
        link: function (scope, element, attr, ctrl) {
            function inputValue(val) {
                if (val) {
                    var digits = val.replace(/[^0-9.]/g, '');

                    if (digits.split('.').length > 2) {
                        digits = digits.substring(0, digits.length - 1);
                    }



                    if (digits !== val) {
                        ctrl.$setViewValue(digits);
                        ctrl.$render();
                    }

                    if (parseFloat(digits) <= 60) {
                        ctrl.$setViewValue(digits);
                        ctrl.$render();
                    } else {
                        digits = '';
                        ctrl.$setViewValue(digits);
                        ctrl.$render();
                    }

                    return parseFloat(digits);
                }
                return undefined;
            }
            ctrl.$parsers.push(inputValue);
        }
    };
});
app.directive("multipleSelect", function () {
    return {
        link: function (scope, element, attr, ctrl) {
            //console.log(element);
            function getSelectValues(select) {
                var result = [];
                //console.log(select.options);
                var options = select && select.options;
                var opt;

                for (var i = 0, iLen = options.length; i < iLen; i++) {
                    opt = options[i];

                    if (opt.selected) {
                        result.push(opt.value || opt.text);
                    }
                }
                return result;
            }
            //console.log(getSelectValues(element[0]));
            $(element).multiselect(
                {
                    buttonWidth: '100%',
                    buttonClass: 'cell-input',
                    nonSelectedText: '',
                });
            // Watch for any changes to the length of our select element
            scope.$watch(function () {
                return element[0].length;
            }, function () {
                element.multiselect('rebuild');
            });

            // Watch for any changes from outside the directive and refresh
            scope.$watch(attr.ngModel, function () {
                element.multiselect('refresh');
            });
        }

    }
});
app.directive("timepicker", function () {
    
    return {
        scope: {
            time : '='
        },
        link: function (scope, element, attr, ctrl) {
            jQuery(element).datetimepicker({
                datepicker: false,
                format: 'H:i',
                value: '12:00',
                onChangeDateTime: function (current_time, $input) {
                    var d = $input.datetimepicker('getValue');
                    scope.time.Hours = d.getHours();
                    scope.time.Minutes = d.getMinutes();
                }
            });
        }
    }
});

app.filter("twoDigits", function () {
    return function (val) {
        return val <= 9 ? '0' + val : val;
    }
});