
$(document).ready(function () {

    GetSubsidiary(null, "");
})


function SaveCustomer(action) {
    var formObj = $('#FormCreateCustomer').serializeObject();

    if (action == "Edit") {
        var Url = base_url + "/customer/EditCustomer";
    } else {

        var Url = base_url + "/customer/AddCustomer";
    }
    var Roles = [];
    
    var jsonData = JSON.stringify({
        "IdCustomer": formObj.IdCustomer,
        "CutomerName": formObj.CutomerName,
        "Nib": formObj.Nib,
        "Npwp": formObj.Npwp,
        "Phone": formObj.Phone,
        "Pic": formObj.Pic,
        "Email": formObj.Email,
        "PicName": formObj.PicName,
        "PicPhone": formObj.PicPhone,
        "PicEmail": formObj.PicEmail,
        "IdSubsidiaryCompany": formObj.IdSubsidiaryCompany,
        "Address": formObj.Address
    });
    console.log(jsonData);
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

                swal({
                    title: 'Customer Berhasil' + action,
                    text: "",
                    confirmButtonClass: 'btn-success text-white',
                    confirmButtonText: 'Oke, Mengerti',
                    type: 'success'
                });
                var delayInMilliseconds = 2000; //2 second

                setTimeout(function () {
                    //code to be executed after 2 second
                    window.location.href = base_url + "/customer/index";
                }, delayInMilliseconds);
            } else if (responsesave.IsSuccess == false) {
                swal({
                    title: 'Factory Gagal ' + action,
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
                title: 'Add Factory Gagal ',
                text: errorresponse,
                confirmButtonClass: 'btn-danger text-white',
                confirmButtonText: 'Oke, Mengerti',
                type: 'error'
            });
        }
    });
}

function GetSubsidiary(IdRole, typeUser) {
    $.ajax({
        url: base_url + "/Customer/GetSubsidiarys",
        type: "GET",
        dataType: "json",
        success: function (responseload) {
            if (responseload.IsSuccess == true) {
                var StrRole = "";
                StrRole += "<option value=''>- Pilih Salah Satu -</option>";
                var JmlRole = (responseload.Data).length;
                var irole = 0;
                for (irole; irole < JmlRole; irole++) {
                    if (responseload.Data[irole].IdSubsidiaryCompany == IdRole) {
                        StrRole += "<option value='" + responseload.Data[irole].IdSubsidiaryCompany + "' selected>" + responseload.Data[irole].Name + "</option>";
                    } else {
                        StrRole += "<option value='" + responseload.Data[irole].IdSubsidiaryCompany + "'>" + responseload.Data[irole].Name + "</option>";
                    }

                }

                $("select[name='IdSubsidiaryCompany']").html(StrRole);
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