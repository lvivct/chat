"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/NameChangeHub").build();
document.getElementById("changeButton").disabled = true;

connection.start().then(function () {
    document.getElementById("changeButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("changeButton").addEventListener("click", function (event) {
    var userId = document.getElementById("userId").value;
    var userName = document.getElementById("userName").value;
    var email = document.getElementById("email").value;

    connection.invoke("UserChange", userId, userName, email).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});