$(function () {

    var dialogWindow = new DialogToCreateEntity($('#StuffCategoryDialog'), 'createStuffCategoryForm');
    dialogWindow.AttachCallDialogEvent($('#stuffCategoryAddLink'));
    dialogWindow.CreateDialog('Adicionar Categoria de Material', 'StuffCategoryId', 'StuffCategory');
    
});