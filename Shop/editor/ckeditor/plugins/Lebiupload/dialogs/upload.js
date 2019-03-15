(function () {
    CKEDITOR.dialog.add("Lebiupload",
    function (a) {
        return {
            id: "id_Lebiupload",
            title: "Upload",
            minWidth: "350px",
            minHeight: "300px",
            contents: [{
                id: "Lebiupload",
                label: "label_Lebiupload",
                title: "",
                expand: true,
                width: "350px",
                height: "300px",
                padding: 0,
                elements: [{
                    type: "html",
                    style: "width:350px;height:300px;overflow:hidden;",
                    html: '<iframe name="mutiimageupload" id="mutiimageupload" src="' + WebPath + '/editor/ckeditor/plugins/Lebiupload/upload.aspx?n=ckeditor&i=1&t='+Math.random()+'" frameborder="0" width="400" height="33px" scrolling="no" allowtransparency="true"></iframe><input name="mutiimage" id="mutiimage" type="hidden" value="" />'

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