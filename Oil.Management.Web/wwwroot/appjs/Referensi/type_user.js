

$('#TableReferesiTypeUser tfoot th').each(function () {
    var title = $(this).text();
    $(this).html('<input type="text" class="form-control" placeholder="Cari ' + title + '" />');
});
var TableReferesiTypeUser = $('#TableReferesiTypeUser').DataTable({
    "paging": true,
    "searching": true,
    "ordering": false,
    "info": true,
    "pageLength": 10,
    "lengthChange": true,
    "scrollX": true,
    "processing": true,
    "ajax": {
        "url": base_url + "/api/references/GetTypeUsers",
        "method": 'GET',
        "beforeSend": function (xhr) { },
        "dataSrc": function (json) {
            if (json.Data == null) {
                swal({
                    title: 'Gagal Menampilkan Data Type User',
                    text: json.ReturnMessage,
                    confirmButtonClass: 'btn-danger text-white',
                    confirmButtonText: 'Oke, Mengerti',
                    type: 'error'
                });
                return json;
            } else {
                return json.Data;
            }
        }
    },
    "columns": [{
        "data": "IdTypeUser",
       
    },
    {
        "data": "TypeName"
    },
    {
        "data": "IdTypeUser",
        "render": function (data, type, full, meta) {
            var Data = "";
            var ParamStatus = "SetStatus('" + full.IdTypeUser + "','" + full.TypeName +  "')";
            Data += '<button type="button" onClick="EditTypeUser(' + full.IdTypeUser + ')" class="btn btn-primary btn-sm"><i class="fa fa-pencil-square-o"></i> Edit</button>&nbsp;&nbsp;';
           
            data = Data;
            return data;
        }
    }
    ],
    "bDestroy": true
});
TableReferesiTypeUser.columns().every(function () {
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
//////// START SECTION TABEL  ///////////


function AddTypeUser() {
    $('#FormTypeUser')[0].reset();
    $("#TitleFormTypeUser").html("Tambah Type User")
    $('#ModalFormTypeUser').modal("show");
   
    $("#BtnSaveTypeUser").attr("onClick", "SaveTypeUser('Tambah');");
   
}

function SaveTypeUser(typeAction) {
    var formObj = $('#FormTypeUser').serializeObject();
    var jsonData = JSON.stringify({
        "IdTypeUser": parseInt(formObj.IdTypeUser),
        "TypeName": formObj.TypeName,
    })

    if (typeAction == "Tambah") {
        var Url = base_url + "/api/references/AddTypeUser";
    } else {
        var Url = base_url + "/api/references/EditTypeUser";

    }

    $.ajax({
        url: Url,
        method: "POST",
        data: jsonData,
        dataType: "json",
        headers: {
            "Content-Type": "application/json"
        },
        beforeSend: function (before) {
            ProgressBar("wait");
        },
        success: function (responsesave) {
            ProgressBar("success");
            if (responsesave.IsSuccess == true) {
                $("#ModalFormTypeUser").modal("hide");
                $("#TableReferesiTypeUser").DataTable().ajax.reload();
                swal({
                    title: 'Berhasil ' + typeAction + ' User',
                    text: "",
                    confirmButtonClass: 'btn-success text-white',
                    confirmButtonText: 'Oke, Mengerti',
                    type: 'success'
                });
            } else if (responsesave.IsSuccess == false) {
                swal({
                    title: 'Gagal ' + typeAction + ' Type User',
                    text: responsesave.ReturnMessage,
                    confirmButtonClass: 'btn-danger text-white',
                    confirmButtonText: 'Oke, Mengerti',
                    type: 'error'
                });
            }
        },
        error: function (errorresponse) {
            ProgressBar("success");
            swal({
                title: 'Gagal ' + typeAction + ' Type User',
                text: errorresponse,
                confirmButtonClass: 'btn-danger text-white',
                confirmButtonText: 'Oke, Mengerti',
                type: 'error'
            });
        }
    });
}

function EditTypeUser(id) {
    alert(id)
}