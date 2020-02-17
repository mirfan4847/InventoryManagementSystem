var UnitService = function () {
    var postAjax = function (param, url, done, fail) {
        $.post(url, param)
        .done(done)
        .fail(fail)
    };

    return {
        PostAjax: postAjax
    }
}();