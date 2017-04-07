app.factory("AuthRequestInterceptor", ["$q", "UserProfile", function ($q, UserProfile) {

       return {

           request: function (config) {
               var authData = UserProfile.GetProfile();

               if (authData && authData.token) {
                   config.headers = config.headers || {};
                   config.headers.Authorization = "Bearer " + authData.token;
               }

               return config || $q.when(config);
           },

           // On request failure
           requestError: function (rejection) {
               // console.log(rejection); // Contains the data about the error on the request.

               // Return the promise rejection.
               return $q.reject(rejection);
           },

           // On response success
           response: function (response) {
               // console.log(response); // Contains the data from the response.

               // Return the response or promise.
               return response || $q.when(response);
           },

           // On response failture
           responseError: function (rejection) {
               // console.log(rejection); // Contains the data about the error.

               // Return the promise rejection.
               return $q.reject(rejection);
           }
       };

}]);