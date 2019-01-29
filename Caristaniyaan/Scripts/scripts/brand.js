$(document).ready(function () {
    $(".lds-ellipsis").hide();
    $(document).keypress(
        function (event) {
            if (event.which == '13') {
                event.preventDefault();
            }


        });
    var table = $('#Brands').DataTable({
        responsive: true,
        serverSide: true,
        processing: true,
        language: {
            processing: "Loading.."
        },
        ajax: {
            url: '/Brand/GetBrands',
            type: "GET",
        },
        columns: [
            {
                "data": "name", "orderable": false
            },
            {
                "data": "SubCategory.name"
            },
            {
                "data": 'Id',
                render: function (data) {
                    return '<button data-brand-id=' + data + ' id="brandNameDelete" class="btn btn-danger">Delete</button>' + ' <button data-brand-id=' + data + ' id="brandNameEdit" class="btn btn-primary">Edit</button>';
                }

            }
        ]
    });

    $("#brandNameAdd").on("click", function () {

        if ($('#brandForm').valid()) {
            var brandname = $('#brandName').val();
            var subcategoryId = $('#subCatName').val();

            var data = new FormData;
            data.append("name", brandname);
            data.append("SubCategoryId", subcategoryId);
            $.ajax({
                type: 'POST',
                url: '/Brand/Add',
                data: data,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    $(".lds-ellipsis").show();
                    $("#brandNameAdd").hide();
                    $("#brandNameUpdate").hide();
                    $("#Brands #brandNameDelete").hide();
                    $("#Brands #brandNameEdit").hide();
                },
                success: function (result) {
                    if (result.success) {
                        $(".alert-success").removeClass("in").show();
                        $(".alert-text").text(result.responseText);
                        $(".alert-success").delay(200).addClass("in").fadeOut(4000);
                        table.row.add(result.data).draw();
                        $('#brandForm')[0].reset();
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
                    $("#brandNameAdd").show();
                    $("#brandNameUpdate").show();
                    $("#Brands #brandNameDelete").show();
                    $("#Brands #brandNameEdit").show();
                }
            });
        }

    });

    $("#Brands").on("click", "#brandNameDelete", function () {
        var button = $(this);
        var res = bootbox.confirm('Are you sure you want to delete?', function (res) {
            if (res) {
                $.ajax({
                    url: '/Brand/Delete/' + button.attr("data-brand-id"),
                    type: 'POST',
                    beforeSend: function () {
                        $(".lds-ellipsis").show();
                        $("#brandNameAdd").hide();
                        $("#brandNameUpdate").hide();
                        $("#Brands #brandNameDelete").hide();
                        $("#Brands #brandNameEdit").hide();
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
                        $("#brandNameAdd").show();
                        $("#brandNameUpdate").show();
                        $("#Brands #brandNameDelete").show();
                        $("#Brands #brandNameEdit").show();
                    }
                });
            }
        });

    });

    $("#Brands").on("click", "#brandNameEdit", function () {
        var button = $(this);
        $.ajax({
            url: '/Brand/GetBrand/' + $(this).attr("data-brand-id"),
            method: 'GET',
            beforeSend: function () {
                $(".lds-ellipsis").show();
                $("#brandNameAdd").hide();
                $("#brandNameUpdate").hide();
                $("#Brands #brandNameDelete").hide();
                $("#Brands #brandNameEdit").hide();
            },
            success: function (result) {
                //$('#header').text('Update Sub Category');
                $('#brandName').val(result.data.name);
                $('#subCatName').val(result.data.SubCategory.Id);
                $('#brandName').attr('data-brand-id', result.data.Id);
                $('#brandName').attr('row-no', table.row(button.parents('tr')).index());
                $('#brandNameAdd').addClass('disabled');
                $('#brandNameUpdate').removeClass('disabled');

                $('html,body').animate({ scrollTop: $('#formBox').offset().top }, 400, function () {
                    $('#brandName').focus();
                });
            },
            complete: function () {
                $(".lds-ellipsis").hide();
                $("#brandNameAdd").show();
                $("#brandNameUpdate").show();
                $("#Brands #brandNameDelete").show();
                $("#Brands #brandNameEdit").show();
            }
        });
    });

    $("#brandNameUpdate").on("click", function () {

        if ($('#brandForm').valid()) {

            var brandname = $('#brandName').val();
            var subcategoryId = $('#subCatName').val();
            var data = new FormData;
            data.append("name", brandname);
            data.append("SubCategoryId", subcategoryId);
            $.ajax({
                type: 'POST',
                url: '/Brand/Update/' + $('#brandName').attr("data-brand-id"),
                data: data,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    $(".lds-ellipsis").show();
                    $("#brandNameAdd").hide();
                    $("#brandNameUpdate").hide();
                    $("#Brands #brandNameDelete").hide();
                    $("#Brands #brandNameEdit").hide();
                },
                success: function (result) {
                    if (result.success) {
                        $(".alert-success").removeClass("in").show();
                        $(".alert-text").text(result.responseText);
                        $(".alert-success").delay(200).addClass("in").fadeOut(4000);
                        $('#brandForm')[0].reset();
                        $('#brandNameAdd').removeClass('disabled');
                        $('#brandNameUpdate').addClass('disabled');
                        var row = table.row($('#brandName').attr("row-no"));
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
                    $("#brandNameAdd").show();
                    $("#brandNameUpdate").show();
                    $("#Brands #brandNameDelete").show();
                    $("#Brands #brandNameEdit").show();
                }
            });
        }
    });

});