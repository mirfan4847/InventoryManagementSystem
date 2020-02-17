var InterfaceService = function () {
    var addInterface = function (param, url, done, fail) {
        $.post(url, param)
        .done(done)
        .fail(fail);
    };

    var editInterface = function (param, url, done, fail) {
        $.post(url, param)
        .done(done)
        .fail(fail);
    };

    return {
        AddInterface: addInterface,
        EditInterface: editInterface
    };
}();