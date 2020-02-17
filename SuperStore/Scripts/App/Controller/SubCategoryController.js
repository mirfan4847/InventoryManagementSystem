var SubCategoryController = function (SubCategoryService) {
    function showProgress() {
        $('#loader').show();
    };
    function hideProgress() {
        $('#loader').hide();
    }

    var addSubCategory = function () {
        showProgress();
        if ($("#CategoryId option:selected").val() == "") {
            $("#CategoryId").css("border", "1px solid red");
            toastr.error("Please select category");
            return;
        } else {
            $("#CategoryId").css("border", "1px solid #d2d6de");
        }
        if ($("#SubCategoryName").val() == "") {
            $("#SubCategoryName").css("border", "1px solid red");
            toastr.error("Please enter sub category");
            return;
        } else {
            $("#SubCategoryName").css("border", "1px solid #d2d6de");
        }

        var param = {
            CategoryId: $("#CategoryId option:selected").val(),
            SubCategoryName: $("#SubCategoryName").val(),
            Description: $('#Description').val()
        };
        SubCategoryService.AddSubCategory(param, "/SubCategory/AddSubCategory", addSubCategoryDone, addSubCategoryFail);
        hideProgress();
    };
    var addSubCategoryDone = function (data) {
        hideProgress();
        if (data == true) {
            toastr.success("Data saved");
            window.location.href = "/SubCategory/Index";
        } else {
            toastr.error("Data not saved");
        }
    };

    var addSubCategoryFail = function () {
        hideProgress();
        toastr.error("Data not saved");
    };

    var loadSubCategories = function () {
        debugger;
        $.ajax({
            type: "Get",
            url: "/SubCategory/GetAll",

            beforeSend: function () {
                showProgress();
            },
            success: function (data) {
                debugger;
                hideProgress();
                $('#loadSubCategory').html(data);
                var tableid = $('#SubcategoryTable');
                //var html = $(data).find('.login_wrapper');
                //if (html.length != 0) {
                //    window.location.href = "/login/login";
                //}
                //else {

                $('#SubcategoryTable thead .filters_1 th').each(function () {
                    var classs = $('#SubcategoryTable thead tr:eq(0) th').eq($(this).index()).attr('class');
                    var title = $('#SubcategoryTable thead tr:eq(0) th').eq($(this).index()).text();
                    if (title != '' && classs != 'Hhide') {
                        $(this).html('<input type="text" placeholder="Search ' + title + '" />');
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
                        $('input', $('.filters_1 th')[colIdx]).on('keyup change', function () {
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
                hideProgress();
                toastr.error(error);
            }

        });
    };

    return {
        LoadSubCategories: loadSubCategories,
        AddSubCategory: addSubCategory
    };

}(SubCategoryService);