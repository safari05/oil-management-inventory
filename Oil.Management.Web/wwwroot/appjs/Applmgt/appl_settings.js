

var urls = {
    GetAppsettings: base_url + "/Applmgt/ApplSettings",
    GetAppSettingByCode: base_url +"/ApplMgt/GetAppSettingByCode",
    AddAppSetting: base_url +"/ApplMgt/AddApplSetting",
    EditAppSetting: base_url +"/ApplMgt/EditApplSetting",
    DeleteAppSetting: base_url +"/ApplMgt/DeleteAppSetting?",
};


//$('#TabelAplikasiSetting tfoot th').each(function () {
//    var title = $(this).text();
//    $(this).html('<input type="text" class="form-control" placeholder="Cari ' + title + '" />');
//});
var TabelAplikasiSetting = $("#TabelAplikasiSetting").DataTable({
     processing: true,
      serverSide: true,
      filter: true,
      pageLength: 5,
      lengthChange: false,
      scrollX: true,
    ajax: {
      url: urls.GetAppsettings,
      type: "POST",
      datatype: "json",
      },
      columnDefs: [{
          targets: [0],
          width: "30%",
          visible: true,
          searchable: true
      },
      {
          targets: [1],
          width: "30%",
          visible: true,
          searchable: true
      },
      {
          targets: [2],
          width: "30%",
          visible: true,
          searchable: true
      },
      {
          targets: [3],
          width: "15%",
          visible: true,
          searchable: true
      },
      ],
    columns: [
      //{ data: "code", name: "code", autoWidth: true },
        {
            "render": function (data, type, full, meta) {
                var ParamAplikasi = "'" + full.Code + "','" + full.Name + "'";
                var aksi_set_Aplikasi = 'onClick="EditAplikasi(' + ParamAplikasi + ');"';
                var aksi_hapus_aplikasi = 'onClick="HapusAplikasi(' + ParamAplikasi + ');"';
                data = '<button type="button" class="btn btn-primary btn-sm" ' + aksi_set_Aplikasi + '><i class="fa fa-edit"></i></button>&nbsp;&nbsp;<button type="button" class="btn btn-danger btn-sm" ' + aksi_hapus_aplikasi + '><i class="fa fa-trash"></i> </button>';
                return data;
            }
        },
        {
            "render": (data, type, full, meta) => {
                return `<code>${full.Code}</code>`;
            }
        },
      { data: "Name", name: "Name", autoWidth: true },
        { data: "ValueString", name: "ValueString", autoWidth: true },
    ],
  });
//TabelAplikasiSetting.columns().every(function () {
//    var that = this;

//    $('input', this.footer()).on('keyup change clear', function () {
//        if (that.search() !== this.value) {
//            that
//                .search(this.value)
//                .draw();
//        }
//    });
//});
//$(".dataTables_filter").css("display", "none");


$(document).ready(function () {
  
});

// show form appliakasi create
function TambahAplikasi()
{
  $("#FormAplikasi").trigger("reset").css("display", "block");
  $("#ImgSearch").css("display", "none");

  $("#HeaderBody").html("Tambah Aplikasi");
  $("#BtnSave").attr("onClick", "SaveAplikasi('TambahData')");
  $("input[name='Code']").attr("readonly", false);
};

// onchange radio button
$("input[name='ValueType'][type='radio']").change(function () {
    if (this.value == 1) {
        $("#ValueType").attr("placeholder", "Ketikkan DateTime").attr({
            "name": "ValueDate",
            "data-toggle": "datepicker",
            "type": "text",
            "readonly": true
        });
        SetDatepicker();
    } else if (this.value == 2) {
        $("#ValueType").attr("placeholder", "Ketikkan Number").attr({
            "name": "ValueNumber",
            "type": "number",
            "readonly": false
        });
    } else if (this.value == 3) {
        $("#ValueType").attr("placeholder", "Ketikkan String").attr({
            "name": "ValueString",
            "type": "text",
            "readonly": false
        }).removeAttr("data-toggle");
    } else if (this.value == 4) {
        $("#ValueType").attr("type", "file").attr({
            "name": "ValueFile",
            "readonly": false
        }).removeAttr("data-toggle");
    }
});


function SaveAplikasi(Tipe) {

    var formObj = $('#FormAplikasi').serializeObject();
    var ValueType = 0;
    var ValueString = null;
    var ValueFile = null;
    if (formObj.ValueType == 1) {
        var ValueDate = formObj.ValueDate;
        var ValueNumber = 0.0;
        ValueType = formObj.ValueType;
        ValueString = "";
    } else if (formObj.ValueType == 2) {
        var ValueDate = "";
        var ValueNumber = formObj.ValueNumber;
        ValueType = formObj.ValueType;
        ValueString = "";
    } else if (formObj.ValueType == 3) {
        var ValueDate = "";
        var ValueNumber = 0.0;
        ValueType = formObj.ValueType;
        ValueString = formObj.ValueString;
    } else if (formObj.ValueType == 4) {
        var ValueDate = "";
        var ValueNumber = 0.0;
        ValueType = 3;
        ValueFile = $('input[type="file"][name="ValueFile"]')[0].files[0];
        // ValueFile = formObj.ValueFile;
    }


    var fd = new FormData();

    if (Tipe == "EditData") {
        var Url = urls.EditAppSetting;
        fd.append('Code', formObj.Code);
        fd.append('Name', formObj.Name);
        fd.append('ValueType', ValueType);
        fd.append('ValueDate', ValueDate);
        fd.append('ValueNumber', parseInt(ValueNumber));
        fd.append('ValueString', ValueString);
        fd.append('FileUpload', ValueFile);

    } else if (Tipe == "TambahData") {
        var Url = urls.AddAppSetting
        fd.append('Code', formObj.Code);
        fd.append('Name', formObj.Name);
        fd.append('ValueType', ValueType);
        fd.append('ValueDate', ValueDate);
        fd.append('ValueNumber', parseInt(ValueNumber));
        fd.append('ValueString', ValueString);
        fd.append('FileUpload', ValueFile);
    }
    $.ajax({
        url: Url,
        method: "POST",
        data: fd,
        contentType: false,
        cache: true,
        processData: false,
        dataType: "json",
        beforeSend: function (before) {
            ProgressBar("wait");
        },
        success: function (responsesave) {
            ProgressBar("success");
            console.log(responsesave)
            if (responsesave.IsSuccess == true) {
                // $('#FormAplikasi')[0].reset();
                $("#TabelAplikasiSetting").DataTable().ajax.reload();
                swal({
                    title: 'Berhasil',
                    text: responsesave.Data,
                    confirmButtonClass: 'btn-success text-white',
                    confirmButtonText: 'Oke, Mengerti',
                    type: 'success'
                });
            } else if (responsesave.IsSuccesss == false) {
                swal({
                    title: 'Gagal',
                    text: responsesave.ReturnMessage,
                    confirmButtonClass: 'btn-danger text-white',
                    confirmButtonText: 'Oke, Mengerti',
                    type: 'error'
                });
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

function EditAplikasi(Code) {
    $("#FormAplikasi").trigger("reset").css("display", "block");
    $("#ImgSearch").css("display", "none");
    $("#HeaderBody").html("Edit Aplikasi");

    $.ajax({
        url: urls.GetAppSettingByCode + '?Code=' + Code,
        type: "GET",
        dataType: "json",
        beforeSend: function (beforesend) {
            ProgressBar("wait");
        },
        success: function (responsesuccess) {
         
            ProgressBar("success");
            if (responsesuccess.IsSuccess == true) {
                $("input[name='Code']").val(responsesuccess.Data.Code).attr("readonly", true);
                $("input[name='Name']").val(responsesuccess.Data.Name);
                if (responsesuccess.Data.ValueType == 1) {
                    $("input[name='ValueType'][value='1']").attr("checked", true);
                    $("#ValueType").attr({
                        "name": "ValueDate",
                        "placeholder": "Ketikkan DateTime",
                        "data-toggle": "datepicker",
                        "readonly": true
                    });
                    $("input[name='ValueDate']").val(responsesuccess.Data.ValueDate);
                    SetDatepicker();
                } else if (responsesuccess.Data.ValueType == 2) {
                    $("input[name='ValueType'][value='2']").attr("checked", true);
                    $("#ValueType").attr({
                        "type": "number",
                        "name": "ValueNumber",
                        "placeholder": "Ketikkan Number",
                        "readonly": false
                    }).removeAttr("data-toggle");
                    $("input[name='ValueNumber']").val(responsesuccess.Data.ValueNumber);
                } else if (responsesuccess.Data.ValueType == 3) {
                    $("input[name='ValueType'][value='3']").attr("checked", true);
                    $("#ValueType").attr({
                        "type": "text",
                        "name": "ValueString",
                        "placeholder": "Ketikkan String",
                        "readonly": false
                    }).removeAttr("data-toggle");
                    $("input[name='ValueString']").val(responsesuccess.Data.ValueString);
                }
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


    $("#BtnSave").attr("onClick", "SaveAplikasi('EditData')");
}


function HapusAplikasi(Code, Name) {
    var Url = urls.DeleteAppSetting + "?Code=" + Code;
    swal({
        title: "Apakah Anda Yakin ?",
        text: "Anda akan menghapus aplikasi " + Name,
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
                beforeSend: function (before) {
                    ProgressBar("wait");
                },
                success: function (responsesuccess) {
                    ProgressBar("success");
                    if (responsesuccess.IsSuccess == true) {
                        $("#TabelAplikasiSetting").DataTable().ajax.reload();
                        swal({
                            title: 'Berhasil',
                            text: "",
                            confirmButtonClass: 'btn-success text-white',
                            confirmButtonText: 'Oke, Mengerti',
                            type: 'success'
                        });
                    } else if (responsesuccess.IsSuccess == false) {
                        swal({
                            title: 'Gagal',
                            text: responsesuccess.ReturnMessage,
                            confirmButtonClass: 'btn-danger text-white',
                            confirmButtonText: 'Oke, Mengerti',
                            type: 'error'
                        });
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
        });
}