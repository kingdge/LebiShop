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
    public partial class fileupload : AdminAjaxBase
    {
        public void LoadPage()
        {
            if (!AjaxLoadCheck())
            {
                return;
            }
            string msg = "";
            HttpFileCollection files = HttpContext.Current.Request.Files;
           
            if (files.Count > 0)
            {
                ///'检查文件扩展名字
                HttpPostedFile postedFile = files[0];
                //string name = DateTime.Now.ToString("yyMMddssfff") + "_w$h_";
                string savepath = GetPath(ShopCache.GetBaseConfig().UpLoadPath);
                string fileName, fileExtension;
                fileName = System.IO.Path.GetFileName(postedFile.FileName);
                fileExtension = System.IO.Path.GetExtension(fileName);
                //name = name + fileExtension;
                int status = FileHelper.UPLoad(postedFile, savepath, fileName);
                if (status != 290)
                {
                    msg = Language.Tag(EX_Type.TypeName(status));
                    Response.Write("{\"msg\":\"" + msg + "\"}");
                    return;
                }
                else
                {
                    Response.Write("{\"msg\":\"OK\",\"url\":\"" + savepath + fileName + "\"}");
                    return;

                }

            }
            msg = Language.Tag("没有选择任何文件");
            Response.Write("{\"msg\":\"" + msg + "\"}");
        }
        private string GetPath(string path)
        {
            
            path = path + "/file/" + DateTime.Now.ToString("yyyy") + "/";
            path = ThemeUrl.CheckURL(path);
            return path;
        }
    }
}