$("#pageNumber").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Package/pList?PageId=" + PageId + "&InPageCount=" + InPageCount, function (result) {
        $("#Content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
        $("#Content").html(result);
    });
});

$("#InPageCount").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Package/pList?PageId=" + PageId + "&InPageCount=" + InPageCount, function (result) {
        $("#Content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
        $("#Content").html(result);
    });
});


$("#Create").click(function () {
    $(".modal-content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
    $.get("/Admin/Package/pCreate", function (result) {
        $(".modal-dialog").removeClass("modal-xl");
        $(".modal-dialog").removeClass("modal-md");
        $(".modal-dialog").addClass("modal-lg");
        $(".modal-content").html(result);
    });
});

function pDelete(id) {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $(".modal-content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
    $.get("/Admin/Package/pRemove?id=" + id + "&PageId=" + PageId + "&InPageCount=" + InPageCount, function (result) {
        $(".modal-dialog").removeClass("modal-xl");
        $(".modal-dialog").removeClass("modal-md");
        $(".modal-dialog").addClass("modal-lg");
        $(".modal-content").html(result);
    });
}


function Remove(id) {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.post("/Admin/Package/Remove?id=" + id + "&PageId=" + PageId + "&InPageCount=" + InPageCount, function (result) {
        $("#modalSave").css('display', 'none');
        $("#modalSave").removeClass('show');
        document.querySelectorAll('.modal-backdrop')[0].parentElement.removeChild(document.querySelectorAll('.modal-backdrop')[0]);
        $("#Content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
        $("#Content").html(result);
    });
}
