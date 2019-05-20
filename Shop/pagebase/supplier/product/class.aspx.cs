using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Model;
using Shop.Bussiness;
using Shop.Tools;
namespace Shop.supplier.product
{
    public partial class Class : SupplierPageBase
    {
        protected string key;
        protected string List;
        protected List<Lebi_Supplier_ProductType> PTypes;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_pro_type", "商品分类"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PTypes = GeteList(0);
        }
        public List<Lebi_Supplier_ProductType> GeteList(int pid)
        {

            //string str = "";

            List<Lebi_Supplier_ProductType> types = B_Lebi_Supplier_ProductType.GetList("parentid=" + pid + " and Supplier_id = " + CurrentSupplier.id + "", "Sort desc");
            return types;
            ////将根节点进行遍历
            //foreach (Lebi_Supplier_ProductType t in types)
            //{

            //    string caozuo = "<a href=\"javascript:Edit(" + t.id + ",0)\">" + Tag("添加子类") + "</a> |  <a href=\"javascript:Edit(0," + t.id + ")\">" + Tag("编辑") + "</a> | <a href=\"javascript:Del(" + t.id + ")\">" + Tag("删除") + "</a>";
            //    str += "<tr class=\"list\" name=\"tr" + t.parentid + "\" id=\"tr" + t.id + "\">";
            //    str += "<td style=\"text-align:center\"><input type='checkbox' id=\"check" + t.id + "\" value='" + t.id + "' name='id' del=\"del\" /></td>";
            //    str += "<td>" + t.id + "</td>";
            //    str += "<td>";
            //    if (t.ImageUrl != "")
            //    {
            //        str += "<img src=\"" + t.ImageUrl + "\" height=\"16\" />&nbsp;";
            //    }
            //    str += Language.Content(t.Name, CurrentLanguage.Code) + "</td>";

            //    str += "<td>" + t.Sort + "</td>";

            //    str += "<td>" + caozuo + "</td></tr>";

            //    //CreateSunNode(int.Parse(ds.Tables[0].Rows[i]["id"].ToString()), ",0", ss);
            //}
            //return str;
        }

        /// <summary>
        /// 某个结点下的所有结点
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public  string findpath(int pid)
        {
            string str = "";
            List<Lebi_Pro_Type> types = EX_Product.Types(pid);
            if (types.Count == 0)
                return "";
            foreach (Lebi_Pro_Type t in types)
            {
                if (str == "")
                    str = t.id.ToString();
                else
                    str += "," + t.id;
                string f = findpath(t.id);
                //if (f != "")
                str += "," + f;
            }

            return str;

        }
        public static int TypeProductCount(int supplier_id,int id)
        {
            string where = "Product_id=0 and Supplier_id = " + supplier_id + "";
            if (id > 0)
            {
                if (DataBase.DBType == "sqlserver")
                {
                    where += " and Charindex('," + id + ",',','+Supplier_ProductType_ids+',')>0";
                }
                if (DataBase.DBType == "access")
                {
                    where += " and Instr(','+Supplier_ProductType_ids+',','," + id + ",')>0";
                }
            }
            else
            {
                where += " and Supplier_ProductType_ids = ''";
            }
            return B_Lebi_Product.Counts(where);
            //return TypeIds(id);
        }
    }
}