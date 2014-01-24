$(function() {
    var employeeCompanyHolderNames,
        toolNames,
        toolNamesCall,
        companyAreasCall,
        companyAreaNames,
        employeeCompanyHolderNamesCall;

    //Set Ajax Call
    employeeCompanyHolderNamesCall = {
        url: '/Api/HolderApi/GetEmployeeCompanyHoldersName',
        type: 'GET',
        datatype: 'json'
    };
    
    toolNamesCall = {
        url: '/Api/ToolApi/GetToolsName',
        type: 'GET',
        datatype: 'json'
    };
    
    companyAreasCall = {
        url: '/Api/CompanyApi/ListCompanyAreas?companyName=' + $('#EmployeeCompanyHolderName').val(),
        type: 'GET',
        datatype: 'json'
    };
    

    //Make Ajax Call
    $.ajax(companyAreasCall)
            .then(companyAreasSucceded)
            .fail(queryFailed);
    
    $.ajax(toolNamesCall)
            .then(toolquerySucceeded)
            .fail(queryFailed);
    
    $('#CompanyAreaName').focus(function () {
        $.ajax(employeeCompanyHolderNamesCall)
            .then(employeeCompanyHolderquerySucceeded)
            .fail(queryFailed);
    });
    

    function companyAreasSucceded(data) {
        companyAreaNames = data;

        $("#CompanyAreaName").autocomplete({
            source: companyAreaNames
        });
    }

    function employeeCompanyHolderquerySucceeded(data) {
        employeeCompanyHolderNames = data;
        
        $("#EmployeeCompanyHolder_Name").autocomplete({
            source: employeeCompanyHolderNames
        });
        
        $("#EmployeeCompanyHolderName").autocomplete({
            source: employeeCompanyHolderNames
        });
        
        $("#EmployeeCompanyHolder").autocomplete({
            source: employeeCompanyHolderNames
        }); 
    }
    
    function toolquerySucceeded(data) {
        toolNames = data;

        $("#Tool_Name").autocomplete({
            source: toolNames
        });
        
        $("#ToolName").autocomplete({
            source: toolNames
        });
        
        $("#Tool").autocomplete({
            source: toolNames
        });
    }
    
    function queryFailed(textStatus) {
        alert('Error chamada ajax. ' + textStatus);
    }
    
    
});