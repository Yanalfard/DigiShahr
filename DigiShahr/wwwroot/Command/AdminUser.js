﻿$("#Search").click(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/User/List?PageId=" + PageId + "&InPageCount=" + InPageCount + "&PhoneNumber=" + $("#PhoneNumberInput").val(), function (result) {
        $("#Content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
        $("#Content").html(result);
    });
});

$("#pageNumber").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/User/List?PageId=" + PageId + "&InPageCount=" + InPageCount + "&PhoneNumber=" + $("#PhoneNumberInput").val(), function (result) {
        $("#Content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
        $("#Content").html(result);
    });
});

$("#InPageCount").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/User/List?PageId=" + PageId + "&InPageCount=" + InPageCount + "&PhoneNumber=" + $("#PhoneNumberInput").val(), function (result) {
        $("#Content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
        $("#Content").html(result);
    });
});