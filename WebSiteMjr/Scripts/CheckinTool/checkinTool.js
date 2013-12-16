$(function() {
    var employeeCompanyHolderNames,
        toolNames,
        toolNamesCall,
        employeeCompanyHolderNamesCall;

    //Set Ajax Call
    employeeCompanyHolderNamesCall = {
        url: '/Api/CheckinToolApi/GetEmployeeCompanyHoldersName',
        type: 'GET',
        datatype: 'json'
    };
    
    toolNamesCall = {
        url: '/Api/CheckinToolApi/GetToolsName',
        type: 'GET',
        datatype: 'json'
    };
    

    //Make Ajax Call
    $.ajax(employeeCompanyHolderNamesCall)
            .then(employeeCompanyHolderquerySucceeded)
            .fail(queryFailed);
    
    $.ajax(toolNamesCall)
            .then(toolquerySucceeded)
            .fail(queryFailed);
    

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