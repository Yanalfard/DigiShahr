$("#pageNumber").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Service/pServiceList?PageId=" + PageId + "&InPageCount=" + InPageCount + "&ServiceName=" + $("#ServiceName").val() + "&phoneNumber=" + $("#phoneNumber").val(), function (result) {
        $("#Content").html(result);
    });
});

$("#InPageCount").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Service/pServiceList?PageId=" + PageId + "&InPageCount=" + InPageCount + "&ServiceName=" + $("#ServiceName").val() + "&phoneNumber=" + $("#phoneNumber").val(), function (result) {
        $("#Content").html(result);
    });
});

$("#Search").click(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.get("/Admin/Service/pServiceList?PageId=" + PageId + "&InPageCount=" + InPageCount + "&ServiceName=" + $("#ServiceName").val() + "&phoneNumber=" + $("#phoneNumber").val(), function (result) {
        $("#Content").html(result);
    });
});

function Info(id) {
    $(".modal-content").html("<img src='/img/adminStatic/load2.gif' class='justify-content-center' width='60' height='60' />");
    $.get("/Admin/Service/pInfo?Id=" + id, function (result) {
        $(".modal-dialog").removeClass("modal-xl");
        $(".modal-dialog").removeClass("modal-md");
        $(".modal-dialog").addClass("modal-lg");
        $(".modal-content").html(result);
    });
}

function Accept(Id) {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    $.post("/Admin/Service/Accept?Id=" + Id, function (result) {
        if (result == "true") {
            window.location.reload();
        }

    });
}

function Reject(Id) {
    $.post("/Admin/Service/Reject?Id=" + Id, function (result) {

        if (result == "true") {
            $.get("/Admin/Service/pServiceList?PageId=" + PageId + "&InPageCount=" + InPageCount + "&ServiceName=" + $("#ServiceName").val() + "&phoneNumber=" + $("#phoneNumber").val(), function (result) {
                $("#Content").html(result);
            });
        }

    });
}