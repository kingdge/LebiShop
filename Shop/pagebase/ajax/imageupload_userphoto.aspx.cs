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
    /// 前台晒单图片上传
    /// </summary>
    public partial class imageupload_userphoto : ShopPage
    {
        public void LoadPage()
        {
            if (!AjaxLoadCheck())
            {
                return;
            }
            string msg = "";
            string name = "";
            string sizestr = "100,100"; //缩略图大小

            HttpFileCollection files = HttpContext.Current.Request.Files;
            BaseConfig conf = ShopCache.GetBaseConfig();
            B_WaterConfig bc = new B_WaterConfig();
            WaterConfig mx = bc.LoadConfig();
            if (files.Count > 0)
            {
                ///'检查文件扩展名字
                HttpPostedFile postedFile = files[0];
                string savepath = GetPath(conf.UpLoadPath);
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
                name = conf.UpLoadRName + name + fileExtension;
                int status = ImageHelper.SaveImage(postedFile, savepath, name);
                if (status != 290)
                {
                    msg = Language.Tag(EX_Type.TypeName(status));
                    Response.Write("{\"msg\":\"" + msg + "\"}");
                    return;
                }

                string OldImage = savepath + name;
                string SmallImage = "";
                //生成所有自定义规格
                string[] sizes = sizestr.Split('$');
                foreach (string ss in sizes)
                {
                    string[] s = ss.Split(',');
                    int w = Convert.ToInt32(s[0]);
                    int h = Convert.ToInt32(s[1]);
                    SmallImage = name.Replace("_w$h_", "_" + w + "$" + h + "_");
                    ImageHelper.UPLoad(OldImage, savepath, SmallImage, w, h);

                }
                SmallImage = savepath + SmallImage;
                //写入数据库
                Lebi_Image model = new Lebi_Image();
                model.Image = savepath + name;
                model.Keyid = 0;
                model.Size = sizestr;
                model.TableName = "temp";
                
                B_Lebi_Image.Add(model);
                msg = "OK";
                Response.Write("{\"msg\":\"" + msg + "\",\"ImageSmall\":\"" + SmallImage + "\",\"ImageUrl\":\"" + model.Image + "\"}");
                return;

            }
            msg = Language.Tag("没有选择任何文件", "");
            Response.Write("{\"msg\":\"" + msg + "\"}");
        }
        private string GetPath(string path)
        {
            path = path + "/photo/" + DateTime.Now.ToString("yyyy") + "/";
            path = ThemeUrl.CheckURL(path);

            return path;
        }
    }
}