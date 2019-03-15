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
using System.Web.Security;
namespace Shop.api
{
    public partial class api : ShopPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string apipwd = LB.Tools.RequestTool.RequestString("apipwd");
            if (apipwd != ShopCache.GetBaseConfig().APIPassWord || apipwd == "")
            {
                Response.Write("{\"msg\":\"验证失败\"}");
                return;
            }
            string action = LB.Tools.RequestTool.RequestString("action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        public void GetUsers()
        {
            int id = RequestTool.RequestInt("user_id", 0);
            int size = RequestTool.RequestInt("size", 1);
            size = size < 0 ? 0 : size;
            size = size > 50 ? 50 : size;
            List<Lebi_User> users = B_Lebi_User.GetList("id>=" + id, "id asc", size, 1);
            List<apiUser> models = new List<apiUser>();
            apiUser model;
            foreach (Lebi_User user in users)
            {
                model = new apiUser();
                model.Address = user.Address;
                model.Password = user.Password;
                model.Area = EX_Area.GetAreaName(user.Area_id, 4);
                model.Birthday = user.Birthday;
                model.CashAccount_Bank = user.CashAccount_Bank;
                model.CashAccount_Code = user.CashAccount_Code;
                model.CashAccount_Name = user.CashAccount_Name;
                model.City = user.City;
                model.Count_Login = user.Count_Login;
                model.Currency_Code = user.Currency_Code;
                model.Email = user.Email;
                model.Face = user.Face;
                model.Fax = user.Fax;
                model.id = user.id;
                model.Introduce = user.Introduce;
                model.IP_Last = user.IP_Last;
                model.IP_This = user.IP_This;
                model.Language = user.Language;
                model.MobilePhone = user.MobilePhone;
                model.Money = user.Money;
                model.Money_xiaofei = user.Money_xiaofei;
                model.Msn = user.Msn;
                model.NickName = user.NickName;
                model.Phone = user.Phone;
                model.Point = user.Point;
                model.Postalcode = user.Postalcode;
                model.QQ = user.QQ;
                model.RealName = user.RealName;
                model.Sex = user.Sex;
                model.Time_Last = user.Time_Last;
                model.Time_lastorder = user.Time_lastorder;
                model.Time_Reg = user.Time_Reg;
                model.Time_This = user.Time_This;
                model.UserName = user.UserName;

                models.Add(model);
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(models);
            LBAPI api = new LBAPI();
            api.data = json;
            api.msg = "OK";
            json = jss.Serialize(api);
            Response.Write(json);
        }
        /// <summary>
        /// 获取订单
        /// </summary>
        public void GetOrders()
        {
            int id = RequestTool.RequestInt("order_id", 0);
            int size = RequestTool.RequestInt("size", 1);
            size = size < 0 ? 0 : size;
            size = size > 50 ? 50 : size;
            List<Lebi_Order> orders = B_Lebi_Order.GetList("id>=" + id, "id asc", size, 1);
            List<apiOrder> models = new List<apiOrder>();
            apiOrder model;
            string domain = "http://" + RequestTool.GetRequestDomain();
            int port = RequestTool.GetRequestPort();
            if (port != 80)
                domain = domain + ":" + port;
            foreach (Lebi_Order order in orders)
            {
                model = new apiOrder();
                model.Code = order.Code;
                model.Currency_Code = order.Code;
                model.Currency_ExchangeRate = order.Currency_ExchangeRate;
                model.Currency_Msige = order.Currency_Msige;
                model.id = order.id;
                model.IsCompleted = order.IsCompleted;

                model.IsInvalid = order.IsInvalid;
                model.IsPaid = order.IsPaid;
                model.IsReceived = order.IsReceived;
                model.IsReceived_All = order.IsReceived_All;
                model.IsShipped = order.IsShipped;
                model.IsShipped_All = order.IsShipped_All;
                model.IsVerified = order.IsVerified;
                model.Money_Bill = order.Money_Bill;
                model.Money_Market = order.Money_Market;
                model.Money_Order = order.Money_Order;
                model.Money_Product = order.Money_Product;
                model.Money_Transport = order.Money_Transport;
                model.OnlinePay = order.OnlinePay;
                model.OnlinePay_Code = order.OnlinePay_Code;
                model.Pay = order.Pay;
                model.Point = order.Point;
                model.Point_Buy = order.Point_Buy;
                model.Remark_Admin = order.Remark_Admin;
                model.Remark_User = order.Remark_User;
                model.T_Address = order.T_Address;
                model.T_Area = EX_Area.GetAreaName(order.T_Area_id, 4);
                model.T_Email = order.T_Email;
                model.T_MobilePhone = order.T_MobilePhone;
                model.T_Name = order.T_Name;
                model.T_Phone = order.T_Phone;
                model.T_Postalcode = order.T_Postalcode;
                model.Time_Add = order.Time_Add;
                model.Time_Completed = order.Time_Completed;
                model.Time_Paid = order.Time_Paid;
                model.Time_Received = order.Time_Received;
                model.Time_Shipped = order.Time_Shipped;
                model.Time_Verified = order.Time_Verified;
                model.Transport_Code = order.Transport_Code;
                model.Transport_Mark = order.Transport_Mark;
                model.Transport_Name = order.Transport_Name;
                model.OrderType = EX_Type.TypeName(order.Type_id_OrderType);
                model.User_id = order.User_id;
                model.User_UserName = order.User_UserName;
                model.Volume = order.Volume;
                model.Weight = order.Weight;
                List<apiOrderProduct> products = new List<apiOrderProduct>();
                List<Lebi_Order_Product> pros = B_Lebi_Order_Product.GetList("Order_id=" + order.id + "", "");
                apiOrderProduct product;
                foreach (Lebi_Order_Product pro in pros)
                {
                    product = new apiOrderProduct();
                    product.Count = pro.Count;
                    product.Guige = "";
                    product.ImageBig = domain + pro.ImageBig;
                    product.ImageSmall = domain + pro.ImageSmall;
                    product.Name = Language.Content(pro.Product_Name, "CN");
                    product.Number = pro.Product_Number;
                    product.Price = pro.Price;
                    product.URL = domain + URL("P_Product", pro.Product_id);
                    products.Add(product);
                }
                model.Products = products;
                models.Add(model);
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(models);
            LBAPI api = new LBAPI();
            api.data = json;
            api.msg = "OK";
            json = jss.Serialize(api);
            Response.Write(json);
        }

        /// <summary>
        /// 获取App菜单
        /// </summary>
        public void AppMenus()
        {
            string lang = RequestTool.RequestSafeString("lang","CN");
            string val = B_BaseConfig.Get("app_menu");
            val =Language.Content(val,lang);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<BaseConfigAppMenu> menus = B_BaseConfig.AppMenu(val);
            if (menus != null)
            {
                for (int i = 0; i < menus.Count; i++)
                {
                    menus[i].Icon = "http://" + RequestTool.GetRequestDomain() + menus[i].Icon;
                }
            }
            val = jss.Serialize(menus);
            LBAPI api = new LBAPI();
            api.data = val;
            api.msg = "OK";
            string json = jss.Serialize(api);
            Response.Write(json);
        }
        /// <summary>
        /// 获取App配置
        /// </summary>
        public void AppConfig()
        {
            string lang = RequestTool.RequestSafeString("lang", "CN");
            string app_name = Language.Content(B_BaseConfig.Get("app_name"), lang);
            string app_lefticon = "http://" + RequestTool.GetRequestDomain() + Language.Content(B_BaseConfig.Get("app_lefticon"), lang);
            string app_lefturl = Language.Content(B_BaseConfig.Get("app_lefturl"), lang);
            string app_righticon = "http://" + RequestTool.GetRequestDomain() + Language.Content(B_BaseConfig.Get("app_righticon"), lang);
            string app_righturl = Language.Content(B_BaseConfig.Get("app_righturl"), lang);
            string app_toplogo = "http://" + RequestTool.GetRequestDomain() + Language.Content(B_BaseConfig.Get("app_toplogo"), lang);
            string app_toplogourl = Language.Content(B_BaseConfig.Get("app_toplogourl"), lang);
            string app_topbackground = Language.Content(B_BaseConfig.Get("app_topbackground"), lang);
            string app_topcolor = Language.Content(B_BaseConfig.Get("app_topcolor"), lang);
            string app_topline = Language.Content(B_BaseConfig.Get("app_topline"), lang);
            string app_bottombackground = Language.Content(B_BaseConfig.Get("app_bottombackground"), lang);
            string app_bottomcolor = Language.Content(B_BaseConfig.Get("app_bottomcolor"), lang);
            string app_bottomline = Language.Content(B_BaseConfig.Get("app_bottomline"), lang);
            string app_bottomcount = Language.Content(B_BaseConfig.Get("app_bottomcount"), lang);
            string app_startimage = Language.Content(B_BaseConfig.Get("app_startimage"), lang);
            string app_starturl = Language.Content(B_BaseConfig.Get("app_starturl"), lang);
            string app_waittimes = Language.Content(B_BaseConfig.Get("app_waittimes"), lang);
            string app_version = Language.Content(B_BaseConfig.Get("app_version"), lang);
            string app_downloadurl = Language.Content(B_BaseConfig.Get("app_downloadurl"), lang);
            string app_updatetime = B_BaseConfig.Get("app_updatetime");
            string app_share = B_BaseConfig.Get("app_share");
            string app_share_qq_key = B_BaseConfig.Get("app_share_qq_key");
            string app_share_qq_secret = B_BaseConfig.Get("app_share_qq_secret");
            string app_share_wechat_key = B_BaseConfig.Get("app_share_wechat_key");
            string app_share_wechat_secret = B_BaseConfig.Get("app_share_wechat_secret");
            string app_share_dingtalk_key = B_BaseConfig.Get("app_share_dingtalk_key");
            string app_share_dingtalk_secret = B_BaseConfig.Get("app_share_dingtalk_secret");
            string val = "{\"app_name\":\"" + app_name + "\",\"app_toplogo\":\"" + app_toplogo + "\",\"app_toplogourl\":\"" + app_toplogourl + "\",\"app_lefticon\":\"" + app_lefticon + "\",\"app_lefturl\":\"" + app_lefturl + "\",\"app_righticon\":\"" + app_righticon + "\",\"app_righturl\":\"" + app_righturl + "\",\"app_topbackground\":\"" + app_topbackground + "\",\"app_topline\":\"" + app_topline + "\",\"app_topcolor\":\"" + app_topcolor + "\",\"app_bottombackground\":\"" + app_bottombackground + "\",\"app_bottomline\":\"" + app_bottomline + "\",\"app_bottomcolor\":\"" + app_bottomcolor + "\",\"app_bottomcount\":\"" + app_bottomcount + "\",\"app_startimage\":\"" + app_startimage + "\",\"app_starturl\":\"" + app_starturl + "\",\"app_waittimes\":\"" + app_waittimes + "\",\"app_version\":\"" + app_version + "\",\"app_downloadurl\":\"" + app_downloadurl + "\",\"updatetime\":\"" + app_updatetime + "\",\"share_settings\":{\"app_share\":\"" + app_share + "\",\"qq\":{\"key\":\"" + app_share_qq_key + "\",\"secret\":\"" + app_share_qq_secret + "\"},\"wechat\":{\"key\":\"" + app_share_wechat_key + "\",\"secret\":\"" + app_share_wechat_secret + "\"},\"dingtalk\":{\"key\":\"" + app_share_dingtalk_key + "\",\"secret\":\"" + app_share_dingtalk_secret + "\"}}}";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            LBAPI api = new LBAPI();
            api.data = val;
            api.msg = "OK";
            string json = jss.Serialize(api);
            Response.Write(json);
        }
        /// <summary>
        /// 条形码
        /// </summary>
        public void ProductCode()
        {
            string lang = RequestTool.RequestSafeString("lang", "CN");
            string Code = RequestTool.RequestSafeString("Code");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            LBAPI api = new LBAPI();
            Lebi_Product pro = B_Lebi_Product.GetModel("Code=lbsql{'" + Code + "'}");
            if (pro != null)
            {
                api.data = Shop.Bussiness.ThemeUrl.GetURL("P_Product", pro.id.ToString(), "", lang);
                api.msg = "OK";
                //Response.Write("{\"msg\":\"OK\",\"url\":\"" + Shop.Bussiness.ThemeUrl.GetURL("P_Product", pro.id.ToString(), "", lang) + "\"}");
            }
            else
            {
                api.msg = Tag("未找到商品");
                //Response.Write("{\"msg\":\"" + Tag("未找到商品") + "\"}");
            }
            string json = jss.Serialize(api);
            SystemLog.Add(json);
            Response.Write(json);
        }
    }
}