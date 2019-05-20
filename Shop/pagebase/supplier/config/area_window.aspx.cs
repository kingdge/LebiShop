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
    public partial class Area_window : SupplierAjaxBase
    {
        protected List<Lebi_Area> models;
        protected string notid;
        protected void Page_Load(object sender, EventArgs e)
        {
            notid = RequestTool.RequestString("notid");
            if (notid == "")
                notid = "0";
            int pid = RequestTool.RequestInt("pid",0);
            models = B_Lebi_Area.GetList("parentid=" + pid + " and id not in (lbsql{" + notid + "})", "");

        }
        /// <summary>
        /// 子区域数量
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public int SonArea(int pid) 
        {
            return B_Lebi_Area.Counts("Parentid="+pid);
        }

    }
}