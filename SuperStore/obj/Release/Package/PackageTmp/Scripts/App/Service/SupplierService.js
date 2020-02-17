var SupplierService = function () {
    var addSupplier = function (param, url, done, fail) {
        $.post(url, param)
        .done(done)
        .fail(fail)
    };
    var editCategory = function (param, url, done, fail) {
        $.post(param, url)
         .done(done)
         .fail(fail)
    };
    return {
        AddSupplier: addSupplier
    };
}();