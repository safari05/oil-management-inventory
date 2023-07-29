$('#TabelManajemenUser tfoot th').each(function () {
    var title = $(this).text();
    $(this).html('<input type="text" class="form-control" placeholder="Cari ' + title + '" />');
});
var TabelManajemenUser = $('#TabelManajemenUser').DataTable({
    "paging": true,
    "searching": true,
    "ordering": false,
    "info": true,
    "pageLength": 10,
    "lengthChange": true,
    "scrollX": true,
    "processing": true,
    "ajax": {
        "url": base_url + "/User/GetUsers",
        "method": 'GET',
        "beforeSend": function (xhr) { },
        "dataSrc": function (json) {
            if (json.Data == null) {
                swal({
                    title: 'Gagal Menampilkan Data User',
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
    "columns": [
        {
            "data": "UserName"
        },{
            "data": "Email"
        },
        
        {
            "render": function (data, type, full, meta) {
                var data = SetFullName(full.FirstName, full.MiddleName, full.LastName);
                return data;
            }
        },
        {
            "render": function (data, type, full, meta) {
                var Data = "";
                if (full.StrStatus == "Aktif") {
                    Data += '<span style="color:green;font-weight:bold;cursor:pointer;"><i class="fa fa-check"></i> ' + full.StrStatus + '</span>';
                } else {
                    Data += '<span style="color:red;font-weight:bold;cursor:pointer;"><i class="fa fa-times"></i> ' + full.StrStatus + '</span>';
                }
                return Data;
            }
        },
        {
            "data": "Roles[1].RoleName"
        },
        {
            "data": "IdUser",
            "render": function (data, type, full, meta) {
                var Data = "";
                var ParamStatus = "SetStatus('" + full.IdUser + "','" + full.FirstName + "','" + full.StrStatus + "')";
                Data += '<button type="button" onClick="EditUser(' + full.IdUser + ')" class="btn btn-primary btn-sm"><i class="fa fa-pencil-square-o"></i> Edit</button>&nbsp;&nbsp;';
                Data += '&nbsp;&nbsp;&nbsp;';
                if (full.StrStatus == "Aktif") {
                    Data += '<button type="button" class="btn btn-danger btn-sm" onClick="' + ParamStatus + '"><i class="fa fa-times"></i> Deaktivasi</button>';
                } else {
                    Data += '<button type="button" class="btn btn-success btn-sm" onClick="' + ParamStatus + '"><i class="fa fa-check"></i> Aktivasi</button>';
                }
                data = Data;
                return data;
            }
        }
        
    ],
    "bDestroy": true
});
TabelManajemenUser.columns().every(function () {
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
//////// START SECTION TABEL USER ///////////

function AddUser() {
    $('#FormUser')[0].reset();
    $("#TitleFormUser").html("Tambah Pengguna")
    $('#ModalFormUser').modal("show");
    GetRoles(null);
    $("#BtnSaveUser").attr("onClick", "SaveUser('Tambah');");
    $("#DivPassword", "#FormUser").show();

    $("select[name='Roles']").removeAttr("disabled");
    $("input[name='Username']").removeAttr("readonly");
    $("input[name='IdUser']", "#FormUser").val(0);
}

function EditUser(IdUserEdit) {
    $('#FormUser')[0].reset();
    $("#TitleFormUser").html("Edit Pengguna");
    $('#ModalFormUser').modal("show");
    $("#BtnSaveUser").attr("onClick", "SaveUser('Edit');");
    $("#DivPassword", "#FormUser").css("display", "none");
    $("input[name='Username']", "#FormUser").attr("readonly");
    $("#DivPassword", "#FormUser").hide();
    $.ajax({
        url: base_url + "/Users/GetUser?IdUser=" + parseInt(IdUserEdit),
        type: "GET",
        dataType: "json",
        beforeSend: function (before) {
            ProgressBar("wait");
        },
        success: function (responseload) {
            ProgressBar("success");
            $("input[name='IdUser']", "#FormUser").val(responseload.Data.IdUser);
            $("input[name='Username']", "#FormUser").val(responseload.Data.Username);
            $("input[name='Email']", "#FormUser").val(responseload.Data.Email);
            $("input[name='FirstName']", "#FormUser").val(responseload.Data.FirstName);
            $("input[name='MiddleName']", "#FormUser").val(responseload.Data.MiddleName);
            $("input[name='LastName']", "#FormUser").val(responseload.Data.LastName);
            $("textarea[name='Address']", "#FormUser").val(responseload.Data.Address);
            $("input[name='PhoneNumber']", "#FormUser").val(responseload.Data.PhoneNumber);
            $("input[name='MobileNumber']", "#FormUser").val(responseload.Data.MobileNumber);
            $("input[name='NoLisensi']", "#FormUser").val(responseload.Data.NoLisensi);
            if (jQuery.isEmptyObject(responseload.Data.Roles)) {
                GetRoles(null);
            } else {
                var JumlahRole = (responseload.Data.Roles).length;
                var irole = 0;
                for (irole; irole < JumlahRole; irole++) {
                    GetRoles(responseload.Data.Roles[irole].IdRole);
                }
            }
            $("input[name='Username']").attr("readonly", true);
        },
        error: function (errorresponse) {
            swal({
                title: 'Gagal Menampilkan Data Pengguna',
                text: errorresponse,
                confirmButtonClass: 'btn-danger text-white',
                confirmButtonText: 'Oke, Mengerti',
                type: 'error'
            });
        }
    });
}

function SetStatus(IdUser, FullName, StrStatus) {
    if (StrStatus == "Aktif") {
        swal({
            title: "Apakah Anda Yakin ?",
            text: "Anda akan deaktivasi user " + FullName,
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-success",
            confirmButtonText: "Ya, Saya Yakin!",
            cancelButtonClass: "btn-danger",
            cancelButtonText: "Tidak, Batalkan!",
            closeOnConfirm: false
        },
            function () {
                var Url = base_url + "/User/SetUserInActive?SetIdUser=" + IdUser;
                $.ajax({
                    url: Url,
                    method: "GET",
                    dataType: "json",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    beforeSend: function (before) {
                        ProgressBar("wait");
                    },
                    success: function (res) {
                        ProgressBar("success");
                        if (res.IsSuccess == true) {
                            $('#TabelManajemenUser').DataTable().ajax.reload();
                            swal({
                                title: 'Berhasil',
                                text: "Berhasil DeAktivasi User",
                                confirmButtonClass: 'btn-success text-white',
                                confirmButtonText: 'Oke, Mengerti',
                                type: 'success'
                            });
                        } else if (res.IsSuccess == false) {
                            swal({
                                title: 'Gagal DeAktivasi User',
                                text: res.ReturnMessage,
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
    } else {
        swal({
            title: "Apakah Anda Yakin ?",
            text: "Anda akan aktivasi user " + FullName,
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-success",
            confirmButtonText: "Ya, Saya Yakin!",
            cancelButtonClass: "btn-danger",
            cancelButtonText: "Tidak, Batalkan!",
            closeOnConfirm: false
        },
            function () {
                var Url = base_url + "/User/SetUserActive?SetIdUser=" + IdUser;
                $.ajax({
                    url: Url,
                    method: "GET",
                    dataType: "json",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    beforeSend: function (before) {
                        ProgressBar("wait");
                    },
                    success: function (res) {
                        ProgressBar("success");
                        if (res.IsSuccess == true) {
                            $('#TabelManajemenUser').DataTable().ajax.reload();
                            swal({
                                title: 'Berhasil',
                                text: "Berhasil Aktivasi User",
                                confirmButtonClass: 'btn-success text-white',
                                confirmButtonText: 'Oke, Mengerti',
                                type: 'success'
                            });
                        } else if (res.IsSuccess == false) {
                            swal({
                                title: 'Gagal Aktivasi User',
                                text: res.ReturnMessage,
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

}

function SaveUser(Tipe) {
    var formObj = $('#FormUser').serializeObject();
    var Roles = [];
    $("select[name='Roles'] option:selected").each(function (index, item) {
        var Row = {};
        Row.IdRole = parseInt($(item).val());
        Row.RoleName = $(item).text();
        Roles.push(Row);
    });

    var JsonData = JSON.stringify({
        'IdUser': parseInt(formObj.IdUser),
        'Username': formObj.Username,
        'Password': formObj.Password,
        'Email': formObj.Email,
        'FirstName': formObj.FirstName,
        'MiddleName': formObj.MiddleName,
        'LastName': formObj.LastName,
        'Address': formObj.Address,
        'PhoneNumber': formObj.PhoneNumber,
        'MobileNumber': formObj.MobileNumber,
        'Roles': Roles
    });

    if (Tipe == "Tambah") {
        var Url = base_url + "/Users/AddUser";
    } else {
        var Url = base_url + "/Users/EditUser";
    }
    $.ajax({
        url: Url,
        method: "POST",
        data: JsonData,
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
                $("#ModalFormUser").modal("hide");
                $("#TabelManajemenUser").DataTable().ajax.reload();
                swal({
                    title: 'Berhasil ' + Tipe + ' User',
                    text: "",
                    confirmButtonClass: 'btn-success text-white',
                    confirmButtonText: 'Oke, Mengerti',
                    type: 'success'
                });
            } else if (responsesave.IsSuccess == false) {
                swal({
                    title: 'Gagal ' + Tipe + ' User',
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
                title: 'Gagal ' + Tipe + ' User',
                text: errorresponse,
                confirmButtonClass: 'btn-danger text-white',
                confirmButtonText: 'Oke, Mengerti',
                type: 'error'
            });
        }
    });
}

function GetRoles(IdRole) {
    $.ajax({
        url: base_url + "/Users/GetRoles",
        type: "GET",
        dataType: "json",
        success: function (responseload) {
            if (responseload.IsSuccess == true) {
                var StrRole = "";
                StrRole += "<option value=''>- Pilih Salah Satu -</option>";
                var JmlRole = (responseload.Data).length;
                var irole = 0;
                for (irole; irole < JmlRole; irole++) {
                    if (responseload.Data[irole].IdRole == IdRole) {
                        StrRole += "<option value='" + responseload.Data[irole].IdRole + "' selected>" + responseload.Data[irole].RoleName + "</option>";
                    } else {
                        StrRole += "<option value='" + responseload.Data[irole].IdRole + "'>" + responseload.Data[irole].RoleName + "</option>";
                    }

                }

                $("select[name='Roles']").html(StrRole);
            } else if (responseload.IsSuccess == false) {
                swal({
                    title: 'Gagal Menampilkan Data Role',
                    text: responseload.ReturnMessage,
                    confirmButtonClass: 'btn-danger text-white',
                    confirmButtonText: 'Oke, Mengerti',
                    type: 'error'
                });
            }
        },
        error: function (errorresponse) {
            swal({
                title: 'Gagal Menampilkan Data Role',
                text: errorresponse,
                confirmButtonClass: 'btn-danger text-white',
                confirmButtonText: 'Oke, Mengerti',
                type: 'error'
            });
        }
    });
}