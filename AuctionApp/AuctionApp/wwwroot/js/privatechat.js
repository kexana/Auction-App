﻿"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

const toastNotification = document.getElementById('liveToast');
var toastSender = document.getElementById('senderName');
var toastContent = document.getElementById('messageContent');
var toastLink = document.getElementById('actionLink');

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;
document.getElementById("sendFileButton").disabled = true;

connection.on("PrivateMessage", function (user, message) {
    if (user == document.getElementById("userInput").innerHTML) {
        const html = `<li class="d-flex justify-content-between mb-4">
            <div class="card w-100">
              <div class="card-header d-flex justify-content-between p-3">
                <p class="fw-bold mb-0">${user}</p>
              </div>
              <div class="card-body">
                <p class="mb-0">
                  ${message}
                </p>
              </div>
            </div>
            <img src="https://cdn-icons-png.flaticon.com/512/3135/3135789.png" alt="avatar"
              class="rounded-circle d-flex align-self-start ms-3 shadow-1-strong" width="60">
          </li>`;
        var li = document.createElement("li");
        li.innerHTML = html;
        document.getElementById("messageList").appendChild(li);
    }
    else {
        toastSender.innerHTML = user;
        toastContent.innerHTML = message;
        toastLink.textContent = "Go to conversation";
        toastLink.href = `/Chat/Message?user=${user}`;
        toastLink.removeAttribute("download");
        var toast = new bootstrap.Toast(toastNotification);
        toast.show();
    }
});



connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    document.getElementById("sendFileButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var name = document.getElementById("userName").innerHTML;
    var user = document.getElementById("userInput").innerHTML;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendPrivateMessage", name, user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
    const html = `<li class="d-flex justify-content-between mb-4">
        <img src="https://cdn-icons-png.flaticon.com/512/3135/3135715.png" alt="avatar"
            class="rounded-circle d-flex align-self-start me-3 shadow-1-strong" width="60">
            <div class="card w-100">
                <div class="card-header d-flex justify-content-between p-3">
                    <p class="fw-bold mb-0">${name}</p>
                </div>
                <div class="card-body">
                    <p class="mb-0">
                        ${message}
                    </p>
                </div>
            </div>
    </li>`;
    var li = document.createElement("li");
    li.innerHTML = html;
    document.getElementById("messageList").appendChild(li);
});

