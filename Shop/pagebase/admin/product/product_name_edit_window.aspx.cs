using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.product
{
    public partial class product_Name_Edit_window : AdminAjaxBase
    {
        protected Lebi_Product model;
        protected int id;
        protected int pid;
        protected int randnum;
        protected string removelang;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestTool.RequestInt("id", 0);
            pid = RequestTool.RequestInt("pid", 0);
            randnum = RequestTool.RequestInt("randnum", 0);
            removelang = RequestTool.RequestString("removelang");
            if (id == 0)
            {
                if (!EX_Admin.Power("product_add", "添加商品"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("product_edit", "编辑商品"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_Product.GetModel(id);
            if (model == null)
            {
                Response.Write("参数错误");
                Response.End();
            }
        }
        /// <summary>
        /// 返回排除传递的语种
        /// </summary>
        /// <returns></returns>
        public static List<Lebi_Language> Languages()
        {
            //List<Lebi_Language> models = new List<Lebi_Language>();
            string removelang = RequestTool.RequestString("removelang");
            string where = "";
            where = "IsUsed=1";
            if (removelang != "")
                where += " and Code <> '" + removelang + "'";
            List<Lebi_Language> tmodels = B_Lebi_Language.GetList(where, "IsDefault desc,id desc");

            //foreach (Lebi_Language model in tmodels)
            //{
            //    if (Shop.LebiAPI.Service.Instanse.Check(model.Code))
            //        models.Add(model);
            //}
            return tmodels;
        }
    }
}