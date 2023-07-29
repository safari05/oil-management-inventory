
function SaveBusinessUnit(action) {
    var formObj = $('#FormCreateBusinessUnit').serializeObject();
    if (action == "Edit") {

        var Url = base_url + "/BusinessUnit/EditBusinessUnit";
        var jsonData = JSON.stringify({
            "IdBusinessUnit": formObj.IdBusinessUnit,
            "BusinessUnitName": formObj.BusinessUnitName,
        });
    } else {
        var jsonData = JSON.stringify({
            "BusinessUnitName": formObj.BusinessUnitName
        });
        var Url = base_url + "/BusinessUnit/AddBusinessUnit";
    }

    
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
                console.log(responsesave)
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
                    window.location.href = base_url + "/BusinessUnit/";
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
