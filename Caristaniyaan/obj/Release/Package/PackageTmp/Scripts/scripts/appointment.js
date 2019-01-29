$(document).ready(function () {
    var table = $('#Appointment').DataTable({
        responsive: true,
        serverSide: true,
        processing: true,
        language: {
            processing: "Loading.."
        },
        ajax: {
            url: '/Request/GetAppointments',
            type: "GET",
        },
        columns: [
            {
                "data": "modalYear", "orderable": false
            },
            {
                "data": "carInfo"
            },
            {
                "data": "name"
            },
            {
                "data": "phonenumber"
            },
            {
                "data": "email"
            },
            {
                "data": "message"
            },
            //{
            //    "data": 'Id',
            //    render: function (data) {
            //        return '<button data-brand-id=' + data + ' id="brandNameDelete" class="btn btn-danger">Delete</button>' + ' <button data-brand-id=' + data + ' id="brandNameEdit" class="btn btn-primary">Edit</button>';
            //    }

            //}
        ]
    });
});