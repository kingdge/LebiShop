using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Collections.Specialized;
using System.Web;
using System.Security.Cryptography;

namespace Shop.Bussiness
{
    public delegate void SupplierRegisterEventHandler(Lebi_Supplier user);
    public class EX_Supplier
    {
        public static event SupplierRegisterEventHandler SupplierRegisterEvent;
        public static void SupplierRegister(Lebi_Supplier user)
        {
            if (SupplierRegisterEvent != null)
            {
                SupplierRegisterEvent(user);
            }
        }
        #region 管理员权限
        public static bool Power(string code, string name, string powercodes = "")
        {
            if (RequestTool.GetConfigKey("DemoSite").Trim() == "1")
            {
                return false;
            }
            if (powercodes.Contains("'" + code + "'"))
                return true;
            else
            {
                Lebi_Supplier_Limit limit = B_Lebi_Supplier_Limit.GetModel("Code='" + code + "'");
                if (limit == null)
                {
                    limit = B_Lebi_Supplier_Limit.GetModel("Code='default'");
                    if (limit == null)
                    {
                        limit = new Lebi_Supplier_Limit();
                        limit.parentid = 0;
                        limit.id = 1;
                        limit.Code = "default";
                        limit.Name = "未分组";
                        B_Lebi_Supplier_Limit.Add(limit);
                        limit.id = B_Lebi_Supplier_Limit.GetMaxId();
                    }
                    limit.Code = code;
                    limit.Name = name;
                    limit.parentid = limit.id;
                    B_Lebi_Supplier_Limit.Add(limit);
                }
            }
            return false;
        }
        //public static bool CheckPower(string code, string name)
        //{
        //    if (!Power(code, name))
        //    {
        //        NoPower();
        //        return false;
        //    }
        //    return true;
        //}
        //public static void NoPower()
        //{
        //    HttpContext.Current.Response.Write("{\"msg\":\"权限不足\"}");
        //    HttpContext.Current.Response.End();
        //}
        #endregion

        /// <summary>
        /// 供应商账号登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="type">账号类型</param>
        /// <param name="supplierid"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool Login(Lebi_User user, string type, int supplierid, out string msg, int adminlogin = 0)
        {
            msg = "OK";
            Lebi_Supplier_User supplieruser = null;
            Lebi_Supplier supplier = null;
            if (supplierid == 0)
            {
                string supplierid_ = CookieTool.GetCookieString("supplier");
                //int supplierid = 0;
                int.TryParse(supplierid_, out supplierid);
            }
            //string and = "";
            //if (type != "")
            //{
            //    and = " and Supplier_id in (select id from [Lebi_Supplier] where Supplier_Group_id in (select id from [Lebi_Supplier_Group] where type='" + type + "'))";
            //}
            if (supplierid > 0)
            {
                supplier = B_Lebi_Supplier.GetModel(supplierid);
                if (supplier != null)
                {
                    Lebi_Supplier_Group group = B_Lebi_Supplier_Group.GetModel(supplier.Supplier_Group_id);
                    if (group.type == type || type == "")
                        supplieruser = B_Lebi_Supplier_User.GetModel("User_id = " + user.id + " and Supplier_id=" + supplierid + " and Type_id_SupplierUserStatus=9011");
                }
            }
            if (supplieruser == null)
            {
                List<Lebi_Supplier_User> users = B_Lebi_Supplier_User.GetList("User_id = " + user.id + " and Type_id_SupplierUserStatus=9011", "");
                if (type == "")
                {
                    foreach (Lebi_Supplier_User u in users)
                    {
                        supplier = B_Lebi_Supplier.GetModel(u.Supplier_id);
                        if (supplier != null)
                        {
                            supplieruser = u;
                            break;
                        }
                    }
                    //supplieruser = users.FirstOrDefault();
                    //if (supplieruser == null)
                    //{
                    //    msg = "User_id = " + user.id + " and Type_id_SupplierUserStatus=9011";
                    //    return false;
                    //}
                    //supplier = B_Lebi_Supplier.GetModel(supplieruser.Supplier_id);
                }
                else
                {
                    foreach (Lebi_Supplier_User u in users)
                    {
                        supplier = B_Lebi_Supplier.GetModel(u.Supplier_id);
                        if (supplier == null)
                            continue;
                        Lebi_Supplier_Group group = B_Lebi_Supplier_Group.GetModel(supplier.Supplier_Group_id);
                        if (group.type == type)
                        {
                            supplieruser = u;
                            break;
                        }
                    }
                }
            }
            if (supplieruser == null)
            {
                Log.Add("登陆失败", "Login", "", supplier, "账号：" + user.UserName);
                msg = "登陆失败";
                return false;
            }
            if (supplier == null)
            {
                Log.Add("登陆失败:账号不存在或不可用", "Login", "", supplier, "账号：" + user.UserName);
                msg = "账号不存在或不可用";
                return false;
            }
            if (supplier.Type_id_SupplierStatus != 442)
            {
                Log.Add("登陆失败:供应商账号不可用", "Login", "", supplier, "账号：" + user.UserName);
                msg = "供应商账号不可用";
                return false;
            }
            supplier.Time_Last = supplier.Time_This;
            supplier.Time_This = DateTime.Now;
            supplier.Count_Login++;
            supplier.IP_Last = supplier.IP_This;
            supplier.IP_This = RequestTool.GetClientIP();
            LB.Tools.CookieTool.SetCookieString("supplier", supplier.id.ToString(), 60 * 24);
            B_Lebi_Supplier.Update(supplier);
            if (adminlogin == 0)
                Log.Add("登陆系统", "Login", "", supplier, "账号：" + user.UserName);
            return true;
        }

        /// <summary>
        /// 当前供应商用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Lebi_Supplier_User CurrentSupplierUser(Lebi_User user)
        {
            Lebi_Supplier_User model = null;
            string supplierid_ = CookieTool.GetCookieString("supplier");
            int supplierid = 0;
            int.TryParse(supplierid_, out supplierid);
            if (supplierid > 0)
                model = B_Lebi_Supplier_User.GetModel("User_id = " + user.id + " and Supplier_id=" + supplierid + " and Type_id_SupplierUserStatus=9011");
            if (model == null)
            {
                model = B_Lebi_Supplier_User.GetList("User_id = " + user.id + " and Type_id_SupplierUserStatus=9011", "").FirstOrDefault();
            }
            if (model == null)
                model = new Lebi_Supplier_User();
            return model;
        }
        /// <summary>
        /// 返回分组下会员数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UserCount(int id)
        {
            int sum = B_Lebi_Supplier.Counts("Supplier_Group_id in (select g.id from Lebi_Supplier_Group as g where g.type='supplier' and g.id = "+ id +")");
            return Convert.ToInt32(sum);
        }
        public static string LevelOption(string type, int level_id, string lang)
        {
            if (type == "")
                type = "supplier";
            List<Lebi_Supplier_Group> levels = B_Lebi_Supplier_Group.GetList("type='" + type + "'", "Grade asc");
            string str = "";
            foreach (Lebi_Supplier_Group level in levels)
            {
                string sel = "";
                if (level_id == level.id)
                    sel = "selected";
                str += "<option value=\"" + level.id + "\" " + sel + ">" + Shop.Bussiness.Language.Content(level.Name, lang) + "</option>";
            }
            return str;
        }
        public static string GroupRadio(string where, string name, string id, string ext, string lang)
        {
            List<Lebi_Supplier_Group> models = B_Lebi_Supplier_Group.GetList(where, "Grade asc");
            string str = "";
            foreach (Lebi_Supplier_Group model in models)
            {
                string sel = "";
                if (("," + id + ",").Contains("," + model.id + ","))
                    sel = "checked";
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-checkbox m-r-20\"><input type=\"checkbox\" id=\"" + name + "" + model.id + "\" name=\"" + name + "\" value=\"" + model.id + "\" class=\"custom-control-input\" " + sel + " " + ext + "/><span class=\"custom-control-label\">" + Shop.Bussiness.Language.Content(model.Name, lang) + "</span></label>";
                }
                else
                {
                    str += "<label><input type=\"radio\" name=\"" + name + "\" value=\"" + model.id + "\" " + sel + " " + ext + "/>" + Shop.Bussiness.Language.Content(model.Name, lang) + "&nbsp;</label>";
                }
            }
            return str;

        }
        public static string GroupRadio(string name, string id, string ext, string lang)
        {
            List<Lebi_Supplier_Group> models = B_Lebi_Supplier_Group.GetList("", "Grade asc");
            string str = "";
            foreach (Lebi_Supplier_Group model in models)
            {
                string sel = "";
                if (("," + id + ",").Contains("," + model.id + ","))
                    sel = "checked";
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-radio m-r-20\"><input type=\"radio\" id=\"" + name + "" + model.id + "\" name=\"" + name + "\" value=\"" + model.id + "\" class=\"custom-control-input\" " + sel + " " + ext + "/><span class=\"custom-control-label\">" + Shop.Bussiness.Language.Content(model.Name, lang) + "</span></label>";
                }
                else
                {
                    str += "<label><input type=\"radio\" name=\"" + name + "\" id=\"" + name + "" + model.id + "\" value=\"" + model.id + "\" " + sel + " " + ext + "/>" + Shop.Bussiness.Language.Content(model.Name, lang) + "&nbsp;</label>";
                }
            }
            return str;

        }
        /// <summary>
        /// 计算订单指定日期及状态的可用金额
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static decimal GetMoney(int user_id, string dateFrom, string status)
        {
            decimal money = 0;
            string where = "Supplier_id = " + user_id + "";
            if (dateFrom != "")
            {
                where = "Time_Add<='" + dateFrom + "'";
            }
            if (status != "")
            {
                if (dateFrom != "")
                {
                    where += " and ";
                }
                where += status;
            }
            List<Lebi_Supplier_Money> models = B_Lebi_Supplier_Money.GetList(where, "");
            foreach (Lebi_Supplier_Money model in models)
            {
                money = money + model.Money;
            }
            return money;
        }
        //获取提现账户信息
        public static string GetBank(int user_id, int id)
        {
            string ret = "";
            Lebi_Supplier_Bank model = B_Lebi_Supplier_Bank.GetModel("user_id = " + user_id + " and id = " + id + "");
            if (model != null)
            {
                ret = model.Name + "，" + model.Code + "，" + model.UserName;
            }
            return ret;
        }
        /// <summary>
        /// 更新商家资金
        /// </summary>
        /// <param name="user"></param>
        public static void UpdateUserMoney(Lebi_Supplier user)
        {
            string money = B_Lebi_Supplier_Money.GetValue("sum(Money)", "Supplier_id=" + user.id + " and Type_id_MoneyStatus=181");
            decimal Money = 0;
            Decimal.TryParse(money, out Money);
            user.Money = Money;
            B_Lebi_Supplier.Update(user);
        }
        //获取商家信息
        public static Lebi_Supplier GetUser(int id)
        {
            Lebi_Supplier model = B_Lebi_Supplier.GetModel("id = " + id);
            if (model == null)
            {
                model = new Lebi_Supplier();
                return model;
            }
            return model;
        }
        //获取商家分组名称
        public static Lebi_Supplier_Group GetGroup(int id)
        {
            Lebi_Supplier_Group model = B_Lebi_Supplier_Group.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Supplier_Group();
                return model;
            }
            return model;
        }
        //获取商家身份验证项目
        public static Lebi_Supplier_Verified_Log GetVerified_Log(int id, int user_id)
        {
            Lebi_Supplier_Verified_Log model = B_Lebi_Supplier_Verified_Log.GetModel("Verified_id =" + id + " and Supplier_id = " + user_id + "");
            if (model == null)
            {
                model = new Lebi_Supplier_Verified_Log();
                return model;
            }
            return model;
        }
        /// <summary>
        /// 商家下拉框
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string SupplierOption(int id, string lang = "CN")
        {
            StringBuilder builderTree = new StringBuilder();
            List<Lebi_Supplier> nodes = B_Lebi_Supplier.GetList("Supplier_Group_id in (select g.id from Lebi_Supplier_Group as g where g.type='supplier')", "");
            foreach (Lebi_Supplier node in nodes)
            {
                //builderTree.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>  \r\n", node.id, node.id == id ? "selected=\"selected\"" : "", Language.Content(node.Name, lang)));
                builderTree.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>  \r\n", node.id, node.id == id ? "selected=\"selected\"" : "", "[" + node.SubName + "]" + node.Company));

            }
            return builderTree.ToString();
        }
        /// <summary>
        ///  商家选择
        /// </summary>
        /// <param name="InputName"></param>
        /// <param name="ids"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string SupplierCheckbox(string InputName, string ids, string lang = "CN")
        {
            List<Lebi_Supplier> models = B_Lebi_Supplier.GetList("Type_id_SupplierStatus = 442", "");
            string str = "";
            foreach (Lebi_Supplier model in models)
            {
                string sel = "";
                if (("," + ids + ",").IndexOf("," + model.id + ",") > -1)
                    sel = "checked";
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-checkbox m-r-20\"><input type=\"checkbox\" id=\"" + InputName + "" + model.id + "\" name=\"" + InputName + "\" value=\"" + model.id + "\" class=\"custom-control-input\" shop=\"true\" " + sel + "><span class=\"custom-control-label\">" + Shop.Bussiness.Language.Content(model.Name, lang) + "</span></label>";
                }
                else
                {
                    str += "<label><input type=\"checkbox\" name=\"" + InputName + "\" id=\"" + InputName + "" + model.id + "\" shop=\"true\" value=\"" + model.id + "\" " + sel + ">" + Shop.Bussiness.Language.Content(model.Name, lang) + "&nbsp;</label>";
                }
            }
            return str;
        }
        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Lebi_Supplier GetSupplier(int id)
        {
            Lebi_Supplier user = B_Lebi_Supplier.GetModel("id = " + id);
            if (user == null)
                user = new Lebi_Supplier();
            return user;
        }
        /// <summary>
        /// 供应商自定义用户分组
        /// </summary>
        /// <param name="supplierid"></param>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static string SupplierUserGroupOption(int supplierid, int sid)
        {
            List<Lebi_Supplier_UserGroup> models = B_Lebi_Supplier_UserGroup.GetList("Supplier_id=" + supplierid + "", "Sort desc");
            string str = "";
            foreach (Lebi_Supplier_UserGroup model in models)
            {
                string sel = "";
                if (sid == model.id)
                    sel = "selected";
                str += "<option value=\"" + model.id + "\" " + sel + ">" + model.Name + "</option>";
            }
            return str;
        }
    }

}
