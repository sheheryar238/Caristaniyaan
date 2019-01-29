$(document).ready(function () {
    $(".lds-ellipsis").hide();
    $(document).keypress(
        function (event) {
            if (event.which == '13') {
                event.preventDefault();
            }


        });
    var table = $('#Products').DataTable({
        responsive: true,
        serverSide: true,
        processing: true,
        language: {
            processing: "Loading.."
        },
        ajax: {
            url: '/Product/GetProducts',
            type: "GET",
        },
        columns: [
            {
                "data": "name", "orderable": false
            },
            {
                "data": 'Id',
                render: function (data) {
                    return '<button data-product-id=' + data + ' id="productDelete" class="btn btn-danger">Delete</button>' + ' <button data-product-id=' + data + ' id="productEdit" class="btn btn-primary">Edit</button>';
                }

            }
        ]
    });

    $("#productAdd").on("click", function () {

        if ($('#productForm').valid()) {
            var productname = $('#productName').val();
            var brandname = $('#brandName :selected').val();
            var productprice = $('#productPrice').val();
            var productwholeprice = $('#productWholePrice').val();
            var productcolor = $('#productColor').val();
            var productquantity = $('#productQuantity').val();
            var productcar = $('#productCar').val();
            var productdetail = $('#productDetail').val();
            var productstatus = $('#productStatus').val();
            var productpriority = $('#productPriority').val();
            var productimage = $('#productImage').get(0).files;

            var data = new FormData;
            data.append("BrandId", brandname);
            data.append("name", productname);
            data.append("price", productprice);
            data.append("whileSalePrice", productwholeprice);
            data.append("color", productcolor);
            data.append("quantity", productquantity);
            data.append("car", productcar);
            data.append("details", productdetail);
            data.append("status", productstatus);
            data.append("image", productimage[0]);
            data.append("priority", productpriority);
            
            $.ajax({
                type: 'POST',
                url: '/Product/Add',
                data: data,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    $(".lds-ellipsis").show();
                    $("#productAdd").hide();
                    $("#productUpdate").hide();
                    $("#Products #productDelete").hide();
                    $("#Products #productEdit").hide();
                },
                success: function (result) {
                    if (result.success) {
                        $(".alert-success").removeClass("in").show();
                        $(".alert-text").text(result.responseText);
                        $(".alert-success").delay(200).addClass("in").fadeOut(4000);
                        table.row.add(result.data).draw();
                        $('#productForm')[0].reset();
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
                    $("#productAdd").show();
                    $("#productUpdate").show();
                    $("#Products #productDelete").show();
                    $("#Products #productEdit").show();
                }
            });
        }

    });

    $("#Products").on("click", "#productDelete", function () {
        var button = $(this);
        var res = bootbox.confirm('Are you sure you want to delete?', function (res) {
            if (res) {
                $.ajax({
                    url: '/Product/Delete/' + button.attr("data-product-id"),
                    type: 'POST',
                    beforeSend: function () {
                        $(".lds-ellipsis").show();
                        $("#productAdd").hide();
                        $("#productUpdate").hide();
                        $("#Products #productDelete").hide();
                        $("#Products #productEdit").hide();
                    },
                    success: function (result) {
                        if (result.success) {
                            table.row(button.parents("tr")).remove().draw();
                            $(".alert-success").removeClass("in").show();
                            $(".alert-text").text(result.responseText);
                            $(".alert-success").delay(200).addClass("in").fadeOut(4000);
                        }
                        else {
                            $(".alert-danger").removeClass("in").show();
                            $(".alert-text").text(result.responseText);
                            $(".alert-danger").delay(200).addClass("in").fadeOut(4000);
                        }

                    },
                    error: function () {
                        $(".alert-danger").removeClass("in").show();
                        $(".alert-text").text('Alert! Sorry couldnt reponse to you action');
                        $(".alert-danger").delay(200).addClass("in").fadeOut(4000);
                    },
                    complete: function () {
                        $(".lds-ellipsis").hide();
                        $("#productAdd").show();
                        $("#productUpdate").show();
                        $("#Products #productDelete").show();
                        $("#Products #productEdit").show();
                    }
                });
            }
        });

    });

    $("#Products").on("click", "#productEdit", function () {
        var button = $(this);
        $.ajax({
            url: '/Product/GetProduct/' + $(this).attr("data-product-id"),
            method: 'GET',
            beforeSend: function () {
                $(".lds-ellipsis").show();
                $("#productAdd").hide();
                $("#productUpdate").hide();
                $("#Products #productDelete").hide();
                $("#Products #productEdit").hide();
            },
            success: function (result) {
                $('#productName').val(result.data.name);
                $('#brandName').val(result.data.Brand.Id).trigger('change');
                $('#productPrice').val(result.data.price);
                $('#productWholePrice').val(result.data.whileSalePrice);
                $('#productColor').val(result.data.color);
                $('#productQuantity').val(result.data.quantity);
                $('#productCar').val(result.data.model);
                $('#productDetail').val(result.data.details).trigger('change');
                $('#productStatus').val(result.data.status);
                $('#productPriority').val(result.data.priority);

                $('#productName').attr('data-product-id', result.data.Id);
                $('#productName').attr('row-no', table.row(button.parents('tr')).index());
                $('#productAdd').addClass('disabled');
                $('#productUpdate').removeClass('disabled');

                $('html,body').animate({ scrollTop: $('#formBox').offset().top }, 400, function () {
                    $('#productName').focus();
                });
            },
            complete: function () {
                $(".lds-ellipsis").hide();
                $("#productAdd").show();
                $("#productUpdate").show();
                $("#Products #productDelete").show();
                $("#Products #productEdit").show();
            }
        });
    });

    $("#productUpdate").on("click", function () {

        if ($('#productForm').valid()) {

            var productname = $('#productName').val();
            var brandname = $('#brandName :selected').val();
            var productprice = $('#productPrice').val();
            var productwholeprice = $('#productWholePrice').val();
            var productcolor = $('#productColor').val();
            var productquantity = $('#productQuantity').val();
            var productcar = $('#productCar').val();
            var productdetail = $('#productDetail').val();
            var productstatus = $('#productStatus').val();
            var productpriority = $('#productPriority').val();
            var productimage = $('#productImage').get(0).files;

            var data = new FormData;
            data.append("BrandId", brandname);
            data.append("name", productname);
            data.append("price", productprice);
            data.append("whileSalePrice", productwholeprice);
            data.append("color", productcolor);
            data.append("quantity", productquantity);
            data.append("car", productcar);
            data.append("details", productdetail);
            data.append("status", productstatus);
            data.append("image", productimage[0]);
            data.append("priority", productpriority);

            $.ajax({
                type: 'POST',
                url: '/Product/Update/' + $('#productName').attr("data-product-id"),
                data: data,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    $(".lds-ellipsis").show();
                    $("#productAdd").hide();
                    $("#productUpdate").hide();
                    $("#Products #productDelete").hide();
                    $("#Products #productEdit").hide();
                },
                success: function (result) {
                    if (result.success) {
                        $(".alert-success").removeClass("in").show();
                        $(".alert-text").text(result.responseText);
                        $(".alert-success").delay(200).addClass("in").fadeOut(4000);
                        $('#productForm')[0].reset();
                        $('#productAdd').removeClass('disabled');
                        $('#productUpdate').addClass('disabled');
                        var row = table.row($('#productName').attr("row-no"));
                        table.cell(row, 0).data(result.cat.name).draw();
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
                    $("#productAdd").show();
                    $("#productUpdate").show();
                    $("#Products #productDelete").show();
                    $("#Products #productEdit").show();
                }
            });
        }
    });

});