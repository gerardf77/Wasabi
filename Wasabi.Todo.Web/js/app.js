var app = angular.module("toDoApp", ["ui.router", "ngResource", "ngGeolocation"]);
app.config(function($stateProvider, $httpProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise("/tasks");

        $stateProvider
            .state("tasks",
            {
                url: "/tasks",
                templateUrl: "js/Views/Tasks.html",
                controller: "TaskController",
                data: {
                    requiresAuthentication: true
                }
            })
            .state("login",
            {
                url: "/login",
                templateUrl: "js/Views/LoginForm.html",
                controller: "MembershipController"
            })
            .state("register",
            {
                url: "/register",
                templateUrl: "js/Views/RegisterForm.html",
                controller: "MembershipController"
            });

        console.log("app.config -> not authorized");

        $httpProvider.interceptors.push("AuthRequestInterceptor");
    }
);
// Initialize the main module
app.run(["$rootScope", "$state", "UserProfile", function ($rootScope, $state, UserProfile) {

    // Expose $state to scope for convenience
    //$rootScope.$state = $state;

    $rootScope.$on("$stateChangeStart", function (e, toState) {

        if (!(toState.data)) return;
        if (!(toState.data.requiredAuthentication)) return;

        var requiresAuthentication = toState.data.requiresAuthentication;

        if (requiresAuthentication && !UserProfile.IsLoggedIn()) {

            e.preventDefault();
            $state.go("login", { notify: false });

            console.log("app.run -> not authorized");
        }
        return;


    });
}]);