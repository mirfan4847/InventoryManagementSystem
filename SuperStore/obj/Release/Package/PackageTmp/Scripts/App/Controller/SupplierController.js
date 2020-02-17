var SupplierController = function (SupplierService) {
    function showProgress() {
        $('#loader').show();
    };
    function hideProgress() {
        $('#loader').hide();
    }

    var addCategory = function () {
        if ($('#').val()) {
            $('#').css('border', '1px red')
            return;
        };

        var param = {

        };
        SupplierService.AddCategory(param, "", addCategoryDone, addCategoryFail);
    };
    var addCategoryDone = function (data) {

    };

    var addCategoryFail = function () {

    };

    var loadSupplier = function () {
        debugger;

        $.ajax({
            type: "GET",
            url: "/Supplier/AllSupplier",
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

                $('#loadSupplier').html(data);
                var tableid = $('#SupplierTable');

                $('#SupplierTable thead .filters1_1 th').each(function () {
                    var classs = $('#SupplierTable thead tr:eq(0) th').eq($(this).index()).attr('class');
                    var title = $('#SupplierTable thead tr:eq(0) th').eq($(this).index()).text();
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
        param = {
            categoryId: categoryId
        }
        showProgress();
        CategoryService.UpdateCategory(param, "/Category/Update", updateCategoryDone, updateCategoryFail);
    };

    var updateCategoryDone = function (data) {
        debugger;
        $('#').show();
        hideProgress();
    };

    var updateCategoryFail = function () {
        hideProgress();
    };

    return {
        LoadSupplier: loadSupplier
    };

}(SupplierService);