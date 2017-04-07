app.factory("UserProfile", function () {

    var isLoggedIn = function () {
        return sessionStorage.getItem("token") !== "null" && sessionStorage.getItem("token") !== null && sessionStorage.getItem("token") !== undefined && sessionStorage.getItem("token") !== "undefined";
    }

    var setProfile = function (username, tokenType, token, expires) {
        sessionStorage.setItem("userName", username);
        sessionStorage.setItem("tokenType", tokenType);
        sessionStorage.setItem("token", token);
        sessionStorage.setItem("expires", expires);
    };

    var getProfile = function () {
        //if (!isLoggedIn()) return null;

        var profile = {
            isLoggedIn: isLoggedIn(),
            username: sessionStorage.getItem("userName"),
            tokenType: sessionStorage.getItem("tokenType"),
            token: sessionStorage.getItem("token"),
            expires: sessionStorage.getItem("expires")
        };

        return profile;
    }

    var clearProfile = function () {
        sessionStorage.setItem("token", "null");
        sessionStorage.setItem("userName", "null");
        sessionStorage.setItem("tokenType", "null");
        sessionStorage.setItem("expires", "null");
    }

    return {
        IsLoggedIn: isLoggedIn,
        SetProfile: setProfile,
        GetProfile: getProfile,
        ClearProfile: clearProfile
    }
});
