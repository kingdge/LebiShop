using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.Collections.Specialized;
using Shop;

namespace Shop.Admin.Product
{
    public partial class product_price_list_window : AdminAjaxBase
    {

        protected string PageString = "";
        protected List<Lebi_User> users;
        protected int userid = 0;
        protected int userlevelid = 0;
        protected string key = "";
        protected Lebi_Product product;
        protected Lebi_UserLevel userlevel;
        protected int productid = 0;
        protected Lebi_Product_Price userlevellimit;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("product_user_price", "商品会员价格"))
            {
                WindowNoPower();
            }
            userlevelid = RequestTool.RequestInt("userlevelid");
            userid = RequestTool.RequestInt("userid");
            productid = RequestTool.RequestInt("productid");
            key = RequestTool.RequestString("key");
            userlevel = B_Lebi_UserLevel.GetModel(userlevelid);
            product = B_Lebi_Product.GetModel(productid);
            if (product == null)
                product = new Lebi_Product();
            if (userlevel == null)
                userlevel = new Lebi_UserLevel();
            string where = "IsAnonymous<>1";
            if (key != "")
                where += " and (UserName like '%" + key + "%' or RealName like '%" + key + "%' or NickName like '%" + key + "%')";
            if (userlevelid > 0)
            {
                where += " and UserLevel_id =" + userlevelid + "";
            }
            PageSize = RequestTool.getpageSize(10);
            int recordCount = B_Lebi_User.Counts(where);
            PageString = Pager.GetPaginationStringForJS("reloadproducts({0},'" + key + "'," + userlevel.id + ");", page, PageSize, recordCount);
            users = B_Lebi_User.GetList(where, "", PageSize, page);

            userlevellimit = B_Lebi_Product_Price.GetModel("Product_id=" + product.id + "");
            if (userlevellimit == null)
                userlevellimit = new Lebi_Product_Price();

        }

        public string GetlevelName(int id)
        {
            Lebi_UserLevel model = B_Lebi_UserLevel.GetModel(id);
            if (model == null)
                return "";
            return Lang(model.Name);
        }


        public string Sub(string con, int len)
        {
            if (con.Length > len)
                con = con.Substring(0, len) + "...";
            return con;
        }
        public Lebi_Product_Price GetPriceInfo(int userid)
        {
            Lebi_Product_Price model = B_Lebi_Product_Price.GetModel("Product_id=" + product.id + " and User_id=" + userid + "");
            if (model == null)
                return new Lebi_Product_Price();
            return model;
        }
        public Lebi_Product_Price GetPriceInfo(Lebi_User user)
        {
            Lebi_Product_Price model = B_Lebi_Product_Price.GetModel("Product_id=" + product.id + " and User_id=" + user.id + "");
            if (model != null)
                return model;
            if (model == null)
                return new Lebi_Product_Price();
            return model;
        }
    }
}