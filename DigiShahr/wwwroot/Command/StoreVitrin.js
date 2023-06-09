﻿$("#SubmitCatagory").click(function () {
    var categoryname = $("#CategoryName").val();
    if (categoryname != "") {

        $.post("/Store/CreateCategory?Name=" + categoryname, function (result) {
            if (result == "true") {
                window.location.reload();
            } else if (result == "SubscribtionTillErorr") {
                window.location.href = "/Store/SubscribtionTillErorr";
            }
            else {
                $("#CreateCategoryErorr").html(result);
            }
        })
    };
});

$("#MainBannerFile").change(function () {
    var fileUpload = $("#MainBannerFile").get(0);
    var files = fileUpload.files;
    var data = new FormData();
    data.append("file", files[0]);

    $.ajax({
        type: "POST",
        url: "/Store/UploadMainBanner",
        contentType: false,
        processData: false,
        data: data,
        async: false,
        beforeSend: function () {
            $("#divloader").show()
        },
        success: function (message) {
            if (message == "true") {
                window.location.reload();
            } else {
                $("#MainBannerFileErorr").html(message);
            }
        },

    });
});

$("#LogoFile").change(function () {
    var fileUpload = $("#LogoFile").get(0);
    var files = fileUpload.files;
    var data = new FormData();
    data.append("file", files[0]);

    $.ajax({
        type: "POST",
        url: "/Store/UploadLogo",
        contentType: false,
        processData: false,
        data: data,
        async: false,
        beforeSend: function () {
            $("#divloader").show()
        },
        success: function (message) {
            if (message == "true") {
                window.location.reload();
            } else {
                $("#LogoFileErorr").html(message);
            }

        },

    });
});
