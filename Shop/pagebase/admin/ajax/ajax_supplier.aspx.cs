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
using System.IO;

namespace Shop.Admin.Ajax
{
    public partial class ajax_supplier : AdminAjaxBase
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
        /// 编辑等级分组
        /// </summary>
        public void Group_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Supplier_Group model = B_Lebi_Supplier_Group.GetModel(id);

            if (model == null)
            {
                model = new Lebi_Supplier_Group();
            }
            B_Lebi_Supplier_Group.BindForm(model);
            model.Supplier_Skin_ids = RequestTool.RequestString("Supplier_Skin_ids");
            model.Verified_ids = RequestTool.RequestString("Verified_ids");
            model.Name = Language.RequestString("Name");
            if (model.id == 0)
            {
                if (!EX_Admin.Power("supplier_group_add", "添加商家分组"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Supplier_Group.Add(model);
                id = B_Lebi_Supplier_Group.GetMaxId();
                Log.Add("添加商家分组", "Supplier_Group", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN"));
            }
            else
            {
                if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Supplier_Group.Update(model);
                Log.Add("编辑商家分组", "Supplier_Group", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN"));
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除会员分组
        /// </summary>
        public void Group_Del()
        {
            string id = RequestTool.RequestString("id");
            if (!EX_Admin.Power("supplier_group_del", "删除商家分组"))
            {
                AjaxNoPower();
                return;
            }
            if (id != "")
            {
                List<Lebi_Supplier_Group> models = B_Lebi_Supplier_Group.GetList("id in (lbsql{" + id + "})", "");
                foreach (Lebi_Supplier_Group model in models)
                {
                    int Level_id = 1;
                    Lebi_Supplier_Group tmodel = B_Lebi_Supplier_Group.GetModel("Grade>" + model.Grade + " order by Grade asc");
                    if (tmodel == null)
                    {
                        Level_id = 1;
                    }
                    else
                    {
                        Level_id = tmodel.id;
                    }
                    Common.ExecuteSql("Update Lebi_Supplier set Supplier_Group_id = " + Level_id + " where Supplier_Group_id = " + model.id + "");
                    //}->
                    B_Lebi_Supplier_Group.Delete(model.id);
                }
                Log.Add("删除商家分组", "Supplier_Group", id.ToString(), CurrentAdmin, id.ToString());
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        public void CheckUserName()
        {
            int id = RequestTool.RequestInt("id", 0);
            string UserName = RequestTool.RequestString("UserName");
            string where = "UserName=lbsql{'" + UserName + "'}";
            if (id > 0)
                where += " and id!=" + id + "";
            int count = B_Lebi_Supplier.Counts(where);
            if (count > 0)
                Response.Write("{\"msg\":\"NO\"}");
            else
                Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑商家
        /// </summary>
        public void User_Edit()
        {
            if (!EX_Admin.Power("supplier_user_edit", "编辑商家"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            string UserName = RequestTool.RequestString("UserName");
            int Level_id = RequestTool.RequestInt("Level_id", 0);
            int IsCash = RequestTool.RequestInt("IsCash", 0);
            int IsSupplierTransport = RequestTool.RequestInt("IsSupplierTransport", 0);
            Lebi_User user = new Lebi_User();
            if (UserName != "")
            {
                user = B_Lebi_User.GetModel("UserName=lbsql{'" + UserName + "'}");
                if (user == null)
                {
                    Response.Write("{\"msg\":\"帐号不存在\"}");
                    return;
                }
                string where = "User_id='" + user.id + "'";
                if (id > 0)
                    where += " and id!=" + id + "";
                int count = B_Lebi_Supplier.Counts(where);
                if (count > 0)
                {
                    Response.Write("{\"msg\":\"此帐号已注册\"}");
                    return;
                }
            }
            if (IsCash == 1 && IsSupplierTransport == 0)
            {
                Response.Write("{\"msg\":\"独立收款的商家必须独立发货\"}");
                return;
            }
            Lebi_Supplier model = B_Lebi_Supplier.GetModel(id);
            //model.IsCash = 0;
            //model.IsSupplierTransport = 0;
            if (model == null)
            {
                model = new Lebi_Supplier();
            }
            B_Lebi_Supplier.SafeBindForm(model);
            model.Name = Language.RequestString("Name");
            model.Description = Language.RequestString("Description");
            model.ClassName = Language.RequestString("ClassName");
            model.SEO_Title = Language.RequestString("SEO_Title");
            model.SEO_Keywords = Language.RequestString("SEO_Keywords");
            model.SEO_Description = Language.RequestString("SEO_Description");
            model.Supplier_Group_id = Level_id;
            if (model.id == 0)
            {
                Lebi_Supplier_Group group = B_Lebi_Supplier_Group.GetModel(Level_id);
                if (group != null)
                {
                    model.Supplier_Group_id = group.id;
                    model.Money_Service = group.ServicePrice;
                    model.Money_Margin = group.MarginPrice;
                    model.BillingDays = group.BillingDays;

                }
                model.User_id = user.id;
                model.UserName = user.UserName;
                B_Lebi_Supplier.Add(model);
                model.id = B_Lebi_Supplier.GetMaxId();
            }
            else
            {
                if (IsSupplierTransport != model.IsSupplierTransport)
                {
                    string sql = "update [Lebi_Order_Product] set IsSupplierTransport="+ model.IsSupplierTransport  + " where Supplier_id=" + id + "";
                    Common.ExecuteSql(sql);
                    string sql2 = "update [Lebi_Product] set IsSupplierTransport=" + model.IsSupplierTransport + " where Supplier_id=" + id + "";
                    Common.ExecuteSql(sql2);
                }
                user = B_Lebi_User.GetModel(model.User_id);
                B_Lebi_Supplier.Update(model);
            }
            if (model.Type_id_SupplierStatus == 442)
            {
                try
                {
                    Lebi_Supplier_User suser = B_Lebi_Supplier_User.GetModel("User_id=" + model.User_id + " and Supplier_id=" + model.id + "");
                    if (suser == null)
                    {
                        suser = new Lebi_Supplier_User();
                        suser.RemarkName = user.RealName;
                        if (suser.RemarkName == "")
                            suser.RemarkName = user.UserName;
                        suser.Supplier_id = model.id;
                        suser.User_id = model.User_id;
                        suser.Type_id_SupplierUserStatus = 9011;
                        B_Lebi_Supplier_User.Add(suser);

                    }
                    else
                    {
                        suser.Supplier_id = model.id;
                        suser.User_id = model.User_id;
                        suser.Type_id_SupplierUserStatus = 9011;
                        B_Lebi_Supplier_User.Update(suser);
                    }
                }
                catch
                { }
            }
            if (model.Domain != "")
            {
                ThemeUrl.CreateURLRewrite_shop();
            }
            if (model.Type_id_SupplierStatus == 444) //如果状态冻结 商品全部冻结 by lebi.kingdge 2015-02-09
            {
                string sql = "update [Lebi_Product] set Type_id_ProductStatus=103 where Supplier_id=" + id + "";
                Common.ExecuteSql(sql);
            }
            Log.Add("编辑商家信息", "Supplier_User", id.ToString(), CurrentAdmin, model.UserName);
            string result = "{\"msg\":\"OK\", \"id\":\"" + model.id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 编辑会员密码
        /// </summary>
        public void User_Edit_Password()
        {
            if (!EX_Admin.Power("supplier_user_edit", "编辑商家"))
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
            Lebi_Supplier model = B_Lebi_Supplier.GetModel(id);
            model.Password = PWD;
            B_Lebi_Supplier.Update(model);
            Log.Add("编辑商家密码", "Supplier_User", id.ToString(), CurrentAdmin, model.UserName);
            string result = "{\"msg\":\"OK\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除商家
        /// </summary>
        public void User_Del()
        {
            if (!EX_Admin.Power("supplier_user_del", "删除商家"))
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
            B_Lebi_Supplier.Delete("id in (lbsql{" + id + "})");
            List<Lebi_Order> modellist = B_Lebi_Order.GetList("Supplier_id in (lbsql{" + id + "})", "");
            foreach (Lebi_Order model in modellist)
            {
                B_Lebi_Order.Delete("id = " + model.id + "");
                B_Lebi_Order_Log.Delete("Order_id = " + model.id + "");
                B_Lebi_Order_Product.Delete("Order_id = " + model.id + "");
            }
            B_Lebi_Brand.Delete("Supplier_id in (lbsql{" + id + "})");
            B_Lebi_ProPerty.Delete("Supplier_id in (lbsql{" + id + "})");
            B_Lebi_ProPerty_Tag.Delete("Supplier_id in (lbsql{" + id + "})");
            B_Lebi_Cash.Delete("Supplier_id in (lbsql{" + id + "})");
            B_Lebi_Message.Delete("Supplier_id in (lbsql{" + id + "})");
            B_Lebi_Supplier_Money.Delete("Supplier_id in (lbsql{" + id + "})");
            B_Lebi_Supplier_Bank.Delete("Supplier_id in (lbsql{" + id + "})");
            B_Lebi_Supplier_BillType.Delete("Supplier_id in (lbsql{" + id + "})");
            B_Lebi_Supplier_ProductType.Delete("Supplier_id in (lbsql{" + id + "})");
            //B_Lebi_Supplier_Verified.Delete("Supplier_id in (lbsql{" + id + "})");
            B_Lebi_Supplier_Verified_Log.Delete("Supplier_id in (lbsql{" + id + "})");
            B_Lebi_Log.Delete("Supplier_id in (lbsql{" + id + "})");
            B_Lebi_ServicePanel.Delete("Supplier_id in (lbsql{" + id + "})");
            B_Lebi_ServicePanel_Group.Delete("Supplier_id in (lbsql{" + id + "})");
            Log.Add("删除商家", "Supplier_User", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑后台菜单
        /// </summary>
        public void Menu_Edit()
        {
            if (!EX_Admin.Power("supplier_menu_edit", "编辑菜单"))
            {
                EX_Admin.NoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            int pid = RequestTool.RequestInt("pid", 0);
            Lebi_Supplier_Menu model = B_Lebi_Supplier_Menu.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Supplier_Menu();
                B_Lebi_Supplier_Menu.BindForm(model);
                model.Isshow = 1;
                B_Lebi_Supplier_Menu.Add(model);
            }
            else
            {
                B_Lebi_Supplier_Menu.BindForm(model);
                B_Lebi_Supplier_Menu.Update(model);
            }
            ImageHelper.LebiImagesUsed(model.Image, "menu", id);
            string action = Tag("编辑菜单");
            Log.Add(action, "Supplier_Menu", id.ToString(), CurrentAdmin, model.Name);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除一个菜单
        /// </summary>
        public void Menu_Del()
        {
            if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Supplier_Menu model = B_Lebi_Supplier_Menu.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            else
            {
                if (model.IsSYS == 0)
                    B_Lebi_Supplier_Menu.Delete(id);
            }
            string action = Tag("删除菜单");
            Log.Add(action, "Supplier_Menu", id.ToString(), CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑权限-编辑过滤页面
        /// </summary>
        public void url_Edit()
        {
            if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int gid = RequestTool.RequestInt("gid", 0);
            string url = RequestTool.RequestString("Url");
            Lebi_Supplier_Group group = B_Lebi_Supplier_Group.GetModel(gid);
            if (url == "")
            {
                Response.Write("{\"msg\":\"" + Tag("地址不能为空") + "\"}");
                return;
            }
            if (group == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Lebi_Supplier_Power p = B_Lebi_Supplier_Power.GetModel(id);
            if (p == null)
            {
                p = B_Lebi_Supplier_Power.GetList("Admin_Group_id=" + group.id + " and Url=lbsql{'" + url + "'}", "").FirstOrDefault();
                if (p != null)
                {
                    Response.Write("{\"msg\":\"" + Tag("地址已经存在") + "\"}");
                    return;
                }
                p = new Lebi_Supplier_Power();
                p.Supplier_Group_id = group.id;
                p.Url = url;
                B_Lebi_Supplier_Power.Add(p);
                string action = Tag("添加过滤页面");
                string description = url;
                Log.Add(action, "Supplier_Group", p.id.ToString(), CurrentAdmin, description);
            }
            else
            {
                p.Url = url;
                B_Lebi_Supplier_Power.Update(p);
                string action = Tag("编辑过滤页面");
                string description = url;
                Log.Add(action, "Supplier_Group", p.id.ToString(), CurrentAdmin, description);
            }

            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑权限-删除过滤页面
        /// </summary>
        public void url_Del()
        {
            if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("uid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Supplier_Power.Delete("id in (lbsql{" + id + "})");
            string action = Tag("删除过滤页面");
            string description = id;
            Log.Add(action, "Supplier_Group", id, CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑权限
        /// </summary>
        public void limit_Edit()
        {
            if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Supplier_Limit model = B_Lebi_Supplier_Limit.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Supplier_Limit();
                model = B_Lebi_Supplier_Limit.BindForm(model);
                B_Lebi_Supplier_Limit.Add(model);
                string action = Tag("添加权限分组");
                string description = Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN");
                Log.Add(action, "Supplier_Group", model.id.ToString(), CurrentAdmin, description);
            }
            else
            {
                model = B_Lebi_Supplier_Limit.BindForm(model);
                B_Lebi_Supplier_Limit.Update(model);
                string action = Tag("编辑权限分组");
                string description = Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN");
                Log.Add(action, "Supplier_Group", model.id.ToString(), CurrentAdmin, description);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除一个权限代码
        /// </summary>
        public void limit_Del()
        {
            if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Supplier_Limit model = B_Lebi_Supplier_Limit.GetModel(id);
            string action = Tag("删除权限分组");
            string description = Shop.Bussiness.Language.Content(model.Name, "CN");
            Log.Add(action, "Supplier_Group", id.ToString(), CurrentAdmin, description);
            B_Lebi_Supplier_Limit.Delete(id);
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 保存权限
        /// </summary>
        public void SavePower()
        {
            if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestString("id");
            int gid = RequestTool.RequestInt("gid", 0);
            Lebi_Supplier_Group group = B_Lebi_Supplier_Group.GetModel(gid);
            if (group == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            B_Lebi_Supplier_Power.Delete("Supplier_Group_id=" + group.id + "");
            List<Lebi_Supplier_Limit> models = B_Lebi_Supplier_Limit.GetList("id in (lbsql{" + ids + "})", "");
            if (models != null)
            {
                Lebi_Supplier_Power p = new Lebi_Supplier_Power();
                foreach (Lebi_Supplier_Limit model in models)
                {
                    p.Supplier_Group_id = group.id;
                    p.Supplier_Limit_Code = model.Code;
                    p.Supplier_Limit_id = model.id;
                    B_Lebi_Supplier_Power.Add(p);
                }
            }
            string action = Tag("编辑权限");
            string description = Shop.Bussiness.Language.Content(group.Name, "CN");
            Log.Add(action, "Supplier_Group", gid.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑菜单
        /// </summary>
        public void SaveMenu()
        {
            if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestString("id");
            int gid = RequestTool.RequestInt("gid", 0);
            Lebi_Supplier_Group group = B_Lebi_Supplier_Group.GetModel(gid);
            if (group == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            group.Menu_ids = ids;
            B_Lebi_Supplier_Group.Update(group);
            string action = Tag("编辑菜单");
            string description = Shop.Bussiness.Language.Content(group.Name, "CN");
            Log.Add(action, "Supplier_Group", gid.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑桌面菜单
        /// </summary>
        public void SaveIndexMenu()
        {
            if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestString("id");
            int gid = RequestTool.RequestInt("gid", 0);
            Lebi_Supplier_Group group = B_Lebi_Supplier_Group.GetModel(gid);
            if (group == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            group.Menu_ids_index = ids;
            B_Lebi_Supplier_Group.Update(group);
            string action = Tag("编辑桌面菜单");
            string description = Shop.Bussiness.Language.Content(group.Name, "CN");
            Log.Add(action, "Supplier_Group", gid.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 编辑商家资金
        /// </summary>
        public void Money_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            SearchUser su = new SearchUser(CurrentAdmin, CurrentLanguage.Code);
            if (id > 0)
            {
                if (!EX_Admin.Power("supplier_money_edit", "编辑商家资金"))
                {
                    AjaxNoPower();
                    return;
                }
                Lebi_Supplier_Money model = B_Lebi_Supplier_Money.GetModel(id);
                Lebi_Supplier user = B_Lebi_Supplier.GetModel(model.Supplier_id);


                if (user == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("商家账号不存在") + "\"}");
                    return;
                }
                model.Type_id_MoneyStatus = RequestTool.RequestInt("Type_id_MoneyStatus", 0);
                model.Type_id_SupplierMoneyType = RequestTool.RequestInt("Type_id_SupplierMoneyType", 0);
                model.Admin_UserName = CurrentAdmin.UserName;
                model.Admin_id = CurrentAdmin.id;
                model.Remark = RequestTool.RequestString("Remark");
                model.Time_Update = DateTime.Now;
                B_Lebi_Supplier_Money.Update(model);
                EX_Supplier.UpdateUserMoney(user);
                Log.Add("编辑商家资金", "Supplier_Money", id.ToString(), CurrentAdmin, user.UserName);
            }
            else
            {
                if (!EX_Admin.Power("supplier_money_add", "添加商家资金"))
                {
                    AjaxNoPower();
                    return;
                }
                string Mode = RequestTool.RequestString("mode");
                string User_Name_To = RequestTool.RequestString("User_Name_To");
                string UserLevel_ids = RequestTool.RequestString("UserLevel_ids");
                string User_ids = RequestTool.RequestString("User_ids");
                string UserName_ids = RequestTool.RequestString("UserName_ids");
                Lebi_Supplier user = B_Lebi_Supplier.GetModel("UserName = lbsql{'" + User_Name_To + "'}");
                if (user == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("商家账号不存在") + "\"}");
                    return;
                }
                Lebi_Supplier_Money model = new Lebi_Supplier_Money();
                Log.Add("添加商家资金", "Supplier_Money", id.ToString(), CurrentAdmin, User_Name_To + " ->" + RequestTool.RequestDecimal("Money", 0));
                model.Money = RequestTool.RequestDecimal("Money", 0);
                model.Type_id_MoneyStatus = RequestTool.RequestInt("Type_id_MoneyStatus", 0);
                model.Type_id_SupplierMoneyType = RequestTool.RequestInt("Type_id_SupplierMoneyType", 0);
                model.Admin_UserName = CurrentAdmin.UserName;
                model.Admin_id = CurrentAdmin.id;
                model.Remark = RequestTool.RequestString("Remark");
                model.Time_Update = DateTime.Now;
                model.Supplier_id = user.id;
                model.User_UserName = user.UserName;
                model.Supplier_SubName = user.SubName;
                B_Lebi_Supplier_Money.Add(model);
                EX_Supplier.UpdateUserMoney(user);
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
            model = B_Lebi_Message_Type.BindForm(model);
            model.Name = Language.RequestString("Name");
            model.Type_id_MessageTypeClass = 351;
            if (addflag)
            {
                if (!EX_Admin.Power("supplier_message_type_add", "添加类别"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Message_Type.Add(model);
                Log.Add("添加站内信类别", "Message_Type", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            else
            {
                if (!EX_Admin.Power("supplier_message_type_edit", "编辑类别"))
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
            if (!EX_Admin.Power("supplier_message_type_del", "删除类别"))
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
            B_Lebi_Message_Type.Delete("Type_id_MessageTypeClass = 351 and id in (lbsql{" + id + "})");
            Log.Add("删除站内信类别", "Message_Type", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 发送站内信
        /// </summary>
        public void Message_Write()
        {
            if (!EX_Admin.Power("supplier_message_write", "发送站内信"))
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
            List<Lebi_Supplier> modellist = B_Lebi_Supplier.GetList(where, "");
            if (modellist.Count == 0)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            foreach (Lebi_Supplier user in modellist)
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
                model.Supplier_id = user.id;
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
            if (!EX_Admin.Power("supplier_message_reply", "回复站内信"))
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
            model.Supplier_id = mes.User_id_From;
            B_Lebi_Message.Add(model);
            Log.Add("回复站内信", "Message", "", CurrentAdmin, mes.User_Name_From);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除站内信
        /// </summary>
        public void Message_Del()
        {
            if (!EX_Admin.Power("supplier_message_del", "删除站内信"))
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
            B_Lebi_Message.Delete("Supplier_id > 0 and id in (lbsql{" + id + "})");
            Log.Add("删除站内信", "Message", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑身份验证项目
        /// </summary>
        public void Verified_Edit()
        {
            if (!EX_Admin.Power("supplier_verified", "身份验证"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Supplier_Verified model = B_Lebi_Supplier_Verified.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Supplier_Verified();
            }
            model = B_Lebi_Supplier_Verified.BindForm(model);
            model.Name = Language.RequestString("Name");
            if (addflag)
            {
                B_Lebi_Supplier_Verified.Add(model);
                Log.Add("添加身份验证", "Supplier_Verified", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            else
            {
                B_Lebi_Supplier_Verified.Update(model);
                Log.Add("编辑身份验证", "Supplier_Verified", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除身份验证项目
        /// </summary>
        public void Verified_Del()
        {
            if (!EX_Admin.Power("supplier_verified", "身份验证"))
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
            B_Lebi_Supplier_Verified.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除身份验证", "Supplier_Verified", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量更新身份验证状态
        /// </summary>
        public void Verified_Log_Update()
        {
            if (!EX_Admin.Power("supplier_verified", "身份验证"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestString("IDS");
            int user_id = RequestTool.RequestInt("user_id", 0);
            string user_name = RequestTool.RequestString("user_name");
            List<Lebi_Supplier_Verified_Log> models = B_Lebi_Supplier_Verified_Log.GetList("id in (lbsql{" + ids + "})", "id desc");
            foreach (Lebi_Supplier_Verified_Log model in models)
            {
                model.Type_id_SupplierVerifiedStatus = RequestTool.RequestInt("SupplierVerifiedStatus" + model.id, 0);
                B_Lebi_Supplier_Verified_Log.Update(model);
            }
            Log.Add("审核身份验证", "Supplier_Verified_Log", ids.ToString(), CurrentAdmin, user_name.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 头部菜单跳转
        /// </summary>
        /// <param name="pid"></param>
        public void MenuJump()
        {
            int pid = RequestTool.RequestInt("pid", 0);
            if (pid == 0)
            {
                ////前往快捷桌面菜单
                //if (EX_Admin.Power("admin_data", "系统桌面"))
                //{
                //    Response.Redirect(AdminMenuURL("default.aspx?desk=1"));
                //}
                if (CurrentAdminGroup.Menu_ids_index != "")
                {
                    List<Lebi_Supplier_Menu> ims = B_Lebi_Supplier_Menu.GetList("id in (" + CurrentAdminGroup.Menu_ids_index + ")", "Sort desc");
                    foreach (Lebi_Supplier_Menu im in ims)
                    {
                        if (im.URL.Contains("?"))
                        {
                            im.URL = im.URL + "&desk=1";
                        }
                        else
                        {
                            im.URL = im.URL + "?desk=1";
                        }
                        Response.Redirect(AdminMenuURL(im.URL));
                        return;
                    }
                }
            }
            string currentgroupmenu = "," + CurrentAdminGroup.Menu_ids + ",";
            List<Lebi_Supplier_Menu> ms = B_Lebi_Supplier_Menu.GetList("parentid=" + pid + "", "Sort desc");
            foreach (Lebi_Supplier_Menu m in ms)
            {
                if (CurrentAdmin.AdminType == "super")
                {
                    List<Lebi_Supplier_Menu> models = B_Lebi_Supplier_Menu.GetList("parentid=" + m.id + "", "Sort desc");
                    foreach (Lebi_Supplier_Menu model in models)
                    {
                        Response.Redirect(AdminMenuURL(model.URL));
                        return;
                    }
                }
                else
                {
                    if (currentgroupmenu.Contains("," + m.id + ","))
                    {
                        List<Lebi_Supplier_Menu> models = B_Lebi_Supplier_Menu.GetList("parentid=" + m.id + "", "Sort desc");
                        foreach (Lebi_Supplier_Menu model in models)
                        {
                            if (currentgroupmenu.Contains("," + model.id + ","))
                            {
                                Response.Redirect(AdminMenuURL(model.URL));
                                return;
                            }
                        }
                    }
                }
            }
            Response.Write(Tag("菜单设置错误"));
        }

        public string AdminMenuURL(string url)
        {
            if (url.IndexOf("http") != 0)
            {
                url = site.AdminPath + "/" + url;
                url = ThemeUrl.CheckURL(url);
            }
            return url;

        }
        /// <summary>
        /// 编辑、添加店铺模板
        /// </summary>
        public void Skin_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Supplier_Skin model = B_Lebi_Supplier_Skin.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Supplier_Skin();
            }
            B_Lebi_Supplier_Skin.BindForm(model);
            //if (model.IsPage == 1)
            //    model.Code = "";
            if (model.id == 0)
            {
                if (!EX_Admin.Power("supplier_skin_add", "添加店铺皮肤"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Supplier_Skin.Add(model);
                id = B_Lebi_Supplier_Skin.GetMaxId();
                Log.Add("添加店铺皮肤", "Supplier_Skin", id.ToString(), CurrentAdmin, model.Name);
            }
            else
            {
                if (!EX_Admin.Power("supplier_skin_edit", "编辑店铺皮肤"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Supplier_Skin.Update(model);
                Log.Add("编辑店铺皮肤", "Supplier_Skin", id.ToString(), CurrentAdmin, model.Name);
            }
            //生成页面
            string SkinContent = Request["Content"];

            string SkinPath = model.Path.TrimEnd('/') + "/index.html";

            GreatSkin(SkinPath, SkinContent);
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 生成页面文件
        /// </summary>
        /// <param name="FileName">生成路径</param>
        /// <param name="SkinContent">内容</param>
        private void GreatSkin(string FileName, string SkinContent)
        {
            FileName = ThemeUrl.GetFullPath(FileName);
            string PhysicsPath = HttpContext.Current.Server.MapPath(@"~/" + ThemeUrl.GetPath(FileName));
            if (!Directory.Exists(PhysicsPath))
            {
                Directory.CreateDirectory(PhysicsPath);
            }
            string PhysicsFileName = HttpContext.Current.Server.MapPath(FileName);
            if (System.IO.File.Exists(PhysicsFileName))
            {
                System.IO.File.Delete(PhysicsFileName);
            }
            HtmlEngine.Instance.WriteFile(PhysicsFileName, SkinContent);
        }
        public void Skin_Del()
        {
            if (!EX_Admin.Power("supplier_skin_del", "删除店铺皮肤"))
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
            B_Lebi_Supplier_Skin.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除店铺皮肤", "Supplier_Skin", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 供应商皮肤下拉框选项
        /// </summary>
        public void GetSupplierSkinOptions()
        {
            int gid = RequestTool.RequestInt("groupid");
            int id = RequestTool.RequestInt("id");
            string str = "<option value=\"0\">┌ " + Tag("默认皮肤") + "</option>";
            Lebi_Supplier_Group group = B_Lebi_Supplier_Group.GetModel(gid);
            if (group != null)
            {
                if (group.Supplier_Skin_ids != "")
                {

                    string sel = "";
                    List<Lebi_Supplier_Skin> models = B_Lebi_Supplier_Skin.GetList("id in (" + group.Supplier_Skin_ids + ")", "");
                    foreach (Lebi_Supplier_Skin model in models)
                    {
                        sel = "";
                        if (id == model.id)
                            sel = "selected";
                        str += "<option value=\"" + model.id + "\" " + sel + ">" + model.Name + "</option>";
                    }
                }
            }
            Response.Write(str);
        }
    }
}