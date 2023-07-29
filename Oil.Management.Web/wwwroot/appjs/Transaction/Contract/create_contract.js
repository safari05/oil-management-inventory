
$(document).ready(function () {
    getFactorys();
})

function SaveContract(action) {
    let fileInput = document.getElementById('FileGuarante');
    var formObj = $('#FormCreateContract').serializeObject();
    var formData = new FormData($('#FormCreateContract')[0]);


    formData.append("Id", formObj.Id);
    formData.append("Name", formObj.Name);
    formData.append("StartContract", formObj.StartContract);
    formData.append("EndContract", formObj.EndContract);
    formData.append("FileGuarante", $("#FileGuarantes")[0].files[0]);
    formData.append("PctDomestic", formObj.PctDomestic);
    formData.append("PctExport", formObj.PctExport);
    formData.append("Description", formObj.Description);
    formData.append("Description", formObj.IdFactory);

    if (action == "Edit") {
        var Url = base_url + "/contract/EditContract";
    } else {
        var Url = base_url + "/contract/AddContract";
    }
    console.log(formData)
 

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
                    title: 'Factory Berhasil' + action,
                    text: "",
                    confirmButtonClass: 'btn-success text-white',
                    confirmButtonText: 'Oke, Mengerti',
                    type: 'success'
                });

                var delayInMilliseconds = 2000; //2 second

                setTimeout(function () {
                    //code to be executed after 2 second
                    window.location.href = base_url + "/Contract/";
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