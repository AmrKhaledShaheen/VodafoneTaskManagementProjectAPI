$(document).ready(function () {
    const path = window.location.pathname;
    const segments = path.split('/');
    const id = segments.pop() || segments[segments.length - 1];
    $("#BtnEditTask").click(function () {
        if (!validate())
            return;
        $.ajax({
            url: "/Task/EditTask",
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                Id: id,
                Title: $("#Title").val().trim(),
                Description: $("#Description").val().trim(),
                Status: $("#Status").val(),
                StartDate: $("#StartDate").val(),
                DueDate: $("#DueDate").val(),
                CompletionDate: $("#CompleteDate").val()
            }),
            success: function (Response) {
                if (Response.succeeded) {
                    $("#AlertMessage").text(Response.message);
                    $("#AlertMessage").show();
                } else {
                    $("#AlertMessage").text(Response.message);
                    $("#AlertMessage").show();
                }
                $('html, body').animate({ scrollTop: 0 }, 'fast');
            },
            error: function (xhr, status, error) {
                $("#AlertMessage").text("Failure: " + error);
                $("#AlertMessage").show();
            }
        });
    });
});


function validate() {
    if ($("#Title").val().length < 3) {
        $("#AlertMessage").text("Task Title is Very Short");
        $("#AlertMessage").show();
    } else if ($("#Description").val().trim().length < 3) {
        $("#AlertMessage").text("Task Description is Very Short");
        $("#AlertMessage").show();
    } else if ($("#Status").val() != "1" && $("#Status").val() != "2" && $("#Status").val() != "3") {
        $("#AlertMessage").text("Choose Task Status");
        $("#AlertMessage").show();
    } else if ($("#StartDate").val().toString().trim() == "") {
        $("#AlertMessage").text("Wrong Task Start Date");
        $("#AlertMessage").show();
    } else if ($("#DueDate").val() == "") {
        $("#AlertMessage").text("Wrong Task Due Date");
        $("#AlertMessage").show();
    } else if ($("#CompleteDate").val() == "") {
        $("#AlertMessage").text("Wrong Task Complete Date");
        $("#AlertMessage").show();
    } else {
        $("#AlertMessage").hide();
        return true;
    }
    return false;
}














/*function FillDataTable() {
    $('#datatable').dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Task/GetPaggedDataFilters",
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                d.ClientNumber = ClientNumberList;
                d.ClientName = ClientNameList;
                d.ProjectNumber = ProjectNumberList;
                d.ProjectName = ProjectNameList;
                d.RequirementStatus = RequirementStatusList;

                d.PageNumber = 0;
                d.PageSize = 10;
                d.SearchText = '';
                d.OrderASText = 'Sn';
                d.OrderDSText = 'asc';
                return d;
            },
            "dataSrc": function (json) {

                if (json.supervisionddl != null) {
                    // Client Number
                    $("#ClientNumber").empty();
                    $.each(json.supervisionddl.ddlCNumber, function (index, row) {
                        $("#ClientNumber").append("<option value='" + row + "'>" + row + "</option>");
                    });

                    // Client Name
                    $("#ClientName").empty();
                    $.each(json.supervisionddl.ddlCName, function (index, row) {
                        $("#ClientName").append("<option value='" + row + "'>" + row + "</option>");
                    });

                    // Project Number
                    $("#ProjectNumber").empty();
                    $.each(json.supervisionddl.ddlPNumber, function (index, row) {
                        $("#ProjectNumber").append("<option value='" + row + "'>" + row + "</option>");
                    });

                    // Project Name
                    $("#ProjectName").empty();
                    $.each(json.supervisionddl.ddlPName, function (index, row) {
                        $("#ProjectName").append("<option value='" + row + "'>" + row + "</option>");
                    });

                    // Requirement Status
                    $("#RequirementStatus").empty();
                    $.each(json.supervisionddl.ddlRequirementStatus, function (index, row) {
                        $("#RequirementStatus").append("<option value='" + row + "'>" + row + "</option>");
                    });
                }
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
            },
            {
                "targets": [8],
                "visible": true,
                "searchable": false
            },
            {
                "targets": [9],
                "visible": true,
                "searchable": false
            },
            {
                "targets": [10],
                "visible": true,
                "searchable": false
            },
            {
                "targets": [11],
                "visible": true,
                "searchable": false
            },
            {
                "targets": [12],
                "visible": true,
                "searchable": false
            },
            {
                "targets": [13],
                "visible": true,
                "searchable": false
            },
        ],
        "columns": [
            {
                "data": null,
                "name": "Sn",
                "render": function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                },
                "title": "Sn",
                "width": "3%"
            },
            {
                "data": "clientId",
                "name": "Client Number",
                "autowidth": "3%"
            },
            {
                "data": "clientName",
                "name": "Client Name",
                "autowidth": "5%"
            },
            {
                "data": "projectId",
                "name": "PricingPolicy",
                "autowidth": "5%"
            },
            {
                "data": "projectName",
                "name": "PreparedByName",
                "autowidth": "6%"
            },
            {
                "data": "scope",
                "name": "Scope",
                "autowidth": "4%"
            },
            {
                "data": "usage",
                "name": "Usage",
                "autowidth": "5%"
            },
            {
                "data": "activity",
                "name": "Activity",
                "autowidth": "5%"
            },
            {
                "data": "zoneName",
                "name": "ZoneName",
                "autowidth": "5%"
            },
            {
                "data": "mainStatus",
                "name": "Main Status",
                "autowidth": "5%"
            },
            {
                "data": "mainStatusDate",
                "name": "Main Status Date",
                "autowidth": "4%"
            },
            {
                "data": "subStatus",
                "name": "Sub Status",
                "autowidth": "5%"
            },
            {
                "data": "subStatusDate",
                "name": "Sub Status Date",
                "autowidth": "4%"
            },
            {
                "render": function (data, type, row) {
                    var paymentButton = '';
                    var approvedProjectButton = '';
                    if (typeValue == 7) {
                        var certificateButton = '';
                        if (row.isSupervision) {
                            certificateButton = `<li>
                                 <a href="#showCertificates" data-bs-toggle="modal" class="dropdown-item text-primary" onclick="setCertificate(${row.sn})">
                                     <i class="ri-article-line fs-16" title="Create Certificates"></i> Create Certificates
                                 </a>
                             </li>`;
                        } else {
                            certificateButton = `<li>
                                 <a href="javascript:void(0);" class="dropdown-item text-muted disabled">
                                     <i class="ri-article-line fs-16"></i> Create Certificates
                                 </a>
                             </li>`;
                        }

                        approvedProjectButton =
                            `<td data-column-id="action" class="gridjs-td">
                             <span>
                                <div class="dropdown">
                                <button class="btn btn-soft-secondary btn-sm dropdown" type="button" data-bs-toggle="dropdown" aria-expanded="false"><i class="ri-more-fill"></i></button>
                                <ul class="dropdown-menu dropdown-menu-end">
                            
                                    <li>
                                        <a href="#showProjectStatus" data-bs-toggle="modal" class="dropdown-item text-primary" onclick="setProjectStatusLogsId(${row.sn})">
                                            <i class="bx bx-calendar fs-16" title="Project Status"></i> Project Status
                                        </a>
                                    </li>
                                    <li>
                                        <a href="#showModalReminder" data-bs-toggle="modal" class="dropdown-item text-primary" onclick="setUpdatetemId(${row.sn})">
                                            <i class="ri-alarm-line fs-16" title="Project Reminder"></i> Project Reminder
                                        </a>
                                    </li>
                                    ${certificateButton}
                                    <li>
                                        <a href="/Admin/ProjectRequirements/Edit?Id=${row.sn}" class="dropdown-item text-primary" title="Show Project Requirements">
                                            <i class="ri-stack-line fs-16"></i> Project Requirements
                                        </a>
                                    </li>
                            
                                    <li>
                                        <a href="/Admin/ProjectSubmital/Index?Id=${row.sn}" class="dropdown-item text-primary" title="Show Project Requirements">
                                            <i class="ri-stack-line fs-16"></i> Project Submital
                                        </a>
                                    </li>
                                    <li>
                                        <a href="/Admin/ProjectMonthlyVisits/Index?Id=${row.sn}" class="dropdown-item text-primary" title="Show Project Requirements">
                                            <i class="ri-stack-line fs-16"></i> Project Visits
                                        </a>
                                    </li>
                                     <li>
                                        <a href="/Admin/PermitsDepartment/Index?Id=${row.sn}" class="dropdown-item text-primary" title="Show Project Requirements">
                                            <i class="ri-eye-line fs-16"></i> Disciplines 
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </span>
                    </td>`;
                    }

                    return `<ul class="list-inline hstack gap-2 mb-0">
                            ${paymentButton}
                            ${approvedProjectButton}
                        </ul>`;
                },
                "orderable": false
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
}*/