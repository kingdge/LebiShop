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
    public partial class product_price : AdminPageBase
    {
        protected int id;
        protected int userid;
        protected List<Lebi_Product_Price> models;
        protected string PageString;
        Lebi_User cuser;
        Lebi_Product cproduct;
        protected string ename = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("product_user_price", "商品会员价格"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PageSize = RequestTool.getpageSize(25);
            id = RequestTool.RequestInt("id");
            userid = RequestTool.RequestInt("userid");
            string where = "1=1";
            if (id > 0)
                where += " and Product_id=" + id;
            if (userid > 0)
                where += " and User_id=" + userid;
            models = B_Lebi_Product_Price.GetList(where, "", PageSize, page);
            int recordCount = B_Lebi_Product_Price.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&userid=" + userid + "&id=" + id, page, PageSize, recordCount);
            cuser = GetUser(userid);
            cproduct = GetPro(id);
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