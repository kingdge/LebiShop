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
    public partial class Cnzz : AdminPageBase
    {
        string domain = HttpContext.Current.Request.Url.Host;
        string key = "Jsit7Kd3";
        string cms = "56770";
        protected string returnValue = "";
        protected Lebi_Cnzz model;
        protected string cnzzurl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("cnzz_view", "CNZZ统计查看"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            model = B_Lebi_Cnzz.GetList("", "").FirstOrDefault();
            if (model != null)
            {
                string user = model.Ccontent;
                string[] list = user.Split('@');
                cnzzurl = "http://intf.cnzz.com/user/companion/56770_login.php?site_id=" + list[0] + "&password=" + list[1] + "&cms=56770";
            }
            //if (model == null)
            //{
            //    datalist.Attributes.Add("style", "display:block");
            //    this.iframe1.Visible = false;
            //}else{
            //    string user = model.Ccontent;
            //    string[] list = user.Split('@');
            //    this.iframe1.Attributes.Add("src", "http://intf.cnzz.com/user/companion/56770_login.php?site_id=" + list[0] + "&password=" + list[1] + "&cms=56770");
            //}
            //ds = bll.detail();
            //if (ds.Tables[0].Rows.Count == 0)
            //{
            //    datalist.Attributes.Add("style", "display:block");
            //    this.iframe1.Visible = false;
            //}
            //else
            //{
            //    if (ds.Tables[0].Rows[0]["state"].ToString() == "0")
            //    {
            //        datalist.Attributes.Add("style", "display:block");
            //        this.iframe1.Visible = false;
            //    }
            //    else
            //    {
            //        string user = ds.Tables[0].Rows[0]["ccontent"].ToString();
            //        string[] list = user.Split('@');
            //        this.iframe1.Attributes.Add("src", "http://intf.cnzz.com/user/companion/56770_login.php?site_id=" + list[0] + "&password=" + list[1] + "&cms=56770");
            //    }
            //}
        }
    }
}