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


namespace Shop.Ajax
{
    /// <summary>
    /// 全局通用的操作
    /// </summary>
    public partial class js : ShopPage
    {
        public string langs = "";
        public void LoadPage()
        {
            if (!AjaxLoadCheck())
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("languagetags['长度不能小于']='" + Tag("长度不能小于") + "';\r\n");
            sb.Append("languagetags['长度不能大于']='" + Tag("长度不能大于") + "';\r\n");
            sb.Append("languagetags['请填写您的邮箱']='" + Tag("请填写您的邮箱") + "';\r\n");
            sb.Append("languagetags['邮箱格式错误']='" + Tag("邮箱格式错误") + "';\r\n");
            sb.Append("languagetags['登录']='" + Tag("登录") + "';\r\n");
            sb.Append("languagetags['系统异常']='" + Tag("系统异常") + "';\r\n");
            sb.Append("languagetags['正在处理']='" + Tag("正在处理") + "';\r\n");
            sb.Append("languagetags['字数']='" + Tag("字数") + "';\r\n");
            sb.Append("languagetags['操作成功']='" + Tag("操作成功") + "';\r\n");
            sb.Append("languagetags['长度限制']='" + Tag("长度限制") + "';\r\n");
            sb.Append("languagetags['已过期']='" + Tag("已过期") + "';\r\n");
            sb.Append("languagetags['正在处理']='" + Tag("正在处理") + "';\r\n");
            sb.Append("languagetags['加入常购清单']='" + Tag("加入常购清单") + "';\r\n");
            sb.Append("var path='" + WebPath.TrimEnd('/') + "';\r\n");
            if (ShopCache.GetMainSite().id != CurrentSite.id && CurrentSite.Domain != "")
                sb.Append("var sitepath='" + WebPath.Replace("//", "/").TrimEnd('/') + "';\r\n");
            else
                sb.Append("var sitepath='" + (WebPath + CurrentSite.Path).Replace("//", "/").TrimEnd('/') + "';\r\n");
            langs = sb.ToString();

        }



    }


}