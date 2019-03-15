using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;
namespace Shop.supplier.product
{
    public partial class product_Edit_list : SupplierAjaxBase
    {
        protected List<Lebi_Product> models;
        protected List<Lebi_ProPerty> ggs;
        protected Lebi_Product modelp;
        protected Lebi_Language_Code modelLan;
        protected int pid;
        protected int tid;
        protected int randnum;
        protected void Page_Load(object sender, EventArgs e)
        {
            pid = RequestTool.RequestInt("pid", 0);
            tid = RequestTool.RequestInt("tid", 0);
            randnum = RequestTool.RequestInt("randnum", 0);
            if (pid == 0 || (pid > 0 && randnum > 0))
            {
                if (!Power("supplier_product_add", "添加商品"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!Power("supplier_product_edit", "编辑商品"))
                {
                    WindowNoPower();
                }
            }
            if (pid > 0 && randnum > 0)
            {
                modelp = B_Lebi_Product.GetModel(randnum);
            }
            else
            {
                modelp = B_Lebi_Product.GetModel(pid);
            }
            if (modelp == null)
            {
                modelp = new Lebi_Product();
                modelp.ProPertyMain = RequestTool.RequestInt("ProPertyMain", 0);
                modelp.ProPerty131 = RequestTool.RequestString("ProPerty131");
            }
            if (tid == 0)
            {
                tid = modelp.Pro_Type_id;
            }
            string property = EX_Product.ProductType_ProPertystr(tid, CurrentSupplier.id);
            if (property != "")
            {
                try
                {
                    ggs = B_Lebi_ProPerty.GetList("Type_id_ProPertyType=131 and id in (" + property + ")", "Sort desc");
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    ggs = new List<Lebi_ProPerty>();
                }
            }
            if (ggs == null)
            {
                ggs = new List<Lebi_ProPerty>();
            }
            modelLan = Language.DefaultLanguage();
            string where = "Product_id!=0 and (IsDel!=1 or IsDel is null)";
            if (pid == 0 || (pid > 0 && randnum > 0))
            {
                where += " and Supplier_id = " + CurrentSupplier.id + " and Product_id=" + randnum + "";
            }
            else
            {
                where += " and Supplier_id = " + CurrentSupplier.id + " and Product_id=" + pid + "";
            }
            models = B_Lebi_Product.GetList(where, "");
        }
        /// <summary>
        /// 返回规格
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public string getproperty(string ids)
        {
            string str = "";
            List<Lebi_ProPerty> list;
            try
            {
                list = B_Lebi_ProPerty.GetList("id in (lbsql{" + ids + "})", "");
            }
            catch (System.Data.SqlClient.SqlException)
            {
                list = new List<Lebi_ProPerty>();
            }
            foreach (Lebi_ProPerty model in list)
            {
                str += Language.Content(model.Name, CurrentLanguage.Code) + ", ";
            }
            return str;
        }
        public string Getpro131List(int pid)
        {
            string str = "";
            List<Lebi_ProPerty> ps = B_Lebi_ProPerty.GetList("parentid=" + pid + "", "Sort desc");
            foreach (Lebi_ProPerty p in ps)
            {
                string sel = "";

                if (("," + modelp.ProPerty131 + ",").Contains("," + p.id + ","))
                    sel = "checked";
                str += " <label><input type=\"checkbox\" name=\"Property131\" shop=\"true\" " + sel + " value=\"" + p.id + "\"" + " />";
                if (p.ImageUrl != "")
                    str += "<img src=\"" + Image(p.ImageUrl, 16, 16) + "\">";
                str += Language.Content(p.Name, CurrentLanguage.Code) + "</label>&nbsp;";
            }
            return str;
        }
    }
}