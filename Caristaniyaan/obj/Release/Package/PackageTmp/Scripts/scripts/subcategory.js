$(document).ready(function () {
    $(".lds-ellipsis").hide();
    $(document).keypress(
        function (event) {
            if (event.which == '13') {
                event.preventDefault();
            }


        });
    var table = $('#SubCategories').DataTable({
        responsive: true,
        serverSide: true,
        processing: true,
        language: {
            processing: "Loading.."
        },
        ajax: {
            url: '/SubCategory/GetSubCategories',
            type: "GET",
        },
        columns: [
            {
                "data": "name", "orderable": false
            },
            {
                "data": "Category.name"
            },
            {
                "data": 'Id',
                render: function (data) {
                    return '<button data-subcat-id=' + data + ' id="subCatNameDelete" class="btn btn-danger">Delete</button>' + ' <button data-subcat-id=' + data + ' id="subCatNameEdit" class="btn btn-primary">Edit</button>';
                }

            }
        ]
    });

    $("#subCatNameAdd").on("click", function () {

        if ($('#subCatForm').valid()) {
            var subcatname = $('#subCatName').val();
            var categoryId = $('#catName').val();
        
            var data = new FormData;
            data.append("name", subcatname);
            data.append("CategoryId", categoryId);
            $.ajax({
                type: 'POST',
                url: '/SubCategory/Add',
                data: data,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    $(".lds-ellipsis").show();
                    $("#subCatNameAdd").hide();
                    $("#subCatNameUpdate").hide();
                    $("#SubCategories #subCatNameDelete").hide();
                    $("#SubCategories #subCatNameEdit").hide();
                },
                success: function (result) {
                    if (result.success) {
                        $(".alert-success").removeClass("in").show();
                        $(".alert-text").text(result.responseText);
                        $(".alert-success").delay(200).addClass("in").fadeOut(4000);
                        table.row.add(result.data).draw();
                        $('#subCatForm')[0].reset();
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
                    $("#subCatNameAdd").show();
                    $("#subCatNameUpdate").show();
                    $("#SubCategories #subCatNameDelete").show();
                    $("#SubCategories #subCatNameEdit").show();
                }
            });
        }

    });

    $("#SubCategories").on("click", "#subCatNameDelete", function () {
        var button = $(this);
        var res = bootbox.confirm('Are you sure you want to delete?', function (res) {
            if (res) {
                $.ajax({
                    url: '/SubCategory/Delete/' + button.attr("data-subcat-id"),
                    type: 'POST',
                    beforeSend: function () {
                        $(".lds-ellipsis").show();
                        $("#subCatNameAdd").hide();
                        $("#subCatNameUpdate").hide();
                        $("#SubCategories #subCatNameDelete").hide();
                        $("#SubCategories #subCatNameEdit").hide();
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
                        $("#subCatNameAdd").show();
                        $("#subCatNameUpdate").show();
                        $("#SubCategories #subCatNameDelete").show();
                        $("#SubCategories #subCatNameEdit").show();
                    }
                });
            }
        });

    });

    $("#SubCategories").on("click", "#subCatNameEdit", function () {
        var button = $(this);
        $.ajax({
            url: '/SubCategory/GetSubCategory/' + $(this).attr("data-subcat-id"),
            method: 'GET',
            beforeSend: function () {
                $(".lds-ellipsis").show();
                $("#subCatNameAdd").hide();
                $("#subCatNameUpdate").hide();
                $("#SubCategories #subCatNameDelete").hide();
                $("#subCategories #subCatNameEdit").hide();
            },
            success: function (result) {
                $('#header').text('Update Sub Category');
                $('#subCatName').val(result.data.name);
                $('#catName').val(result.data.Category.Id);
                $('#subCatName').attr('data-subcat-id', result.data.Id);
                $('#subCatName').attr('row-no', table.row(button.parents('tr')).index());
                $('#subCatNameAdd').addClass('disabled');
                $('#subCatNameUpdate').removeClass('disabled');

                $('html,body').animate({ scrollTop: $('#formBox').offset().top }, 400, function () {
                    $('#subCatName').focus();
                });
            },
            complete: function () {
                $(".lds-ellipsis").hide();
                $("#subCatNameAdd").show();
                $("#subCatNameUpdate").show();
                $("#SubCategories #subCatNameDelete").show();
                $("#SubCategories #subCatNameEdit").show();
            }
        });
    });

    $("#subCatNameUpdate").on("click", function () {

        if ($('#subCatForm').valid()) {

            var catname = $('#subCatName').val();
            var categoryId = $('#catName').val();
            var data = new FormData;
            data.append("name", catname);
            data.append("CategoryId", categoryId);
            $.ajax({
                type: 'POST',
                url: '/SubCategory/Update/' + $('#subCatName').attr("data-subcat-id"),
                data: data,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    $(".lds-ellipsis").show();
                    $("#subCatNameAdd").hide();
                    $("#subCatNameUpdate").hide();
                    $("#SubCategories #subCaNameDelete").hide();
                    $("#SubCategories #subCatNameEdit").hide();
                },
                success: function (result) {
                    if (result.success) {
                        $(".alert-success").removeClass("in").show();
                        $(".alert-text").text(result.responseText);
                        $(".alert-success").delay(200).addClass("in").fadeOut(4000);
                        $('#header').text('Add new Sub Category');
                        $('#subCatForm')[0].reset();
                        $('#subCatNameAdd').removeClass('disabled');
                        $('#subCatNameUpdate').addClass('disabled');
                        var row = table.row($('#subCatName').attr("row-no"));
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
                    $("#subCatNameAdd").show();
                    $("#subCatNameUpdate").show();
                    $("#SubCategories #subCatNameDelete").show();
                    $("#Subategories #subCatNameEdit").show();
                }
            });
        }
    });

});