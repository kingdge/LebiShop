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
    public partial class areaagent_request : AdminPageBase
    {

        protected string key;
        protected List<Lebi_Agent_Product_request> models;
        protected string PageString;
        protected int User_id;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            User_id = RequestTool.RequestInt("User_id", 0);
            string where = "1=1";
            models = B_Lebi_Agent_Product_request.GetList(where, "", PageSize, page);
            int recordCount = B_Lebi_Agent_Product_request.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key + "&User_id=" + User_id, page, PageSize, recordCount);
        }

        public int CountSon(int pid)
        {
            return B_Lebi_Product.Counts("Product_id=" + pid + " and Product_id<>0");
        }



    }
}