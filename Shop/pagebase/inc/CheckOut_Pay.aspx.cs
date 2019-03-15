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
    public partial class CheckOut_Pay : Bussiness.ShopPage
    {

        protected List<Lebi_Pay> pays;
        protected List<Lebi_OnlinePay> onpays;
        protected Basket basket;
        protected int sid = 0;//结算供应商ID 
        public void LoadPage()
        {
            sid = RequestTool.RequestInt("sid", 0);
            basket = new Basket(sid);
            bool offlinepay = false;
            if (CurrentUser.Transport_Price_id == "")
                CurrentUser.Transport_Price_id = "0";
            List<Lebi_Transport_Price> prices = B_Lebi_Transport_Price.GetList("id in (" + CurrentUser.Transport_Price_id + ")", "");
            if (prices.Count == 0)
            {
                Response.Write(Tag("请先选择配送方式"));
                Response.End();
                return;
            }
            Lebi_Transport_Price currenttranprice = new Lebi_Transport_Price();
            Lebi_Transport currenttran = new Lebi_Transport();
            foreach (Lebi_Transport_Price price in prices)
            {
                currenttran = B_Lebi_Transport.GetModel(price.Transport_id);
                if (currenttran == null)
                {
                    Response.Write(Tag("请先选择配送方式"));
                    Response.End();
                    return;
                }
                if (basket.cashsupplierid == price.Supplier_id)//商城收款，供应商发货的情况必须用在线支付，所以下永远匹配不上。
                    currenttranprice = price;
            }
            string where = "IsUsed=1";
            //if (prices.Count > 1 || currenttranprice.Supplier_id > 0 || basket.cashsupplierid > 0)
            //{
            //    where += " and Code='OnlinePay'";
            //}
            //else
            //{
            if (currenttran.IsCanofflinePay == 1 && currenttranprice.IsCanofflinePay == 1)
                offlinepay = true;
            if (offlinepay == false)
                where += " and Code!='OfflinePay'";
            //}
            string onpaywhere = "IsUsed=1 and parentid=0 and ','+Language_ids+',' like '%," + CurrentLanguage.id + ",%'";
            //string useragent=Request.UserAgent.ToString().ToLower();
            //if (!useragent.Contains("micromessenger"))
            //{
            //    onpaywhere += " and Code!='weixinpay'";
            //}
            if (CurrentSite.IsMobile == 1)
            {
                onpaywhere += " and (showtype='' or showtype like '%wap%')";
            }
            else
            {
                onpaywhere += " and (showtype='' or showtype like '%web%')";
            }
            Shop.Bussiness.SystemLog.Add(onpaywhere);
            pays = B_Lebi_Pay.GetList(where, "Sort desc");
            if ((CurrentUser.Pay_id == 0 || pays.Count == 1) && pays.Count > 0)
                CurrentUser.Pay_id = pays.FirstOrDefault().id;
            onpays = B_Lebi_OnlinePay.GetList(onpaywhere, "Sort desc");
            if (onpays.Count == 0)
            {
                CurrentUser.OnlinePay_id = 0;
            }
            else
            {
                if (CurrentUser.OnlinePay_id == 0 || onpays.Count == 1)
                    CurrentUser.OnlinePay_id = onpays.FirstOrDefault().id;
                else
                {
                    bool flag = false;
                    foreach (Lebi_OnlinePay p in onpays)
                    {
                        if (p.id == CurrentUser.OnlinePay_id)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == false)
                        CurrentUser.OnlinePay_id = onpays.FirstOrDefault().id;
                }
            }

        }
        /// <summary>
        /// 获取一个支付方式的对于供应商设置或（默认设置）
        /// </summary>
        /// <param name="pay"></param>
        /// <returns></returns>
        public Lebi_OnlinePay Getpay(Lebi_OnlinePay pay)
        {
            if (basket.cashsupplierid == 0)
                return pay;
            Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(basket.cashsupplierid);
            if (supplier != null)
            {
                //供应商发货商城收款的情况
                if (supplier.IsCash == 0)
                    return pay;
            }

            Lebi_OnlinePay p = B_Lebi_OnlinePay.GetModel("parentid=" + pay.id + " and Supplier_id=" + basket.cashsupplierid + " and IsUsed=1");
            if (p != null)
            {
                if (Lang(p.Name) == "")
                    p.Name = pay.Name;
                if (Lang(p.Description) == "")
                    p.Name = pay.Name;
                if (p.Logo == "")
                    p.Logo = pay.Logo;
            }
            return p;
        }

    }
}