﻿"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/KitchenReport").build();

connection.start().then(function () {
    console.log("Connected");
    
}).catch(function (err) {
    console.error(err.toString());

});

connection.on("KitchenUpdate", function () {
    window.location.reload();
});