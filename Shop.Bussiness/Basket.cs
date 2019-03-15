using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;
using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.Script.Serialization;
using System.Collections.Specialized;

namespace Shop.Bussiness
{
    public class Basket : ShopPage
    {
        public decimal Weight = 0;//订单总重量
        public decimal Volume = 0;//订单总体积
        public decimal Money_Product = 0;//商品总金额
        public decimal Money_Property = 0;//属性价格（如延长保修）总和金额
        public decimal Money_Product_begin = 0;//未计算促销前的商品总金额
        public decimal Money_Cut = 0;//减免金额
        public decimal Money_Give = 0;//返还金额
        //public decimal Money_Transport_One = 0;//定额运费,
        //public bool IsTransportPriceOne = false;//是否定额运费,
        public decimal Money_Market = 0;//商品市场价总金额
        public int Count = 0;//商品总数
        public decimal Point = 0;//获得积分总数
        public decimal Point_Product = 0;//商品获得积分总数
        public decimal Point_Free = 0;//赠送积分
        public decimal Point_Buy = 0;//积分换购所需积分总数
        public List<Lebi_User_Product> Products; //购物车商品
        public List<Lebi_User_Product> FreeProducts; //赠品
        public List<Lebi_Promotion_Type> PromotionTypes;//满足条件的促销活动
        public List<BasketShop> Shops;//将商品按照商铺分组，并计算运费
        public bool IsMutiCash = false;//是否需要分开结算（供应商独立收款的情况）
        public int cashsupplierid = 0;//当前结账的供应商ID
        public decimal Money_Refund_VAT = 0;//所含税额
        public decimal Money_Refund_Fee = 0;//退税手续费
        public decimal Money_Refund = 0;//退税总额
        public decimal Money_Product_NOVAT = 0;//商品金额不含税
        public decimal OtherSite_Money_Refund = 0;//其他网站退税总额
        public decimal Money_Tax = 0; //税金
        /// <summary>
        /// sid=0，取得全部供应商的商品
        /// </summary>
        /// <param name="type"></param>
        public Basket(int sid)
        {
            Products = Basket.UserProduct(CurrentUser, 142);

            PromotionTypes = new List<Lebi_Promotion_Type>();
            FreeProducts = new List<Lebi_User_Product>();
            Shops = new List<BasketShop>();
            List<BasketShop> tempShops = new List<BasketShop>();
            List<string> brandstring = new List<string>();
            foreach (Lebi_User_Product pro in Products)
            {
                try
                {
                    Lebi_Product product = B_Lebi_Product.GetModel(pro.Product_id);
                    if (product == null)
                        continue;
                    bool ishavebrand = false;
                    for (int i = 0; i < brandstring.Count(); i++)
                    {
                        if (brandstring[i].Contains(product.Brand_id.ToString() + ":"))
                        {
                            brandstring[i] += "," + pro.Product_id;
                            ishavebrand = true;
                            break;
                        }
                    }
                    if (!ishavebrand)
                    {
                        brandstring.Add(product.Brand_id.ToString() + ":" + pro.Product_id);
                    }
                    if (product.Type_id_ProductType == 324)//预定商品
                        pro.Product_Price = product.Price_reserve;//预定金额
                    else
                        pro.Product_Price = EX_Product.ProductPrice(product, CurrentUserLevel, CurrentUser, pro.count);//单价
                    //<-{计算品牌不含税商品总额  by lebi.kingdge 2015-08-22
                    Lebi_Brand brand = B_Lebi_Brand.GetModel(product.Brand_id);
                    if (brand != null)
                    {
                        if (brand.IsVAT == 0)
                            Money_Product_NOVAT += pro.Product_Price * pro.count;
                    }
                    //}->
                    Lebi_Supplier shop = B_Lebi_Supplier.GetModel(product.Supplier_id);
                    if (shop == null)
                    {
                        product.Supplier_id = 0;//容错，商品供应商删除时，商城发货
                        shop = new Lebi_Supplier();//自营商品
                    }
                    else
                    {
                        if (shop.IsSupplierTransport == 0)//商城代发货的情况
                        {
                            product.Supplier_id = 0;
                            shop = new Lebi_Supplier();//自营商品
                        }
                    }

                    BasketShop bshop = (from m in tempShops
                                        where m.Shop.id == product.Supplier_id
                                        select m).ToList().FirstOrDefault();
                    if (bshop == null)
                    {

                        if (product.Supplier_id == 0)
                        {
                            //shop = new Lebi_Supplier();//自营商品
                            bshop = new BasketShop();
                            bshop.Shop = shop;
                            bshop.Products = new List<Lebi_User_Product>();
                            tempShops.Add(bshop);

                        }
                        else
                        {
                            bshop = new BasketShop();
                            bshop.Shop = shop;
                            bshop.Products = new List<Lebi_User_Product>();
                            tempShops.Add(bshop);
                        }
                    }
                    (from m in tempShops
                     where m.Shop.id == product.Supplier_id
                     select m).ToList().FirstOrDefault().Products.Add(pro);
                }
                catch (System.NullReferenceException)
                {

                }
            }
            tempShops = tempShops.OrderBy(a => a.Shop.id).ToList();
            if (SYS.IsSupplierCash == "1")
            {
                if (sid == 0)
                    sid = RequestTool.RequestInt("sid");
                if (sid == 0)
                {
                    string tempid = CookieTool.GetCookieString("supplier");
                    int.TryParse(tempid, out sid);
                }
                cashsupplierid = sid;
                bool flag = false;
                foreach (BasketShop shop in tempShops)
                {
                    if (shop.Shop.IsCash == 1)
                        IsMutiCash = true;
                    if (shop.Shop.id == cashsupplierid)
                        flag = true;
                }
                if (!flag)
                {
                    try
                    {
                        cashsupplierid = tempShops.FirstOrDefault().Shop.id;
                    }
                    catch
                    {
                        cashsupplierid = 0;
                    }
                }

                if (sid > 0)
                {
                    //只保留当前结算供应商的数据
                    List<int> ids = new List<int>();
                    for (int i = 0; i < tempShops.Count; i++)
                    {
                        if (tempShops[i].Shop.id == cashsupplierid)
                            Shops.Add(tempShops[i]);
                    }

                }
                else
                {
                    Shops = tempShops;
                }
            }
            else
            {
                Shops = tempShops;
            }
            Products = new List<Lebi_User_Product>();
            for (int i = 0; i < Shops.Count; i++)
            {
                Shops[i] = SetMoneyAndPoint(CurrentUser, CurrentUserLevel, Shops[i]);
                Weight += Shops[i].Weight;
                Volume += Shops[i].Volume;
                Money_Product += Shops[i].Money_Product;
                Money_Product_begin += Shops[i].Money_Product_begin;
                Money_Property += Shops[i].Money_Property;
                Money_Cut += Shops[i].Money_Cut;
                Money_Give += Shops[i].Money_Give;
                Money_Market += Shops[i].Money_Market;
                Count += Shops[i].Count;
                Point += Shops[i].Point;
                Point_Buy += Shops[i].Point_Buy;
                Point_Product += Shops[i].Point_Product;
                Point_Free += Shops[i].Point_Free;
                FreeProducts.AddRange(Shops[i].FreeProducts);
                PromotionTypes.AddRange(Shops[i].PromotionTypes);
                Products.AddRange(Shops[i].Products);
            }
            //<-{退税计算  by lebi.kingdge 2015-08-22
            decimal Refund_MinMoney = 0;
            decimal Refund_VAT = 0;
            decimal.TryParse(SYS.Refund_MinMoney, out Refund_MinMoney);
            decimal.TryParse(SYS.Refund_VAT, out Refund_VAT);
            if (Refund_MinMoney > 0)
            {
                if ((Money_Product - Money_Product_NOVAT) > Refund_MinMoney)
                {
                    Money_Refund_VAT = (Money_Product - Money_Product_NOVAT) * Refund_VAT / 100;
                    Money_Refund_Fee = Refund_Fee(SYS.Refund_StepR, Money_Refund_VAT, Money_Product - Money_Product_NOVAT);
                    Money_Refund = Money_Refund_VAT - Money_Refund_Fee;
                }
                else
                {
                    Money_Refund = 0;
                    Money_Refund_VAT = 0;
                    Money_Refund_Fee = 0;
                }
                //循环品牌计算各品牌退税
                if (Refund_VAT > 0)
                {
                    foreach (string bran in brandstring)
                    {
                        string[] arr = bran.Split(':');
                        Lebi_Brand brand = B_Lebi_Brand.GetModel("id=" + arr[0] + " and IsVAT = 1");
                        if (brand != null)
                        {
                            decimal Brand_Money_Product = 0;
                            decimal OtherSite_Money_Refund_VAT = 0;
                            decimal OtherSite_Money_Refund_Fee = 0;
                            List<Lebi_User_Product> user_products = (from m in Products where ("," + arr[1] + ",").Contains("," + m.Product_id + ",") select m).ToList();
                            foreach (Lebi_User_Product user_product in user_products)
                            {
                                Brand_Money_Product += user_product.Product_Price * user_product.count;
                            }
                            if (Brand_Money_Product >= Refund_MinMoney)
                            {
                                OtherSite_Money_Refund_VAT = Brand_Money_Product * Refund_VAT / 100;
                                OtherSite_Money_Refund_Fee = Refund_Fee(SYS.Refund_StepR, OtherSite_Money_Refund_VAT, Brand_Money_Product);
                                OtherSite_Money_Refund += OtherSite_Money_Refund_VAT - OtherSite_Money_Refund_Fee;
                            }
                        }
                    }
                }
            }
            //}->
            //<-{税金计算  by lebi.kingdge 2017-02-17
            decimal TaxRate = 0;
            decimal.TryParse(SYS.TaxRate, out TaxRate);
            if (TaxRate > 0)
            {
                Money_Tax = Money_Product * TaxRate / 100;
            }
            //}->
        }
        //返回退税手续费
        public static decimal Refund_Fee(string Refund_StepR, decimal Money_Refund_VAT, decimal Money_Product)
        {
            double Money_Refund_VAT_ = Convert.ToDouble(Money_Refund_VAT);
            try
            {
                if (Refund_StepR != null)
                {
                    List<BaseConfigStepR> stepRs = B_BaseConfig.StepR(Refund_StepR);
                    if (stepRs.Count > 0)
                    {
                        foreach (BaseConfigStepR stepR in stepRs)
                        {
                            if (Money_Product > stepR.S)
                            {
                                double R = Convert.ToDouble(stepR.R);
                                return Convert.ToDecimal(Math.Pow(Money_Refund_VAT_, R));
                            }
                        }
                    }
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }
        /// <summary>
        /// 返回用户商品List
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<Lebi_User_Product> UserProduct(Lebi_User CurrentUser, int t)
        {
            return EX_User.UserProduct(CurrentUser, t);
        }
        /// <summary>
        /// 清空购物车|收藏夹
        /// </summary>
        public static void Clear(Lebi_User CurrentUser, int t)
        {
            if (CurrentUser.id > 0)//已经登录
            {
                B_Lebi_User_Product.Delete("user_id=" + CurrentUser.id + " and type_id_UserProductType=" + t + "");
            }
            else//未登录
            {
                string CookieName = "UserProduct" + t;
                NameValueCollection nv = new NameValueCollection();
                CookieTool.WriteCookie(CookieName, nv, 0);
            }
        }
        /// <summary>
        /// 检查购物车是否满足代金券使用条件
        /// </summary>
        /// <param name="basket"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        public static bool CheckCard(Basket basket, Lebi_Card card)
        {
            if (card.Time_Begin > System.DateTime.Now)
            {
                return false;
            }
            if (card.Time_End <= System.DateTime.Now)
            {
                card.Type_id_CardStatus = 204;
                B_Lebi_Card.Update(card);
                return false;
            }
            if ((basket.Money_Product - basket.Money_Cut) < card.Money_Buy)
                return false;
            if (card.Pro_Type_ids == "")
                return true;
            foreach (Lebi_User_Product p in basket.Products)
            {
                Lebi_Product pro = B_Lebi_Product.GetModel(p.Product_id);
                Lebi_Pro_Type type = EX_Product.TopProductType(pro);
                if (("," + card.Pro_Type_ids + ",").Contains("," + type.id + ","))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 计算促销活动
        /// </summary>
        /// <param name="Products">商品信息</param>
        /// <param name="CurrentUser">购买者</param>
        /// <param name="shop">供应商，为空表示自营商品</param>
        /// <param name="basketshop"></param>
        public static BasketShop SetMoneyAndPoint(Lebi_User CurrentUser, Lebi_UserLevel CurrentUserLevel, BasketShop basketshop)
        {
            string pids = "0";
            decimal Money_Product_begin = 0;
            int Count = 0;
            decimal Weight = 0;//订单总重量
            decimal Volume = 0;//订单总体积

            int Money_Transport_One = 0;
            bool IsTransportPriceOne = false;
            decimal Money_Market = 0;
            decimal Money_Cut = 0;
            decimal Money_Give = 0;
            decimal Point_Free = 0;//赠送积分
            decimal Point_Product = 0;//商品获得的积分
            decimal Point = 0;//获得的积分总数
            decimal Point_Buy = 0;//积分换购所需积分
            decimal Money_Product = 0;
            decimal Money_Property = 0;
            List<Lebi_User_Product> FreeProducts = new List<Lebi_User_Product>();
            List<Lebi_Promotion_Type> PromotionTypes = new List<Lebi_Promotion_Type>();
            if (CurrentUserLevel == null)
                CurrentUserLevel = B_Lebi_UserLevel.GetModel(CurrentUser.UserLevel_id);
            List<Lebi_User_Product> Products = basketshop.Products;
            Lebi_Supplier shop = basketshop.Shop;
            foreach (Lebi_User_Product pro in Products)
            {
                Lebi_Product product = B_Lebi_Product.GetModel(pro.Product_id);
                if (product == null)
                    continue;
                pro.Discount = 100;
                pro.Pointagain = 1;
                //pro.Product_Price = EX_Product.ProductPrice(product, CurrentUserLevel,pro.count);//单价
                if (CurrentUserLevel.MoneyToPoint > 0)
                    pro.Product_Point = pro.Product_Price * CurrentUserLevel.MoneyToPoint + pro.Product_Point;//单个产品可得的积分
                pids += "," + pro.Product_id;
                Money_Product_begin = Math.Round(Money_Product_begin + pro.Product_Price * pro.count, 2);
                Count = Count + pro.count;
                Money_Market = Money_Market + EX_Product.ProductMarketPrice(product) * pro.count;  //兼容分销  2018.3.8 by lebi.kingdge
                Money_Property = Money_Property + pro.ProPerty_Price * pro.count;
                Money_Product = Money_Product + pro.Product_Price * pro.count;
                Weight = Weight + product.Weight * pro.count;
                Volume = Volume + product.VolumeH * product.VolumeL * product.VolumeW * pro.count;
                Point_Product = Point_Product + Math.Round(pro.Product_Point * pro.count,2);
                Point = Point_Product + Point_Free;
                //320一般商品321限时抢购322团购323积分换购
                if (product.Type_id_ProductType == 323 && product.Time_Expired > System.DateTime.Now)
                {
                    Point_Buy = Point_Buy + product.Price_Sale * pro.count;
                }
            }

            List<Lebi_Promotion_Type> cps = Promotion.CurrentPromotionType();
            if (shop == null)
                shop = new Lebi_Supplier();
            foreach (Lebi_Promotion_Type cp in cps)
            {
                //检查是否商铺单独促销
                if (cp.Type_id_PromotionType == 421)
                {
                    if (shop.id > 0)
                        continue;
                }
                if (cp.Type_id_PromotionType == 422)
                {
                    if (shop.id != cp.Supplier_id)
                        continue;
                }
                //检查会员组别
                if (!("," + cp.UserLevel_ids + ",").Contains("," + CurrentUserLevel.id + ","))
                    continue;

                bool flag = true;
                string where = "1=1";
                List<Lebi_User_Product> sps = null;
                foreach (Lebi_Promotion p in Promotion.GetPromotion(cp.id))//只匹配一个条件
                {
                    flag = true;
                    if (p.Case804 == "")
                        p.Case804 = "0";
                    if (p.Case805 == "")
                        p.Case805 = "0";
                    //验证订单金额:801
                    if (p.IsCase801 == 1)
                    {
                        if (Money_Product_begin < p.Case801)
                        {
                            flag = false;
                            continue;
                        }
                    }
                    //验证订单商品数量:802

                    if (p.IsCase802 == 1)
                    {
                        if (Count < p.Case802)
                        {
                            flag = false;
                            continue;
                        }
                    }
                    //验证商品分类:804
                    if (p.IsCase804 == 1)
                    {
                        where += " and Pro_Type_id in (" + p.Case804 + ")";
                        if (B_Lebi_Product.Counts("id in (" + pids + ") and Pro_Type_id in (" + p.Case804 + ")") == 0)
                        {
                            flag = false;
                            continue;
                        }
                    }
                    //验证限制商品:805
                    if (p.IsCase805 == 1)
                    {
                        where += "and id in (" + p.Case805 + ")";
                        if (B_Lebi_Product.Counts("id in (" + pids + ") and id in (" + p.Case805 + ")") == 0)
                        {
                            flag = false;
                            continue;
                        }
                    }
                    //验证单品数量:803
                    if (p.IsCase803 == 1)
                    {
                        string tids = "0";
                        //取出购物车中，数量大于水平线的商品
                        foreach (Lebi_User_Product up in Products)
                        {
                            if (up.count >= p.Case803)
                            {
                                tids += "," + up.Product_id;
                            }
                        }
                        if (tids == "0")
                        {
                            flag = false;
                            continue;
                        }
                        where += " and id in (" + tids + ")";
                        if (B_Lebi_Product.Counts(where) == 0)
                        {
                            flag = false;
                            continue;
                        }
                    }
                    //验证订单内指定商品数量:806
                    if (p.IsCase806 == 1)
                    {
                        if (p.Case804 == "" && p.Case805 == "")//未设置限制条件的话，直接失败
                        {
                            flag = false;
                            continue;
                        }
                        int count = 0;
                        //计算购物车中包含商品的总数
                        foreach (Lebi_User_Product up in Products)
                        {
                            if (("," + p.Case804 + ",").Contains("," + up.Pro_Type_id + ",") || ("," + p.Case805 + ",").Contains("," + up.Product_id + ","))
                            {
                                count = count + up.count;
                            }
                        }
                        if (count < p.Case806)
                        {
                            flag = false;
                            continue;
                        }

                    }
                    //条件检查结束
                    if (flag)
                    {
                        //取出验证成功的指定商品
                        sps = B_Lebi_User_Product.GetList("User_id=" + CurrentUser.id + " and type_id_UserProductType=142 and Product_id in (select id from [Lebi_Product] where " + where + ")", "");

                        //计算定额运费901
                        if (p.IsRule901 == 1)
                        {
                            Money_Transport_One = p.Rule901;
                            IsTransportPriceOne = true;
                        }
                        //计算折扣902
                        if (p.IsRule902 == 1)
                        {
                            foreach (Lebi_User_Product pro in Products)
                            {
                                pro.Discount = pro.Discount * p.Rule902 / 100;
                            }
                        }
                        //计算指定商品折扣903
                        if (p.IsRule903 == 1)
                        {
                            if (sps != null)
                            {
                                foreach (Lebi_User_Product pro in Products)
                                {
                                    foreach (Lebi_User_Product sp in sps)
                                    {
                                        if (pro.id == sp.id)
                                            pro.Discount = pro.Discount * p.Rule903 / 100;
                                    }
                                }

                            }
                        }
                        //计算减免金额904
                        if (p.IsRule904 == 1)
                        {
                            Money_Cut = p.Rule904;
                        }
                        //计算返还金额905
                        if (p.IsRule905 == 1)
                        {
                            Money_Give = p.Rule905;
                        }
                        //计算赠送积分906
                        if (p.IsRule906 == 1)
                        {
                            Point_Free = p.Rule906;
                        }
                        //计算翻倍积分907
                        if (p.IsRule907 == 1)
                        {
                            foreach (Lebi_User_Product pro in Products)
                            {
                                pro.Pointagain = pro.Pointagain + p.Rule907;
                            }
                        }
                        //计算指定商品折扣翻倍积分908
                        if (p.IsRule908 == 1)
                        {
                            if (sps != null)
                            {
                                foreach (Lebi_User_Product pro in Products)
                                {
                                    foreach (Lebi_User_Product sp in sps)
                                    {
                                        if (pro.id == sp.id)
                                            pro.Pointagain = pro.Pointagain + p.Rule908;
                                    }
                                }

                            }
                        }
                        //赠送商品909
                        if (p.IsRule909 == 1)
                        {
                            //暂时不做
                        }
                        //赠送指定商品910
                        if (p.IsRule910 == 1)
                        {
                            if (sps != null)
                            {

                                foreach (Lebi_User_Product sp in sps)
                                {

                                    sp.count = p.Rule910;
                                    sp.Product_Price = 0;
                                    sp.Product_Point = 0;
                                    sp.Pointagain = 0;
                                    sp.Discount = 0;
                                    FreeProducts.Add(sp);

                                }

                            }
                        }
                        //第N个指定商品打折
                        if (p.IsRule912 == 1 && p.IsCase803 == 1 && p.Case803 > 0)
                        {
                            foreach (Lebi_User_Product pro in Products)
                            {
                                foreach (Lebi_User_Product sp in sps)
                                {
                                    if (pro.id == sp.id)
                                    {
                                        if (pro.count > p.Case803)
                                        {
                                            int n = sp.count / p.Case803;
                                            Money_Cut = Money_Cut + pro.Product_Price * (100 - p.Rule912) / 100 * n;
                                        }
                                    }
                                }
                            }

                        }
                        //重新计算价格积分,

                        Money_Product = 0;
                        Point_Product = 0;
                        Point = 0;
                        foreach (Lebi_User_Product pro in Products)
                        {
                            Money_Product += Math.Round(pro.Product_Price * pro.count * pro.Discount / 100, 2);
                            Point_Product += Math.Round(pro.Product_Point * pro.count * pro.Pointagain);
                            Point = Point_Product + Point_Free;
                        }

                        break;//验证成功不再验证
                    }
                }
                if (flag)
                    PromotionTypes.Add(cp);
            }
            basketshop = new BasketShop();
            basketshop.FreeProducts = FreeProducts;
            basketshop.Money_Cut = Money_Cut;
            basketshop.Money_Give = Money_Give;
            basketshop.Money_Market = Money_Market;
            basketshop.Money_Product = Money_Product;
            basketshop.Money_Product_begin = Money_Product_begin;
            basketshop.Money_Transport = 0;
            basketshop.Point = Point;
            basketshop.Point_Free = Point_Free;
            basketshop.Point_Buy = Point_Buy;
            basketshop.Point_Product = Point_Product;
            basketshop.Products = Products;
            basketshop.PromotionTypes = PromotionTypes;
            basketshop.Shop = shop;
            basketshop.Volume = Volume;
            basketshop.Weight = Weight;
            basketshop.IsTransportPriceOne = IsTransportPriceOne;
            basketshop.Money_Transport_One = Money_Transport_One;
            basketshop.Count = Count;
            basketshop.Money_Property = Money_Property;
            return basketshop;

        }

    }

}

