$(function () {
    'use strict';
    //Slider
    var $owl = $('.owl');
    $owl.each(function () {
        var $a = $(this);
        $a.owlCarousel({
            singleItem: JSON.parse($a.attr('data-singleItem')),
            items: $a.attr('data-items'),
            itemsDesktop: [1199, $a.attr('data-itemsDesktop')],
            itemsDesktopSmall: [992, $a.attr('data-itemsDesktopSmall')],
            itemsTablet: [797, $a.attr('data-itemsTablet')],
            itemsMobile: [479, $a.attr('data-itemsMobile')],
            navigation: JSON.parse($a.attr('data-buttons')),
            pagination: JSON.parse($a.attr('data-pag')),
            autoPlay: JSON.parse($a.attr('data-autoPlay')),
            navigationText: ["", ""]
        });
    });
    //Menu rs
    $('.menu-btn').on('click', function (e) {
        if ($(this).hasClass('active')) {
            $('.menu-rs').animate({ right: '-250px' }, 500);
        }
        else {
            $('.menu-rs').animate({ right: '0px' }, 500);
        }
    });
    $('.r-mv').on('click', function () {
        $('.menu-rs').animate({ right: '-250px' }, 500);
    });
    window.onresize = function () {
        mr_top();
    }
    function mr_top() {
        $('.header-fixed').next().css('marginTop', ($('header').height()) + 'px');
    }
    mr_top();
    //Preloander
    $(window).load(function () {
        $('.preloader i').fadeOut();
        $('.preloader').delay(500).fadeOut('slow');
        $('body').delay(600).css({ 'overflow': 'visible' });
    });
    //cart dropdown 
    $('.cart .dropdown-menu').on('click', function (e) {
        e.stopPropagation();
    });
    //zoom image
    $('.image-zoom').magnificPopup({
        type: 'image',
        gallery: {
            enabled: true
        }
    });
    //Search form
    $('.search-box').on('click', function (e) {
        e.stopPropagation();
    });
    $('.icon-search').on('click', function (e) {
        $('.search-txt').val('');
        $('.search-box').fadeIn();
        e.stopPropagation();
    });
    $('.search-txt').keypress(function (event) {
        if (event.keyCode == 13) {
            $('.search-box').fadeOut();
        }
    });
    $('body').click(function () {
        $('.search-box').fadeOut();
    });
});

$(document).ready(function () {

    $(".lds-ellipsis").hide();

    $("#appointmentbtn").on("click", function () {

        if ($('#appointmentForm').valid()) {
            var modalYear = $('#modalYear').val();
            var carinfo = $('#carInfo').val();
            var name = $('#name').val();
            var phone = $('#phone').val();
            var email = $('#email').val();
            var msg = $('#message').val()

            var data = new FormData;
            data.append("modalYear", modalYear);
            data.append("carInfo", carinfo);
            data.append("name", name);
            data.append("phonenumber", phone);
            data.append("email", email);
            data.append("message", msg);

            $.ajax({
                type: 'POST',
                url: '/Request/Appoinment',
                data: data,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    $(".lds-ellipsis").show();
                },
                success: function (result) {
                    if (result.success) {
                        $(".alert-success").removeClass("in").show();
                        $(".alert-text").text(result.responseText);
                        $(".alert-success").delay(200).addClass("in").fadeOut(4000);
                        $('#appointmentForm')[0].reset();
                    }
                    else {
                        $(".alert-danger").removeClass("in").show();
                        $(".alert-text").text(result.responseText);
                        $(".alert-danger").delay(200).addClass("in").fadeOut(4000);
                    }
                },
                error: function () {
                    $(".alert-danger").removeClass("in").show();
                    $(".alert-danger").delay(200).addClass("in").fadeOut(4000);
                },
                complete: function () {
                    $(".lds-ellipsis").hide();
                }
            });
        }

    });

    $("#demandbtn").on("click", function () {

        if ($('#demandForm').valid()) {
            var modalYear = $('#modalYear').val();
            var carinfo = $('#carInfo').val();
            var name = $('#name').val();
            var phone = $('#phone').val();
            var email = $('#email').val();
            var itemName = $('#itemName').val();
            var itemDetail = $('#itemDetail').val();
            var img =  $('#img').get(0).files;

            var data = new FormData;
            data.append("modalYear", modalYear);
            data.append("carInfo", carinfo);
            data.append("name", name);
            data.append("phonenumber", phone);
            data.append("email", email);
            data.append("itemName", itemName);
            data.append("itemDetail", itemDetail);
            data.append("itemImage", img[0]);

            $.ajax({
                type: 'POST',
                url: '/Request/Demand',
                data: data,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    $(".lds-ellipsis").show();
                },
                success: function (result) {
                    if (result.success) {
                        $(".alert-success").removeClass("in").show();
                        $(".alert-text").text(result.responseText);
                        $(".alert-success").delay(200).addClass("in").fadeOut(4000);
                        $('#demandForm')[0].reset();
                    }
                    else {
                        $(".alert-danger").removeClass("in").show();
                        $(".alert-text").text(result.responseText);
                        $(".alert-danger").delay(200).addClass("in").fadeOut(4000);
                    }
                },
                error: function () {
                    $(".alert-danger").removeClass("in").show();
                    $(".alert-danger").delay(200).addClass("in").fadeOut(4000);
                },
                complete: function () {
                    $(".lds-ellipsis").hide();
                }
            });
        }

    });

    $("#orderbtn").on("click", function () {

        if ($('#orderForm').valid()) {
            var fname = $('#fname').val();
            var lname = $('#lname').val();
            var email = $('#email').val();
            var phoneno = $('#phoneno').val();
            var address = $('#address').val();
            var city = $('#city').val();
            var postcode = $('#postcode').val();
            var province = $('#province').val();
            var country = $('#country').val();
            var cartItems = localStorage.getItem("cartItems");

            var data = new FormData;
            data.append("fname", fname);
            data.append("lname", lname);
            data.append("email", email);
            data.append("phoneno", phoneno);
            data.append("address", address);
            data.append("city", city);
            data.append("postcode", postcode);
            data.append("province", province);
            data.append("countary", country);
            data.append("cartItems", cartItems);

            $.ajax({
                type: 'POST',
                url: '/Request/Order',
                data: data,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    $(".lds-ellipsis").show();
                },
                success: function (result) {
                    if (result.success) {
                        $(".alert-success").removeClass("in").show();
                        $(".alert-text").text(result.responseText);
                        $(".alert-success").delay(200).addClass("in").fadeOut(4000);
                        $('#orderForm')[0].reset();
                    }
                    else {
                        $(".alert-danger").removeClass("in").show();
                        $(".alert-text").text(result.responseText);
                        $(".alert-danger").delay(200).addClass("in").fadeOut(4000);
                    }
                },
                error: function () {
                    $(".alert-danger").removeClass("in").show();
                    $(".alert-danger").delay(200).addClass("in").fadeOut(4000);
                },
                complete: function () {
                    $(".lds-ellipsis").hide();
                }
            });
        }

    });
});