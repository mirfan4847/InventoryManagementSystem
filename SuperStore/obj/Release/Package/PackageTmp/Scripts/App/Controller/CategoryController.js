var CategoryController = function (CategoryService) {
    function showProgress() {
        $('#loader').show();
    };
    function hideProgress() {
        $('#loader').hide();
    }

    var addCategory = function () {
        debugger;
        if ($('#CategoryName').val() == "") {
            $('#CategoryName').css('border', '1px solid red');
            toastr.error("Please enter category");
            return;
        } else {
            $('#CategoryName').css('border', '#d2d6de');
        }
        var param = {
            CategoryName: $('#CategoryName').val(),
            Description: $("#Description").val()
        };
        CategoryService.AddCategory(param, "/Category/AddCategory", addCategoryDone, addCategoryFail);
    };
    var addCategoryDone = function (data) {
        if (data == true) {
            toastr.success("Category Added");
            window.location.href = "/Category/Index";

        } else {
            toastr.error("Category not added, Please try again");
        }
    };

    var addCategoryFail = function () {
        toastr.error("Category not added, Please try again");
    };

    var loadCategories = function () {
        debugger;

        $.ajax({
            type: "GET",
            url: "/Category/GetAll",
            //data: { fromDate: DateTimeFrom, toDate: DateTimeTo, StatusId: 8 },
            beforeSend: function () {
                //showProgress();
            },
            success: function (data) {
                hideProgress();

                //var html = $(data).find('.login_wrapper');
                //if (html.length != 0) {
                //    window.location.href = "/login/login";
                //}
                //else {

                $('#loadCategory').html(data);
                var tableid = $('#categoryTable');

                $('#categoryTable thead .filters1_1 th').each(function () {
                    var classs = $('#categoryTable thead tr:eq(0) th').eq($(this).index()).attr('class');
                    var title = $('#categoryTable thead tr:eq(0) th').eq($(this).index()).text();
                    if (title != '' && classs != 'Hhide') {
                        $(this).html('<input type="text" placeholder="Search ' + title + '" style="width:100%" />');
                    }
                });
                var table1 = tableid.DataTable({
                    // dom: 'lBfrtip',
                    "order": [],
                    columnDefs: [{ orderable: false, targets: 'no-sort' }],
                    lengthChange: true,
                    responsive: true,
                    orderCellsTop: true,
                    "pageLength": 50,
                    "bDestroy": true,
                    "sPaginationType": "full_numbers",
                    "oTableTools": {
                        "aButtons": ["copy", "xls", "pdf"]
                    }

                });
                if (tableid.length != 0) {
                    table1.columns().eq(0).each(function (colIdx) {
                        $('input', $('.filters1_1 th')[colIdx]).on('keyup change', function () {
                            if (table1.column(colIdx).visible()) {
                                if (filterColIndex == 0 || filterColIndex == colIdx) {
                                    setTimeout(resetColIndex, 1500);
                                    console.log(colIdx);
                                    filterColIndex = colIdx;
                                    table1
                                        .column(colIdx)
                                        .search(this.value)
                                        .draw();
                                }
                            }
                        });
                    });
                }

                //}

            },
            error: function (error) {
                //console.log(error);
                hideProgress();
                toastr.error(error);
            }

        });


    };


    var updateCategory = function (categoryId) {
        debugger;
        if ($("#CategoryName").val() == "") {
            $("#CategoryName").css('border', '1px solid red');
            toastr.error("Please enter category");
            return;
        } else {
            $("#CategoryName").css('border', '#d2d6de');
        }
        param = {
            CategoryId: categoryId,
            CategoryName: $("#CategoryName").val()
        }
        showProgress();
        CategoryService.AddCategory(param, "/Category/Update", updateCategoryDone, updateCategoryFail);
    };

    var updateCategoryDone = function (data) {
        debugger;
        if (data == "Updated") {
            toastr.success("Category updated");
        } else {
            toastr.error("Category not updated");
        }
        hideProgress();
    };

    var updateCategoryFail = function () {
        hideProgress();
        toastr.error("Category not updated");
    };

    return {
        LoadCategories: loadCategories,
        UpdateCategory: updateCategory,
        AddCategory: addCategory
    };

}(CategoryService);