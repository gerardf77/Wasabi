app.controller("NavBarController", ["$window", "$rootScope", "$scope", "$state", "AuthService", "UserProfile", function ($window, $rootScope, $scope, $state, AuthService, UserProfile) {
        $scope.Logoff = function() {
                AuthService.Logoff.Post("",
                    function(response) {
                        UserProfile.ClearProfile();
                        $window.location.href = "";
                    },
                    function(error) {
                        if (error.data) {
                            $rootScope.ServerErrors = error.data.Message;
                        }
                    });
        }

        var user = UserProfile.GetProfile();
        $rootScope.isAuthenticated = user.isLoggedIn;
        $rootScope.username = user.username;
    }
]);