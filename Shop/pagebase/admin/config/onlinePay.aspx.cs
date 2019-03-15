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
    public partial class OnLinePay : AdminPageBase
    {
        protected string lang;
        protected string key;
        protected int show;
        protected int pic;
        protected List<Lebi_OnlinePay> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("onlinepay_list", "在线支付列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            lang = RequestTool.RequestString("lang");
            key = RequestTool.RequestString("key");
            show = RequestTool.RequestInt("show", 2);
            pic = RequestTool.RequestInt("pic", 2);
            string where = "parentid=0";
            models = B_Lebi_OnlinePay.GetList(where, "IsUsed desc,Sort desc", PageSize, page);
            int recordCount = B_Lebi_OnlinePay.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&show=" + show + "&key=" + key + "&pic=" + pic, page, PageSize, recordCount);

        }

        public List<Lebi_OnlinePay> GetPays(int id)
        {
            return B_Lebi_OnlinePay.GetList("parentid=" + id, "IsUsed desc,Sort desc");
        }

    }
}