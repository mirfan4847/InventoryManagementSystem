var SaleService = function () {
    var ajaxPost = function (param, url, done, fail) {
        $.post(url, param)
        .done(done)
        .fail(fail)
    };

    var ajaxGet = function (param, url, done, fail) {
        $.get(url, param)
        .done(done)
        .fail(fail)
    };

    return {
        AjaxPost: ajaxPost,
        AjaxGet: ajaxGet
    };
}();