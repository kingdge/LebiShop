using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using Shop.Model;
using Shop.Tools;
using System.Linq;
using System.Collections.Specialized;
using Shop.Bussiness;
namespace Shop
{
    public class P_SupplierRegister : ShopPage
    {
        protected Site_Supplier site;
        protected string backurl = "";
        protected string Version = "";
        protected bool IsSet = true;
        protected List<Lebi_Supplier_Verified> verifieds;
        protected Lebi_Supplier supplier;
        protected string status;
        protected string logintype = "";
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
            site = new Site_Supplier();
            backurl = RequestTool.RequestString("url").Replace("<", "").Replace(">", "");
            if (backurl == "")
                backurl = site.AdminPath + "/login.aspx";
            if (CurrentUser.id == 0)
            {
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain()) + ""));
            }
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a>" + Tag("商家注册") + "</a>";
            verifieds = B_Lebi_Supplier_Verified.GetList("", "Sort desc");
            logintype = RequestTool.RequestString("logintype", "");

            List<Lebi_Supplier> suppliers = B_Lebi_Supplier.GetList("User_id=" + CurrentUser.id + "", "id desc");
            if (suppliers.Count == 0)
            {
                supplier = new Lebi_Supplier();
                supplier.Type_id_SupplierStatus = 441;
            }
            if (logintype == "" && suppliers.Count > 0)
            {
                supplier = suppliers.FirstOrDefault();
                Lebi_Supplier_Group group = B_Lebi_Supplier_Group.GetModel(supplier.Supplier_Group_id);
                logintype = group.type;
            }
            else
            {
                foreach (Lebi_Supplier sup in suppliers)
                {
                    Lebi_Supplier_Group group = B_Lebi_Supplier_Group.GetModel(sup.Supplier_Group_id);
                    if (logintype == group.type)
                    {
                        supplier = sup;
                    }
                }
            }
            logintype = logintype == "" ? "supplier" : logintype;
            if (supplier == null)
            {
                supplier = new Lebi_Supplier();
                supplier.Address = CurrentUser.Address;
                supplier.Area_id = CurrentUser.Area_id;
                supplier.Fax = CurrentUser.Fax;
                supplier.Email = CurrentUser.Email;
                supplier.MobilePhone = CurrentUser.MobilePhone;
                supplier.Msn = CurrentUser.Msn;
                supplier.Phone = CurrentUser.Phone;
                supplier.Postalcode = CurrentUser.Postalcode;
                supplier.QQ = CurrentUser.QQ;
                supplier.RealName = CurrentUser.RealName;
                supplier.UserName = CurrentUser.UserName;
                supplier.User_id = CurrentUser.id;
                supplier.Type_id_SupplierStatus = 441;
                status = Tag("新注册");
            }
            else
            {
                if (supplier.Type_id_SupplierStatus == 442)
                {
                    Response.Redirect(site.AdminPath + "/login.aspx");
                }
                status = TypeName(supplier.Type_id_SupplierStatus);
            }
        }

        public string GetVerifiedImage(int vid)
        {
            string where = "Verified_id = " + vid + " and Supplier_id = " + supplier.id + "";
            Lebi_Supplier_Verified_Log log = B_Lebi_Supplier_Verified_Log.GetModel(where);
            if (log == null)
            {
                return "";
            }
            return log.ImageUrl;
        }

        public string GroupOption(int id)
        {
            string str = "";
            List<Lebi_Supplier_Group> models = B_Lebi_Supplier_Group.GetList("IsShow=1 and type='" + logintype + "'", "");
            foreach (Lebi_Supplier_Group model in models)
            {
                string sel = "";
                if (id == model.id)
                    sel = "selected";
                str += "<option value=\"" + model.id + "\" " + sel + " vids=\"" + model.Verified_ids + "\">" + Lang(model.Name) + "</option>";
            }
            return str;
        }
    }
}