﻿
$('#TableBusinessUnit tfoot th').each(function () {
    var title = $(this).text();
    $(this).html('<input type="text" class="form-control" placeholder="Cari ' + title + '" />');
});

console.info(base_url + "/BusinessUnit/GetBusinessUnits")

var TableBusinessUnit = $('#TableBusinessUnit').DataTable({
    "paging": true,
    "searching": true,
    "ordering": false,
    "info": true,
    "pageLength": 10,
    "lengthChange": true,
    "scrollX": true,
    "processing": true,
    "ajax": {
        "url": base_url + "/BusinessUnit/GetBusinessUnits",
        "method": 'GET',
        "beforeSend": function (xhr) { },
        "dataSrc": function (json) {
            
            if (json.Data == null) {
                swal({
                    title: 'Gagal Menampilkan Data Business Unit',
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
        "data": "IdBusinessUnit",

    },
    {
        "data": "BusinessUnitName"
    }, {
        "data": "StrStatus"
    }, {
        "data": "IdBusinessUnit",
        "render": function (data, type, full, meta) {
            var Data = "";
            var ParamStatus = "SetStatus('" + full.IdBusinessUnit + "','" + full.BusinessUnitName + "')";
            Data += '<button type="button" onClick="EditBusinessUnit(' + full.IdBusinessUnit + ')" class="btn btn-primary btn-sm"><i class="fa fa-pencil-square-o"></i> Edit</button>&nbsp;&nbsp;';

            data = Data;
            return data;
        }
    },

    ],
    "bDestroy": true
});
TableBusinessUnit.columns().every(function () {
    var that = this;

    $('input', this.footer()).on('keyup change clear', function () {
        if (that.search() !== this.value) {
            that
                .search(this.value)
                .draw();
        }
    });
});



function EditBusinessUnit(id) {
    window.location.href = base_url + "/BusinessUnit/edit/" + id
}