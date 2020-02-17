var ProductService = function () {
    var loadSubcateory = function (param,url,done,fail) {
        $.post(url, param)
        .done(done)
        .fail(fail)
    };

    var addProduct = function (param, url, done, fail) {
        debugger;
        $.post(url, param)
        .done(done)
        .fail(fail)
    };

    return {
        LoadSubcateory: loadSubcateory,
        AddProduct: addProduct
    };
}();