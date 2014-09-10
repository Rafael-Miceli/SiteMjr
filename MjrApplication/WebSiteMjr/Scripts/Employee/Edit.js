$(function () {
    var doesEmployeeHasLogin,
        employeeVerifyLoginCall;

    //Set Ajax call
    employeeVerifyLoginCall = {
        url: '/Api/EmployeeApi/GetEmployeeUser?employeeId=' + $('#Id').val(),
        type: 'GET',
        datatype: 'json'
    };

    //Make Ajax call

    $.ajax(employeeVerifyLoginCall)
        .then(employeeVerifyLoginCallSucceded)
        .fail(employeeVerifyLoginCallFailed);



    function employeeVerifyLoginCallSucceded(data) {
        doesEmployeeHasLogin = data;

        if (doesEmployeeHasLogin) {
            $('#btnCriarLogin').css('visibility', 'hidden');
        }
    }

    function employeeVerifyLoginCallFailed(textStatus) {
        alert('Error chamada ajax. ' + textStatus);
    }
});