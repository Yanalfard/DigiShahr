$('#fromDate').MdPersianDateTimePicker({
    targetTextSelector: '#fromDate',
    targetDateSelector: '#fromDate',
});

$('#toDate').MdPersianDateTimePicker({
    targetTextSelector: '#toDate',
    targetDateSelector: '#toDate',
});

$("#search").click(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Order/pList?PageId=" + PageId + "&InPageCount=" + InPageCount + "&orderId=" + $("#orderId").val() + "&phoneNumber=" + $("#phoneNumber").val(), function (result) {
        $("#Content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
        $("#Content").html(result);
    });
});

$("#pageNumber").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Order/pList?PageId=" + PageId + "&InPageCount=" + InPageCount + "&orderId=" + $("#orderId").val() + "&phoneNumber=" + $("#phoneNumber").val(), function (result) {
        $("#Content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
        $("#Content").html(result);
    });
});

$("#InPageCount").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Order/pList?PageId=" + PageId + "&InPageCount=" + InPageCount + "&orderId=" + $("#orderId").val() + "&phoneNumber=" + $("#phoneNumber").val(), function (result) {
        $("#Content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
        $("#Content").html(result);
    });
});

function Info(Id) {
    $(".modal-content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
    $.get("/Admin/Order/pInfo?Id=" + Id, function (result) {
        $(".modal-dialog").removeClass("modal-xl");
        $(".modal-dialog").removeClass("modal-md");
        $(".modal-dialog").addClass("modal-lg");
        $(".modal-content").html(result);
    });
}