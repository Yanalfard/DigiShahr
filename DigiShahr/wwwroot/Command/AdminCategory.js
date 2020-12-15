$("#Create").click(function () {
    $.get("/Admin/Category/pCreate", function (result) {
        $(".modal-content").html(result);
    });
});

function edit(Id) {
    $.get("/Admin/Category/pEdit?Id=" + Id, function (result) {
        $(".modal-content").html(result);
    });
}

function Remove(Id) {
    $.get("/Admin/Category/pRemove?Id=" + Id, function (result) {
        $(".modal-content").html(result);
    });
}

$("#pageNumber").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Category/Index?PageId=" + PageId + "&InPageCount=" + InPageCount, function () {
        window.location.href = "/Admin/Category?PageId=" + PageId + "&InPageCount=" + InPageCount;
    });
});

$("#InPageCount").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Category/Index?PageId=" + PageId + "&InPageCount=" + InPageCount, function () {
        window.location.href = "/Admin/Category?PageId=" + PageId + "&InPageCount=" + InPageCount;
    });
});