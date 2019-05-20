using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.Config
{
    public partial class OnLinePay : SupplierPageBase
    {
        protected string lang;
        protected string key;
        protected int show;
        protected int pic;
        protected List<Lebi_OnlinePay> models;
        protected string PageString;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("onlinepay_list", "在线支付列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PageSize = RequestTool.getpageSize(25);
            lang = RequestTool.RequestString("lang");
            key = RequestTool.RequestString("key");
            show = RequestTool.RequestInt("show",2);
            pic = RequestTool.RequestInt("pic", 2);
            string where = "IsUsed=1 and parentid=0 and Code!='weixinpay'";
            models = B_Lebi_OnlinePay.GetList(where, "id asc", PageSize, page);
            int recordCount = B_Lebi_OnlinePay.Counts(where);

            PageString =Shop.Bussiness.Pager.GetPaginationString("?page={0}&show=" + show + "&key=" + key + "&pic=" + pic, page, PageSize, recordCount);
            
        }

        public Lebi_OnlinePay Getpay(int id)
        {
            Lebi_OnlinePay model = B_Lebi_OnlinePay.GetModel("Supplier_id="+CurrentSupplier.id+" and parentid="+id);
            if (model == null)
                model = new Lebi_OnlinePay();
            return model;
        }
    }
}