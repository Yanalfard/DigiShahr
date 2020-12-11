$("#pageNumber").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Store/pStoreList?PageId=" + PageId + "&InPageCount=" + InPageCount + "&storeName=" + $("#storeName").val() + "&phoneNumber=" + $("#phoneNumber").val(), function (result) {
        $("#Content").html(result);
    });
});

$("#InPageCount").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Store/pStoreList?PageId=" + PageId + "&InPageCount=" + InPageCount + "&storeName=" + $("#storeName").val() + "&phoneNumber=" + $("#phoneNumber").val(), function (result) {
        $("#Content").html(result);
    });
});

$("#Search").click(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Store/pStoreList?PageId=" + PageId + "&InPageCount=" + InPageCount + "&storeName=" + $("#storeName").val() + "&phoneNumber=" + $("#phoneNumber").val(), function (result) {
        $("#Content").html(result);
    });
});

function Info(id) {
    $(".modal-content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
    $.get("/Admin/Store/pInfo?Id=" + id, function (result) {
        $(".modal-dialog").removeClass("modal-xl");
        $(".modal-dialog").removeClass("modal-md");
        $(".modal-dialog").addClass("modal-lg");
        $(".modal-content").html(result);
    });
}

function Accept(Id) {
    $.post("/Admin/Store/Accept?Id=" + Id, function (result) {

        if (result == "true") {
            $.get("/Admin/Store/pStoreList?PageId=" + PageId + "&InPageCount=" + InPageCount + "&storeName=" + $("#storeName").val() + "&phoneNumber=" + $("#phoneNumber").val(), function (result) {
                $("#Content").html(result);
            });
        }

    });
}

function Reject(Id) {
    $.post("/Admin/Store/Reject?Id=" + Id, function (result) {

        if (result == "true") {
            $.get("/Admin/Store/pStoreList?PageId=" + PageId + "&InPageCount=" + InPageCount + "&storeName=" + $("#storeName").val() + "&phoneNumber=" + $("#phoneNumber").val(), function (result) {
                $("#Content").html(result);
            });
        }

    });
}