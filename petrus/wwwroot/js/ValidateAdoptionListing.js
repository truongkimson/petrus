window.onload = function () {

}



function ValidateSelected() {
    var x = document.forms["myForm"]["selected"].value;
    if (x == "") {
        alert("Please select a listing");
        return false;
    }
    else {
        return true;
    }
}
