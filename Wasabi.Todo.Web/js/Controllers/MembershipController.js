app.controller("MembershipController", ["$rootScope", "$scope", "$state", "AuthService", "AuthRequestInterceptor", "UserProfile", function ($rootScope, $scope, $state, AuthService, AuthRequestInterceptor, UserProfile) {

    if (UserProfile.IsLoggedIn()) {
        $state.go("tasks", { notify: false });
    }

    $scope.ServerErrors = [];
        $scope.ServerValidationSummary = "";
        $scope.ServerValidationSummary.errorMessage = "";

        $scope.email = "gerardflood@hotmail.com";
        $scope.password = "P@ssw0rd";
        $scope.accessToken = "";
        $scope.refreshToken = "";

        $scope.firstName = "John";
        $scope.lastName = "Smith";

        $scope.isLoggedIn = UserProfile.IsLoggedIn();

        // function to submit the form after all validation has occurred			
        $scope.submitForm = function (form) {

            // check to make sure the form is completely valid
            if (form.$valid) {

                try {
                    this.responseData = "";
                    var userInfo = {
                        FirstName: $scope.firstName,
                        LastName: $scope.lastName,
                        Email: $scope.email,
                        Password: $scope.password,
                        ConfirmPassword: $scope.password
                    };

                    AuthService.Register.Post(userInfo, function (response) {
                        $scope.responseData = "User Registration successful";
                        $scope.password = "";
                        $scope.ServerErrors = [];

                        $state.go("login", { notify: false });
                    }, function (error) {

                        if (error.data.ModelState) {
                            for (var key in error.data.ModelState) {
                                for (var err in error.data.ModelState[key]) {
                                    $scope.ServerErrors.push(error.data.ModelState[key][err]);
                                }
                            }
                        }
                        console.log($scope.ServerErrors);
                        form.ServerValidationSummary.$setValidity("ServerValidationSummary", false);
                    });
                } catch (e) {
                    $scope.ServerErrors.push(e.message);
                } 
            }

        };

        $scope.Login = function (form) {

            // check to make sure the form is completely valid
            if (form.$valid) {

                var userLogin = {
                    grant_type: "password",
                    username: $scope.email,
                    password: $scope.password
                };

                $scope.responseData = "";
                AuthService.Login.Post(userLogin,
                    function(response) {
                        UserProfile.SetProfile(response.userName,
                            response.token_type,
                            response.access_token,
                            response.expires);

                        var user = UserProfile.GetProfile();

                        $rootScope.isAuthenticated = true;
                        $rootScope.username = user.username;

                        $state.go("tasks", { notify: false });
                    },
                    function(error) {
                        if (error.data) {
                            $scope.ServerErrors = error.data.error_description;
                        }
                        console.log($scope.ServerErrors);
                        form.ServerValidationSummary.$setValidity("ServerValidationSummary", false);
                    });
            }
        };
    }
]);
