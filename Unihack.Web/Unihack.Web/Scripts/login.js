function login() {
	debugger;
	EasyLoading.show();
	var email = $('#email').val();
	var password = $('#pass').val();
	var formData = {
		"Email": email,
		"Password": password,
	};
	if (email != "" && password != "") {
		AjaxHelper.post(
			'Account/Login',
			formData,
			function (response) {
				if (response == 1) {
					EasyLoading.hide();
					window.location = "http://localhost:63063/Home/Index"
					$('.#email').val("");
					$('#pass').val("");
				}
				else{
					EasyLoading.hide();
					toastr.error("An error occured");
				}
			},
			function () {
				EasyLoading.hide();
				toastr.error("An error occured");
			}
		)
	}
}

