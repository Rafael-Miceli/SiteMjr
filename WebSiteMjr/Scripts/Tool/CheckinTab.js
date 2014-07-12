$(function () {
    var employeeCompanyHolderNames,
        companyAreasCall,
        companyAreaNames,
        employeeCompanyHolderNamesCall;

    //Set Ajax Call
    employeeCompanyHolderNamesCall = {
        url: '/Api/HolderApi/GetNotDeletedHoldersName',
        type: 'GET',
        datatype: 'json'
    };
    
    //Make Ajax Call
    $.ajax(employeeCompanyHolderNamesCall)
            .then(employeeCompanyHolderquerySucceeded)
            .fail(queryFailed);
    

    var companyName = '';

    $('#CompanyAreaName').focus(function () {

        companyAreasCall = {
            url: '/Api/CompanyApi/ListCompanyAreas?companyName=' + $('#HolderName').val(),
            type: 'GET',
            datatype: 'json'
        };

        if (companyName != $('#HolderName').val()) {

            companyName = $('#HolderName').val();

            $.ajax(companyAreasCall)
            .then(companyAreasSucceded)
            .fail(queryFailed);
        }
    });


    function companyAreasSucceded(data) {
        companyAreaNames = data;

        $("#CompanyAreaName").autocomplete({
            source: companyAreaNames
        });
    }

    function employeeCompanyHolderquerySucceeded(data) {
        employeeCompanyHolderNames = data;

        $("#HolderName").autocomplete({
            source: employeeCompanyHolderNames
        });
    }
    
    function queryFailed(textStatus) {
        alert('Error chamada ajax. ' + textStatus);
    }


});