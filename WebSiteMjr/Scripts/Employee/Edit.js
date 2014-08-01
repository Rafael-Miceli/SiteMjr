$(function () {
    var employeeLogin,
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
        employeeLogin = data;

        if (employeeLogin != 'undefined' || employeeLogin != null) {
            alert('Suma com o botão');
        }
    }

    function employeeVerifyLoginCallFailed(textStatus) {
        alert('Error chamada ajax. ' + textStatus);
    }
});