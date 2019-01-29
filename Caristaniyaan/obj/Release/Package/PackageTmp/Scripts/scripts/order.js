$(document).ready(function () {


    $(".lds-ellipsis").hide();
    $("#orderProcess").on("click", function () {

        $.ajax({
            url: '/Admin/Deliver/' + $("#orderProcess").attr("data-model-id"),
            type: 'POST',
            beforeSend: function () {
                $(".lds-ellipsis").show();
                $("#orderProcess").hide();
            },
            success: function (result) {
                if (!result.success) {
                    $(".alert-danger").removeClass("in").show();
                    $(".alert-text").text(result.itemname + " is out of stock for this Order!");
                    $(".alert-danger").delay(200).addClass("in").fadeOut(9000);
                }
                else {
                    window.location.href = '/Admin/PendingOrders';
                }

            },
            error: function () {
                $(".alert-danger").removeClass("in").show();
                $(".alert-text").text('Alert! Sorry couldnt reponse to you action');
                $(".alert-danger").delay(200).addClass("in").fadeOut(4000);
            },
            complete: function () {
                $(".lds-ellipsis").hide();
                $("#orderProcess").show();
                $("#pdf").show();
            }
        });

    });

});