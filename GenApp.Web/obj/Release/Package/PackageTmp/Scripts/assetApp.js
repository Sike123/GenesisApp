var assetApp = angular.module('assetModule', ['ngRoute', 'ui.bootstrap', 'ngCookies']);

//http://haroldrv.com/2015/02/understanding-angularjs-q-service-and-promises/
assetApp.service("assetService", ['$http', '$cookies', '$q', function ($http, $cookies, $q) {

    var getAssetDetails = function (assetId) {
        var deferred = $q.defer();
        $http.get("/api/Asset/" + assetId, { headers: { 'Authorization': 'Bearer ' + $cookies.get('accessToken') } })
             .then(function (response) {

                 deferred.resolve(response);
             }).catch(function (response) {

                 alert("Oops something wrong: " + response.data);
                 deferred.reject(response);
             }).finally(function () {

             });
        return deferred.promise;
    };
    var getAllAssets = function () {
        var deferred = $q.defer();


        $http.get("/api/Asset/", {

            headers: {
                'Authorization': 'Bearer ' + $cookies.get("accessToken"),
                'Content-Type': 'application/json'
            }
        })
            .then(function (response) {
                console.log("response from getAllAssets Function: ");
                console.log(response);
                deferred.resolve(response);

            }).catch(function (response) {
                alert("Could not get asset details" + response.status + " " + response.data);
            }).finally(function () {

            });
        return deferred.promise;
    };

    var saveAssetDetails = function (asset) {
        var deferred = $q.defer();

        $http.post('api/Asset/', asset, { headers: { 'Authorization': 'Bearer ' + $cookies.get('accessToken') } })
            .then(function (response) {
                deferred.resolve(response);

            })
            .catch(function (response) {
                deferred.reject(response);
            })
            .finally(function () {
            });
        return deferred.promise;
    }

    var updateAsset = function (asset) {
        console.log("updateAsset method in service invoked");
        var deferred = $q.defer();

        $http.put('api/Asset/', asset, { headers: { 'Authorization': 'Bearer ' + $cookies.get('accessToken') } })
            .then(function (response) {
                deferred.resolve(response);
            })
            .catch(function (error) {
                deferred.reject(error);
            });
        return deferred.promise;
    }

    var deleteAsset = function (assetId) {
        var deferred = $q.defer();
        $http.delete("/api/Asset/" + assetId, { headers: { 'Authorization': 'Bearer ' + $cookies.get('accessToken') } })
            .then(function (response) {
                deferred.resolve(response);
            })
            .catch(function (response) {
                deferred.reject(response);
            });
        return deferred.promise;
    }

    return {
        getAssetDetails: getAssetDetails,
        getAllAssets: getAllAssets,
        saveAssetDetails: saveAssetDetails,
        deleteAsset: deleteAsset,
        updateAsset: updateAsset
    };
}]);

assetApp.controller("assetController", [
    '$scope', '$routeParams', '$http', 'assetService', 'currentUser',
    function ($scope, $routeParams, $http, assetService, currentUser) {

        $scope.loggedIn = false;
        //as the modal is about to show 
        $('#deleteModal').on('show.bs.modal', function (e) {
            //need more reading on this 
            // http://stackoverflow.com/questions/5309926/how-to-get-the-data-id-attribute
            var assetToDelete = $(this).data("id");
            $scope.assetIdToDelete = $(e.relatedTarget).data(assetToDelete).id;

        });

        $('#templateModal').on('hidden.bs.modal', function () {
            window.location.reload();
        });

        $scope.deleteAsset = function () {

            var promise = assetService.deleteAsset($scope.assetIdToDelete);

            promise.then(function (response) {
                currentUser.setAndDisplayMessageModal("Asset Deleted", response.data);

            }).catch(function (error) {
                currentUser.setAndDisplayMessageModal("Error in Removing the Asset", error.data);

            });

        }

        $scope.GetAllBooks = function () {

            var assets = assetService.getAllAssets();

            assets.then(function (response) {

                $scope.books = response.data;
            });
        }
    }
]);

assetApp.controller("createAssetController", ['$scope', '$http', '$window', '$routeParams', 'assetService', 'currentUser',
function ($scope, $http, $window, $routeParams, assetService, currentUser) {

    var assetId = $routeParams.assetId;
    $scope.isViewPage = false;

    $scope.checkIfItsViewPage = function () {
        console.log("check is view page triggered");
        if (assetId) {

            $scope.isViewPage = true;
            var assetDetails = assetService.getAssetDetails(assetId);

            assetDetails.then(function (response) {

                $scope.id = response.data.Id;
                $scope.name = response.data.Name;
                $scope.isbn = response.data.Isbn;
                $scope.publisher = response.data.Publisher;
                $scope.edition = response.data.Edition;
            })
                .catch(function (error) {
                    alert("Could not display the contents " + error.statusText);
                });
        }
    }

    $scope.submit = function () {
        var book = {
            "Name": $scope.asset.name,
            "Isbn": $scope.asset.isbn,
            "Publisher": $scope.asset.publisher,
            "Edition": $scope.asset.edition
        }

        var promise = assetService.saveAssetDetails(book);
        promise.then(function (response) {
            currentUser.setAndDisplayMessageModal("Success", response.data);
            $window.location.href = "/#/BooksList";
        }, function (error) {
            currentUser.setAndDisplayConfirmationModal("Error", error.statusText + " " + error.data);
        });
    }
}]);

assetApp.controller("updateAssetController", ['$scope', '$http', '$window', '$routeParams', 'assetService', 'currentUser',

    function ($scope, $http, $window, $routeParams, assetService, currentUser) {

        console.log("updateAssetController invoked");
        var assetId = $routeParams.assetId;
        var asset = assetService.getAssetDetails(assetId);
        asset.then(function (response) {
            $scope.id = response.data.Id;
            $scope.name = response.data.Name;
            $scope.isbn = response.data.Isbn;
            $scope.publisher = response.data.Publisher;
            $scope.edition = response.data.Edition;

        });


        $scope.submit = function () {
            var book = {
                "Id": $routeParams.assetId,
                "Name": $scope.name,
                "Isbn": $scope.isbn,
                "Publisher": $scope.publisher,
                "Edition": $scope.edition
            }

            var promise = assetService.updateAsset(book);
            promise.then(function (response) {
                currentUser.setAndDisplayMessageModal("Update Successful", response.data);
                $window.location.href = "/#/BookList";
            }).catch(function (error) {
                console.log("Exception logged from Client Side during Assset Update");
                console.log(error);
                //alert("Error in Updating Product " + error.statusText);
                currentUser.setAndDisplayConfirmationModal("Update Could not be Successful: " + error.statusText);
            });
        }
    }]);
