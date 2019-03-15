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


namespace Shop.Ajax
{
    /// <summary>
    /// 这个文件放置
    /// 登录后才能进行的动作
    /// </summary>
    public partial class Ajax_userin : ShopPage
    {
        public void LoadPage()
        {
            if (CurrentUser.id == 0)
            {
                Response.Write("{\"msg\":\"" + Tag("请先登陆") + "\",\"url\":\"" + URL("P_Login", "") + "\"}");
                return;
            }
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }


        /// <summary>
        /// 修改密码
        /// </summary>
        public void SetPassword()
        {
            string PWD = RequestTool.RequestSafeString("Password");
            string PWD1 = RequestTool.RequestSafeString("Password1");
            string PWD2 = RequestTool.RequestSafeString("Password2");
            PWD = EX_User.MD5(PWD);
            if (PWD != CurrentUser.Password && CurrentUser.Password != "")
            {
                Response.Write("{\"msg\":\"" + Tag("原始密码错误") + "\"}");
                return;
            }
            CurrentUser.Password = EX_User.MD5(PWD1);
            B_Lebi_User.Update(CurrentUser);
            Response.Write("{\"msg\":\"OK\"}");

        }
        /// <summary>
        /// 修改支付密码
        /// </summary>
        public void SetPayPassword()
        {
            string PWD = RequestTool.RequestSafeString("Pay_Password");
            string PWD1 = RequestTool.RequestSafeString("Pay_Password1");
            string PWD2 = RequestTool.RequestSafeString("Pay_Password2");
            PWD = EX_User.MD5(PWD);
            if (PWD != CurrentUser.Pay_Password && CurrentUser.Pay_Password != "")
            {
                Response.Write("{\"msg\":\"" + Tag("原始密码错误") + "\"}");
                return;
            }
            else
            {
                Lebi_User_Answer user_answer = B_Lebi_User_Answer.GetModel("User_id= "+ CurrentUser.id);
                if (user_answer == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("请先设置安全问题") + "\"}");
                    return;
                }
            }
            CurrentUser.Pay_Password = EX_User.MD5(PWD1);
            B_Lebi_User.Update(CurrentUser);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 修改个人资料
        /// </summary>
        public void User_Info()
        {
            bool Checkmobilephone = false;
            bool Checkemail = false;
            string MobilePhone = RequestTool.RequestSafeString("MobilePhone");
            string Email = RequestTool.RequestSafeString("Email");
            if (SYS.UserRegCheckedType.Contains("mobilephone"))
            {
                if (CurrentUser.MobilePhone != MobilePhone)
                {
                    Checkmobilephone = true;
                    string MobilePhone_checkcode = RequestTool.RequestSafeString("MobilePhone_checkcode");
                    try
                    {
                        string phonecheckcode = (string)Session["phonecheckcode"];
                        if (phonecheckcode != (MobilePhone + MobilePhone_checkcode))
                        {
                            Response.Write("{\"msg\":\"" + Tag("手机验证码错误") + "\"}");
                            return;
                        }
                        if (SYS.IsMobilePhoneMutiReg == "0")
                        {
                            int phonecount = B_Lebi_User.Counts("MobilePhone=lbsql{'" + MobilePhone + "'} and IsCheckedMobilePhone=1");
                            if (phonecount > 0)
                            {
                                Response.Write("{\"msg\":\"" + Tag("此手机号已经注册") + "\"}");
                                return;
                            }
                        }
                    }
                    catch
                    {
                        Response.Write("{\"msg\":\"" + Tag("手机验证码错误") + "\"}");
                        return;
                    }
                }
            }
            if (SYS.UserRegCheckedType.Contains("email"))
            {
                if (CurrentUser.Email != Email)
                {
                    Checkemail = true;
                    string Email_checkcode = RequestTool.RequestSafeString("Email_checkcode");
                    try
                    {
                        string emailcheckcode = (string)Session["emailcheckcode"];
                        if (emailcheckcode != (Email + Email_checkcode))
                        {
                            Response.Write("{\"msg\":\"" + Tag("邮件验证码错误") + "\"}");
                            return;
                        }
                        int emailcount = B_Lebi_User.Counts("Email=lbsql{'" + Email + "'} and IsCheckedEmail=1");
                        if (emailcount > 0)
                        {
                            Response.Write("{\"msg\":\"" + Tag("此邮箱已经注册") + "\"}");
                            return;
                        }
                    }
                    catch
                    {
                        Response.Write("{\"msg\":\"" + Tag("邮件验证码错误") + "\"}");
                        return;
                    }
                }
            }
            B_Lebi_User.SafeBindForm(CurrentUser);
            if (Checkemail)
                CurrentUser.IsCheckedEmail = 1;
            if (Checkmobilephone)
                CurrentUser.IsCheckedMobilePhone = 1;
            B_Lebi_User.Update(CurrentUser);
            EX_User.UserInfoEdit(CurrentUser);//触发编辑用户资料事件
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 账号合并
        /// </summary>
        public void User_Bind()
        {
            string UserName = RequestTool.RequestSafeString("UserName");
            string PWD = RequestTool.RequestSafeString("Password");
            PWD = EX_User.MD5(PWD);
            string shortpwd = PWD;
            if (shortpwd.Length > 24)
                shortpwd = PWD.Substring(8, 16);
            Lebi_User DelUser = B_Lebi_User.GetModel("(UserName=lbsql{'" + UserName + "'} or (Email=lbsql{'" + UserName + "'} and IsCheckedEmail=1) or (MobilePhone=lbsql{'" + UserName + "'} and IsCheckedMobilePhone=1)) and (Password='" + PWD + "' or Password='" + shortpwd + "')");
            if (DelUser == null)
            {
                Response.Write("{\"msg\":\"" + Tag("用户名或密码错误") + "\"}");
                return;
            }
            if (CurrentUser.id == DelUser.id)
            {
                Response.Write("{\"msg\":\"" + Tag("用户不存在") + "\"}");
                return;
            }
            if (EX_User.UserBind(CurrentUser, DelUser))
            {
                Response.Write("{\"msg\":\"OK\"}");
                return;
            }
            else
            {
                Response.Write("{\"msg\":\"" + Tag("系统异常") + "\"}");
                return;
            }
        }
        /// <summary>
        /// 删除收藏商品
        /// </summary>
        public void UserLike_Del()
        {
            string ids = RequestTool.RequestSafeString("id");
            if (ids == "")
            {
                Response.Write("{\"msg\":\"OK\"}");
                return;
            }
            List<Lebi_User_Product> models = B_Lebi_User_Product.GetList("User_id=" + CurrentUser.id + " and id in (lbsql{" + ids + "})", "");
            foreach (Lebi_User_Product model in models)
            {
                B_Lebi_User_Product.Delete(model.id);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除常购商品
        /// </summary>
        public void UserOftenBuy_Del()
        {
            string ids = RequestTool.RequestSafeString("id");
            if (ids == "")
            {
                Response.Write("{\"msg\":\"OK\"}");
                return;
            }
            List<Lebi_User_Product> models = B_Lebi_User_Product.GetList("User_id=" + CurrentUser.id + " and id in (lbsql{" + ids + "})", "");
            foreach (Lebi_User_Product model in models)
            {
                B_Lebi_User_Product.Delete(model.id);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 更新常购商品
        /// </summary>
        public void UserOftenBuy_Update()
        {
            string id = RequestTool.RequestSafeString("Uid");
            List<Lebi_User_Product> models = B_Lebi_User_Product.GetList("User_id=" + CurrentUser.id + " and id in (lbsql{" + id + "})", "");
            foreach (Lebi_User_Product model in models)
            {
                model.count = RequestTool.RequestInt("Count" + model.id + "", 0);
                model.WarnDays = RequestTool.RequestInt("WarnDays" + model.id + "", 0);
                model.Time_addemail = System.DateTime.Now.Date.AddDays(model.WarnDays);
                B_Lebi_User_Product.Update(model);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 积分转换
        /// </summary>
        public void PointToMoney()
        {
            string t = RequestTool.RequestSafeString("t");
            if (!CurrentUserLevel.PointToMoney.Contains(t))
            {
                Response.Write("{\"msg\":\"" + Tag("非法提交") + "\"}");
                return;
            }
            string[] arr = t.Split(':');
            if (arr.Length != 2)
            {
                Response.Write("{\"msg\":\"" + Tag("非法提交") + "\"}");
                return;
            }
            if (Convert.ToDecimal(arr[0]) > Convert.ToDecimal(CurrentUser.Point))
            {
                Response.Write("{\"msg\":\"" + Tag("积分不足") + "\"}");
                return;
            }
            Lebi_User_Money model = new Lebi_User_Money();
            model.Money = Convert.ToDecimal(arr[1]);
            model.Type_id_MoneyStatus = 181;
            model.Type_id_MoneyType = 194;
            model.Remark = "积分兑换";
            model.Time_Update = DateTime.Now;
            model.User_id = CurrentUser.id;
            model.User_RealName = CurrentUser.RealName;
            model.User_UserName = CurrentUser.UserName;
            B_Lebi_User_Money.Add(model);

            Lebi_User_Point pmodel = new Lebi_User_Point();
            pmodel.Point = 0 - Convert.ToDecimal(arr[0]);
            pmodel.Type_id_PointStatus = 171;
            pmodel.Remark = "积分兑换";
            pmodel.Time_Update = DateTime.Now;
            pmodel.User_id = CurrentUser.id;
            pmodel.User_RealName = CurrentUser.RealName;
            pmodel.User_UserName = CurrentUser.UserName;
            B_Lebi_User_Point.Add(pmodel);

            CurrentUser.Point = CurrentUser.Point - Convert.ToDecimal(arr[0]);
            CurrentUser.Money = CurrentUser.Money + Convert.ToDecimal(arr[1]);
            B_Lebi_User.Update(CurrentUser);
            //发送短信
            //Lebi_User user = B_Lebi_User.GetModel(CurrentUser.id);
            SMS.SendSMS_balance(CurrentUser);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 收藏夹批量添加购物车
        /// </summary>
        public void LikeToBasket()
        {
            string ids = RequestTool.RequestSafeString("id");
            string mes = "";
            if (ids == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请先选择商品") + "\"}");
                return;
            }else{
                List<Lebi_User_Product> models = B_Lebi_User_Product.GetList("(Type_id_UserProductType=141 or Type_id_UserProductType=144) and User_id=" + CurrentUser.id + " and id in (lbsql{" + ids + "})", "");
                foreach (Lebi_User_Product model in models)
                {
                    //<-{ 判断收藏夹商品是否为上架状态 by lebi.kingdge 2015-02-10
                    Lebi_Product Product = B_Lebi_Product.GetModel("id = "+ model.Product_id +" and Type_id_ProductStatus = 101");
                    if (Product == null)
                    {
                        Response.Write("{\"msg\":\""+Lang(Product.Name)+" " + Tag("该商品已经下架") + "\"}");
                        return;
                    }
                    //}->
                    int num = model.count;
                    int levelcount = ProductLevelCount(Product);
                    if (num < levelcount)
                    {
                        num = levelcount;
                    }
                    Lebi_User_Product updatemodel = B_Lebi_User_Product.GetModel("User_id=" + CurrentUser.id + " and product_id=" + model.Product_id + " and type_id_UserProductType=142");
                    if (updatemodel != null){
                        if (updatemodel.count < num)
                        {
                            updatemodel.count = model.count;
                            B_Lebi_User_Product.Update(updatemodel);
                        }
                    }
                    else
                    {
                        model.Type_id_UserProductType = 142;
                        model.count = num;
                        B_Lebi_User_Product.Add(model);
                    }
                }
                mes = Tag("商品已加入购物车") + "<div>" + Tag("数量") + " <span>" + Basket_Product_Count() + "</span> " + Tag("件") + " <span>" + FormatMoney(Basket_Product_Price()) + "</span></div><div><a href='" + URL("P_Basket", "") + "' class='btn btn-7'><s></s>" + Tag("查看购物车") + "</a>&nbsp;&nbsp;<a href='javascript:void(0)' onclick='cloesedialog();' class='btn btn-11'><s></s>" + Tag("关闭") + "</a></div>";
            }
            Response.Write("{\"msg\":\"OK\",\"count\":\"" + Basket_Product_Count() + "\",\"amount\":\"" + FormatMoney(Basket_Product_Price()) + "\",\"mes\":\"" + mes + "\",\"url\":\"" + URL("P_AddToBasket", "") + "\"}");
        }
        /// <summary>
        /// 发表商品评价
        /// </summary>
        public void Comment_Write()
        {
            int id = RequestTool.RequestInt("id", 0);
            //int Product_id = 0;
            if (!Comment.CheckSafeWord(RequestTool.RequestSafeString("Content")))
            {
                Response.Write("{\"msg\":\"" + Tag("内容中包含敏感词") + "\"}");
                return;
            }
            Lebi_Order_Product Order_Product = B_Lebi_Order_Product.GetModel("User_id=" + CurrentUser.id + " and id = "+ id);
            if (Order_Product == null)
            {
                Response.Write("{\"msg\":\"" + Tag("该商品已经下架") + "\"}");
                return;
            }
            else
            {
                Lebi_Order order = B_Lebi_Order.GetModel(Order_Product.Order_id);
                if (order == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("无效订单") + "\"}");
                    return;
                }
                else
                {
                    if (order.IsReceived == 0)
                    {
                        Response.Write("{\"msg\":\"" + Tag("已购买商品在已收货之后才可以发表评价") + "\"}");
                        return;
                    }
                }
                Order_Product.IsCommented = 1;
                B_Lebi_Order_Product.Update(Order_Product);
            }
            Lebi_Product checkproduct = B_Lebi_Product.GetModel(Order_Product.Product_id);
            if (checkproduct == null)
            {
                Response.Write("{\"msg\":\"" + Tag("该商品已经下架") + "\"}");
                return;
            }
            Lebi_Comment model = new Lebi_Comment();
            model.TableName = "Product";
            model.Keyid = Order_Product.Product_id;
            model.Admin_UserName = "管理员";
            model.Admin_id = 0;
            model.User_id = CurrentUser.id;
            model.User_UserName = CurrentUser.NickName;
            model.Content = RequestTool.RequestSafeString("Content");
            model.Star = RequestTool.RequestInt("Star", 5);
            model.Parentid = 0;
            model.Status = 280;
            model.Time_Add = System.DateTime.Now;
            model.Language_Code = CurrentLanguage.Code;
            model.Images = RequestTool.RequestSafeString("Images");
            model.ImagesSmall = RequestTool.RequestSafeString("ImagesSmall");
            model.Product_id = Order_Product.Product_id;
            model.IsRead = 1;
            model.Supplier_id = checkproduct.Supplier_id;
            B_Lebi_Comment.Add(model);
            //List<Lebi_Order_Product> modelproducts = B_Lebi_Order_Product.GetList("User_id=" + CurrentUser.id + " and id = " + id + "", "");
            //foreach (Lebi_Order_Product modelproduct in modelproducts)
            //{
            //    modelproduct.IsCommented = 1;
            //    B_Lebi_Order_Product.Update(modelproduct);
            //}
            Lebi_Product product = B_Lebi_Product.GetModel(Order_Product.Product_id);
            if (product != null)
            {
                EX_Product.UpdateStar(product);
            }
            model.id = B_Lebi_Comment.GetMaxId();
            //处理图片
            ImageHelper.LebiImagesUsed(model.Images, "comment", model.id);
            //发送邮件
            Lebi_User user = B_Lebi_User.GetModel(CurrentUser.id);
            Email.SendEmail_comment(user, model);
            //发送短信
            SMS.SendSMS_comment(user, model);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 晒单评论
        /// </summary>
        public void Comment_reply()
        {
            int id = RequestTool.RequestInt("id", 0);
            int Product_id = 0;
            Lebi_Product product = B_Lebi_Product.GetModel(id);
            if (product != null)
            {
                Product_id = product.Product_id;
            }
            Lebi_Comment pmodel = B_Lebi_Comment.GetModel(id);
            if (pmodel == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            string Content = RequestTool.RequestSafeString("comment");
            if (Content == "")
            {
                Response.Write("{\"msg\":\"" + Tag("内容不能为空") + "\"}");
                return;
            }
            Lebi_Comment model = new Lebi_Comment();
            model.TableName = "Product";
            model.Keyid = id;
            //model.Admin_UserName = CurrentUser.NickName;
            model.Admin_id = 0;
            model.User_id = CurrentUser.id;
            model.User_UserName = CurrentUser.NickName;
            model.Content = Content;
            model.Parentid = id;
            model.Status = 280;
            model.Time_Add = System.DateTime.Now;
            model.Language_Code = CurrentLanguage.Code;
            model.Product_id = Product_id;
            model.IsRead = 0;
            model.Supplier_id = product.Supplier_id;
            if (!Comment.CheckSafeWord(model.Content))
            {
                Response.Write("{\"msg\":\"" + Tag("内容中包含敏感词") + "\"}");
                return;
            }
            B_Lebi_Comment.Add(model);
            //发送邮件
            if (ShopCache.GetBaseConfig().AdminMailSign.ToLower().Contains("comment"))
            {
                Lebi_User user = B_Lebi_User.GetModel(CurrentUser.id);
                Email.SendEmail_comment(user, model);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 发表商品咨询
        /// </summary>
        public void Ask_Write()
        {
            int id = RequestTool.RequestInt("id", 0);
            int Product_id = 0;
            Lebi_Product product = B_Lebi_Product.GetModel(id);
            if (product != null)
            {
                Product_id = product.Product_id;
            }
            Lebi_Comment model = new Lebi_Comment();
            model.TableName = "Product_Ask";
            model.Keyid = id;
            model.Admin_UserName = "管理员";
            model.Admin_id = 0;
            model.User_id = CurrentUser.id;
            model.User_UserName = CurrentUser.UserName;
            model.Content = RequestTool.RequestSafeString("Content");
            model.Star = 0;
            model.Parentid = 0;
            model.Status = 282;  //283已回复
            model.Time_Add = System.DateTime.Now;
            model.Language_Code = CurrentLanguage.Code;
            model.Product_id = Product_id;
            model.IsRead = 1;
            model.Supplier_id = product.Supplier_id;
            if (!Comment.CheckSafeWord(model.Content))
            {
                Response.Write("{\"msg\":\"" + Tag("内容中包含敏感词") + "\"}");
                return;
            }
            B_Lebi_Comment.Add(model);
            //发送邮件
            Lebi_User user = B_Lebi_User.GetModel(CurrentUser.id);
            Email.SendEmail_ask(user, model);
            //发送短信
            SMS.SendSMS_ask(user, model);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 发送站内信
        /// </summary>
        public void Message_Write()
        {
            if (CurrentUser.id > 0)//已经登录
            {
                int id = RequestTool.RequestInt("id", 0);
                int Message_Type_id = RequestTool.RequestInt("Message_Type_id", 0);
                string Title = RequestTool.RequestSafeString("Title");
                string Content = RequestTool.RequestSafeString("Content");
                Lebi_Message model = new Lebi_Message();
                if (id != 0)
                {
                    Lebi_Message mes = B_Lebi_Message.GetModel(id);
                    if (mes == null)
                    {
                        Response.Write("{\"msg\":\"" + Tag("回复信息不存在") + "\"}");
                        return;
                    }
                    else
                    {
                        model.Message_Type_id = mes.Message_Type_id;
                    }
                }
                model.Title = Title;
                model.Content = Content;
                model.User_id_From = CurrentUser.id;
                model.User_Name_From = CurrentUser.UserName;
                model.User_id_To = 0;
                model.User_Name_To = "管理员";
                model.IsRead = 0;
                model.IsSystem = 0;
                model.Time_Add = System.DateTime.Now;
                model.Language = CurrentLanguage.Code;
                model.IP = RequestTool.GetClientIP();
                B_Lebi_Message.Add(model);
                //发送邮件
                Lebi_User user = B_Lebi_User.GetModel(CurrentUser.id);
                Email.SendEmail_message(user, model);
                //发送短信
                SMS.SendSMS_message(user, model);
                Response.Write("{\"msg\":\"OK\"}");
            }
        }
        /// <summary>
        /// 删除站内信
        /// </summary>
        public void Message_Delete()
        {
            if (CurrentUser.id > 0)//已经登录
            {
                string id = RequestTool.RequestSafeString("ids");
                if (id == "")
                {
                    Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                    return;
                }
                B_Lebi_Message.Delete("(User_id_To=" + CurrentUser.id + " or User_id_From=" + CurrentUser.id + ") and id in (lbsql{" + id + "})");
                Response.Write("{\"msg\":\"OK\"}");
            }
        }
        /// <summary>
        /// 删除商品评价
        /// </summary>
        public void Comment_Del()
        {
            if (CurrentUser.id > 0)//已经登录
            {
                string id = RequestTool.RequestSafeString("ids");
                if (id == "")
                {
                    Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                    return;
                }
                B_Lebi_Comment.Delete("TableName = 'Product' and User_id=" + CurrentUser.id + " and Parentid in (lbsql{" + id + "})");
                B_Lebi_Comment.Delete("Parentid = 0 and TableName = 'Product' and User_id=" + CurrentUser.id + " and id in (lbsql{" + id + "})");
                Response.Write("{\"msg\":\"OK\"}");
            }
        }
        /// <summary>
        /// 编辑订单-订单状态变更
        /// </summary>
        public void Order_Status()
        {
            if (CurrentUser.id > 0)//已经登录
            {
                int id = RequestTool.RequestInt("id", 0);
                string type = RequestTool.RequestSafeString("type");
                Lebi_Order model = B_Lebi_Order.GetModel("User_id = " + CurrentUser.id + " and id = " + id + "");
                if (model == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                }
                else
                {
                    if (type == "invalid")
                    {
                        if (Shop.Bussiness.Order.CancelOrder(model))
                        {
                            model.IsInvalid = 1;
                            model.Time_Completed = System.DateTime.Now;
                            //if (model.IsPaid == 1)  //如果已付款，返回到预存款账户 by lebi.kingdge 20151018
                            //{
                            //    model.IsPaid = 0;
                            //    Money.AddMoney(CurrentUser, model.Money_Pay + model.Money_UserCut, 195, null, Tag("取消订单") + " " + model.Code, Tag("取消订单") + " " + model.Code);
                            //}
                            B_Lebi_Order.Update(model);
                        }
                    }
                    //if (type == "received")
                    //{
                    //    model.IsReceived = 1;
                    //    model.Time_Received = System.DateTime.Now;
                    //    B_Lebi_Order.Update(model);
                    //}
                }
                Response.Write("{\"msg\":\"OK\"}");
            }
        }
        /// <summary>
        /// 取消帐号绑定
        /// </summary>
        public void Cancalbind()
        {
            string t = RequestTool.RequestString("t");
            int c = 0;
            if (CurrentUser.bind_facebook_id != "")
                c++;
            if (CurrentUser.bind_taobao_id != "")
                c++;
            if (CurrentUser.bind_weibo_id != "")
                c++;
            if (CurrentUser.bind_qq_id != "")
                c++;
            if (CurrentUser.bind_weixin_id != "")
                c++;
            if (c < 2)
            {
                if (CurrentUser.IsPlatformAccount == 1)
                {
                    Response.Write("{\"msg\":\"" + Tag("不可以解除全部帐号绑定") + "\"}");
                    return;
                }
            }
            if (t == "qq")
            {
                CurrentUser.bind_qq_id = "";
                CurrentUser.bind_qq_nickname = "";
                CurrentUser.bind_qq_token = "";
            }
            if (t == "weibo")
            {
                CurrentUser.bind_weibo_id = "";
                CurrentUser.bind_weibo_nickname = "";
                CurrentUser.bind_weibo_token = "";
            }
            if (t == "taobao")
            {
                CurrentUser.bind_taobao_id = "";
                CurrentUser.bind_taobao_nickname = "";
                CurrentUser.bind_taobao_token = "";
            }
            if (t == "facebook")
            {
                CurrentUser.bind_taobao_id = "";
                CurrentUser.bind_taobao_nickname = "";
                CurrentUser.bind_taobao_token = "";
            }
            if (t == "weixin")
            {
                CurrentUser.bind_weixin_id = "";
                CurrentUser.bind_weixin_nickname = "";
                CurrentUser.bind_weixin_token = "";
            }
            B_Lebi_User.Update(CurrentUser);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑安全问题
        /// </summary>
        public void Question_Edit()
        {
            int type = RequestTool.RequestInt("type", 0);
            int Question_id1 = RequestTool.RequestInt("Question_id1", 0);
            int Question_id2 = RequestTool.RequestInt("Question_id2", 0);
            string Answer1 = EX_User.MD5(RequestTool.RequestSafeString("Answer1"));
            string Answer2 = EX_User.MD5(RequestTool.RequestSafeString("Answer2"));
            if (Question_id1 == Question_id2)
            {
                Response.Write("{\"msg\":\"" + Tag("请选择两个不同的问题") + "\"}");
                return;
            }
            if (Answer1 == "" || Answer2 == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请填写问题答案") + "\"}");
                return;
            }
            bool addflag = false;
            Lebi_User_Answer model = B_Lebi_User_Answer.GetModel("User_id = " + CurrentUser.id + "");
            if (model == null)
            {
                addflag = true;
                model = new Lebi_User_Answer();
            }
            if (addflag)
            {
                model.User_Question_id = Question_id1;
                model.Answer = Answer1;
                model.User_id = CurrentUser.id;
                B_Lebi_User_Answer.Add(model);
                model.User_Question_id = Question_id2;
                model.Answer = Answer2;
                model.User_id = CurrentUser.id;
                B_Lebi_User_Answer.Add(model);
            }
            else
            {
                int i = 0;
                List<Lebi_User_Answer> modellists = B_Lebi_User_Answer.GetList("User_id = " + CurrentUser.id + "", "id asc");
                foreach (Lebi_User_Answer modellist in modellists)
                {
                    if (i == 0 && modellist.Answer != Answer1)
                    {
                        Response.Write("{\"msg\":\"" + Tag("问题答案回答不正确") + "\"}");
                        return;
                    }
                    if (i == 1 && modellist.Answer != Answer2)
                    {
                        Response.Write("{\"msg\":\"" + Tag("问题答案回答不正确") + "\"}");
                        return;
                    }
                    i++;
                }
                if (type == 1)
                {
                    CurrentUser.Pay_Password = "";
                    B_Lebi_User.Update(CurrentUser);
                    Response.Write("{\"msg\":\"OK\",\"type\":\"1\",\"url\":\"" + URL("P_UserChangePassword", "") + "\"}");
                    return;
                }
                else
                {
                    B_Lebi_User_Answer.Delete("User_id = " + CurrentUser.id + "");
                }
            }
            Response.Write("{\"msg\":\"OK\",\"type\":\"0\"}");

        }
        /// <summary>
        /// 提现
        /// </summary>
        public void TackMoney()
        {
            string PayType = RequestTool.RequestSafeString("PayType");
            string CashAccount_Code = RequestTool.RequestSafeString("CashAccount_Code");
            string CashAccount_Name = RequestTool.RequestSafeString("CashAccount_Name");
            string CashAccount_Bank = RequestTool.RequestSafeString("CashAccount_Bank");
            string Pay_Password = RequestTool.RequestSafeString("Pay_Password");
            decimal RMoney = RequestTool.RequestDecimal("RMoney", 0);
            decimal Fee = RMoney * Convert.ToDecimal(SYS.WithdrawalFeeRate) / 100;
            int count = B_Lebi_Cash.Counts("User_id=" + CurrentUser.id + " and Type_id_CashStatus=401");
            if (count > 0)
            {
                Response.Write("{\"msg\":\"" + Tag("请等待上次提现处理完毕") + "\"}");
                return;
            }
            if (RMoney+ CurrentUser.Money_fanxian > CurrentUser.Money)
            {
                Response.Write("{\"msg\":\"" + Tag("余额不足") + "\"}");
                return;
            }
            decimal TakeMoneyLimit = 0;
            decimal.TryParse(SYS.TakeMoneyLimit, out TakeMoneyLimit);
            if (RMoney < TakeMoneyLimit)
            {
                Response.Write("{\"msg\":\"" + Tag("未达到最低提现要求") + "\"}");
                return;
            }
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
            CurrentUser.CashAccount_Code = CashAccount_Code;
            CurrentUser.CashAccount_Name = CashAccount_Name;
            CurrentUser.CashAccount_Bank = CashAccount_Bank;
            B_Lebi_User.Update(CurrentUser);
            Lebi_Cash model = new Lebi_Cash();
            model.PayType = PayType;
            model.AccountName = CashAccount_Name;
            model.AccountCode = CashAccount_Code;
            model.Bank = CashAccount_Bank;
            model.User_id = CurrentUser.id;
            model.User_UserName = CurrentUser.UserName;
            model.Money = RMoney;
            model.Fee = Fee;
            model.Type_id_CashStatus = 401;
            B_Lebi_Cash.Add(model);
            //扣款
            Money.AddMoney(CurrentUser, 0 - RMoney, 193, null, "", "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 余额购买其他内容
        /// </summary>
        public void BuyOther()
        {
            int keyid = RequestTool.RequestInt("keyid");
            string tablename = RequestTool.RequestSafeString("tablename");
            decimal postmoney = RequestTool.RequestDecimal("money");
            string paypwd = RequestTool.RequestSafeString("paypwd");
            decimal money = 0;

            if (EX_User.MD5(paypwd) != CurrentUser.Pay_Password)
            {
                Response.Write("{\"msg\":\"" + Tag("支付密码错误") + "\"}");
                return;
            }
            if (tablename == "Agent_Product_Level")
            {
                Lebi_Agent_Product_Level lev = B_Lebi_Agent_Product_Level.GetModel(keyid);
                if (lev == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                    return;
                }
                Lebi_Agent_Product_User user = B_Lebi_Agent_Product_User.GetModel("User_id=" + CurrentUser.id + "");
                if (user == null)
                {
                    //新购买的情况
                    money = lev.Price;
                    if (money > CurrentUser.Money)
                    {
                        Response.Write("{\"msg\":\"" + Tag("余额不足") + "\"}");
                        return;
                    }
                    user = new Lebi_Agent_Product_User();
                    user.Agent_Product_Level_id = lev.id;
                    user.Commission = lev.Commission;
                    user.Count_Product = lev.Count_Product;
                    user.Count_product_change = lev.Count_product_change;
                    user.User_id = CurrentUser.id;
                    user.User_UserName = CurrentUser.UserName;
                    user.Time_end = System.DateTime.Now.AddYears(lev.Years);
                    B_Lebi_Agent_Product_User.Add(user);
                }
                else
                {
                    if (user.Agent_Product_Level_id == lev.id)
                    {
                        //续费的情况
                        money = lev.Price;
                        if (money > CurrentUser.Money)
                        {
                            Response.Write("{\"msg\":\"" + Tag("余额不足") + "\"}");
                            return;
                        }
                        user.Time_end = user.Time_end.AddYears(lev.Years);
                        B_Lebi_Agent_Product_User.Update(user);
                        //更新代理商品的过期时间
                    }
                    else
                    {
                        //升级的情况

                        Lebi_Agent_Product_Level userlev = B_Lebi_Agent_Product_Level.GetModel(user.Agent_Product_Level_id);
                        if (userlev.Sort > lev.Sort)
                        {
                            Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                            return;
                        }
                        money = lev.Price - userlev.Price;
                        if (money < 0)
                        {
                            Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                            return;
                        }
                        if (money > CurrentUser.Money)
                        {
                            Response.Write("{\"msg\":\"" + Tag("余额不足") + "\"}");
                            return;
                        }
                        user.Agent_Product_Level_id = lev.id;
                        user.Commission = lev.Commission;
                        user.Count_Product = lev.Count_Product;
                        user.Count_product_change = lev.Count_product_change;
                        B_Lebi_Agent_Product_User.Update(user);
                    }
                }
                EX_User.GiveUserCard(CurrentUser, lev.CardOrder_id);//赠送代金券
                Money.AddMoney(CurrentUser, 0 - money, 196, null, "", "");
            }
            else if (tablename == "Agent_Area")
            {
                Lebi_Agent_Area area = B_Lebi_Agent_Area.GetModel(keyid);
                if (area == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                    return;
                }
                if (area.User_id > 0 && area.User_id != CurrentUser.id)
                {
                    Response.Write("{\"msg\":\"" + Tag("不能代理此区域") + "\"}");
                    return;
                }
                money = area.Price;
                if (money > CurrentUser.Money)
                {
                    Response.Write("{\"msg\":\"" + Tag("余额不足") + "\"}");
                    return;
                }
                if (area.User_id == 0)
                {
                    //新购买的情况
                    area.User_id = CurrentUser.id;
                    area.User_UserName = CurrentUser.UserName;
                    area.Time_end = System.DateTime.Now.AddYears(1);
                    area.IsFailure = 0;
                    B_Lebi_Agent_Area.Update(area);
                }
                else
                {
                    //续费的情况
                    area.Time_end = area.Time_end.AddYears(1);
                    area.IsFailure = 0;
                    B_Lebi_Agent_Area.Update(area);
                }
                EX_User.GiveUserCard(CurrentUser, area.CardOrder_id);//赠送代金券
                Money.AddMoney(CurrentUser, 0 - money, 196, null, "", "");
            }
            else if (tablename == "suppliermargin")//供应商保证金
            {
                Lebi_Supplier supplier = B_Lebi_Supplier.GetModel("User_id=" + CurrentUser.id);
                if (supplier == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                    return;
                }
                //新购买的情况
                money = supplier.Money_Margin - supplier.Money_Margin_pay;
                if (money > CurrentUser.Money)
                {
                    Response.Write("{\"msg\":\"" + Tag("余额不足") + "\"}");
                    return;
                }
                supplier.Money_Margin_pay += money;
                B_Lebi_Supplier.Update(supplier);
                Money.AddMoney(CurrentUser, 0 - money, 197, null, "", "");
            }
            else if (tablename == "suppliermargin")//供应商保证金
            {
                Lebi_Supplier supplier = B_Lebi_Supplier.GetModel("User_id=" + CurrentUser.id);
                if (supplier == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                    return;
                }
                money = supplier.Money_Margin - supplier.Money_Margin_pay;
                if (money > CurrentUser.Money)
                {
                    Response.Write("{\"msg\":\"" + Tag("余额不足") + "\"}");
                    return;
                }
                supplier.Money_Margin_pay += money;
                B_Lebi_Supplier.Update(supplier);
                Money.AddMoney(CurrentUser, 0 - money, 197, null, "", "");
            }
            else if (tablename == "supplierservice")//供应商服务费
            {
                Lebi_Supplier supplier = B_Lebi_Supplier.GetModel("User_id=" + CurrentUser.id);
                if (supplier == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                    return;
                }
                Lebi_Supplier_Group group = B_Lebi_Supplier_Group.GetModel("id=" + supplier.Supplier_Group_id);
                if (group == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                    return;
                }
                money = supplier.Money_Service;
                if (money > CurrentUser.Money)
                {
                    Response.Write("{\"msg\":\"" + Tag("余额不足") + "\"}");
                    return;
                }
                supplier.Time_End = supplier.Time_End.AddDays(group.ServiceDays);
                B_Lebi_Supplier.Update(supplier);
                Money.AddMoney(CurrentUser, 0 - money, 198, null, "", "");
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 添加/编辑收款账号
        /// </summary>
        public void Bank_Edit()
        {

            int id = RequestTool.RequestInt("id", 0);
            Lebi_User_Bank model = B_Lebi_User_Bank.GetModel("User_id=" + CurrentUser.id + " and id = " + id);
            if (model == null)
            {
                model = new Lebi_User_Bank();
                model = B_Lebi_User_Bank.SafeBindForm(model);
                model.User_id = CurrentUser.id;
                B_Lebi_User_Bank.Add(model);
            }
            else
            {
                model = B_Lebi_User_Bank.SafeBindForm(model);
                B_Lebi_User_Bank.Update(model);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除收款账号
        /// </summary>
        public void Bank_Del()
        {
            string id = RequestTool.RequestSafeString("id");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_User_Bank.Delete("User_id = " + CurrentUser.id + " and id in (lbsql{" + id + "})");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 发放卡券
        /// </summary>
        public void CardGet()
        {
            int id = RequestTool.RequestInt("id", 0);
            string verifycode = RequestTool.RequestString("verifycode");
            if (CurrentCheckCode != verifycode)
            {
                Response.Write("{\"msg\":\"" + Tag("验证码错误") + "\"}");
                return;
            }
            //Lebi_CardOrder order = B_Lebi_CardOrder.GetModel("Type_id_CardType=312 and id = " + id + "");
            //if (order == null)
            //{
            //    Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
            //    return;
            //}
            //int count = B_Lebi_Card.Counts("Type_id_CardType=312 and CardOrder_id=" + id + " and Type_id_CardStatus=200 and User_id=0");
            //if (count == 0)
            //{
            //    Response.Write("{\"msg\":\"" + Tag("卡券数量不足") + "\"}");
            //    return;
            //}
            int getcount = B_Lebi_Card.Counts("Type_id_CardType=312 and CardOrder_id=" + id + " and User_id=" + CurrentUser.id + "");
            if (getcount > 0)
            {
                Response.Write("{\"msg\":\"" + Tag("不可以重复领取") + "\"}");
                return;
            }
            Lebi_Card c = B_Lebi_Card.GetModel("Type_id_CardType=312 and CardOrder_id=" + id + " and Type_id_CardStatus=200 and User_id=0");
            if (c == null)
            {
                Response.Write("{\"msg\":\"" + Tag("卡券数量不足") + "\"}");
                return;
            }
            c.User_id = CurrentUser.id;
            c.Type_id_CardStatus = 201;//已发放
            c.User_UserName = CurrentUser.UserName;
            B_Lebi_Card.Update(c);
            Response.Write("{\"msg\":\"OK\"}");
        }
    }
}