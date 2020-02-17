var ProductController = function (ProductService) {
    var counter = 0;
    function showProgress() {
        $('#loader').show();
    };
    function hideProgress() {
        $('#loader').hide();
    }

    var loadProduct = function () {
        debugger;
        $.ajax({
            type: "Get",
            url: "/Product/AllProduct",
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

                $('#loadProduct').html(data);
                var tableid = $('#ProductTable');

                $('#ProductTable thead .filter_1 th').each(function () {
                    var classs = $('#ProductTable thead tr:eq(0) th').eq($(this).index()).attr('class');
                    var title = $('#ProductTable thead tr:eq(0) th').eq($(this).index()).text();
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
                debugger;
                if (tableid.length != 0) {
                    table1.columns().eq(0).each(function (colIdx) {
                        $('input', $('.filter_1 th')[colIdx]).on('keyup change', function () {
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

    var loadSubcategory = function (container) {
        debugger;
        //if ($(container).val() == "1") {
        //    return;
        //}
        var param = {
            CategoryId: $(container).val(),
        };
        ProductService.LoadSubcateory(param, "/Product/GetSubCategory", loadSubcategoryDone, loadSubcategoryfail);
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

    var addProduct = function () {
        debugger;
        if ($('#Name').val() == "") {
            $('#Name').css('border', '1px solid red');
            toastr.error('Please enter Name');
            return;
        } else {
            $('#Name').css('border', '#d2d6de');
        }
        if ($('#Code').val() == "") {
            $('#Code').css('border', '1px solid red');
            toastr.error('Please generate code');
            return;
        } else {
            $('#Code').css('border', '#d2d6de');
        }
        if ($('#CategoryId option:selected').val() == "0") {
            $('#CategoryId').css('border', '1px solid red');
            toastr.error('Please select category');
            return;
        } else {
            $('#CategoryId').css('border', '#d2d6de');
        }

        if ($('#SubCategoryId option:selected').val() == "0") {
            $('#SubCategoryId').css('border', '1px solid red');
            toastr.error('Please select sub category');
            return;
        } else {
            $('#SubCategoryId').css('border', '#d2d6de');
        }

        var files = [];
        $(".attach").each(function () {
            var id = $(this).attr('id');
            files.push(id);
        });

        if ($('#Description').val() == "") {
            $('#Description').css('border', '1px solid red');
            toastr.error('Please enter Name');
            return;
        } else {
            $('#Description').css('border', '#d2d6de');
        }
        var param = {
            Name: $('#Name').val(),
            Code: $('#Code').val(),
            Description: $('#Description').val(),
            CategoryId: $('#CategoryId option:selected').val(),
            SubCategoryId: $('#SubCategoryId option:selected').val(),
            file: files
        };
        showProgress();
        ProductService.LoadSubcateory(param, "/Product/AddProduct", addProductDone, addProductFail);

    };

    var addProductDone = function (data) {
        debugger;
        hideProgress();
        if (data == true)
            toastr.success("Product added successfully");
        else
            toastr.error("Not added");
    };
    var addProductFail = function () {
        hideProgress();
        toastr.error("Not added");
    };

    var getSalePrice = function (contain) {
        debugger;
        var tax = "";
        if ($("#RetailPrice").val() == "") {
            return toastr.error("Please select Tax");
        }
        var retail = $("#RetailPrice").val();
        if ($(contain).val() == "1") {
            return toastr.error("Please select Tax");
        }
        else if ($(contain).val() == "2") {
            tax = parseFloat(5 / 100) * parseFloat(retail);
        }
        else if ($(contain).val() == "3") {
            tax = parseFloat(10 / 100) * parseFloat(retail);
        }
        else if ($(contain).val() == "4") {
            tax = parseFloat(15 / 100) * parseFloat(retail);
        }
        $("#SalePrice").val(tax + parseFloat(retail));
    };


    var loadProductBySubcategory = function (container) {
        debugger;
        //if ($(container).val() == "1") {
        //    return;
        //}
        var param = {
            SubcategoryId: $(container).val(),
        };
        ProductService.LoadSubcateory(param, "/Product/LoadProductBySubcategory", loadProductBySubcategoryDone, loadProductBySubcategoryFail);
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
        $(clone).children().find('#btnAttachment').attr('id', 'btnAttachment' + counter);
        $(clone).children().find('#fileUpload').attr('id', 'fileUpload' + counter);
        $(clone).children().find('img').attr('onclick', 'ProductController.RemoveCreateGrid("divCreateContainer' + counter + '");');
        $(clone).children().find('#atch').html('');
        $(clone).children().find('#atch').removeClass();
        $(clone).children().find('input,select').css({ "border": "" });
        document.getElementById('ParentContainer').appendChild(clone);
    }
    function removeCreateGrid(inputFieldId) {
        //$('#' + inputFieldId).parent('div').parent('div').parent('div').remove();
        $('#' + inputFieldId).remove();
        counter--;
    }

    var calculation = function (container) {
        $(container).on("keypress", cntrlCalculation(container))
    };

    var cntrlCalculation = function (container) {
        $(container).val()
    };

    return {
        LoadProduct: loadProduct,
        LoadSubcategory: loadSubcategory,
        GetSalePrice: getSalePrice,
        AddProduct: addProduct,
        LoadProductBySubcategory: loadProductBySubcategory,
        AddCreateGrid: addCreateGrid,
        RemoveCreateGrid: removeCreateGrid
    };
}(ProductService);