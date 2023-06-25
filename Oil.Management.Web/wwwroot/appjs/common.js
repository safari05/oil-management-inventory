var base_url = location.protocol + "//" + document.location.host;


jQuery.fn.serializeObject = function () {
    var arrayData, objectData;
    arrayData = this.serializeArray();
    objectData = {};

    $.each(arrayData, function () {
        var value;

        if (this.value != null) {
            value = this.value;
        } else {
            value = '';
        }

        if (objectData[this.name] != null) {
            if (!objectData[this.name].push) {
                objectData[this.name] = [objectData[this.name]];
            }

            objectData[this.name].push(value);
        } else {
            objectData[this.name] = value;
        }
    });

    return objectData;
};

function GlobalAjax(Url) {
  var ResultArray = $.ajax({
    url: Url,
    type: "GET",
    dataType: "json",
    async: false,
  });

  var result = ResultArray.responseJSON; //JSON.stringify
  if (result.IsSuccess == false) {
    iziToast.error({
      title: "Gagal Menampilkan Data",
      message: result.ReturnMessage,
      position: "topRight",
    });
  } else {
    return result.Data;
  }
}

SetDatepicker();

function DestroyDatepicker() {
    $('[data-toggle="datepicker"]').datepicker("destroy");
}
function SetDatepicker() {
  $('[data-toggle="datepicker"]').datepicker({
    format: "dd-mm-yyyy",
    pick: function (e) {
      var date = e.date.getDate();
      jQuery(this).attr("value", date);
    },
  });

  $('[data-toggle="datepicker-maxtoday"]').datepicker({
    format: "dd-mm-yyyy",
    startDate: "01-01-0001",
    endDate: GetCurrentDate("TanggalOnly"),
  });
  $('[data-toggle="datepicker-maxtoday-7"]').datepicker({
    format: "dd-mm-yyyy",
    startDate: "01-01-0001",
    endDate: GetCurrentDate("TanggalOnly") + 7,
  });
  $(".clockpickers").clockpicker({
    donetext: "Done",
  });
}

function GetCurrentDate(Tipe) {
  if (Tipe == "TanggalOnly") {
    var ResponseCurrDate = GlobalAjax(base_url + "/Commons/GetCurrentDate");
    return ResponseCurrDate;
  }
}

function SetFullName(FirstName, MiddleName, LastName) {
    var satu = FirstName == null ? '' : FirstName;
    var dua = MiddleName == null ? '' : MiddleName;
    var tiga = LastName == null ? '' : LastName;
    if (FirstName != null) satu += ' ';
    if (MiddleName != null) dua += ' ';
    if (LastName != null) tiga += ' ';
    return satu + dua + tiga;
}

cekQuotes = (param1) => {
    let regex = /[^\w\s]/gi;
    let retChar;
    if (regex.test(param1) == true) {
        retChar = param1.replace("'", "\\'");
    } else {
        retChar = param1;
    }
    return retChar;
}


function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function indoDate(Date) {
    var SplitTanggal = Date.split("-");
    var Hari = SplitTanggal[0];
    var Bulan = SplitTanggal[1];
    var Tahun = SplitTanggal[2];

    var ArrayBulan = ["Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember"];
    if (Bulan < 10) {
        Bulan = Bulan.replace('0', '');
    }

    return Hari + " " + ArrayBulan[Bulan - 1] + " " + Tahun;
}


// function handle firts char to capital
function handleInput(e) {
    var textBox = event.target;
    var start = textBox.selectionStart;
    var end = textBox.selectionEnd;
    textBox.value = textBox.value.charAt(0).toUpperCase() + textBox.value.slice(1);
    textBox.setSelectionRange(start, end);
}

// function validasi only character
function isCharacter(event) {
    var inputValue = event.charCode;
    if (!(inputValue >= 65 && inputValue <= 123) && (inputValue != 32 && inputValue != 0)) {
        event.preventDefault();
    }

}
