// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const xhr = new XMLHttpRequest();


const checkAdoptionSpeed = () => {
    console.log('adoption speed clicked');
    let type = parseInt($('#type')[0].value),
        age = parseInt($('#age')[0].value),
        gender = parseInt($('#gender')[0].value),
        maturitySize = parseInt($('#maturity-size')[0].value),
        furLength = parseInt($('#fur-length')[0].value),
        qty = parseInt($('#qty')[0].value),
        fee = parseInt($('#fee')[0].value),
        desc = parseInt($('#desc')[0].value.length),
        color1 = parseInt($('#desc')[0].value),
        vaccinated = parseInt($('#vaccinated')[0].value),
        dewormed = parseInt($('#dewormed')[0].value),
        sterilized = parseInt($('#sterilized')[0].value),
        health = parseInt($('#health')[0].value);

    const payload = {
        type,
        age,
        gender,
        maturitySize,
        furLength,
        qty,
        fee,
        desc,
        color1,
        vaccinated,
        dewormed,
        sterilized,
        health
    }

    console.log(payload);

    xhr.open("POST", 'http://127.0.0.1:5000/api/adoption-speed', true);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === XMLHttpRequest.DONE && xhr.status === 200) {
            document.getElementById('adoption-speed').innerHTML = xhr.responseText;
        }
    };
    xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    xhr.send(JSON.stringify(payload));
}