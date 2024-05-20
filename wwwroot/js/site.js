// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function check() {

    var mobile = document.getElementById('mobile');


    var message = document.getElementById('message');

 

    if (mobile.value.length == 10) {

    return true;

    }
    else {
        message.innerHTML = "required 10 digits, match requested format!"
    }

}