// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const xhr = new XMLHttpRequest();


const checkAdoptionSpeed = () => {
    console.log('adoption speed clicked');
    let age = document.getElementById('age').value,
        vaccinated = document.getElementById('vaccinated').value,
        sterilized = document.getElementById('sterilized').value;

    console.log(age, vaccinated, sterilized);

    const payload = {
        age: age,
        notVaccinated: vaccinated == 1 ? 1 : 0,
        notSterilized: sterilized == 1 ? 1 : 0
    }
    
    xhr.open("POST", 'http://127.0.0.1:5000/api/adoption-speed', true);
    xhr.onreadystatechange = () => {
        if (xhr.readyState === XMLHttpRequest.DONE && xhr.status === 200) {
            document.getElementById('adoption-speed').innerHTML = xhr.responseText;
        }
    };
    xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    xhr.send(JSON.stringify(payload));
}