using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Web.UI.HtmlControls;
using System.Linq;

namespace Shop.Bussiness
{
    public class InstallBase : System.Web.UI.Page
    {
        protected string version = "6";
        protected string version_son = "1.00";
        public InstallBase()
        {
            
        }
    }
}