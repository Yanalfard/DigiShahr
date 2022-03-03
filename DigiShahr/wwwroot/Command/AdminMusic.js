$("#file").change(function () {
    var files = $("#file").get(0).files;
    if (files[0].size > 20971520) {
        $("#Submit").removeClass('btn-success');
        $("#Submit").addClass('btn-danger');
        $("#Submit").html("حجم فایل بیش از اندازه میباشد");
        $("#Submit").attr("disabled", "disabled");
    } else {
        $("#Submit").addClass('btn-success');
        $("#Submit").removeClass('btn-danger');
        $("#Submit").html("افزودن");
        $("#Submit").removeAttr("disabled");
        if ($("#file").val().includes(".mp3") == false) {
            $("#Submit").removeClass('btn-success');
            $("#Submit").addClass('btn-danger');
            $("#Submit").html("فرمت فایل باید mp3 باشد");
            $("#Submit").attr("disabled", "disabled");
        } else {
            $("#Submit").addClass('btn-success');
            $("#Submit").removeClass('btn-danger');
            $("#Submit").html("افزودن");
            $("#Submit").removeAttr("disabled");
        }
    }
});

$("#fileEdit").change(function () {
    var files = $("#file").get(0).files;
    if (files[0].size > 20971520) {
        $("#Submit").removeClass('btn-success');
        $("#Submit").addClass('btn-danger');
        $("#Submit").html("حجم فایل بیش از اندازه میباشد");
        $("#Submit").attr("disabled", "disabled");
    } else {
        $("#Submit").addClass('btn-success');
        $("#Submit").removeClass('btn-danger');
        $("#Submit").html("ویرایش");
        $("#Submit").removeAttr("disabled");
        if ($("#file").val().includes(".mp3") == false) {
            $("#Submit").removeClass('btn-success');
            $("#Submit").addClass('btn-danger');
            $("#Submit").html("فرمت فایل باید mp3 باشد");
            $("#Submit").attr("disabled", "disabled");
        } else {
            $("#Submit").addClass('btn-success');
            $("#Submit").removeClass('btn-danger');
            $("#Submit").html("ویرایش");
            $("#Submit").removeAttr("disabled");
        }
    }
});


$("#Submit").click(function () {
    $(".loadingicon").removeClass('d-none');
});

$("#Create").click(function () {
    $.get("/Admin/Music/pCreate", function (result) {
        $(".modal-content").html(result);
    });
});
function Edit(id) {
    $.get("/Admin/Music/pEdit?id=" + id, function (result) {
        $(".modal-content").html(result);
    });
}
function Remove(id) {
     $.get("/Admin/Music/pRemove?id=" + id, function (result) {
        $(".modal-content").html(result);
    });
}

function BtnRemoveMusic(id) {
    $.post("/Admin/Music/Remove?id=" + id, function () {
        window.location.reload();
    });
}

$("#pageNumber").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    window.location.href = "/Admin/Music?PageId=" + PageId + "&InPageCount=" + InPageCount;
});

$("#InPageCount").change(function () {
    var PageId = $("#pageNumber option:selected").val();
    var InPageCount = $("#InPageCount option:selected").val();
    window.location.href = "/Admin/Music?PageId=" + PageId + "&InPageCount=" + InPageCount;
});