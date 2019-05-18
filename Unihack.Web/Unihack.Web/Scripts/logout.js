function logout() {
    debugger
    EasyLoading.show();
    AjaxHelper.post(
        "Account/Logout", null,
        function (response) {
            debugger
            window.location = "http://localhost:63063/Account/Login"
        },
        function () {
            debugger
        }
    );
    EasyLoading.hide();

}
