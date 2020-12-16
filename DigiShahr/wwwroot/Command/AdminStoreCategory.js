$("#Create").click(function () {
    $.get("/Admin/StoreCategory/pCreate", function (result) {
        $(".modal-content").html(result);
    });
});

function Child(Id) {
    $.get("/Admin/StoreCategory/pChild?Id=" + Id, function (result) {
        $(".modal-content").html(result);
    });
}

function edit(Id) {
    $.get("/Admin/StoreCategory/pEdit?Id=" + Id, function (result) {
        $(".modal-content").html(result);
    });
}

function Remove(Id) {
    $.get("/Admin/StoreCategory/pRemove?Id=" + Id, function (result) {
        $(".modal-content").html(result);
    });
}

$("#pageNumber").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/StoreCategory/Index?PageId=" + PageId + "&InPageCount=" + InPageCount, function () {
        window.location.href = "/Admin/Category?PageId=" + PageId + "&InPageCount=" + InPageCount;
    });
});

$("#InPageCount").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/StoreCategory/Index?PageId=" + PageId + "&InPageCount=" + InPageCount, function () {
        window.location.href = "/Admin/Category?PageId=" + PageId + "&InPageCount=" + InPageCount;
    });
});