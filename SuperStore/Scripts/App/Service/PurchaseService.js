var PurchasedService = function () {
    var ajaxPost = function (param, url, done, fail) {
        $.post(url, param)
        .done(done)
        .fail(fail)
    };

    var ajaxGet = function (url, done, fail) {
        $.get(url)
        .done(done)
        .fail(fail)
    };

    return {
        AjaxPost: ajaxPost,
        AjaxGet: ajaxGet
    }
}();