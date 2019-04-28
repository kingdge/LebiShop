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

namespace Shop.Admin.product
{
    public partial class productlimit_list_window : AdminAjaxBase
    {

        protected string PageString = "";
        protected List<Lebi_Product> products;
        protected int userid = 0;
        protected int userlevelid = 0;
        protected int typeid = 0;
        protected string key = "";
        protected int showall = 0;
        protected Lebi_User user;
        protected Lebi_UserLevel userlevel;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("product_user_limit", "商品会员权限"))
            {
                WindowNoPower();
            }
            userlevelid = RequestTool.RequestInt("userlevelid");
            userid = RequestTool.RequestInt("userid");
            typeid = RequestTool.RequestInt("typeid");
            showall = RequestTool.RequestInt("showall");
            key = RequestTool.RequestString("key");
            user = B_Lebi_User.GetModel(userid);
            userlevel = B_Lebi_UserLevel.GetModel(userlevelid);
            if (user == null)
                user = new Lebi_User();
            if (userlevel == null)
                userlevel = new Lebi_UserLevel();
            string where = "Product_id=0 and (IsDel!=1 or IsDel is null)";
            if (key != "")
                where += " and (Name like '%" + key + "%' or Number like '%" + key + "%' or Code like '%" + key + "%')";
            if (typeid > 0)
            {
                string tids = EX_Product.TypeIds(typeid);
                where += " and Pro_Type_id in (" + tids + ")";
            }

            PageSize = RequestTool.getpageSize(10);
            int recordCount = B_Lebi_Product.Counts(where);
            PageString = Pager.GetPaginationStringForJS("reloadproducts({0}," + typeid + ",'" + key + "'," + user.id + "," + userlevel.id + ");", page, PageSize, recordCount);
            products = B_Lebi_Product.GetList(where, "", PageSize, page);

        }

        public string GetUnitName(int id)
        {
            Lebi_Units model = B_Lebi_Units.GetModel(id);
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
        public Lebi_Product_Limit GetLimitInfo(int proid)
        {
            Lebi_Product_Limit model = null;
            if (user.id > 0)
            {
                model = B_Lebi_Product_Limit.GetModel("Product_id=" + proid + " and User_id=" + user.id + "");
                if (model == null)
                {
                    int c = B_Lebi_Product_Limit.Counts("User_id=" + user.id + "");
                    if (c == 0)
                    {
                        model = B_Lebi_Product_Limit.GetModel("Product_id=" + proid + " and  UserLevel_id=" + user.UserLevel_id + "");
                    }
                }
            }
            else
            {
                model = B_Lebi_Product_Limit.GetModel("Product_id=" + proid + " and  UserLevel_id=" + userlevel.id + "");
            }

            if (model == null)
                return new Lebi_Product_Limit();
            return model;
        }
    }
}