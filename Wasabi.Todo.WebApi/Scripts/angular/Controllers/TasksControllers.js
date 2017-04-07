angular.module('toDoApp.controllers', []).controller('TasksController', ['$scope', '$geolocation', 'Task', function($scope, $state, $geolocation, Task) {

        // variables
        $scope.tasks = [];
        $scope.isError = false;
        $scope.editIndex = false;
        $scope.labelTaskAction = "Add Task";

        // declare location object
        var location = {
            Latitude: "",
            Longitude: ""
        };

        // get geolocation from browser
        $geolocation.getCurrentPosition({
            timeout: 60000
        }).then(function(position) {
            location.Latitude = position.latitude;
            location.Longitude = position.longitude;
        });

        // get list of tasks from our api
        $scope.getAllTasks = function() {

            $scope.tasks = Task.query();
        };
        $scope.getAllTasks();


        //// ---------- Save  ---------- 
        //$scope.saveTask = function() {

        //    var taskService = new TaskService({ Message: $scope.task.Message, Location: location });

        //    if ($scope.editIndex === false) {

        //        taskService.$save()
        //            .then(function(result) {
        //                $scope.getAllTasks();
        //            })
        //            .catch(function(error) {
        //                $scope.isError = true;
        //                $scope.errorMsg = error.statusText;
        //                console.write(error);
        //            })
        //            .finally(function() {
        //                $scope.tasks.push({ task: $scope.task });
        //            });

        //    } else {

        //        TaskService.update({ id: $scope.task.TaskId },
        //            task,
        //            function(result) {
        //                $scope.task = { Message: '', Location: {}, Done: false };
        //            },
        //            function(error) {
        //                $scope.isError = true;
        //                $scope.errorMsg = error.statusText;
        //                console.write(error);
        //            });

        //        $scope.tasks[$scope.editIndex].task = $scope;
        //    }

        //    $scope.editIndex = false;
        //    $scope.labelTaskAction = "Add Task";
        //}


        //// ------------- Edit  --------------
        //$scope.editTask = function(index) {
        //    $scope.labelTaskAction = "Update Task";
        //    $scope.task = $scope.tasks[index];
        //    $scope.editIndex = index;
        //}


        //// ---------- Mark as Complete  ---------- 
        //$scope.completeTask = function(index) {
        //    $scope.tasks[index].IsCompleted = true;
        //}


        //// ---------- Mark as Incomplete  ---------- 
        //$scope.uncompleteTask = function(index) {
        //    $scope.tasks[index].IsCompleted = false;
        //}


        //// ------------- Delete  --------------
        //$scope.deleteTask = function(index) {

        //    var task = TaskService.get({ id: $scope.task.TaskId },
        //        function(result) {
        //            task.Message = $scope.task.Message;
        //            task.IsCompleted = $scope.task.IsCompleted;
        //            $scope.task = { Message: '', Location: {}, Done: false };
        //        },
        //        function(error) {
        //            $scope.isError = true;
        //            $scope.errorMsg = error.statusText;
        //            console.write(error);
        //        });

        //    TaskService.delete({ id: $scope.task.TaskId },
        //        task,
        //        function(result) {
        //            $scope.task = { Message: '', Location: {}, Done: false };
        //        },
        //        function(error) {
        //            $scope.isError = true;
        //            $scope.errorMsg = error.statusText;
        //            console.write(error);
        //        });

        //    $scope.tasks.splice(index, 1);
        //}
}])
.controller('TaskViewController', function ($scope, $stateParams, Task) {

    $scope.Task = Task.get({ id: $stateParams.id });
    //    var task = TaskService.get({ id: $scope.task.TaskId },
    //        function(result) {
    //            task.Message = $scope.task.Message;
    //            task.IsCompleted = $scope.task.IsCompleted;
    //            $scope.task = { Message: '', Location: {}, Done: false };
    //        },

}).controller('TaskCreateController', function ($scope, $state, $stateParams, Task) {

    $scope.Task = new Task();

    $scope.addTask = function () {
        $scope.Task.$save(function () {
            $state.go('Tasks');
        });
    }

}).controller('TaskEditController', function ($scope, $state, $stateParams, Task) {

    $scope.updateTask = function () {
        $scope.Task.$update(function () {
            $state.go('Tasks');
        });
    };

    $scope.loadTask = function () {
        $scope.Task = Task.get({ id: $stateParams.id });
    };

    $scope.loadTask();
});