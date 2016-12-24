
(function () {
    var adminApp = angular.module('adminModule', ['ngRoute', 'ui.bootstrap', 'ngCookies']);
    adminApp.service("adminService", ['$http', '$q', '$cookies',function ($http, $q,$cookies) {

        var registerRole = function (url, obj) {
            console.log("registerRole Service Function Activated");
            console.log(obj.Name);
            var deferred = $q.defer();
            var result = $http({
               method:"Post",
                data: obj,
                url: url,
                headers: {
                    'Authorization': 'Bearer ' + $cookies.get("accessToken"),
                    'Content-Type': 'application/json'
                }
            }).success(function (response) {
                return deferred.resolve(response);
            }).catch(function (error) {
                return deferred.resolve(error);
            });

            return deferred.promise;
        }

        return {
            registerRole: registerRole
        };
    }]);

    adminApp.controller("adminController", ["$scope", "$http", "$window", "$routeParams", 'currentUser', 'adminService',
         function ($scope, $http, $window, $routeParams, currentUser, adminService) {

             var routeUrl = '/api/Roles/Create';
             $scope.submit = function () {
               
                 var createRoleBindingModel = {
                     "Name": $scope.roleName
                 };

                
                 var result = adminService.registerRole(routeUrl, createRoleBindingModel);
                 result.then(function (response) {
                     console.log("Info Log From Admin Controller");
                     console.log(response);

                 })
                     .catch(function (response) {
                         console.log("Error Log from Admin Controller");
                         console.log(response);
                     });

             }
         }

         

    ]);


})();