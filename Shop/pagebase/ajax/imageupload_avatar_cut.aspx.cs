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
    /// 用户头像上传裁剪
    /// </summary>
    public partial class imageupload_avatar_cut : ShopPage
    {
        public void LoadPage()
        {
            if (!AjaxLoadCheck())
            {
                return;
            }
            //Response.ContentType = "text/plain";
            //Response.Charset = "utf-8";

            System.Drawing.Bitmap bitmap = null;   //按截图区域生成Bitmap
            System.Drawing.Image thumbImg = null;      //被截图 
            System.Drawing.Graphics gps = null;    //存绘图对象   
            System.Drawing.Image finalImg = null;  //最终图片
            string pointX = RequestTool.RequestSafeString("pointX");   //X坐标
            string pointY = RequestTool.RequestSafeString("pointY");   //Y坐标
            string imgUrl = RequestTool.RequestSafeString("imgUrl");   //被截图图片地址
            string rlSize = "150";  // context.Request.Params["maxVal"];        //截图矩形的大小

            int finalWidth = 150;
            int finalHeight = 150;
            BaseConfig conf = ShopCache.GetBaseConfig();
            B_WaterConfig bc = new B_WaterConfig();
            if (!string.IsNullOrEmpty(pointX) && !string.IsNullOrEmpty(pointY) && !string.IsNullOrEmpty(imgUrl))
            {
                string ext = System.IO.Path.GetExtension(imgUrl).ToLower();   //上传文件的后缀（小写）

                bitmap = new System.Drawing.Bitmap(Convert.ToInt32(rlSize), Convert.ToInt32(rlSize));

                thumbImg = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(imgUrl));

                System.Drawing.Rectangle rl = new System.Drawing.Rectangle(Convert.ToInt32(pointX), Convert.ToInt32(pointY), Convert.ToInt32(rlSize), Convert.ToInt32(rlSize));   //得到截图矩形

                gps = System.Drawing.Graphics.FromImage(bitmap);      //读到绘图对象

                gps.DrawImage(thumbImg, 0, 0, rl, System.Drawing.GraphicsUnit.Pixel);
                string savePath = conf.UpLoadPath + "/avatar/admin/";
                finalImg = PubClass.GetThumbNailImage(bitmap, finalWidth, finalHeight);
                if (!File.Exists(savePath))   //如果路径不存在，则创建
                {
                    System.IO.Directory.CreateDirectory(ImageHelper.rootpath(savePath));
                }
                string rndname = DateTime.Now.ToString("yyMMddssfff");
                string finalPath = savePath + rndname + "_small" + ext;
                finalImg.Save(HttpContext.Current.Server.MapPath(finalPath));
                string formconfig = HttpContext.Current.Server.MapPath(@"~/" + imgUrl);
                string toconfig = HttpContext.Current.Server.MapPath(@"~"+ conf.UpLoadPath +"/avatar/admin/" + rndname + "_original" + ext);
                ImageHelper.DeleteImage(formconfig);
                FileTool.CopyFile(formconfig, toconfig, true);

                bitmap.Dispose();
                thumbImg.Dispose();
                gps.Dispose();
                finalImg.Dispose();
                GC.Collect();
                ////写入数据库
                //Lebi_Image model = new Lebi_Image();
                //model.Image = finalPath;
                //model.Keyid = CurrentUser.id;
                //model.Size = "120$120";
                //model.TableName = "avatar";
                //B_Lebi_Image.Add(model);
                //PubClass.FileDel(HttpContext.Current.Server.MapPath(imgUrl));
                Response.Write(finalPath);
            }
        }
    }
}