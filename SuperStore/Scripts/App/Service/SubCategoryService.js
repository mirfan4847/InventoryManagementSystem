var SubCategoryService = function () {
    var addSubCategory = function (param, url, done, fail) {
        $.post(url, param)
        .done(done)
        .fail(fail)
    };

    return {
        AddSubCategory: addSubCategory
    };
}();