function searchProducts() {
    var searchValue = $("#searchInput").val().toLowerCase();

    $("tbody tr").hide();

    $("tbody tr").filter(function () {
        return $(this).text().toLowerCase().indexOf(searchValue) > -1;
    }).show();
}