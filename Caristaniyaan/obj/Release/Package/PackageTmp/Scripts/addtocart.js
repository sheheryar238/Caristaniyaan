var cart = [];
window.onload = function () {
    if (localStorage.getItem("cartItems") === null) {
        //Code here if user cart is empty on the page load
        calculateTotalItems();
        calculateGrandTotal();
    }
    else {
        cart = JSON.parse(localStorage.getItem("cartItems"));
        for (var i = 0; i < cart.length; i++) {
            createDiv(i);
        }
        calculateTotalItems();
        calculateGrandTotal();
    }
}

function addToCart(pid) {
    if (typeof (Storage) !== "undefined") {
        // Code for localStorage/sessionStorage.
        var list = { Id: pid, Pic: document.getElementById("pic").src, Name: document.getElementById("name").innerText, Price: parseInt(document.getElementById("price").innerText), Color: document.getElementById("color").innerText, Car: document.getElementById("car").innerText, Quantity: parseInt(document.getElementById("quantity").value), Total: parseInt(document.getElementById("quantity").value) * parseInt(document.getElementById("price").innerText), Link: window.location.href };

        var len = cart.length;
        if (cart.length == 0) {
            //inserting first item in cart list
            cart.push(list);
            createDiv(0);
            calculateGrandTotal();
            calculateTotalItems();
            localStorage.setItem("cartItems", JSON.stringify(cart));
        }
        else {
            for (var i = 0; i < cart.length; i++) {
                var str1 = cart[i].Name.toString();
                var str2 = document.getElementById("name").innerText.toString();
                var n = str1.localeCompare(str2);

                if (n == 0) {
                    //If the item is already purchased then this block will run 
                    cart[i].Quantity = parseInt(cart[i].Quantity) + parseInt(document.getElementById("quantity").value);
                    cart[i].Total = parseInt(cart[i].Quantity) * parseInt(cart[i].Price);
                    updateDiv(i);
                    calculateGrandTotal();
                    localStorage.setItem("cartItems", JSON.stringify(cart));
                    break;
                }
                else {
                    //if item is not found and loop has searched till last item then this block will run
                    if (len == i + 1) {
                        cart.push(list);
                        createDiv(parseInt(i + 1));
                        calculateGrandTotal();
                        calculateTotalItems();
                        localStorage.setItem("cartItems", JSON.stringify(cart));
                        break;
                    }
                }
            }
        }
    }
    else {
        // Sorry! No Web Storage support..
        alert("Please update your browser to continue...");
    }

}

function createDiv(index) {
    var obj = document.createElement("li");
    obj.setAttribute("id", index);
    obj.innerHTML = "<div class='" + "media" + "'>" +
        "<div class='" + "media-left" + "'>" +
        "<a href='" + cart[index].Link + "'>" +
        "<img class='" + "media-object" + "' " + "src='" + cart[index].Pic + "' " + "width='" + "50" + "' " + "alt='" + "image" + "'> " +
        " </a> " +
        "</div>" +
        "<div class='" + "media-body" + "'>" +
        "<h4 class='" + "product-name" + "'>" + "<a href='" + cart[index].Link + "'>" + cart[index].Name + "</a></h4>" +
        "<p>Rs" + cart[index].Price + " <span>x " + cart[index].Quantity + "</span>" + " <i style='" + "color: red" + "' " + "class='" + "fa fa-lg fa-remove pull-right btn" + "' " + "onclick=deleteItem('" + index + "') " + "> " + "</i></p>" +
        "</div>" +
        "</div>";
    var list = document.getElementById("cartProducts");
    document.getElementById("cartProducts").insertBefore(obj, list.childNodes[0]);
}

function updateDiv(index) {
    var obj = document.getElementById(index);
    obj.innerHTML = "<div class='" + "media" + "'>" +
        "<div class='" + "media-left" + "'>" +
        "<a href='" + cart[index].Link + "'>" +
        "<img class='" + "media-object" + "' " + "src='" + cart[index].Pic + "' " + "width='" + "50" + "' " + "alt='" + "image" + "'> " +
        " </a> " +
        "</div>" +
        "<div class='" + "media-body" + "'>" +
        "<h4 class='" + "product-name" + "'>" + "<a href='" + cart[index].Link + "'>" + cart[index].Name + "</a></h4>" +
        "<p>Rs" + cart[index].Price + " <span>x " + cart[index].Quantity + "</span>" + " <i style='" + "color: red" + "' " + "class='" + "fa fa-lg fa-remove pull-right btn" + "' " + "onclick=deleteItem('" + index + "') " + "> " + "</i></p>" +
        "</div>" +
        "</div>";
}

function deleteItem(index) {
    for (var i = 0; i < cart.length; i++) {
        var myNode = document.getElementById(i);
        while (myNode.firstChild) {
            myNode.removeChild(myNode.firstChild);
        }
        myNode.parentNode.removeChild(myNode);
    }
    cart.splice(index, 1);
    for (var i = 0; i < cart.length; i++) {
        createDiv(i);
    }
    calculateGrandTotal();
    calculateTotalItems();
    localStorage.setItem("cartItems", JSON.stringify(cart));
}

function calculateGrandTotal() {
    var gt = 0;
    if (cart.length > 0) {
        for (var i = 0; i < cart.length; i++) {
            gt += parseInt(cart[i].Total);
        }
    }
    document.getElementById("grandTotal").innerText = "Rs " + gt;
}

function calculateTotalItems() {
    document.getElementById("totalItems").innerText = cart.length;
}