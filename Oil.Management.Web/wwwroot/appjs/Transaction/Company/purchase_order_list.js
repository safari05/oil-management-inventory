$('#TbCompanyPo tfoot th').each(function () {
    var title = $(this).text();
    $(this).html('<input type="text" class="form-control" placeholder="Cari ' + title + '" />');
});

let approveFinance = $('#ApproveFinn').val();
var TbCompanyPo = $('#TbCompanyPo').DataTable({
    "paging": true,
    "searching": true,
    "ordering": false,
    "info": true,
    "pageLength": 10,
    "lengthChange": true,
    "scrollX": true,
    "processing": true,
    "ajax": {
        "url": base_url + "/CompanyPo/GetPoCompanys",
        "method": 'GET',
        "beforeSend": function (xhr) { },
        "dataSrc": function (json) {
            if (json.Data == null) {
                swal({
                    title: 'Gagal Menampilkan Data Company PO',
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
    "columns": [
        { "data": "IdTrxPo" },
        { "data": "CodePo" },
        { "data": "PoName" },
        { "data": "ContractName" },
        { "data": "Total" },
        { "data": "Amount" },
        { "data": "TimeArival" },
        {
            "data": "StrStatus",
            "render": function (data, type, full, meta) {
                var Data = "";
                if (full.StrStatus == "Waiting") {
                    Data += '<div class="badge badge-warning"> ' + full.StrStatus + '</div> '
                } else if (full.StrStatus == "Pending") {

                    Data += '<div class="badge badge-secondary"> ' + full.StrStatus + '</div> '
                } else if (full.StrStatus == "Approve") {

                    Data += '<div class="badge badge-success"> ' + full.StrStatus + '</div> '
                } else {
                    Data += '<div class="badge badge-danger"> ' + full.StrStatus + '</div> '
                }
                return Data;
            }
           
        },
        { "data": "Tax" },
        {
            "data": "IdTrxPo",
            "data": "StrStatus",
            "render": function (data, type, full, meta) {
                var Data = "";
                var ParamStatus = "SetStatus('" + full.IdTrxPo + "','" + full.PoName + "')";
                if (full.StrStatus == "Approve" || full.StrStatus == "Reject") {
                    Data += '<div class="badge badge-secondary"> Edit </div>';
                } else {
                    Data += '<button type="button" onClick="EditCompanyPo(' + full.IdTrxPo + ')" class="btn btn-primary btn-sm"><i class="fa fa-pencil-square-o"></i> Edit</button>&nbsp;&nbsp;';
                }
                
                if (approveFinance == 1) {
                    if (full.StrStatus != "Approve" && full.StrStatus != "Reject") {
                        Data += '<button type="button" onClick="ApproveCompanyPo(' + full.IdTrxPo + ')" class="btn btn-warning btn-sm"><i class="fa fa-pencil-square-o"></i> Approve </button>&nbsp;&nbsp;';

                    }
                   

                }
                data = Data;
                return data;
            }
        }
    ],
    "bDestroy": true
})

TbCompanyPo.columns().every(function () {
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

function ApproveCompanyPo(id) {
    window.location.href = base_url + "/CompanyPo/approve/" + id
}

function EditCompanyPo(id) {
    window.location.href = base_url + "/CompanyPo/edit/" + id
}
