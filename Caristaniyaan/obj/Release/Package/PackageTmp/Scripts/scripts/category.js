$(document).ready(function () {
    $(".lds-ellipsis").hide();
    $(document).keypress(
        function (event) {
            if (event.which == '13') {
                event.preventDefault();
            }


        });
    var table = $('#Categories').DataTable({
        responsive: true,
        serverSide: true,
        processing: true,
        language: {
            processing: "Loading.."
        },
        ajax: {
            url: '/Category/GetCategories',
            type: "GET",
        },
        columns: [
            {
                "data": "name", "orderable": false
            },
            {
                "data": 'Id',
                render: function (data) {
                    return '<button data-cat-id=' + data + ' id="catNameDelete" class="btn btn-danger">Delete</button>' + ' <button data-cat-id=' + data + ' id="catNameEdit" class="btn btn-primary">Edit</button>';
                }

            }
        ]
    });

    $("#catNameAdd").on("click", function () {

        if ($('#catForm').valid()) {
            var categoryName = $('#catName').val();

            var data = new FormData;
            data.append("name", categoryName);
            $.ajax({
                type: 'POST',
                url: '/Category/Add',
                data: data,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    $(".lds-ellipsis").show();
                    $("#catNameAdd").hide();
                    $("#catNameUpdate").hide();
                    $("#Categories #catNameDelete").hide();
                    $("#Categories #catNameEdit").hide();
                },
                success: function (result) {
                    if (result.success) {
                        $(".alert-success").removeClass("in").show();
                        $(".alert-text").text(result.responseText);
                        $(".alert-success").delay(200).addClass("in").fadeOut(4000);
                        table.row.add(result.data).draw();
                        $('#catName').val('');
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
                    $("#catNameAdd").show();
                    $("#catNameUpdate").show();
                    $("#Categories #catNameDelete").show();
                    $("#Categories #catNameEdit").show();
                }
            });
        }

    });

    $("#Categories").on("click", "#catNameDelete", function () {
        var button = $(this);
        var res = bootbox.confirm('Are you sure you want to delete?', function (res) {
            if (res) {
                $.ajax({
                    url: '/Category/Delete/' + button.attr("data-cat-id"),
                    type: 'POST',
                    beforeSend: function () {
                        $(".lds-ellipsis").show();
                        $("#catNameAdd").hide();
                        $("#catNameUpdate").hide();
                        $("#Categories #catNameDelete").hide();
                        $("#Categories #catNameEdit").hide();
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
                        $("#catNameAdd").show();
                        $("#catNameUpdate").show();
                        $("#Categories #catNameDelete").show();
                        $("#Categories #catNameEdit").show();
                    }
                });
            }
        });

    });

    $("#Categories").on("click", "#catNameEdit", function () {
        var button = $(this);
        $.ajax({
            url: '/Category/GetCategory/' + $(this).attr("data-cat-id"),
            method: 'GET',
            beforeSend: function () {
                $(".lds-ellipsis").show();
                $("#catNameAdd").hide();
                $("#catNameUpdate").hide();
                $("#Categories #catNameDelete").hide();
                $("#Categories #catNameEdit").hide();
            },
            success: function (result) {
                $('#header').text('Update Brand');
                $('#catName').val(result.data.name);
                $('#catName').attr('data-cat-id', result.data.Id);
                $('#catName').attr('row-no', table.row(button.parents('tr')).index());
                $('#catNameAdd').addClass('disabled');
                $('#catNameUpdate').removeClass('disabled');

                $('html,body').animate({ scrollTop: $('#formBox').offset().top }, 400, function () {
                    $('#catName').focus();
                });
            },
            complete: function () {
                $(".lds-ellipsis").hide();
                $("#catNameAdd").show();
                $("#catNameUpdate").show();
                $("#Categories #catNameDelete").show();
                $("#Categories #catNameEdit").show();
            }
        });
    });

    $("#catNameUpdate").on("click", function () {

        if ($('#catForm').valid()) {

            var catname = $('#catName').val();
            var data = new FormData;
            data.append("name", catname);
            $.ajax({
                type: 'POST',
                url: '/Category/Update/' + $('#catName').attr("data-cat-id"),
                data: data,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    $(".lds-ellipsis").show();
                    $("#catNameAdd").hide();
                    $("#catNameUpdate").hide();
                    $("#Categories #caNameDelete").hide();
                    $("#Categories #catNameEdit").hide();
                },
                success: function (result) {
                    if (result.success) {
                        $(".alert-success").removeClass("in").show();
                        $(".alert-text").text(result.responseText);
                        $(".alert-success").delay(200).addClass("in").fadeOut(4000);
                        $('#header').text('Add new Category');
                        $('#catName').val('');
                        $('#catNameAdd').removeClass('disabled');
                        $('#catNameUpdate').addClass('disabled');
                        var row = table.row($('#catName').attr("row-no"));
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
                    $("#catNameAdd").show();
                    $("#catNameUpdate").show();
                    $("#Categories #catNameDelete").show();
                    $("#Categories #catNameEdit").show();
                }
            });
        }
    });

});