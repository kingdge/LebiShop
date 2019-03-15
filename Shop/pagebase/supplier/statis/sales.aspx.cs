using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.Statis
{
    public partial class Statis_Sales : SupplierPageBase
    {
        protected string key;
        protected string dateFrom;
        protected string dateTo;
        protected int Pay_id;
        protected int Transport_id;
        protected int Y_dateFrom;
        protected int M_dateFrom;
        protected int D_dateFrom;
        protected int Y_dateTo;
        protected int M_dateTo;
        protected int D_dateTo;
        protected int display;
        protected List<Lebi_Pay> pays;
        protected List<Lebi_Transport> transports;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_statis", "数据统计"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            display = RequestTool.RequestInt("display", 0);
            Pay_id = RequestTool.RequestInt("Pay_id",0);
            Transport_id = RequestTool.RequestInt("Transport_id", 0);
            dateFrom = RequestTool.RequestString("dateFrom");
            if (dateFrom == "")
                dateFrom = System.DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            dateTo = RequestTool.RequestString("dateTo");
            if (dateTo == "")
                dateTo = System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            Y_dateFrom = Convert.ToDateTime(dateFrom).Year;
            M_dateFrom = Convert.ToDateTime(dateFrom).Month;
            D_dateFrom = Convert.ToDateTime(dateFrom).Day;
            Y_dateTo = Convert.ToDateTime(dateTo).Year;
            M_dateTo = Convert.ToDateTime(dateTo).Month;
            D_dateTo = Convert.ToDateTime(dateTo).Day;
            pays = B_Lebi_Pay.GetList("", "Sort desc");
            transports = B_Lebi_Transport.GetList("", "Sort desc");
        }
    }
}