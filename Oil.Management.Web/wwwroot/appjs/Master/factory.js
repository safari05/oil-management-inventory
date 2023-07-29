$('#TableFactory tfoot th').each(function () {
    var title = $(this).text();
    $(this).html('<input type="text" class="form-control" placeholder="Cari ' + title + '" />');
});


var TableFactory = $('#TableFactory').DataTable({
    "paging": true,
    "searching": true,
    "ordering": false,
    "info": true,
    "pageLength": 10,
    "lengthChange": true,
    "scrollX": true,
    "processing": true,
    "ajax": {
        "url": base_url + "/factory/GetFactorys",
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
        "data": "IdFactory",

    },
    {
        "data": "FactoryName"
    },
    {
        "data": "Nib"
    },
    {
        "data": "Pic"
    }, 
    {
        "data": "Email"
    },
    {
        "data": "Address"
    }, {
        "data": "StrStatus"
    }, {
        "data": "IdFactory",
        "render": function (data, type, full, meta) {
            var Data = "";
            var ParamStatus = "SetStatus('" + full.IdFactory + "','" + full.FactoryName  + "')";
            Data += '<button type="button" onClick="EditFactory(' + full.IdFactory + ')" class="btn btn-primary btn-sm"><i class="fa fa-pencil-square-o"></i> Edit</button>&nbsp;&nbsp;';

            data = Data;
            return data;
        }
    },
  
    ],
    "bDestroy": true
});
TableFactory.columns().every(function () {
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

function EditFactory(id) {
    window.location.href =base_url+"/factory/edit/"+id
}