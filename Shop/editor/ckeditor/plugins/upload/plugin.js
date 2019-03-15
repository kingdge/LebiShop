(function() {
    CKEDITOR.plugins.add("upload", {
        requires: ["dialog"],
        init: function(a) {
            a.addCommand("upload", new CKEDITOR.dialogCommand("upload"));
            a.ui.addButton("upload", {
                label: "圖片上傳",
                command: "upload",
                icon: this.path + "upload.png"
            });
            CKEDITOR.dialog.add("upload", this.path + "dialogs/upload.js")
 
        }
 
    })
 
})();