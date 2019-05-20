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
    public partial class Property : SupplierPageBase
    {
        protected List<Lebi_ProPerty> models;
        protected List<Lebi_ProPerty_Tag> tags;
        protected List<Lebi_Type> types;
        protected Lebi_Type tmodel;
        protected string PageString;
        protected int tid;
        protected string key;
        protected string tag;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_property", "属性规格"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            tid = RequestTool.RequestInt("tid", 133);
            key = RequestTool.RequestSafeString("key");
            tag = RequestTool.RequestSafeString("tag");
            PageSize = RequestTool.getpageSize(25);
            string where = "Supplier_id = "+ CurrentSupplier.id +" and parentid = 0 and Type_id_ProPertyType = " + tid + "";
            if (key != "")
                where += " and (Name like lbsql{'%" + key + "%'} or Code like lbsql{'%" + key + "%'})";
            if (tag != "")
            {
                if (tag == "none")
                    where += " and (Tag = '' or Tag is null)";
                else
                    where += " and Tag = lbsql{'" + tag + "'}";
            }
            models = B_Lebi_ProPerty.GetList(where, "Sort desc", PageSize, page);
            types = B_Lebi_Type.GetList("Class='ProPertyType' and id in(131,133)", "Sort desc");
            int recordCount = B_Lebi_ProPerty.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationString("?page={0}&tid=" + tid + "&tag=" + tag + "&key=" + key + "", page, PageSize, recordCount);
            tmodel = B_Lebi_Type.GetModel(tid);
            tags = B_Lebi_ProPerty_Tag.GetList("Type_id_ProPertyType = " + tid + " and Supplier_id = "+ CurrentSupplier.id, "Sort desc");
        }
        public List<Lebi_ProPerty> GetModels(int type)
        {
            List<Lebi_ProPerty> models;
            string where = "Type_id_ProPertyType=" + type + " and parentid=0";
            models = B_Lebi_ProPerty.GetList(where, "Sort desc");
            return models;

        }
        public string GetProPerty(int pid, int top = 0)
        {
            List<Lebi_ProPerty> models;
            string where = "parentid=" + pid + "";
            if (top == 0)
                models = B_Lebi_ProPerty.GetList(where, "Sort desc");
            else
                models = B_Lebi_ProPerty.GetList(where, "Sort desc", top, 1);
            string str = "";

            foreach (Lebi_ProPerty model in models)
            {
                if (str == "")
                    str = Language.Content(model.Name, CurrentLanguage.Code);
                else
                    str += ", " + Language.Content(model.Name, CurrentLanguage.Code);
                if (model.Code != "")
                    str += "(" + model.Code + ")";

            }
            return str;

        }


    }
}