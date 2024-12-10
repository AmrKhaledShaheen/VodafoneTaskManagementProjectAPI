$(document).ready(function () {
    $("#Email").change(function () {
        if (!validateEmail($("#Email").val())) {
            $("#EmailError").show();
        } else {
            $("#EmailError").hide();
        }
    });
    $("#Password").change(function () {
        if (!validatePassword($("#Password").val())) {
            $("#PasswordError").show();
        } else {
            $("#PasswordError").hide();
        }
    });
    $("#UserName").change(function () {
        if ($("#UserName").val().trim().length < 3) {
            $("#UserNameError").show();
        } else {
            $("#UserNameError").hide();
        }
    });
    $("#EmailSignUp").change(function () {
        if (!validateEmail($("#EmailSignUp").val())) {
            $("#EmailSignUpError").show();
        } else {
            $("#EmailSignUpError").hide();
        }
    });
    $("#PasswordSignUp").change(function () {
        if (!validatePassword($("#PasswordSignUp").val())) {

            $("#PasswordSignUpError").show();
        } else {
            $("#PasswordSignUpError").hide();
        }
    });
    $("#SignUp").click(function () {
        if ($("#PasswordSignUp").val().length < 1) {
            $("#PasswordSignUpError").show();
            return;
        } else {
            $("#PasswordSignUpError").hide();
        }
        if ($("#EmailSignUp").val().length < 1) {
            $("#EmailSignUpError").show();
            return;
        } else {
            $("#EmailSignUpError").hide();
        }
        if ($("#UserName").val().length < 1) {
            $("#UserNameError").show();
            return;
        } else {
            $("#UserNameError").hide();
        }
        SignUpApi();
    });
    $("#SignIn").click(function () {
        if ($("#Password").val().length < 1) {
            $("#PasswordError").show();
            return;
        } else {
            $("#PasswordError").hide();
        }
        if ($("#Email").val().length < 1) {
            $("#EmailError").show();
            return;
        } else {
            $("#EmailError").hide();
        }
        SignInApi();
    });
});

function SignUpApi() {
    $.ajax({
        url: "/AccountManagement/SignUp",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            UserName: $("#UserName").val(),
            Email: $("#EmailSignUp").val(),
            Password: $("#PasswordSignUp").val()
        }),
        success: function (Response) {
            if (Response.succeeded) {
                /*$("#Email").val(Respose.title);*/
                $("#AlertMessageSignUp").text(Response.message);
                $("#AlertMessageSignUp").show();
                /*document.querySelector('.cont').classList.toggle('s--signup');*/
            } else {
                $("#AlertMessageSignUp").text(Response.message);
                $("#AlertMessageSignUp").show();
            }
        },
        error: function (xhr, status, error) {
            $("#AlertMessageSignUp").text("Failure");
        }
    });
}
function SignInApi() {
    $.ajax({
        url: "/AccountManagement/Login",
        method: "POST",
        contentType: "application/json",
        data: JSON.stringify({
            Email: $("#Email").val(),
            Password: $("#Password").val()
        }),
        success: function (Response) {
            if (!Response.succeeded) {
                $("#AlertMessage").text(Response.message);
                $("#AlertMessage").show();
            } else {
                window.location.href = "/Task/Index";
            }
        },
        error: function (xhr, status, error) {
            $("#AlertMessage").text("Failure");
        }
    });
}
document.querySelector('.img__btn').addEventListener('click', function () {
    document.querySelector('.cont').classList.toggle('s--signup');
});

function validateEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}

function validatePassword(password) {
    var passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$/;
    return passwordRegex.test(password);
}
