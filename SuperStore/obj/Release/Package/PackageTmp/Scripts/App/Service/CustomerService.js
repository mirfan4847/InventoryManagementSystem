var CustomerService = function () {

    var ajaxPost = function (param, url, done, fail) {
        $.post(url, param)
        .done(done)
        .fail()
    };
    var ajaxGet = function (param, url, done, fail) {
        $.post(url, param)
        .done(done)
        .fail()
    };
    return {
        AjaxPost: ajaxPost,
        AjaxGet: ajaxGet
    };
}();