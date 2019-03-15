using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

using System.Text;
using System.Collections.Generic;

using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;


namespace Shop.Ajax
{
    /// <summary>
    /// 用户头像上传
    /// </summary>
    public partial class imageupload_avatar : ShopPage
    {
        public void LoadPage()
        {
            if (!AjaxLoadCheck())
            {
                return;
            }
            string msg = "";
            string name = "";
            int minWidth = 150;   //最小宽度
            int minHeight = 150;  //最小高度
            int maxWidth = 300;  //最大宽度
            int maxHeight = 300;  //最大高度
            HttpFileCollection files = HttpContext.Current.Request.Files;
            BaseConfig conf = ShopCache.GetBaseConfig();
            B_WaterConfig bc = new B_WaterConfig();

            WaterConfig mx = bc.LoadConfig();
            if (files.Count > 0)
            {
                ///'检查文件扩展名字
                HttpPostedFile postedFile = files[0];
                string savepath = GetPath(ShopCache.GetBaseConfig().UpLoadPath);
                string fileName, fileExtension;
                fileName = System.IO.Path.GetFileName(postedFile.FileName);
                fileExtension = System.IO.Path.GetExtension(fileName);
                if (conf.UpLoadSaveName == "" || conf.UpLoadSaveName == null)
                    conf.UpLoadSaveName = "0";
                if (conf.UpLoadSaveName == "0")
                {
                    name = DateTime.Now.ToString("yyMMddssfff") + "_w$h_";
                }
                else
                {
                    name = System.IO.Path.GetFileNameWithoutExtension(fileName) + "_w$h_";
                }
                if (RequestTool.RequestSafeString("path") == "Temp")
                {
                    name = DateTime.Now.ToString("yyMMddssfff") + "_w$h_";
                }
                name = conf.UpLoadRName + name + fileExtension;
                int status = ImageHelper.SaveImage(postedFile, savepath, name);
                if (status != 290)
                {
                    msg = Language.Tag(EX_Type.TypeName(status));
                    Response.Write("{\"msg\":\"" + msg + "\"}");
                    return;
                }
                else
                {
                    string OldImage = savepath + name;
                    string ImageMedium = name.Replace("_w$h_", "_200$200_");
                    ////写入数据库
                    //Lebi_Image model = new Lebi_Image();
                    //model.Image = savepath + ImageMedium;
                    //model.Keyid = 0;
                    //model.Size = "";
                    //model.TableName = "temp";
                    //B_Lebi_Image.Add(model);


                    //if (originalImage.Width > minWidth && originalImage.Height > minHeight)
                    //{
                    ImageHelper.UPLoad(OldImage, savepath, ImageMedium, maxWidth, maxHeight);
                    //}
                    string FilePath = ImageHelper.rootpath(savepath + ImageMedium);
                    System.Drawing.Image originalImage = System.Drawing.Image.FromFile(FilePath);
                    ImageHelper.DeleteImage(OldImage);
                    Response.Write("{\"msg\":\"OK\",\"ImageUrl\":\"" + savepath + ImageMedium + "\",\"width\":\"" + originalImage.Width + "\",\"height\":\"" + originalImage.Height + "\"}");
                    return;

                }

            }
            msg = Language.Tag("没有选择任何文件");
            Response.Write("{\"msg\":\"" + msg + "\"}");
        }
        private string GetPath(string path)
        {
            string path_ = RequestTool.RequestSafeString("path");
            path_ = path_.Replace(".", "");
            path_ = path_.Replace("/", "");
            if (path_ == "")
                path_ = "Temp";
            path = path + "/" + path_ + "/";
            path = ThemeUrl.CheckURL(path);
            return path;
        }
    }
}