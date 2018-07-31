/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.filebrowserBrowseUrl = '/CKEditor/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/CKEditor/ckfinder/ckfinder.html?type=Images';
    config.filebrowserFlashBrowseUrl = '/CKEditor/ckfinder/ckfinder.html?type=Flash';
    config.filebrowserUploadUrl = '/CKEditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/CKEditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '/CKEditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    CKFinder.setupCKEditor(null, '/CKEditor/ckfinder/');
};
