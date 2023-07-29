$('#TableFactory tfoot th').each(function () {
    var title = $(this).text();
    $(this).html('<input type="text" class="form-control" placeholder="Cari ' + title + '" />');
});


var TableCustomer = $('#TableCustomer').DataTable({
    "paging": true,
    "searching": true,
    "ordering": false,
    "info": true,
    "pageLength": 10,
    "lengthChange": true,
    "scrollX": true,
    "processing": true,
    "ajax": {
        "url": base_url + "/customer/GetCustomers",
        "method": 'GET',
        "beforeSend": function (xhr) { },
        "dataSrc": function (json) {
            if (json.Data == null) {
                swal({
                    title: 'Gagal Menampilkan Data Customer',
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
        "data": "IdCustomer",

    },
        {
          "data": "NameSubsidiary"
        },
        {
            "data": "CutomerName"
        },
        {
         "data": "Nib"
        },
        {
            "data": "Npwp"
        },
        {
            "data": "Phone"
        },
    
        {
            "data": "Email"
        },
        {
         "data": "Address"
        },
        {
            "data": "PicName"
        },
        {
            "data":"PicEmail"
        },
        {
            "data": "PicPhone"
        },
        {
            "data": "StrStatus"
        },
        {
            "data": "IdCustomer",
            "render": function (data, type, full, meta) {
                var Data = "";
                var ParamStatus = "SetStatus('" + full.IdCustomer + "','" + full.CustomerName + "')";
                Data += '<button type="button" onClick="EditCustomer(' + full.IdCustomer + ')" class="btn btn-primary btn-sm"><i class="fa fa-pencil-square-o"></i> Edit</button>&nbsp;&nbsp;';

                data = Data;
                return data;
        }
    },

    ],
    "bDestroy": true
});
TableCustomer.columns().every(function () {
    var that = this;

    $('input', this.footer()).on('keyup change clear', function () {
        if (that.search() !== this.value) {
            that
                .search(this.value)
                .draw();
        }
    });
});
$(".dataTables_filter").css("display", "none");
$(".dataTables_length").css("display", "none");

function EditCustomer(id) {
    window.location.href = base_url + "/customer/edit/" + id
}