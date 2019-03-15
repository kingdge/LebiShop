using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.cms
{
    public partial class Images : AdminPageBase
    {
        protected string t;
        protected List<Lebi_Image> pages;
        protected string PageString;
        protected string type;
        protected int status;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("image_del", "删除图库"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(50);
            type = RequestTool.RequestString("type");
            status = RequestTool.RequestInt("status", 2);
            string where = "1=1";
            if (type != "")
                where += " and TableName=lbsql{'" + type + "'}";
            if (type != "temp")
            {
                if (status == 0)
                    where += " and Keyid=0";
                if (status == 1)
                    where += " and Keyid>0";
            }

            int recordCount = B_Lebi_Image.Counts(where);
            pages = B_Lebi_Image.GetList(where, "id desc", PageSize, page);
            PageString = Pager.GetPaginationString("?page={0}&type=" + type + "&status=" + status, page, PageSize, recordCount);
        }
        public string ImageType()
        {
            string res = "";
            string str1 = "config,Product,producttype,productbrand,productproperty,friendlink,advertment,comment,Other,temp";
            string str2 = "系统,商品,商品分类,商品品牌,商品规格,友情链接,广告,晒单,其它,临时图片";
            string[] arr1 = str1.Split(',');
            string[] arr2 = str2.Split(',');
            int i = 0;
            foreach (string tag in arr1)
            {
                //string sel = "";
                //if (type == tag)
                //    sel = "selected";
                //res += "<option value=\"" + tag + "\" " + sel + ">" + Tag(arr2[i]) + "</option>";
                res += "<li ";
                if (type == tag)
                {
                     res += "class=\"selected\"";
                }
                res += "><a href=\"?type=" + tag + "&status=" + status + "\">" + Tag(arr2[i]) + "</a></li>";
                i++;
            }
            return res;
        }
        public string getimage(string img)
        {
            string newimg = img.Replace("w$h", "100$100");
            if (ImageHelper.IsExists(newimg))
                return newimg;
            return img;
        }
    }
}