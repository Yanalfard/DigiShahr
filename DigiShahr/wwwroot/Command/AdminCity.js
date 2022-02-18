$("#Create").click(function () {
    $.get("/Admin/City/pCreate", function (result) {
        $(".modal-content").html(result);
    });
});

function edit(Id) {
    $.get("/Admin/City/pEdit?Id=" + Id, function (result) {
        $(".modal-content").html(result);
    });
}

$("#pageNumber").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/City/Index?PageId=" + PageId + "&InPageCount=" + InPageCount, function () {
        window.location.href = "/Admin/City?PageId=" + PageId + "&InPageCount=" + InPageCount;
    });
});

$("#InPageCount").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/City/Index?PageId=" + PageId + "&InPageCount=" + InPageCount, function () {
        window.location.href = "/Admin/City?PageId=" + PageId + "&InPageCount=" + InPageCount;
    });
});