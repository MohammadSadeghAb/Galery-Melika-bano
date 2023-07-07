$(function () {

    $('form').submit(function (e) {

        if ($(this).valid()) {

            $('button').prop('disabled', true)
            $('input[type=button]').prop('disabled', true)

        }

    })

})

function Add() {
    let number = document.getElementById("myNumber");

    number.value++;
}

function Low() {
    let number = document.getElementById("myNumber");

    if (number.value > 0) {
        number.value--;
    }
}