/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */
var picData = "";
function GetData(data) {
    picData = data;
    var ps = document.getElementsByClassName("cke_dialog");
    for (var i = ps.length - 1; i >= 0; i--) {
        text = ps[i].getElementsByClassName("cke_dialog_title")[0].innerText.trim();
        if (text == "Upload") {
            ps[i].getElementsByClassName("cke_dialog_ui_button")[0].click();
            setTimeout(function () {
                var rs = document.querySelectorAll('div[role="dialog"]')
                for (var ii = ps.length - 1; ii >= 0; ii--) {
                    if (ii != i) {
                        rs[ii].remove();
                    }
                }
            }, 100);
        }
    }
    $(".cke_dialog_background_cover").remove();
    return true;
}
CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.font_names = 'ËÎÌå/ËÎÌå;ºÚÌå/ºÚÌå;·ÂËÎ/·ÂËÎ_GB2312;¿¬Ìå/¿¬Ìå_GB2312;Á¥Êé/Á¥Êé;Ó×Ô²/Ó×Ô²;Î¢ÈíÑÅºÚ/Î¢ÈíÑÅºÚ;' + config.font_names;
    config.uiColor = '#F7F8F9'
    config.scayt_autoStartup = false
    config.language = 'zh-cn'; //ÖÐÎÄ
    config.filebrowserBrowseUrl = '/editor/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/editor/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/editor/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/editor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/editor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '/editor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    config.toolbar = 'Full';
    config.allowedContent = true;
    config.toolbar_Full =
    [
    ['Source', '-', 'Preview', 'Templates'],
    ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print', 'SpellChecker', 'Scayt'],
    ['Undo', 'Redo', '-', 'Find', 'Replace', 'RemoveFormat'],
    ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'],
    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
    ['Link', 'Unlink', 'Anchor'],
    '/',
    ['Styles', 'Format', 'Font', 'FontSize'],
    ['TextColor', 'BGColor'],
    ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
    ['Lebiupload', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar'],
    ['Maximize', 'ShowBlocks']
    ];
    config.toolbar_Basic =
    [
		['Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink'],
		['FontSize', 'TextColor', 'BGColor', 'Lebiupload', 'Flash', 'Smiley'],
        ['Source', 'Maximize']
    ];
    config.toolbar_BasicFront =
    [
		['Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink'],
		['TextColor', 'BGColor', 'Smiley'],
        ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Maximize']
    ];
    config.extraPlugins = "Lebiupload";
    config.smiley_path = CKEDITOR.basePath + 'plugins/smiley/images/';
    config.smiley_images = ['xiaonei_1.gif', 'xiaonei_2.gif', 'xiaonei_3.gif', 'xiaonei_4.gif', 'xiaonei_5.gif', 'xiaonei_6.gif', 'xiaonei_7.gif', 'xiaonei_8.gif', 'xiaonei_9.gif', 'xiaonei_10.gif', 'xiaonei_11.gif', 'xiaonei_12.gif', 'xiaonei_13.gif', 'xiaonei_14.gif', 'xiaonei_15.gif', 'xiaonei_16.gif', 'xiaonei_17.gif', 'xiaonei_18.gif', 'xiaonei_19.gif', 'xiaonei_20.gif', 'xiaonei_21.gif', 'xiaonei_22.gif', 'xiaonei_23.gif', 'xiaonei_24.gif', 'xiaonei_25.gif', 'xiaonei_26.gif', 'xiaonei_27.gif', 'xiaonei_28.gif', 'xiaonei_29.gif', 'xiaonei_30.gif', 'xiaonei_31.gif', 'xiaonei_32.gif', 'xiaonei_33.gif', 'xiaonei_34.gif', 'xiaonei_35.gif', 'xiaonei_36.gif', 'xiaonei_37.gif', 'xiaonei_38.gif', 'xiaonei_39.gif', 'xiaonei_40.gif', 'xiaonei_41.gif', 'xiaonei_42.gif', 'xiaonei_43.gif', 'xiaonei_44.gif', 'xiaonei_45.gif', 'xiaonei_46.gif', 'xiaonei_47.gif', 'xiaonei_48.gif', 'xiaonei_49.gif', 'xiaonei_50.gif', 'xiaonei_51.gif', 'xiaonei_52.gif', 'xiaonei_53.gif', 'xiaonei_54.gif', 'xiaonei_55.gif', 'xiaonei_56.gif', 'xiaonei_57.gif', 'xiaonei_58.gif', 'xiaonei_59.gif'];
};
