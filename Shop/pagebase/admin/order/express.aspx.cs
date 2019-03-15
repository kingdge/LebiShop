using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.order
{
    public partial class Express : AdminPageBase
    {
        protected string key;
        protected List<Lebi_Express> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("express_list", "模板列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            string where = "1=1";
            if (key != "")
                where += " and Name like lbsql{'%" + key + "%'}";
            models = B_Lebi_Express.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Express.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);
            
        }
        public Lebi_Transport GetName(int transport_id)
        {
            Lebi_Transport transport = B_Lebi_Transport.GetModel(transport_id);
            if (transport == null)
                transport = new Lebi_Transport();
            return transport;
        }
    }
}