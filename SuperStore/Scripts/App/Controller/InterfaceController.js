var InterfaceController = function (InterfaceService) {

    function showProgress() {
        $("#loader").show();
    }
    function hideProgress() {
        $("#loader").hide();
    }


    var loadInterface = function () {
        debugger;
        $.ajax({
            type: "GET",
            url: "/Interface/GetAll",
            //data: { fromDate: DateTimeFrom, toDate: DateTimeTo, StatusId: 8 },
            beforeSend: function () {
                //showProgress();
            },
            success: function (data) {
                // hideProgress();

                $('#loadInterface').html(data);
                var tableid = $('#tblInterface');

                $('#tblInterface thead .filters th').each(function () {
                    var classs = $('#tblInterface thead tr:eq(0) th').eq($(this).index()).attr('class');
                    var title = $('#tblInterface thead tr:eq(0) th').eq($(this).index()).text();
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
                        $('input', $('.filters th')[colIdx]).on('keyup change', function () {
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
                //hideProgress();
                //toastr.error(error);
            }

        });


    };

    var addInterface = function () {
        debugger;
        if ($('#Name').val() == "") {
            $('#Name').css("border", "1px solid red");
            return;
        } else {
            $('#Name').css({ "border": "0.2px solid  #f4f4f4" });
        }
        if ($('#Description').val() == "") {
            $('#Description').css("border", "1px solid red");
            return;
        } else {
            $('#Description').css({ "border": "0.2px solid  #f4f4f4" });
        }
        var param = {
            Name: Name,
            Description: Description
        }
        showProgress();
        InterfaceService.AddInterface(param, "/Interface/Add_post", addInterfaceDone, addInterfaceFail);
    }

    var addInterfaceDone = function (data) {
        hideProgress();
        toastr.error("Something went wrong, Please try again.");
    };
    addInterfaceFail = function () {
        hideProgress();
        toastr.error("Data not saved");
    }


    var MoveSingleRole = function (fromDropdown, toDropdown) {
        $("#ddlSelectedInterfaces").css("border", "1px solid #D1E5EE");
        var numbering = "";
        $("#" + fromDropdown + " option:selected").each(function () {

            var _item = $(this).val();
            if (!optionExists(toDropdown.replace("#", ""), _item)) {
                numbering = $("#" + toDropdown + " option").length + 1;
                $("#" + toDropdown).append('<option value="' + $(this).val() + '" >' + numbering + ") " + $(this).text().split(")")[1] + "</option>");
                $("#" + fromDropdown + " option[value=" + _item + "]").remove();
            }
        });
        ReOrderNumbering("#" + toDropdown);
        ReOrderNumbering("#" + fromDropdown);
    };
    var MoveTotalRoles = function (fromDropdown, toDropdown) {
        $("#ddlSelectedInterfaces").css("border", "1px solid #D1E5EE");
        var numbering = "";
        $("#" + fromDropdown + " option").each(function () {
            var _item1 = $(this).val();
            if (!optionExists(toDropdown.replace("#", ""), _item1)) {
                numbering = $("#" + toDropdown + " option").length + 1;
                $("#" + toDropdown).append('<option value="' + $(this).val() + '" >' + numbering + ") " + $(this).text().split(")")[1] + "</option>");
                $("#" + fromDropdown + " option[value=" + _item1 + "]").remove();
            }

        });
        ReOrderNumbering("#" + toDropdown);
        ReOrderNumbering("#" + fromDropdown);
    };
    function optionExists(elementId, val) {
        return $("#" + elementId + " option[value='" + val + "']").length !== 0
    }

    var addRolePermission = function () {
        var selected = [];
        $('#SelectedInterfaces > option').each(function (ind, obj) {
            var v = $(this).attr('value');
            selected.push(v);
        });

        var param = {
            InterfaceArr: selected,
            RoleId: $('#RoleId option:selected').val()
        };
        showProgress();
        InterfaceService.AddInterface(param, "/RolePermission/AddRolePermission", addRolePermissionDone, addRolePermissionFail);
    };

    var addRolePermissionDone = function (data) {
        hideProgress();
        if (data == true)
            toastr.success("Data saved successfully");
        else
            toastr.success("Data saved successfully");
    };

    var addRolePermissionFail = function () {
        hideProgress();
        toastr.error("Data not saved");
    };

    return {
        LoadInterface: loadInterface,
        AddInterface: addInterface,
        MoveSingleRole: MoveSingleRole,
        MoveTotalRoles: MoveTotalRoles,
        AddRolePermission: addRolePermission
    };
}(InterfaceService);