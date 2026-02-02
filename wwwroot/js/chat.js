"use strict";

var connection = new signalR.HubConnectionBuilder()
                            .withUrl("/chatHub")
                            .build();

document.getElementById("messages").style.display = 'none';

connection.start().catch(function (err) {
    return console.error("1" + err.toString());
});

document.getElementById("joinButton").addEventListener("click", function (event) {
    var chatName = document.getElementById("inputChat").value;
    connection.invoke("JoinChat", chatName)
        .catch(err => console.error("Ошибка вызова:", err.toString()));
    document.getElementById("join").style.display = 'none';
    document.getElementById("messages").style.display = 'block';
    event.preventDefault();
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("inputMessage").value;
    var user = document.getElementById("inputUser").value;
    var chatName = document.getElementById("inputChat").value;

    connection.invoke("SendMessage", chatName, user, message)
        .catch(err => console.error("Ошибка вызова:", err.toString()));
    event.preventDefault();
});

connection.on("ReceiveMessage", function (message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = message;
});