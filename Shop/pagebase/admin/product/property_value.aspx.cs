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
    public partial class Property_Value : AdminPageBase
    {

        protected List<Lebi_ProPerty> models;
        protected Lebi_ProPerty pmodel;
        protected Lebi_Type tmodel;
        protected string PageString;
        protected int pid;
        protected string key;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("property_add", "添加属性规格") || !EX_Admin.Power("property_edit", "编辑属性规格"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            pid = RequestTool.RequestInt("pid",0);
            key = RequestTool.RequestString("key");
            pmodel = B_Lebi_ProPerty.GetModel(pid);
            string where = "parentid=" + pid;
            if (key != "")
                where += " and (Name like lbsql{'%" + key + "%'} or Code like lbsql{'%" + key + "%'})";
            models = B_Lebi_ProPerty.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_ProPerty.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&pid=" + pid + "&key=" + key + "", page, PageSize, recordCount);
            tmodel = B_Lebi_Type.GetModel(pmodel.Type_id_ProPertyType);

        }

      
    }
}