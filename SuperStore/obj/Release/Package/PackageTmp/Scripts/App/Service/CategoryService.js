var CategoryService = function () {
    var addCategory = function (param, url, done, fail) {
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
        AddCategory: addCategory,
        EditCategory: editCategory
    };
}();