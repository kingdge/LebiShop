using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using System.Collections.Specialized;
using Shop.Model;
using Shop.Tools;
using Shop.DataAccess;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Linq;
using Shop.Bussiness;
namespace Shop.Bussiness
{
    public class SupplierAjaxBase : SupplierBase
    {
        protected List<Lebi_Supplier_Menu> AllMenus;
        protected override void OnLoad(EventArgs e)
        {
            if (!Shop.LebiAPI.Service.Instanse.Check("plugin_gongyingshang"))
            {
                Response.Write("");
                Response.End();
                return;
            }
            PageLoadCheck(); //页面载入检查

            #region 当前用户信息
            int selectsupplierid = RequestTool.RequestInt("selectsupplierid");
            if (selectsupplierid > 0)
            {
                //切换超级账号
                string msg = "";
                EX_Supplier.Login(CurrentUser, "", selectsupplierid, out msg);
                CurrentSupplierUser = B_Lebi_Supplier_User.GetModel("User_id = " + CurrentUser.id + " and Supplier_id=" + selectsupplierid + " and Type_id_SupplierUserStatus=9011");
            }
            if (CurrentSupplierUser == null)
                CurrentSupplierUser = EX_Supplier.CurrentSupplierUser(CurrentUser);

            if (CurrentSupplierUser.id == 0)
            {
                Response.Redirect(Shop.Bussiness.Site.Instance.SupplierPath + "/Login.aspx");
                return;
            }


            CurrentSupplier = B_Lebi_Supplier.GetModel(CurrentSupplierUser.Supplier_id);
            if (CurrentSupplier.Type_id_SupplierStatus != 442)
            {
                Response.Redirect(Shop.Bussiness.Site.Instance.SupplierPath + "/Login.aspx");
                return;
            }
            CurrentSupplierUserGroup = B_Lebi_Supplier_UserGroup.GetModel(CurrentSupplierUser.Supplier_UserGroup_id);
            if (CurrentSupplierUserGroup == null)
                CurrentSupplierUserGroup = new Lebi_Supplier_UserGroup();
            if (CurrentSupplier != null)
            {
                CurrentSupplierGroup = B_Lebi_Supplier_Group.GetModel(CurrentSupplier.Supplier_Group_id);

                if (CurrentSupplierGroup == null)
                    CurrentSupplierGroup = new Lebi_Supplier_Group();
            }
            else
            {
                Response.Redirect(Shop.Bussiness.Site.Instance.SupplierPath + "/Login.aspx");
                return;
            }
            #endregion
            
            #region 配合前台主题
            string themecode = "";
            int siteid = 0;
            var nv = CookieTool.GetCookie("ThemeStatus");
            if (!string.IsNullOrEmpty(nv.Get("theme")))
            {
                themecode = nv.Get("theme");
            }
            if (!string.IsNullOrEmpty(nv.Get("theme")))
            {
                int.TryParse(nv.Get("site"), out siteid);
            }
            if (siteid == 0)
                siteid = ShopCache.GetMainSite().id;
            LoadTheme(themecode, siteid, CurrentLanguage.Code, "", true);
            #endregion

            Suppliers = GetSuppliers();
            base.OnLoad(e);
        }

    }


}
