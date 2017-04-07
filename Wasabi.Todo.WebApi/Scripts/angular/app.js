var app = angular.module("toDoApp", ["ui.router", "ngResource", "ngGeolocation", "toDoApp.Services", "toDoApp.Controllers"]);

app.config(["$httpProvider", function ($httpProvider, $stateProvider) {
            $httpProvider.interceptors.push("authHttpRequestInterceptor");

            $stateProvider.state("tasks", {
                url: "/tasks",
                templateUrl: "partials/tasks.html",
                controller: "TasksController"
            //}).state("viewTask", {
            //    url: "/tasks/:id/view",
            //    templateUrl: "Scripts/angular/Views/tasks-view.html",
            //    controller: "TaskController"
            }).state("newTask", {
                url: "/tasks/new",
                templateUrl: "Scripts/angular/Views/TaskAdd.html",
                controller: "TaskCreateController"
            }).state("editTask", {
                url: "/tasks/:id/edit",
                templateUrl: "Scripts/angular/Views/TaskEdit.html",
                controller: "TaskEditController"
            }).state("registerUser", {
                url: "/user/new",
                templateUrl: "Scripts/angular/Views/UserRegister.html",
                controller: "MembershipController"
            });
        }
]); 

app.constant("appSettings", {
        serverPath: "http://localhost:2356/"
    })
    .run(function($state) {
        $state.go("registerUser");
    });