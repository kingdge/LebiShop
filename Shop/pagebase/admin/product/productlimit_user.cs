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
    public partial class productlimit_user : AdminPageBase
    {

        protected int proid;
        protected int userid;

        protected List<Lebi_Product_Limit> models;
        protected string PageString;
        Lebi_User cuser;
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
            userid = RequestTool.RequestInt("userid");

            string where = "UserLevel_id=0";
            if (proid > 0)
                where += " and Product_id=" + proid;
            if (userid > 0)
                where += " and User_id=" + userid;
            models = B_Lebi_Product_Limit.GetList(where, "", PageSize, page);

            int recordCount = B_Lebi_Product_Limit.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&userid=" + userid + "&proid=" + proid, page, PageSize, recordCount);
            cuser = GetUser(userid);
            cproduct = GetPro(proid);
            if (cuser.id > 0)
                ename = cuser.UserName;
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
        public Lebi_User GetUser(int id)
        {
            Lebi_User pro = B_Lebi_User.GetModel(id);
            if (pro == null)
                return new Lebi_User();
            return pro;
        }
    }
}