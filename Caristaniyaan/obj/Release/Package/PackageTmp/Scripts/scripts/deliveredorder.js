$(document).ready(function () {

    var table = $('#deliveredOrders').DataTable({
        responsive: true,
        serverSide: true,
        processing: true,
        language: {
            processing: "Loading"
        },
        ajax: {
            url: '/Admin/GetDeliveredOrders',
            type: "GET"
        },
        columns: [
            {
                "data": "fname", "orderable": false
            },
            {
                "data": "lname", "orderable": false
            },
            {
                "data": "phoneno", "orderable": false
            },
            {
                "data": "status", "orderable": false,
                render: function (data) {
                    if (data === 0) {

                        return '<span class="label label-warning">Pending</span>';
                    } else if (data === 1) {

                        return '<span class="label label-primary">In Process</span>';

                    } else if (data === 2) {

                        return '<span class="label label-danger">Declined</span>';

                    } else if (data === 3) {

                        return '<span class="label label-success">Delivered</span>';
                    }
                }
            },
            {
                "data": "Id", "orderable": false,
                render: function (data) {
                    return '<a data-order-id=' + data + ' id="showDetails" href="/Admin/Order/' + data + '" class="btn btn-primary">Show Details</a>';
                }

            }
        ]
    });
});