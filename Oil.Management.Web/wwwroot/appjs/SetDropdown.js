urlDropdown = {
    GetListUser: base_url + "/Pegawais/GetListUser",
    GetStatusPnsNonPns: base_url + "/Pegawais/GetStatusPnsNonPns",
    GetJenisPegawai: base_url + "/Pegawais/GetJenisPegawai",
    GetSumberGaji: base_url + "/Pegawais/GetSumberGaji",
    GetListJabatan: base_url + "/Pegawais/GetListJabatan",
    GetUnits: base_url + "/Units/GetUnits",
    GetUnitsBySch: base_url + "/Units/GetUnitsBySch",
    GetProdiByIdUnit: base_url + "/Units/GetProdiByIdUnit",
    GetAgamas: base_url + "/Schools/GetAgamas",
    GetJenjangPendidikans: base_url + "/Schools/GetJenjangPendidikans",
    GetJenisGolonganPegawai: base_url + "/Schools/GetJenisGolonganPegawai",
    GetPtks: base_url + "/Schools/GetPtks",
    GetSumberGajis: base_url + "/Schools/GetSumberGajis",
    GetStatusPerkawinans: base_url + "/Schools/GetStatusPerkawinans",
    GetDiklat: base_url + "/Schools/GetDiklat",
    GetUnitsSchools: base_url + "/Units/GetUnitsBySch?",
}

/*
 * funct acces api GetUnits
 * @param {Issch == bolean, id == int } Isch == true ? schools : non schools
 * **/
function AsyncGetAccess(urlApi, method) {
    const accessApi = fetch(urlApi,
        {
            method: method,
        })
        .then(response => response.json())
        .then(response => response);
    return accessApi;
}

async function UIDropdownUnits(id) {
    const spinner = document.getElementById("spinner");
    spinner.removeAttribute('hidden');
    // store ui into variabel 
    let htmlCmbUnits = '';
    // temp access api to variabel
    const getUnits = await AsyncGetAccess(urlDropdown.GetUnitsSchools + "IsSch=true", 'Get');

    spinner.setAttribute('hidden', '');
    if (getUnits.IsSuccess) {
        const dataUnits = getUnits.Data;
        htmlCmbUnits += `<option value=""> --Pilih Unit Sekolah </option>`;
        dataUnits.forEach((value, key) => {
            if (value.Id == id) {
                htmlCmbUnits += `<option value=${value.Id} selected> ${value.Nama} </option>`;
            } {
                htmlCmbUnits += `<option value=${value.Id}> ${value.Nama} </option>`;
            }


        })
    } else {
        htmlCmbUnits += `<option value="0"> --Data Tidak ada-- </option>`;
    }
    $('select[name=UnitName]').empty().append(htmlCmbUnits);
}


function buildDropdown(handleData, idDropdown, type) {
    
    var Url;
    if (type == "GetStatusPnsNonPns") {
        Url = urlDropdown.GetStatusPnsNonPns;
    } else if (type == "GetJenisPegawai") {
        Url = urlDropdown.GetJenisPegawai;
    } else if (type == "GetSumberGaji") {
        Url = urlDropdown.GetJenisPegawai;
    } else if (type == "GetListJabatan") {
        Url = urlDropdown.GetListJabatan
    } else if (type == "GetAgamas") {
        Url = urlDropdown.GetAgamas
    } else if (type == "GetJenjangPendidikans") {
        Url = urlDropdown.GetJenjangPendidikans
    } else if (type == "GetJenisGolonganPegawai") {
        Url = urlDropdown.GetJenisGolonganPegawai
    } else if (type == "GetPtks") {
        Url = urlDropdown.GetPtks
    } else if (type == "GetSumberGajis") {
        Url = urlDropdown.GetSumberGaji
    } else if (type == "GetStatusPerkawinans") {
        Url = urlDropdown.GetStatusPerkawinans
    }
    $.ajax({
        url: Url,
        method: "GET",
        beforeSend: function (before) {
            ProgressBar("wait");
        },
        success: function (responsesave) {
            ProgressBar("success");
            if (responsesave.IsSuccess == true) {
                var html_combo = "";
                html_combo = '<option value="">- Pilih Salah Satu -</option>';
                for (var i = 0; i < (responsesave.Data).length; i++) {
                    if (responsesave.Data[i].Id == idDropdown) {
                        html_combo += '<option value="' + responsesave.Data[i].Id + '" selected>' + responsesave.Data[i].Nama + '</option>';
                    } else {
                        html_combo += '<option value="' + responsesave.Data[i].Id + '">' + responsesave.Data[i].Nama + '</option>';
                    }
                }
                handleData(html_combo);
            } else if (responsesave.IsSuccess == false) {
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

function buildDropdownUser(handleData, IdUser) {
    var Url = urlDropdown.GetListUser
    $.ajax({
        url: Url,
        method: "GET",
        beforeSend: function (before) {
            ProgressBar("wait");
        },
        success: function (responsesave) {
            ProgressBar("success");
            if (responsesave.IsSuccess == true) {
                var html_combo = "";
                html_combo = '<option value="">- Pilih Salah Satu -</option>';
                for (var i = 0; i < (responsesave.Data).length; i++) {
                    if (responsesave.Data[i].IdUser == IdUser) {
                        html_combo += '<option value="' + responsesave.Data[i].IdUser + '" selected>' + SetFullName(responsesave.Data[i].FirstName, responsesave.Data[i].MiddleName, responsesave.Data[i].LastName) + '</option>';
                    } else {
                        html_combo += '<option value="' + responsesave.Data[i].IdUser + '">' + SetFullName(responsesave.Data[i].FirstName, responsesave.Data[i].MiddleName, responsesave.Data[i].LastName) + '</option>';
                    }
                }
                handleData(html_combo);
            } else if (responsesave.IsSuccess == false) {
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

function buildDropdownUnit(handleData, IdUnit) {
    var Url = urlDropdown.GetUnits
    $.ajax({
        url: Url,
        method: "GET",
        beforeSend: function (before) {
            ProgressBar("wait");
        },
        success: function (responsesave) {
            ProgressBar("success");
            if (responsesave.IsSuccess == true) {
                var html_combo = "";
                html_combo = '<option value="-1">- Pilih Salah Satu -</option>';
                for (var i = 0; i < (responsesave.Data).length; i++) {
                    if (responsesave.Data[i].IdUnit == IdUnit) {
                        html_combo += '<option value="' + responsesave.Data[i].IdUnit + '" selected>' + responsesave.Data[i].Nama + '</option>';
                    } else {
                        html_combo += '<option value="' + responsesave.Data[i].IdUnit + '">' + responsesave.Data[i].Nama + '</option>';
                    }
                }
                handleData(html_combo);
            } else if (responsesave.IsSuccess == false) {
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

function buildDropdownIschs(handleData, IdUnit, IsSch) {
    var Url = urlDropdown.GetUnitsBySch + "?IsSch="+IsSch;
    $.ajax({
        url: Url,
        method: "GET",
        beforeSend: function (before) {
            ProgressBar("wait");
        },
        success: function (responsesave) {
            ProgressBar("success");
            if (responsesave.IsSuccess == true) {
                var html_combo = "";
                html_combo = '<option value="-1">- Pilih Salah Satu -</option>';

                for (var i = 0; i < responsesave.Data.length; i++) {
                    
                    if (responsesave.Data[i].Id == IdUnit) {
                        
                        html_combo += '<option value="' + responsesave.Data[i].IdUnit + '" selected>' + responsesave.Data[i].Nama + '</option>';
                    } else {
                        html_combo += '<option value="' + responsesave.Data[i].IdUnit + '">' + responsesave.Data[i].Nama + '</option>';
                    }
                }
                handleData(html_combo);
            } else if (responsesave.IsSuccess == false) {
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



function buildDropdownProdi(handleData, IdProdi, IdUnit) {
    var Url = urlDropdown.GetProdiByIdUnit + "?IdUnit=" + IdUnit;
    $.ajax({
        url: Url,
        method: "GET",
        beforeSend: function (before) {
            ProgressBar("wait");
        },
        success: function (responsesave) {
            ProgressBar("success");
            if (responsesave.IsSuccess == true) {
                $('#IdProdiUnit').css("display", "");
                var html_combo = "";
                html_combo = '<option value="-1">- Pilih Salah Satu -</option>';
                for (var i = 0; i < (responsesave.Data).length; i++) {
                    if (responsesave.Data[i].IdProdi == IdProdi) {
                        html_combo += '<option value="' + responsesave.Data[i].Id + '" selected>' + responsesave.Data[i].Nama + '</option>';
                    } else {
                        html_combo += '<option value="' + responsesave.Data[i].Id + '">' + responsesave.Data[i].Nama + '</option>';
                    }
                }
                handleData(html_combo);
            } else if (responsesave.IsSuccess == false) {
                $('#IdProdiUnit').css("display", "none");
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

function buildDropdownDiklat(handleData) {
    var Url = urlDropdown.GetDiklat;
    
    $.ajax({
        url: Url,
        method: "GET",
        beforeSend: function (before) {
            ProgressBar("wait");
        },
        success: function (responsesave) {
            ProgressBar("success");
            if (responsesave.IsSuccess == true) {
               
                var html_combo = "";
                html_combo = '<option value="-1">- Pilih Salah Satu -</option>';
                for (var i = 0; i < (responsesave.Data).length; i++) {
                    html_combo += '<option value="' + responsesave.Data[i].Id + '">' + responsesave.Data[i].Nama + '</option>';
                }
                handleData(html_combo);
            } else if (responsesave.IsSuccess == false) {
                //$('#IdProdiUnit').css("display", "none");
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