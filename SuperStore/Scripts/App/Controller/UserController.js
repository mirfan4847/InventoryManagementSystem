var UserController = function (UserService) {

    function showProgress() {
        $('#loader').show();
    };
    function hideProgress() {
        $('#loader').hide();
    }

    var loadUsers = function () {
        debugger;

        $.ajax({
            type: "GET",
            url: "/Users/GetAllUser",
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

                $('#loadUsers').html(data);
                var tableid = $('#UserTable');

                $('#UserTable thead .filter_1 th').each(function () {
                    var classs = $('#UserTable thead tr:eq(0) th').eq($(this).index()).attr('class');
                    var title = $('#UserTable thead tr:eq(0) th').eq($(this).index()).text();
                    if (title != '' && classs != 'Hhide') {
                        $(this).html('<input type="text" placeholder="Search ' + title + '" style="width:100%" />');
                    }
                });
                debugger;
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
            }

        });
    };


    var registration = function () {
        debugger;
        if ($("#Firstname").val() == "") {
            $("#Firstname").css('border', '1px solid red');
            toastr.error("Please enter First Name");
            return;
        } else {
            $("#Firstname").css('border', '1px solid #ccc');
        }
        if ($("#Lastname").val() == "") {
            $("#Lastname").css('border', '1px solid red');
            toastr.error("Please enter Last Name");
            return;
        } else {
            $("#Lastname").css('border', '1px solid #ccc');
        }
        if ($("#Phone").val() == "") {
            $("#Phone").css('border', '1px solid red');
            toastr.error("Please enter Phone Number");
            return;
        } else {
            $("#Phone").css('border', '1px solid #ccc');
        }
        if ($("#Address").val() == "") {
            $("#Address").css('border', '1px solid red');
            toastr.error("Please enter Address");
            return;
        } else {
            $("#Address").css('border', '1px solid #ccc');
        }
        if ($("#Username").val() == "") {
            $("#Username").css('border', '1px solid red');
            toastr.error("Please enter Username");
            return;
        } else {
            $("#Username").css('border', '1px solid #ccc');
        }
        if ($("#Password").val() == "") {
            $("#Password").css('border', '1px solid red');
            toastr.error("Please enter Password");
            return;
        } else {
            $("#Password").css('border', '1px solid #ccc');
        }

        var param = {
            Firstname: $("#Firstname").val(),
            Lastname: $("#Lastname").val(),
            Phone: $("#Phone").val(),
            Address: $("#Address").val(),
            Username: $("#Username").val(),
            Password: $("#Password").val(),
        };
        showProgress();
        UserService.Registration(param, "Registration", registrationDone, registrationFail);
    };

    var clearRegistration = function () {
        $("#Firstname").val(''),
        $("#Lastname").val(''),
        $("#Phone").val(''),
        $("#Address").val(''),
        $("#Username").val(''),
        $("#Password").val(''),
         $("#ConfirmPassword").val('')
    };

    var registrationDone = function (data) {
        hideProgress();
        if (data == true) {
            toastr.success("Registration Successfully");
            clearRegistration();
        } else {
            toastr.error("Not register, Please try again");
        }
    };
    var registrationFail = function () {
        hideProgress();
    };

    return {
        LoadUsers: loadUsers,
        Registration: registration
    };
}(UserService);