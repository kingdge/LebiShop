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
    public partial class ProPerty_Category : SupplierPageBase
    {
        protected string lang;
        protected string key;
        protected int id;
        protected List<Lebi_Supplier_ProPerty> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_property", "属性规格"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PageSize = RequestTool.getpageSize(25);
            lang = RequestTool.RequestString("lang");
            key = RequestTool.RequestString("key");
            id = RequestTool.RequestInt("id", 0);
            string where = "Supplier_id = " + CurrentSupplier.id + " and ','+ProPerty+',' like '%," + id + ",%'";
            if (key != "")
                where += " and Pro_Type_id in (select Lebi_Pro_Type.id from Lebi_Pro_Type where Lebi_Pro_Type.Name like lbsql{'%" + key + "%'} and Lebi_Pro_Type.id = Pro_Type_id)";
            models = B_Lebi_Supplier_ProPerty.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Supplier_ProPerty.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationString("?page={0}&id=" + id +"&key=" + key, page, PageSize, recordCount);
        }
        /// <summary>
        /// 某个商品分类的路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Categorypath(int id)
        {
            Shop.Model.Lebi_Pro_Type pro_type = Shop.Bussiness.B_Lebi_Pro_Type.GetModel(id);
            if (pro_type == null) { pro_type = new Shop.Model.Lebi_Pro_Type(); }
            string str = EX_Product.TypePath(pro_type, id.ToString());
            string[] arr = str.Split(',');
            string res="";
            for (int i = 0; i < arr.Length; i++)
            {
                Lebi_Pro_Type model = B_Lebi_Pro_Type.GetModel("id = " + int.Parse(arr[i]) + " and IsShow = 1");
                if (model == null)
                    continue;
                res += Lang(model.Name);
                if (i < arr.Length-1) { res += " &raquo; "; }
            }
            return res;
        }
    }
}