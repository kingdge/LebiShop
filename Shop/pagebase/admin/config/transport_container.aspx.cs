using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.storeConfig
{
    public partial class Transport_Container : AdminPageBase
    {
        protected string key;
        protected List<Lebi_Transport_Container> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("transport_list", "配送方式列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            key = RequestTool.RequestString("key");
            PageSize = RequestTool.getpageSize(25);
            string where = "1=1";
            //if (pid > 0)
            //    where += " and Parentid="+pid;
            if (key != "")
                where += " and [Name] like lbsql{'%" + key + "%'}";
            models = B_Lebi_Transport_Container.GetList(where, "Volume desc", PageSize, page);
            int recordCount = B_Lebi_Transport_Container.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&key=" + key , page, PageSize, recordCount);
            
        }
    }
}