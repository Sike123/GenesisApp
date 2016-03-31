(function () {
    "use strict";

    angular.module("common.services",['ngCookies'])
     .constant("appSettings",
         {
             serverPath: "http://localhost:51227/"
         }
     )
    .service("currentUser",['$cookies',currentUser]);

    function currentUser($cookies) {
        var profile = {
            isLoggedIn: false,
            username: "",
            password:"",
            token: "",
            role:""
        }

        var setProfile = function (username, token,userRole) {
            profile.userName = username;
            $cookies.put('userName', username);
            profile.token = token;
            $cookies.put('accessToken', token);

            profile.role = ('userRole', userRole);
            $cookies.put('userRole', userRole);
            profile.isLoggedIn = true;
            $cookies.put('isLoggedIn', true);


        }

        var removeProfile = function () {
            profile.userName = null;
            $cookies.put('userName', null);
            profile.token = null;
            $cookies.put('accessToken', null);
            profile.isLoggedIn = false;
            $cookies.put('isLoggedIn', false);
            
        }



        var getProfile = function () {
          
            profile.userName = $cookies.get('userName');
            profile.token = $cookies.get('accessToken');
            profile.isLoggedIn = $cookies.get('isLoggedIn');

            return profile;
        }
        
        var setAndDisplayModal = function (messageHeader,messageBody) {
 
            $('#templateModal').on('show.bs.modal', function () {
                var modal = $(this);
                modal.find('.modal-body').text(messageBody);
                modal.find('.modal-header').text(messageHeader);
            });

            $('#templateModal').modal('show');
        }

        return {
            setProfile: setProfile,
            getProfile: getProfile,
            removeProfile: removeProfile,
            setAndDisplayModal:setAndDisplayModal
        }
    }

    

})();