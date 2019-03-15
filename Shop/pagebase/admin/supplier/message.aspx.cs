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
    public partial class Message : AdminPageBase
    {
        protected string lang;
        protected string type;
        protected string type_id;
        protected string key;
        protected List<Lebi_Message> models;
        protected string PageString;
        protected string dateFrom;
        protected string dateTo;
        protected int user_id;
        protected Lebi_Supplier modeluser;
        protected string UserName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("supplier_message_list", "站内信列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            lang = RequestTool.RequestString("lang");
            key = RequestTool.RequestString("key");
            type = RequestTool.RequestString("type");
            type_id = RequestTool.RequestString("type_id");
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            user_id = RequestTool.RequestInt("user_id", 0);
            if (user_id > 0)
            {
                modeluser = B_Lebi_Supplier.GetModel(user_id);
                if (modeluser == null)
                {
                    UserName = "";
                }
                else
                {
                    UserName = modeluser.UserName;
                }
            }
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            string where = "Supplier_id > 0";
            if (key != "")
                where += " and (Title like lbsql{'%" + key + "%'} or User_Name_To like lbsql{'%" + key + "%'} or User_Name_From like lbsql{'%" + key + "%'})";
            if (user_id > 0)
                where += " and (User_id_To = " + user_id + " or User_id_From = " + user_id + ")";
            if (lang != "")
                where += " and Language = lbsql{'" + lang + "'}";
            if (type == "0")
                where += " and User_id_To =0";
            if (type == "1")
                where += " and User_id_From =0";
            if (type_id !="")
                where += " and Message_Type_id = " + type_id;
            if (dateFrom != "" && dateTo != "")
                where += " and (datediff(d,Time_Add,'" + FormatDate(lbsql_dateFrom) + "')<=0 and datediff(d,Time_Add,'" + FormatDate(lbsql_dateTo) + "')>=0)";
            models = B_Lebi_Message.GetList(where, "Time_Add desc", PageSize, page);
            int recordCount = B_Lebi_Message.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&user_id="+ user_id +"&type=" + type + "&lang=" + lang + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&type_id=" + type_id + "&key=" + key, page, PageSize, recordCount);
        }
        public string Message_TypeOption(string type_id,string lang)
        {
            if (lang == "")
            {
                lang = CurrentLanguage.Code;
            }
            List<Lebi_Message_Type> models = B_Lebi_Message_Type.GetList("", "");
            string str = "";
            foreach (Lebi_Message_Type model in models)
            {
                string sel = "";
                if (type_id == ""+model.id+"")
                    sel = "selected";
                str += "<option value=\"" + model.id + "\" " + sel + ">" + Shop.Bussiness.Language.Content(model.Name, lang) + "</option>";
            }
            return str;
        }
    }
}