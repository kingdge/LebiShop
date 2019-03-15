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
    public partial class Transport_Price : AdminPageBase
    {

        protected int OfflinePay;
        protected int OnePrice;
        protected List<Lebi_Transport_Price> models;
        protected Lebi_Transport tmodel;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("transport_price_list", "配送区域列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            OfflinePay = RequestTool.RequestInt("OfflinePay",2);
            OnePrice = RequestTool.RequestInt("OnePrice", 2);
            OfflinePay = OfflinePay > 2 ? 2 : OfflinePay;
            OnePrice = OnePrice > 2 ? 2 : OnePrice;

            int tid = RequestTool.RequestInt("tid", 0);
            tmodel = B_Lebi_Transport.GetModel(tid);
            string where = "Supplier_id=0 and Transport_id=" + tmodel.id;
            //if (pid > 0)
            //    where += " and Parentid="+pid;
            if (OnePrice == 0 || OnePrice==1)
                where += " and IsOnePrice=" + OnePrice;
            if (OfflinePay == 0 || OfflinePay == 1)
                where += " and IsCanofflinePay=" + OfflinePay;
            models = B_Lebi_Transport_Price.GetList(where, "id asc", PageSize, page);
            int recordCount = B_Lebi_Transport_Price.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&tid="+tid+"&OnePrice=" + OnePrice + "&OfflinePay=" + OfflinePay, page, PageSize, recordCount);
            
        }
        public string AreaName(int id)
        {
            string str = "";
            Lebi_Area area = B_Lebi_Area.GetModel(id);
            if (area != null)
            {
                str = area.Name + "> ";
                if (area.Parentid > 0)
                {
                    str = AreaName(area.Parentid) + str;
                }
            }
            return str;
        }


        

    }
}