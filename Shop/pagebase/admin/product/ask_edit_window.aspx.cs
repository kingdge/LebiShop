using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.Product
{
    public partial class Ask_Edit_window : AdminAjaxBase
    {
        protected Lebi_Comment model;
        protected List<Lebi_Comment> models;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("ask_edit", "编辑商品咨询"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Comment.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Comment();
            }
            models = B_Lebi_Comment.GetList("Parentid = "+ id, "id desc", PageSize, page);
        }
        public Lebi_Product GetProduct(int id)
        {
            Lebi_Product model = B_Lebi_Product.GetModel(id);
            if (model == null)
                model = new Lebi_Product();
            return model;
        }
    }
}