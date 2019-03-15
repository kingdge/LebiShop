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
    public partial class CheckOut_Transport : Bussiness.ShopPage
    {

        protected Lebi_Transport_Price TPrice; //当前运输方式区域价格
        protected Lebi_Transport Transport;    //当前运输方式
        protected bool havedefault = true;    //是否拥有默认的运输方式
        protected Basket basket;
        protected List<BasketShop> Shops;
        protected Lebi_User_Address address;
        protected int sid = 0;//结算供应商ID 
        public void LoadPage()
        {

            if (CurrentUser.User_Address_id == 0)
            {
                //未设置收货地区的话，不显示内容
                Response.Write("");
                Response.End();
                return;
            }
            address = B_Lebi_User_Address.GetModel(CurrentUser.User_Address_id);
            if (address == null)
            {
                //未设置收获地区的话，不显示内容
                Response.Write("");
                Response.End();
                return;
            }

            CurrentUser.Transport_Price_id = CurrentUser.Transport_Price_id == "" ? "0" : CurrentUser.Transport_Price_id;

            if (TPrice == null)
                TPrice = new Lebi_Transport_Price();
            if (Transport == null)
                Transport = new Lebi_Transport();
            sid = RequestTool.RequestInt("sid", 0);
            basket = new Basket(sid);
            Shops = new List<BasketShop>();
            if (basket.IsMutiCash)
            {
                foreach (Shop.Model.BasketShop shop in basket.Shops)
                {
                    if (shop.Shop.id == basket.cashsupplierid)
                        Shops.Add(shop);
                }
            }
            else
            {
                Shops = basket.Shops;
            }
            //检查运输方式是否设置正确
            foreach (Shop.Model.BasketShop shop in Shops)
            {
                Lebi_Transport st = GetTransport(shop.Shop.id);
                if (st.id == 0)
                    havedefault = false;

            }

           
        }

        /// <summary>
        /// 计算购物车商品的运费
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        public decimal GetYunFei(BasketShop shop)
        {
            if (shop.IsTransportPriceOne)
                return shop.Money_Transport_One;
            if (CurrentUser.Transport_Price_id != "" && (!CurrentUser.Transport_Price_id.Contains("undefined")))
            {
                Lebi_Transport_Price price = B_Lebi_Transport_Price.GetModel("id in (" + CurrentUser.Transport_Price_id + ") and Supplier_id=" + shop.Shop.id + "");
                return GetYunFei(price, shop);
            }
            return 0;
        }
        public decimal GetYunFei(Lebi_Transport_Price price, BasketShop shop)
        {
            if (shop.IsTransportPriceOne)
                return shop.Money_Transport_One;
            //Lebi_Transport_Price price = B_Lebi_Transport_Price.GetModel("Supplier_id in (" + CurrentUser.Transport_Price_id + ") and Supplier_id=" + shop.Shop.id + "");
            List<Lebi_User_Product> pros = shop.Products;
            try
            {
                return EX_Area.GetYunFei(pros, price, shop.Money_Product);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 可选运输方式
        /// </summary>
        /// <param name="shop"></param>
        /// <param name="areaid"></param>
        /// <returns></returns>
        public List<Lebi_Transport_Price> GetTPrices(int areaid, int supid)
        {
            List<Lebi_Transport_Price> TPrices = EX_Area.TransportPrices_Get(areaid, supid);

            return TPrices;
        }
        /// <summary>
        /// 获取选中ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetRadioCheckedId(List<Lebi_Transport_Price> TPrices)
        {
            foreach (Lebi_Transport_Price tp in TPrices)
            {
                if (("," + CurrentUser.Transport_Price_id + ",").Contains("," + tp.id + ","))
                    return tp.id;
            }
            return TPrices.FirstOrDefault().id;
        }
        /// <summary>
        /// 获得当前运输方式
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public Lebi_Transport GetTransport(int shopid)
        {
            Lebi_Transport_Price price = B_Lebi_Transport_Price.GetModel("id in (" + CurrentUser.Transport_Price_id + ") and Supplier_id=" + shopid + "");
            if (price == null)
                return new Lebi_Transport();
            Lebi_Transport model = B_Lebi_Transport.GetModel(price.Transport_id);
            if (model == null)
                model = new Lebi_Transport();
            return model;

        }

        /// <summary>
        /// 返回自提点的默认提货日期
        /// </summary>
        /// <param name="pick"></param>
        /// <returns></returns>
        public string GetdefaultPickDate(Lebi_PickUp pick)
        {
            DateTime pickdate = CurrentUser.PickUp_Date;
            if (pick.IsCanWeekend == 0 && (pickdate.DayOfWeek == DayOfWeek.Saturday || pickdate.DayOfWeek == DayOfWeek.Sunday))
            {
                return System.DateTime.Now.AddDays(pick.BeginDays).ToString("yyyy-MM-dd");
            }
            if (System.DateTime.Now.Date.AddDays(pick.BeginDays) > pickdate)
            {
                return System.DateTime.Now.AddDays(pick.BeginDays).ToString("yyyy-MM-dd");
            }
            string NoServiceDays = pick.NoServiceDays.TrimStart('0').Replace(".0", ".");
            string nowday = pickdate.ToString("M.d");
            if (("," + NoServiceDays + ",").Contains("," + nowday + ","))
            {
                return System.DateTime.Now.AddDays(pick.BeginDays).ToString("yyyy-MM-dd");
            }
            return pickdate.ToString("yyyy-MM-dd");
        }

        public Lebi_PickUp GetPickup(string sid, BasketShop shop)
        {
            try
            {
                Lebi_PickUp model = B_Lebi_PickUp.GetModel("id in (" + sid + ") and Supplier_id=" + shop.Shop.id + "");
                return model;
            }
            catch
            {
                return null;
            }
        }

        public List<Lebi_PickUp> GetPickups(BasketShop shop)
        {
            //自提点
            List<Lebi_PickUp> pickups = B_Lebi_PickUp.GetList("(','+Language_ids+',' like ',%" + CurrentLanguage.id + "%,' or Language_ids='') and Supplier_id=" + shop.Shop.id + "", "Sort desc");
            return pickups;
        }
    }
}