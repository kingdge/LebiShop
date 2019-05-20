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
    public partial class subproduct_list : SupplierAjaxBase
    {
        protected List<Lebi_Product> models;
        protected Lebi_Product modelp;
        protected Lebi_Language_Code modelLan;
        protected int pid;
        protected int tid;
        protected int randnum;
        protected void Page_Load(object sender, EventArgs e)
        {
            pid = RequestTool.RequestInt("pid",0);
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
                modelp = B_Lebi_Product.GetModel("(IsDel!=1 or IsDel is null) and Supplier_id = " + CurrentSupplier.id + " and id = " + randnum);
            }
            else
            {
                modelp = B_Lebi_Product.GetModel("(IsDel!=1 or IsDel is null) and Supplier_id = " + CurrentSupplier.id + " and id = " + pid);
            }
            if (modelp == null)
            {
                modelp = new Lebi_Product();
                modelp.ProPertyMain = RequestTool.RequestInt("ProPertyMain",0);
                modelp.ProPerty131 = RequestTool.RequestString("ProPerty131");
            }
            modelLan = Language.DefaultLanguage();
            string where = "(IsDel!=1 or IsDel is null)";
            if (pid == 0 || (pid > 0 && randnum > 0))
            {
                where += " and Supplier_id = " + CurrentSupplier.id + " and Product_id=" + randnum + " and Product_id!=0";
            }else{
                where += " and Product_id=" + pid + " and Product_id!=0";
            }
            models = B_Lebi_Product.GetList(where,"");
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
                list = B_Lebi_ProPerty.GetList("id in (lbsql{" + ids + "})", "parentSort desc");
            }
            catch (System.Data.SqlClient.SqlException)
            {
                list = new List<Lebi_ProPerty>();
            }
            foreach (Lebi_ProPerty model in list)
            {
                str += Language.Content(model.Name,CurrentLanguage.Code)+", ";
            }
            return str;
        }
    }
}