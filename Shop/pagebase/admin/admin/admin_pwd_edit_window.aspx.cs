using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.admin
{
    public partial class Admin_pwd_Edit_window : AdminAjaxBase
    {
        protected Lebi_Administrator model;
        protected List<Lebi_Admin_Group> groups;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Administrator.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Administrator();
            }

            groups = B_Lebi_Admin_Group.GetList("","Sort desc");
        }
    }
}