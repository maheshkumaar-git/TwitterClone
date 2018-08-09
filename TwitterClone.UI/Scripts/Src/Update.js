$(document).ready(function () {
    $("#btnDeactivate").click(function (e) {

        $.ajax({
            type: "POST",
            url: "/User/Deactivate",
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            success: function (result) {
                location.href = '@Url.Action("Login", "User")'
        },
            error: function (error) {
                alert("Error");
            }
    });
    return false;
});


});