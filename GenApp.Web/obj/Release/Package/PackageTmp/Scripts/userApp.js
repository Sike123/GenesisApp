var userModule = angular.module('userModule', ['ngRoute', 'ui.bootstrap', 'ngCookies']);

userModule.service("userService", [
    '$http', '$q', '$cookies', 'currentUser', function ($http, $q, $cookies, currentUser) {
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
userModule.controller("userController", ['$scope', '$http', '$window', '$routeParams', 'userService','currentUser',
    function ($scope, $http, $window, $routeParams, userService,currentUser) {
        var routeUrl = '';
        $scope.ErrorMessage = "ErrorList: ";
        var messageHeader = "";
        var messageBody = "";
      
        $scope.checkIfTheUserIsLoggedIn = function () {
            this.model.user = userService.getUserProfile();
            routeUrl = '/api/Account/RegisterUser';
            if (this.model.user.isLoggedIn === 'true') {
                $window.location.href = "/#/BooksList";
            }

        }

        $scope.logOut = function () {
            console.log("logout triggered");
            currentUser.setAndDisplayConfirmationModal("Confirm Sign Out", "Are you sure you want to sign out.","LogOut");

        //    userService.logOut();
          //  $window.location.href = "/#/";
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
            

                    messageHeader = "Authentication Successful";
                    messageBody = "Welcome: " + response.userName;
                 
                    $('#templateModal').modal('show');
                 

                } else {
                    alert("You Are Not Logged in ");
                }

            }).catch(function (response) {
                console.log(response);
                $scope.errorMessage = response.data.error_description;
            });


            $('#templateModal').on('show.bs.modal', function (e) {
                var modal = $(this);
                modal.find('.modal-body').text(messageBody);
                modal.find('.modal-header').text(messageHeader);
            });

            //take the user to the booklist when the modal has finished disrendering
            $('#templateModal').on('hidden.bs.modal', function(e) {
                $window.location.href = "/#/BooksList";
            });
        }

       
    }]);
