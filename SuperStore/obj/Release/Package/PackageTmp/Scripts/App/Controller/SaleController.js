var filterColIndex = 0;

function resetColIndex() {
    filterColIndex = 0;
}

var SaleController = function (SaleService) {

    function showProgress() {
        $('#loader').show();
    };
    function hideProgress() {
        $('#loader').hide();
    }

    // sale item
    var saleProduct = function () {
        debugger;
        if ($("#ProductId option:selected").val() == "0") {
            $("#ProductId").css("border", "1px solid red");
            toastr.error("Please select Product Name");
            return;
        } else {
            $("#ProductId").css("border", "1px solid #ccc");
        }
        if ($("#UnitId option:selected").val() == "0") {
            $("#UnitId").css("border", "1px solid red");
            toastr.error("Please select Product Name");
            return;
        } else {
            $("#UnitId").css("border", "1px solid #ccc");
        }
        if ($("#RetailPrice").val() == "") {
            $("#RetailPrice").css("border", "1px solid red");
            toastr.error("Please select Product Name");
            return;
        } else {
            $("#RetailPrice").css("border", "1px solid #ccc");
        }
        if ($("#Quantity").val() == "") {
            $("#Quantity").css("border", "1px solid red");
            toastr.error("Please select Product Name");
            return;
        } else {
            $("#Quantity").css("border", "1px solid #ccc");
        }

        var param = {
            ProductName: $('#ProductId option:selected').text(),
            ProductId: $('#ProductId option:selected').val(),
            UnitId: $('#UnitId option:selected').val(),
            UnitName: $('#UnitId option:selected').text(),
            Price: $('#Price').val(),
            Quantity: $('#Quantity').val(),
            SubTotal: $("#Subtotal").val()
        };
        SaleService.AjaxPost(param, "/Sale/CreateNewSale_Post", saleProductDone, saleProductFail);
    };
    var saleProductDone = function (data) {
        debugger;
        toastr.success("Item Added");
        $('#loadPurchaseTable').html("");
        $('#loadPurchaseTable').html(data);
        ClearField();
    };
    var saleProductFail = function () {
        debugger;
        toastr.error("Data not saved");
    };


    function ClearField() {
        $('#ProductId').val(0);
        $('#UnitId').val(0);
        $('#CostPrice').val('');
        $('#RetailPrice').val('');
        $('#Quantity').val('');
        $("#Subtotal").val('');
    }

    // end sale item

    // insert data into db
    var saleItem = function () {
        debugger;
        var obj = {};
        var array = [];

        $('[id*=divCreateContainer]').map(function () {
            obj = {
                UnitId: $(this).find(".UnitId").val(),
                ProductId: $(this).find(".ProductId").val(),
                Price: $(this).find(".Price").val(),
                Quantity: $(this).find(".Quantity").val()
            }
            array.push(obj);
        }).get();

        var model = {
            NetPrice: $("#txtTotalAmount").val(),
            Discount: $("#txtdiscount").val(),
            PaymentType: $("#ddlPaymentMethod option:selected").text(),
            Paid: $("#txtPaid").val(),
            Due: $("#txtDue").val(),
            ArrSaleDetail: array,
            CustomerId: $('#CustomerId option:selected').val()
        };
        showProgress();
        SaleService.AjaxPost(model, "/Sale/SaleItem_Post", saleItemDone, saleItemFail);
    };
    var saleItemDone = function (data) {
        debugger;
        hideProgress();
        if (data == true) {
            toastr.success("Item Sold successfully");
            window.location.reload();
        }
        else
            toastr.error("Not sale, Try again");
    };
    var saleItemFail = function () {
        hideProgress();
        toastr.error("Not sale, Try again");
    };

    //


    var loadSales = function () {
        debugger;

        $.ajax({
            type: "GET",
            url: "/Sale/GetAll",
            //data: { fromDate: DateTimeFrom, toDate: DateTimeTo, StatusId: 8 },
            beforeSend: function () {
                //showProgress();
            },
            success: function (data) {
                hideProgress();
                $('#loadSaleData').html(data);
                var tableid = $('#tblSale');

                $('#tblSale thead .filters_1 th').each(function () {
                    var classs = $('#tblSale thead tr:eq(0) th').eq($(this).index()).attr('class');
                    var title = $('#tblSale thead tr:eq(0) th').eq($(this).index()).text();
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
                //console.log(error);
                hideProgress();
                toastr.error(error);
            }

        });


    };


    var getSaleDetail = function () {
        debugger;
        var ProductIds = $("#divMultiSelectProduct").find("#ProductId").val();
        if (ProductIds == null) {
            $("#divMultiSelectProduct").css("border", "1px solid red");
            toastr.error("Please select Product");
            return;
        } else {
            $("#divMultiSelectProduct").css("border", "1px solid #ccc");
        }

        //Single textbox daterange
        //var daterangepicker = $('#daterangepicker').text().split("-");
        //var fromdate = daterangepicker[0].trim();
        //var toDate = daterangepicker[1].trim();

        var fromdate = $('#FromDate').val();
        var toDate = $('#ToDate').val();
        if (fromdate == "") {
            $('#FromDate').css("border", "1px solid red");
            toastr.error("Please select from date");
            return;
        } else {
            $('#FromDate').css("border", "1px solid #ccc");
        }

        if (fromdate == "") {
            $('#ToDate').css("border", "1px solid red");
            toastr.error("Please select to date");
            return;
        } else {
            $('#ToDate').css("border", "1px solid #ccc");
        }

        var param = {
            ProductIds: ProductIds,
            fromDate: fromdate,
            toDate: toDate
        };
        SaleService.AjaxPost(param, "/Sale/SaleDetail", GetSaleDetailDone, GetSaleDetailFail);
    };
    var GetSaleDetailDone = function (data) {
        hideProgress();
        $('#loadSaleDetail').html('');
        $('#loadSaleDetail').html(data);
        var tableid = $('#saleTable');
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
    var GetSaleDetailFail = function () {
        hideProgress();
    };

    return {
        SaleProduct: saleProduct,
        SaleItem: saleItem,
        LoadSales: loadSales,
        GetSaleDetail: getSaleDetail
    };
}(SaleService);