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
using System.Web.Script.Serialization;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
namespace Shop.Ajax
{
    public partial class ImageUploadmuti : PageBase
    {


        public void LoadPage()
        {
            if (!IsPostBack)
            {
                //if (!AjaxLoadCheck())
                //{
                //    return;
                //}
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
                            JavaScriptSerializer jss = new JavaScriptSerializer();
                            LBAPI api = jss.Deserialize<LBAPI>(res);
                            result = res;
                        }
                        catch (Exception ex)
                        {
                            result = "{\"msg\":\"" + ex.Message + "\"}";
                        }

                    }
                    else
                    {
                        string OldImage = savepath + name;
                        //string ImageBig = name.Replace("_w$h_", "_" + conf.ImageBigWidth + "$" + conf.ImageBigHeight + "_");
                        //string ImageMedium = name.Replace("_w$h_", "_" + conf.ImageMediumWidth + "$" + conf.ImageMediumHeight + "_");
                        //string ImageSmall = name.Replace("_w$h_", "_" + conf.ImageSmallWidth + "$" + conf.ImageSmallHeight + "_");
                        //string size = conf.ImageBigWidth + "$" + conf.ImageBigHeight + "," + conf.ImageMediumWidth + "$" + conf.ImageMediumHeight + "," + conf.ImageSmallWidth + "$" + conf.ImageSmallHeight;
                        //if (mx.OnAndOff == "1")
                        //{
                        //    ImageHelper.UPLoad(OldImage, savepath, "temp_" + name, Convert.ToInt32(conf.ImageBigWidth), Convert.ToInt32(conf.ImageBigHeight));
                        //    ImageHelper.MakeWater(System.Web.HttpContext.Current.Server.MapPath(savepath) + "temp_" + name, savepath, ImageBig, mx);
                        //    ImageHelper.DeleteImage(savepath + "temp_" + name);
                        //}
                        //else
                        //{
                        //    ImageHelper.UPLoad(OldImage, savepath, ImageBig, Convert.ToInt32(conf.ImageBigWidth), Convert.ToInt32(conf.ImageBigHeight));
                        //}

                        //ImageHelper.UPLoad(OldImage, savepath, ImageMedium, Convert.ToInt32(conf.ImageMediumWidth), Convert.ToInt32(conf.ImageMediumHeight));
                        //ImageHelper.UPLoad(OldImage, savepath, ImageSmall, Convert.ToInt32(conf.ImageSmallWidth), Convert.ToInt32(conf.ImageSmallHeight));


                        ////生成所有规格
                        //string where = "id not in (select id from Lebi_ImageSize where (Width=" + conf.ImageBigWidth + " and Height=" + conf.ImageBigHeight + ") or (Width=" + conf.ImageMediumWidth + " and Height=" + conf.ImageMediumHeight + ") or (Width=" + conf.ImageSmallWidth + " and Height=" + conf.ImageSmallHeight + "))";
                        //List<Lebi_ImageSize> ss = B_Lebi_ImageSize.GetList(where, "");
                        //foreach (Lebi_ImageSize s in ss)
                        //{
                        //    ImageHelper.UPLoad(OldImage, savepath, name.Replace("_w$h_", "_" + s.Width + "$" + s.Height + "_"), s.Width, s.Height);
                        //    size += "," + s.Width + "$" + s.Height;
                        //}
                        //写入数据库
                        Lebi_Image model = new Lebi_Image();
                        model.Image = savepath + name;
                        model.Keyid = 0;
                        model.Size = "";
                        model.TableName = "temp";
                        B_Lebi_Image.Add(model);

                        result = "{\"img\":\"" + OldImage + "\",\"msg\":\"OK\"}";
                    }
                }


            }
            catch (System.Exception ex)
            {
                result = "{\"msg\":\"" + ex.Message + "\"}";
            }
            return result;
        }
        private string GetPath(string path)
        {

            path = path + "/Product/" + DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MMdd") + "/";
            path = ThemeUrl.CheckURL(path);
            return path;
        }

    }
}
