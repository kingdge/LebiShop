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
    public partial class ajax_agent : AdminAjaxBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }
        public void AgentConfig_Set()
        {
            if (!EX_Admin.Power("agent_config", "代理-参数设置"))
            {
                AjaxNoPower();
                return;
            }
            B_BaseConfig dob = new B_BaseConfig();
            BaseConfig model = new BaseConfig();
            model.Angent_Commission = RequestTool.RequestString("Angent_Commission");
            model.Angent_Commission_require = RequestTool.RequestString("Angent_Commission_require");
            model.Angent1_Commission = RequestTool.RequestString("Angent1_Commission");
            model.Angent2_Commission = RequestTool.RequestString("Angent2_Commission");
            model.Angent3_Commission = RequestTool.RequestString("Angent3_Commission");
            model.IsUsedAgent = RequestTool.RequestString("IsUsedAgent");
            model.IsUsedAgent_Area = RequestTool.RequestString("IsUsedAgent_Area");
            model.IsUsedAgent_Product = RequestTool.RequestString("IsUsedAgent_Product");
            model.CommissionMoneyDays = RequestTool.RequestString("CommissionMoneyDays");
            model.AgentEndDays = RequestTool.RequestString("AgentEndDays");
            dob.SaveConfig(model);

            Log.Add("代理-参数设置", "Config", "", CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑会员分组推广佣金比例
        /// </summary>
        public void UserLevel_Edit()
        {
            if (!EX_Admin.Power("agent_config", "代理-参数设置"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Agent_UserLevel model = B_Lebi_Agent_UserLevel.GetModel("UserLevel_id = " + id + "");
            if (model == null)
            {
                model = new Lebi_Agent_UserLevel();
            }
            B_Lebi_Agent_UserLevel.SafeBindForm(model);
            if (model.id == 0)
            {
                model.UserLevel_id = id;
                B_Lebi_Agent_UserLevel.Add(model);
                Log.Add("添加会员分组推广佣金比例", "Agent_UserLevel", id.ToString(), CurrentAdmin, "");
            }
            else
            {
                B_Lebi_Agent_UserLevel.Update(model);
                Log.Add("编辑会员分组推广佣金比例", "Agent_UserLevel", id.ToString(), CurrentAdmin, "");
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 编辑会员推广佣金比例
        /// </summary>
        public void User_Edit()
        {
            if (!EX_Admin.Power("agent_config", "代理-参数设置"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Agent_User model = B_Lebi_Agent_User.GetModel("User_id = " + id + "");
            if (model == null)
            {
                model = new Lebi_Agent_User();
            }
            B_Lebi_Agent_User.SafeBindForm(model);
            if (model.id == 0)
            {
                model.User_id = id;
                B_Lebi_Agent_User.Add(model);
                Log.Add("添加会员推广佣金比例", "Agent_User", id.ToString(), CurrentAdmin, "");
            }
            else
            {
                B_Lebi_Agent_User.Update(model);
                Log.Add("编辑会员推广佣金比例", "Agent_User", id.ToString(), CurrentAdmin, "");
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 批量编辑会员推广佣金比例
        /// </summary>
        public void User_Update()
        {
            if (!EX_Admin.Power("agent_config", "代理-参数设置"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("id");
            List<Lebi_User> lists = B_Lebi_User.GetList("id in (lbsql{" + ids + "})", "");
            foreach (Lebi_User list in lists)
            {
                Lebi_Agent_User model = B_Lebi_Agent_User.GetModel("User_id = " + list.id + "");
                if (model == null)
                {
                    model = new Lebi_Agent_User();
                }
                B_Lebi_Agent_User.SafeBindForm(model);
                if (model.id == 0)
                {
                    model.User_id = list.id;
                    B_Lebi_Agent_User.Add(model);
                    Log.Add("添加会员推广佣金比例", "Agent_User", list.id.ToString(), CurrentAdmin, "");
                }
                else
                {
                    B_Lebi_Agent_User.Update(model);
                    Log.Add("编辑会员推广佣金比例", "Agent_User", list.id.ToString(), CurrentAdmin, "");
                }
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + ids + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 编辑区域代理
        /// </summary>
        public void AreaAgent_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            int Area_id = RequestTool.RequestInt("Area_id", 0);
            int PArea_id = RequestTool.RequestInt("PArea_id", 0);
            if (Area_id == 0)
                Area_id = PArea_id;
            Lebi_Agent_Area model = B_Lebi_Agent_Area.GetModel(id);
            int count = B_Lebi_Agent_Area.Counts("Area_id=" + Area_id + " and id!=" + id + "");
            if (count > 0)
            {
                Response.Write("{\"msg\":\"" + Tag("不能选择此区域") + "\"}");
                return;
            }
            if (model == null)
            {
                model = new Lebi_Agent_Area();
            }
            B_Lebi_Agent_Area.BindForm(model);
            model.Area_id = Area_id;
            if (model.id == 0)
            {
                //if (!EX_Admin.Power("supplier_group_add", "添加商家分组"))
                //{
                //    AjaxNoPower();
                //    return;
                //}
                B_Lebi_Agent_Area.Add(model);
                id = B_Lebi_Agent_Area.GetMaxId();
                Log.Add("添加代理区域", "Agent_Area", id.ToString(), CurrentAdmin, model.User_UserName + "[" + model.Area_id + "]");
            }
            else
            {
                //if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
                //{
                //    AjaxNoPower();
                //    return;
                //}
                B_Lebi_Agent_Area.Update(model);
                Log.Add("编辑代理区域", "Agent_Area", id.ToString(), CurrentAdmin, model.User_UserName + "[" + model.Area_id + "]");
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 批量添加代理地区
        /// </summary>
        public void AreaAgent_MutiAdd()
        {
            int id = RequestTool.RequestInt("id", 0);
            int Area_id = RequestTool.RequestInt("Area_id", 0);
            int PArea_id = RequestTool.RequestInt("PArea_id", 0);
            List<Lebi_Area> areas = B_Lebi_Area.GetList("Parentid=" + PArea_id + "", "");
            Lebi_Agent_Area model;
            foreach (Lebi_Area area in areas)
            {
                int count = B_Lebi_Agent_Area.Counts("Area_id=" + area.id + "");
                if (count > 0)
                {
                    continue;
                }
                model = new Lebi_Agent_Area();
                B_Lebi_Agent_Area.BindForm(model);
                model.Area_id = area.id;
                B_Lebi_Agent_Area.Add(model);
            }
            Log.Add("批量添加代理区域", "Agent_Area", id.ToString(), CurrentAdmin, "");
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }

        /// <summary>
        /// 编辑区域代理-代理人
        /// </summary>
        public void AreaAgentUser_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Agent_Area model = B_Lebi_Agent_Area.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            string User_UserName = RequestTool.RequestString("User_UserName");
            if (User_UserName == "")
            {
                Response.Write("{\"msg\":\"" + Tag("用户名不能空") + "\"}");
                return;
            }
            Lebi_User user = B_Lebi_User.GetModel("UserName=lbsql{'" + User_UserName + "'}");
            if (user == null)
            {
                Response.Write("{\"msg\":\"" + Tag("用户不存在") + "\"}");
                return;
            }

            if (model.Time_end > System.DateTime.Now.Date && model.User_id != user.id)
            {
                Response.Write("{\"msg\":\"" + Tag("其他代理还未到期") + "\"}");
                return;
            }
            if (model == null)
            {
                model = new Lebi_Agent_Area();
            }
            B_Lebi_Agent_Area.BindForm(model);
            model.User_id = user.id;
            B_Lebi_Agent_Area.Update(model);
            Log.Add("绑定区域代理", "Agent_Area", id.ToString(), CurrentAdmin, model.User_UserName + "[" + model.Area_id + "]");
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除区域代理
        /// </summary>
        public void AreaAgent_Del()
        {
            string ids = RequestTool.RequestString("ids");
            //if (!EX_Admin.Power("supplier_group_del", "删除商家分组"))
            //{
            //    AjaxNoPower();
            //    return;
            //}

            if (ids != "")
                B_Lebi_Agent_Area.Delete("id in (lbsql{" + ids + "})");
            Log.Add("删除区域代理", "Agent_Area", ids, CurrentAdmin, ids);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑商品代理级别
        /// </summary>
        public void ProductAgentLevel_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);

            Lebi_Agent_Product_Level model = B_Lebi_Agent_Product_Level.GetModel(id);
            string User_UserName = RequestTool.RequestString("User_UserName");

            if (model == null)
            {
                model = new Lebi_Agent_Product_Level();
            }
            B_Lebi_Agent_Product_Level.BindForm(model);
            model.Years = 1;
            if (model.id == 0)
            {
                //if (!EX_Admin.Power("supplier_group_add", "添加商家分组"))
                //{
                //    AjaxNoPower();
                //    return;
                //}
                B_Lebi_Agent_Product_Level.Add(model);
                id = B_Lebi_Agent_Product_Level.GetMaxId();
                Log.Add("添加商品代理级别", "Agent_ProductLevel", id.ToString(), CurrentAdmin);
            }
            else
            {
                //if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
                //{
                //    AjaxNoPower();
                //    return;
                //}
                B_Lebi_Agent_Product_Level.Update(model);
                Log.Add("编辑商品代理级别", "Agent_ProductLevel", id.ToString(), CurrentAdmin);
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除商品代理级别
        /// </summary>
        public void ProductAgentLevel_Del()
        {
            string ids = RequestTool.RequestString("ids");
            //if (!EX_Admin.Power("supplier_group_del", "删除商家分组"))
            //{
            //    AjaxNoPower();
            //    return;
            //}

            if (ids != "")
                B_Lebi_Agent_Product_Level.Delete("id in (lbsql{" + ids + "})");
            Log.Add("删除商品代理级别", "Agent_ProductLevel", ids, CurrentAdmin, ids);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑商品代理
        /// </summary>
        public void ProductAgentUser_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Agent_Product_User model = B_Lebi_Agent_Product_User.GetModel(id);
            string User_UserName = RequestTool.RequestString("User_UserName");
            if (User_UserName == "")
            {
                Response.Write("{\"msg\":\"" + Tag("用户名不能空") + "\"}");
                return;
            }
            Lebi_User user = B_Lebi_User.GetModel("UserName=lbsql{'" + User_UserName + "'}");
            if (user == null)
            {
                Response.Write("{\"msg\":\"" + Tag("用户不存在") + "\"}");
                return;
            }
            int count = B_Lebi_Agent_Product_User.Counts("User_id=" + user.id + " and id!=" + id + "");
            if (count > 0)
            {
                Response.Write("{\"msg\":\"" + Tag("用户已存在") + "\"}");
                return;
            }
            if (model == null)
            {
                model = new Lebi_Agent_Product_User();
            }
            B_Lebi_Agent_Product_User.BindForm(model);
            model.User_id = user.id;
            if (model.id == 0)
            {
                //if (!EX_Admin.Power("supplier_group_add", "添加商家分组"))
                //{
                //    AjaxNoPower();
                //    return;
                //}
                B_Lebi_Agent_Product_User.Add(model);
                id = B_Lebi_Agent_Product_User.GetMaxId();
                Log.Add("添加商品代理", "Agent_ProductUser", id.ToString(), CurrentAdmin);
            }
            else
            {
                //if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
                //{
                //    AjaxNoPower();
                //    return;
                //}
                B_Lebi_Agent_Product_User.Update(model);
                //Common.ExecuteSql("update Lebi_Agent_Product set Commission=" + model.Commission + " where User_id=" + model.User_id + "");
                Log.Add("编辑商品代理", "Agent_ProductUser", id.ToString(), CurrentAdmin);
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除商品代理
        /// </summary>
        public void ProductAgentUser_Del()
        {
            string ids = RequestTool.RequestString("ids");
            //if (!EX_Admin.Power("supplier_group_del", "删除商家分组"))
            //{
            //    AjaxNoPower();
            //    return;
            //}
            if (ids != "")
            {
                B_Lebi_Agent_Product_User.Delete("id in (lbsql{" + ids + "})");
                //取消商品代理绑定
                Common.ExecuteSql("update Lebi_Agent_Product set User_id=0,User_UserName='',Commission=0 where User_id in (lbsql{" + ids + "})");
            }
            Log.Add("删除商品代理级别", "Agent_ProductUser", ids, CurrentAdmin, ids);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 添加代理商品
        /// </summary>
        public void Product_Add()
        {
            SearchProduct sp = new SearchProduct(CurrentAdmin, CurrentLanguage.Code);
            List<Lebi_Product> models = B_Lebi_Product.GetList("Product_id=0 " + sp.SQL, "");
            if (models.Count == 0)
            {
                Response.Write("{\"msg\":\"" + Tag("没有商品") + "\"}");
                return;
            }
            Lebi_Agent_Product pro;
            string ids = "";
            foreach (Lebi_Product model in models)
            {
                pro = B_Lebi_Agent_Product.GetModel("Product_id=" + model.id + "");
                if (pro == null)
                {
                    pro = new Lebi_Agent_Product();
                    pro.Product_id = model.id;
                    B_Lebi_Agent_Product.Add(pro);
                    ids += pro.id + ",";
                }
            }
            Log.Add("添加代理区商品", "Agent_Product", ids, CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除代理区商品
        /// </summary>
        public void Product_Del()
        {
            string ids = RequestTool.RequestString("productid");
            //if (!EX_Admin.Power("supplier_group_del", "删除商家分组"))
            //{
            //    AjaxNoPower();
            //    return;
            //}

            if (ids != "")
                B_Lebi_Agent_Product.Delete("id in (lbsql{" + ids + "})");
            Log.Add("删除代理区商品", "Agent_Product", ids, CurrentAdmin, ids);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除商品代理申请
        /// </summary>
        public void ProductRequest_Del()
        {
            string ids = RequestTool.RequestString("productid");
            if (ids != "")
                B_Lebi_Agent_Product_request.Delete("id in (lbsql{" + ids + "})");
            Log.Add("删除代理商品申请", "Agent_Product", ids, CurrentAdmin, ids);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 处理商品代理申请
        /// </summary>
        public void ProductRequest_Manage()
        {
            int id = RequestTool.RequestInt("id");
            int t = RequestTool.RequestInt("t");
            Lebi_Agent_Product_request model = B_Lebi_Agent_Product_request.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Lebi_Agent_Product pro = B_Lebi_Agent_Product.GetModel("Product_id=" + model.Product_id);
            if (pro == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Lebi_Agent_Product_User user = B_Lebi_Agent_Product_User.GetModel("User_id=" + model.User_id + "");
            if (user == null)
            {
                Response.Write("{\"msg\":\"" + Tag("此用户无代理资格") + "\"}");
                return;
            }
            if (pro.User_id > 0)
            {
                Response.Write("{\"msg\":\"" + Tag("其他用户已代理此商品") + "\"}");
                return;
            }
            if (t == 1)
            {
                if (model.Product_id_old > 0)//替换商品的情况。修改旧数据
                {
                    Lebi_Agent_Product oldpro = B_Lebi_Agent_Product.GetModel("Product_id=" + model.Product_id_old + "");
                    if (oldpro != null)
                    {
                        if (oldpro.User_id != model.User_id)
                        {
                            Response.Write("{\"msg\":\"" + Tag("提换商品非此用户代理") + "\"}");
                            return;
                        }
                        oldpro.User_id = 0;
                        oldpro.User_UserName = "";
                        B_Lebi_Agent_Product.Update(oldpro);
                    }
                }
                model.Type_id_AgentProductRequestStatus = 371;
                pro.User_id = model.User_id;
                pro.User_UserName = model.User_UserName;
                //pro.Commission = user.Commission;
                B_Lebi_Agent_Product.Update(pro);
            }
            else
            {
                model.Type_id_AgentProductRequestStatus = 372;
            }
            model.Admin_id = CurrentAdmin.id;
            model.Admin_UserName = CurrentAdmin.UserName;
            B_Lebi_Agent_Product_request.Update(model);
            Log.Add("处理代理商品申请", "Agent_Product", id.ToString(), CurrentAdmin);
            Response.Write("{\"msg\":\"OK\"}");
        }
    }
}