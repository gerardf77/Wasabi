app.controller("TaskController", ["$scope", "$state", "GeolocationService", "TaskService", "UserProfile", function ($scope, $state, GeolocationService, TaskService, UserProfile) {

    // Common properties
    $scope.ServerErrors = [];
    $scope.ServerValidationSummary = "";
    $scope.ServerValidationSummary.errorMessage = "";
    $scope.tasks = [];
    $scope.isError = false;
    $scope.isUpdating = false;
    $scope.editIndex = false;
    $scope.labelTaskAction = "Add";

    // User profile data
    $scope.User = {};
    $scope.User = UserProfile.GetProfile();
    if (!UserProfile.IsLoggedIn() || $scope.User === null || $scope.User === "undefined" || !$scope.User.isLoggedIn) {
        $state.go("login", { notify: false });
    }

    // Geolocation data
    var location = { Longtitude: "", Latitude: "" };
    GeolocationService().then(function(position) {
            location.Latitude = position.coords.latitude;
            location.Longtitude = position.coords.longitude;
        },
        function(reason) {
            $scope.errorMsg = "Could not be determined.";
        });

    // Task properties
    $scope.TaskId = 0;
    $scope.message = "";
    $scope.Location = location;
    $scope.UserName = $scope.User.username;
    $scope.dateadded = "";
    $scope.datemodified = "";

    // TaskService operations
    $scope.Getall = function() {
        $scope.tasks = TaskService.query();
    };

    $scope.Save = function (form) {
        if (form.$valid) {

            var taskService = new TaskService({ Message: $scope.message, UserName: $scope.User.username, Location: location });

            if ($scope.editIndex === false) {

                taskService.$save()
                    .then(function(result) {
                        $scope.Getall();
                    })
                    .catch(function(error) {
                        $scope.isError = true;
                        $scope.errorMsg = error.statusText;
                        $scope.ServerErrors = error.data.Message;
                        console.log(error);

                        form.ServerValidationSummary.$setValidity("ServerValidationSummary", false);
                    })
                    .finally(function() {
                        $scope.tasks.push({ task: $scope.task });
                    });

            } else {

                var task = {
                    Message: $scope.message,
                    UserName: $scope.User.username,
                    Location: {
                        Latitude: $scope.Location.Latitude,
                        Longtitude: $scope.Location.Longtitude
                    }
                };

                TaskService.update({ id: $scope.task.TaskId },
                    task,
                    function(result) {
                        $scope.task = { Message: "", Location: {}, Done: false };
                    },
                    function(error) {
                        $scope.isError = true;
                        $scope.errorMsg = error.statusText;
                        console.log(error);
                    });

                $scope.tasks[$scope.editIndex].task = $scope;
            }

            $scope.editIndex = false;
            $scope.labelTaskAction = "Add";
        }
        var is = form.message.$invalid;
        var isa = form.message.$pristine;
    }
    $scope.EditTask = function(index) {
        $scope.labelTaskAction = "Update";
        $scope.isUpdating = true;
        $scope.task = $scope.tasks[index];
        $scope.editIndex = index;
    }
    $scope.CancelUpdate = function () {
        $scope.labelTaskAction = "Add";
        $scope.isUpdating = false;
        $scope.task = "";
        $scope.editIndex = false;
    }
    $scope.CompleteTask = function(index) {
        $scope.tasks[index].IsCompleted = true;

        TaskService.update({ id: $scope.tasks[index].TaskId },
            $scope.tasks[index],
            function(result) {
                $scope.task = { Message: "", Location: {}, Done: false };
            },
            function(error) {
                $scope.isError = true;
                $scope.errorMsg = error.statusText;
                console.log(error);
            });

    }
    $scope.UncompleteTask = function(index) {
        $scope.tasks[index].IsCompleted = false;

        TaskService.update({ id: $scope.tasks[index].TaskId },
            $scope.tasks[index],
            function (result) {
                $scope.task = { Message: "", Location: {}, Done: false };
            },
            function (error) {
                $scope.isError = true;
                $scope.errorMsg = error.statusText;
                console.log(error);
            });

    }
    $scope.Deletetask = function(index) {

        var task = TaskService.get({ id: $scope.tasks[index].TaskId },
            function(result) {
                task.Message = $scope.task.message;
                task.IsCompleted = $scope.task.IsCompleted;
                $scope.task = { Message: "", Location: {}, Done: false };
            },
            function(error) {
                $scope.isError = true;
                $scope.errorMsg = error.statusText;
                console.log(error);
            });

        TaskService.delete({ id: $scope.task.TaskId },
            task,
            function(result) {
                $scope.task = { Message: "", Location: {}, Done: false };
            },
            function(error) {
                $scope.isError = true;
                $scope.errorMsg = error.statusText;
                console.log(error);
            });

        $scope.tasks.splice(index, 1);
    }

    $scope.Getall();

}]);