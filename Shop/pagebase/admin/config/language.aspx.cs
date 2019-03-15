using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Threading;

namespace Shop.Admin.sorteConfig
{
    public partial class Language : AdminPageBase
    {
        protected List<Lebi_Site> models;
        protected int sitenum;
        protected bool NeedCody = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("site_list", "站点列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            string where = "1=1";
            //int sitenum = 0;//允许的站点数量
            //if (Shop.LebiAPI.Service.Instanse.Check("domain3"))
            //{
            //    sitenum = 30;
            //}
            //else if (Shop.LebiAPI.Service.Instanse.Check("domain20"))
            //{
            //    sitenum = 20;
            //}
            //else if (Shop.LebiAPI.Service.Instanse.Check("domain10"))
            //{
            //    sitenum = 10;
            //}
            //else if (Shop.LebiAPI.Service.Instanse.Check("domain2"))
            //{
            //    sitenum = 2;
            //}
            //else
            //{
            //    sitenum = 1;
            //}
            sitenum = Shop.Bussiness.Site.Instance.SiteCount;
            //if (Shop.LebiAPI.Service.Instanse.Check("domain3"))
            //    sitenum = 30;
            if (domain3admin && CurrentAdmin.Site_ids != "")
            {
                where += " and id in (" + CurrentAdmin.Site_ids + ")";
            }
            models = B_Lebi_Site.GetList(where, "Sort desc", sitenum, 1);
            foreach (Lebi_Site site in models)
            {
                if (site.Domain != "" && site.Path != "/" && site.Path != "")
                {
                    NeedCody = true;
                }
            }
            //Language表从Site表提取数据
            //List<Lebi_Language> langs = B_Lebi_Language.GetList("", "");
            //foreach (Lebi_Language lang in langs)
            //{
            //    Lebi_Site s = B_Lebi_Site.GetModel(lang.Site_id);
            //    if (lang.Copyright == "")
            //        lang.Copyright = Bussiness.Language.Content(s.Copyright, lang.Code);
            //    if (lang.Email == "")
            //        lang.Email = Bussiness.Language.Content(s.Email, lang.Code);
            //    if (lang.Fax == "")
            //        lang.Fax = Bussiness.Language.Content(s.Fax, lang.Code);
            //    if (lang.FootHtml == "")
            //        lang.FootHtml = Bussiness.Language.Content(s.FootHtml, lang.Code);
            //    if (lang.Logo == "")
            //        lang.Logo = Bussiness.Language.Content(s.Logoimg, lang.Code);
            //    if (lang.Phone == "")
            //        lang.Phone = Bussiness.Language.Content(s.Phone, lang.Code);
            //    if (lang.QQ == "")
            //        lang.QQ = Bussiness.Language.Content(s.QQ, lang.Code);
            //    if (lang.ServiceP == "")
            //        lang.ServiceP = Bussiness.Language.Content(s.ServiceP, lang.Code);
            //    if (lang.Site_Description == "")
            //        lang.Site_Description = Bussiness.Language.Content(s.Description, lang.Code);
            //    if (lang.Site_Keywords == "")
            //        lang.Site_Keywords = Bussiness.Language.Content(s.Keywords, lang.Code);
            //    if (lang.Site_Name == "")
            //        lang.Site_Name = Bussiness.Language.Content(s.Name, lang.Code);
            //    if (lang.Site_Title == "")
            //        lang.Site_Title = Bussiness.Language.Content(s.Title, lang.Code);
            //    B_Lebi_Language.Update(lang);
            //}
            //提取结束
        }
        public Lebi_Language GetLanguage(string code, int siteid)
        {
            Lebi_Language lang = B_Lebi_Language.GetModel("Site_id=" + siteid + " and Code='" + code + "'");
            if (lang == null)
                lang = new Lebi_Language();
            return lang;
        }
    }
}