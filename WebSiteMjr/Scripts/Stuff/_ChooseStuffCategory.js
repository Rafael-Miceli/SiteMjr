$(function () {

    //var dialogWindow = new DialogToCreateEntity('StuffCategoryDialog', 'createStuffCategoryForm');
    //dialogWindow.AttachCallDialogEvent('stuffCategoryAddLink');


    $('#StuffCategoryDialog').dialog({
        autoOpen: false,
        width: 400,
        height: 200,
        modal: true,
        title: 'Adicionar Categoria de Material',
        buttons: {
            'Salvar': function () {
                var createStuffCategoryForm = $('#createStuffCategoryForm');
                if (createStuffCategoryForm.valid()) {
                    $.post(createStuffCategoryForm.attr('action'), createStuffCategoryForm.serialize(), function (data) {
                        if (data.Error != undefined) {
                            if (data.Error != '') {
                                alert(data.Error);
                            }
                        }
                        else {
                            // Add the new stuff category to the dropdown list and select it
                            $('#StuffCategoryId').append(
                                    $('<option></option>')
                                        .val(data.StuffCategory.Id)
                                        .html(data.StuffCategory.Name)
                                        .prop('selected', true)  // Selects the new stuff category in the DropDown LB
                                );
                            $('#StuffCategoryDialog').dialog('close');
                        }
                    });
                }
            },
            'Cancelar': function () {
                $(this).dialog('close');
            }
        }
    });

    $('#stuffCategoryAddLink').click(function () {
        var createFormUrl = $(this).attr('href');
        $('#StuffCategoryDialog').html('').load(createFormUrl, function () {
            // The createstuffCategoryForm is loaded on the fly using jQuery load. 
            // In order to have client validation working it is necessary to tell the 
            // jQuery.validator to parse the newly added content
            jQuery.validator.unobtrusive.parse('#createStuffCategoryForm');
            $('#StuffCategoryDialog').dialog('open');
        });

        return false;
    });
});