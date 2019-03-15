using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.theme
{
    public partial class PageList : AdminPageBase
    {
        protected List<Lebi_Theme_Page> models;
        protected string key;
        protected void Page_Load(object sender, EventArgs e)
        {
            key = RequestTool.RequestString("key");
            string where = "";
            if (key != "")
                where = " (Code like lbsql{'%" + key + "%'} or Name like lbsql{'%" + key + "%'} or PageName like lbsql{'%" + key + "%'})";
            models = B_Lebi_Theme_Page.GetList(where, "Sort desc,Code asc");
            foreach (Lebi_Theme_Page p in models)
            {
                if (p.Name.Contains("[{\"") && Language.Content(p.Name, "CN") != "")
                {
                    p.Name = Language.Content(p.Name, "CN");
                    B_Lebi_Theme_Page.Update(p);
                }
            }
        }

    }
}