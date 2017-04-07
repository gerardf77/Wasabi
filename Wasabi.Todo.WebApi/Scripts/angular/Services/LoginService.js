angular.module("toDoApp.Services",[]).factory("LoginService", ["$scope", "$resource", "appSettings", LoginService]);

function LoginService($scope, $resource, appSettings) {
    this.Login = function(userInfo) {
        return $resource(appSettings + "/Api/Account/Register",
            {},
            {
                login: {
                    method: "POST",
                    interceptor: {
                        response: function(data) {
                            return data;
                        },
                        responseError: function(error) {
                            $scope.isError = true;
                            $scope.errorMsg = error.statusText;
                            console.log(error);
                        }
                    }
                }
            });
    };

    this.Login = function(userLogin) {
        return $resource(appSettings + "/TOKEN",
            {},
            {
                login: {
                    method: "POST",
                    interceptor: {
                        response: function(data) {
                            return data;
                        },
                        responseError: function(error) {
                            $scope.isError = true;
                            $scope.errorMsg = error.statusText;
                            console.log(error);
                        }
                    }
                }
            });
    };

    return {
        Register: this.Register,
        Login: this.Login
    };

};