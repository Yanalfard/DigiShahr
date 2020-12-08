$("#Create").click(function () {
    $(".modal-content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
    $.get("/Admin/Package/pCreate", function (result) {
        $(".modal-dialog").removeClass("modal-xl");
        $(".modal-dialog").removeClass("modal-lg");
        $(".modal-dialog").addClass("modal-md");
        $(".modal-content").html(result);
    });
});

function Info(Id) {
    $(".modal-content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
    $.get("/Admin/Package/pInfo?Id" + Id, function (result) {
        $(".modal-dialog").removeClass("modal-xl");
        $(".modal-dialog").removeClass("modal-lg");
        $(".modal-dialog").addClass("modal-md");
        $(".modal-content").html(result);
    });
}