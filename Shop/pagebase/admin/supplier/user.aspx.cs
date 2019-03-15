using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Admin.Supplier
{
    public partial class Supplier_User : AdminPageBase
    {
        protected string key;
        protected int level_id;
        protected string dateFrom;
        protected string dateTo;
        protected int status;
        protected List<Lebi_Supplier> models;
        protected string PageString;
        protected string type = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("supplier_user_list", "商家列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            //lang = RequestTool.RequestString("lang");
            level_id = RequestTool.RequestInt("level_id", 0);
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            status = RequestTool.RequestInt("status", 0);
            type = RequestTool.RequestString("type");
            if (type == "")
                type = "supplier";
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            string where = "Supplier_Group_id in (select g.id from Lebi_Supplier_Group as g where g.type=lbsql{'" + type + "'})";
            if (key != "")
                where += " and (Name like lbsql{'%" + key + "%'} or Company like lbsql{'%" + key + "%'} or UserName like lbsql{'%" + key + "%'} or RealName like lbsql{'%" + key + "%'} or Phone like lbsql{'%" + key + "%'} or SupplierNumber like lbsql{'%" + key + "%'})";
            //if (lang != "")
            //    where += " and Language = '" + lang + "'";
            if (level_id > 0)
                where += " and Supplier_Group_id = " + level_id;
            if (status != 0)
                where += " and Type_id_SupplierStatus = " + status;
            if (dateFrom != "" && dateTo != "")
                where += " and (datediff(d,Time_Reg,'" + FormatDate(lbsql_dateFrom) + "')<=0 and datediff(d,Time_Reg,'" + FormatDate(lbsql_dateTo) + "')>=0)";

            models = B_Lebi_Supplier.GetList(where, "Time_Reg desc", PageSize, page);
            int recordCount = B_Lebi_Supplier.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&level_id=" + level_id + "&status=" + status + "&key=" + key + "&type=" + type, page, PageSize, recordCount);
        }
        public string Supplier_GroupOption(string level_id, string lang)
        {
            if (lang == "")
            {
                lang = CurrentLanguage.Code;
            }
            if (type == "")
                type = "supplier";
            List<Lebi_Supplier_Group> levels = B_Lebi_Supplier_Group.GetList("type=lbsql{'" + type + "'}", "Grade asc");
            string str = "";
            foreach (Lebi_Supplier_Group level in levels)
            {
                string sel = "";
                if (level_id == "" + level.id + "")
                    sel = "selected";
                str += "<option value=\"" + level.id + "\" " + sel + ">" + Shop.Bussiness.Language.Content(level.Name, lang) + "</option>";
            }
            return str;
        }
        /// <summary>
        /// 返回供应商分组名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GroupName(int id)
        {
            if (type == "")
                type = "supplier";
            List<Lebi_Supplier_Group> levels = B_Lebi_Supplier_Group.GetList("type=lbsql{'" + type + "'}", "Grade asc");
            Lebi_Supplier_Group group = B_Lebi_Supplier_Group.GetModel(id);
            if (group == null)
                return "";
            return Lang(group.Name);
        }
    }
}