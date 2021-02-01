/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    //config.language = 'vi';
    config.enterMode = CKEDITOR.ENTER_BR;
    var duong_dan = '/Content/';
    config.filebrowserBrowseUrl = duong_dan + 'ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = duong_dan + 'ckfinder/ckfinder.html?type=Images';
    config.filebrowserFlashBrowseUrl = duong_dan + 'ckfinder/ckfinder.html?type=Flash';
    config.filebrowserUploadUrl = duong_dan + 'ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = duong_dan + 'ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = duong_dan + '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    // Define changes to default configuration here. For example:
};

