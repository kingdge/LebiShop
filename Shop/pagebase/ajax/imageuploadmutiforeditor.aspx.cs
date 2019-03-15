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
    public partial class imageuploadmutiforeditor : PageBase
    {


        public void LoadPage()
        {
            if (!IsPostBack)
            {
                if (!AjaxLoadCheck())
                {
                    return;
                }
                int n = Convert.ToInt32(Request.Form["myid"]);

                string filename = TestUpload();

                Response.ContentType = "text/plain";
                Response.Write(filename);
                Response.End();



            }
        }


        private string TestUpload()
        {
            HttpFileCollection files = HttpContext.Current.Request.Files;
            B_WaterConfig bc = new B_WaterConfig();
            WaterConfig mx = bc.LoadConfig();
            BaseConfig conf = ShopCache.GetBaseConfig();
            string result = "";
            string name = "";
            StringBuilder strMsg = new StringBuilder();
            string savepath = GetPath(conf.UpLoadPath);
            try
            {
                for (int iFile = 0; iFile < files.Count; iFile++)
                {
                    ///'检查文件扩展名字
                    HttpPostedFile postedFile = files[iFile];
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
                        continue;
                    }
                    if (Shop.LebiAPI.Service.Instanse.Check("imageserver") && WebConfig.Instrance.UpLoadURL != "")
                    {
                        try
                        {
                            string res = HtmlEngine.PostFile(WebConfig.Instrance.UpLoadURL, ImageHelper.rootpath(savepath + name));
                            res = ImageHelper.GetImageByServerResult(res);
                            ImageHelper.DeleteImage(savepath + name);
                            return res;
                        }
                        catch (Exception ex)
                        {
                            return ex.Message;
                        }
                    }

                    string OldImage =savepath + name;

                    result = OldImage;

                }
            }
            catch (System.Exception Ex)
            {
                string err = Ex.Message;
            }
            return result;
        }
        private string GetPath(string path)
        {

            path = path + "/userfiles/images/" + DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MMdd") + "/";
            path = ThemeUrl.CheckURL(path);
            return path;
        }

    }
}
