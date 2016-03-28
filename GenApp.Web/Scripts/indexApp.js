


//var indexApp = angular.module('indexModule', ['ngRoute', 'ngCookies', 'ui.bootstrap', 'assetModule', 'userModule', 'common.services']);

(function () {
    angular.module('indexModule', ['ngRoute', 'ngCookies', 'ui.bootstrap', 'assetModule', 'userModule', 'common.services'])
        .config([
            '$routeProvider',
            function ($routeProvider) {
                $routeProvider
                    .when('/', {
                        templateUrl: 'HTMLTemplates/Login.html'
                        
                    })
                    .when('/BooksList', {
                        templateUrl: 'HTMLTemplates/BooksList.html',
                        controller: 'assetController'
                    })
                    .when('/Create/:assetId', {
                        templateUrl: 'HTMLTemplates/Create.html',
                        controller: 'createAssetController'
                    })
                    .when('/Create', {
                        templateUrl: 'HTMLTemplates/Create.html',
                        controller: 'createAssetController'
                    })
                    .when('/Update/:assetId', {
                        templateUrl: 'HTMLTemplates/Update.html',
                        controller: 'updateAssetController'
                    })
                    .when('/RegisterUser', {
                        templateUrl: 'HTMLTemplates/RegisterUser.html',
                        controller: 'userController'
                    })
                    .otherwise({
                        redirectTo: '/'
                    });

                //   $locationProvider.html5Mode({ enabled: true, requiredBase: false });
            }
        ])
        .service("indexService", [
            'currentUser', function (currentUser) {

                var logOut = function () {

                    currentUser.removeProfile();

                }

                var isAuthenticated = function () {


                    var profile = currentUser.getProfile();
                    return profile.isLoggedIn;
                }

                return {
                    logOut: logOut,
                    isAuthenticated: isAuthenticated
                }

            }
        ])
        .controller('indexController', ["$scope", '$window', '$routeParams', 'indexService', function ($scope, $window, $routeParams, indexService) {

            $scope.hasLoggedIn = false;


                $scope.isUserAuthenticated = function() {


                    var isUserLoggedIn = indexService.isAuthenticated();


                    if (typeof (isUserLoggedIn) === "string" && isUserLoggedIn === "true") {


                        $scope.hasLoggedIn = true;


                    } else {

                        $scope.hasLoggedIn = false;

                    }


            


            }

            $scope.logOut = function () {
                
                indexService.logOut();
                indexService.isAuthenticated();
                $window.location.href = "/#/";
            }
        }
        ]);
})();


//http: //www.codeproject.com/Articles/826307/AngularJS-With-MVC-Web-API




