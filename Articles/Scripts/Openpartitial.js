    
function openPartial(view) {
    $("#linkResult").load(view);
} 

$(function () {
    $('.tree-toggle').parent().children('ul.tree').toggle();
})



$('.tree-toggle').click(function () {
    $(this).parent().children('ul.tree').toggle(200);
});


function openModal(view) {
    jQuery.noConflict();
    $("#myModalBox").modal('show');
    $("#ModalView").load(view);
}

 