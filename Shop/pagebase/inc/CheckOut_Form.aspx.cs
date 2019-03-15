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
    public partial class order_form : Bussiness.ShopPage
    {

        protected Basket basket;
        protected decimal money_transport;
        protected decimal money_bill;
        protected decimal money_order;
        //protected decimal Money_UserCut;
        //protected decimal money_card311;
        //protected decimal money_card312;
        //protected decimal money_dixian = 0;
        //protected decimal money_need = 0;//减去代金券后的应付款金额
        protected int sid = 0;//结算供应商ID 
        public void LoadPage()
        {
            sid = RequestTool.RequestInt("sid", 0);
            basket = new Basket(sid);
            money_transport = RequestTool.RequestDecimal("money_transport", 0);
            money_bill = RequestTool.RequestDecimal("money_bill", 0);

            money_order = basket.Money_Product - basket.Money_Cut + money_bill + money_transport + basket.Money_Property + basket.Money_Tax;

        }


    }
}