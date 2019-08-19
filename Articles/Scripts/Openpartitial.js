
function openPartial(view) {
    $("#linkResult").load(view);
}

$('.tree-toggle').click(function () {
    $(this).parent().children('ul.tree').toggle(200);
});

function updateMeny() {
    $('#menyPartial').load('/Home/MenyPartial');
}