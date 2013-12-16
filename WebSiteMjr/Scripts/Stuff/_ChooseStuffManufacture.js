$(function () {
    $('#StuffManufactureDialog').dialog({
        autoOpen: false,
        width: 400,
        height: 200,
        modal: true,
        title: 'Adicionar Fabricante do Material',
        buttons: {
            'Salvar': function () {
                var createStuffManufactureForm = $('#createStuffManufactureForm');
                if (createStuffManufactureForm.valid()) {
                    $.post(createStuffManufactureForm.attr('action'), createStuffManufactureForm.serialize(), function (data) {
                        if (data.Error != undefined) {
                            if (data.Error != '') {
                                alert(data.Error);
                            }
                        }
                        else {
                            // Add the new stuff Manufacture to the dropdown list and select it
                            $('#StuffManufactureId').append(
                                    $('<option></option>')
                                        .val(data.StuffManufacture.Id)
                                        .html(data.StuffManufacture.Name)
                                        .prop('selected', true)  // Selects the new stuff Manufacture in the DropDown LB
                                );
                            $('#StuffManufactureDialog').dialog('close');
                        }
                    });
                }
            },
            'Cancelar': function () {
                $(this).dialog('close');
            }
        }
    });

    $('#stuffManufactureAddLink').click(function () {
        var createFormUrl = $(this).attr('href');
        $('#StuffManufactureDialog').html('').load(createFormUrl, function () {
            // The createstuffManufactureForm is loaded on the fly using jQuery load. 
            // In order to have client validation working it is necessary to tell the 
            // jQuery.validator to parse the newly added content
            jQuery.validator.unobtrusive.parse('#createStuffManufactureForm');
            $('#StuffManufactureDialog').dialog('open');
        });

        return false;
    });
});