using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;
using Shop.Bussiness;
using Shop.Tools;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Shop.Supplier.Ajax
{
    public partial class ajax_order : SupplierAjaxBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Shop.Tools.RequestTool.RequestString("__Action");
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
            int IsVerified = B_Lebi_Order.Counts("Supplier_id = " + CurrentSupplier.id + " and Type_id_OrderType = 211 and IsVerified = 0 and IsInvalid = 0");
            int IsPaid = B_Lebi_Order.Counts("Supplier_id = " + CurrentSupplier.id + " and Type_id_OrderType = 211 and IsPaid = 1 and IsShipped = 0 and IsInvalid = 0");
            int IsShipped = B_Lebi_Order.Counts("Supplier_id = " + CurrentSupplier.id + " and Type_id_OrderType = 211 and IsVerified = 1 and IsShipped = 0 and IsInvalid = 0");
            Shop.Tools.CookieTool.SetCookieString("Supplier_NewEvent_Order_IsVerified", IsVerified.ToString(), 3600);
            Shop.Tools.CookieTool.SetCookieString("Supplier_NewEvent_Order_IsPaid", IsPaid.ToString(), 3600);
            Shop.Tools.CookieTool.SetCookieString("Supplier_NewEvent_Order_IsShipped", IsShipped.ToString(), 3600);
            Response.Write("{\"IsVerified\":\"" + IsVerified + "\",\"IsPaid\":\"" + IsPaid + "\",\"IsShipped\":\"" + IsShipped + "\"}");
        }
        ///// <summary>
        ///// 编辑付款账号
        ///// </summary>
        //public void Cash_Edit()
        //{
        //    if (!Power("supplier_cash_list", "申请提现"))
        //    {
        //        AjaxNoPower();
        //        return;
        //    }
        //    int id = RequestTool.RequestInt("id", 0);
        //    decimal Money = RequestTool.RequestInt("Money", 0);
        //    int BillingDays = -CurrentSupplier.BillingDays;
        //    decimal CanMoney = EX_Supplier.GetMoney(CurrentSupplier.id, System.DateTime.Now.AddDays(BillingDays).ToString("yyyy-MM-dd"), "Type_id_MoneyStatus = 181");
        //    if (Money > CanMoney)
        //    {
        //        Response.Write("{\"msg\":\"" + Tag("申请金额不能大于可提现金额") + "\"}");
        //        return;
        //    }

        //    Lebi_Supplier_Bank bank = B_Lebi_Supplier_Bank.GetModel(RequestTool.RequestInt("Supplier_Bank_id", 0));
        //    if (bank == null)
        //    {
        //        Response.Write("{\"msg\":\"" + Tag("请设置提现帐号") + "\"}");
        //        return;
        //    }
        //    Lebi_Cash model = new Lebi_Cash();
        //    model = B_Lebi_Cash.SafeBindForm(model);
        //    model.Supplier_id = CurrentSupplier.id;
        //    model.Supplier_User_UserName = CurrentSupplier.UserName;
        //    //model.Supplier_User_RealName = CurrentSupplier.RealName;
        //    model.Type_id_CashStatus = 401;
        //    model.Time_add = DateTime.Now;
        //    model.Bank = bank.Name;
        //    model.AccountCode = bank.Code;
        //    model.AccountName = bank.UserName;
        //    B_Lebi_Cash.Add(model);
        //    //CurrentSupplier.Money = CurrentSupplier.Money - Money;
        //    //B_Lebi_Supplier.Update(CurrentSupplier);
        //    id = B_Lebi_Cash.GetMaxId();

        //    Lebi_Supplier_Money money = new Lebi_Supplier_Money();
        //    money.Money = 0 - Money;
        //    money.Supplier_id = CurrentSupplier.id;
        //    money.Supplier_User_UserName = CurrentSupplier.UserName;
        //    money.Type_id_MoneyStatus = 181;//冻结资金
        //    money.Type_id_SupplierMoneyType = 343;//提现
        //    money.Supplier_User_RealName = CurrentSupplier.RealName;
        //    B_Lebi_Supplier_Money.Add(money);
        //    EX_Supplier.UpdateUserMoney(CurrentSupplier);
        //    Log.Add("申请提现", "Cash", id.ToString(), CurrentSupplier, model.Money.ToString());
        //    string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
        //    Response.Write(result);
        //}
        /// <summary>
        /// 删除付款账号
        /// </summary>
        public void Bank_Del()
        {
            if (!Power("supplier_bank_list", "付款账号"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("Fid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Supplier_Bank.Delete("id in (lbsql{" + id + "}) and Supplier_id = " + CurrentSupplier.id + "");
            Log.Add("删除付款方式", "Bank", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑商家
        /// </summary>
        public void Profile_Edit()
        {
            if (!Power("supplier_profile", "编辑资料"))
            {
                AjaxNoPower();
                return;
            }
            string UserName = RequestTool.RequestSafeString("UserName");
            Lebi_Supplier model = B_Lebi_Supplier.GetModel(CurrentSupplier.id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"账号不存在\"}");
                return;
            }
            B_Lebi_Supplier.SafeBindForm(model);
            B_Lebi_Supplier.Update(model);
            Log.Add("编辑资料", "User", CurrentSupplier.User_id.ToString(), CurrentSupplier, model.UserName);
            string result = "{\"msg\":\"OK\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 编辑密码
        /// </summary>
        public void Password_Edit()
        {
            if (!Power("supplier_password", "编辑密码"))
            {
                AjaxNoPower();
                return;
            }
            string OldPWD = RequestTool.RequestSafeString("OldPWD");
            string PWD1 = RequestTool.RequestSafeString("PWD1");
            string PWD2 = RequestTool.RequestSafeString("PWD2");
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            string PWD = BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(PWD1))).Replace("-", "").ToLower();
            string md5OldPWD = BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(OldPWD))).Replace("-", "").ToLower();
            if (PWD1 != PWD2)
            {
                Response.Write("{\"msg\":\"两次输入的密码不一致\"}");
                return;
            }
            Lebi_User model = B_Lebi_User.GetModel("id = " + CurrentSupplier.User_id + "");
            if (model.Password != md5OldPWD)
            {
                Response.Write("{\"msg\":\"原始密码不正确\"}");
                return;
            }
            model.Password = PWD;
            B_Lebi_User.Update(model);
            Log.Add("编辑密码", "User", CurrentSupplier.User_id.ToString(), CurrentSupplier, "");
            string result = "{\"msg\":\"OK\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 编辑发票信息
        /// </summary>
        public void BillType_Edit()
        {
            if (!Power("supplier_billtype_list", "发票管理"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int Type_id_BillType = RequestTool.RequestInt("Type_id_BillType", 0);
            bool addflag = false;
            string where = "";
            if (id == 0)
            {
                where = "Type_id_BillType = " + Type_id_BillType + " and Supplier_id = " + CurrentSupplier.id + "";
            }
            else
            {
                where = "id = " + id + " and Supplier_id = " + CurrentSupplier.id + "";
            }
            Lebi_Supplier_BillType model = B_Lebi_Supplier_BillType.GetModel(where);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Supplier_BillType();
            }
            model = B_Lebi_Supplier_BillType.SafeBindForm(model);
            if (addflag)
            {
                model.Supplier_id = CurrentSupplier.id;
                B_Lebi_Supplier_BillType.Add(model);
                id = B_Lebi_Supplier_BillType.GetMaxId();
                Log.Add("添加发票类型", "BillType", id.ToString(), CurrentSupplier, Shop.Bussiness.EX_Type.TypeName(model.Type_id_BillType));
            }
            else
            {
                B_Lebi_Supplier_BillType.Update(model);
                Log.Add("编辑发票类型", "BillType", id.ToString(), CurrentSupplier, Shop.Bussiness.EX_Type.TypeName(model.Type_id_BillType));
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除发票信息
        /// </summary>
        public void BillType_Del()
        {
            if (!Power("supplier_billtype_list", "发票管理"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("Fid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Supplier_BillType.Delete("id in (lbsql{" + id + "}) and Supplier_id = " + CurrentSupplier.id + "");
            Log.Add("删除发票类型", "BillType", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 修改订单状态
        /// </summary>
        public void Order_type()
        {
            int id = RequestTool.RequestInt("id", 0);
            int t = RequestTool.RequestInt("t", 2);
            string str = RequestTool.RequestSafeString("model");
            string action = "";
            Lebi_Order model = B_Lebi_Order.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            if (model.Supplier_id != CurrentSupplier.id)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }

            if (str == "IsRefund")
            {

                action = "取消订单";
                if (t == 1)
                {
                    action += " 同意";
                    //if (model.Supplier_id > 0)
                    //{
                    //    Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(model.Supplier_id);
                    //    if (supplier.IsCash == 1)
                    //    {
                    //        Response.Write("{\"msg:" + Tag("独立收款商家不能操作取消订单") + "\"}");
                    //        return;
                    //    }
                    //}
                    Shop.Bussiness.Order.Order_Cancal(model, 1);//取消订单
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
                        //mes.Admin_id = CurrentAdmin.id;
                        //mes.Admin_UserName = Language.Tag("管理员", user.Language);
                        mes.Supplier_id = CurrentSupplier.id;
                        mes.Supplier_SubName = CurrentSupplier.SubName;
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
                        mes.Supplier_id = CurrentSupplier.id;
                        mes.Supplier_SubName = CurrentSupplier.SubName;
                        B_Lebi_Comment.Add(mes);
                    }
                }

            }
            if (str == "IsInvalid")
            {

                action = "订单审核";
                if (t == 1)
                {
                    action += " 无效";
                    Shop.Bussiness.Order.Order_Cancal(model);//取消订单
                }
                else
                {
                    action += " 有效";
                    Shop.Bussiness.Order.Order_Confirm(model);//修改为有效订单
                }
                Log.Add(action, "Order", id.ToString(), CurrentSupplier, model.Code);

            }
            if (str == "IsVerified")
            {

                action = "订单审核";
                if (t == 0)
                {
                    action += " 未确认";
                    Shop.Bussiness.Order.Order_Check_Cancal(model);
                }
                else
                {
                    action += " 已确认";
                    Shop.Bussiness.Order.Order_Confirm(model);
                }
                Log.Add(action, "Order", id.ToString(), CurrentSupplier, "");
            }
            if (str == "IsPaid")
            {


                action = "订单支付";
                if (t == 0)
                {
                    action += " 未支付";
                    Shop.Bussiness.Order.Order_Pay_Cancal(model);
                }
                else
                {
                    action += " 已支付";
                    Shop.Bussiness.Order.PaySuccess(model);
                }
                Log.Add(action, "Order", id.ToString(), CurrentSupplier, "");
            }
            if (str == "IsCompleted")
            {
                if (model.IsVerified != 1)
                {
                    Response.Write("{\"msg\":\"未确认订单不能进行此操作\"}");
                    return;
                }

                if (!Power("supplier_order_complete", "订单完成"))
                {
                    AjaxNoPower();
                    return;
                }
                action = "订单完成";
                if (t == 0)
                {
                    action += " 未完成";
                    Shop.Bussiness.Order.Order_Completed_Cancal(model);//完成订单-取消
                }
                else
                {
                    if (model.IsPaid != 1)
                    {
                        Response.Write("{\"msg\":\"未付款订单不能进行此操作\"}");
                        return;
                    }
                    action += " 已完成";
                    Shop.Bussiness.Order.Order_Completed(model);//完成订单
                }
                Log.Add(action, "Order", id.ToString(), CurrentSupplier, "");
            }

            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 发货操作
        /// </summary>
        public void Order_fahuo()
        {
            if (!Power("supplier_order_shipping", "订单发货"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int Transport_id = RequestTool.RequestInt("Transport_id", 0);
            Lebi_Transport tran = B_Lebi_Transport.GetModel(Transport_id);
            Lebi_Order model = B_Lebi_Order.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id + "");
            if (model == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            Lebi_Transport_Order torder = new Lebi_Transport_Order();
            torder.Code = RequestTool.RequestSafeString("Code");
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
                if (pro.Supplier_id == CurrentSupplier.id)
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
                }
                if (pro.Count_Shipped < pro.Count)
                    isfahuo_all = false;
                B_Lebi_Order_Product.Update(pro);
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            torder.Supplier_id = CurrentSupplier.id;
            torder.Supplier_SubName = CurrentSupplier.UserName;
            torder.Product = jss.Serialize(tps);
            torder.Type_id_TransportOrderStatus = 220;//默认状态：在途
            B_Lebi_Transport_Order.Add(torder);
            model.IsShipped = 1;
            model.IsShipped_All = isfahuo_all ? 1 : 0;
            model.Time_Shipped = System.DateTime.Now; ;
            B_Lebi_Order.Update(model);

            Log.Add("订单发货", "Order", id.ToString(), CurrentSupplier, torder.Transport_Name + " " + torder.Code);
            //发送邮件
            if (ShopCache.GetBaseConfig().MailSign.ToLower().Contains("dingdanfahuo"))
            {
                Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                Email.SendEmail_ordershipping(user, model, torder);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 订单留言
        /// </summary>
        public void Comment_Edit()
        {
            if (!Power("supplier_order_comment_edit", "添加订单留言"))
            {
                AjaxNoPower();
                return;
            }
            Lebi_Comment model = new Lebi_Comment();
            model.TableName = "Order";
            model.Keyid = RequestTool.RequestInt("id", 0);
            model.Supplier_SubName = CurrentSupplier.SubName;
            model.Supplier_id = CurrentSupplier.id;
            model.Content = RequestTool.RequestSafeString("Comment");
            B_Lebi_Comment.Add(model);
            Log.Add("添加订单留言", "Comments", RequestTool.RequestInt("id", 0).ToString(), CurrentSupplier, RequestTool.RequestString("Comment"));
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 订单留言-删除
        /// </summary>
        public void Comment_Del()
        {
            if (!Power("supplier_order_comment_del", "删除订单留言"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("commid");
            if (ids != "")
                B_Lebi_Comment.Delete("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + ids + "})");
            Log.Add("删除订单留言", "Comments", ids.ToString(), CurrentSupplier, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑发货人信息
        /// </summary>
        public void Express_Shipper_Edit()
        {
            if (!Power("supplier_express_shipper_list", "发货人管理"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            string SiteName = RequestTool.RequestSafeString("SiteName");
            string UserName = RequestTool.RequestSafeString("UserName");
            string Address = RequestTool.RequestSafeString("Address");
            string City = RequestTool.RequestSafeString("City");
            string ZipCode = RequestTool.RequestSafeString("ZipCode");
            string Tel = RequestTool.RequestSafeString("Tel");
            string Mobile = RequestTool.RequestSafeString("Mobile");
            string Remark = RequestTool.RequestSafeString("Remark");
            int Status = RequestTool.RequestInt("Status", 0);
            int Sort = RequestTool.RequestInt("Sort", 0);
            Lebi_Express_Shipper model = B_Lebi_Express_Shipper.GetModel("Supplier_id=" + CurrentSupplier.id + " and id =" + id);
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
            model.Supplier_id = CurrentSupplier.id;
            if (addflag)
            {
                B_Lebi_Express_Shipper.Add(model);
                id = B_Lebi_Express_Shipper.GetMaxId();
                Log.Add("添加发货人", "Express_Shipper", id.ToString(), CurrentSupplier, SiteName);
            }
            else
            {
                B_Lebi_Express_Shipper.Update(model);
                Log.Add("编辑发货人", "Express_Shipper", id.ToString(), CurrentSupplier, SiteName);
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 批量更新发货人信息
        /// </summary>
        public void Express_Shipper_Update()
        {
            if (!Power("supplier_express_shipper_list", "发货人管理"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("Uid");
            List<Lebi_Express_Shipper> models = B_Lebi_Express_Shipper.GetList("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + id + "})", "");
            foreach (Lebi_Express_Shipper model in models)
            {
                model.Sort = RequestTool.RequestInt("Sort" + model.id + "", 0);
                B_Lebi_Express_Shipper.Update(model);
            }
            Log.Add("编辑发货人", "Express_Shipper", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除发货人信息
        /// </summary>
        public void Express_Shipper_Del()
        {
            if (!Power("supplier_express_shipper_list", "发货人管理"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("Fid");
            if (id == "")
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            B_Lebi_Express_Shipper.Delete("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + id + "})");
            Log.Add("删除发货人", "Express_Shipper", id.ToString(), CurrentSupplier, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 添加订单至快递单打印清单
        /// </summary>
        public void Express_Log_Add()
        {
            if (!Power("supplier_express_print", "打印清单"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"请先选择订单\"}");
                return;
            }
            //=====================================
            //这是我添的
            List<Lebi_Order> orders = B_Lebi_Order.GetList("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + id + "})", "");
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
            model.Supplier_id = CurrentSupplier.id;
            B_Lebi_Express_Log.Add(model);
            int MaxId = B_Lebi_Express_Log.GetMaxId();
            string ids = id;
            string[] idsArr;
            idsArr = ids.Split(new char[1] { ',' });
            foreach (string i in idsArr)
            {
                Lebi_Express_LogList models = B_Lebi_Express_LogList.GetModel("Supplier_id = " + CurrentSupplier.id + " and Order_Id = " + int.Parse(i));
                if (models == null)
                {
                    Lebi_Order modelorder = B_Lebi_Order.GetModel(int.Parse(i));
                    models = new Lebi_Express_LogList();
                    models.Express_Log_Id = MaxId;
                    models.Order_Id = int.Parse(i);
                    models.Order_Code = modelorder.Code;
                    models.Status = 0;
                    models.Supplier_id = CurrentSupplier.id;
                    B_Lebi_Express_LogList.Add(models);
                }
            }
            Log.Add("添加打印清单", "Express_LogList", id.ToString(), CurrentSupplier, Shop.Bussiness.Order.CreateOrderCode());
            string result = "{\"msg\":\"OK\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除打印清单信息
        /// </summary>
        public void Express_Log_Del()
        {
            if (!Power("supplier_express_print", "打印清单"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"请选择要删除的打印清单\"}");
                return;
            }
            B_Lebi_Express_Log.Delete("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + id + "})");
            B_Lebi_Express_LogList.Delete("Supplier_id = " + CurrentSupplier.id + " and Express_Log_Id in (lbsql{" + id + "})");
            Log.Add("删除打印清单", "Express_Log", id.ToString(), CurrentSupplier, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除打印清单信息
        /// </summary>
        public void Express_LogList_Del()
        {
            if (!Power("supplier_express_print", "打印清单"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"请选择要删除的打印清单\"}");
                return;
            }
            B_Lebi_Express_LogList.Delete("Supplier_id = " + CurrentSupplier.id + " and Id in (lbsql{" + id + "})");
            Log.Add("删除打印清单", "Express_LogList", id.ToString(), CurrentSupplier, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 获取打印清单下的ID列表
        /// </summary>
        public void Express_LogList_IdList()
        {
            if (!Power("supplier_express_print", "打印清单"))
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
            List<Lebi_Express_LogList> logs = B_Lebi_Express_LogList.GetList("Supplier_id = " + CurrentSupplier.id + " and Status = 0 and Express_Log_Id =" + id + "", "");
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
            string Uid = RequestTool.RequestSafeString("Uid");
            if (Uid == "")
            {
                Response.Write("{\"msg\":\"请选择要操作的打印清单\"}");
                return;
            }
            if (id == 0)
            {
                List<Lebi_Express_Log> models = B_Lebi_Express_Log.GetList("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + Uid + "})", "");
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
                        modelorder = B_Lebi_Order.SafeBindForm(modelorder);
                        modelorder.IsPrintExpress = RequestTool.RequestInt("Status" + model.id + "", 0);
                        B_Lebi_Order.Update(modelorder);
                    }
                }
            }
            else
            {
                List<Lebi_Express_LogList> modellist = B_Lebi_Express_LogList.GetList("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + Uid + "})", "");
                foreach (Lebi_Express_LogList model in modellist)
                {
                    model.Status = RequestTool.RequestInt("Status" + model.id + "", 0);
                    B_Lebi_Express_LogList.Update(model);
                    Lebi_Order modelorder = B_Lebi_Order.GetModel(model.Order_Id);
                    modelorder = B_Lebi_Order.SafeBindForm(modelorder);
                    modelorder.IsPrintExpress = RequestTool.RequestInt("Status" + model.id + "", 0);
                    B_Lebi_Order.Update(modelorder);
                }
            }
            Log.Add("更新快递单清单已打印", "Order", id.ToString(), CurrentSupplier, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 更新快递单打印状态
        /// </summary>
        public void Express_Print()
        {
            string id = RequestTool.RequestSafeString("id");
            int Tid = RequestTool.RequestInt("Tid", 0);
            int Eid = RequestTool.RequestInt("Eid", 0);
            if (id == "")
            {
                Response.Write("{\"msg\":\"没有可供打印的快递单\"}");
                return;
            }
            List<Lebi_Express_LogList> modellist = B_Lebi_Express_LogList.GetList("Supplier_id = " + CurrentSupplier.id + " and Order_Id in (lbsql{" + id + "})", "");
            foreach (Lebi_Express_LogList model in modellist)
            {
                model.Status = 1;
                B_Lebi_Express_LogList.Update(model);
                Lebi_Order modelorder = B_Lebi_Order.GetModel(model.Order_Id);
                modelorder = B_Lebi_Order.SafeBindForm(modelorder);
                modelorder.IsPrintExpress = 1;
                B_Lebi_Order.Update(modelorder);
            }
            List<Lebi_Express_LogList> log = B_Lebi_Express_LogList.GetList("Supplier_id = " + CurrentSupplier.id + " and Status = 0 and Express_Log_Id = " + Eid + "", "");
            if (log.Count == 0)
            {
                List<Lebi_Express_Log> models = B_Lebi_Express_Log.GetList("id = " + Eid + "", "");
                foreach (Lebi_Express_Log model in models)
                {
                    model.Status = 1;
                    B_Lebi_Express_Log.Update(model);
                }
            }
            Log.Add("更新快递单清单已打印", "Express_Log", id.ToString(), CurrentSupplier, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 佣金提现，-转现金账户
        /// </summary>
        public void TackSupplierMoney()
        {
            string money_fu_ = B_Lebi_Supplier_Money.GetValue("sum(Money)", "Supplier_id=" + CurrentSupplier.id + " and Type_id_MoneyStatus=181 and Type_id_SupplierMoneyType in (342,343)");
            string money_ = B_Lebi_Supplier_Money.GetValue("sum(Money)", "Supplier_id=" + CurrentSupplier.id + " and Type_id_MoneyStatus=181 and Type_id_SupplierMoneyType  in (341,344) and datediff(d,Time_add,'" + System.DateTime.Now + "')>=" + CurrentSupplier.BillingDays + "");
            decimal money_fu = 0;
            decimal money = 0;
            decimal.TryParse(money_, out money);
            decimal.TryParse(money_fu_, out money_fu);
            money = money + money_fu;
            money = money - CurrentSupplier.Money_Margin;
            if (money > 0)
            {
                Money.AddMoney(CurrentUser, money, 191, null, "", "");


                Lebi_Supplier_Money model = new Lebi_Supplier_Money();
                model.Money = 0 - money;
                model.Supplier_id = CurrentSupplier.id;
                model.Supplier_SubName = CurrentSupplier.SubName;
                model.Type_id_MoneyStatus = 181;//有效资金
                model.Type_id_SupplierMoneyType = 343;//提现
                model.User_UserName = CurrentSupplier.UserName;
                B_Lebi_Supplier_Money.Add(model);
                EX_Supplier.UpdateUserMoney(CurrentSupplier);
                Response.Write("{\"msg\":\"OK\"}");
            }
            else
            {
                Response.Write("{\"msg\":\"" + Tag("无可用提现金额") + "\"}");
            }
        }
        /// <summary>
        /// 编辑订单金额 
        /// </summary>
        public void Order_Money_Edit()
        {
            if (!Power("supplier_order_price_edit", "编辑订单金额"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Order model = B_Lebi_Order.GetModel("Supplier_id=" + CurrentSupplier.id + " and id =" + id);
            if (model == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
            }
            else
            {
                decimal Money_Bill = RequestTool.RequestDecimal("Money_Bill", 0);
                decimal Money_Tax = RequestTool.RequestDecimal("Money_Tax", 0);
                decimal Money_Product = RequestTool.RequestDecimal("Money_Product", 0);
                decimal Money_Transport = RequestTool.RequestDecimal("Money_Transport", 0);

                string action = "";
                string description = "";
                if (model.Money_Product != Money_Product)
                {
                    action = "编辑商品金额";
                    description = model.Money_Product.ToString() + " -> " + Money_Product;
                    Log.Add(action, "Order", model.id.ToString(), CurrentSupplier, description);
                }
                if (model.Money_Transport != Money_Transport)
                {
                    action = "编辑运费";
                    description = model.Money_Transport.ToString() + " -> " + Money_Transport;
                    Log.Add(action, "Order", model.id.ToString(), CurrentSupplier, description);
                }
                if (model.Money_Bill != Money_Bill)
                {
                    action = "编辑发票税金";
                    description = model.Money_Bill.ToString() + " -> " + Money_Bill;
                    Log.Add(action, "Order", model.id.ToString(), CurrentSupplier, description);
                }
                if (model.Money_Tax != Money_Tax)
                {
                    action = "编辑税金";
                    description = model.Money_Tax.ToString() + " -> " + Money_Tax;
                    Log.Add(action, "Order", model.id.ToString(), CurrentSupplier, description);
                }
                model.Money_Product = Money_Product;
                model.Money_Transport = Money_Transport;
                model.Money_Bill = Money_Bill;
                model.Money_Tax = Money_Tax;
                //model.Money_Give = Money_Give;
                model.Money_Order = model.Money_Product + model.Money_Transport + model.Money_Bill + model.Money_Property + model.Money_Tax - model.Money_Transport_Cut - model.Money_Cut;
                if (model.Type_id_OrderType == 212)//退货单
                    model.Money_Pay = 0;
                else
                    model.Money_Pay = model.Money_Order - model.Money_UserCut;
                //model.Point = Point;
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
        /// 编辑订单-收货人
        /// </summary>
        public void Order_shouhuo_Edit()
        {
            if (!Power("supplier_order_edit", "编辑订单"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Order model = B_Lebi_Order.GetModel("Supplier_id=" + CurrentSupplier.id + " and id =" + id);
            if (model == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
            }
            else
            {
                model = B_Lebi_Order.SafeBindForm(model);
                B_Lebi_Order.Update(model);
            }
            Log.Add("编辑收货人", "Order", id.ToString(), CurrentSupplier, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除订单商品
        /// </summary>
        public void OrderPro_Del()
        {
            if (!Power("supplier_order_product_del", "删除订单商品"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("proid");
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Order model = B_Lebi_Order.GetModel("Supplier_id=" + CurrentSupplier.id + " and id =" + id);
            if (model == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
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
                        //修改冻结库存
                        Lebi_Product pro = B_Lebi_Product.GetModel(p.Product_id);
                        //pro.Count_Freeze = pro.Count_Freeze - p.Count;
                        //B_Lebi_Product.Update(pro);
                        EX_Product.ProductStock_Freeze(pro, 0 - (p.Count - p.Count_Shipped));
                    }
                    B_Lebi_Order_Product.Delete(p.id);
                }
                Shop.Bussiness.Order.ResetOrder(model);//重新计算订单
            }
            Log.Add("删除订单商品", "Order", model.id.ToString(), CurrentSupplier, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 修改订单商品
        /// </summary>
        public void OrderPro_Edit()
        {
            if (!Power("supplier_order_product_edit", "编辑订单商品"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("Uproid");
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Order model = B_Lebi_Order.GetModel("Supplier_id=" + CurrentSupplier.id + " and id =" + id);
            if (model == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
            }
            List<Lebi_Order_Product> pros = B_Lebi_Order_Product.GetList("id in (lbsql{" + ids + "})", "");
            foreach (Lebi_Order_Product pro in pros)
            {
                Lebi_Order_Product modelp = B_Lebi_Order_Product.GetModel(pro.id);
                pro.Price = RequestTool.RequestDecimal("Price" + pro.id, 0);
                pro.Count = RequestTool.RequestInt("Count" + pro.id, 0);
                if (pro.Price != modelp.Price)
                {
                    Log.Add("编辑订单商品单价[" + modelp.Product_Number + "]", "Order", id.ToString(), CurrentSupplier, modelp.Price + "->" + pro.Price);
                }
                if (pro.Count != modelp.Count)
                {
                    //修改冻结库存
                    //Lebi_Product product = B_Lebi_Product.GetModel(pro.Product_id);
                    //product.Count_Freeze = product.Count_Freeze - modelp.Count + pro.Count;
                    //B_Lebi_Product.Update(product);
                    if (ShopCache.GetBaseConfig().ProductStockFreezeTime == "orderadd")
                    {
                        //修改冻结库存
                        Lebi_Product product = B_Lebi_Product.GetModel(pro.Product_id);
                        EX_Product.ProductStock_Freeze(product, (pro.Count - pro.Count_Shipped) - (modelp.Count - modelp.Count_Shipped));
                    }
                    Log.Add("编辑订单商品数量[" + modelp.Product_Number + "]", "Order", id.ToString(), CurrentSupplier, (modelp.Count - modelp.Count_Shipped) + "->" + (pro.Count - pro.Count_Shipped));
                }
                pro.Money = pro.Price * pro.Count;
                B_Lebi_Order_Product.Update(pro);
            }
            Log.Add("编辑订单商品", "Order", ids.ToString(), CurrentSupplier, "");
            Shop.Bussiness.Order.ResetOrder(model);//重新计算订单

            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 修改订单商品/修改规格
        /// </summary>
        public void order_product_edit()
        {
            if (!Power("supplier_order_product_edit", "编辑订单商品"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int productid = RequestTool.RequestInt("productid", 0);
            int orderid = RequestTool.RequestInt("orderid", 0);
            Lebi_Order model = B_Lebi_Order.GetModel("Supplier_id=" + CurrentSupplier.id + " and id =" + orderid);
            if (model == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
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
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            Lebi_Order_Product oproduct = B_Lebi_Order_Product.GetModel(id);
            if (oproduct == null)
            {
                oproduct = new Lebi_Order_Product();
                oproduct.Count = 1;
                oproduct.ImageBig = product.ImageBig;
                oproduct.ImageMedium = product.ImageMedium;
                oproduct.ImageOriginal = product.ImageOriginal;
                oproduct.ImageSmall = product.ImageSmall;
                oproduct.Money = product.Price;
                oproduct.Price_Cost = product.Price_Cost;
                oproduct.Order_Code = model.Code;
                oproduct.Order_id = model.id;
                oproduct.Price = product.Price;
                oproduct.Product_id = product.id;
                oproduct.Product_Name = product.Name;
                oproduct.Product_Number = product.Number;
                oproduct.Type_id_OrderProductType = 251;
                oproduct.User_id = model.User_id;
                oproduct.Weight = product.Weight;
                oproduct.Supplier_id = model.Supplier_id;
                oproduct.Price_Cost = product.Price_Cost;
                oproduct.IsSupplierTransport = product.IsSupplierTransport;
                B_Lebi_Order_Product.Add(oproduct);
                if (ShopCache.GetBaseConfig().ProductStockFreezeTime == "orderadd")
                {
                    //冻结库存
                    EX_Product.ProductStock_Freeze(product, oproduct.Count);
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
                oproduct.Price_Cost = product.Price_Cost;
                oproduct.Supplier_id = model.Supplier_id;
                B_Lebi_Order_Product.Update(oproduct);
            }
            Shop.Bussiness.Order.ResetOrder(model);//重新计算订单
            Log.Add("修改订单商品", "Order", model.id.ToString(), CurrentSupplier);
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
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }

            Lebi_Order order = B_Lebi_Order.GetModel(torder.Order_id);
            if (order.Supplier_id != CurrentSupplier.id)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            List<Lebi_Order_Product> opros = B_Lebi_Order_Product.GetList("Order_id=" + order.id + "", "");
            bool receiveall = true;
            foreach (Lebi_Order_Product opro in opros)
            {
                opro.Count_Received = RequestTool.RequestInt("Count" + opro.Product_id, 0);
                if (opro.Count_Received != opro.Count_Shipped)
                {
                    receiveall = false;
                }
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

            if (!Power("supplier_torder_cash", "处理退货资金"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int type = RequestTool.RequestInt("type", 0);
            //type含义：1退款到提现2退款到用户账户3生成新订单
            Lebi_Order order = B_Lebi_Order.GetModel("Supplier_id=" + CurrentSupplier.id + " and id =" + id);
            if (order == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
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
                    neworder.Code = Shop.Bussiness.Order.CreateOrderCode();
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
                    neworder.Supplier_id = order.Supplier_id;
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

                    Log.Add("生成新订单", "Order", order.id.ToString(), CurrentSupplier);
                    Log.Add("生成新订单", "Order", neworder.id.ToString(), CurrentSupplier);
                    break;
            }
            Shop.Bussiness.Order.Order_Completed(order);
            B_Lebi_Order.Update(order);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 获取订单备注
        /// </summary>
        public void Order_Memo()
        {
            if (!Power("supplier_order_edit", "编辑订单"))
            {
                AjaxNoPower();
                return;
            }
            string Remark_Admin = "";
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Order model = B_Lebi_Order.GetModel("Supplier_id=" + CurrentSupplier.id + " and id =" + id);
            if (model != null)
            {
                Remark_Admin = model.Remark_Admin;
                Response.Write(Remark_Admin);
            }
        }
        /// <summary>
        /// 编辑订单
        /// </summary>
        public void Order_Edit()
        {
            if (!Power("supplier_order_edit", "编辑订单"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int mark = RequestTool.RequestInt("mark", 0);
            int Supplier_Delivery_id = RequestTool.RequestInt("Supplier_Delivery_id", 0);
            string Remark_Admin = RequestTool.RequestSafeString("Remark_Admin");
            Lebi_Order model = B_Lebi_Order.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
            }
            else
            {
                model.Mark = mark;
                model.Remark_Admin = Remark_Admin;
                model.Supplier_Delivery_id = Supplier_Delivery_id;
                B_Lebi_Order.Update(model);
            }
            Log.Add("编辑订单", "Order", id.ToString(), CurrentSupplier, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
    }
}