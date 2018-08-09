$(document).ready(function () {


    $(".aMessage").click(function (e) {
        $("#comment").val(this.innerHTML);
        $("#btnTweet").hide();
        $("#btnDelete").show();
        $("#btnUpdate").show();
        curTweetId = $(this).prop('id');
    });

    $("#btnTweet").click(function (e) {
        if ($("#comment").val() == "")
            alert("Please enter comment");
        else {

            var cmt = $("#comment").val();
            $.ajax({
                type: "POST",
                url: "/Home/ManageTweet",
                contentType: "application/json; charset=utf-8",
                data: '{"tweetId":"' + 0 + '", "message":"' + cmt + '"}',
                dataType: "html",
                success: function (result) {
                    $("#comment").text('');
                    location.reload();
                },
                error: function (error) {
                    alert("Error");
                }
            });
        }
        return false;
    });


    $("#btnUpdate").click(function (e) {
        if ($("#comment").val() == "")
            alert("Please enter your message");
        else {
            var cmt = $("#comment").val();
            $.ajax({
                type: "POST",
                url: "/Home/ManageTweet",
                contentType: "application/json; charset=utf-8",
                data: '{"tweetId":"' + curTweetId + '", "message":"' + cmt + '"}',
                dataType: "html",
                success: function (result) {
                    $("#comment").text('');

                    $("#btnDelete").hide();
                    $("#btnUpdate").hide();
                    $("#btnTweet").show();
                    location.reload();
                },
                error: function (error) {
                    alert("Error");
                }
            });
        }
        return false;
    });

    $("#btnDelete").click(function (e) {

        $.ajax({
            type: "POST",
            url: "/Home/DeleteTweet",
            contentType: "application/json; charset=utf-8",
            data: '{"tweetId":"' + curTweetId + '"}',
            dataType: "html",
            success: function (result) {
                $("#comment").text('');

                $("#btnDelete").hide();
                $("#btnUpdate").hide();
                $("#btnTweet").show();
                location.reload();
            },
            error: function (error) {
                alert("Error");
            }
        });

        return false;
    });
});