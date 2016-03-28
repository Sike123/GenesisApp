var userModule = angular.module('userModule', ['ngRoute', 'ui.bootstrap', 'ngCookies']);

userModule.service("userService", [
    '$http', '$q', '$cookies', 'currentUser', function ($http, $q, $cookies, currentUser) {


        var setUserProfile=function(response) {
            
        }

        var getRolesForUser=function(userId) {
            
        }

        var login = function (userName,passWord) {
       
            var deferred = $q.defer();
            var data = "grant_type=password&username=" + userName + "&password=" +passWord;
            $http.post("/Token", data, {
                headers: 'Content-Type:application/x-www-form-urlencoded'
            }).success(function (response) {
                currentUser.setProfile(userName, response.access_token);
               
                deferred.resolve(response);
            }).catch(function (response) {
                deferred.reject(response);

            });

            return deferred.promise;
        };

        var registerUser = function (url, obj) {
            var deferred = $q.defer();

            var result = $http({
                method: 'Post',
                url: '/api/Account/Register',
                data: obj,
                headers: {
                    'Authorization': 'Bearer ' + $cookies.get('accessToken'),
                    'Content-Type': 'application/json'
                }
            }
            ).success(function (response) {
                deferred.resolve(response);

            }).catch(function (error) {
                deferred.reject(error);
            });


            return deferred.promise;
        }

        //Do it from here
        var logOut = function () {
            currentUser.removeProfile();

        }

        var getUserProfile = function () {
            return currentUser.getProfile();

        }

        return {
            registerUser: registerUser,
            login: login,
            logOut: logOut,
            getUserProfile: getUserProfile
        };

    }
]);


//http://haroldrv.com/2015/02/understanding-angularjs-q-service-and-promises/
userModule.controller("userController", ['$scope', '$http', '$window', '$routeParams', 'userService',
    function ($scope, $http, $window, $routeParams, userService) {
        var routeUrl = '';
        $scope.ErrorMessage = "ErrorList: ";        
 
        $scope.checkIfTheUserIsLoggedIn = function () {
            this.model.user = userService.getUserProfile();
            routeUrl = '/api/Account/RegisterUser';
            if (this.model.user.isLoggedIn === 'true') {
                $window.location.href = "/#/BooksList";
            }

        }

        $scope.logOut = function () {
            userService.logOut();
            $window.location.href = "/#/";
        }

        $scope.register = function () {
            var user = {
                "Email": $scope.email,
                "Password": $scope.password,
                "ConfirmPassword": $scope.confirmPassword
            };

            var result = userService.registerUser(routeUrl, user);


            result.then(function (response) {
                alert("User Registered Successfully. " + response.message + response.data);
                $window.location.href = "/#/";
            }).catch(function (response) {

                console.log(response);
                if (response.data.exceptionMessage) $scope.errorMessage = response.data.exceptionMessage;
                if (response.data.Message) $scope.errorMessage = response.data.Message + "\n";
                if (response.data.ModelState) {
                    for (var key in response.data.ModelState) {
                        $scope.errorMessage += response.data.ModelState[key] + "\n";
                    }
                }
            });
        }

        $scope.login = function () {
            var result = userService.login(this.model.user.username,this.model.user.password);
            result.then(function (response) {
                if (response.access_token) {
                    
                    alert("Welcome:" + response.userName);
                    $window.location.href = "/#/BooksList";

                } else {
                    alert("You Are Not Logged in ");
                }

            }).catch(function (response) {
                $scope.errorMessage = response.data.error_description;
            });
        }
    }]);
