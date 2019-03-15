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
    public partial class CheckOut_Bill : Bussiness.ShopPage
    {

        protected List<Lebi_BillType> models;
        protected Lebi_BillType billtype;
        protected Basket basket;
        protected int sid = 0;//结算供应商ID 
        public void LoadPage()
        {

            models = B_Lebi_BillType.GetList("","Sort desc");
            billtype = models.FirstOrDefault();
            sid = RequestTool.RequestInt("sid", 0);
            basket = new Basket(sid);
        }

        
    }
}