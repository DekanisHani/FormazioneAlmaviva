// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function caricamentoListPersone() {
    $.get({
        url: "/Home/_ListPersone",
        cache: false
    }).done(function (data) {
        $("#ListPersone").html(data);
    }).fail(function () {
        console.log("ERRORE");
    });
}

function caricamentoFormPersone() {
    $.get({
        url: "/Home/_FormPersone",
        cache: false
    }).done(function (data) {
        $("#FormPersone").html(data);
    }).fail(function () {
        console.log("ERRORE");
    });
}
