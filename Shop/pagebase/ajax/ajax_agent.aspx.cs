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
using System.Collections.Specialized;


namespace Shop.Ajax
{
    /// 这个文件放置
    /// 登录前进行的动作
    public partial class Ajax_agent : ShopPage
    {

        public void LoadPage()
        {
            if (!AjaxLoadCheck())
            {
                return;
            }
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }
        /// <summary>
        /// 检查用户名是否注册
        /// </summary>
        public void CheckUserName()
        {
            string UserName = RequestTool.RequestString("UserName");
            int count = B_Lebi_Supplier.Counts("UserName=lbsql{'" + UserName + "'}");
            if (count > 0)
                Response.Write("NO");
            else
                Response.Write("OK");
        }
        /// <summary>
        /// 用户这注册
        /// </summary>
        public void User_Reg()
        {
            string url = "";
            string verifycode = RequestTool.RequestString("verifycode");
            if (CurrentCheckCode != verifycode)
            {
                Response.Write("{\"msg\":\"" + Tag("验证码错误") + "\"}");
                return;
            }
            string UserName = RequestTool.RequestSafeString("UserName");
            string PWD = RequestTool.RequestSafeString("Password");
            //检查用户名存在
            int count = B_Lebi_Supplier.Counts("UserName=lbsql{'" + UserName + "'}");
            if (count > 0)
            {
                Response.Write("{\"msg\":\"" + Tag("用户名已注册") + "\"}");
                return;
            }
            Lebi_Supplier model = new Lebi_Supplier();
            B_Lebi_Supplier.SafeBindForm(model);
            model.Password = EX_User.MD5(PWD);
            model.Language = CurrentLanguage.Code;
            model.Time_Reg = DateTime.Now;
            model.Time_This = DateTime.Now;
            model.Time_Last = DateTime.Now;
            model.Count_Login = 1;
            model.IP_This = RequestTool.GetClientIP();
            model.IP_Last = RequestTool.GetClientIP();
            B_Lebi_Supplier.Add(model);
            //登录
            EX_User.UserLogin(UserName, model.Password, GetDT(), true);
            ////发送邮件
            //if (ShopCache.GetBaseConfig().MailSign.ToLower().Contains("zhuce"))
            //{
            //    Lebi_User user = B_Lebi_User.GetModel(B_Lebi_User.GetMaxId());
            //    Email.SendEmail_newuser(user);
            //}
            //url = RequestTool.RequestString("url").Replace("<", "").Replace(">", "");
            //if (url.ToLower().IndexOf("http") > -1 || url.ToLower().IndexOf(URL("P_Register", "").ToLower()) > -1 || url.ToLower().IndexOf(URL("P_Login", "").ToLower()) > -1 || url == "")
            //{
            //    url = URL("P_Index", "");
            //}
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 请求代理一个商品
        /// </summary>
        public void Product_Request()
        {
            int id = RequestTool.RequestInt("id");
            int oldid = RequestTool.RequestInt("oldid");
            Lebi_Agent_Product apro = B_Lebi_Agent_Product.GetModel(id);
            if (apro == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            if (apro.User_id > 0)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Lebi_Agent_Product_User user = B_Lebi_Agent_Product_User.GetModel("User_id=" + CurrentUser.id + "");
            if (user == null)
                user = new Lebi_Agent_Product_User();
            if (user.Count_product_change - user.Count_product_change_used < 1)
            {
                //验证是否超过可用的修改次数
                Response.Write("{\"msg\":\"" + Tag("无权修改") + "\"}");
                return;
            }
            if (oldid == 0 && B_Lebi_Agent_Product.Counts("User_id=" + CurrentUser.id + "") >= user.Count_Product)
            {
                //验证是否超过代理商品的上限
                Response.Write("{\"msg\":\"" + Tag("不能申请更多商品") + "\"}");
                return;
            }
            Lebi_Agent_Product_request model = B_Lebi_Agent_Product_request.GetModel("User_id=" + CurrentUser.id + " and Product_id=" + apro.Product_id + " and Type_id_AgentProductRequestStatus=370");
            if (model != null)
            {
                Response.Write("{\"msg\":\"" + Tag("不能重复申请") + "\"}");
                return;
            }
            model = new Lebi_Agent_Product_request();
            model.Product_id = apro.Product_id;
            model.Type_id_AgentProductRequestStatus = 370;
            model.User_id = CurrentUser.id;
            model.User_UserName = CurrentUser.UserName;
            model.Product_id_old = oldid;
            B_Lebi_Agent_Product_request.Add(model);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 佣金提现，-转现金账户
        /// </summary>
        public void TackAgentMoney()
        {
            int d = 0;
            int.TryParse(SYS.CommissionMoneyDays,out d);
            d = 0 - d;
            decimal money = 0;
            string money_ = Common.GetValue("select sum(Money) from Lebi_Agent_Money where User_id=" + CurrentUser.id + " and Type_id_AgentMoneyStatus=382 and Time_add<'" + System.DateTime.Now.AddDays(d) + "')");
            decimal.TryParse(money_, out money);
            if (money > 0)
            {
                Money.AddMoney(CurrentUser, money, 191, null, "", "");
                Response.Write("{\"msg\":\"OK\"}");
            }
            string sql = "update Lebi_Agent_Money set Type_id_AgentMoneyStatus=384 where User_id=" + CurrentUser.id + " and Type_id_AgentMoneyStatus=382 and Time_add<'" + System.DateTime.Now.AddDays(d) + "')";
            Common.ExecuteSql(sql);
            Response.Write("{\"msg\":\"" + Tag("金额不能为0") + "\"}");
            return;

        }
    }
}