$(document).ready(function () {

    $("#BtnAddTask").click(function () {
        if (!validate())
            return;
        $.ajax({
            url: "/Task/CreateTask",
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify({
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
                $('html, body').animate({ scrollTop: 0 }, 'slow');
            },
            error: function (xhr, status, error) {
                $("#AlertMessage").text("Failure");
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












