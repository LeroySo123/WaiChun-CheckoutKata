// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var basket = [];
function AddtoBasket(itemname) {
    let btnID = "#btn_" + itemname;
    let ddlID = "#" + itemname + "Quantity";
    let itemID = $(btnID).val();
    let itemQuantity = $(ddlID).val();
    if (itemID != "" && itemQuantity != "") {
        let addItem = { "ItemID": parseInt(itemID), "ItemCount": parseInt(itemQuantity) }
        basket.push(addItem);
        $("#BasketItemDiv").append('<div class="row">' +
            '<div class="col-sm">' + itemname + '</div>' +
            '<div class="col-sm">' + itemQuantity + '</div>' +
            '</div>')
        $(ddlID).val("");
    }
}

function ClickCheckOut() {
    if (basket.length > 0) {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "/home/check",
            data: JSON.stringify(basket),
            dataType: 'json',
            success: function (data) {
                alert(data);
            }
        });
    }
}