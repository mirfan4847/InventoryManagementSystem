var StockService = function () {
    var ajaxPost = function (param, url, done, fail) {
        $.post(url, param)
        .done(done)
        .fail(fail);
    };

    return {
        AjaxPost: ajaxPost
    };
}();