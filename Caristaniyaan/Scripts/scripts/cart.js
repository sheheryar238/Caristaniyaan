var cart = [];
document.getElementById("cart-btn").style.display = 'none';
window.onload = function () {
    if (localStorage.getItem("cartItems") === null) {
        //Code here if user cart is empty on the page load
        document.getElementById("total-1").innerText = "Rs " + 0;
        document.getElementById("total-2").innerText = "Rs " + 0;
    }
    else {
        cart = JSON.parse(localStorage.getItem("cartItems"));
        showItems();
        totalBill();
    }
}

function showItems() {
    for (var i = 0; i < cart.length; i++) {
        var obj = document.createElement("div");
        obj.setAttribute("id", i);
        obj.setAttribute("class", "row grid-table");
        obj.innerHTML = "<div class='" + "col-xs-2 grid-item p-t-0 overl border-right-1x h-100" + "'>" +
            "<div class='" + "p-10" + "'>" +
            "<img class='" + "w-full" + "' " + "src='" + cart[i].Pic + "' " + "alt='" + "image" + "'> " +
            "</div>" + "</div>" +
            "<div class='" + "col-xs-5 grid-item product-name" + "'>" +
            "<h3 class='" + "f-16 p-0" + "'>" + "<a href='" + cart[i].Link + "'>" + cart[i].Name + "</a></h3>" +
            "<p class='" + "m-t-5 f-normal color-9 f-14" + "'>Model: " + cart[i].Car + "</p>" +
            "</div>" +
            "<div class='" + "col-xs-2 grid-item" + "'>" + "<input type='" + "number" + "' " + "min='" + "1" + "' " + "id='" + "qty" + i + "' " + "value='" + cart[i].Quantity + "' " + "class='" + "form-item form-item-1x" + "'" + "onchange=quantityChange('" + i + "')" + ">" +
            "</div>" +
            "<div class='" + "col-xs-2 grid-item" + "'>" + "<b class='" + "color-dc4c46" + "' " + "id='" + "price" + i + "' " + ">Rs" + cart[i].Total + "</b></div>" +
            "<div class='" + "col-xs-1 grid-item" + "'>" + "<i class='" + "fa fa-remove" + "' " + "onclick=deleteProduct('" + i + "') " + "> " + "</i></div>";
        var list = document.getElementById("displayItems");
        list.insertBefore(obj, document.getElementById("continueShopping"));
    }
}

function deleteProduct(index) {
    for (var i = 0; i < cart.length; i++) {
        var myNode = document.getElementById(i);
        while (myNode.firstChild) {
            myNode.removeChild(myNode.firstChild);
        }
        myNode.parentNode.removeChild(myNode);
    }
    cart.splice(index, 1);
    showItems();
    totalBill();
    localStorage.setItem("cartItems", JSON.stringify(cart));
}

function totalBill() {
    var gt = 0;
    if (cart.length > 0) {
        for (var i = 0; i < cart.length; i++) {
            gt += parseInt(cart[i].Total);
        }
    }
    document.getElementById("total-1").innerText = "Rs " + gt;
    document.getElementById("total-2").innerText = "Rs " + gt;
}

function quantityChange(index) {
    var qty = parseInt(document.getElementById("qty" + index).value);
    if (qty > 0) {
        cart[index].Quantity = parseInt(qty);
        cart[index].Total = parseInt(qty) * parseInt(cart[index].Price);
        document.getElementById("price" + index).innerText = "Rs " + cart[index].Total;
        totalBill();
        localStorage.setItem("cartItems", JSON.stringify(cart));
    }
    else {
        qty = 1;
        document.getElementById("qty" + index).value = parseInt(qty);
        cart[index].Quantity = parseInt(qty);
        cart[index].Total = parseInt(qty) * parseInt(cart[index].Price);
        document.getElementById("price" + index).innerText = "Rs " + cart[index].Total;
        totalBill();
        localStorage.setItem("cartItems", JSON.stringify(cart));
    }
}