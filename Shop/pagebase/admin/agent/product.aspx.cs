using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.agent
{
    public partial class product : AdminPageBase
    {

        protected string key;
        protected List<Lebi_Agent_Product> models;
        protected string PageString;
        protected int User_id;
        protected int status;
        protected void Page_Load(object sender, EventArgs e)
        {

            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            status = RequestTool.RequestInt("status", 0);
            string where = "1=1";
            if (key != "")
                where += " and User_UserName like '%" + key + "%'";
            if (status > 0)
            {
                if (status == 1)
                    where += " and User_id>0";
                if (status == 2)
                    where += " and User_id=0";
            }
            models = B_Lebi_Agent_Product.GetList(where, "", PageSize, page);
            int recordCount = B_Lebi_Agent_Product.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key + "&User_id=" + User_id, page, PageSize, recordCount);
        }

        public int CountSon(int pid)
        {
            return B_Lebi_Product.Counts("Product_id=" + pid + " and Product_id<>0");
        }
        public Lebi_Agent_Product_User GetAgentUser(int userid)
        {
            Lebi_Agent_Product_User auser = B_Lebi_Agent_Product_User.GetModel("User_id=" + userid + "");
            if (auser == null)
                auser = new Lebi_Agent_Product_User();
            return auser;
        }


    }
}