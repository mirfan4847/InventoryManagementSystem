var PurchasedController = function (PurchasedService) {
    var counter = 0;
    function showProgress() {
        $('#loader').show();
    };
    function hideProgress() {
        $('#loader').hide();
    }

    var loadPurchased = function () {
        debugger;

        $.ajax({
            type: "GET",
            url: "/Purchase/GetAll",
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

                $('#loadPurchase').html(data);
                var tableid = $('#tblpurchase');

                $('#tblpurchase thead .filters_1 th').each(function () {
                    var classs = $('#tblpurchase thead tr:eq(0) th').eq($(this).index()).attr('class');
                    var title = $('#tblpurchase thead tr:eq(0) th').eq($(this).index()).text();
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




    var purchasedProduct = function () {
        debugger;
        var obj = {};
        var array = [];

        $('[id*=divCreateContainer]').map(function () {
            obj = {
                UnitId: $(this).find("#UnitId").val(),
                ProductId: $(this).find("#ProductId").val(),
                CostPrice: $(this).find("#CostPrice").val(),
                Quantity: $(this).find("#Quantity").val(),
                RetailPrice: $(this).find("#RetailPrice").val()
            }
            array.push(obj);
        }).get();

        var model = {
            CategoryId: $("#CategoryId option:selected").val(),
            SubCategoryId: $("#SubCategoryId option:selected").val(),
            SupplierId: $("#supplierId option:selected").val(),
            TotalPrice: $("#TotalPrice").val(),
            Discount: $("#Discount").val(),
            ddlPaymentMethod: $("#ddlPaymentMethod option:selected").val(),
            Paid: $("#Paid").val(),
            Due: $("#Due").val(),
            PaymentMethod: $("#PaymentMethod option:selected").val(),
            arryPurchaseDetail: array
        };
        showProgress();
        PurchasedService.AjaxPost(model, "/Purchase/PurchasedProduct_Post", purchasedProductDone, purchasedProductFail);
    };
    var purchasedProductDone = function (data) {
        debugger;
        hideProgress();
        if (data == true)
            toastr.success("Product purchased successfully");
        else
            toastr.error("Product not purchased");
    };
    var purchasedProductFail = function () {
        hideProgress();
        toastr.error("Product not purchased");
    };


    var loadSubcategory = function (container) {
        debugger;
        //if ($(container).val() == "1") {
        //    return;
        //}
        var param = {
            CategoryId: $(container).val(),
        };
        PurchasedService.AjaxPost(param, "/Product/GetSubCategory", loadSubcategoryDone, loadSubcategoryfail);
    };

    var loadSubcategoryDone = function (data) {
        debugger;
        hideProgress();
        $("#SubCategoryId").html("");
        $("#SubCategoryId").append('<option value="0">---Select One---</option>')
        $.each(data, function (i, d) {
            $("#SubCategoryId").append('<option value="' + d.SubCategoryId + '">' +
                         d.SubCategoryName + '</option>');
        });
    };
    var loadSubcategoryfail = function () {
        hideProgress();
        toastr.error("Not Load");
    };


    var loadProductBySubcategory = function (container) {
        debugger;
        //if ($(container).val() == "1") {
        //    return;
        //}
        var param = {
            SubcategoryId: $(container).val(),
        };
        PurchasedService.AjaxPost(param, "/Product/LoadProductBySubcategory", loadProductBySubcategoryDone, loadProductBySubcategoryFail);
    };

    var loadProductBySubcategoryDone = function (data) {
        debugger;
        hideProgress();
        $("#ProductId").html("");
        $("#ProductId").append('<option value="0">---Select One---</option>')
        $.each(data, function (i, d) {
            $("#ProductId").append('<option value="' + d.ProductId + '">' +
                         d.Name + '</option>');
        });
    };
    var loadProductBySubcategoryFail = function () {
        hideProgress();
        toastr.error("Not Load");
    };


    var addCreateGrid = function () {
        debugger;
        counter++;

        var createContainer = document.getElementById("divCreateContainer");
        var clone = createContainer.cloneNode(true);
        clone.setAttribute('id', 'divCreateContainer' + counter);
        $(clone).children().find('input').each(function () {
            $(this).val('');
            $(this).css('border', '1px solid #ccc');
        });
        $(clone).children().find('img').removeClass('addCreateGrid').addClass('removeCreateGrid');
        $(clone).children().find('img').attr('onclick', 'PurchasedController.RemoveCreateGrid("divCreateContainer' + counter + '");');

        $(clone).children().find('input,select').css({ "border": "1px solid #ccc" });
        document.getElementById('ParentContainer').appendChild(clone);
    }
    function removeCreateGrid(inputFieldId) {
        //$('#' + inputFieldId).parent('div').parent('div').parent('div').remove();
        $('#' + inputFieldId).remove();
        counter--;
    }

    var PurchasePro = function () {
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
            CostPrice: $('#CostPrice').val(),
            RetailPrice: $('#RetailPrice').val(),
            Quantity: $('#Quantity').val(),
            SubTotal: $("#Subtotal").val()
        };
        PurchasedService.AjaxPost(param, "/Purchase/PurchasedPro_Post", PurchaseProDone, PurchaseProFail);
    };
    var PurchaseProDone = function (data) {
        toastr.success("Item Added");
        $('#loadPurchaseTable').html("");
        $('#loadPurchaseTable').html(data);
        ClearField();
    };
    var PurchaseProFail = function () {
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

    // Purchase Revamp
    var purchasedProduct = function () {

        if ($('#txtPaid').val() == "") {
            $('#txtPaid').css("border", "1px solid red");
            toastr.error("Please enter paid value");
            return;
        } else {
            $('#txtPaid').css("border", "1px solid #ccc");
        }

        var model = $('#productModel').val();
        debugger;
        var obj = {};
        var array = [];

        var UnitId = [];
        var ProductId = [];
        var Quantity = [];
        var RetailPrice = [];

        $('[id*=divCreateContainer]').map(function () {
            obj = {
                UnitId: $(this).find(".UnitId").val(),
                ProductId: $(this).find(".ProductId").val(),
                Quantity: $(this).find(".Quantity").val(),
                RetailPrice: $(this).find(".RetailPrice").val()
            }
            array.push(obj);
        }).get();

        //var UnitId = $('[id*=UnitId]').map(function () {
        //    return this.value
        //}).get();
        //var ProductId = $('[id*=ProductId]').map(function () {
        //    return this.value
        //}).get();
        //var Quantity = $('[id*=Quantity]').map(function () {
        //    return this.value
        //}).get();
        //var RetailPrice = $('[id*=RetailPrice]').map(function () {
        //    return this.value
        //}).get();


        var model = {
            //CategoryId: $("#CategoryId option:selected").val(),
            //SubCategoryId: $("#SubCategoryId option:selected").val(),
            //SupplierId: $("#supplierId option:selected").val(),
            TotalPrice: $("#txtTotalAmount").val(),
            Discount: $("#txtdiscount").val(),
            NetTotal: $("#netTotal").val(),
            Paid: $("#txtPaid").val(),
            Due: $("#txtDue").val(),
            Method: $("#ddlPaymentMethod option:selected").text(),
            ArrPurchaseDetail: array,
            SupplierId: $('#SupplierId option:selected').val()
        };
        showProgress();
        PurchasedService.AjaxPost(model, "/Purchase/PurchasedProduct_Post", purchasedProductDone, purchasedProductFail);
    };
    var purchasedProductDone = function (data) {
        debugger;
        hideProgress();
        if (data == true) {
            toastr.success("Product purchased successfully");
            window.location.reload();
        }
        else
            toastr.error("Product purchased successfully");
    };
    var purchasedProductFail = function () {
        hideProgress();
    };




    return {
        LoadPurchased: loadPurchased,
        PurchasedProduct: purchasedProduct,
        LoadSubcategory: loadSubcategory,
        LoadProductBySubcategory: loadProductBySubcategory,
        AddCreateGrid: addCreateGrid,
        RemoveCreateGrid: removeCreateGrid,
        PurchasePro: PurchasePro
    };

}(PurchasedService);