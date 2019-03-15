using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;

namespace Shop.inc
{
    public partial class CheckOut_Address : Bussiness.ShopPage
    {
        protected int id;
        protected int edit_id;
        protected List<Lebi_User_Address> models;
        protected Lebi_User_Address address;
        protected Lebi_User_Address edit_address;
        protected int sid = 0;//结算供应商ID 
        public void LoadPage()
        {
            edit_id = RequestTool.RequestInt("edit_id", 0);
            id = RequestTool.RequestInt("id", 0);
            sid = RequestTool.RequestInt("sid", 0);
            if (id == 0)
            {
                try
                {
                    id = B_Lebi_User_Address.GetMaxId("User_id=" + CurrentUser.id + "");
                }
                catch (InvalidCastException)
                {
                    id=0;
                }
            }
            models = B_Lebi_User_Address.GetList("User_id="+CurrentUser.id +"","");
            address = B_Lebi_User_Address.GetModel(CurrentUser.User_Address_id);
            if (address == null)
                address = B_Lebi_User_Address.GetModel("User_id=" + CurrentUser.id + " order by id desc");
            if (address == null)
                address = new Lebi_User_Address();
            if (edit_id > 0)
            {
                edit_address = B_Lebi_User_Address.GetModel(edit_id);
            }
            if (edit_address == null)
                edit_address = new Lebi_User_Address();
        }

    }
}