(function() {
    CKEDITOR.plugins.add("Lebiupload", {
        requires: ["dialog"],
        init: function(a) {
            a.addCommand("Lebiupload", new CKEDITOR.dialogCommand("Lebiupload"));
            a.ui.addButton("Lebiupload", {
                label: "图片上传，双击使用",
                command: "Lebiupload",
                icon: this.path + "upload.png"
            });
            CKEDITOR.dialog.add("Lebiupload", this.path + "dialogs/upload.js")
 
        }
 
    })
 
})();