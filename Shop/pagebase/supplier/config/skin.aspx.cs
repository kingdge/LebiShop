using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.Config
{
    public partial class skin : SupplierPageBase
    {
        protected Lebi_Supplier model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_skin", "编辑皮肤"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Supplier.GetModel("User_id = " + CurrentSupplier.User_id + "");
            if (model == null)
                model = new Lebi_Supplier();
            Lebi_Supplier_Skin skin = B_Lebi_Supplier_Skin.GetModel(id);
            if (skin != null)
            {
                model.Supplier_Skin_id = skin.id;
                model.head = skin.head;
                model.shortbar = skin.shortbar;
                model.longbar = skin.longbar;
            }
        }

        //public string GetSkins(int id)
        //{
        //    string str = "";
        //    List<Lebi_Supplier_Skin> skins = B_Lebi_Supplier_Skin.GetList("IsShow=1", "Sort desc");
        //    foreach (Lebi_Supplier_Skin skin in skins)
        //    {
        //        string sel = "";
        //        if (id == skin.id)
        //            sel = "selected";
        //        str += "<option value=\"" + skin.id + "\" " + sel + ">" + skin.Name + "</option>";
        //    }
        //    return str;
        //}
        public List<Lebi_Supplier_Skin> GetSkins()
        {
            try
            {
                List<Lebi_Supplier_Skin> skins = B_Lebi_Supplier_Skin.GetList("IsShow=1 and IsPublic=1 and id in (" + CurrentSupplierGroup.Supplier_Skin_ids + ",0," + model.Supplier_Skin_id + ")", "Sort desc");
                return skins;
            }
            catch
            {
                return new List<Lebi_Supplier_Skin>();
            }
        }
    }
}