$(function () {
    // ==== Azeite no código ====
    $(".editor-field #Phone").mask("(99) 9999-9999");
    
    
    $(".editor-field1 #Phone").mask("(99) 99999-9999");
    

    //Ideal
    //$("[mask-type='CelPhone']").mask("(99) 99999-9999");

    //$("#CheckinDateTime_Date").mask("99/99/9999 99:99");
    $("#CheckinDateTime").mask("99/99/9999 99:99");
    $("#StrCheckinDateTime").mask("99/99/9999 99:99");
    $(".editor-field1 #CheckinDateTime").mask("99/99/9999");
    
    // ==== Azeite no código ====
});