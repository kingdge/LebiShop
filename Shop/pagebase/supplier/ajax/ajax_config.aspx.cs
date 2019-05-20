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


namespace Shop.Supplier.Ajax
{
    public partial class ajax_config : SupplierAjaxBase
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
        /// 编辑配送区域价格
        /// </summary>
        public void Transport_Price_Edit()
        {
            if (!Power("supplier_transport_list", "配送方式"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int tid = RequestTool.RequestInt("tid", 0);
            Lebi_Transport tmodel = B_Lebi_Transport.GetModel(tid);
            Lebi_Transport_Price model = B_Lebi_Transport_Price.GetModel("id = " + id + " and Supplier_id = " + CurrentSupplier.id + "");
            if (model != null)
            {
                int Area_id = model.Area_id;
                B_Lebi_Transport_Price.SafeBindForm(model);
                model.Transport_id = tmodel.id;
                model.Area_id = Area_id;
                B_Lebi_Transport_Price.Update(model);
                Log.Add("编辑配送区域", "Transport_Price", id.ToString(), CurrentSupplier, tmodel.Name);
            }
            else
            {
                string aids = RequestTool.RequestString("Area_ids");
                if (aids == "")
                    aids = "0";
                List<Lebi_Area> areas = B_Lebi_Area.GetList("id in (lbsql{" + aids + "})", "");
                model = new Lebi_Transport_Price();
                foreach (Lebi_Area area in areas)
                {
                    //避免重复添加
                    int count = B_Lebi_Transport_Price.Counts("Transport_id=" + tmodel.id + " and Area_id=" + area.id + " and Supplier_id = " + CurrentSupplier.id + "");
                    if (count > 0)
                        continue;
                    B_Lebi_Transport_Price.SafeBindForm(model);
                    model.Area_id = area.id;
                    model.Transport_id = tmodel.id;
                    model.Supplier_id = CurrentSupplier.id;
                    B_Lebi_Transport_Price.Add(model);
                }
                Log.Add("添加配送区域", "Transport_Price", id.ToString(), CurrentSupplier, tmodel.Name);
            }
            Response.Write("{\"msg\":\"OK\"}");

        }
        /// <summary>
        /// 批量更新配送区域价格
        /// </summary>
        public void Transport_Price_Update()
        {
            if (!Power("supplier_transport_list", "配送方式"))
            {
                AjaxNoPower();
                return;
            }
            int tid = RequestTool.RequestInt("tid", 0);
            string id = RequestTool.RequestSafeString("Uid");
            Lebi_Transport tmodel = B_Lebi_Transport.GetModel(tid);
            List<Lebi_Transport_Price> models = B_Lebi_Transport_Price.GetList("id in (lbsql{" + id + "}) and Transport_id=" + tid + "  and Supplier_id = " + CurrentSupplier.id + "", "");
            foreach (Lebi_Transport_Price model in models)
            {
                model.Price = RequestTool.GetFormDecimal("Price" + model.id + "", 0);
                model.Weight_Start = RequestTool.GetFormDecimal("Weight_Start" + model.id + "", 0);
                model.Weight_Step = RequestTool.GetFormDecimal("Weight_Step" + model.id + "", 0);
                model.Price_Step = RequestTool.GetFormDecimal("Price_Step" + model.id + "", 0);
                B_Lebi_Transport_Price.Update(model);
            }
            Log.Add("编辑配送区域", "Transport_Price", id.ToString(), CurrentSupplier, tmodel.Name);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除区域配送定价
        /// </summary>
        public void Transport_Price_Del()
        {
            if (!Power("supplier_transport_list", "配送方式"))
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
            B_Lebi_Transport_Price.Delete("id in (lbsql{" + id + "}) and Supplier_id = " + CurrentSupplier.id + "");
            Log.Add("删除配送区域", "Transport_Price", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑付款账号
        /// </summary>
        public void Bank_Edit()
        {
            if (!Power("supplier_bank_list", "收款账号"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Supplier_Bank model = B_Lebi_Supplier_Bank.GetModel("id = " + id + " and Supplier_id = " + CurrentSupplier.id + "");
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Supplier_Bank();
            }
            model = B_Lebi_Supplier_Bank.SafeBindForm(model);
            if (addflag)
            {
                model.Supplier_id = CurrentSupplier.id;
                B_Lebi_Supplier_Bank.Add(model);
                id = B_Lebi_Supplier_Bank.GetMaxId();
                Log.Add("添加收款账号", "Bank", id.ToString(), CurrentSupplier, model.Name);
            }
            else
            {
                B_Lebi_Supplier_Bank.Update(model);
                Log.Add("编辑收款账号", "Bank", id.ToString(), CurrentSupplier, model.Name);
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除付款账号
        /// </summary>
        public void Bank_Del()
        {
            if (!Power("supplier_bank_list", "收款账号"))
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
            Log.Add("删除收款账号", "Bank", id.ToString(), CurrentSupplier, id.ToString());
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
            Log.Add("编辑资料", "User", CurrentSupplier.id.ToString(), CurrentSupplier, model.UserName);
            string result = "{\"msg\":\"OK\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 编辑商家皮肤
        /// </summary>
        public void Skin_Edit()
        {
            if (!Power("supplier_skin", "编辑皮肤"))
            {
                AjaxNoPower();
                return;
            }

            B_Lebi_Supplier.SafeBindForm(CurrentSupplier);
            CurrentSupplier.head = RequestTool.RequestStringForUserEditor("head");
            CurrentSupplier.shortbar = RequestTool.RequestStringForUserEditor("shortbar");
            CurrentSupplier.longbar = RequestTool.RequestStringForUserEditor("longbar");
            B_Lebi_Supplier.Update(CurrentSupplier);
            Log.Add("编辑皮肤", "User", CurrentSupplier.id.ToString(), CurrentSupplier, CurrentSupplier.UserName);
            string result = "{\"msg\":\"OK\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 编辑商家
        /// </summary>
        public void Description_Edit()
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
            model.Name = Language.RequestSafeString("Name");
            model.Description = Language.RequestStringForUserEditor("Description");
            model.SEO_Title = Language.RequestSafeString("SEO_Title");
            model.SEO_Keywords = Language.RequestSafeString("SEO_Keywords");
            model.SEO_Description = Language.RequestSafeString("SEO_Description");
            B_Lebi_Supplier.SafeBindForm(model);
            B_Lebi_Supplier.Update(model);
            Log.Add("编辑资料", "User", CurrentSupplier.id.ToString(), CurrentSupplier, model.UserName);
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
            Lebi_User model = B_Lebi_User.GetModel(CurrentSupplier.User_id);
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
        /// 编辑身份验证项目
        /// </summary>
        public void Verified_Edit()
        {
            if (!Power("supplier_verified", "身份验证"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int Type_id_BillType = RequestTool.RequestInt("Type_id_BillType", 0);
            bool addflag = false;
            string where = "";
            where = "Verified_id = " + id + " and Supplier_id = " + CurrentSupplier.id + "";
            Lebi_Supplier_Verified Verified = B_Lebi_Supplier_Verified.GetModel(id);
            if (Verified == null)
                Verified = new Lebi_Supplier_Verified();
            Lebi_Supplier_Verified_Log model = B_Lebi_Supplier_Verified_Log.GetModel(where);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Supplier_Verified_Log();
            }
            model = B_Lebi_Supplier_Verified_Log.SafeBindForm(model);
            model.Type_id_SupplierVerifiedStatus = 9020;
            model.Time_Add = DateTime.Now;
            if (addflag)
            {
                model.Verified_id = id;
                model.Supplier_id = CurrentSupplier.id;
                B_Lebi_Supplier_Verified_Log.Add(model);
                id = B_Lebi_Supplier_Verified_Log.GetMaxId();
                Log.Add("身份验证", "Supplier_Verified", id.ToString(), CurrentSupplier, Lang(Verified.Name));
            }
            else
            {
                B_Lebi_Supplier_Verified_Log.Update(model);
                Log.Add("身份验证", "Supplier_Verified", id.ToString(), CurrentSupplier, Lang(Verified.Name));
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 发送站内信
        /// </summary>
        public void Message_Write()
        {
            if (!Power("supplier_message", "站内信"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            int Message_Type_id = RequestTool.RequestInt("Message_Type_id", 0);
            string Title = RequestTool.RequestSafeString("Title");
            string Content = RequestTool.RequestSafeString("Content");
            Lebi_Message model = new Lebi_Message();
            if (id != 0)
            {
                Lebi_Message mes = B_Lebi_Message.GetModel("Supplier_id=" + CurrentSupplier.id + " and id " + id);
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
            model.User_id_From = CurrentSupplier.id;
            model.User_Name_From = CurrentSupplier.UserName;
            model.User_id_To = 0;
            model.User_Name_To = "管理员";
            model.IsRead = 0;
            model.IsSystem = 0;
            model.Time_Add = System.DateTime.Now;
            model.Language = CurrentLanguage.Code;
            model.IP = RequestTool.GetClientIP();
            model.Supplier_id = CurrentSupplier.id;
            B_Lebi_Message.Add(model);
            Log.Add("发送站内信", "Message", model.id.ToString(), CurrentSupplier, Title);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 回复站内信
        /// </summary>
        public void Message_Reply()
        {
            if (!Power("supplier_message", "站内信"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            string Title = RequestTool.RequestSafeString("Title");
            string Content = RequestTool.RequestSafeString("Content");
            Lebi_Message mes = B_Lebi_Message.GetModel("Supplier_id=" + CurrentSupplier.id + " and id = " + id);
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
            model.User_id_From = mes.User_id_To;
            model.User_Name_From = mes.User_Name_To;
            model.User_id_To = mes.User_id_From;
            model.User_Name_To = mes.User_Name_From;
            model.IsRead = 0;
            model.IsSystem = 0;
            model.Time_Add = System.DateTime.Now;
            model.Language = mes.Language;
            model.IP = RequestTool.GetClientIP();
            model.Supplier_id = CurrentSupplier.id;
            B_Lebi_Message.Add(model);
            Log.Add("回复站内信", "Message", model.id.ToString(), CurrentSupplier, Title);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除站内信
        /// </summary>
        public void Message_Del()
        {
            if (!Power("supplier_message", "站内信"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Message.Delete("Supplier_id=" + CurrentSupplier.id + " and id in (lbsql{" + id + "})");
            Log.Add("删除站内信", "Message", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 客服面板设置信息
        /// </summary>
        public void ServicePanel_Config()
        {
            if (!Power("supplier_servicepanel_list", "客服面板"))
            {
                AjaxNoPower();
                return;
            }
            Model.ServicePanel sp = new Model.ServicePanel();
            sp.X = RequestTool.RequestSafeString("X");
            sp.Y = RequestTool.RequestSafeString("Y");
            sp.Theme = RequestTool.RequestSafeString("Theme");
            sp.Status = RequestTool.RequestSafeString("Status");
            sp.IsFloat = RequestTool.RequestSafeString("IsFloat");
            sp.Style = RequestTool.RequestSafeString("Style");
            string con = B_ServicePanel.ToJson(sp);
            //BaseConfig bconf = ShopCache.GetBaseConfig();
            Lebi_Supplier model = B_Lebi_Supplier.GetModel(CurrentSupplier.id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"账号不存在\"}");
                return;
            }
            model.ServicePanel = con;
            B_Lebi_Supplier.Update(model);
            Log.Add("配置客服面板", "ServicePanel", CurrentSupplier.id.ToString(), CurrentSupplier, "");
            string temp = "";
            temp += "var ShopstartX = document.body.clientWidth-" + RequestTool.RequestSafeString("X") + ";\n";
            if (RequestTool.RequestSafeString("IsFloat") == "1" && RequestTool.RequestSafeString("Style") == "1")
            {
                temp += "var ShopstartY = " + RequestTool.RequestSafeString("Y") + ";\n";
            }
            else if (RequestTool.RequestSafeString("IsFloat") == "0" && RequestTool.RequestSafeString("Style") == "1")
            {
                temp += "var ShopstartY = " + RequestTool.RequestSafeString("Y") + ";\n";
            }
            if (RequestTool.RequestSafeString("IsFloat") == "1" && RequestTool.RequestSafeString("Style") == "1")
            {
                temp += "var verticalpos='fromtop';\n";
            }
            else if (RequestTool.RequestSafeString("IsFloat") == "0" && RequestTool.RequestSafeString("Style") == "1")
            {
                temp += "var verticalpos='frombottom';\n";
            }
            temp += "var winWidth = 0;\n";
            temp += "function iecompattest(){\n";
            temp += "   return (document.compatMode && document.compatMode!='BackCompat')? document.documentElement : document.body\n";
            temp += "}\n";

            temp += "function ShopServicePannelstaticbar(){\n";
            temp += "	if (window.innerWidth)\n";
            temp += "	winWidth = window.innerWidth;\n";
            temp += "	else if ((document.body) && (document.body.clientWidth))\n";
            temp += "	winWidth = document.body.clientWidth;\n";
            temp += "	if (document.documentElement && document.documentElement.clientWidth){winWidth = document.documentElement.clientWidth;}\n";
            temp += "	ShopstartX = winWidth-" + RequestTool.RequestSafeString("X") + ";\n";
            temp += "	barheight=document.getElementById('divShopServicePannel').offsetHeight\n";
            temp += "	var ns = (navigator.appName.indexOf('Netscape') != -1) || window.opera;\n";
            temp += "	var d = document;\n";
            temp += "	function ml(id){\n";
            temp += "		var el=d.getElementById(id);\n";
            temp += "		el.style.visibility='visible'\n";
            temp += "		if(d.layers)el.style=el;\n";
            temp += "		el.sP=function(x,y){this.style.left=x+'px';this.style.top=y+'px';};\n";
            temp += "		el.x = ShopstartX;\n";
            temp += "		if (verticalpos=='fromtop')\n";
            temp += "		el.y = ShopstartY;\n";
            temp += "		else{\n";
            temp += "		el.y = ns ? pageYOffset + innerHeight : iecompattest().scrollTop + iecompattest().clientHeight;\n";
            temp += "		el.y -= ShopstartY;\n";
            temp += "		}\n";
            temp += "		return el;\n";
            temp += "	}\n";
            temp += "	window.ShopServicePannel=function(){\n";
            temp += "		if (verticalpos=='fromtop'){\n";
            temp += "		var pY = ns ? pageYOffset : iecompattest().scrollTop;\n";
            temp += "		ftlObj.y += (pY + ShopstartY - ftlObj.y)/8;\n";
            temp += "		}\n";
            temp += "		else{\n";
            temp += "		var pY = ns ? pageYOffset + innerHeight - barheight: iecompattest().scrollTop + iecompattest().clientHeight - barheight;\n";
            temp += "		ftlObj.y += (pY - ShopstartY - ftlObj.y)/8;\n";
            temp += "		}\n";
            temp += "		ftlObj.sP(ftlObj.x, ftlObj.y);\n";
            temp += "		setTimeout('stayTopLeft()', 10);\n";
            temp += "	}\n";
            temp += "	ftlObj = ml('ShopServicePannel');\n";
            temp += "	ShopServicePannel();\n";
            temp += "}\n";
            temp += "if(typeof(HTMLElement)!='undefined'){\n";
            temp += "  HTMLElement.prototype.contains=function (obj)\n";
            temp += "  {\n";
            temp += "	  while(obj!=null&&typeof(obj.tagName)!='undefind'){\n";
            temp += "　 　 if(obj==this) return true;\n";
            temp += "　　	　obj=obj.parentNode;\n";
            temp += "　	  }\n";
            temp += "	  return false;\n";
            temp += "  }\n";
            temp += "}\n";
            temp += "if (window.addEventListener)\n";
            temp += "window.addEventListener('load', ShopServicePannelstaticbar, false)\n";
            temp += "else if (window.attachEvent)\n";
            temp += "window.attachEvent('onload', ShopServicePannelstaticbar)\n";
            temp += "else if (document.getElementById)\n";
            temp += "window.onload=ShopServicePannelstaticbar;\n";
            temp += "window.onresize=ShopServicePannelstaticbar;\n";
            temp += "function ShopServicePannelOver(){\n";
            temp += "   document.getElementById('ShopServicePannelMenu').style.display = 'none';\n";
            temp += "   document.getElementById('ShopServicePannelOnline').style.display = 'block';\n";
            temp += "   document.getElementById('divShopServicePannel').style.width = '170px';\n";
            temp += "}\n";
            temp += "function ShopServicePannelOut(){\n";
            temp += "   document.getElementById('ShopServicePannelMenu').style.display = 'block';\n";
            temp += "   document.getElementById('ShopServicePannelOnline').style.display = 'none';\n";
            temp += "}\n";
            temp += "if(typeof(HTMLElement)!='undefined') \n";
            temp += "{\n";
            temp += "   HTMLElement.prototype.contains=function(obj)\n";
            temp += "   {\n";
            temp += "       while(obj!=null&&typeof(obj.tagName)!='undefind'){\n";
            temp += "   　　　 if(obj==this) return true;\n";
            temp += "   　　　 obj=obj.parentNode;\n";
            temp += "   　　}\n";
            temp += "          return false;\n";
            temp += "   };\n";
            temp += "}\n";
            temp += "function ShowShopServicePannel(theEvent){\n";
            temp += "　 if (theEvent){\n";
            temp += "　    var browser=navigator.userAgent;\n";
            temp += "　	if (browser.indexOf('Firefox')>0){\n";
            temp += "　　     if (document.getElementById('ShopServicePannelOnline').contains(theEvent.relatedTarget)) {\n";
            temp += "　　         return;\n";
            temp += "         }\n";
            temp += "      }\n";
            temp += "	    if (browser.indexOf('MSIE')>0 || browser.indexOf('Safari')>0){ //IE Safari\n";
            temp += "          if (document.getElementById('ShopServicePannelOnline').contains(event.toElement)) {\n";
            temp += "	          return;\n";
            temp += "          }\n";
            temp += "      }\n";
            temp += "   }\n";
            temp += "   document.getElementById('ShopServicePannelMenu').style.display = 'block';\n";
            temp += "   document.getElementById('ShopServicePannelOnline').style.display = 'none';\n";
            temp += "}";
            HtmlEngine save = new HtmlEngine();
            save.CreateFile("/supplier/js/user/ServicePanel" + CurrentSupplier.id + ".js", temp);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑客服面板成员信息
        /// </summary>
        public void ServicePanel_Edit()
        {
            if (!Power("supplier_servicepanel_list", "客服面板"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_ServicePanel model = B_Lebi_ServicePanel.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_ServicePanel();
            }
            model = B_Lebi_ServicePanel.SafeBindForm(model);
            model.Name = RequestTool.RequestSafeString("Name");
            model.Account = RequestTool.RequestSafeString("Account");
            model.Language_ids = RequestTool.RequestSafeString("Language_ids");
            model.ServicePanel_Type_id = RequestTool.RequestInt("ServicePanel_Type_id", 0);
            model.ServicePanel_Group_id = RequestTool.RequestInt("ServicePanel_Group_id", 0);
            model.Sort = RequestTool.RequestInt("Sort", 0);
            model.Supplier_id = CurrentSupplier.id;
            if (addflag)
            {
                B_Lebi_ServicePanel.Add(model);
                id = B_Lebi_ServicePanel.GetMaxId();
                Log.Add("添加客服成员", "ServicePanel", id.ToString(), CurrentSupplier, model.Name);
            }
            else
            {
                B_Lebi_ServicePanel.Update(model);
                Log.Add("编辑客服成员", "ServicePanel", id.ToString(), CurrentSupplier, model.Name);
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 批量更新客服面板成员信息
        /// </summary>
        public void ServicePanel_Update()
        {
            if (!Power("supplier_servicepanel_list", "客服面板"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("Uid");
            List<Lebi_ServicePanel> models = B_Lebi_ServicePanel.GetList("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + id + "})", "");
            foreach (Lebi_ServicePanel model in models)
            {
                model.Sort = RequestTool.RequestInt("Sort" + model.id + "", 0);
                model.Name = RequestTool.RequestSafeString("Name" + model.id);
                model.Account = RequestTool.RequestSafeString("Account" + model.id);
                B_Lebi_ServicePanel.Update(model);
            }
            Log.Add("编辑客服成员", "ServicePanel", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除客服面板成员信息
        /// </summary>
        public void ServicePanel_Del()
        {
            if (!Power("supplier_servicepanel_list", "客服面板"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_ServicePanel.Delete("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + id + "})");
            Log.Add("删除客服成员", "ServicePanel", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑客服面板部门信息
        /// </summary>
        public void ServicePanel_Group_Edit()
        {
            if (!Power("supplier_servicepanel_list", "客服面板"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_ServicePanel_Group model = B_Lebi_ServicePanel_Group.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_ServicePanel_Group();
            }
            model = B_Lebi_ServicePanel_Group.SafeBindForm(model);
            model.Name = RequestTool.RequestSafeString("Name");
            model.Language_ids = RequestTool.RequestSafeString("Language_ids");
            model.Sort = RequestTool.RequestInt("Sort", 0);
            if (addflag)
            {
                model.Supplier_id = CurrentSupplier.id;
                B_Lebi_ServicePanel_Group.Add(model);
                id = B_Lebi_ServicePanel_Group.GetMaxId();
                Log.Add("添加客服部门", "ServicePanel_Group", id.ToString(), CurrentSupplier, model.Name);
            }
            else
            {
                B_Lebi_ServicePanel_Group.Update(model);
                Log.Add("编辑客服部门", "ServicePanel_Group", id.ToString(), CurrentSupplier, model.Name);
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 批量更新客服面板部门信息
        /// </summary>
        public void ServicePanel_Group_Update()
        {
            if (!Power("supplier_servicepanel_list", "客服面板"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("Uid");
            List<Lebi_ServicePanel_Group> models = B_Lebi_ServicePanel_Group.GetList("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + id + "})", "");
            foreach (Lebi_ServicePanel_Group model in models)
            {
                model.Sort = RequestTool.RequestInt("Sort" + model.id + "", 0);
                model.Name = RequestTool.RequestSafeString("Name" + model.id);
                B_Lebi_ServicePanel_Group.Update(model);
            }
            Log.Add("编辑客服部门", "ServicePanel_Group", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除客服面板部门信息
        /// </summary>
        public void ServicePanel_Group_Del()
        {
            if (!Power("supplier_servicepanel_list", "客服面板"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestSafeString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_ServicePanel_Group.Delete("Supplier_id = " + CurrentSupplier.id + " and id in (lbsql{" + id + "})");
            Log.Add("删除客服部门", "ServicePanel_Group", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑在线支付
        /// </summary>
        public void OnlinePay_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_OnlinePay parentmodel = B_Lebi_OnlinePay.GetModel(id);
            if (parentmodel == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Lebi_OnlinePay model = B_Lebi_OnlinePay.GetModel("parentid=" + parentmodel.id + " and Supplier_id=" + CurrentSupplier.id + "");
            if (model == null)
                model = new Lebi_OnlinePay();
            if (model.id == 0)
            {
                if (!Power("onlinepay_add", "添加在线支付"))
                {
                    AjaxNoPower();
                    return;
                }
            }
            else
            {
                if (!Power("onlinepay_edit", "编辑在线支付"))
                {
                    AjaxNoPower();
                    return;
                }
            }
            model.UserName = RequestTool.RequestSafeString("UserName");
            model.UserKey = RequestTool.RequestSafeString("UserKey");
            model.Email = RequestTool.RequestSafeString("Email");
            model.Sort = RequestTool.RequestInt("Sort", 0);
            model.IsUsed = RequestTool.RequestInt("IsUsed", 0);
            model.Supplier_id = CurrentSupplier.id;
            model.parentid = parentmodel.id;
            if (model.id == 0)
            {
                B_Lebi_OnlinePay.Add(model);
                model.id = B_Lebi_OnlinePay.GetMaxId();
            }
            else
            {
                B_Lebi_OnlinePay.Update(model);
            }
            Log.Add("编辑在线支付", "OnlinePay", model.id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + model.id + "\"}");
        }
        /// <summary>
        /// 编辑|添加店铺幻灯图片
        /// </summary>
        public void indeximage_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Page model = B_Lebi_Page.GetModel(id);
            Lebi_Node node = NodePage.GetNodeByCode("shopindeximages");
            if (model == null)
            {
                model = new Lebi_Page();
            }
            B_Lebi_Page.SafeBindForm(model);
            model.Supplier_id = CurrentSupplier.id;
            model.Node_id = node.id;
            if (model.id == 0)
            {
                if (!Power("indeximage_add", "添加店铺幻灯"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Page.Add(model);
                id = B_Lebi_Page.GetMaxId();
                string action = "添加店铺幻灯";
                string description = model.Name;
                Log.Add(action, "Page", id.ToString(), CurrentSupplier, description);
            }
            else
            {
                if (!Power("indeximage_edit", "编辑店铺幻灯"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Page.Update(model);
                string action = Tag("编辑店铺幻灯");
                string description = model.Name;
                Log.Add(action, "Page", id.ToString(), CurrentSupplier, description);
            }
            //=========================================
            //处理静态页面
            ImageHelper.LebiImagesUsed(model.ImageOriginal, "page", id);
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }
        /// <summary>
        /// 删除店铺幻灯图片
        /// </summary>
        public void indeximage_Del()
        {
            if (!EX_Admin.Power("indeximage_del", "删除店铺幻灯"))
            {
                AjaxNoPower();
                return;
            }

            string id = RequestTool.RequestSafeString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Page.Delete("id in (lbsql{" + id + "}) and Supplier_id=" + CurrentSupplier.id + "");
            Log.Add("删除店铺幻灯", "Page", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑配送点
        /// </summary>
        public void Delivery_Edit()
        {
            if (!Power("supplier_delivery_list", "配送点管理"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Supplier_Delivery model = B_Lebi_Supplier_Delivery.GetModel("id = " + id + " and Supplier_id = " + CurrentSupplier.id + "");
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Supplier_Delivery();
            }
            model = B_Lebi_Supplier_Delivery.SafeBindForm(model);
            if (addflag)
            {
                model.Supplier_id = CurrentSupplier.id;
                B_Lebi_Supplier_Delivery.Add(model);
                id = B_Lebi_Supplier_Delivery.GetMaxId();
                Log.Add("添加配送点", "Delivery", id.ToString(), CurrentSupplier, model.Name);
            }
            else
            {
                B_Lebi_Supplier_Delivery.Update(model);
                Log.Add("编辑配送点", "Delivery", id.ToString(), CurrentSupplier, model.Name);
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除配送点
        /// </summary>
        public void Delivery_Del()
        {
            if (!Power("supplier_delivery_list", "配送点管理"))
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
            B_Lebi_Supplier_Delivery.Delete("id in (lbsql{" + id + "}) and Supplier_id = " + CurrentSupplier.id + "");
            Log.Add("删除配送点", "Delivery", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除自提点
        /// </summary>
        public void pickup_Del()
        {
            if (!Power("pickup_manage", "管理自提点"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Fid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_PickUp.Delete("Supplier_id=" + CurrentSupplier.id + " and id in (lbsql{" + id + "})");
            Log.Add("删除自提点", "PickUp", id.ToString(), CurrentSupplier, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑自提点
        /// </summary>
        public void pickup_Edit()
        {
            if (!Power("pickup_manage", "管理自提点"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_PickUp model = B_Lebi_PickUp.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_PickUp();
                model.Supplier_id = CurrentSupplier.id;
            }
            model = B_Lebi_PickUp.BindForm(model);

            if (addflag)
            {

                B_Lebi_PickUp.Add(model);
                id = B_Lebi_PickUp.GetMaxId();
                Log.Add("添加自提点", "PickUp", id.ToString(), CurrentSupplier, model.Name);
            }
            else
            {
                if (model.Supplier_id != CurrentSupplier.id)
                {
                    Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                    return;
                }
                B_Lebi_PickUp.Update(model);
                Log.Add("编辑自提点", "PickUp", id.ToString(), CurrentSupplier, model.Name);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        public void EmailConfig_Edit()
        {
            if (!Power("supplier_emailconfig", "邮件设置"))
            {
                AjaxNoPower();
                return;
            }
            B_BaseConfig_Supplier dob = new B_BaseConfig_Supplier();
            BaseConfig_Supplier model = new BaseConfig_Supplier();
            model.Email = Language.RequestString("Email");
            model.MailName = RequestTool.RequestString("MailName");
            string pwd = RequestTool.RequestString("MailPassWord");
            if (pwd != "******")
                model.MailPassWord = pwd;
            model.SmtpAddress = RequestTool.RequestString("SmtpAddress");
            model.MailAddress = RequestTool.RequestString("MailAddress");
            model.MailDisplayName = RequestTool.RequestString("MailDisplayName");
            if (Convert.ToInt32(model.Mail_SendTime) < 1)
                model.Mail_SendTime = "1";
            model.AdminMailAddress = RequestTool.RequestString("AdminMailAddress");
            model.AdminMailSign = RequestTool.RequestString("AdminMailSign");
            model.MailPort = RequestTool.RequestString("MailPort");
            model.MailIsSSL = RequestTool.RequestInt("MailIsSSL").ToString();
            dob.SaveConfig(model,CurrentSupplier.id);
            //更新队列时间
            TimeWork tw = new TimeWork();
            tw.work_email_restart();
            Log.Add("编辑邮件设置", "Config", "", CurrentSupplier, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 测试邮件配置
        /// </summary>
        public void EmailTestSend()
        {
            Email.SendEmail_test("Supplier", CurrentSupplier.id);
            Response.Write("{\"msg\":\"OK\"}");
        }
        public void SMSConfig_Edit()
        {
            if (!Power("supplier_smsconfig", "手机短信设置"))
            {
                AjaxNoPower();
                return;
            }
            B_BaseConfig_Supplier dob = new B_BaseConfig_Supplier();
            BaseConfig_Supplier model = new BaseConfig_Supplier();
            model.SMS_user = RequestTool.RequestString("SMS_user");
            string pwd = RequestTool.RequestString("SMS_password");
            if (pwd != "******")
                model.SMS_password = pwd;
            model.SMS_server = RequestTool.RequestString("SMS_server");
            model.SMS_state = RequestTool.RequestString("SMS_state", "0");
            model.SMS_apitype = RequestTool.RequestString("SMS_apitype");
            model.SMS_sendmode = RequestTool.RequestString("SMS_sendmode");
            model.SMS_reciveno = RequestTool.RequestString("SMS_reciveno");
            model.SMS_serverport = RequestTool.RequestString("SMS_serverport", "20002");
            model.SMS_maxlen = RequestTool.RequestString("SMS_maxlen", "0");
            model.SMS_lastmsg = RequestTool.RequestString("SMS_lastmsg");
            model.SMS_httpapi = RequestTool.RequestString("SMS_httpapi");
            dob.SaveConfig(model, CurrentSupplier.id);
            Log.Add("手机短信设置", "Config", "", CurrentSupplier, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
    }
}