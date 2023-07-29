$('#TableSubsidiary tfoot th').each(function () {
    var title = $(this).text();
    $(this).html('<input type="text" class="form-control" placeholder="Cari ' + title + '" />');
});

var TableSubsidiary = $('#TableSubsidiary').DataTable({
    "paging": true,
    "searching": true,
    "ordering": false,
    "info": true,
    "pageLength": 10,
    "lengthChange": true,
    "scrollX": true,
    "processing": true,
    "ajax": {
        "url": base_url + "/subsidiary/GetSubsidiarys",
        "method": 'GET',
        "beforeSend": function (xhr) { },
        "dataSrc": function (json) {
            if (json.Data == null) {
                swal({
                    title: 'Gagal Menampilkan Data Factory',
                    text: json.ReturnMessage,
                    confirmButtonClass: 'btn-danger text-white',
                    confirmButtonText: 'Oke, Mengerti',
                    type: 'error'
                });
                return json;
            } else {
                console.log(json.Data);
                return json.Data;
            }
        }
    },
    "columns": [{
        "data": "IdSubsidiaryCompany",

    },
    {
        "data": "Name"
    },
    {
        "data": "FactoryName"
    },
    {
        "data": "BusinessName"
    },
    {
        "data": "Nib"
    },
    {
        "data": "Npwp"
    }, {
        "data": "Phone"
    }, 
    {
        "data":"Email"
        },
        {
            "data": "PicName"
        },{
            "data": "PicPhone"
        },{
            "data": "PicEmail"
        },{
            "data": "PicEmail"
        }

    ],
    "bDestroy": true
});