// Injects an HTTP interceptor that replaces a "Bearer" authorization header
// with the current Bearer token.
app.factory('oauthHttpInterceptor', function (OAuth) {
    return {
        request: function (config) {
            // This is just example logic, you could check the URL (for example)
            if (config.headers.Authorization === 'Bearer') {
                config.headers.Authorization = 'Bearer ' + btoa(OAuth.accessToken);
            }
            return config;
        }
    };
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('oauthHttpInterceptor');
});