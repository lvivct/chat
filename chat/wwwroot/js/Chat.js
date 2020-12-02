"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;
function getDate() {
    var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
        "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var days = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];

    var today = new Date();
    var mm = months[today.getMonth()];
    var day = days[today.getDay()];

    return day + " " + mm + " " + today.getHours() + ":" + today.getMinutes();
};
connection.on("ReceiveMessage", function (user, message) {
    var encodedMsg = user + ": " + message;
    var li = document.createElement("li");
   
    var messgeCard = document.createElement("div");
    messgeCard.setAttribute("class", "row cardflat pt-2 pb-2 pr-3 pl-3");
    messgeCard.innerHTML = message;
    li.appendChild(messgeCard);

   

    var underMessage = document.createElement("div");
    underMessage.setAttribute("class", "row ml-auto d-flex");

    var date = document.createElement("small");
    date.setAttribute("class", "ml-auto pr-1 ordinary-text-color");
    date.innerHTML = getDate();
    underMessage.appendChild(date);

    var name = document.createElement("small");
    name.setAttribute("class", "pr-1 ordinary-text-color");
    name.innerHTML = user;
    underMessage.appendChild(name);

     
    var img = document.createElement("img");
    img.setAttribute("class", "p-1 small-profile rounded-circle z-depth-0");
    img.src = "/images/" + document.getElementById("photoPath").value;
    underMessage.appendChild(img);

    li.appendChild(underMessage);
    document.getElementById("messagesList").appendChild(li);

    document.getElementById("messageInput").value = "";
    var messageList = document.getElementById("messages");
    messageList.scrollTop = messageList.scrollHeight;
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
    var photoPath = document.getElementById("photoPath").value;

    connection.invoke("SendMessage", user, message, chatId, userId, photoPath).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});