using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;
using Shop.Bussiness;
using Shop.Tools;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

namespace Shop.supplier.Ajax
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    public partial class ajax_search : SupplierAjaxBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Shop.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }
        /// <summary>
        /// 处理搜索商品的条件
        /// </summary>
        public void ProductSearch()
        {
            SearchProduct su = new SearchProduct(CurrentSupplier, CurrentLanguage.Code);
            string url = su.URL;
            Response.Write("{\"msg\":\"OK\",\"url\":\"" + url + "\"}");
        }
    }
}