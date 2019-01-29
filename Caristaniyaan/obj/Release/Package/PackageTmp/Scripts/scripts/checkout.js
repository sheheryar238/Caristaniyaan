var cart = [];
window.onload = function () {
    if (localStorage.getItem("cartItems") === null) {
        //Code here if user cart is empty on the page load
        document.getElementById("subTotal").innerText = "Rs " + 0;
        document.getElementById("Total").innerText = "Rs " + 0;
    }
    else {
        cart = JSON.parse(localStorage.getItem("cartItems"));
        var gt = 0;
        if (cart.length > 0) {
            for (var i = 0; i < cart.length; i++) {
                gt += parseInt(cart[i].Total);
            }
        }
        document.getElementById("subTotal").innerText = "Rs " + gt;
        document.getElementById("Total").innerText = "Rs " + gt;
    }
}