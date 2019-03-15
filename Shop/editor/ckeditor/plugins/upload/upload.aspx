<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upload.aspx.cs" Inherits="Shop.upload" %>

<html>
<head>
    <title>LebiShop Upload</title>
    <script type="text/javascript" src="<%=site.AdminJsPath %>/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="<%=site.AdminJsPath %>/dropzone.js"></script>
    <style type="text/css">
        html, body
        {
            margin: 0;
            padding: 0;
            font-size: 12px;
        }
        .SaPload
        {
            width: 1000px;
            height: 40px;
            margin: 0 auto;
            overflow: hidden;
            display: block;
        }
        .box2 .row
        {
            margin-bottom: 10px;
        }
        .box2title
        {
            padding: 0 10px 10px 10px;
            font-size: 14px;
            font-weight: 700;
        }
        html
        {
            background: #fff;
        }
        body
        {
            background: #fff;
        }
        body
        {
            margin: 0;
            padding: 10px;
            width: auto;
            margin: 0 auto;
        }
        .box1
        {
            width: auto;
            padding: 0px 20px 10px 20px;
            margin-bottom: 0;
        }
        .box1 button
        {
            height: 140px;
            font-size: 50px;
            color: #333;
            padding: 0;
        }
        .box2
        {
            width: auto;
            padding: 0 20px;
        }
        .box2 .row
        {
            margin-bottom: 10px;
        }
        .box2title
        {
            padding: 0 10px 10px 10px;
            font-size: 14px;
            font-weight: 700;
        }
        .form-horizontal
        {
            width: auto;
        }
        .box3
        {
            padding: 30px 60px 0 0;
        }
        a.btn
        {
            background: #eee;
            border: 1px solid #ccc;
            border-radius: 15px;
            display: block;
            text-align: center;
            padding: 20px 0;
            cursor: pointer;
            margin-bottom: 20px;
        }
        .niantie
        {
            width: auto;
            border-radius: 15px;
            padding: 10px;
            border: 1px solid #ccc;
            width: 100%;
        }
        #imgholder
        {
            height: 200px;
            border-radius: 15px;
            padding: 10px;
            border: 1px solid #ccc;
            overflow: auto;
            cursor:pointer;
            z-index:-1;
            background: url("message.jpg") 40px 35px no-repeat;
        }
        .dz-message span{display:none}
        .uploaditem
        {
            width: 70px;
            height: 90px;
            float: left;
            position: relative;
            padding: 6px;
            border-right: solid 1px #999;
            border-bottom: solid 1px #999;
            border: 0px;
        }
        .uploaditem button
        {
            position: absolute;
            right: 0px;
        }
        .uploaditem img
        {
            width: 70px;
            height: 70px;
            display: block;
        }
        .progress
        {
            margin-bottom: 20px;
        }
        .loadingbox
        {
            margin-top: 2px;
            width: 60px;
        }
        
    </style>
</head>
<body>
    <script type="text/javascript">
        var sp1;
        var returnStr = "";
        function sdata(d) {
            parent.GetData(d);
            clearAll();
        }
        function sdata2() {
            var d = $("#copyImg").val();
            returnStr += "<p><img src='" + d + "' /></p>";
            parent.document.getElementById('mutiimage').value = returnStr;
            sdata(returnStr);

        }

        function cp(sder) {
            if ($(sder).val().length > 1) {
                sdata2();
            }
        }
    </script>
    <div class="thispage">
        <div class="row">
            <div>
                <div class="box1">
                    <div id="imgholder" class="dropzone" title="请选择或拖拽上传图片">
                    </div>
                </div>
                <div class="box2">
                    <div class="row">
                        <div class="col-xs-12">
                            <input type="text" id="copyImg" onblur="cp(this)" class="niantie" placeholder="网络图片地址">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="display: none;">
            <div id="imgloading">
                <div class="uploaditem">
                    
                    <img data-dz-thumbnail src="">
                    <div class="loadingbox">
                        <div class="progress">
                            <div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="100"
                                aria-valuemin="0" aria-valuemax="100" data-dz-uploadprogress="" style="width: 0%; background-color:#cccccc;height:5px;">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <ul>
                <li class="foldertemplate"><i class="glyphicon glyphicon-folder-open"></i><a onclick="openFolder(this)">
                </a></li>
                <li class="filetemplate" style='display: none;'>
                    <img onclick="fileselectcallback(this)" /><a class='delete' onclick="deletefile(this)"><i
                        class='glyphicon glyphicon-remove'></i></a><a class='edit' onclick="editfile(this)"><i
                            class='glyphicon glyphicon-edit'></i></a> </li>
            </ul>
        </div>
    </div>
   <script type="text/javascript">
       var allfiles = new Array();
       var iscompleted = false;
       function clearAll() {
           allfiles = new Array();
           $("#imgholder").children().remove();
       }
       function fileselectcallback() {
           returnStr = ""
           for (var i = 0; i < allfiles.length; i++) {
               returnStr += "<p><img src='<%=webconfig.ImageURL %>" + allfiles[i] + "' /></p>";
           }
           parent.document.getElementById('mutiimage').value = returnStr;
           sdata(returnStr);
           $("#imgholder").attr("style", "background:url(\"message.jpg\") 40px 35px no-repeat");
       }

       function savenetpic() {
           var url = $("#tb_netpicurl").val();

       }

       var returnStr = "";
       var filecount = 0;
       var oversize = false;
       var dz = null;
       var extarr = new Array("jpg", "jpeg", "gif", "png", "bmp");
       $(function () {
           dz = new Dropzone("#imgholder", {
               url: "<%=site.WebPath %>/ajax/imageupload.aspx",
               previewsContainer: "#imgholder",
               previewTemplate: $("#imgloading").html().toString(),
               thumbnailWidth: 70,
               thumbnailHeight: 70,
               maxFiles: 20,
               maxFilesize: 4,
               acceptedFiles: "." + extarr.join(",."),
               success: function (e) {
                   var json = $.parseJSON(e.xhr.responseText);
                   if (json.msg == 'OK') {
                       allfiles.push(json.img);
                       if (iscompleted)
                           fileselectcallback();
                   }
               },
               maxfilesexceeded: function (file) {
                   if (!oversize) {
                       dz.removeFile(file);
                       oversize = true;
                   }
               }
                , totaluploadprogress: function (e) {
                    iscompleted = (e == 100);
                }
           });
           dz.on("addedfile", function (file) {
               //alert(file.name);
               var f = file.name.split('.');
               var ext = f[f.length - 1].toLowerCase();
               if (extarr.indexOf(ext) <= -1) {
                   dz.removeFile(file);
               }
               $("#imgholder").show();
               $("#imgholder").attr("style", "background:none");
           });
       });
    </script>
</body>
</html>
