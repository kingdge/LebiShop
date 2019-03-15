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


namespace Shop.Admin.Ajax
{
    public partial class Ajax_user : AdminAjaxBase
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
        /// 编辑会员分组
        /// </summary>
        public void UserLevel_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_UserLevel model = B_Lebi_UserLevel.GetModel(id);

            if (model == null)
            {
                model = new Lebi_UserLevel();
            }
            B_Lebi_UserLevel.SafeBindForm(model);
            model.Name = Language.RequestString("Name");
            model.PriceName = Language.RequestString("PriceName");
            if (model.id == 0)
            {
                if (!EX_Admin.Power("userlevel_add", "添加会员分组"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_UserLevel.Add(model);
                id = B_Lebi_UserLevel.GetMaxId();
                Log.Add("添加会员分组", "UserLevel", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN"));
            }
            else
            {
                if (!EX_Admin.Power("userlevel_edit", "编辑会员分组"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_UserLevel.Update(model);
                Log.Add("编辑会员分组", "UserLevel", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN"));
            }
            ImageHelper.LebiImagesUsed(model.ImageUrl, "config", id);
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除会员分组
        /// </summary>
        public void UserLevel_Del()
        {
            int id = RequestTool.RequestInt("id", 0);
            if (!EX_Admin.Power("userlevel_del", "删除会员分组"))
            {
                AjaxNoPower();
                return;
            }
            //Lebi_Page model = B_Lebi_Page.GetModel(id);
            // if (model == null)
            //{
            //    Response.Write("{\"msg\":\"" + Tag("参数错误") +"\"}");
            //    return;
            //}
            //<-{删除会员分组，等级更新为上一等级 by kingdge
            int UserLevel_id = 1;
            Lebi_UserLevel model = B_Lebi_UserLevel.GetModel(id);
            if (model == null)
            {
                UserLevel_id = 1;
            }
            else
            {
                UserLevel_id = model.id;
            }
            Common.ExecuteSql("Update Lebi_User set UserLevel_id = " + UserLevel_id + " where UserLevel_id = " + id + "");
            //}->
            List<Lebi_UserLevel> ls = B_Lebi_UserLevel.GetList("id in (lbsql{" + id + "})", "");
            foreach (var l in ls)
            {
                int count = B_Lebi_User.Counts("(IsDel!=1 or IsDel is null) and UserLevel_id=" + l.id + "");
                if (count > 0)
                {
                    Response.Write("{\"msg\":\"分组下存在账号，不能删除\"}");
                    return;
                }
            }
            B_Lebi_UserLevel.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除会员分组", "UserLevel", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        public void CheckUserName()
        {
            int id = RequestTool.RequestInt("id", 0);
            string UserName = RequestTool.RequestString("UserName");
            string where = "UserName=lbsql{'" + UserName + "'}";
            if (id > 0)
                where += " and id!=" + id + "";
            int count = B_Lebi_User.Counts(where);
            if (count > 0)
                Response.Write("{\"msg\":\"NO\"}");
            else
                Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑会员
        /// </summary>
        public void User_Edit()
        {
            if (!EX_Admin.Power("user_edit", "编辑会员"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int User_id_parent = RequestTool.RequestInt("User_id_parent", 0);
            string UserName = RequestTool.RequestString("UserName");
            string where = "UserName=lbsql{'" + UserName + "'}";
            if (id > 0)
                where += " and id!=" + id + "";
            int count = B_Lebi_User.Counts(where);
            if (count > 0 && UserName != "")
            {
                Response.Write("{\"msg\":\"帐号已存在\"}");
                return;
            }
            Lebi_User model = B_Lebi_User.GetModel(id);

            if (model == null)
            {
                model = new Lebi_User();
            }
            B_Lebi_User.SafeBindForm(model);

            if (model.id == 0)
            {
                string PWD1 = RequestTool.RequestString("PWD1");
                string PWD2 = RequestTool.RequestString("PWD2");
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                string PWD = BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(PWD1))).Replace("-", "").ToLower();
                if (PWD1 != PWD2)
                {
                    Response.Write("{\"msg\":\"两次输入的密码不一致\"}");
                    return;
                }
                model.Password = PWD;
                model.Site_id = ShopCache.GetMainSite().id;
                B_Lebi_User.Add(model);
                model.id = B_Lebi_User.GetMaxId();
                EX_User.UserRegister(model);//触发事件
            }
            else
            {
                //<-{更新会员下级用户数量 by lebi.kingdge 2015-04-09
                if (User_id_parent > 0)
                {
                    int Count_sonuser = B_Lebi_User.Counts("id = " + User_id_parent + "");
                    string sql = "update [Lebi_User] set Count_sonuser=" + Count_sonuser + " where id=" + User_id_parent + "";
                    Common.ExecuteSql(sql);
                }
                model.Count_sonuser = B_Lebi_User.Counts("User_id_parent = " + id + "");
                //}->
                B_Lebi_User.Update(model);
                EX_User.UserInfoEdit(model);//触发编辑用户资料事件
            }
            Log.Add("编辑会员信息", "User", id.ToString(), CurrentAdmin, model.UserName);
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 编辑会员密码
        /// </summary>
        public void User_Edit_Password()
        {
            if (!EX_Admin.Power("user_edit", "编辑会员"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            string PWD1 = RequestTool.RequestString("PWD1");
            string PWD2 = RequestTool.RequestString("PWD2");
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            string PWD = BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(PWD1))).Replace("-", "").ToLower();
            if (PWD1 != PWD2)
            {
                Response.Write("{\"msg\":\"两次输入的密码不一致\"}");
                return;
            }
            Lebi_User model = B_Lebi_User.GetModel(id);
            model.Password = PWD;
            B_Lebi_User.Update(model);
            Log.Add("编辑会员密码", "User", id.ToString(), CurrentAdmin, model.UserName);
            string result = "{\"msg\":\"OK\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除会员
        /// </summary>
        public void User_Del()
        {
            if (!EX_Admin.Power("user_del", "删除会员"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            if (RequestTool.GetConfigKey("IsDelFalse").ToLower() == "true")
            {
                List<Lebi_User> users = B_Lebi_User.GetList("id in (lbsql{" + id + "})", "");
                foreach (var u in users)
                {
                    u.IsDel = 1;
                    B_Lebi_User.Update(u);
                }
                
            }
            else
            {


                List<Lebi_Order> modellist = B_Lebi_Order.GetList("User_id in (lbsql{" + id + "})", "");
                foreach (Lebi_Order model in modellist)
                {
                    B_Lebi_Order.Delete("id = " + model.id + "");
                    B_Lebi_Order_Log.Delete("Order_id = " + model.id + "");
                    B_Lebi_Order_Product.Delete("Order_id = " + model.id + "");
                }
                B_Lebi_User.Delete("id in (lbsql{" + id + "})");
                B_Lebi_Message.Delete("User_id_To in (lbsql{" + id + "})");
                B_Lebi_User_Answer.Delete("User_id in (lbsql{" + id + "})");
                B_Lebi_User_Answer.Delete("User_id in (lbsql{" + id + "})");
                B_Lebi_User_BuyMoney.Delete("User_id in (lbsql{" + id + "})");
                B_Lebi_User_Card.Delete("User_id in (lbsql{" + id + "})");
                B_Lebi_User_Money.Delete("User_id in (lbsql{" + id + "})");
                B_Lebi_User_Point.Delete("User_id in (lbsql{" + id + "})");
                B_Lebi_User_Product.Delete("User_id in (lbsql{" + id + "})");
                B_Lebi_User_Address.Delete("User_id in (lbsql{" + id + "})");
                B_Lebi_Supplier.Delete("User_id in (lbsql{" + id + "})");
                //B_Lebi_Supplier_ProductType.Delete("Supplier_id in (lbsql{" + id + "})");
                //B_Lebi_Supplier_Verified_Log.Delete("Supplier_id in (lbsql{" + id + "})");
                B_Lebi_Agent_Area.Delete("User_id in (lbsql{" + id + "})");
                B_Lebi_Agent_Money.Delete("User_id in (lbsql{" + id + "})");
                B_Lebi_Agent_Product.Delete("User_id in (lbsql{" + id + "})");
                B_Lebi_Agent_Product_request.Delete("User_id in (lbsql{" + id + "})");
                B_Lebi_Agent_Product_User.Delete("User_id in (lbsql{" + id + "})");
                B_Lebi_Bill.Delete("User_id in (lbsql{" + id + "})");
                B_Lebi_Cash.Delete("User_id in (lbsql{" + id + "})");
                B_Lebi_Comment.Delete("User_id in (lbsql{" + id + "})");
                B_Lebi_weixin_qrcode.Delete("User_id in (lbsql{" + id + "})");
            }
            Log.Add("删除会员", "User", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑会员积分
        /// </summary>
        public void UserPoint_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            SearchUser su = new SearchUser(CurrentAdmin, CurrentLanguage.Code);
            if (id > 0)
            {
                if (!EX_Admin.Power("user_point_edit", "编辑会员积分"))
                {
                    AjaxNoPower();
                    return;
                }
                Lebi_User_Point model = B_Lebi_User_Point.GetModel(id);
                Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                Log.Add("编辑会员积分", "User_Point", id.ToString(), CurrentAdmin, user.UserName);
                if (user == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("会员账号不存在") + "\"}");
                    return;
                }
                model.Point = RequestTool.RequestDecimal("Point", 0);
                model.Type_id_PointStatus = RequestTool.RequestInt("Type_id_PointStatus", 0);
                model.Admin_UserName = CurrentAdmin.UserName;
                model.Admin_id = CurrentAdmin.id;
                model.Remark = RequestTool.RequestString("Remark");
                model.Time_Update = DateTime.Now;
                B_Lebi_User_Point.Update(model);
                Point.UpdateUserPoint(user);
            }
            else
            {
                if (!EX_Admin.Power("user_point_add", "添加会员积分"))
                {
                    AjaxNoPower();
                    return;
                }
                string Mode = RequestTool.RequestString("mode");
                string User_Name_To = RequestTool.RequestString("User_Name_To");
                string UserLevel_ids = RequestTool.RequestString("UserLevel_ids");
                string User_ids = RequestTool.RequestString("User_ids");
                string UserName_ids = RequestTool.RequestString("UserName_ids");

                if (Mode == "user")
                {
                    Lebi_User user = B_Lebi_User.GetModel("UserName = lbsql{'" + User_Name_To + "'}");
                    if (user == null)
                    {
                        Response.Write("{\"msg\":\"" + Tag("会员账号不存在") + "\"}");
                        return;
                    }
                    Log.Add("添加会员积分", "User_Point", id.ToString(), CurrentAdmin, User_Name_To + " ->" + RequestTool.RequestDecimal("Point", 0));
                    Point.AddPoint(user, RequestTool.RequestDecimal("Point", 0), RequestTool.RequestInt("Type_id_PointStatus", 0), CurrentAdmin, RequestTool.RequestSafeString("Remark"));
                }
                else
                {
                    string where = "";
                    int i = 0;
                    if (User_ids == "")
                    {
                        Response.Write("{\"msg\":\"" + Tag("没有选择任何数据") + "\"}");
                        return;
                        where = "1=1 " + su.SQL;
                        Log.Add("添加会员积分", "User_Point", "", CurrentAdmin, su.Description + " ->" + RequestTool.RequestDecimal("Point", 0));
                    }
                    else
                    {
                        where = "id in (" + User_ids + ")";
                        Log.Add("添加会员积分", "User_Point", "", CurrentAdmin, UserName_ids + " ->" + RequestTool.RequestDecimal("Point", 0));
                    }
                    List<Lebi_User> modellist = B_Lebi_User.GetList(where, "");
                    foreach (Lebi_User user in modellist)
                    {
                        Point.AddPoint(user, RequestTool.RequestDecimal("Point", 0), RequestTool.RequestInt("Type_id_PointStatus", 0), CurrentAdmin, RequestTool.RequestSafeString("Remark"));
                    }
                }
            }
            string result = "{\"msg\":\"OK\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 编辑会员资金
        /// </summary>
        public void UserMoney_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            SearchUser su = new SearchUser(CurrentAdmin, CurrentLanguage.Code);
            decimal money = RequestTool.RequestDecimal("Money", 0);
            int Type_id_MoneyType = RequestTool.RequestInt("Type_id_MoneyType", 0);
            string Remark = RequestTool.RequestString("Remark");
            if (id > 0)
            {
                if (!EX_Admin.Power("user_money_edit", "编辑会员资金"))
                {
                    AjaxNoPower();
                    return;
                }
                Lebi_User_Money model = B_Lebi_User_Money.GetModel(id);
                Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                Log.Add("编辑会员资金", "User_Money", id.ToString(), CurrentAdmin, user.UserName);
                if (user == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("会员账号不存在") + "\"}");
                    return;
                }
                //model.Type_id_MoneyStatus = RequestTool.RequestInt("Type_id_MoneyStatus", 0);
                model.Type_id_MoneyType = Type_id_MoneyType;
                model.Admin_UserName = CurrentAdmin.UserName;
                model.Admin_id = CurrentAdmin.id;
                model.Remark = Remark;
                model.Time_Update = DateTime.Now;
                B_Lebi_User_Money.Update(model);
                //EX_User.UpdateUserMoney(user);
            }
            else
            {
                if (!EX_Admin.Power("user_money_add", "添加会员资金"))
                {
                    AjaxNoPower();
                    return;
                }
                string Mode = RequestTool.RequestString("mode");
                string User_Name_To = RequestTool.RequestString("User_Name_To");
                string UserLevel_ids = RequestTool.RequestString("UserLevel_ids");
                string User_ids = RequestTool.RequestString("User_ids");
                string UserName_ids = RequestTool.RequestString("UserName_ids");

                if (Mode == "user")
                {
                    Lebi_User user = B_Lebi_User.GetModel("UserName = lbsql{'" + User_Name_To + "'}");
                    if (user == null)
                    {
                        Response.Write("{\"msg\":\"" + Tag("会员账号不存在") + "\"}");
                        return;
                    }
                    Lebi_User_Money model = new Lebi_User_Money();
                    Log.Add("添加会员资金", "User_Money", id.ToString(), CurrentAdmin, User_Name_To + " ->" + RequestTool.RequestDecimal("Money", 0));
                    Money.AddMoney(user, money, Type_id_MoneyType, CurrentAdmin, "", Remark, false);

                }
                else
                {
                    string where = "";
                    if (User_ids == "")
                    {
                        Response.Write("{\"msg\":\"" + Tag("没有选择任何数据") + "\"}");
                        return;
                        where = "1=1 " + su.SQL;
                        Log.Add("添加会员资金", "User_Money", "", CurrentAdmin, su.Description + " ->" + RequestTool.RequestDecimal("Money", 0));
                    }
                    else
                    {
                        where = "id in (" + User_ids + ")";
                        Log.Add("添加会员资金", "User_Money", "", CurrentAdmin, UserName_ids + " ->" + RequestTool.RequestDecimal("Money", 0));
                    }
                    List<Lebi_User> modellist = B_Lebi_User.GetList(where, "");
                    foreach (Lebi_User user in modellist)
                    {
                        Money.AddMoney(user, money, Type_id_MoneyType, CurrentAdmin, "", Remark, false);

                    }
                }
            }
            string result = "{\"msg\":\"OK\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 编辑站内信类别
        /// </summary>
        public void Message_Type_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Message_Type model = B_Lebi_Message_Type.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Message_Type();
            }
            model = B_Lebi_Message_Type.SafeBindForm(model);
            model.Name = Language.RequestString("Name");
            model.Type_id_MessageTypeClass = 350;
            if (addflag)
            {
                if (!EX_Admin.Power("message_type_add", "添加类别"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Message_Type.Add(model);
                Log.Add("添加站内信类别", "Message_Type", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            else
            {
                if (!EX_Admin.Power("message_type_edit", "编辑类别"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Message_Type.Update(model);
                Log.Add("编辑站内信类别", "Message_Type", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除站内信类别
        /// </summary>
        public void Message_Type_Del()
        {
            if (!EX_Admin.Power("message_type_del", "删除类别"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Message_Type.Delete("Type_id_MessageTypeClass = 350 and id in (lbsql{" + id + "})");
            Log.Add("删除站内信类别", "Message_Type", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 发送站内信
        /// </summary>
        public void Message_Write()
        {
            if (!EX_Admin.Power("message_write", "发送站内信"))
            {
                AjaxNoPower();
                return;
            }
            int Mode = RequestTool.RequestInt("Mode", 0);
            int type_id = RequestTool.RequestInt("type_id", 0);
            string Title = Language.RequestString("Title");
            string Content = Language.RequestString("Content");
            string User_Name_To = RequestTool.RequestString("User_Name_To");
            //string UserLevel_ids = RequestTool.RequestString("UserLevel_ids");
            string User_ids = RequestTool.RequestString("User_ids");
            //string UserName_ids = RequestTool.RequestString("UserName_ids");
            SearchUser su = new SearchUser(CurrentAdmin, CurrentLanguage.Code);
            //if (User_ids != "")
            //    Mode = 1;
            //if (Mode == 0)
            //{
            //    Lebi_User user = B_Lebi_User.GetModel("UserName = '" + User_Name_To + "'");
            //    if (user == null)
            //    {
            //        Response.Write("{\"msg\":\"" + Tag("会员账号不存在") + "\"}");
            //        return;
            //    }
            //    Lebi_Message model = new Lebi_Message();
            //    model.Title = Shop.Bussiness.Language.Content(Title, user.Language);
            //    model.Content = Shop.Bussiness.Language.Content(Content, user.Language);
            //    model.User_id_From = 0;
            //    model.User_Name_From = "管理员";
            //    model.User_id_To = user.id;
            //    model.User_Name_To = User_Name_To;
            //    model.IsRead = 0;
            //    model.IsSystem = 0;
            //    model.Time_Add = System.DateTime.Now;
            //    model.Language = user.Language;
            //    model.Message_Type_id = type_id;
            //    model.IP = RequestTool.GetClientIP();
            //    B_Lebi_Message.Add(model);
            //    Log.Add("发送站内信-指定会员", "Message", "", CurrentAdmin, User_Name_To);
            //}
            //else
            //{
            //string gradename = "";
            string where = "";
            if (User_Name_To != "")
                where = "UserName = lbsql{'" + User_Name_To + "'}";
            else if (User_ids != "")
                where = "id in (lbsql{" + User_ids + "})";
            else
                where = "1=1 " + su.SQL;
            //int i = 0;
            List<Lebi_User> modellist = B_Lebi_User.GetList(where, "");
            if (modellist.Count == 0)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            foreach (Lebi_User user in modellist)
            {
                Lebi_Message model = new Lebi_Message();
                model.Title = Shop.Bussiness.Language.Content(Title, user.Language);
                model.Content = Shop.Bussiness.Language.Content(Content, user.Language);
                model.User_id_From = 0;
                model.User_Name_From = "管理员";
                model.User_id_To = user.id;
                model.User_Name_To = user.UserName;
                model.IsRead = 0;
                model.IsSystem = 0;
                model.Time_Add = System.DateTime.Now;
                model.Language = user.Language;
                model.Message_Type_id = type_id;
                model.IP = RequestTool.GetClientIP();
                model.Supplier_id = 0;
                B_Lebi_Message.Add(model);
            }
            Log.Add("发送站内信", "Message", "", CurrentAdmin, su.Description);

            //}
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 回复站内信
        /// </summary>
        public void Message_Reply()
        {
            if (!EX_Admin.Power("message_reply", "回复站内信"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            string Title = RequestTool.RequestString("Title");
            string Content = RequestTool.RequestString("Content");
            Lebi_Message mes = B_Lebi_Message.GetModel(id);
            Lebi_Message model = new Lebi_Message();
            if (mes == null)
            {
                Response.Write("{\"msg\":\"" + Tag("回复信息不存在") + "\"}");
                return;
            }
            else
            {
                model.Message_Type_id = mes.Message_Type_id;
            }
            model.Title = Title;
            model.Content = Content;
            model.User_id_From = 0;
            model.User_Name_From = "管理员";
            model.User_id_To = mes.User_id_From;
            model.User_Name_To = mes.User_Name_From;
            model.IsRead = 0;
            model.IsSystem = 0;
            model.Time_Add = System.DateTime.Now;
            model.Language = mes.Language;
            model.IP = RequestTool.GetClientIP();
            model.Supplier_id = mes.Supplier_id;
            B_Lebi_Message.Add(model);
            Log.Add("回复站内信", "Message", "", CurrentAdmin, mes.User_Name_From);
            //发送短信
            SMS.SendSMS_messagereply(model);
            //APP推送
            APP.Push_messagereply(model);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除站内信
        /// </summary>
        public void Message_Del()
        {
            if (!EX_Admin.Power("message_del", "删除站内信"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Message.Delete("Supplier_id = 0 and id in (lbsql{" + id + "})");
            Log.Add("删除站内信", "Message", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 发放卡券
        /// </summary>
        public void SendCard()
        {
            if (!EX_Admin.Power("user_card_add", "发放卡券"))
            {
                AjaxNoPower();
                return;
            }

            string User_Name_To = RequestTool.RequestString("User_Name_To");
            string User_ids = RequestTool.RequestString("User_ids");
            int orderid = RequestTool.RequestInt("orderid", 0);
            Lebi_CardOrder co = B_Lebi_CardOrder.GetModel(orderid);
            if (co == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            SearchUser su = new SearchUser(CurrentAdmin, CurrentLanguage.Code);
            int count = B_Lebi_Card.Counts("CardOrder_id=" + orderid + " and Type_id_CardStatus=200 and User_id=0");
            string where = "";
            if (User_Name_To != "")
                where = "UserName = lbsql{'" + User_Name_To + "'}";
            else if (User_ids != "")
                where = "id in (lbsql{" + User_ids + "})";
            else
                where = "1=1 " + su.SQL;
            if (count < B_Lebi_User.Counts(where))
            {
                Response.Write("{\"msg\":\"" + Tag("卡券数量不足") + "\"}");
                return;
            }
            List<Lebi_User> users = B_Lebi_User.GetList(where, "");
            if (users.Count == 0)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            foreach (Lebi_User user in users)
            {
                Lebi_Card c = B_Lebi_Card.GetModel("CardOrder_id=" + orderid + " and Type_id_CardStatus=200 and User_id=0");
                if (c == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                    return;
                }
                c.User_id = user.id;
                c.Type_id_CardStatus = 201;//已发放
                c.User_UserName = user.UserName;
                B_Lebi_Card.Update(c);
            }
            Log.Add("发送" + EX_Type.TypeName(co.Type_id_CardType) + "", "card", "", CurrentAdmin, su.Description + User_Name_To + User_ids);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 发送手机短信
        /// </summary>
        public void SMS_Write()
        {
            if (!EX_Admin.Power("sms_write", "发送手机短信"))
            {
                AjaxNoPower();
                return;
            }
            int Mode = RequestTool.RequestInt("Mode", 0);
            string Content = RequestTool.RequestString("Content");
            string PhoneNO = RequestTool.RequestString("PhoneNO");
            string User_ids = RequestTool.RequestString("User_ids");
            SearchUser su = new SearchUser(CurrentAdmin, CurrentLanguage.Code);
            string where = "";
            if (PhoneNO != "")
            {
                SMS.SendSMS_custom(Content, PhoneNO);
            }
            else
            {
                if (User_ids != "")
                    where = "id in (lbsql{" + User_ids + "})";
                else
                    where = "1=1 " + su.SQL;
                //int i = 0;
                List<Lebi_User> modellist = B_Lebi_User.GetList(where, "");
                if (modellist.Count == 0)
                {
                    Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                    return;
                }
                foreach (Lebi_User user in modellist)
                {
                    SMS.SendSMS_custom(Content, user.MobilePhone);
                }
            }
            Log.Add("发送手机短信", "Message", "", CurrentAdmin, su.Description);

            //}
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑安全问题
        /// </summary>
        public void Question_Edit()
        {
            if (!EX_Admin.Power("user_edit", "编辑会员"))
            {
                AjaxNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
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
            Lebi_User_Answer model = B_Lebi_User_Answer.GetModel("User_id = " + id + "");
            if (model == null)
            {
                model = new Lebi_User_Answer();
                model.User_Question_id = Question_id1;
                model.Answer = Answer1;
                model.User_id = id;
                B_Lebi_User_Answer.Add(model);
                model.User_Question_id = Question_id2;
                model.Answer = Answer2;
                model.User_id = id;
                B_Lebi_User_Answer.Add(model);
                Log.Add("添加安全问题", "User_Answer", id.ToString(), CurrentAdmin, id.ToString());
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除安全问题
        /// </summary>
        public void Question_Del()
        {
            if (!EX_Admin.Power("user_edit", "编辑会员"))
            {
                AjaxNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            B_Lebi_User_Answer.Delete("User_id = " + id + "");
            Log.Add("删除安全问题", "User_Answer", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
    }
}