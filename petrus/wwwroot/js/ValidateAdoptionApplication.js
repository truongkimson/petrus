window.onload = function () {

}


function ValidateForm() {
    if ((ValidateResidenceType() == false) || (ValidateDogOwned() == false) || (ValidateAcceptConditions() == false) ) {
        return false;
    }
}


function ValidateResidenceType() {
    var x = document.forms["myForm"]["residenceType"].value;
    if (x == "") {
        alert("Residence must be declared");
        return false;
    }
    else {
        return true;
    }
}
function ValidateDogOwned() {
    var y = document.forms["myForm"]["dogsOwned"].value;
    if (y == "") {
        alert("Number of pets owned must be declared");
        return false;
    }
    else {
        return true;
    }
}
function ValidateAcceptConditions() {
    if (document.getElementById("acceptConditions").checked == false) {
        alert("Please declare that you accept the conditions stated");
        return false;
    } else {
        return true;
    }
}

