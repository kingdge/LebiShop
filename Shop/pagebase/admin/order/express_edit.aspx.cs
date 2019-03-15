using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.order
{
    public partial class Express_Edit : AdminPageBase
    {
        protected Lebi_Express model;
        protected int id;
        protected int random = new Random().Next();
        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("express_add", "添加模板"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("express_edit", "编辑模板"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_Express.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Express();
            }
        }
    }
}