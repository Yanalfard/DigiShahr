$("#Create").click(function () {
    $.get("/Admin/Naighborhood/pCreate", function (result) {
        $(".modal-content").html(result);
    });
});

function edit(Id) {
    $.get("/Admin/Naighborhood/pEdit?id=" + Id, function (result) {
        $(".modal-content").html(result);
    });
}

$("#pageNumber").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Naighborhood/pList?PageId=" + PageId + "&InPageCount=" + InPageCount, function (result) {
        $("#Content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
        $("#Content").html(result);
    });
});

$("#InPageCount").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Naighborhood/pList?PageId=" + PageId + "&InPageCount=" + InPageCount, function (result) {
        $("#Content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
        $("#Content").html(result);
    });
});