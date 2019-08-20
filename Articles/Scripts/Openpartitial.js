
function openPartial(view) {
    $("#linkResult").load(view);
}


function updateMeny() {
    $('#menyPartial').load('/Home/MenyPartial');
}


$('#menyPartial').load('/Home/MenyPartial');
$('.dropdown-toggle').dropdown();


    
