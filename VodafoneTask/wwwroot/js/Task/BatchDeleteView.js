$(document).ready(function () {

});


function onClickRestore() {
    $.ajax({
        url: "/Task/RestoreTask",
        method: "POST",
        contentType: "application/json",
        data: '',
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
}

function onClickBatchDelete() {
    var cc = $("#StartDate").val();
    $.ajax({
        url: "/Task/BatchDelete",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            DateFrom: $("#StartDate").val(),
            DateTo: $("#DueDate").val(),
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
}