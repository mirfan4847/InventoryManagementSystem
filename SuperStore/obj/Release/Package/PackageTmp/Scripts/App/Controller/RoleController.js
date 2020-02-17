var RoleController = function (RoleService) {
    function showProgress() {
        $("#loader").show();
    }
    function hideProgress() {
        $("#loader").hide();
    }
    debugger;

    var loadRoles = function () {
        debugger;
        $.ajax({
            type: "GET",
            url: "/Role/GetAll",
            //data: { fromDate: DateTimeFrom, toDate: DateTimeTo, StatusId: 8 },
            beforeSend: function () {
                //showProgress();
            },
            success: function (data) {
                // hideProgress();

                $('#loadRole').html(data);
                var tableid = $('#RoleTable');

                $('#RoleTable thead .filters th').each(function () {
                    var classs = $('#RoleTable thead tr:eq(0) th').eq($(this).index()).attr('class');
                    var title = $('#RoleTable thead tr:eq(0) th').eq($(this).index()).text();
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

    return {
        LoadRoles: loadRoles
    };
}(RoleService);