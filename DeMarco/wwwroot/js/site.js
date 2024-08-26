$('#galleryModal').on('show.bs.modal', function (e) {
    $('#galleryImage').attr("src", $(e.relatedTarget).data("large-src"));
});

//Dropdowns
var dropdownElementList = [].slice.call(document.querySelectorAll('.dropdown-toggle'))
var dropdownList = dropdownElementList.map(function (dropdownToggleEl) {
    return new bootstrap.Dropdown(dropdownToggleEl)
})