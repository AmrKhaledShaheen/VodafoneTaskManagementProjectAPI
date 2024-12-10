$(document).ready(function () {
    FillDataTable();

});


var Status = "";
var FilterBy = "";
var DateFrom = "";
var DateTo = "";

function FillDataTable() {
    $('#datatable').dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Task/GetPaggedDataFilter",
            "type": "GET",
            "datatype": "json",
            "data": function (d) {
                d.Status = Status;
                d.FilterBy = FilterBy;
                d.DateFrom = DateFrom;
                d.DateTo = DateTo;
                return d;
            },
            "dataSrc": function (json) {
                return json.data;
            }
        },
        "columnDefs": [
            {
                "targets": [0],
                "visible": true,
                "searchable": false
            },
            {
                "targets": [1],
                "visible": true,
                "searchable": false
            },
            {
                "targets": [2],
                "visible": true,
                "searchable": false
            },
            {
                "targets": [3],
                "visible": true,
                "searchable": false
            },
            {
                "targets": [4],
                "visible": true,
                "searchable": false
            },
            {
                "targets": [5],
                "visible": true,
                "searchable": false
            },
            {
                "targets": [6],
                "visible": true,
                "searchable": false
            },
            {
                "targets": [7],
                "visible": true,
                "searchable": false
            }
        ],
        "columns": [
            {
                "data": "id",
                "name": "id",
                "autowidth": "3%"
            },
            {
                "data": "title",
                "name": "Title",
                "autowidth": "5%"
            },
            {
                "data": "description",
                "name": "Description",
                "autowidth": "5%"
            },
            {
                "data": "status",
                "name": "Status",
                "autowidth": "6%"
            },
            {
                "data": "startDate",
                "name": "StartDate",
                "autowidth": "4%"
            },
            {
                "data": "dueDate",
                "name": "DueDate",
                "autowidth": "5%"
            },
            {
                "data": "completionDate",
                "name": "CompletionDate",
                "autowidth": "5%"
            } ,
            {
                "render": function (data, type, row) {
                    return `<a href="/Task/EditView/${row.id}" class="dropdown-item text-primary" >
                                            <i class="fa-solid fa-pen-to-square"></i>Edit
                            </a>
                            <a href="#" class="dropdown-item text-primary" onclick = "onClickDeleteTask(${row.id})">
                                            <i class="fa-solid fa-pen-to-square"></i>Delete
                            </a>`;
                }
            }
        ],
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'copyHtml5',
                text: 'Copy',
                className: 'btn btn-secondary' // Add your custom class here
            },
            {
                extend: 'excelHtml5',
                text: 'Excel',
                className: 'btn btn-success' // Add your custom class here
            },
            {
                extend: 'csvHtml5',
                text: 'CSV',
                className: 'btn btn-warning' // Add your custom class here
            },
            {
                extend: 'pdfHtml5',
                text: 'PDF',
                className: 'btn btn-info' // Add your custom class here
            }
        ],
        "initComplete": function () {
            $('#loading').hide();
        }
    });
}

function onClickDeleteTask(id) {
    $.ajax({
        url: "/Task/DeleteTask/"+id,
        method: "POST",
        contentType: "application/json",
        success: function (Response) {
            if (Response.succeeded) {
                var table = $('#datatable').DataTable();
                table.ajax.reload();
            }
        },
        error: function (xhr, status, error) {
            $("#AlertMessage").text("Failure");
            $("#AlertMessage").show();
        }
    });
}
function onChangeFilter() {
    if ($("#Status").val() !== "") {
        Status = $("#Status").val();
    }
    if ($("#FilterBy").val() !== "") {
        FilterBy = $("#FilterBy").val();
    }
    if ($("#DateFrom").val() !== "") {
        DateFrom = $("#DateFrom").val();
    }
    if ($("#DateTo").val() !== "") {
        DateTo = $("#DateTo").val();
    }
    var table = $('#datatable').DataTable();
    table.ajax.reload();
}