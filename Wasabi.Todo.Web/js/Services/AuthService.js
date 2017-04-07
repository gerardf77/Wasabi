app.factory("AuthService", ["$resource", "$httpParamSerializer", "AuthRequestInterceptor", function ($resource, $httpParamSerializer, AuthRequestInterceptor) {

    return {
        Register: $resource("/api/account/register",{},
            {
                Post: {
                    method: "POST"
                }
            }),
        Login: $resource("/token",{},
            {
                Post: {
                    method: "POST",
                    headers: {"Content-Type":"application/x-www-form-urlencoded; charset=UTF-8"},
                    transformRequest: function (data) {
                        return $.param(data);
                    }
                }
            }),
        Logoff: $resource("/api/account/logout", {},
            {
                Post: {
                    method: "POST",
                    interceptor: AuthRequestInterceptor
                }
            }),
        UserInfo: $resource("/api/account/userinfo", {},
            {
                Get: {
                    method: "POST",
                    interceptor: AuthRequestInterceptor
                }
            })
    };

}]);