angular.module("toDoApp.Services",[]).factory("Task", ["$resource"]);

function Task($resource) {
    return $resource("/api/tasks/:id",
        { id: "@id" },
        {
            update: {
                method: "PUT"
            },
            get: {
                method: "GET",
                interceptor: {
                    response: function(data) {
                        return data;
                    },
                    responseError: function(error) {
                        //$scope.isError = true;
                        //$scope.errorMsg = error.statusText;
                        console.log(error);
                    }
                }
            }
        });
    };