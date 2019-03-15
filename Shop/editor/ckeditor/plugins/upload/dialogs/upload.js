(function () {
    CKEDITOR.dialog.add("upload",
    function (a) {
        return {
            title: "Upload",
            minWidth: "350px",
            minHeight: "300px",
            contents: [{
                id: "upload",
                label: "",
                title: "",
                expand: true,
                width: "350px",
                height: "300px",
                padding: 0,
                elements: [{
                    type: "html",
                    style: "width:350px;height:300px;overflow:hidden;",
                    html: '<iframe name="mutiimageupload" id="mutiimageupload" src="' + WebPath + '/editor/ckeditor/plugins/upload/upload.aspx?n=ckeditor&i=1" frameborder="0" width="400" height="33px" scrolling="no" allowtransparency="true"></iframe><input name="mutiimage" id="mutiimage" type="hidden" value="" />'

                }]
            }],

            onOk: function () {
                //点击确定按钮后的操作
                var htmlstr = $("#mutiimage").val();
                a.insertHtml(htmlstr);
                $("#mutiimage").val("");
            }
        }
    })
})();