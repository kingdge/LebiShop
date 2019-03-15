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
    /// 自定义上传
    /// </summary>
    public partial class ImageUpload_Theme : PageBase
    {
        public void LoadPage()
        {
            if (!AjaxLoadCheck())
            {
                return;
            }
            string msg = "";

            HttpFileCollection files = HttpContext.Current.Request.Files;
            BaseConfig conf = ShopCache.GetBaseConfig();
            B_WaterConfig bc = new B_WaterConfig();
            WaterConfig mx = bc.LoadConfig();
            if (files.Count > 0)
            {
                ///'检查文件扩展名字
                HttpPostedFile postedFile = files[0];
                string name = "icon";
                string savepath = GetPath();
                string fileName, fileExtension;
                fileName = System.IO.Path.GetFileName(postedFile.FileName);
                fileExtension = System.IO.Path.GetExtension(fileName);
                name = name + fileExtension;
                string ServerPath = System.Web.HttpContext.Current.Server.MapPath("/");
                string fdir = ServerPath + savepath + "/icon" + fileExtension;
                string fdirs = ServerPath + savepath + "/icon_small" + fileExtension;
                if (File.Exists(fdir))
                    File.Delete(fdir);
                if (File.Exists(fdirs))
                    File.Delete(fdirs);
                int status = ImageHelper.SaveImage(postedFile, savepath, name);
                if (status != 290)
                {
                    msg = Language.Tag(EX_Type.TypeName(status));
                    Response.Write("{\"msg\":\"" + msg + "\"}");
                    return;
                }

                string OldImage = savepath + name;
                //生成所有自定义规格
                ImageHelper.UPLoad(OldImage, savepath, "icon_small" + fileExtension, 100, 100, "Cut");

                //写入数据库
                Lebi_Image model = new Lebi_Image();
                model.Image = name;
                model.Keyid = 0;
                model.Size = "100,100";
                model.TableName = "temp";
                B_Lebi_Image.Add(model);
                msg = "OK";
                Response.Write("{\"msg\":\"" + msg + "\",\"ImageUrl\":\"" + model.Image + "\"}");
                return;

            }
            msg = Language.Tag("没有选择任何文件", "");
            Response.Write("{\"msg\":\"" + msg + "\"}");
        }
        private string GetPath()
        {
            string path_ = RequestTool.RequestSafeString("path");
            path_ = path_.Replace(".", "");
            path_ = path_.Replace("/", "");
            string path = "/" + path_ + "/";
            path = ThemeUrl.CheckURL(path);
            return path;
        }
    }
}