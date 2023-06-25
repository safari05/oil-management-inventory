/**
 * Sign User Javascript (DKM ADM).
 *
 * Javascript for the Sign module.
 *
 * Contents:
 *
 *
 * @package DKM ADM.
 * @author Eri Safari <safari.erie@gmail.com>
 * @website
 * @version 1.0.0
 * @date 22 Sep 2020
 * @copyright (c) 2020
 */

var Sign = function () {}; // /constructor function prototype
const idAppl = 1;
$(".toggle-password").click(function () {
    $(this).toggleClass("fa-eye fa-eye-slash");
    var input = $($(this).attr("toggle"));
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});

var urls = {
  Login: base_url + "/Sign/Login",
};
!(function ($) {
    "use strict";



  // TODO: Init Function
  (Sign.prototype.init = function () {}),
    // TODO : Common Ajax
    // Info : Common initialize Ajax

    // TODO : Login
    // Info : User akses to controller for login auth own apps
    (Sign.prototype.login = function () {
      let email = document.getElementById("email").value;
      let password = document.getElementById("password").value;
      let formData = new FormData();
      formData.append("IdAppl", idAppl);
      formData.append("UsernameOrEmail", email);
      formData.append("Password", password);
      if (email == "null") {
        alert("Username tidak boleh kosong");
      } else {
        $.ajax({
          url: urls.Login,
          type: "POST",
          datatype: "JSON",
          data: formData,
          processData: false,
            contentType: false,
            beforeSend: function (before) {
                $("button[name='BtnLogin']").text('Sedang memuat, mohon tunggu...').attr("disabled", true);
                ProgressBar("wait");
            },
          success: (response) => {
            console.log(response);
              if (response.IsSuccess) {
                  ProgressBar("success");
                  iziToast.success({
                      title: "Login Berhasil Selamat Datang " + response.Data.FirstName,
                      message: 'Anda akan diarahkan kehalaman utama',
                      position: 'topRight',
                      timeout: 3000
                  });
                  setTimeout(function () {
                      window.location.href = base_url + "/Home";
                      $('#ProsesLoading').hide();
                      $('#BtnLoading').show();
                  }, 3000);
              
            } else {
                  iziToast.error({
                      title: "Login Gagal",
                      message: response.ReturnMessage,
                      position: 'topRight',
                      timeout: 3000
                  });
                  setTimeout(function () {

                      $("button[name='BtnLogin']").text('Login').attr("disabled", false);
                      ProgressBar("success");
                  },3000)
            }
          },
        });
      }
      });

  //init function constructor
  ($.Sign = new Sign()), ($.Sign.Constructor = Sign);
})(window.jQuery),
  (function ($) {
    "use strict";

    //boot function..
    $.Sign.init();

  })(window.jQuery);
