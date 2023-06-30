
// object urls
const urls = {
    GetAppl: base_url + "/Applmgt/GetAppls",
    GetApplParentMenu: base_url + "/Applmgt/GetApplParentMenu",
    GetApplMgtTask: base_url + "/Applmgt/GetApplMgtTask?",
    AddApplTask: base_url + "/Applmgt/AddApplTask",
    DeleteApplTask: base_url + "/Applmgt/DeleteApplTask",
    EditApplTask: base_url + "/Applmgt/EditApplTask",
}


$(document).ready(function () {

});


// generate to datatables
function TabelMenuSetting(idAppl) {
    $('#TabelMenuSetting tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text" class="form-control" placeholder="Cari ' + title + '" />');
    });

    var TabelMenuSetting = $('#TabelMenuSetting').DataTable({
        processing: true,
        serverSide: true,
        filter: true,
        pageLength: 5,
        lengthChange: false,
        scrollX: true,
        "ajax": {
            "url": urls.GetApplMgtTask + "IdAppl=" + idAppl,
            datatype: "json",
            "method": 'POST',
            "beforeSend": function (xhr) {
            },
            "dataSrc": function (json) {
                if (json.data == null) {
                    swal({ title: 'Gagal Menampilkan Data Menu', text: json.ReturnMessage, confirmButtonClass: 'btn-danger text-white', confirmButtonText: 'Oke, Mengerti', type: 'error' });
                    return json;
                } else {
                    return json.data;
                }
            }
        },
        columnDefs: [
            { targets: [0], width: "40%", visible: true, searchable: true },
            { targets: [1], width: "40%", visible: true, searchable: true },
            { targets: [2], width: "15%", visible: true, searchable: true },
            { targets: [3], width: "15%", visible: true, searchable: true },
        ],
        "columns": [
            { "data": "ApplName" },
            { "data": "ApplTaskName" },
            {
                "data": "ControllerName",
                "data": "ActionName",
                "render": function (data, type, full, meta) {
                    return "<strong>" + full.ControllerName + "</strong> / <strong>" + full.ActionName + "</strong>";
                }
            },
            {
                "data": "IdApplTask",
                "data": "IdApplTaskParent",
                "data": "IdAppl",
                "data": "Description",
                "data": "IconName",
                "render": function (data, type, full, meta) {
                    var ParamMenu = "'" + full.IdAppl + "','" + full.IdApplTask + "','" + full.IdApplTaskParent + "','" + full.ApplTaskName + "','" + full.IdAppl + "','" + full.ControllerName + "','" + full.ActionName + "','" + full.Description + "','" + full.IconName + "'";
                    var ParamHapusMenu = "'" + full.IdApplTask + "','" + full.ApplTaskName + "'";
                    var aksi_set_menu = 'onClick="EditMenu(' + ParamMenu + ');"';
                    var aksi_hapus_menu = 'onClick="HapusMenu(' + ParamHapusMenu + ');"';
                    data = '<button type="button" class="btn btn-primary" ' + aksi_set_menu + '><i class="fa fa-pencil-square-o"></i> Edit</button>&nbsp;&nbsp;<button type="button" class="btn btn-danger" ' + aksi_hapus_menu + '><i class="fa fa-trash"></i> Hapus</button>';
                    return data;
                }
            },
        ],
        "bDestroy": true
    });

    TabelMenuSetting.columns().every(function () {
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
}


//ComboGetAppl(function (HtmlCombo) {
   
//    $("select[name='IdAppl']", "#FormTabelMenuAplikasi").empty();
//    $("select[name='IdAppl']", "#FormTabelMenuAplikasi").append(HtmlCombo);
//});

// trigger onchange
$("select[name='IdAppl']").change(function () {
    let idApplChoice = this.value;
    ComboGetParentMenu(function (HtmlCombo) {
        $("select[name='IdApplTaskParent']").empty();
        $("select[name='IdApplTaskParent']").append(HtmlCombo);
    }, 0, idApplChoice);

    if (idApplChoice != null || idApplChoice != "") {
        $("#DivBtnTambah").removeAttr("style");

        $("#ImgSearchTabel").css("display", "none");

        $("#TabelMenuSetting").removeAttr("style");
        $(".table-responsive").removeAttr("style");
        TabelMenuSetting(this.value);
    } else {
        $("#DivBtnTambah").css("display", "none");

        $("#TabelMenuSetting").css("display", "none");
        $(".table-responsive").css("display", "none");
        $("#ImgSearchTabel").removeAttr("style");
        $("#ImgSearch").removeAttr("style");
        $("#FormMenu").css("display", "none");
    }

});






function ComboGetParentMenu(handleData, IdApplTaskParent, IdAppl) {
   
    var Url = urls.GetApplParentMenu + "?IdApp=" + IdAppl;
    console.log(Url);
    $.ajax({
        url: Url,
        type: "GET",
        dataType: "json",
        beforeSend: function (beforesend) {
            ProgressBar("wait");
        },
        success: function (responsesuccess) {
            console.log(responsesuccess);
            ProgressBar("success");
            if (responsesuccess.IsSuccess == true) {
                console.log(responsesuccess);
                var HtmlCombo = "";
                var JmlData = (responsesuccess.Data).length;
                HtmlCombo += "<option value=''>- Pilih Salah Satu -</option>";
                for (var idata = 0; idata < JmlData; idata++) {
                    //if (responsesuccess.Data[idata].IdApplTaskParent == null) {
                        if (responsesuccess.Data[idata].IdApplTask == IdApplTaskParent) {
                            HtmlCombo += "<option value='" + responsesuccess.Data[idata].IdApplTask + "' selected>" + responsesuccess.Data[idata].ApplTaskName + "</option>";
                        } else {
                            HtmlCombo += "<option value='" + responsesuccess.Data[idata].IdApplTask + "'>" + responsesuccess.Data[idata].ApplTaskName + "</option>";
                        }
                    //}
                }

                handleData(HtmlCombo);
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

function TambahMenu() {
    $("#FormMenu").trigger("reset").css("display", "block");
    $("#ImgSearch").css("display", "none");

    var formObj = $('#FormTabelMenuAplikasi').serializeObject();
    $("input[name='IdAppl']", "#FormMenu").val(formObj.IdAppl);
    $("input[name='ApplName']", "#FormMenu").val($("select[name='IdAppl'] option:selected", "#FormTabelMenuAplikasi").text());

    $("#HeaderBody").html("Form Tambah Menu");

    ComboGetParentMenu(function (HtmlCombo) {
        $("select[name='IdApplTaskParent']").empty();
        $("select[name='IdApplTaskParent']").append(HtmlCombo);
    }, 0, formObj.IdAppl);

    $("#BtnSave").attr("onClick", "save_menu('TambahData')");
}


function save_menu(Tipe) {
    var formObj = $('#FormMenu').serializeObject();

    var IdApplTask = 0;
    var IdApplTaskParent = 0;
    var IdAppl = 0;
    IdApplTask = parseInt(formObj.IdApplTask);
    if (IdApplTask == "" || IdApplTask == "NaN") { IdApplTask = null; }
    IdApplTaskParent = parseInt(formObj.IdApplTaskParent);
    if (IdApplTaskParent == "" || IdApplTaskParent == "NaN") { IdApplTaskParent = null; }
    IdAppl = parseInt(formObj.IdAppl);
    if (IdAppl == "" || IdAppl == "NaN") { IdAppl = null; }

    if (Tipe == "EditData") {
        var TextHeadSwal = "Merubah Menu";
        var Url = urls.EditApplTask
        var JsonData = {
            "IdApplTask": parseInt(IdApplTask),
            "ApplTaskName": formObj.ApplTaskName,
            "ControllerName": formObj.ControllerName,
            "ActionName": formObj.ActionName,
            "Description": formObj.Description,
            "IconName": formObj.IconName
        };
    } else if (Tipe == "TambahData") {
        var TextHeadSwal = "Menambah Menu";
        var Url = urls.AddApplTask
        var JsonData = {
            "IdApplTaskParent": parseInt(IdApplTaskParent),
            "ApplTaskName": formObj.ApplTaskName,
            "IdAppl": IdAppl,
            "ControllerName": formObj.ControllerName,
            "ActionName": formObj.ActionName,
            "Description": formObj.Description,
            "IconName": formObj.IconName
        };
    }

    $.ajax({
        url: Url,
        method: "POST",
        data: JsonData,
        dataType: "json",
        beforeSend: function (before) {
            ProgressBar("wait");
        },
        success: function (responsesave) {
            ProgressBar("success");
            if (responsesave.IsSuccess == true) {
                TambahMenu();
                $("#TabelMenuSetting").DataTable().ajax.reload();
                swal({ title: 'Berhasil ' + TextHeadSwal, text: "", confirmButtonClass: 'btn-success text-white', confirmButtonText: 'Oke, Mengerti', type: 'success' });
            } else if (responsesave.IsSuccess == false) {
                swal({ title: 'Gagal ' + TextHeadSwal, text: responsesave.ReturnMessage, confirmButtonClass: 'btn-danger text-white', confirmButtonText: 'Oke, Mengerti', type: 'error' });
            }
        }, error: function (responserror, a, e) {
            ProgressBar("success");
            swal({ title: 'Error :(', text: JSON.stringify(responserror) + " : " + e, confirmButtonClass: 'btn-danger text-white', confirmButtonText: 'Oke, Mengerti', type: 'error' });
        }
    });
}

function EditMenu(IdAppl, IdApplTask, IdApplTaskParent, ApplTaskName, IdAppl, ControllerName, ActionName, Description, IconName) {
    $("#FormMenu").trigger("reset").css("display", "block");
    $("#ImgSearch").css("display", "none");

    $("#HeaderBody").html("Form Edit Menu");

    var formObj = $('#FormTabelMenuAplikasi').serializeObject();
    ComboGetParentMenu(function (HtmlCombo) {
        $("select[name='IdApplTaskParent']").html(HtmlCombo);
    }, IdApplTaskParent, formObj.IdAppl);

    $("input[name='IdAppl']", "#FormMenu").val(IdAppl);
    $("input[name='ApplName']", "#FormMenu").val($("select[name='IdAppl'] option:selected", "#FormTabelMenuAplikasi").text());

    $("input[name='IdApplTask']").val(IdApplTask);
    $("input[name='ApplTaskName']").val(ApplTaskName);
    $("input[name='ControllerName']").val(ControllerName);
    $("input[name='ActionName']").val(ActionName);
    $("input[name='IconName']").val(IconName);
    $("textarea[name='Description']").val(Description);
    $("#BtnSave").attr("onClick", "save_menu('EditData')");
}

function HapusMenu(IdApplTask, ApplTaskName) {
    // var Url = base_url + "/ApplTasks/DeleteApplTask?IdApplTask=" + parseInt(IdApplTask);
    var Url = urls.DeleteApplTask + "?IdApplTask=" + parseInt(IdApplTask);
    swal({
        title: "Apakah Anda Yakin ?",
        text: "Anda akan menghapus menu " + ApplTaskName,
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Ya, Saya Yakin!",
        cancelButtonClass: "btn-success",
        cancelButtonText: "Tidak, Batalkan!",
        closeOnConfirm: false
    },
        function () {
            $.ajax({
                url: Url,
                method: "POST",
                dataType: "json",
                headers: {
                    "Content-Type": "application/json"
                },
                beforeSend: function (before) {
                    ProgressBar("wait");
                },
                success: function (responsesuccess) {
                    ProgressBar("success");
                    if (responsesuccess.IsSuccess == true) {
                        $("#TabelMenuSetting").DataTable().ajax.reload();
                        swal({ title: 'Berhasil', text: "", confirmButtonClass: 'btn-success text-white', confirmButtonText: 'Oke, Mengerti', type: 'success' });
                    } else if (responsesuccess.IsSuccess == false) {
                        swal({ title: 'Gagal', text: responsesuccess.ReturnMessage, confirmButtonClass: 'btn-danger text-white', confirmButtonText: 'Oke, Mengerti', type: 'error' });
                    }
                }, error: function (responserror, a, e) {
                    ProgressBar("success");
                    swal({ title: 'Error :(', text: JSON.stringify(responserror) + " : " + e, confirmButtonClass: 'btn-danger text-white', confirmButtonText: 'Oke, Mengerti', type: 'error' });
                }
            });
        });
}

