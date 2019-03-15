using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Collections;
using System.Web.Script.Serialization;
namespace Shop.Admin.Config
{
    public partial class MyServices : AdminPageBase
    {

        protected List<UserService> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("License", "我的服务"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            string ps = SYS.LicensePackage;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                models = jss.Deserialize<List<UserService>>(ps);
            }
            catch
            {
                models = new List<UserService>();
            }
            if (models == null)
                models = new List<UserService>();
        }

        protected Hashtable ServiceName
        {
            get
            {
                Hashtable ht = new Hashtable();
                ht.Add("biaozhun", "标准授权");
                ht.Add("zengqiang", "增强授权");
                ht.Add("cn", "中文");
                ht.Add("en", "英文");
                ht.Add("jp", "日文");
                return ht;
            }

        }
    }
}