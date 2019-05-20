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

namespace Shop.Admin.Ajax
{
    /// <summary>
    /// 订单相关的操作
    /// </summary>
    public partial class Ajax_order : AdminAjaxBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);
        }
        /// <summary>
        /// 获取订单数据
        /// </summary>
        public void GetOrder()
        {
            int unVerified = B_Lebi_Order.Counts("Type_id_OrderType = 211 and IsVerified = 0 and IsInvalid = 0");
            int Verified = B_Lebi_Order.Counts("Type_id_OrderType = 211 and IsVerified = 1 and IsInvalid = 0");
            int unPaid = B_Lebi_Order.Counts("Type_id_OrderType = 211 and IsPaid = 0 and IsShipped = 0 and IsInvalid = 0");
            int Paid = B_Lebi_Order.Counts("Type_id_OrderType = 211 and IsPaid = 1 and IsShipped = 0 and IsInvalid = 0");
            int unShipped = B_Lebi_Order.Counts("Type_id_OrderType = 211 and IsVerified = 1 and IsShipped = 0 and IsInvalid = 0");
            int Shipped = B_Lebi_Order.Counts("Type_id_OrderType = 211 and IsVerified = 1 and IsShipped = 1 and IsInvalid = 0");
            int unReceived = B_Lebi_Order.Counts("Type_id_OrderType = 211 and IsVerified = 1 and IsShipped = 1 and isReceived_All = 0 and IsInvalid = 0");
            int Received = B_Lebi_Order.Counts("Type_id_OrderType = 211 and IsVerified = 1 and IsShipped = 1 and isReceived_All = 1 and IsInvalid = 0");
            int unCompleted = B_Lebi_Order.Counts("Type_id_OrderType = 211 and Iscompleted = 0");
            B_BaseConfig dob = new B_BaseConfig();
            BaseConfig model = new BaseConfig();
            model.NewEvent_Order_IsVerified = unVerified.ToString();
            model.NewEvent_Order_IsPaid = Paid.ToString();
            model.NewEvent_Order_IsShipped = unShipped.ToString();
            dob.SaveConfig(model);
            Response.Write("{\"unVerified\":\"" + unVerified + "\",\"Verified\":\"" + Verified + "\",\"Paid\":\"" + Paid + "\",\"unPaid\":\"" + unPaid + "\",\"unShipped\":\"" + unShipped + "\",\"Shipped\":\"" + Shipped + "\",\"unReceived\":\"" + unReceived + "\",\"Received\":\"" + Received + "\",\"unCompleted\":\"" + unCompleted + "\"}");
        }
        /// <summary>
        /// 编辑订单
        /// </summary>
        public void Order_Edit()
        {
            if (!EX_Admin.Power("order_edit", "编辑订单"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Order model = B_Lebi_Order.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
            }
            else
            {
                model = B_Lebi_Order.BindForm(model);
                B_Lebi_Order.Update(model);
            }

            Log.Add("编辑订单", "Order", id.ToString(), CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑订单-收货人
        /// </summary>
        public void Order_shouhuo_Edit()
        {
            if (!EX_Admin.Power("order_edit", "编辑订单"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Order model = B_Lebi_Order.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
            }
            else
            {
                model = B_Lebi_Order.BindForm(model);
                B_Lebi_Order.Update(model);
            }
            Log.Add("编辑收货人", "Order", id.ToString(), CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑订单-订单状态变更
        /// </summary>
        public void Order_type()
        {
            int id = RequestTool.RequestInt("id", 0);
            int t = RequestTool.RequestInt("t", 2);
            string str = RequestTool.RequestString("model");
            string action = "";
            Lebi_Order model = B_Lebi_Order.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
            }
            else
            {
                if (str == "IsRefund")
                {
                    if (!EX_Admin.Power("order_status_edit", "订单审核"))
                    {
                        AjaxNoPower();
                        return;
                    }
                    SystemLog.Add(model.Code + "-取消订单-" + t + "");
                    action = "取消订单";
                    if (t == 1)
                    {
                        action += " 同意";
                        if (model.Supplier_id > 0)
                        {
                            Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(model.Supplier_id);
                            if (supplier.IsCash == 1)
                            {
                                Response.Write("{\"msg:\"" + Tag("独立收款商家不能操作取消订单") + "\"}");
                                return;
                            }
                        }
                        Order.Order_Cancal(model, 1);//取消订单
                        Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                        if (user != null)
                        {
                            Lebi_Language lang = B_Lebi_Language.GetModel("Code='" + user.Language + "'");
                            Lebi_Comment mes = new Lebi_Comment();
                            mes.Content = Language.Tag("取消订单", user.Language) + "：" + Language.Tag("同意", user.Language);
                            mes.Keyid = model.id;
                            mes.TableName = "Order";
                            mes.User_id = 0;
                            mes.User_UserName = "";
                            mes.Admin_id = CurrentAdmin.id;
                            mes.Admin_UserName = Language.Tag("管理员", user.Language);
                            B_Lebi_Comment.Add(mes);
                        }
                    }
                    else
                    {
                        action += " 拒绝";
                        model.IsRefund = 3;
                        B_Lebi_Order.Update(model);
                        Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                        if (user != null)
                        {
                            Lebi_Language lang = B_Lebi_Language.GetModel("Code='" + user.Language + "'");
                            Lebi_Comment mes = new Lebi_Comment();
                            mes.Content = Language.Tag("取消订单", user.Language) + "：" + Language.Tag("拒绝", user.Language);
                            mes.Keyid = model.id;
                            mes.TableName = "Order";
                            mes.User_id = 0;
                            mes.User_UserName = "";
                            mes.Admin_id = CurrentAdmin.id;
                            mes.Admin_UserName = Language.Tag("管理员", user.Language);
                            B_Lebi_Comment.Add(mes);
                        }
                    }
                    Log.Add(action, "Order", id.ToString(), CurrentAdmin, model.Code);

                }
                if (str == "IsInvalid")
                {
                    if (!EX_Admin.Power("order_status_edit", "订单审核"))
                    {
                        AjaxNoPower();
                        return;
                    }
                    action = "订单审核";
                    if (t == 1)
                    {
                        if (model.IsPaid == 1)
                        {
                            Response.Write("{\"msg\":\"" + Tag("已付款订单不能进行此操作") + "\"}");
                            return;
                        }
                        if (model.IsShipped == 1 || model.IsShipped_All == 1)
                        {
                            Response.Write("{\"msg\":\"" + Tag("已发货订单不能进行此操作") + "\"}");
                            return;
                        }
                        if (model.IsCompleted == 1)
                        {
                            Response.Write("{\"msg\":\"" + Tag("已完成订单不能进行此操作") + "\"}");
                            return;
                        }
                        action += " 无效";
                        Order.Order_Cancal(model);//取消订单
                    }
                    else
                    {
                        action += " 有效";
                        string res = "";
                        Order.Order_Confirm(model, out res);//修改为有效订单
                        if (res != "")
                        {
                            Response.Write("{\"msg\":\"" + res + "\"}");
                            return;
                        }
                    }
                    Log.Add(action, "Order", id.ToString(), CurrentAdmin, model.Code);

                }
                if (str == "IsVerified")
                {
                    if (!EX_Admin.Power("order_status_edit", "订单审核"))
                    {
                        AjaxNoPower();
                        return;
                    }
                    action = "订单审核";
                    if (t == 0)
                    {
                        if (model.IsShipped == 1 || model.IsShipped_All == 1)
                        {
                            Response.Write("{\"msg\":\"" + Tag("已发货订单不能进行此操作") + "\"}");
                            return;
                        }
                        if (model.IsCompleted == 1)
                        {
                            Response.Write("{\"msg\":\"" + Tag("已完成订单不能进行此操作") + "\"}");
                            return;
                        }
                        action += " 未确认";
                        Order.Order_Check_Cancal(model);
                    }
                    else
                    {
                        action += " 已确认";
                        string res = "";
                        Order.Order_Confirm(model, out res);//修改为有效订单
                        if (res != "")
                        {
                            Response.Write("{\"msg\":\"" + res + "\"}");
                            return;
                        }
                    }
                    Log.Add(action, "Order", id.ToString(), CurrentAdmin, "");
                }
                if (str == "IsPaid")
                {

                    if (!EX_Admin.Power("order_status_ispaid", "订单支付"))
                    {
                        AjaxNoPower();
                        return;
                    }
                    action = "订单支付";
                    if (t == 0)
                    {
                        action += " 未支付";
                        Order.Order_Pay_Cancal(model);
                    }
                    else
                    {
                        action += " 已支付";
                        if (!EX_Admin.Power("checkout_super", "0余额付款"))
                        {
                            Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                            if (user == null)
                            {
                                user = new Lebi_User();
                            }
                            if (user.Money < model.Money_Pay)
                            {
                                Response.Write("{\"msg\":\"" + Tag("余额不足") + "\"}");
                                return;
                            }
                        }
                        // <-{手工操作订单支付状态，不写入进销存财务记录 by lebi.kingdge 2019.1.4
                        model.OnlinePay_Code = "ERP";
                        B_Lebi_Order.Update(model);
                        //}->
                        Order.PaySuccess(model);
                    }
                    Log.Add(action, "Order", id.ToString(), CurrentAdmin, "");
                }
                if (str == "IsCompleted")
                {
                    if (model.IsVerified != 1)
                    {
                        Response.Write("{\"msg\":\"" + Tag("未确认订单不能进行此操作")+"\"}");
                        return;
                    }
                    if (!EX_Admin.Power("order_complete", "订单完成"))
                    {
                        AjaxNoPower();
                        return;
                    }
                    action = "订单完成";
                    if (t == 0)
                    {
                        action += " 未完成";
                        if (model.IsCompleted == 0)
                        {
                            Response.Write("{\"msg\":\"" + Tag("未完成订单不能进行此操作") + "\"}");
                            return;
                        }
                        Order.Order_Completed_Cancal(model);//完成订单-取消
                    }
                    else
                    {
                        if (model.IsPaid != 1)
                        {
                            Response.Write("{\"msg\":\"" + Tag("未付款订单不能进行此操作") + "\"}");
                            return;
                        }
                        if (model.IsShipped_All != 1)
                        {
                            Response.Write("{\"msg\":\"" + Tag("未发货订单不能进行此操作") + "\"}");
                            return;
                        }
                        action += " 已完成";
                        Order.Order_Completed(model);//完成订单
                    }
                    Log.Add(action, "Order", id.ToString(), CurrentAdmin, "");
                }
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 校验订单支付状态
        /// </summary>
        public void Order_Pay_Check()
        {
            if (!EX_Admin.Power("order_status_ispaid", "订单支付"))
            {
                AjaxNoPower();
                return;
            }
            int status = 1;
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Order order = B_Lebi_Order.GetModel(id);
            if (order == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
            }
            //<-{检查资金明细中是否有消费记录 by lebi.kingdge 2018.8.21
            if (order.Money_Order > 0) { 
                decimal money_pay = 0;
                string _money_pay = B_Lebi_User_Money.GetValue("sum(money)", "Order_id = " + order.id + " and Order_PayNo = '" + order.PayNo + "' and (Type_id_MoneyType = 192 or Type_id_MoneyType = 195)");
                money_pay = Convert.ToDecimal(_money_pay);
                if ((0 - money_pay) < (order.Money_Order-order.Money_Paid))
                {
                    Lebi_User user = B_Lebi_User.GetModel(order.User_id);
                    if (user == null)
                    {
                        user = new Lebi_User();
                    }
                    Log.Add("未支付[资金明细校验错误]", "Order", order.id.ToString(), user, "");
                    status = 0;
                }
            }
            //}->
            Response.Write("{\"msg\":\"OK\",\"status\":\""+ status +"\",\"mes\":\"" + Tag("已支付") + "\"}");
        }
        /// <summary>
        /// 删除订单商品
        /// </summary>
        public void OrderPro_Del()
        {
            if (!EX_Admin.Power("order_product_del", "删除订单商品"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestString("proid");
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Order model = B_Lebi_Order.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
            }
            if (ids != "")
            {
                List<Lebi_Order_Product> ps = B_Lebi_Order_Product.GetList("Order_id=" + model.id + " and id in (lbsql{" + ids + "})", "");
                foreach (Lebi_Order_Product p in ps)
                {
                    if (p.Count_Shipped > 0)
                    {
                        Response.Write("{\"msg\":\"" + Tag("商品已发货") + "\"}");
                        return;
                    }
                    if (ShopCache.GetBaseConfig().ProductStockFreezeTime == "orderadd")
                    {
                        ////修改冻结库存
                        Lebi_Product pro = B_Lebi_Product.GetModel(p.Product_id);
                        ////pro.Count_Freeze = pro.Count_Freeze - p.Count;
                        ////B_Lebi_Product.Update(pro);
                        //EX_Product.ProductStock_Freeze(pro, 0 - (p.Count - p.Count_Shipped));
                        //更新库存
                        EX_Product.ProductStock_Change(pro, (p.Count - p.Count_Shipped), 302, model, "删除订单商品");
                    }
                    B_Lebi_Order_Product.Delete(p.id);
                }
                Order.ResetOrder(model);//重新计算订单
            }
            Log.Add("删除订单商品", "Order", model.id.ToString(), CurrentAdmin, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 修改订单商品
        /// </summary>
        public void OrderPro_Edit()
        {
            if (!EX_Admin.Power("order_product_edit", "编辑订单商品"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestString("Uproid");
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Order model = B_Lebi_Order.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
            }
            List<Lebi_Order_Product> pros = B_Lebi_Order_Product.GetList("id in (lbsql{" + ids + "})", "");
            foreach (Lebi_Order_Product pro in pros)
            {
                Lebi_Order_Product modelp = B_Lebi_Order_Product.GetModel(pro.id);
                pro.Price = RequestTool.RequestDecimal("Price" + pro.id, 0);
                pro.Count = RequestTool.RequestInt("Count" + pro.id, 0);
                if (pro.Price != modelp.Price)
                {
                    Log.Add("编辑订单商品单价[" + modelp.Product_Number + "]", "Order", id.ToString(), CurrentAdmin, modelp.Price + "->" + pro.Price);
                }else
                {
                    Lebi_Product product = B_Lebi_Product.GetModel(pro.Product_id);
                    if (product == null)
                    {
                        Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                        return;
                    }
                    Lebi_User CurrentUser = B_Lebi_User.GetModel(model.User_id);
                    Lebi_UserLevel CurrentUserLevel = B_Lebi_UserLevel.GetModel(CurrentUser.UserLevel_id);
                    decimal price = EX_Product.ProductPrice(product, CurrentUserLevel, CurrentUser, pro.Count);
                    decimal Product_Point = 0;
                    if (CurrentUserLevel.MoneyToPoint > 0)
                        Product_Point = price * CurrentUserLevel.MoneyToPoint;//单个产品可得的积分
                    if (pro.Price != price)
                    {
                        pro.Price = price;
                        pro.Point_Product = Product_Point;
                    }
                }
                if (pro.Count != modelp.Count)
                {
                    int changeCount = (pro.Count - pro.Count_Shipped) - (modelp.Count - modelp.Count_Shipped);
                    Lebi_Product product = B_Lebi_Product.GetModel(pro.Product_id);
                    if (ShopCache.GetBaseConfig().ProductStockFreezeTime == "orderadd")
                    {
                        //修改冻结库存
                        //product.Count_Freeze = product.Count_Freeze - modelp.Count + pro.Count;
                        //B_Lebi_Product.Update(product);
                        //if (product.Product_id > 0)
                        //{
                        //    //子商品库存变更，修改父商品库存
                        //    Lebi_Product parentproduct = B_Lebi_Product.GetModel(product.Product_id);
                        //    if (parentproduct != null)
                        //    {
                        //        parentproduct.Count_Freeze = parentproduct.Count_Freeze - modelp.Count + pro.Count;
                        //        B_Lebi_Product.Update(parentproduct);
                        //    }
                        //}
                        //EX_Product.ProductStock_Freeze(product, (pro.Count - pro.Count_Shipped) - (modelp.Count - modelp.Count_Shipped));
                        //更新库存
                        EX_Product.ProductStock_Change(product, (0 - changeCount), 302, model);
                    }
                    //更新销量
                    int num = 1;
                    if (SYS.SalesFlag == "0")
                    {
                        int.TryParse(SYS.SalesNum1, out num);
                        product.Count_Sales_Show = product.Count_Sales_Show + (changeCount * num);
                    }
                    else
                    {
                        int.TryParse(SYS.SalesNum2, out num);
                        Random r = new Random();
                        int c = r.Next(1, num);
                        product.Count_Sales_Show = product.Count_Sales_Show + (changeCount + c);
                    }
                    B_Lebi_Product.Update(product);
                    Log.Add("编辑订单商品数量[" + modelp.Product_Number + "]", "Order", id.ToString(), CurrentAdmin, (modelp.Count - modelp.Count_Shipped) + "->" + (pro.Count - pro.Count_Shipped));
                }
                pro.Money = pro.Price * pro.Count;
                B_Lebi_Order_Product.Update(pro);
            }
            Log.Add("编辑订单商品", "Order", ids.ToString(), CurrentAdmin, "");
            Order.ResetOrder(model);//重新计算订单

            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑订单金额 
        /// </summary>
        public void Order_Money_Edit()
        {
            if (!EX_Admin.Power("order_price_edit", "编辑订单金额"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Order model = B_Lebi_Order.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
            }
            else
            {
                decimal Money_Bill = RequestTool.RequestDecimal("Money_Bill", 0);
                decimal Money_Tax = RequestTool.RequestDecimal("Money_Tax", 0);
                decimal Money_Product = RequestTool.RequestDecimal("Money_Product", 0);
                decimal Money_Transport = RequestTool.RequestDecimal("Money_Transport", 0);
                decimal Money_Give = RequestTool.RequestDecimal("Money_Give", 0);
                decimal Point = RequestTool.RequestDecimal("Point", 0);
                decimal Money_Property= RequestTool.RequestDecimal("Money_Property", 0);
                string action = "";
                string description = "";
                if (model.Money_Product != Money_Product)
                {
                    action = "编辑商品金额";
                    description = model.Money_Product.ToString() + " -> " + Money_Product;
                    Log.Add(action, "Order", model.id.ToString(), CurrentAdmin, description);
                }
                //if (model.Money_Transport != Money_Transport)
                //{
                //    action = "编辑运费";
                //    description = model.Money_Transport.ToString() + " -> " + Money_Transport;
                //    Log.Add(action, "Order", model.id.ToString(), CurrentAdmin, description);
                //}
                if (model.Money_Bill != Money_Bill)
                {
                    action = "编辑发票税金";
                    description = model.Money_Bill.ToString() + " -> " + Money_Bill;
                    Log.Add(action, "Order", model.id.ToString(), CurrentAdmin, description);
                }
                if (model.Money_Tax != Money_Tax)
                {
                    action = "编辑税金";
                    description = model.Money_Tax.ToString() + " -> " + Money_Tax;
                    Log.Add(action, "Order", model.id.ToString(), CurrentAdmin, description);
                }
                if (model.Money_Give != Money_Give)
                {
                    action = "编辑返现";
                    description = model.Money_Give.ToString() + " -> " + Money_Give;
                    Log.Add(action, "Order", model.id.ToString(), CurrentAdmin, description);
                }
                if (model.Money_Property != Money_Property)
                {
                    action = "编辑其它金额";
                    description = model.Money_Property.ToString() + " -> " + Money_Property;
                    Log.Add(action, "Order", model.id.ToString(), CurrentAdmin, description);
                }
                if (model.Point != Point)
                {
                    action = "编辑订单积分";
                    description = model.Point.ToString() + " -> " + Point;
                    Log.Add(action, "Order", model.id.ToString(), CurrentAdmin, description);
                }
                model.Money_Product = Money_Product;
                //model.Money_Transport = Money_Transport;
                model.Money_Bill = Money_Bill;
                model.Money_Tax = Money_Tax;
                model.Money_Give = Money_Give;
                model.Money_Property = Money_Property;
                model.Money_Order = model.Money_Product + model.Money_Transport + model.Money_Bill + model.Money_Property + model.Money_Tax - model.Money_Transport_Cut - model.Money_Cut;
                if (SYS.IntOrderMoney == "1")
                {
                    model.Money_Order = (int)model.Money_Order;
                }
                if (model.Type_id_OrderType == 212)//退货单
                    model.Money_Pay = 0;
                else
                    model.Money_Pay = model.Money_Order - model.Money_UserCut - model.Money_fromorder - model.Money_UseCard311 - model.Money_UseCard312 - model.Money_Paid;
                model.Point = Point;
                B_Lebi_Order.Update(model);
                //更新发票记录
                if (model.Money_Order > 0)
                {
                    Lebi_Bill bill = B_Lebi_Bill.GetModel("Order_id=" + model.id + "");
                    if (bill != null)
                    {
                        if (bill.Money != model.Money_Order)
                        {
                            bill.Money = model.Money_Order;
                            B_Lebi_Bill.Update(bill);
                        }
                    }
                }
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑订单金额 
        /// </summary>
        public void Order_Money_Transport_Edit()
        {
            if (!EX_Admin.Power("order_price_transport_edit", "编辑订单运费金额"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Order model = B_Lebi_Order.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
            }
            else
            {
                decimal Money_Transport = RequestTool.RequestDecimal("Money_Transport", 0);
                string action = "";
                string description = "";
                if (model.Money_Transport != Money_Transport)
                {
                    action = "编辑运费";
                    description = model.Money_Transport.ToString() + " -> " + Money_Transport;
                    Log.Add(action, "Order", model.id.ToString(), CurrentAdmin, description);
                }
                model.Money_Transport = Money_Transport;
                model.Money_Order = model.Money_Product + model.Money_Transport + model.Money_Bill + model.Money_Property + model.Money_Tax - model.Money_Transport_Cut - model.Money_Cut;
                if (SYS.IntOrderMoney == "1")
                {
                    model.Money_Order = (int)model.Money_Order;
                }
                if (model.Type_id_OrderType == 212)//退货单
                    model.Money_Pay = 0;
                else
                    model.Money_Pay = model.Money_Order - model.Money_UserCut - model.Money_fromorder - model.Money_UseCard311 - model.Money_UseCard312 - model.Money_Paid;
                B_Lebi_Order.Update(model);
                //更新发票记录
                if (model.Money_Order > 0)
                {
                    Lebi_Bill bill = B_Lebi_Bill.GetModel("Order_id=" + model.id + "");
                    if (bill != null)
                    {
                        if (bill.Money != model.Money_Order)
                        {
                            bill.Money = model.Money_Order;
                            B_Lebi_Bill.Update(bill);
                        }
                    }
                }
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 发货操作
        /// </summary>
        public void Order_fahuo()
        {
            if (!EX_Admin.Power("order_shipping", "订单发货"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int Transport_id = RequestTool.RequestInt("Transport_id", 0);
            Lebi_Transport tran = B_Lebi_Transport.GetModel(Transport_id);
            Lebi_Order model = B_Lebi_Order.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Lebi_Transport_Order torder = new Lebi_Transport_Order();
            torder.Code = RequestTool.RequestString("Code");
            torder.Money = RequestTool.RequestDecimal("Money");
            torder.Order_id = model.id;

            torder.T_Address = model.T_Address;
            torder.T_Email = model.T_Email;
            torder.T_MobilePhone = model.T_MobilePhone;
            torder.T_Name = model.T_Name;
            torder.T_Phone = model.T_Phone;
            torder.Transport_Code = tran == null ? model.Transport_Code : tran.Code;
            torder.Transport_id = tran == null ? model.Transport_id : tran.id;
            torder.Transport_Name = tran == null ? model.Transport_Name : tran.Name;
            torder.User_id = model.User_id;

            List<TransportProduct> tps = new List<TransportProduct>();
            TransportProduct tp;
            List<Lebi_Order_Product> pros = B_Lebi_Order_Product.GetList("Order_id=" + model.id + "", "");
            int count = 0;
            foreach (Lebi_Order_Product pro in pros)
            {
                count = count + RequestTool.RequestInt("Count" + pro.id, 0);
            }
            if (count == 0)
            {
                Response.Write("{\"msg\":\"" + Tag("发货数量不能为0") + "\"}");
                return;
            }
            bool isfahuo_all = true;
            foreach (Lebi_Order_Product pro in pros)
            {
                count = RequestTool.RequestInt("Count" + pro.id, 0);
                if (count == 0)
                {
                    if (pro.Count - pro.Count_Shipped > 0)
                        isfahuo_all = false;
                    continue;
                }
                tp = new TransportProduct();
                tp.Count = count;
                tp.ImageBig = pro.ImageBig;
                tp.ImageMedium = pro.ImageMedium;
                tp.ImageOriginal = pro.ImageOriginal;
                tp.ImageSmall = pro.ImageSmall;
                tp.Product_Number = pro.Product_Number;
                tp.Product_id = pro.Product_id;
                tp.Product_Name = pro.Product_Name;
                tps.Add(tp);

                pro.Count_Shipped += count;
                if (pro.Count_Shipped < pro.Count)
                    isfahuo_all = false;
                B_Lebi_Order_Product.Update(pro);
                //更新冻结库存
                Lebi_Product product = B_Lebi_Product.GetModel(pro.Product_id);
                EX_Product.ProductStock_Freeze(product, 0 - count);
                ////更新库存
                //EX_Product.ProductStock_Change(product, (0 - count), 302, model.Code, model.id);

            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            torder.Admin_id = CurrentAdmin.id;
            torder.AdminName = CurrentAdmin.UserName;
            torder.Product = jss.Serialize(tps);
            torder.Type_id_TransportOrderStatus = 220;//默认状态：在途
            B_Lebi_Transport_Order.Add(torder);
            torder.id = B_Lebi_Transport_Order.GetMaxId();


            model.IsShipped = 1;
            model.IsShipped_All = isfahuo_all ? 1 : 0;
            model.Time_Shipped = System.DateTime.Now; ;
            B_Lebi_Order.Update(model);
            Log.Add("订单发货", "Order", id.ToString(), CurrentAdmin, torder.Transport_Name + " " + torder.Code);
            Lebi_User user = B_Lebi_User.GetModel(model.User_id);
            //发送邮件
            Email.SendEmail_ordershipping(user, model, torder);
            //发送短信
            SMS.SendSMS_ordershipping(user, model, torder);
            //APP推送
            APP.Push_ordershipping(user, model, torder);
            //触发库存变动事件
            //EX_Product.StockChangeBySale(torder);
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + torder.id + "\"}");
        }
        /// <summary>
        /// 编辑订单配送方式和单号
        /// </summary>
        public void Order_Shipping_Edit()
        {
            if (!EX_Admin.Power("order_edit", "编辑订单"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int tid = RequestTool.RequestInt("tid", 0);
            int Transport_id = RequestTool.RequestInt("Transport_id", 0);
            string Code = RequestTool.RequestSafeString("Code");
            Lebi_Transport tran = B_Lebi_Transport.GetModel(Transport_id);
            Lebi_Transport_Order torder = B_Lebi_Transport_Order.GetModel(tid);
            if (torder == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            torder.Code = Code;
            torder.Transport_Code = tran.Code;
            torder.Transport_id = Transport_id;
            torder.Transport_Name = tran.Name;
            B_Lebi_Transport_Order.Update(torder);
            Log.Add("编辑订单配送方式", "Order", id.ToString(), CurrentAdmin, tran.Name + "[" + Code + "]");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 获取订单备注
        /// </summary>
        public void Order_Memo()
        {
            if (!EX_Admin.Power("order_edit", "编辑订单"))
            {
                AjaxNoPower();
                return;
            }
            string Remark_Admin = "";
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Order model = B_Lebi_Order.GetModel(id);
            if (model != null)
            {
                Remark_Admin = model.Remark_Admin;
                Response.Write(Remark_Admin);
            }
        }
        /// <summary>
        /// 订单留言
        /// </summary>
        public void Comment_Edit()
        {
            if (!EX_Admin.Power("order_comment_edit", "添加订单留言"))
            {
                AjaxNoPower();
                return;
            }
            Lebi_Comment model = new Lebi_Comment();
            model.TableName = "Order";
            model.Keyid = RequestTool.RequestInt("id", 0);
            model.Admin_UserName = CurrentAdmin.UserName;
            model.Admin_id = CurrentAdmin.id;
            model.Content = RequestTool.RequestString("Comment");
            B_Lebi_Comment.Add(model);
            Log.Add("添加订单留言", "Comments", RequestTool.RequestInt("id", 0).ToString(), CurrentAdmin, RequestTool.RequestString("Comment"));
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 订单留言-删除
        /// </summary>
        public void Comment_Del()
        {
            if (!EX_Admin.Power("order_comment_del", "删除订单留言"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestString("commid");
            if (ids != "")
                B_Lebi_Comment.Delete("id in (lbsql{" + ids + "})");
            Log.Add("删除订单留言", "Comments", ids.ToString(), CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除订单
        /// </summary>
        public void Order_Del()
        {
            if (!EX_Admin.Power("order_del", "删除订单"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            List<Lebi_Order> modellist = B_Lebi_Order.GetList("id in (lbsql{" + id + "})", "");
            foreach (Lebi_Order model in modellist)
            {
                if (model.IsInvalid != 1)
                {
                    Response.Write("{\"msg\":\"" + Tag("只可以删除无效订单") + "\"}");
                    return;
                }
                Order.OrderDelete(model);
            }
            Log.Add("删除订单", "Order", id.ToString(), CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑快递单打印模板-FLASH
        /// </summary>
        public void Express_Edit()
        {
            if (!EX_Admin.Power("express_edit", "编辑模板"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            string data = RequestTool.RequestString("data");
            string mobname = RequestTool.RequestString("mobname");
            string mobwidth = RequestTool.RequestString("mobwidth");
            string mobheight = RequestTool.RequestString("mobheight");
            Lebi_Express model = B_Lebi_Express.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
            }
            else
            {
                model.Name = mobname;
                model.Width = mobwidth;
                model.Height = mobheight;
                model.Pos = data;
                B_Lebi_Express.Update(model);
            }
            Log.Add("编辑快递单模板", "Express", id.ToString(), CurrentAdmin, RequestTool.RequestString("mobname"));
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑快递单打印模板
        /// </summary>
        public void Express_Edit_Window()
        {
            bool addflag = false;
            int id = RequestTool.RequestInt("id", 0);
            string Name = RequestTool.RequestString("Name");
            string Width = RequestTool.RequestString("Width");
            string Height = RequestTool.RequestString("Height");
            string Background = RequestTool.RequestString("Background");
            int Status = RequestTool.RequestInt("Status", 0);
            int Sort = RequestTool.RequestInt("Sort", 0);
            int Transport_id = RequestTool.RequestInt("Transport_id", 0);
            Lebi_Express model = B_Lebi_Express.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Express();
            }
            model.Name = Name;
            model.Width = Width;
            model.Height = Height;
            model.Background = Background;
            model.Status = Status;
            model.Sort = Sort;
            model.Transport_id = Transport_id;
            if (addflag)
            {
                if (!EX_Admin.Power("express_add", "添加模板"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Express.Add(model);
                id = B_Lebi_Express.GetMaxId();
                Log.Add("添加模板", "Express", id.ToString(), CurrentAdmin, RequestTool.RequestString("Name"));
            }
            else
            {
                if (!EX_Admin.Power("express_edit", "编辑模板"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Express.Update(model);
                Log.Add("编辑模板", "Express", id.ToString(), CurrentAdmin, RequestTool.RequestString("Name"));
            }
            ImageHelper.LebiImagesUsed(model.Background, "config", id);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 复制快递单打印模板
        /// </summary>
        public void Express_Copy()
        {
            if (!EX_Admin.Power("express_edit", "编辑模板"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Express model = B_Lebi_Express.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            model.Name = "[复制]" + model.Name;
            B_Lebi_Express.Add(model);
            Log.Add("复制模板", "Express", id.ToString(), CurrentAdmin, model.Name);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除快递单打印模板
        /// </summary>
        public void Express_Del()
        {
            if (!EX_Admin.Power("express_del", "删除模板"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            Lebi_Express model = B_Lebi_Express.GetModel("id in (lbsql{" + id + "})");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Log.Add("删除模板", "Express", id.ToString(), CurrentAdmin, model.Name);
            B_Lebi_Express.Delete("id in (lbsql{" + id + "})");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量更新快递单打印模板序号
        /// </summary>
        public void Express_Update()
        {
            if (!EX_Admin.Power("express_edit", "编辑快递单模板"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Uid");
            List<Lebi_Express> models = B_Lebi_Express.GetList("id in (lbsql{" + id + "})", "");
            foreach (Lebi_Express model in models)
            {
                model.Sort = RequestTool.RequestInt("Sort" + model.id + "", 0);
                B_Lebi_Express.Update(model);
            }
            Log.Add("编辑模板", "Express", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑发货人信息
        /// </summary>
        public void Express_Shipper_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            string SiteName = RequestTool.RequestString("SiteName");
            string UserName = RequestTool.RequestString("UserName");
            string Address = RequestTool.RequestString("Address");
            string City = RequestTool.RequestString("City");
            string ZipCode = RequestTool.RequestString("ZipCode");
            string Tel = RequestTool.RequestString("Tel");
            string Mobile = RequestTool.RequestString("Mobile");
            string Remark = RequestTool.RequestString("Remark");
            int Status = RequestTool.RequestInt("Status", 0);
            int Sort = RequestTool.RequestInt("Sort", 0);
            Lebi_Express_Shipper model = B_Lebi_Express_Shipper.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Express_Shipper();
            }
            model.SiteName = SiteName;
            model.UserName = UserName;
            model.Address = Address;
            model.City = City;
            model.ZipCode = ZipCode;
            model.Tel = Tel;
            model.Mobile = Mobile;
            model.City = City;
            model.Status = Status;
            model.Sort = Sort;
            model.Remark = Remark;
            if (addflag)
            {
                if (!EX_Admin.Power("express_shipper_add", "添加发货人"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Express_Shipper.Add(model);
                id = B_Lebi_Express_Shipper.GetMaxId();
                Log.Add("添加发货人", "Express_Shipper", id.ToString(), CurrentAdmin, SiteName);
            }
            else
            {
                if (!EX_Admin.Power("express_shipper_edit", "编辑发货人"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Express_Shipper.Update(model);
                Log.Add("编辑发货人", "Express_Shipper", id.ToString(), CurrentAdmin, SiteName);
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 批量更新发货人信息
        /// </summary>
        public void Express_Shipper_Update()
        {
            if (!EX_Admin.Power("express_shipper_edit", "编辑发货人"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Uid");
            List<Lebi_Express_Shipper> models = B_Lebi_Express_Shipper.GetList("id in (lbsql{" + id + "})", "");
            foreach (Lebi_Express_Shipper model in models)
            {
                model.Sort = RequestTool.RequestInt("Sort" + model.id + "", 0);
                B_Lebi_Express_Shipper.Update(model);
            }
            Log.Add("编辑发货人", "Express_Shipper", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除发货人信息
        /// </summary>
        public void Express_Shipper_Del()
        {
            if (!EX_Admin.Power("express_shipper_del", "删除发货人"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            B_Lebi_Express_Shipper.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除发货人", "Express_Shipper", id.ToString(), CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 添加订单至快递单打印清单
        /// </summary>
        public void Express_Log_Add()
        {
            if (!EX_Admin.Power("express_log_add", "添加打印清单"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"请先选择订单\"}");
                return;
            }
            //=====================================
            //这是我添的
            List<Lebi_Order> orders = B_Lebi_Order.GetList("id in (lbsql{" + id + "})", "");
            int Transport_id = orders.FirstOrDefault().Transport_id;
            string Transport_Name = orders.FirstOrDefault().Transport_Name;
            foreach (Lebi_Order order in orders)
            {
                if (Transport_id != order.Transport_id)
                {
                    Response.Write("{\"msg\":\"请选择同一种配送方式的订单\"}");
                    return;
                }
            }
            //=======================================
            Lebi_Express_Log model = new Lebi_Express_Log();
            model.Number = Shop.Bussiness.Order.CreateOrderCode();
            model.Time_Add = DateTime.Now;
            model.Status = 0;
            model.IdList = id;
            model.Transport_id = Transport_id;
            model.Transport_Name = Transport_Name;
            B_Lebi_Express_Log.Add(model);
            int MaxId = B_Lebi_Express_Log.GetMaxId();
            string ids = id;
            string[] idsArr;
            idsArr = ids.Split(new char[1] { ',' });
            foreach (string i in idsArr)
            {
                Lebi_Express_LogList models = B_Lebi_Express_LogList.GetModel("Order_Id = " + int.Parse(i));
                if (models == null)
                {
                    Lebi_Order modelorder = B_Lebi_Order.GetModel(int.Parse(i));
                    models = new Lebi_Express_LogList();
                    models.Express_Log_Id = MaxId;
                    models.Order_Id = int.Parse(i);
                    models.Order_Code = modelorder.Code;
                    models.Status = 0;
                    B_Lebi_Express_LogList.Add(models);
                }
            }
            Log.Add("添加打印清单", "Express_LogList", id.ToString(), CurrentAdmin, Shop.Bussiness.Order.CreateOrderCode());
            string result = "{\"msg\":\"OK\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除打印清单信息
        /// </summary>
        public void Express_Log_Del()
        {
            if (!EX_Admin.Power("express_log_del", "删除打印清单"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"请选择要删除的打印清单\"}");
                return;
            }
            B_Lebi_Express_Log.Delete("id in (lbsql{" + id + "})");
            B_Lebi_Express_LogList.Delete("Express_Log_Id in (lbsql{" + id + "})");
            Log.Add("删除打印清单", "Express_Log", id.ToString(), CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除打印清单信息
        /// </summary>
        public void Express_LogList_Del()
        {
            if (!EX_Admin.Power("express_log_del", "删除打印清单"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"请选择要删除的打印清单\"}");
                return;
            }
            B_Lebi_Express_LogList.Delete("Id in (lbsql{" + id + "})");
            Log.Add("删除打印清单", "Express_LogList", id.ToString(), CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 获取打印清单下的ID列表
        /// </summary>
        public void Express_LogList_IdList()
        {
            if (!EX_Admin.Power("express_log_print", "打印打印清单"))
            {
                AjaxNoPower();
                return;
            }
            string idlist = "";
            int id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                Response.Write("{\"msg\":\"请先选择订单清单\"}");
                return;
            }
            List<Lebi_Express_LogList> logs = B_Lebi_Express_LogList.GetList("Status = 0 and Express_Log_Id =" + id + "", "");
            foreach (Lebi_Express_LogList log in logs)
            {
                idlist += "," + log.Order_Id;
            }
            if (idlist == "")
            {
                Response.Write("{\"msg\":\"当前打印清单暂无需要打印的订单\"}");
                return;
            }
            idlist = idlist.Substring(1);
            if (idlist != "")
            {
                Response.Write("{\"msg\":\"OK\",\"id\":\"" + idlist + "\"}");
            }
            else
            {
                Response.Write("{\"msg\":\"没有可打印的订单\"}");
            }
        }
        /// <summary>
        /// 批量更新发货清单已打印
        /// </summary>
        public void Express_Log_Update()
        {
            int id = RequestTool.RequestInt("id", 0);
            string Uid = RequestTool.RequestString("Uid");
            if (Uid == "")
            {
                Response.Write("{\"msg\":\"请选择要操作的打印清单\"}");
                return;
            }
            if (id == 0)
            {
                List<Lebi_Express_Log> models = B_Lebi_Express_Log.GetList("id in (lbsql{" + Uid + "})", "");
                foreach (Lebi_Express_Log model in models)
                {
                    model.Status = RequestTool.RequestInt("Status" + model.id + "", 0);
                    B_Lebi_Express_Log.Update(model);
                    List<Lebi_Express_LogList> modellists = B_Lebi_Express_LogList.GetList("Express_Log_Id = " + model.id + "",

"");
                    foreach (Lebi_Express_LogList modellist in modellists)
                    {
                        modellist.Status = RequestTool.RequestInt("Status" + model.id + "", 0);
                        B_Lebi_Express_LogList.Update(modellist);
                        Lebi_Order modelorder = B_Lebi_Order.GetModel(modellist.Order_Id);
                        modelorder = B_Lebi_Order.BindForm(modelorder);
                        modelorder.IsPrintExpress = RequestTool.RequestInt("Status" + model.id + "", 0);
                        B_Lebi_Order.Update(modelorder);
                    }
                }
            }
            else
            {
                List<Lebi_Express_LogList> modellist = B_Lebi_Express_LogList.GetList("id in (lbsql{" + Uid + "})", "");
                foreach (Lebi_Express_LogList model in modellist)
                {
                    model.Status = RequestTool.RequestInt("Status" + model.id + "", 0);
                    B_Lebi_Express_LogList.Update(model);
                    Lebi_Order modelorder = B_Lebi_Order.GetModel(model.Order_Id);
                    modelorder = B_Lebi_Order.BindForm(modelorder);
                    modelorder.IsPrintExpress = RequestTool.RequestInt("Status" + model.id + "", 0);
                    B_Lebi_Order.Update(modelorder);
                }
            }
            Log.Add("更新快递单清单已打印", "Order", id.ToString(), CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 更新快递单打印状态
        /// </summary>
        public void Express_Print()
        {
            string id = RequestTool.RequestString("id");
            int Tid = RequestTool.RequestInt("Tid", 0);
            int Eid = RequestTool.RequestInt("Eid", 0);
            if (id == "")
            {
                Response.Write("{\"msg\":\"没有可供打印的快递单\"}");
                return;
            }
            List<Lebi_Express_LogList> modellist = B_Lebi_Express_LogList.GetList("Order_Id in (lbsql{" + id + "})", "");
            foreach (Lebi_Express_LogList model in modellist)
            {
                model.Status = 1;
                B_Lebi_Express_LogList.Update(model);
                Lebi_Order modelorder = B_Lebi_Order.GetModel(model.Order_Id);
                modelorder = B_Lebi_Order.BindForm(modelorder);
                modelorder.IsPrintExpress = 1;
                B_Lebi_Order.Update(modelorder);
            }
            List<Lebi_Express_LogList> log = B_Lebi_Express_LogList.GetList("Status = 0 and Express_Log_Id = " + Eid + "", "");
            if (log.Count == 0)
            {
                List<Lebi_Express_Log> models = B_Lebi_Express_Log.GetList("id = " + Eid + "", "");
                foreach (Lebi_Express_Log model in models)
                {
                    model.Status = 1;
                    B_Lebi_Express_Log.Update(model);
                }
            }
            Log.Add("更新快递单清单已打印", "Express_Log", id.ToString(), CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 编辑当发票
        /// </summary>
        public void Bill_Edit()
        {
            if (!EX_Admin.Power("bill_edit", "修改发票"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int BillType_id = RequestTool.RequestInt("BillType_id");
            Lebi_BillType bt = B_Lebi_BillType.GetModel(BillType_id);
            if (bt == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }

            Lebi_Bill model = B_Lebi_Bill.GetModel(id);
            if (model == null)
            {
                if (bt.Type_id_BillType == 150)
                {
                    Response.Write("{\"msg\":\"OK\"}");
                }
                model = new Lebi_Bill();
                B_Lebi_Bill.BindForm(model);
                B_Lebi_Bill.Add(model);
                model.id = B_Lebi_Bill.GetMaxId();
                Log.Add("添加发票", "Bill", model.id.ToString(), CurrentAdmin, "");
            }
            else
            {
                B_Lebi_Bill.BindForm(model);
                B_Lebi_Bill.Update(model);
                Log.Add("修改发票", "Bill", model.id.ToString(), CurrentAdmin, "");
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除发票信息
        /// </summary>
        public void Bill_Del()
        {
            if (!EX_Admin.Power("bill_del", "删除发票"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("id");

            Log.Add("删除发票", "Bill", id, CurrentAdmin);
            B_Lebi_Bill.Delete("id in (lbsql{" + id + "})");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 获取自定义页面链接
        /// </summary>
        public void GetAdminSkin()
        {
            int i = 0;
            int id = RequestTool.RequestInt("id", 0);
            string res = "";
            List<Lebi_AdminSkin> models = B_Lebi_AdminSkin.GetList("Type_id_AdminSkinType = 361", "Sort desc");
            foreach (Lebi_AdminSkin model in models)
            {
                res += "<a href=\"" + site.AdminPath + "/custom/" + model.Code + ".aspx?id=" + id + "\" target=\"_blank\">" + model.Name + "</a>";
            }
            Response.Write(res);
        }
        /// <summary>
        /// 删除提现记录
        /// </summary>
        public void Cash_Del()
        {
            if (!EX_Admin.Power("cash_del", "删除提现记录"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("id");

            Log.Add("删除提现记录", "Cash", id, CurrentAdmin);
            B_Lebi_Cash.Delete("id in (lbsql{" + id + "})");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑提现记录
        /// </summary>
        public void Cash_Edit()
        {
            if (!EX_Admin.Power("cash_edit", "提现管理"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Cash model = B_Lebi_Cash.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            int t = RequestTool.RequestInt("t", 0);
            model.Type_id_CashStatus = t;
            model.Admin_id = CurrentAdmin.id;
            model.Admin_UserName = CurrentAdmin.UserName;
            //if (t == 402)
            //{
            //    //打款，
            //    if (model.User_id > 0)
            //    {
            //        Lebi_User user = B_Lebi_User.GetModel(model.User_id);
            //        Money.AddMoney(user, 0 - model.Money, 193, CurrentAdmin, "", "");
            //    }
            //}
            model.Time_update = System.DateTime.Now;
            model.Remark = RequestTool.RequestSafeString("Remark");
            B_Lebi_Cash.Update(model);
            Log.Add("编辑提现记录", "Cash", id.ToString(), CurrentAdmin);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量修改提现记录状态
        /// </summary>
        public void Cash_Update()
        {
            if (!EX_Admin.Power("cash_edit", "提现管理"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("id");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            List<Lebi_Cash> models = B_Lebi_Cash.GetList("id in (lbsql{" + id + "}) and Type_id_CashStatus != 402", "");
            foreach (Lebi_Cash model in models)
            {
                model.Time_update = System.DateTime.Now;
                model.Type_id_CashStatus = 402;
                B_Lebi_Cash.Update(model);
            }
            Log.Add("编辑提现记录", "Cash", id.ToString(), CurrentAdmin);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 收货操作-退货单
        /// </summary>
        public void Order_shouhuo()
        {
            if (!EX_Admin.Power("order_shipping_shouhuo", "退货单订单收货"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Transport_Order torder = B_Lebi_Transport_Order.GetModel(id);
            if (torder == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Lebi_Order order = B_Lebi_Order.GetModel(torder.Order_id);
            List<Lebi_Order_Product> opros = B_Lebi_Order_Product.GetList("Order_id=" + order.id + "", "");
            bool receiveall = true;
            foreach (Lebi_Order_Product opro in opros)
            {
                int Count_Received = RequestTool.RequestInt("Count" + opro.Product_id, 0);
                if (Count_Received < opro.Count_Received)
                {
                    Response.Write("{\"msg\":\"" + Tag("不能小于已收货数量") + "\"}");
                    return;
                }
                if (Count_Received > 0 && Count_Received > opro.Count)
                {
                    Count_Received = opro.Count;
                }
                if (Count_Received != opro.Count_Shipped)
                {
                    receiveall = false;
                }
                //更新库存
                Lebi_Product product = B_Lebi_Product.GetModel(opro.Product_id);
                if (product == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                    return;
                }
                EX_Product.ProductStock_Change(product, (Count_Received - opro.Count_Received), 301, order);
                opro.Count_Received = Count_Received;
                B_Lebi_Order_Product.Update(opro);
            }
            torder.Type_id_TransportOrderStatus = RequestTool.RequestInt("Type_id_TransportOrderStatus");
            torder.Money = RequestTool.RequestDecimal("Money");
            torder.Time_Received = System.DateTime.Now;
            B_Lebi_Transport_Order.Update(torder);
            if (receiveall)
                order.IsReceived_All = 1;
            order.IsReceived = 1;
            B_Lebi_Order.Update(order);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 退货单操作-处理退货资金
        /// </summary>
        public void TOrder_Cash()
        {

            if (!EX_Admin.Power("torder_cash", "处理退货资金"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int type = RequestTool.RequestInt("type", 0);
            //type含义：1退款到提现2退款到用户账户3生成新订单
            Lebi_Order order = B_Lebi_Order.GetModel(id);
            if (order == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            if (order.Type_id_OrderType != 212)
            {
                Response.Write("{\"msg\":\"非退货单不能进行此操作\"}");
                return;
            }
            Lebi_User user = B_Lebi_User.GetModel(order.User_id);
            switch (type)
            {
                //case 1:

                //    Lebi_Cash model = new Lebi_Cash();
                //    model.AccountName = user.CashAccount_Name;
                //    model.AccountCode = user.CashAccount_Code;
                //    model.Bank = user.CashAccount_Bank;
                //    model.User_id = user.id;
                //    model.User_UserName = user.UserName;
                //    model.Money = order.Money_Give;
                //    model.Type_id_CashStatus = 401;
                //    model.Remark = order.Code + "退款";
                //    model.Admin_id = CurrentAdmin.id;
                //    model.Admin_UserName = CurrentAdmin.UserName;
                //    B_Lebi_Cash.Add(model);
                //    order.IsPaid = 1;
                //    order.Time_Paid = System.DateTime.Now;
                //    break;
                case 2:
                    Money.AddMoney(user, order.Money_Give, 195, null, order.Code + "退款", order.Code + "退款");
                    order.IsPaid = 1;
                    order.Time_Paid = System.DateTime.Now;
                    Shop.Bussiness.Order.PaySuccess_FenPeiHuoKuan(order);//处理货款
                    break;
                case 3:
                    Lebi_Order old = B_Lebi_Order.GetModel(order.Order_id);
                    Lebi_Order neworder = new Lebi_Order();
                    neworder.Type_id_OrderType = 211;
                    neworder.Code = Order.CreateOrderCode();
                    neworder.Money_fromorder = order.Money_Give;
                    neworder.Money_Market = 0 - order.Money_Market;
                    neworder.Money_Order = 0 - order.Money_Order;
                    neworder.Money_Product = 0 - order.Money_Product;
                    //rder.Pay = old.Pay;
                    neworder.T_Address = old.T_Address;
                    neworder.T_Area_id = old.T_Area_id;
                    neworder.T_Email = old.T_Email;
                    neworder.T_MobilePhone = order.T_MobilePhone;
                    neworder.T_Name = old.T_Name;
                    neworder.T_Phone = old.T_Phone;
                    neworder.T_Postalcode = old.T_Postalcode;
                    neworder.Time_Add = System.DateTime.Now;
                    neworder.Transport_Code = old.Transport_Code;
                    neworder.Transport_id = old.Transport_id;
                    neworder.Transport_Name = old.Transport_Name;
                    neworder.Transport_Price_id = old.Transport_Price_id;
                    neworder.User_id = old.User_id;
                    neworder.User_UserName = old.User_UserName;
                    neworder.Weight = order.Weight;
                    neworder.Remark_Admin = order.Remark_Admin;
                    neworder.Order_id = order.id;
                    neworder.Site_id = order.Site_id;
                    B_Lebi_Order.Add(neworder);
                    neworder.id = B_Lebi_Order.GetMaxId();
                    List<Lebi_Order_Product> ops = B_Lebi_Order_Product.GetList("Order_id=" + order.id + "", "");
                    foreach (Lebi_Order_Product op in ops)
                    {
                        op.Count = op.Count_Received;
                        op.Count_Received = 0;
                        op.Count_Return = 0;
                        op.Count_Shipped = 0;
                        op.Discount = 0;
                        op.IsCommented = 0;
                        op.Order_Code = neworder.Code;
                        op.Order_id = neworder.id;
                        op.Time_Add = System.DateTime.Now;
                        B_Lebi_Order_Product.Add(op);
                    }
                    order.IsCreateNewOrder = 1;
                    order.Time_CreateNewOrder = System.DateTime.Now;
                    Shop.Bussiness.Order.Order_Completed(order);

                    Log.Add("生成新订单", "Order", order.id.ToString(), CurrentAdmin);
                    Log.Add("生成新订单", "Order", neworder.id.ToString(), CurrentAdmin);
                    break;
            }
            order.IsCompleted = 1;
            order.Time_Completed = System.DateTime.Now;
            B_Lebi_Order.Update(order);
            //扣除退货积分 by lebi.kingdge 2015-01-16
            Lebi_User_Point modelpoint = new Lebi_User_Point();
            modelpoint.Point = order.Point;
            modelpoint.Type_id_PointStatus = 171;
            modelpoint.Admin_UserName = CurrentAdmin.UserName;
            modelpoint.Admin_id = CurrentAdmin.id;
            modelpoint.Remark = order.Code + Tag("退款");
            modelpoint.Time_Update = DateTime.Now;
            modelpoint.User_id = user.id;
            modelpoint.User_RealName = user.RealName;
            modelpoint.User_UserName = user.UserName;
            B_Lebi_User_Point.Add(modelpoint);
            Point.UpdateUserPoint(user);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 修改订单商品/修改规格
        /// </summary>
        public void order_product_edit()
        {
            if (!EX_Admin.Power("order_product_edit", "编辑订单商品"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int productid = RequestTool.RequestInt("productid", 0);
            int orderid = RequestTool.RequestInt("orderid", 0);
            Lebi_Order model = B_Lebi_Order.GetModel(orderid);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            if (model.IsPaid == 1)
            {
                Response.Write("{\"msg\":\"已付款订单不能进行此操作\"}");
                return;
            }
            if (model.IsVerified == 1)
            {
                Response.Write("{\"msg\":\"已确认订单不能进行此操作\"}");
                return;
            }
            Lebi_Product product = B_Lebi_Product.GetModel(productid);
            if (product == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Lebi_Order_Product oproduct = B_Lebi_Order_Product.GetModel(id);
            if (oproduct == null)
            {
                //<-{当添加供应商发货商品时，判断是否可以添加
                if (product.IsSupplierTransport == 1)
                {
                    int productcount = B_Lebi_Order_Product.Counts("Order_id=" + model.id + "");
                    int supplierproductcount = B_Lebi_Order_Product.Counts("Order_id=" + model.id + " and Supplier_id = " + product.Supplier_id + "");
                    if (productcount >0 && productcount > supplierproductcount)
                    {
                        Response.Write("{\"msg\":\"不能添加供应商发货商品\"}");
                        return;
                    }
                    if (model.Supplier_id == 0)
                    {
                        model.Supplier_id = product.Supplier_id;
                        B_Lebi_Order.Update(model);
                    }
                }
                //}->
                int count = B_Lebi_Order_Product.Counts("Order_id=" + model.id + " and Product_id=" + product.id + "");
                if (count > 0)
                {
                    Response.Write("{\"msg\":\"" + Tag("商品已存在") + "\"}");
                    return;
                }
                oproduct = new Lebi_Order_Product();
                Lebi_User CurrentUser = B_Lebi_User.GetModel(model.User_id);
                Lebi_UserLevel CurrentUserLevel = B_Lebi_UserLevel.GetModel(CurrentUser.UserLevel_id);
                //<-{增加对起订量的判断 by lebi.kingdge 2018-04-17
                int levelcount = EX_Product.ProductLevelCount(product, CurrentUserLevel, CurrentUser); ;
                if (levelcount > 1)
                {
                    oproduct.Count = levelcount;
                }else
                {
                    oproduct.Count = 1;
                }
                //}->
                decimal price = EX_Product.ProductPrice(product, CurrentUserLevel, CurrentUser, oproduct.Count);
                decimal Product_Point = 0;
                if (CurrentUserLevel.MoneyToPoint > 0)
                    Product_Point = price * CurrentUserLevel.MoneyToPoint;//单个产品可得的积分
                oproduct.Point_Product = Math.Round(Product_Point, 2);
                oproduct.ImageBig = product.ImageBig;
                oproduct.ImageMedium = product.ImageMedium;
                oproduct.ImageOriginal = product.ImageOriginal;
                oproduct.ImageSmall = product.ImageSmall;
                oproduct.Money = product.Price;
                oproduct.Order_Code = model.Code;
                oproduct.Order_id = model.id;
                oproduct.Price = price;//product.Price;
                oproduct.Price_Cost = product.Price_Cost;
                oproduct.Product_id = product.id;
                oproduct.Product_Name = product.Name;
                oproduct.Product_Number = product.Number;
                oproduct.Type_id_OrderProductType = 251;
                oproduct.User_id = model.User_id;
                oproduct.Weight = product.Weight;
                oproduct.Supplier_id = product.Supplier_id;
                oproduct.IsSupplierTransport = product.IsSupplierTransport;
                oproduct.PackageRate = product.PackageRate;
                oproduct.NetWeight = product.NetWeight;
                oproduct.Units_id = product.Units_id;
                oproduct.Volume = (product.VolumeH * product.VolumeL * product.VolumeW) / 1000000;
                B_Lebi_Order_Product.Add(oproduct);
                if (ShopCache.GetBaseConfig().ProductStockFreezeTime == "orderadd")
                {
                    ////冻结库存
                    //EX_Product.ProductStock_Freeze(product, oproduct.Count);
                    //更新库存
                    EX_Product.ProductStock_Change(product, (0 - oproduct.Count), 302, model);
                }
            }
            else
            {
                oproduct.Price_Cost = product.Price_Cost;
                oproduct.Product_id = product.id;
                oproduct.Product_Name = product.Name;
                oproduct.Product_Number = product.Number;
                oproduct.ImageBig = product.ImageBig;
                oproduct.ImageMedium = product.ImageMedium;
                oproduct.ImageOriginal = product.ImageOriginal;
                oproduct.ImageSmall = product.ImageSmall;
                oproduct.Supplier_id = model.Supplier_id;
                oproduct.IsSupplierTransport = product.IsSupplierTransport;
                oproduct.PackageRate = product.PackageRate;
                oproduct.NetWeight = product.NetWeight;
                oproduct.Units_id = product.Units_id;
                oproduct.Volume = (product.VolumeH * product.VolumeL * product.VolumeW) / 1000000;
                B_Lebi_Order_Product.Update(oproduct);

            }
            Order.ResetOrder(model);//重新计算订单
            Log.Add("修改订单商品", "Order", model.id.ToString(), CurrentAdmin);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除订单调查
        /// </summary>
        public void Order_ProPerty_Del()
        {
            if (!EX_Admin.Power("order_property_list", "订单调查"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("id");

            Log.Add("删除订单调查", "Order_ProPerty", id, CurrentAdmin);
            B_Lebi_Order_ProPerty.Delete("id in (lbsql{" + id + "})");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 充值单管理-已支付
        /// </summary>
        public void MoneyOrder_paid()
        {
            if (!EX_Admin.Power("moneyorder", "充值单管理"))
            {
                AjaxNoPower();
                return;
            }
            int orderid = RequestTool.RequestInt("id", 0);
            decimal money = RequestTool.RequestDecimal("money");
            Lebi_Order order = B_Lebi_Order.GetModel(orderid);
            if (order == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            order.Remark_Admin = RequestTool.RequestString("Remark_Admin");
            order.IsCompleted = 1;
            if (money != 0)
            {
                order.Money_Pay = money;
                order.Money_Order = money;
            }
            if (order.IsPaid == 0)
            {
                Order.PaySuccess(order);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 预定商品到货
        /// 修改订单状态
        /// 通知用户支付尾款
        /// </summary>
        public void ReserveStockOK()
        {
            string ids = RequestTool.RequestString("ids");
            if (ids != "")
            {
                List<Lebi_Order_Product> ops = B_Lebi_Order_Product.GetList("id in (" + ids + ")", "");
                foreach (Lebi_Order_Product op in ops)
                {
                    if (op.IsStockOK != 0)
                        continue;
                    Lebi_Order order = B_Lebi_Order.GetModel(op.Order_id);
                    if (order == null)
                        continue;
                    if (order.IsPaid == 1)
                    {
                        order.IsPaid = 0;
                        order.Money_Pay = (op.Price - op.Price_Reserve) * op.Count;
                    }
                    else
                    {
                        order.Money_Pay += (op.Price - op.Price_Reserve) * op.Count;
                    }
                    order.Money_Product += order.Money_Pay;
                    order.Money_Order += order.Money_Pay;

                    string code = order.Code;
                    string[] arr = order.Code.Split('-');
                    int flag = 1;
                    if (arr.Length > 1)
                    {
                        string t = arr[arr.Length - 1];
                        int.TryParse(t, out flag);
                        flag++;
                        code = "";
                        for (int i = 0; i < arr.Length - 1; i++)
                        {
                            code += arr[i] + "-";
                        }
                        code = code.TrimEnd('-');

                    }
                    code = code + "-" + flag.ToString();
                    order.Code = code;
                    B_Lebi_Order.Update(order);
                    op.IsStockOK = 1;
                    op.Time_StockOK = DateTime.Now;
                    B_Lebi_Order_Product.Update(op);
                    Lebi_User user = B_Lebi_User.GetModel(order.User_id);
                    if (user != null)
                    {
                        //发送提醒邮件
                        Email.SendEmail_reserveok(user, order, op);
                        //发送提醒手机短信.
                        SMS.SendSMS_reserveok(user, order, op);
                        //APP推送
                        APP.Push_reserveok(user, order, op);
                    }
                    Log.Add("预定商品到货", "Order", order.id.ToString(), CurrentAdmin, ids.ToString());
                }
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 添加一个空订单
        /// </summary>
        public void Order_Add()
        {
            if (!EX_Admin.Power("order_add", "后台下单"))
            {
                AjaxNoPower();
                return;
            }

            int userid = RequestTool.RequestInt("userid");
            Lebi_User user = B_Lebi_User.GetModel(userid);
            if (user == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Lebi_Order order = new Lebi_Order();
            order.Code = Order.CreateOrderCode();
            order.User_id = user.id;
            order.User_NickName = user.NickName;
            order.User_UserName = user.UserName;
            order.Type_id_OrderType = 211;
            Lebi_Order lastorder = B_Lebi_Order.GetModel("User_id=" + user.id + " and Type_id_OrderType=211 and T_Address!='' order by id desc");
            if (lastorder != null)
            {
                order.T_Address = lastorder.T_Address;
                order.T_Area_id = lastorder.T_Area_id;
                order.T_Email = lastorder.T_Email;
                order.T_MobilePhone = lastorder.T_MobilePhone;
                order.T_Name = lastorder.T_Name;
                order.T_Phone = lastorder.T_Phone;
                order.T_Postalcode = lastorder.T_Postalcode;
                order.Pay = lastorder.Pay;
                order.Pay_id = lastorder.Pay_id;
                order.Transport_Code = lastorder.Transport_Code;
                order.Transport_id = lastorder.Transport_id;
                order.Transport_Name = lastorder.Transport_Name;
                order.OnlinePay = lastorder.OnlinePay;
                order.OnlinePay_Code = lastorder.OnlinePay_Code;
                order.OnlinePay_id = lastorder.OnlinePay_id;
                order.Language_id = lastorder.Language_id;
                order.Site_id = lastorder.Site_id;
                order.BillType_id = lastorder.BillType_id;
                order.BillType_Name = lastorder.BillType_Name;
                order.BillType_TaxRate = lastorder.BillType_TaxRate;
            }
            order.Site_id = ShopCache.GetMainSite().id;
            B_Lebi_Order.Add(order);
            order.id = B_Lebi_Order.GetMaxId();
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + order.id + "\"}");
        }
        /// <summary>
        /// 编辑订单基本信息
        /// </summary>
        public void Order_baseinfo_Edit()
        {
            if (!EX_Admin.Power("order_edit", "编辑订单"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Order model = B_Lebi_Order.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            else
            {
                model = B_Lebi_Order.BindForm(model);
                Lebi_OnlinePay opay = B_Lebi_OnlinePay.GetModel(model.OnlinePay_id);
                if (opay != null)
                {
                    model.OnlinePay_Code = opay.Code;
                    model.OnlinePay = opay.Name;
                }
                Lebi_Pay pay = B_Lebi_Pay.GetModel(model.Pay_id);
                if (pay != null)
                {
                    model.Pay = pay.Name;
                }
                Lebi_Transport tr = B_Lebi_Transport.GetModel(model.Transport_id);
                if (tr != null)
                {
                    model.Transport_Code = tr.Code;
                    model.Transport_Name = tr.Name;
                }
                Lebi_PickUp pick = B_Lebi_PickUp.GetModel(model.PickUp_id);
                if (pick != null)
                {
                    model.PickUp_Name = pick.Name;
                }
                B_Lebi_Order.Update(model);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
    }
}