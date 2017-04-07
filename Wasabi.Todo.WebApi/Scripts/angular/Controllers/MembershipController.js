app.controller("MembershipController",
[
    "$scope", "LoginService", "UserProfile", function ($scope, LoginService, UserProfile) {
        var vm = this;
        vm.responseData = "";
        vm.userName = "";
        vm.userEmail = "";
        vm.userPassword = "";
        vm.accessToken = "";
        vm.refreshToken = "";

        vm.isLoggedIn = function() {
            var profile = UserProfile.getProfile();
            return profile.isLoggedIn;
        };

        vm.registerUser = function() {
            vm.responseData = "";
            var userInfo = {
                Email: vm.userEmail,
                Password: vm.userPassword,
                ConfirmPassword: vm.userPassword
            };

            var registerResult = LoginService.register(userInfo);
            registerResult.then(function(data) {
                    vm.responseData = "User Registration successful";
                    vm.userPassword = "";
                },
                function(response) {
                    vm.responseData = response.statusText + "\r\n";

                    if (response.data.exceptionMessage) vm.responseData += response.data.exceptionMessage;

                    if (response.data.modelState) {
                        for (var key in response.data.modelState) {
                            vm.responseData += response.data.modelState[key] + "\r\n";
                        }
                    }
                });
        };

        vm.login = function() {
            var userLogin = {
                grant_type: 'password',
                username: vm.userEmail,
                password: vm.userPassword
            };

            vm.responseData = "";
            var loginResult = LoginService.login(userLogin);
            loginResult.then(function(resp) {
                    vm.userName = resp.data.userName;
                    UserProfile.setProfile(resp.data.userName, resp.data.access_token, resp.data.refresh_token);
                },
                function(response) {
                    vm.responseData = response.statusText + " : \r\n";
                    if (response.data.error) vm.responseData += response.data.error_description;
                });
        };
    }
]);
