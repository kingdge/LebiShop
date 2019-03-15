using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;

namespace Shop
{
    public partial class pic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //"HW"://指定高宽缩放（可能变形）     
            //"W"://指定宽，高按比例 
            //"H"://指定高，宽按比例
            //"NHW"://生成不超过尺寸的不变型的缩略图
            //"CUT"://指定高宽裁减（不变形）
            string mode = RequestTool.RequestString("m").ToLower();
            if (mode == "")
                mode = "nhw";
            int width = RequestTool.RequestInt("w");
            int height = RequestTool.RequestInt("h");
            string picname = RequestTool.RequestSafeString("p");
            if (picname == "")
                picname = RequestTool.RequestSafeString("n");
            string size = "";



            try
            {
                if (!ImageHelper.IsExists(picname))
                {
                    picname = "/theme/system/images/no.jpg";
                }
                string[] exnamearr = picname.Split('.');
                string exname = "." + exnamearr[exnamearr.Length - 1];
                if (exname.Length > 5)
                    exname = ".jpg";

                System.Drawing.Image img = ImageHelper.MakeThumbnail(picname, width, height, mode);
                showpic(img, exname);
                img.Dispose();
                return;
            }
            catch
            {
                picname = "/theme/system/images/no.jpg";
                System.Drawing.Image img = ImageHelper.MakeThumbnail(picname, width, height, mode);
                showpic(img, ".jpeg");
                return;
            }

        }

        private void showpic(string img, string exname)
        {
            string temppic = rootpath(img);
            System.Drawing.Image image = System.Drawing.Image.FromFile(temppic);
            showpic(image, exname);
        }
        private void showpic(System.Drawing.Image image, string exname)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            System.Drawing.Imaging.ImageFormat t = System.Drawing.Imaging.ImageFormat.Jpeg;
            if (exname.ToLower() == ".gif")
                t = System.Drawing.Imaging.ImageFormat.Gif;
            if (exname.ToLower() == ".png")
                t = System.Drawing.Imaging.ImageFormat.Png;
            if (image.Width > 300)
            {

                B_WaterConfig bc = new B_WaterConfig();
                WaterConfig mx = bc.LoadConfig();
                if (mx.OnAndOff == "1")
                {
                    image = ImageHelper.MakeWater(image, mx);
                }
            }
            image.Save(ms, t);
            Response.ClearContent();
            Response.ContentType = "image/jpeg";
            Response.BinaryWrite(ms.ToArray());

        }
        private string rootpath(string path)
        {
            return System.Web.HttpContext.Current.Server.MapPath("~" + path);
        }

    }
}