using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

namespace Shop.admin.plugin.weixin
{
    /// <summary>
    /// 微信相关
    /// </summary>
    public partial class ajax : AdminAjaxBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }
        /// <summary>
        /// 保存菜单
        /// </summary>
        public void Menu_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            int IsLogin = RequestTool.RequestInt("IsLogin", 0);
            Lebi_weixin_menu model = B_Lebi_weixin_menu.GetModel(id);
            bool addflag = false;
            if (model == null)
            {
                model = new Lebi_weixin_menu();
                addflag = true;
            }
            model = B_Lebi_weixin_menu.BindForm(model);
            //if (model.parentid == 0)
            //    model.url = "";
            if (IsLogin == 1)
            {
                if (!model.url.Contains("weixinlogin=1"))
                {
                    if (model.url.Contains("?"))
                        model.url += "&weixinlogin=1";
                    else
                        model.url += "?weixinlogin=1";
                }
            }
            else
            {
                if (model.url.Contains("weixinlogin=1"))
                {
                    if (model.url.Contains("?"))
                        model.url = model.url.Replace("?weixinlogin=1", "");
                    else
                        model.url = model.url.Replace("&weixinlogin=1", "");
                }
            }
            if (addflag)
                B_Lebi_weixin_menu.Add(model);
            else
                B_Lebi_weixin_menu.Update(model);
            Response.Write("{\"msg\":\"OK\"}");

        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        public void Menu_Del()
        {
            int id = RequestTool.RequestInt("id", 0);

            Lebi_weixin_menu model = B_Lebi_weixin_menu.GetModel(id);

            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            B_Lebi_weixin_menu.Delete("id=" + id + " or parentid=" + id + "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 下载菜单
        /// </summary>
        public void Menu_download()
        {
            string msg = Shop.Platform.weixin.Instance.DownMenu();
            Response.Write("{\"msg\":\"" + msg + "\"}");
        }
        /// <summary>
        /// 发布菜单
        /// </summary>
        public void Menu_publish()
        {
            string msg = Shop.Platform.weixin.Instance.CreateMenu().errmsg;
            Response.Write("{\"msg\":\"" + msg + "\"}");
        }
    }
}