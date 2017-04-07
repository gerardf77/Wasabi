app.factory("TaskService", ["$resource", "AuthRequestInterceptor", function ($resource, AuthRequestInterceptor) {
    return $resource("api/tasks/:id",
        { id: "@id" },
        {
            query: {
                method: "GET",
                interceptor: AuthRequestInterceptor,
                isArray: true
            },
            update: {
                method: "PUT"
            },
            get: {
                method: "GET"
            }
        });
    }
]);