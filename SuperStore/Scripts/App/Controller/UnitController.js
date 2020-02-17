var UnitController = function (UnitService) {

    function showProgress() {
        $('#loader').show();
    };
    function hideProgress() {
        $('#loader').hide();
    }


    var loadUnits = function () {
        debugger;
        $.ajax({
            type: "GET",
            url: "/Unit/GetAll",
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

                $('#loadUnits').html(data);
                var tableid = $('#UnitTable');

                $('#UnitTable thead .filters_1 th').each(function () {
                    var classs = $('#UnitTable thead tr:eq(0) th').eq($(this).index()).attr('class');
                    var title = $('#UnitTable thead tr:eq(0) th').eq($(this).index()).text();
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


    var CreatUnit = function () {
        debugger;
        var name = $('#Name');
        if ($(name).val() == "") {
            $(name).css({ "border": "1px solid red" });
            return toastr.error("Please enter name");
        } else
            $(name).css({ "border": "1px solid #ccc" });

        var param = {
            Name: name
        };
        showProgress();
        UnitService.PostAjax(param, "Unit/Create", CreatUnitDone, CreatUnitFail);
    };

    var CreatUnitDone = function (data) {
        hideProgress();
        if (data == false)
            toastr.error("Data not saved");
        else
            toastr.success("Data saved successfully");
    };

    var CreatUnitFail = function () {
        hideProgress();
        toastr.error("Data not saved");
    };


    var updateUnit = function () {
        debugger;
        var name = $("#Name");
        if ($(name).val() == "") {
            $(name).css({ "border": "1px solid red" });
            return toastr.error("Please enter name");
        }
        $(name).css({ "border": "1px solid #ccc" });

        var param = {
            Name: $(name).val(),
            UnitId: $('#UnitId').val(),
            Description: $('#Description').val()
        };
        showProgress();
        UnitService.PostAjax(param, "/Unit/Update", updateUnitDone, updateUnitFail);
    };

    var updateUnitDone = function (data) {
        hideProgress();
        if (data == false)
            toastr.error("Data not saved");
        else
            toastr.success("Data saved successfully");
    };

    var updateUnitFail = function () {
        hideProgress();
        toastr.error("Data not saved");
    };


    var deactiveunit = function (id) {
        var param = {
            id: id
        };
        showProgress();
        UnitService.PostAjax(param, "/Unit/Deactive", updateUnitDone, updateUnitFail);
    };
    var deactiveunitDone = function (data) {
        hideProgress();
        if (data == false)
            toastr.error("Not deactive");
        else
            toastr.success("Deactive successfully");
    };
    var deactiveunitFail = function () {
        hideProgress();
        toastr.error("Not deactive");
    };
    return {
        LoadUnits: loadUnits,
        CreatUnit: CreatUnit,
        UpdateUnit: updateUnit,
        Deactiveunit: deactiveunit
    }
}(UnitService);