

function SaveCompanyPo(action) {
    var formObj = $('#FormCreatePoCompany').serializeObject();
    var formData = new FormData($('#FormCreatePoCompany')[0]);
    formData.append("IdTrxPo", formObj.IdTrxPo);
    formData.append("CodePo", formObj.CodePo);
    formData.append("PoName", formObj.PoName);
    formData.append("TotalOrder", formObj.TotalOrder);
    formData.append("IdContract", formObj.IdContract);
    formData.append("SatuanVolume", formObj.SatuanVolume);
    formData.append("Status", formObj.Status);
    formData.append("TimeArival", formObj.TimeArival);
    formData.append("Amount", formObj.Amount);
    formData.append("Description", formObj.Description);

    if (action == "Edit") {
        var Url = base_url + "/companyPo/EditCompanyPo";
    } else if (action == "Approve")
    {
        var Url = base_url + "/companyPo/ApproveOrRejectCompanyPo";
    }else {
        var Url = base_url + "/companyPo/AddCompanyPo";

    }


    $.ajax({
        url: Url,
        type: "POST",
        dataType: "json",
        contentType: false,
        data: formData,
        processData: false,
        beforeSend: function (before) {
            ProgressBar("wait");
        },
        success: function (responsesave) {
            ProgressBar("success");
            if (responsesave.IsSuccess == true) {
                console.log(responsesave)
                swal({
                    title: 'Purchase Order Company Berhasil' + action,
                    text: "",
                    confirmButtonClass: 'btn-success text-white',
                    confirmButtonText: 'Oke, Mengerti',
                    type: 'success'
                });

                var delayInMilliseconds = 2000; //2 second

                setTimeout(function () {
                    //code to be executed after 2 second
                    window.location.href = base_url + "/CompanyPo/";
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