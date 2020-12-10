$("#pageNumber").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Package/OrderList?PageId=" + PageId + "&InPageCount=" + InPageCount + "&Id=" + $("#orderId").val(), function (result) {
        $("#Content").html(result);
    });
});

$("#InPageCount").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Package/OrderList?PageId=" + PageId + "&InPageCount=" + InPageCount + "&Id=" + $("#orderId").val(), function (result) {
        $("#Content").html(result);
    });
});


$("#Search").click(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Package/OrderList?PageId=" + PageId + "&InPageCount=" + InPageCount + "&Id=" + $("#orderId").val(), function (result) {
        $("#Content").html(result);
    });
});

function Info(Id) {
    $.get("/Admin/Package/pOrderInfo?Id=" + Id, function (result) {
        $(".modal-content").html(result);
    });
}