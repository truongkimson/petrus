window.onload = function () {

}

function routeByCheckedBox(clickedId) {

    var getSelectedValue = document.querySelector('input[name="selected"]:checked');
    if (getSelectedValue != null) {
        var oldhref = document.getElementById(clickedId).getAttribute("href");
        var newhref = oldhref + '/' + getSelectedValue.id;
        document.getElementById(clickedId).setAttribute('href', newhref);
    }
}