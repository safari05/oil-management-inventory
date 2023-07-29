
function SaveFactory(action) {
    
    var formObj = $('#FormCreateFactory').serializeObject();
    
    if (action == "Edit") {
        var Url = base_url + "/factory/EditFactory";
    } else {

        var Url = base_url + "/factory/AddFactory";
    }
    var jsonData = JSON.stringify({
        "IdFactory": formObj.IdFactory,
        "FactoryName": formObj.FactoryName,
        "Nib": formObj.Nib,
        "Pic": formObj.Pic,
        "Phone": formObj.Phone,
        "Email": formObj.Email,
        "Address": formObj.Address
        ,
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
                    title: 'Factory Berhasil' + action,
                    text: "",
                    confirmButtonClass: 'btn-success text-white',
                    confirmButtonText: 'Oke, Mengerti',
                    type: 'success'
                });
                var delayInMilliseconds = 2000; //2 second

                setTimeout(function () {
                    //code to be executed after 2 second
                    window.location.href = base_url + "/factory/index";
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
