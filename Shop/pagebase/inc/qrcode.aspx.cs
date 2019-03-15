using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.Web.Script.Serialization;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

using System.Drawing;
namespace Shop.inc
{
    public partial class qrcode : System.Web.UI.Page
    {

        public void LoadPage()
        {
            string txt = RequestTool.RequestString("txt");
            Bitmap image = QRCode.Instance.CreateImage(txt);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            Response.ClearContent();
            Response.ContentType = "image/Gif";
            Response.BinaryWrite(ms.ToArray());
        }



    }
}