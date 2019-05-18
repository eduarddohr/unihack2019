function register() {
	debugger;
	EasyLoading.show();
	var email = $('#r-email').val();
	var password = $('#r-pass').val();
	var password2 = $('#r-pass2').val();
	var managerId = $('#r-myList').val();
	var name = $('#r-name').val();
	var formData = {
		"Email": email,
		"Password": password,
		"ConfirmPassword": password2,
		"ManagerId": managerId,
		"Name": name
	};
	if (email != "" && password != "" && password2 != "" && name) {
		debugger
		AjaxHelper.post(
			'Account/Register',
			formData,
			function (response) {
				EasyLoading.hide();
				if (response == 1) {
					toastr.success("Your registration was successfull")
					window.location = "http://localhost:63063/Account/Login";
					$('.#r-email').val("");
					$('#r-pass').val("");
					$('#r-pass2').val("");
					$('#r-myList').val();
					$('#r-name').val();
				}
				if (response == -1) {
					toastr.error("An error occured")
					$('#r-email').val("");
					$('#r-pass').val("");
					$('#r-pass2').val("");
					$('#r-myList').val();
					$('#r-name').val();
				}
			},
			function () {
				toastr.error("An error occured")
				EasyLoading.hide();
			}
		)
	}
	else {
		EasyLoading.hide();
		toastr.error("All fields are are required");
	}
}