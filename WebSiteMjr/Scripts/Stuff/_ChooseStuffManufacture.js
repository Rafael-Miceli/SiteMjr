$(function () {
    
    var dialogWindow = new DialogToCreateEntity($('#StuffManufactureDialog'), 'createStuffManufactureForm');
    dialogWindow.AttachCallDialogEvent($('#stuffManufactureAddLink'));
    dialogWindow.CreateDialog('Adicionar Fabricante do Material', 'StuffManufactureId', 'StuffManufacture');
    
});