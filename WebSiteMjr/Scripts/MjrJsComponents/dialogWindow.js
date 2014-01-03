var DialogToCreateEntity = function (dialogContainer, dialogFormId) {

    function CreateDialog(dialogTitle, comboboxToPopulateId, dataTypeReturn) {
        dialogContainer.dialog({
            autoOpen: false,
            width: 400,
            height: 200,
            modal: true,
            title: dialogTitle,
            buttons: {
                'Salvar': function() {
                    var createStuffCategoryForm = $('#' + dialogFormId);
                    if (createStuffCategoryForm.valid()) {
                        $.post(createStuffCategoryForm.attr('action'), createStuffCategoryForm.serialize(), function(data) {
                            if (data.Error != undefined) {
                                if (data.Error != '') {
                                    alert(data.Error);
                                }
                            } else {
                                // Add the new stuff category to the dropdown list and select it
                                populateComboboxWithCreatedData(comboboxToPopulateId, dataTypeReturn, data);
                                dialogContainer.dialog('close');
                            }
                        });
                    }
                },
                'Cancelar': function() {
                    $(this).dialog('close');
                }
            }
        });
    }
    
    function populateComboboxWithCreatedData(comboboxToPopulateId, dataTypeReturn, data) {
        $('#'+comboboxToPopulateId).append(function () {
            if (dataTypeReturn === 'StuffCategory') {
                $('<option></option>')
                .val(data.StuffCategory.Id)
                .html(data.StuffCategory.Name)
                .prop('selected', true); // Selects the new stuff category in the DropDown LB
            }
            if (dataTypeReturn === 'StuffManufacture') {
                $('<option></option>')
                .val(data.StuffManufacture.Id)
                .html(data.StuffManufacture.Name)
                .prop('selected', true); // Selects the new stuff category in the DropDown LB
            }
        });
    }

    function AttachCallDialogEvent(dialogLink) {
        dialogLink.click(function () {
            var createFormUrl = $(this).attr('href');
            dialogContainer.html('').load(createFormUrl, function () {
                // The createstuffCategoryForm is loaded on the fly using jQuery load. 
                // In order to have client validation working it is necessary to tell the 
                // jQuery.validator to parse the newly added content
                jQuery.validator.unobtrusive.parse('#' + dialogFormId);
                dialogContainer.dialog('open');
            });

            return false;
        });
    }

    return {
        
        AttachCallDialogEvent: AttachCallDialogEvent,
        CreateDialog: CreateDialog

    };
}