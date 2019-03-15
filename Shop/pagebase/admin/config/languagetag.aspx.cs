using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.storeConfig
{
    public partial class LanguageTag : AdminPageBase
    {
        protected string lang;
        protected int type;
        protected string key;
        protected string mode;
        protected List<Lebi_Language_Tag> models;
        protected string PageString;

        protected List<Lebi_Language_Code> langs;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("language_tag_list", "语言标签列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            mode = RequestTool.RequestString("mode");
            langs = Language.Languages();
            List<Lebi_Language_Code> temps = Language.Languages();
            foreach (Lebi_Language_Code l in Language.AdminLanguages())
            {
                bool flag = false;
                foreach (Lebi_Language_Code cl in temps)
                {
                    if (cl.id == l.id)
                        flag = true;
                }
                if (!flag)
                    langs.Add(l);
            }
            string where = "1=1";
            if (key != "")
            {
                where += " and (Tag like lbsql{'%" + key + "%'} or ";
                int i = 1;foreach (Lebi_Language_Code lang in langs)
                {
                    where += "" + lang.Code + " like lbsql{'%" + key + "%'}";
                    if (i < langs.Count)
                        where += " or ";
                i++;}
                where += " )";
                //where += " and (Tag like lbsql{'%" + key + "%'} or CN like lbsql{'%" + key + "%'} or EN like lbsql{'%" + key + "%'} or ja like lbsql{'%" + key + "%'})";
            }
            if (mode == "NeedTranslation")
            {
                where += " and ( ";
                int i = 1; foreach (Lebi_Language_Code lang in langs)
                {
                    if (lang.Code != "CN")
                        where += "(" + lang.Code + "='' or Tag = " + lang.Code + ")";
                    else
                        where += "" + lang.Code + "=''";
                    if (i < langs.Count)
                        where += " or ";
                    i++;
                }
                where += " )";
            }
            //Response.Write(where);
            //Response.End();
            models = B_Lebi_Language_Tag.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Language_Tag.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key + "&mode=" + mode, page, PageSize, recordCount);
        }

        public string TagValue(Lebi_Language_Tag tag, string lang)
        {
            if (tag == null)
                return "";
            string res = "";
            Type type = tag.GetType();
            System.Reflection.PropertyInfo p = type.GetProperty(lang);
            if (p == null)
                return "";
            res = p.GetValue(tag, null).ToString();
            return res;
        }
    }
}