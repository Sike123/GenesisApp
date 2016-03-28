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
            token: ""
        }

        var setProfile = function (username, token) {
            profile.userName = username;
            $cookies.put('userName', username);
            profile.token = token;
            $cookies.put('accessToken', token);

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

        return {
            setProfile: setProfile,
            getProfile: getProfile,
            removeProfile:removeProfile
        }
    }


})();