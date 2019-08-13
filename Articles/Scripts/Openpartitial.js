var toggler = document.getElementsByClassName("link");
var i;

for (i = 0; i < toggler.length; i++) {
    toggler[i].addEventListener("click", function () {
        alert(this)
        var Id = toggler[i].Id;

        $("#linkResult").load('/Home/Clauses?clausesId=' + Id);
    });
}