angular.module('toDoApp',[]).factory('authHttpRequestInterceptor', ['$window',
   function ($window) {

       return {

           request: function (config) {
               // This is the authentication service that I use.
               // I store the bearer token in the local storage and retrieve it when needed.
               // You can use your own implementation for this
               //var authData = authenticationDataService.getAuthData();

               //if (authData && authData.accessToken) {
               //    config.headers["Authorization"] = 'Bearer' + authData.accessToken;
               //}
               alert('authHttpRequestInterceptor');

               return config;
           }
       };
   }]);

