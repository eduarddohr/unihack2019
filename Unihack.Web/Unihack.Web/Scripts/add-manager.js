function addManager(){
    debugger;
    EasyLoading.show();
    var email = $('#m-email').val();
    var password = $('#m-pass').val();
    var password2 = $('#m-pass2').val();
    var name = $('#m-name').val();
    var zone = $('#zone').val();
    var formData = {
        "Email": email,
        "Password": password,
        "ConfirmPassword": password2,
        "Name": name,
        "Zone":zone
    };
    if (email != "" && password != "" && password2 != "" && name && zone != "") {
        debugger
        AjaxHelper.post(
            "Account/AddManager",
            formData,
            function (response) {
                debugger
                EasyLoading.hide();
                if (response == 1) {
                    toastr.success("Manager added successfully")
                    window.location = "http://localhost:63063/Home/Index";
                    $('.#m-email').val("");
                    $('#m-pass').val("");
                    $('#m-pass2').val("");
                    $('#zone').val();
                    $('#m-name').val();
                }
            },
            function () {
                debugger

                toastr.error("Unable to add a new manager")
            }
        );
    }
    EasyLoading.hide();
}