using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;

namespace Shop.inc
{
    public partial class product_qrcode : Bussiness.ShopPage
    {
        protected Lebi_Product product;
        protected string tourl = "";
        public void LoadPage()
        {
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='P_Product'");
            int id = Rint("id");
            product = GetProduct(id);
            Lebi_Site site = B_Lebi_Site.GetModel("IsMobile=1 order by Sort desc");
            Shop.Bussiness.Site website = new Shop.Bussiness.Site();
            if (site != null)
            {
                if (site.Domain != "")
                    tourl = "http://" + site.Domain + "/product.aspx?id=" + product.id;
                else
                {
                    tourl = "http://" + ShopCache.GetMainSite().Domain + website.WebPath;
                    tourl = tourl.TrimEnd('/') + site.Path;
                    tourl = tourl.TrimEnd('/') + "/product.aspx?id=" + product.id;
                }
            }
            Response.Write(tourl);
            tourl = QRfromGoogle(tourl);
        }
        //' * google api 二维码生成【QRcode可以存储最多4296个字母数字类型的任意文本，具体可以查看二维码数据格式】
        //' * @param string $chl 二维码包含的信息，可以是数字、字符、二进制信息、汉字。不能混合数据类型，数据必须经过UTF-8 URL-encoded.如果需要传递的信息超过2K个字节请使用POST方式
        //' * @param int $widhtHeight 生成二维码的尺寸设置
        //' * @param string $EC_level 可选纠错级别，QR码支持四个等级纠错，用来恢复丢失的、读错的、模糊的、数据。
        //' *                         L-默认：可以识别已损失的7%的数据
        //' *                         M-可以识别已损失15%的数据
        //' *                         Q-可以识别已损失25%的数据
        //' *                         H-可以识别已损失30%的数据
        //' * @param int $margin 生成的二维码离图片边框的距离
        public string QRfromGoogle(string chl)
        {
            int widhtHeight = 300;
            string EC_level = "L";
            int margin = 0;
            string QRfromGoogle;
            //chl = UrlEncode1(chl);
            QRfromGoogle = "http://chart.apis.google.com/chart?chs=" + widhtHeight + "x" + widhtHeight + "&cht=qr&chld=" + EC_level + "|" + margin + "&chl=" + chl;
            return QRfromGoogle;
        }
        //url编码，添加空格转成%20
        public string UrlEncode1(string con)
        {
            string UrlEncode = "";
            UrlEncode = HttpUtility.UrlEncode(con, Encoding.UTF8);
            UrlEncode = UrlEncode.Replace("+", "%20");
            return UrlEncode;
        }
    }
}