$(function () {

    $("#tabs").tabs({
        beforeLoad: function(event, ui) {
            ui.jqXHR.error(function() {
                ui.panel.html("Erro ao carregar aba.");
            });
        }
    });

    var dialogWindow = new DialogToCreateEntity($('#StuffCategoryDialog'), 'createStuffCategoryForm');
    dialogWindow.AttachCallDialogEvent($('#stuffCategoryAddLink'));
    dialogWindow.CreateDialog('Adicionar Categoria de Material', 'StuffCategoryId', 'StuffCategory');
    
    var dialogStuffManufactureWindow = new DialogToCreateEntity($('#StuffManufactureDialog'), 'createStuffManufactureForm');
    dialogStuffManufactureWindow.AttachCallDialogEvent($('#stuffManufactureAddLink'));
    dialogStuffManufactureWindow.CreateDialog('Adicionar Fabricante do Material', 'StuffManufactureId', 'StuffManufacture');

});