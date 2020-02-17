var UserService = function () {
    var registration = function (param, url, done, fail) {
        $.post(url, param)
        .done(done)
        .fail(fail)
    };

    return {
        Registration: registration
    };
}();