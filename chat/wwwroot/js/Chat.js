"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var encodedMsg = user + ": " + message;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("ReceiveMessageToChat", function (user, message, when, chatId) {
    user += ": ";

    document.getElementById("nameToChat " + chatId).innerText = user;
    document.getElementById("messageToChat " + chatId).innerText = message;
    document.getElementById("whenToChat " + chatId).innerText = when;
});

connection.start().then(function () {
    var chatId = document.getElementById("chatId").value;
    connection.invoke("AddToGroupAsync", chatId);

    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var chatId = document.getElementById("chatId").value;
    var userId = document.getElementById("userId").value;

    connection.invoke("SendMessage", user, message, chatId, userId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});