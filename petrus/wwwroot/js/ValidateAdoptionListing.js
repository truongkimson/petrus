window.onload = function () {

}



function ValidateSelected() {
    var x = document.forms["requestForm"]["requestSelected"].value;

    if (x == null|| x=="") {
        alert("Please select a listing");
        return false;
    }
    else {
        alert(x);
        return true;
    }
}
