// A $( document ).ready() block.
$(document).ready(function () {
    getFactorys();
    getBusinessUnits();

});


function SaveSubsidiary(action) {
    var formObj = $('#FormCreateSubsidiary').serializeObject();
    if (action == "Edit") {

        var url = base_url + "/subsidiary/EditSubsidiary";
        var jsonData = JSON.stringify({
            "IdBusinessUnit": formObj.IdBusinessUnit,
            "BusinessUnitName": formObj.BusinessUnitName,
        });
    } else {
        var jsonData = JSON.stringify({
            "IdFactory": formObj.IdFactory,
            "IdBusniessUnit": formObj.IdBusinessUnit,
            "Name": formObj.SubsidiaryName,
            "Nib": formObj.Nib,
            "Npwp": formObj.Npwp,
            "Phone": formObj.Phone,
            "Fax": formObj.Fax,
            "Email": formObj.Email,
            "Description": formObj.Description,
            "PicName": formObj.PicName,
            "PicEmail": formObj.PicEmail,
            "PicPhone": formObj.PicPhone
        });
        var url = base_url + "/subsidiary/AddSubsidiary";
    }

    $.ajax({
        url: url,
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
                    window.location.href = base_url + "/Subsidiary/";
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

function getFactorys() {
    $('.js-factory').select2({
        placeholder: "Search Keyword..",
        ajax: {
            type: 'GET',
            url: base_url + "/subsidiary/Factorys",
            data: function (params) {
                var queryParameters = {
                    factoryname: params.term
                }
                return queryParameters;
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.FactoryName,
                            id: item.IdFactory
                        }
                    })
                };
            }
        }
    });
}

function getBusinessUnits() {
    $('.js-business-unit').select2({
        placeholder: "Search Keyword..",
        ajax: {
            type: 'GET',
            url: base_url + "/subsidiary/BusniessUnits",
            data: function (params) {
                var queryParameters = {
                    busniessNama: params.term
                }
                return queryParameters;
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.BusinessUnitName,
                            id: item.IdBusinessUnit
                        }
                    })
                };
            }
        }
    });
}