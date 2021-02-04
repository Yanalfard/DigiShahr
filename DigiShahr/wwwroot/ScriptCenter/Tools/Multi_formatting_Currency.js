for (var i = 0; i < $(".price").length; i++) {
    $(".price")[i].innerHTML = $(".price")[i].innerHTML.replace(/\B(?=(\d{3})+(?!\d))/g, ',');
}