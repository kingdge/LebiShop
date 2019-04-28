using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.product
{
    public partial class productlimit_userlevel : AdminPageBase
    {

        protected int proid;
        protected int userlevelid;

        protected List<Lebi_Product_Limit> models;
        protected string PageString;
        Lebi_UserLevel cuser;
        Lebi_Product cproduct;
        protected string ename = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("product_user_limit", "商品会员权限"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PageSize = RequestTool.getpageSize(25);
            proid = RequestTool.RequestInt("proid");
            userlevelid = RequestTool.RequestInt("userlevelid");

            string where = "User_id=0";
            if (proid > 0)
                where += " and Product_id=" + proid;
            if (userlevelid > 0)
                where += " and UserLevel_id=" + userlevelid;
            models = B_Lebi_Product_Limit.GetList(where, "", PageSize, page);

            int recordCount = B_Lebi_Product_Limit.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&userlevelid=" + userlevelid + "&proid=" + proid, page, PageSize, recordCount);
            cuser = GetUserLevel(userlevelid);
            cproduct = GetPro(proid);
            if (cuser.id > 0)
                ename = Lang(cuser.Name);
            if (cproduct.id > 0)
                ename = Lang(cproduct.Name);
        }


        public Lebi_Product GetPro(int id)
        {
            Lebi_Product pro = B_Lebi_Product.GetModel(id);
            if (pro == null)
                return new Lebi_Product();
            return pro;
        }
        public Lebi_UserLevel GetUserLevel(int id)
        {
            Lebi_UserLevel pro = B_Lebi_UserLevel.GetModel(id);
            if (pro == null)
                return new Lebi_UserLevel();
            return pro;
        }
    }
}