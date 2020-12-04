"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message, messageDate) {
    var li = document.createElement("li");
    var messgeCard = document.createElement("div");
    var underMessage = document.createElement("div");
    var date = document.createElement("small");
    var name = document.createElement("small");
    var img = document.createElement("img");

    messgeCard.innerHTML = message;
    date.innerHTML = messageDate;
    name.innerHTML = user;
    img.src = "/images/" + document.getElementById("photoPath").value;

    underMessage.appendChild(img);
    if (document.getElementById("userInput").value === user) {
        li.setAttribute("class","w-50 ml-auto m-3 ordinary-text-color");
        messgeCard.setAttribute("class", "row cardflat pt-2 pb-2 pr-3 pl-3");     
        underMessage.setAttribute("class", "row ml-auto d-flex");
        date.setAttribute("class", "ml-auto pr-1 ordinary-text-color");    
        name.setAttribute("class", "pr-1 ordinary-text-color");   
        img.setAttribute("class", "p-1 small-profile rounded-circle z-depth-0");

        
        underMessage.appendChild(date);
        underMessage.appendChild(name);
        underMessage.appendChild(img);
    } else {
        li.setAttribute("class", "w-50 mr-auto m-4 ordinary-text-color");
        messgeCard.setAttribute("class", "row cardflat pt-2 pb-2 pr-3 pl-3");
        underMessage.setAttribute("class", "row mr-auto d-flex");
        date.setAttribute("class", "mr-auto pr-1 ordinary-text-color");
        name.setAttribute("class", "pr-1 ordinary-text-color");
        img.setAttribute("class", "p-1 small-profile rounded-circle z-depth-0");

        underMessage.appendChild(img);
        underMessage.appendChild(name);
        underMessage.appendChild(date);
    }
    
    li.appendChild(messgeCard);
    var name = document.createElement("small");
    name.setAttribute("class", "pr-1 ordinary-text-color");
    name.innerHTML = user;
    underMessage.appendChild(name);

     
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
    var userId = document.getElementById("userId").value;

    connection.invoke("AddToGroupAsync", userId, chatId);

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