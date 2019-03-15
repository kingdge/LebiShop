using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using Shop.Model;
using Shop.Tools;
using System.Linq;
using System.Collections.Specialized;
using System.Web.Script.Serialization;

namespace Shop.Bussiness
{
    public partial class Help_Content : ShopControl
    {
        public static List<Lebi_Page> models
        {
            get;
            set;
        }
        
    }
}