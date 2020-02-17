var StockController = function (StockService) {
    function showProgress() {
        $('#loader').show();
    };
    function hideProgress() {
        $('#loader').hide();
    }

    //var renderSubcategory = function (container) {
    //    debugger;
    //    $(container).on("change", renderSubcategoryCtrl);
    //};

    var renderSubcategory = function (id) {
        debugger;
        var param = {
            categoryId: id
        };
        StockService.AjaxPost(param, "/Stock/GetSubcategoryByCategoryId", renderSubcategoryCtrlDone, renderSubcategoryCtrlFail);
    };
    var renderSubcategoryCtrlDone = function (data) {
        debugger;
        hideProgress();
        if (data != null) {
            var SubCategoryId = "";
            $.each(data, function (i, d) {
                SubCategoryId += '<option value="' + d.SubCategoryId + '">' +
                             d.SubCategoryName + '</option>';
            });
            $("#divMultiSelectSubCategory").find("#SubCategoryId").html(SubCategoryId);
        }
    };
    var renderSubcategoryCtrlFail = function () {
        hideProgress();
        toastr.error("Sub category not load");
    };



    var searchStock = function () {
        debugger;
        var CategoryId = $("#divMultiSelectCategory").find("#CategoryId").val();
        var SubCategoryId = $("#divMultiSelectSubCategory").find("#SubCategoryId").val();
        var ProductId = $("#divMultiSelectProduct").find("#ProductId").val();

        var param = {
            CategoryIds: CategoryId,
            SubcategoryIds: SubCategoryId,
            ProductsIds: ProductId
        };
        showProgress();
        StockService.AjaxPost(param, "/Stock/ShowStock", searchStockDone, searchStockFail);
    };
    var searchStockDone = function (data) {
        hideProgress();
        $('#loadStock').html('');
        $('#loadStock').html(data);
        var tableid = $('#StockTable');
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

    };
    var searchStockFail = function () {
        hideProgress();
    };

    return {
        RenderSubcategory: renderSubcategory,
        SearchStock: searchStock
    }

}(StockService);