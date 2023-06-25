$('#TabelRoleSetting tfoot th').each(function () {
    var title = $(this).text();
    $(this).html('<input type="text" class="form-control" placeholder="Cari ' + title + '" />');
});
var TabelRoleSetting = $('#TabelRoleSetting').DataTable({
    "paging": true,
    "searching": true,
    "ordering": false,
    "info": false,
    "pageLength": 5,
    "lengthChange": false,
    "scrollX": true,
    "processing": true,
    "ajax": {
        "url": base_url + "/Users/GetRoles",
        "method": 'GET',
        headers: {
            "Content-Type": "application/json"
        },
        "beforeSend": function (xhr) {
        },
        "dataSrc": function (json) {
            if (json.Data == null) {
                swal({ title: 'Gagal Menampilkan Data Role', text: json.ReturnMessage, confirmButtonClass: 'btn-danger text-white', confirmButtonText: 'Oke, Mengerti', type: 'error' });
                return json;
            } else {
                return json.Data;
            }
        }
    },
    columnDefs: [
        { targets: [0], width: "35%", visible: true },
        { targets: [1], width: "35%", visible: true },
        { targets: [2], width: "30%", visible: true },
    ],
    "columns": [
        { "data": "IdRole" },
        {
            "data": "RoleName",
            "render": function (data, type, full, meta) {
                return full.RoleName;
            }
        },
        {
            "data": "IdUser",
            "render": function (data, type, full, meta) {
                var ParamRole = "'" + full.IdRole + "','" + full.RoleName + "'";
                var aksi_set_menu = 'onClick="EditMenu(' + ParamRole + ');"';
                var aksi_set_hak_akses = 'onClick="EditHakAkses(' + ParamRole + ');"';
                data = '<button type="button" class="btn btn-primary" ' + aksi_set_menu + '><i class="fa fa-pencil-square-o"></i> Set Menu</button>';
                return data;
            }
        },
    ],
    "bDestroy": true
});
TabelRoleSetting.columns().every(function () {
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


$("#HeaderBody").html("Manajemen Role");



// modifikasi fungsi seperti dual list box
$('.ComboSumber').on('change', function () {
    var ValueSelected = this.value;
    var TextSelected = $('option:selected', this).text();

    var HtmlSelected = "<option value='" + ValueSelected + "' selected>" + TextSelected + "</option>";
    $('.ComboTerpilih').append(HtmlSelected);
    $('option:selected', this).remove();
});

$('.ComboTerpilih').on('change', function () {
    var ValueSelected = this.value;
    var TextSelected = $('option:selected', this).text();

    var HtmlSelected = "<option value='" + ValueSelected + "'>" + TextSelected + "</option>";
    $('.ComboSumber').append(HtmlSelected);
    $('option:selected', this).remove();
});

function PilihSemua() {
    $('.ComboSumber').find('option').each(function (index, item) {
        var ValueSelected = $(item).val();
        var TextSelected = $(item).text();

        var HtmlSelected = "<option value='" + ValueSelected + "' selected>" + TextSelected + "</option>";
        $('.ComboTerpilih').append(HtmlSelected);

        $(this).remove();
    });
}

function BatalPilihSemua() {
    $('.ComboTerpilih').find('option').each(function (index, item) {
        var ValueSelected = $(item).val();
        var TextSelected = $(item).text();

        var HtmlSelected = "<option value='" + ValueSelected + "'>" + TextSelected + "</option>";
        $('.ComboSumber').append(HtmlSelected);

        $(this).remove();
    });
}


function ComboGetApplTaskByRole(handleData, IdRole) {
    var Url = base_url + "/Applmgt/GetApplTaskByRole?IdRole=" + parseInt(IdRole);
    $.ajax({
        url: Url,
        type: "GET",
        dataType: "json",
        headers: {
            "Content-Type": "application/json"
        },
        beforeSend: function (beforesend) {
            ProgressBar("wait");
        },
        success: function (responsesuccess) {
            ProgressBar("success");
            if (responsesuccess.IsSuccess == true) {
                var HtmlComboNoAssignTasks = "";
                var HtmlComboAssignTasks = "";
                var JmlNoAssignTasks = (responsesuccess.Data.NoAssignTasks).length;
                var JmlAssignTasks = (responsesuccess.Data.AssignTasks).length;

                for (var inat = 0; inat < JmlNoAssignTasks; inat++) {
                    HtmlComboNoAssignTasks += "<option value='" + responsesuccess.Data.NoAssignTasks[inat].IdApplTask + "'>" + responsesuccess.Data.NoAssignTasks[inat].ApplTaskName + "</option>";
                }
                for (var iat = 0; iat < JmlAssignTasks; iat++) {
                    HtmlComboAssignTasks += "<option value='" + responsesuccess.Data.AssignTasks[iat].IdApplTask + "' selected>" + responsesuccess.Data.AssignTasks[iat].ApplTaskName + "</option>";
                }

                handleData(HtmlComboNoAssignTasks, HtmlComboAssignTasks);

            }
        },
        error: function (responserror, a, e) {
            ProgressBar("success");
            swal({
                title: 'Error :(',
                text: JSON.stringify(responserror) + " : " + e,
                confirmButtonClass: 'btn-danger text-white',
                confirmButtonText: 'Oke, Mengerti',
                type: 'error'
            });
        }
    });
}
function EditMenu(IdRole, RoleName) {
    $("#ImgSearch").css("display", "none");
    $("#FormSetData").css("display", "block");
    $("#FormRole").css("display", "none");

    $("#LabelNoAssign").html("No Assign Menu");
    $("#LabelAssign").html("Assign Menu");

    $("#HeaderBody").html("Set Menu - " + RoleName);
    $('.ComboSumber').attr("name", "NoAssignTasks[]");
    $('.ComboTerpilih').attr("name", "AssignTasks[]");
    $("#BtnSave").attr("onClick", "save_data('SetMenu')");

    $("input[name='IdRole']").val(IdRole);
    //ComboGetApplTaskByRole(IdRole);

    ComboGetApplTaskByRole(function (HtmlComboNoAssignTasks, HtmlComboAssignTasks) {
        $("select[name='NoAssignTasks[]']").html(HtmlComboNoAssignTasks);
        $("select[name='AssignTasks[]']").html(HtmlComboAssignTasks);
    }, IdRole);
}

function EditHakAkses(IdRole, RoleName) {
    $("#ImgSearch").css("display", "none");
    $("#FormSetData").css("display", "block");
    $("#SetHakAkses").css("display", "block");

    $("#LabelNoAssign").html("No Assign Accesses");
    $("#LabelAssign").html("Assign Accesses");

    $("#HeaderBody").html("Set Hak Akses - " + RoleName);
    $('.ComboSumber').attr("name", "NoAssignAccesses[]");
    $('.ComboTerpilih').attr("name", "AssignAccesses[]");
    $("#BtnSave").attr("onClick", "save_data('SetHakAkses')");

    $("input[name='IdRole']").val(IdRole);

    ComboGetAccessByRole(function (HtmlComboNoAssignAccesses, HtmlComboAssignAccesses) {
        $("select[name='NoAssignAccesses[]']").html(HtmlComboNoAssignAccesses);
        $("select[name='AssignAccesses[]']").html(HtmlComboAssignAccesses);
    }, IdRole);
}

function save_data(Tipe) {
    var formObj = $('#FormSetData').serializeObject();
    if (Tipe == "SetMenu") {
        var IdApplTasks = "";
        $('.ComboTerpilih').find('option').each(function (index, item) {
            IdApplTasks += "&IdApplTasks=" + parseInt($(item).val());
        });
        var Url = base_url + "/Applmgt/AddTasksRole?IdRole=" + parseInt(formObj.IdRole) + IdApplTasks;
    } else if (Tipe == "SetHakAkses") {
        var IdAccesses = "";
        $('.ComboTerpilih').find('option').each(function (index, item) {
            IdAccesses += "&IdHakAksess=" + parseInt($(item).val());
        });
        var Url = base_url + "/HakAksess/AddHakAksesJabatan?IdJabatanProyek=" + parseInt(formObj.IdRole) + IdAccesses;
    }
    $.ajax({
        url: Url,
        method: "POST",
        dataType: "json",
        beforeSend: function (before) {
            ProgressBar("wait");
        },
        success: function (responsesave) {
            ProgressBar("success");
            if (responsesave.IsSuccess == true) {
                $('#FormSetData')[0].reset();
                $("#TabelRoleSetting").DataTable().ajax.reload();
                swal({ title: 'Berhasil', text: "", confirmButtonClass: 'btn-success text-white', confirmButtonText: 'Oke, Mengerti', type: 'success' });
            } else if (responsesave.IsSuccess == false) {
                swal({ title: 'Gagal', text: responsesave.ReturnMessage, confirmButtonClass: 'btn-danger text-white', confirmButtonText: 'Oke, Mengerti', type: 'error' });
            }
        }, error: function (responserror, a, e) {
            ProgressBar("success");
            swal({ title: 'Error :(', text: JSON.stringify(responserror) + " : " + e, confirmButtonClass: 'btn-danger text-white', confirmButtonText: 'Oke, Mengerti', type: 'error' });
        }
    });
}

function TambahData() {
    $("#FormRole").css("display", "block");
    $("#FormSetData").css("display", "none");
    $("#ImgSearch").css("display", "none");
    $("#HeaderBody").html("Tambah Hak Akses");
}

function EditData(IdRole) {
    $("#FormRole").css("display", "block");
    $("#FormSetData").css("display", "none");
    $("#ImgSearch").css("display", "none");

    var Url = base_url + "/Users/GetRole?IdRole=" + parseInt(IdRole);
    $.ajax({
        url: Url,
        type: "GET",
        dataType: "json",
        headers: {
            "Content-Type": "application/json"
        },
        beforeSend: function (beforesend) {
            ProgressBar("wait");
        },
        success: function (responsesuccess) {
            ProgressBar("success");
            if (responsesuccess.IsSuccess == true) {
                $("input[name='IdRole']").val(responsesuccess.Data.IdRole);
                $("input[name='RoleName']").val(responsesuccess.Data.RoleName);
            } else if (responsesave.IsSuccess == false) {
                swal({ title: 'Gagal Menampilkan Role', text: responsesuccess.ReturnMessage, confirmButtonClass: 'btn-danger text-white', confirmButtonText: 'Oke, Mengerti', type: 'error' });
            }
        }, error: function (responserror, a, e) {
            ProgressBar("success");
            swal({ title: 'Error :(', text: JSON.stringify(responserror) + " : " + e, confirmButtonClass: 'btn-danger text-white', confirmButtonText: 'Oke, Mengerti', type: 'error' });
        }
    });
}

function save_role() {
    var formObj = $('#FormRole').serializeObject();
    var Url = base_url + "/Users/AddRole?IdRole=" + parseInt(formObj.IdRole) + "&RoleName=" + formObj.RoleName;
    $.ajax({
        url: Url,
        method: "POST",
        dataType: "json",
        beforeSend: function (before) {
            ProgressBar("wait");
        },
        success: function (responsesave) {
            ProgressBar("success");
            if (responsesave.IsSuccess == true) {
                $('#FormRole')[0].reset();
                $("#TabelRoleSetting").DataTable().ajax.reload();
                window.open(location.href, '_self');
                swal({ title: 'Berhasil Menambah Role', text: "", confirmButtonClass: 'btn-success text-white', confirmButtonText: 'Oke, Mengerti', type: 'success' });
            } else if (responsesave.IsSuccess == false) {
                swal({ title: 'Gagal Menambah Role', text: responsesave.ReturnMessage, confirmButtonClass: 'btn-danger text-white', confirmButtonText: 'Oke, Mengerti', type: 'error' });
            }
        }, error: function (responserror, a, e) {
            ProgressBar("success");
            swal({ title: 'Error :(', text: JSON.stringify(responserror) + " : " + e, confirmButtonClass: 'btn-danger text-white', confirmButtonText: 'Oke, Mengerti', type: 'error' });
        }
    });
}