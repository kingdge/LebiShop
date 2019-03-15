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

namespace Shop.Admin.Ajax
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    public partial class ajax_search : AdminAjaxBase
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
        /// 处理搜索会员的条件
        /// </summary>
        public void UserSearch()
        {
            SearchUser su = new SearchUser(CurrentAdmin, CurrentLanguage.Code);
            string url = su.URL;
            Response.Write("{\"msg\":\"OK\",\"url\":\"" + url + "\"}");
        }
        /// <summary>
        /// 处理搜索卡券的条件
        /// </summary>
        public void CardSearch()
        {
            SearchCard su = new SearchCard(CurrentAdmin, CurrentLanguage.Code);
            string url = su.URL;
            Response.Write("{\"msg\":\"OK\",\"url\":\"" + url + "\"}");
        }
        /// <summary>
        /// 处理搜索商品的条件
        /// </summary>
        public void ProductSearch()
        {
            SearchProduct su = new SearchProduct(CurrentAdmin, CurrentLanguage.Code);
            string url = su.URL;
            Response.Write("{\"msg\":\"OK\",\"url\":\"" + url + "\"}");
        }
        /// <summary>
        /// 处理搜索订单的条件
        /// </summary>
        public void OrderSearch()
        {
            SearchOrder su = new SearchOrder(CurrentAdmin, CurrentLanguage.Code);
            string url = su.URL;
            Response.Write("{\"msg\":\"OK\",\"url\":\"" + url + "\"}");
        }
    }
}