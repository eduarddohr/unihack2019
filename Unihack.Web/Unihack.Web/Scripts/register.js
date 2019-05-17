function register() {
	debugger;
	EasyLoading.show();
	var email = $('#r-email').val();
	var password = $('#r-pass').val();
	var password2 = $('#r-pass2').val();
	var formData = {
		"Email": email,
		"Password": password,
		"ConfirmPassword": password2
	};
	if (email != "" && password != "" && password2 != "") {
		debugger
		AjaxHelper.post(
			'Account/Register',
			formData,
			function (response) {
				debugger
				EasyLoading.hide();
				if (response == 1) {
					window.location = "http://localhost:63063/Account/Login";
					$('.#r-email').val("");
					$('#r-pass').val("");
					$('#r-pass2').val("");
				}
				if (response == -1) {
					$('#r-email').val("");
					$('#r-pass').val("");
					$('#r-pass2').val("");
				}
			},
			function () {
				EasyLoading.hide();
			}
		)
	}
}