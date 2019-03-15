using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Shop.Ajax
{
    /// <summary>
    /// 订单单相关操作
    /// 登录后才能进行的动作
    /// </summary>
    public partial class Ajax_order : ShopPage
    {
        public void LoadPage()
        {
            if (CurrentUser.id == 0)
            {
                //未登录
                if (SYS.IsAnonymousUser == "1")
                {
                    CurrentUser = EX_User.CreateAnonymous();
                }
                else
                {
                    Response.Write("{\"msg\":\"请登录\"}");
                    Response.End();
                    return;
                }
            }
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }


        /// <summary>
        /// 添加/编辑收货人信息
        /// </summary>
        public void Address_Edit()
        {

            int id = RequestTool.RequestInt("id", 0);
            Lebi_User_Address model = B_Lebi_User_Address.GetModel("User_id=" + CurrentUser.id + " and id = " + id);
            if (model == null)
            {
                model = new Lebi_User_Address();
                model = B_Lebi_User_Address.SafeBindForm(model);
                model.User_id = CurrentUser.id;
                B_Lebi_User_Address.Add(model);
                id = B_Lebi_User_Address.GetMaxId("User_id=" + CurrentUser.id + "");
            }
            else
            {
                model = B_Lebi_User_Address.SafeBindForm(model);
                B_Lebi_User_Address.Update(model);
            }
            CurrentUser.User_Address_id = id;
            B_Lebi_User.Update(CurrentUser);
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }
        /// <summary>
        /// 删除收货人信息
        /// </summary>
        public void Address_Del()
        {
            string id = RequestTool.RequestSafeString("id");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_User_Address.Delete("User_id = " + CurrentUser.id + " and id in (lbsql{" + id + "})");
            Lebi_User_Address model = B_Lebi_User_Address.GetModel("User_id = " + CurrentUser.id + " and id = " + CurrentUser.User_Address_id + "");
            if (model == null)
            {
                Lebi_User_Address models = B_Lebi_User_Address.GetModel("User_id = " + CurrentUser.id + "");
                if (models != null)
                {
                    CurrentUser.User_Address_id = B_Lebi_User_Address.GetMaxId("User_id=" + CurrentUser.id + "");
                }
                B_Lebi_User.Update(CurrentUser);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 设置收货人
        /// </summary>
        public void Address_Set()
        {
            int id = RequestTool.RequestInt("address_id", 0);
            Lebi_User_Address model = B_Lebi_User_Address.GetModel("User_id=" + CurrentUser.id + " and id = " + id);
            if (model != null)
            {
                if (model.User_id == CurrentUser.id)
                {

                    CurrentUser.User_Address_id = model.id;
                    B_Lebi_User.Update(CurrentUser);
                    Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
                    return;
                }
            }
            Response.Write("{\"msg" + Tag("参数错误") + "\"}");
        }
        /// <summary>
        /// 设置运输方式
        /// </summary>
        public void transport_Set()
        {
            string id = RequestTool.RequestSafeString("transport_id");
            string pickupid = RequestTool.RequestSafeString("pickup_id");
            DateTime pickupdate = RequestTool.RequestDate("pickupdate");
            id = id.Trim(',');
            pickupid = pickupid.Trim(',');
            CurrentUser.Transport_Price_id = id;
            CurrentUser.PickUp_id = pickupid;
            CurrentUser.PickUp_Date = pickupdate;
            B_Lebi_User.Update(CurrentUser);
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }
        /// <summary>
        /// 生成新订单
        /// </summary>
        public void order_save()
        {
            if (CurrentUserLevel.BuyRight != 1)
            {
                Response.Write("{\"msg\":\"" + Tag("您所在的分组不允许下单") + "\"}");
                return;
            }
            int pay_id = RequestTool.RequestInt("pay_id", 0);
            int sid = RequestTool.RequestInt("sid", 0);//结算供应商ID
            int onlinepay_id = RequestTool.RequestInt("onlinepay_id", 0);
            decimal Money_UserCut = RequestTool.RequestDecimal("Money_UserCut", 0);
            int usermoneytype = RequestTool.RequestInt("usermoneytype", 0);
            string Pay_Password = RequestTool.RequestSafeString("Pay_Password");
            Lebi_PickUp pick = null;
            DateTime pickdate = System.DateTime.Now;
            if (usermoneytype == 3)
            {
                if (Pay_Password == "")
                {
                    Response.Write("{\"msg\":\"" + Tag("请输入支付密码") + "\"}");
                    return;
                }
                else
                {
                    if (EX_User.MD5(Pay_Password) != CurrentUser.Pay_Password)
                    {
                        Response.Write("{\"msg\":\"" + Tag("支付密码不正确") + "\"}");
                        return;
                    }
                }
                if (Money_UserCut > 0 && Money_UserCut > CurrentUser.Money)
                {
                    Response.Write("{\"msg\":\"" + Tag("余额不足") + "\"}");
                    return;
                }
            }
            Lebi_Pay pay = B_Lebi_Pay.GetModel(pay_id);
            if (pay == null)
            {
                Response.Write("{\"msg\":\"" + Tag("请设置付款方式") + "\"}");
                return;
            }
            Basket basket = new Basket(sid);
            int CustomOfflineMoney = RequestTool.RequestInt("CustomOfflineMoney" + pay.id, 0);
            decimal OfflineMoney = RequestTool.RequestDecimal("OfflineMoney" + pay.id, 0);
            if (CustomOfflineMoney == 1)
            {
                if (pay.Code != "OfflinePay" && pay.Code != "OnlinePay")
                {
                    //订单如果选择了线下支付，并且非货到付款
                    if (OfflineMoney < basket.Money_Product)
                    {
                        Response.Write("{\"msg\":\"" + Tag("打款金额不能少于订单金额") + "\"}");
                        return;
                    }
                }
            }
            if (pay.Code == "OnlinePay")
            {
                Lebi_OnlinePay onpay = B_Lebi_OnlinePay.GetModel(onlinepay_id);
                if (onpay == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("请设置付款方式") + "\"}");
                    return;
                }
            }

            if (basket.Products.Count == 0)
            {
                Response.Write("{\"msg\":\"" + Tag("购物车为空") + "\"}");
                return;
            }
            foreach (Lebi_User_Product up in basket.Products)
            {
                if (up.count < 1)
                {
                    Response.Write("{\"msg\":\"" + Tag("购物车异常") + "\"}");
                    return;
                }
            }
            if (basket.Point_Buy > 0 && (basket.Point_Buy > CurrentUser.Point))
            {
                Response.Write("{\"msg\":\"" + Tag("积分不足") + "\"}");
                return;
            }
            int ProductCount = 0;
            foreach (Lebi_User_Product up in basket.Products)
            {
                Lebi_Product pro = B_Lebi_Product.GetModel(up.Product_id);
                if (pro.Type_id_ProductType != 323)
                {
                    ProductCount += up.count;
                }
                //<-{ 判断是否上架状态 by lebi.kingdge 2015-02-10
                if (pro.Type_id_ProductStatus != 101)
                {
                    Response.Write("{\"msg\":\"" + Lang(pro.Name) + " " + Tag("该商品已经下架") + "\"}");
                    return;
                }
                //}->
                if (pro.Type_id_ProductType != 324)
                {
                    int levelcount = ProductLevelCount(pro);
                    if (up.count < levelcount)
                    {
                        Response.Write("{\"msg\":\"" + Lang(pro.Name) + " " + Tag("起订量") + " " + levelcount + "\"}");
                        return;
                    }
                }
                if (pro.Type_id_ProductType != 320 && pro.Time_Expired > System.DateTime.Now)
                {
                    if (pro.Count_Limit < up.count && pro.Count_Limit > 0)
                    {
                        Response.Write("{\"msg\":\"" + Tag("购买数量大于限购数量") + "\"}");
                        return;
                    }
                }
                if (SYS.IsNullStockSale != "1")
                {
                    if (pro.Count_Stock - pro.Count_Freeze < up.count && pro.Type_id_ProductType != 324)
                    {
                        Response.Write("{\"msg\":\"" + Lang(pro.Name) + "" + Tag("库存不足") + "\"}");
                        return;
                    }
                }
            }
            //验证当前分组允许的最低订单提交金额
            if (CurrentUserLevel.OrderSubmit > 0)
            {
                if (basket.Money_Product < CurrentUserLevel.OrderSubmit)
                {
                    Response.Write("{\"msg\":\"" + Tag("单笔订单最低金额为：") + FormatMoney(CurrentUserLevel.OrderSubmit) + "\"}");
                    return;
                }
            }
            //验证当前分组允许的最低订单提交数量
            if (CurrentUserLevel.OrderSubmitCount > 0 && ProductCount > 0)
            {
                if (ProductCount < CurrentUserLevel.OrderSubmitCount)
                {
                    Response.Write("{\"msg\":\"" + Tag("单笔订单最低数量为：") + CurrentUserLevel.OrderSubmitCount + "\"}");
                    return;
                }
            }
            Lebi_User_Address shouhuo = B_Lebi_User_Address.GetModel(CurrentUser.User_Address_id);
            if (shouhuo == null)
            {
                Response.Write("{\"msg\":\"" + Tag("未设置收获地址") + "\"}");
                return;
            }
            if (CurrentUser.Transport_Price_id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("运费设置错误") + "\"}");
                return;
            }

            foreach (BasketShop shop in basket.Shops)
            {

                Lebi_Transport_Price tprice = B_Lebi_Transport_Price.GetModel("id in (lbsql{" + CurrentUser.Transport_Price_id + "}) and Supplier_id=" + shop.Shop.id + "");
                if (tprice == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("运费设置错误") + "\"}");
                    return;
                }
                Lebi_Transport transport = B_Lebi_Transport.GetModel(tprice.Transport_id);
                if (tprice == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("运费设置错误") + "\"}");
                    return;
                }
                //检查运费设置是否正确
                if (!EX_Area.CheckAreaPrice(tprice, shouhuo.Area_id))
                {
                    Response.Write("{\"msg\":\"" + Tag("运费设置错误") + "\"}");
                    return;
                }
                if (transport.Type_id_TransportType == 332)//自提检查
                {
                    int pickup_id = RequestTool.RequestInt("pickup_id" + shop.Shop.id);
                    string pickdate_ = RequestTool.RequestString("pickupdate_" + pickup_id);

                    try
                    {
                        pickdate = Convert.ToDateTime(pickdate_);
                    }
                    catch
                    {
                        Response.Write("{\"msg\":\"" + Tag("配送方式设置错误") + "\"}");
                        return;
                    }

                    pick = B_Lebi_PickUp.GetModel(pickup_id);
                    if (pick == null)
                    {
                        Response.Write("{\"msg\":\"" + Tag("配送方式设置错误") + "\"}");
                        return;
                    }
                    if (pick.IsCanWeekend == 0 && (pickdate.DayOfWeek == DayOfWeek.Saturday || pickdate.DayOfWeek == DayOfWeek.Sunday))
                    {
                        Response.Write("{\"msg\":\"" + Tag("配送方式设置错误") + "\"}");
                        return;
                    }
                    if (System.DateTime.Now.Date.AddDays(pick.BeginDays) > pickdate)
                    {
                        Response.Write("{\"msg\":\"" + Tag("配送方式设置错误") + "\"}");
                        return;
                    }
                    string NoServiceDays = pick.NoServiceDays.TrimStart('0').Replace(".0", ".");
                    string nowday = pickdate.ToString("M.d");
                    if (("," + NoServiceDays + ",").Contains("," + nowday + ","))
                    {
                        Response.Write("{\"msg\":\"" + Tag("配送方式设置错误") + "\"}");
                        return;
                    }
                }
            }
            //检查代金券
            string pay312 = RequestTool.RequestSafeString("pay312");
            if (pay312 != "")
            {
                List<Lebi_Card> cs = B_Lebi_Card.GetList("User_id=" + CurrentUser.id + " and id in (lbsql{" + pay312 + "})", "id asc");
                int flag = cs.FirstOrDefault().IsCanOtherUse;
                if (flag == 0 && cs.Count > 1)
                {
                    Response.Write("{\"msg\":\"" + Tag("代金券异常") + "\"}");
                    return;
                }
                foreach (Lebi_Card c in cs)
                {
                    if (flag != c.IsCanOtherUse)
                    {
                        Response.Write("{\"msg\":\"" + Tag("代金券异常") + "\"}");
                        return;
                    }
                    if (!Basket.CheckCard(basket, c))
                    {
                        Response.Write("{\"msg\":\"" + Tag("代金券异常") + "\"}");
                        return;
                    }
                }
            }
            //检查代金券结束
            //检查发票信息
            int billtype_id = RequestTool.RequestInt("billtype_id", 0);
            Lebi_BillType billtype = B_Lebi_BillType.GetModel(billtype_id);
            if (ShopCache.GetBaseConfig().BillFlag == "1")
            {
                if (billtype == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("请设置发票内容") + "\"}");
                    return;
                }
            }
            CurrentUser.Pay_id = pay.id;
            CurrentUser.OnlinePay_id = onlinepay_id;
            Lebi_Order ordergroup;
            List<Lebi_Order> orders = Shop.Bussiness.Order.CreateOrder(CurrentUser, basket, shouhuo, billtype, CurrentCurrency, CurrentSite, CurrentLanguage, out ordergroup);
            CurrentUser.Count_Order = CurrentUser.Count_Order + orders.Count;
            B_Lebi_User.Update(CurrentUser);
            List<Lebi_Order> ordermodel = B_Lebi_Order.GetList("User_id=" + CurrentUser.id + "", "id desc");
            ordergroup = ordermodel.FirstOrDefault();
            if (OfflineMoney == 0)
            {
                OfflineMoney = ordergroup.Money_Order;
            }
            string remark = RequestTool.RequestSafeString("remark");
            ordergroup.Remark_User = remark;
            bool needupdate = false;
            if (remark != "")
            {
                Lebi_Comment model = new Lebi_Comment();
                model.Content = remark;
                model.Keyid = ordergroup.id;
                model.TableName = "Order";
                model.User_id = CurrentUser.id;
                model.User_UserName = CurrentUser.UserName;
                B_Lebi_Comment.Add(model);
                needupdate = true;
            }

            if (pick != null)
            {
                ordergroup.PickUp_Date = pickdate;
                ordergroup.PickUp_id = pick.id;
                ordergroup.PickUp_Name = pick.Name;
                needupdate = true;
            }
            if (needupdate)
            {
                B_Lebi_Order.Update(ordergroup);
            }

            if (pay.Code != "OfflinePay" && pay.Code != "OnlinePay")
            {
                //订单如果选择了线下支付，并且非货到付款
                //生成一笔充值单
                //OfflineMoney

                Lebi_Currency DefaultCurrency = B_Lebi_Currency.GetModel("IsDefault=1");
                if (DefaultCurrency == null)
                    DefaultCurrency = B_Lebi_Currency.GetList("", "Sort desc").FirstOrDefault();
                Lebi_Order order = new Lebi_Order();
                order.Code = "M" + Shop.Bussiness.Order.CreateOrderCode();
                order.Money_Order = OfflineMoney;
                order.Money_Pay = OfflineMoney;
                order.User_id = CurrentUser.id;
                order.User_UserName = CurrentUser.UserName;
                order.IsPaid = 0;
                order.Currency_Code = ordergroup.Currency_Code;
                order.Currency_ExchangeRate = ordergroup.Currency_ExchangeRate;
                order.Currency_id = ordergroup.Currency_id;
                order.Currency_Msige = ordergroup.Currency_Msige;
                order.Type_id_OrderType = 214;
                order.Pay_id = pay.id;
                order.Pay = pay.Name;
                order.Site_id = CurrentSite.id;
                order.Language_id = CurrentLanguage.id;
                order.Remark_Admin = "";
                order.Order_id = ordergroup.id;
                B_Lebi_Order.Add(order);
            }
            //if (orders.Count == 1)
            //    ordergroup = orders.FirstOrDefault();
            //Shop.Bussiness.Order.SupplierOrder(order);//根据商品供应商分单
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + ordergroup.id + "\"}");
            return;
        }
        /// <summary>
        /// 生成退货单
        /// </summary>
        public void torder_save()
        {
            int order_id = RequestTool.RequestInt("order_id", 0);
            string opid = RequestTool.RequestSafeString("opid");
            if (opid == "")
            {
                Response.Write("{\"msg\":\"" + Tag("未选择任何商品") + "\"}");
                return;
            }
            int count = 0;
            Lebi_Order order = B_Lebi_Order.GetModel(order_id);
            if (order == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            if (order.User_id != CurrentUser.id)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            List<Lebi_Order_Product> ops = B_Lebi_Order_Product.GetList("id in (lbsql{" + opid + "}) and Order_id=" + order.id + "", "");

            foreach (Lebi_Order_Product op in ops)
            {

                int rcount = RequestTool.RequestInt("product_" + op.id, 0);
                if (rcount > (op.Count_Received - op.Count_Return))
                {
                    Response.Write("{\"msg\":\"" + Tag("退货数量不能大于收货数量") + "\"}");
                    return;
                }
                if (rcount < 1)
                {
                    Response.Write("{\"msg\":\"" + Tag("退货数量不能小于1") + "\"}");
                    return;
                }
                count = count + rcount;
            }
            if (count == 0)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            //CurrentUser.Count_Order = CurrentUser.Count_Order + 1;
            //B_Lebi_User.Update(CurrentUser);
            //foreach (Lebi_Order_Product op in ops)
            //{
            //    op.Count_Return = op.Count_Return + RequestTool.RequestInt("product_" + op.id, 0);
            //    B_Lebi_Order_Product.Update(op);
            //}
            Lebi_Order model = new Lebi_Order();
            model.Order_id = order.id;
            model.User_id = CurrentUser.id;
            model.User_UserName = CurrentUser.UserName;
            model.T_Name = order.T_Name;
            model.T_Address = order.T_Address;
            model.T_Area_id = order.T_Area_id;
            model.T_MobilePhone = order.T_MobilePhone;
            model.T_Phone = order.T_Phone;
            model.T_Postalcode = order.T_Postalcode;
            model.Weight = 0;
            model.Money_Product = 0;
            model.Type_id_OrderType = 212;
            model.Code = Shop.Bussiness.Order.CreateOrderCode();
            model.Supplier_id = order.Supplier_id;
            model.Site_id = CurrentSite.id;
            model.Language_id = CurrentLanguage.id;
            B_Lebi_Order.Add(model);
            model.id = B_Lebi_Order.GetMaxId();

            foreach (Lebi_Order_Product op in ops)
            {

                count = RequestTool.RequestInt("product_" + op.id, 0);
                op.Count_Return = op.Count_Return + count;
                B_Lebi_Order_Product.Update(op);

                op.Count = count;
                op.Count_Shipped = 0;
                op.Count_Received = 0;
                op.Order_Code = model.Code;
                op.Order_id = model.id;
                op.Money = op.Price * op.Count;

                B_Lebi_Order_Product.Add(op);
                model.Money_Product = model.Money_Product + (op.Price - op.Money_Give_one - op.Money_Card312_one) * count;
                model.Weight = model.Weight + op.Weight * count;
                model.Volume = model.Weight + op.Volume * count;
                model.Point = model.Point + op.Point_Give_one;

            }
            model.Money_Product = 0 - model.Money_Product;
            model.Money_Order = model.Money_Product;
            model.Money_Pay = model.Money_Product;
            model.Money_Give = 0 - model.Money_Order;
            model.Point = 0 - model.Point;
            B_Lebi_Order.Update(model);
            //处理留言
            Lebi_Comment com = new Lebi_Comment();
            com.Content = RequestTool.RequestSafeString("say");
            com.Keyid = model.id;
            com.TableName = "Order";
            com.User_id = CurrentUser.id;
            com.User_UserName = CurrentUser.UserName;
            B_Lebi_Comment.Add(com);
            Lebi_User user = B_Lebi_User.GetModel(CurrentUser.id);
            Lebi_Order ordermodel = B_Lebi_Order.GetModel("Order_id=" + order_id + "");
            //发送邮件
            if (ShopCache.GetBaseConfig().MailSign.ToLower().Contains("dingdantijiao") || ShopCache.GetBaseConfig().AdminMailSign.ToLower().Contains("ordersubmit"))
            {
                Email.SendEmail_ordersubmit(user, ordermodel);
            }
            //发送短信
            if (ShopCache.GetBaseConfig().SMS_sendmode.Contains("SMSTPL_ordersubmit") || ShopCache.GetBaseConfig().SMS_sendmode.Contains("SMSTPL_Admin_ordersubmit"))
            {

                SMS.SendSMS_ordersubmit(user, order);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 退货单发货
        /// </summary>
        public void torder_shipping()
        {
            int order_id = RequestTool.RequestInt("order_id", 0);
            Lebi_Order model = B_Lebi_Order.GetModel("User_id=" + CurrentUser.id + " and id = " + order_id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            if (model.IsVerified != 1)
            {
                Response.Write("{\"msg\":\"" + Tag("请等待客服人员确认") + "\"}");
                return;
            }
            Lebi_Transport_Order torder = B_Lebi_Transport_Order.GetModel("Order_id=" + model.id + "");
            if (torder != null)
            {
                torder.Code = RequestTool.RequestSafeString("Code");
                torder.Transport_Name = RequestTool.RequestSafeString("Transport_Name");
                B_Lebi_Transport_Order.Update(torder);
            }
            else
            {
                Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(model.Supplier_id);
                if (supplier == null)
                    supplier = new Lebi_Supplier();
                torder = new Lebi_Transport_Order();
                torder.Code = RequestTool.RequestSafeString("Code");
                torder.Order_id = model.id;

                torder.T_Address = model.T_Address;
                torder.T_Email = model.T_Email;
                torder.T_MobilePhone = model.T_MobilePhone;
                torder.T_Name = model.T_Name;
                torder.T_Phone = model.T_Phone;
                //torder.Transport_Code = model.Transport_Code;
                //torder.Transport_id = model.Transport_id;
                torder.Transport_Name = RequestTool.RequestSafeString("Transport_Name");
                torder.User_id = model.User_id;
                torder.Supplier_id = supplier.id;
                torder.Supplier_SubName = supplier.SubName;
                List<TransportProduct> tps = new List<TransportProduct>();
                TransportProduct tp;
                List<Lebi_Order_Product> pros = B_Lebi_Order_Product.GetList("Order_id=" + model.id + "", "");

                foreach (Lebi_Order_Product pro in pros)
                {

                    tp = new TransportProduct();
                    tp.Count = pro.Count;
                    tp.ImageBig = pro.ImageBig;
                    tp.ImageMedium = pro.ImageMedium;
                    tp.ImageOriginal = pro.ImageOriginal;
                    tp.ImageSmall = pro.ImageSmall;
                    tp.Product_Number = pro.Product_Number;
                    tp.Product_id = pro.Product_id;
                    tp.Product_Name = pro.Product_Name;
                    tps.Add(tp);

                    pro.Count_Shipped = pro.Count;

                    B_Lebi_Order_Product.Update(pro);
                }

                JavaScriptSerializer jss = new JavaScriptSerializer();
                torder.Product = jss.Serialize(tps);
                torder.Type_id_TransportOrderStatus = 220;//默认状态：在途
                B_Lebi_Transport_Order.Add(torder);
                model.IsShipped = 1;
                model.IsShipped_All = 1;

                B_Lebi_Order.Update(model);
            }
            Response.Write("{\"msg\":\"OK\"}");

        }
        /// <summary>
        /// 确认收货
        /// </summary>
        public void Received()
        {
            int id = RequestTool.RequestInt("id", 0);
            int tid = RequestTool.RequestInt("tid", 0);
            Lebi_Transport_Order torder = B_Lebi_Transport_Order.GetModel("User_id=" + CurrentUser.id + " and id = " + tid);
            if (torder == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            if (torder.Type_id_TransportOrderStatus != 220)
            {
                Response.Write("{\"msg\":\"" + Tag("当前状态不可收货") + "\"}");
                return;
            }
            if (torder.User_id != CurrentUser.id)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            torder.Type_id_TransportOrderStatus = 223;
            torder.Time_Received = System.DateTime.Now;
            B_Lebi_Transport_Order.Update(torder);
            EX_Area.UpdateShouHuoCount(torder);
            //发送短信
            SMS.SendSMS_orderrecive(CurrentUser, torder);
            Response.Write("{\"msg\":\"OK\"}");

        }
        /// <summary>
        /// 提交订单留言
        /// </summary>
        public void OrderComment_Edit()
        {
            string comment = RequestTool.RequestSafeString("comment");
            int order_id = RequestTool.RequestInt("order_id", 0);
            Lebi_Order order = B_Lebi_Order.GetModel("User_id=" + CurrentUser.id + " and id = " + order_id);
            if (order == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Lebi_Comment model = new Lebi_Comment();
            model.Content = comment;
            model.Keyid = order_id;
            model.TableName = "Order";
            model.User_id = CurrentUser.id;
            model.User_UserName = CurrentUser.UserName;
            B_Lebi_Comment.Add(model);
            //发送邮件
            Email.SendEmail_ordercomment(CurrentUser, model);
            //发送短信
            SMS.SendSMS_ordercomment(CurrentUser, model);
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        public void OrderCancal()
        {
            int id = RequestTool.RequestInt("id", 0);
            string Remark = RequestTool.RequestSafeString("Remark");
            Lebi_Order order = B_Lebi_Order.GetModel("User_id=" + CurrentUser.id + " and id = " + id + ""); //增加未审核判断 by lebi.kingdge 2015-04-21
            if (order == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            if (!Shop.Bussiness.Order.CancelOrder(order))
            {
                Response.Write("{\"msg\":\"" + Tag("已取消") + "\"}");
                return;
            }
            if (order.IsShipped == 1 || order.IsShipped_All == 1 || order.IsCompleted == 1 || order.IsReceived == 1 || order.IsReceived_All == 1)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Lebi_Comment model = new Lebi_Comment();
            if (Remark != "")
            {
                model.Content = Tag("取消订单") + "：" + Remark;
            }
            else
            {
                model.Content = Tag("取消订单");
            }
            model.Keyid = order.id;
            model.TableName = "Order";
            model.User_id = CurrentUser.id;
            model.User_UserName = CurrentUser.UserName;
            B_Lebi_Comment.Add(model);
            if (order.IsPaid == 0)
            {
                Shop.Bussiness.Order.Order_Cancal(order);
            }
            else
            {
                order.IsRefund = 2;
                order.Time_Refund = System.DateTime.Now;
                B_Lebi_Order.Update(order);
            }
            Log.Add("取消订单", "Order", order.id.ToString(), CurrentUser);
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 收银台查询购物卡
        /// </summary>
        public void MoneyCardCheack()
        {
            string moneycardcode = RequestTool.RequestSafeString("moneycardcode");
            string moneycardpwd = RequestTool.RequestSafeString("moneycardpwd");
            Lebi_Card card = B_Lebi_Card.GetModel("Code=lbsql{'" + moneycardcode + "'} and Type_id_CardStatus in (201,203)");
            if (card == null)
            {
                Response.Write("{\"msg\":\"" + Tag("输入错误") + "\"}");
                return;
            }
            if (card.Time_End < System.DateTime.Now)
            {
                Response.Write("{\"msg\":\"" + Tag("已过期") + "\"}");
                return;
            }
            if (card.Password != moneycardpwd)
            {
                Response.Write("{\"msg\":\"" + Tag("输入错误") + "\"}");
                return;
            }
            Lebi_CardOrder cardorder = B_Lebi_CardOrder.GetModel(card.CardOrder_id);
            if (cardorder == null)
                cardorder = new Lebi_CardOrder();
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + card.id + "\",\"name\":\"" + Lang(cardorder.Name) + "\",\"code\":\"" + card.Code + "\",\"timeend\":\"" + card.Time_End.ToString("yyyy-MM-dd") + "\",\"money\":\"" + FormatMoney(card.Money) + "\",\"money_used\":\"" + FormatMoney(card.Money_Used) + "\",\"money_last\":\"" + FormatMoney((card.Money_Last)) + "\",\"money_lastvalue\":\"" + card.Money_Last + "\"}");
        }
        /// <summary>
        /// 生成充值订单
        /// </summary>
        public void CreateMoneyOrder()
        {
            decimal money = RequestTool.RequestDecimal("RMoney", 0);
            if (money == 0)
            {
                Response.Write("{\"msg\":\"" + Tag("充值金额必须大于0") + "\"}");
                return;
            }
            Lebi_Currency DefaultCurrency = B_Lebi_Currency.GetModel("IsDefault=1");
            if (DefaultCurrency == null)
                DefaultCurrency = B_Lebi_Currency.GetList("", "Sort desc").FirstOrDefault();
            int onlinepay_id = RequestTool.RequestInt("onlinepay_id");
            int pay_id = RequestTool.RequestInt("Pay_id");
            int paytype = RequestTool.RequestInt("paytype");
            //Lebi_MoneyOrder order = new Lebi_MoneyOrder();
            //order.Code = "M" + Shop.Bussiness.Order.CreateOrderCode();
            //order.Money = money;
            //order.User_id = CurrentUser.id;
            //order.User_UserName = CurrentUser.UserName;
            //order.IsPaid = 0;
            //order.Currency_Code = DefaultCurrency.Code;
            //order.Currency_ExchangeRate = DefaultCurrency.ExchangeRate;
            //order.Currency_id = DefaultCurrency.id;
            //order.Currency_Msige = DefaultCurrency.Msige;
            //B_Lebi_MoneyOrder.Add(order);
            Lebi_Order order = new Lebi_Order();
            order.Code = "M" + Shop.Bussiness.Order.CreateOrderCode();
            order.Money_Order = money;
            order.Money_Pay = money;
            order.User_id = CurrentUser.id;
            order.User_UserName = CurrentUser.UserName;
            order.IsPaid = 0;
            order.Currency_Code = DefaultCurrency.Code;
            order.Currency_ExchangeRate = DefaultCurrency.ExchangeRate;
            order.Currency_id = DefaultCurrency.id;
            order.Currency_Msige = DefaultCurrency.Msige;
            order.Type_id_OrderType = 214;
            Lebi_Pay pay = B_Lebi_Pay.GetModel(pay_id);
            if (paytype == 0)
            {

                Lebi_OnlinePay onlinepay = B_Lebi_OnlinePay.GetModel(onlinepay_id);
                if (onlinepay == null)
                    onlinepay = new Lebi_OnlinePay();
                order.OnlinePay_id = onlinepay.id;
                order.OnlinePay = onlinepay.Name;
                pay = B_Lebi_Pay.GetModel("Code='OnlinePay'");
            }
            if (pay == null)
                pay = new Lebi_Pay();
            order.Pay_id = pay.id;
            order.Pay = pay.Name;

            order.Site_id = CurrentSite.id;
            order.Language_id = CurrentLanguage.id;
            B_Lebi_Order.Add(order);
            order.id = B_Lebi_Order.GetMaxId();
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + order.id + "\",\"code\":\"" + order.Code + "\",\"url\":\"" + URL("P_Pay", order.id + "," + order.Code) + "\"}");

        }

        /// <summary>
        /// 修改订单的自提时间
        /// </summary>
        public void OrderPickUpDate_Edit()
        {
            int orderid = RequestTool.RequestInt("orderid", 0);
            Lebi_Order order = B_Lebi_Order.GetModel("User_id=" + CurrentUser.id + " and id = " + orderid + "");//and IsCompleted = 0 and IsReceived=0
            if (order == null)
            {
                Response.Write("{\"msg\":\"" + Tag("输入错误") + "\"}");
                return;
            }
            if (order.IsCompleted == 1 || order.IsReceived != 0)
            {
                Response.Write("{\"msg\":\"" + Tag("当前订单状态不允许修改") + "\"}");
                return;
            }
            DateTime pickupdate = RequestTool.RequestTime("pickupdate");
            order.PickUp_Date = pickupdate;
            B_Lebi_Order.Update(order);
            Response.Write("{\"msg\":\"OK\"}");
        }
    }


}