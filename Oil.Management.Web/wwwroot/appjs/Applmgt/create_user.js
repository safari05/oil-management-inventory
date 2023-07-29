

$(document).ready(function () {
   
    GetRoles(null, "");
    $('#DivSubsidiary').hide();
})

function GetRoleChange() {
    var role = document.getElementById("roles").value;
    if (role == 2) {
        GetSubsidiary();
        $('#DivSubsidiary').show();
    } else {
        $('#DivSubsidiary').hide();
    }
}

function GetSubsidiary() {
    $.ajax({
        url: base_url + "/Users/GetSubsidiarys",
        type: "GET",
        dataType: "json",
        success: function (responseload) {
            console.log(responseload)
            if (responseload.IsSuccess == true) {
                var StrSubsidiary= "";
                StrSubsidiary += "<option value='0'>- Pilih Salah Satu -</option>";
                var Jml = (responseload.Data).length;
                var irole = 0;
                for (irole; irole < Jml; irole++) {
                    StrSubsidiary += "<option value='" + responseload.Data[irole].IdSubsidiaryCompany+ "'>" + responseload.Data[irole].Name+ "</option>";

                }

                $("select[name='IdSubsidiaryCompany']").html(StrSubsidiary);
            } else if (responseload.IsSuccess == false) {
                swal({
                    title: 'Gagal Menampilkan Data Subsidiary',
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


function GetRoles(IdRole, typeUser) {
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

function SaveUser(Tipe) {
    var formObj = $('#FormUser').serializeObject();
    var Roles = [];
    $("select[name='Roles'] option:selected").each(function (index, item) {
        var Row = {};
        Row.IdRole = parseInt($(item).val());
        Row.RoleName = $(item).text();
        Roles.push(Row);
    });

var jsonData = JSON.stringify({
    'IdUser': parseInt(formObj.IdUser),
    'UserName': formObj.Username,
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
    console.log(jsonData)

if (Tipe == "Add") {
    var Url = base_url + "/User/AddUser";
} else {
    var Url = base_url + "/Users/EditUser";
}
$.ajax({
    url: Url,
    method: "POST",
    data: jsonData,
    contentType: "application/json",
    beforeSend: function (before) {
        ProgressBar("wait");
    },
    success: function (responsesave) {
        ProgressBar("success");
        if (responsesave.IsSuccess == true) {
            
            swal({
                title: 'Berhasil ' + Tipe + ' User',
                text: "",
                confirmButtonClass: 'btn-success text-white',
                confirmButtonText: 'Oke, Mengerti',
                type: 'success'
            });
            var delayInMilliseconds = 2000; //2 second
            setTimeout(function () {
                //code to be executed after 2 second
                window.location.href = base_url + "/user";
            }, delayInMilliseconds);
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