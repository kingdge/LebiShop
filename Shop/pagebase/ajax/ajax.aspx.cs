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
using System.Threading;
namespace Shop.Ajax
{
    /// <summary>
    /// 全局通用的操作
    /// </summary>
    public partial class Ajax : ShopPage
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
        /// 生成地区选择框
        /// </summary>
        public void GetAreaList()
        {
            int topid = RequestTool.RequestInt("topid", 0);
            int area_id = RequestTool.RequestInt("area_id", 0);
            int Parentid = 0;
            area_id = area_id == 0 ? topid : area_id;
            string Types = "";
            Thread thread = new Thread(() => { Types = ThreadGetAreaList(topid, area_id, Parentid); });
            thread.IsBackground = true;
            thread.Start();
            thread.Join();
            Response.Write(Types);
        }
        public string ThreadGetAreaList(int topid,int area_id,int Parentid)
        {
            List<Lebi_Area> models = B_Lebi_Area.GetList("Parentid=" + area_id + "", "Sort desc");
            Lebi_Area area = B_Lebi_Area.GetModel(area_id);
            if (area == null)
            {
                area = new Lebi_Area();
            }
            if (models.Count == 0)
            {
                Parentid = area.Parentid;
                models = B_Lebi_Area.GetList("Parentid=" + Parentid + "", "Sort desc");

            }
            else
            {
                Parentid = area_id;
            }
            string str = "<select id=\"Area_id\" onchange=\"SelectAreaList(" + topid + ",'Area_id');\" class=\"form-control\">";
            str += "<option value=\"0\" selected>" + Tag(" 请选择 ") + "</option>";
            foreach (Lebi_Area model in models)
            {
                if (area_id == model.id)
                    str += "<option value=\"" + model.id + "\" selected>" + model.Name + "</option>";
                else
                    str += "<option value=\"" + model.id + "\">" + model.Name + "</option>";
            }
            str += "</select>";
            if (topid != area_id)
                str = CreatSelect(Parentid, topid) + str;
            return str;
        }
        private string CreatSelect(int id, int topid)
        {
            string str = "<select id=\"Area_" + id + "\"  onchange=\"SelectAreaList(" + topid + ",'Area_" + id + "');\" class=\"form-control\">";
            Lebi_Area area = B_Lebi_Area.GetModel(id);
            if (area == null)
                return "";
            if (topid == area.id)
            {
                return "";
            }
            List<Lebi_Area> models = B_Lebi_Area.GetList("Parentid=" + area.Parentid + "", "Sort desc");
            if (models.Count == 0)
            {
                return "";
            }
            foreach (Lebi_Area model in models)
            {
                if (id == model.id)
                    str += "<option value=\"" + model.id + "\" selected>" + model.Name + "</option>";
                else
                    str += "<option value=\"" + model.id + "\">" + model.Name + "</option>";
            }
            str += "</select> ";
            str = CreatSelect(area.Parentid, topid) + str;
            return str;
        }
        /// <summary>
        /// 生成商品分类选择框
        /// </summary>
        public void GetProductTypeList()
        {
            int id = RequestTool.RequestInt("id", 0);
            int Parentid = 0;
            string Types = "";
            Thread thread = new Thread(() => { Types = ThreadGetProductTypeList(id, Parentid); });
            thread.IsBackground = true;
            thread.Start();
            thread.Join();
            Response.Write(Types);
        }
        public string ThreadGetProductTypeList(int id, int Parentid)
        {
            List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList("Parentid=" + id + "", "Sort desc");
            Lebi_Pro_Type area = B_Lebi_Pro_Type.GetModel(id);
            if (models.Count == 0)
            {
                if (area == null)
                    Parentid = 0;
                else
                    Parentid = area.Parentid;
                models = B_Lebi_Pro_Type.GetList("Parentid=" + Parentid + "", "Sort desc");

            }
            else
            {
                Parentid = id;
            }
            string str = "<select id=\"Pro_Type_id\" name=\"Pro_Type_id\" shop=\"true\" onchange=\"SelectProductType('Pro_Type_id');\" class=\"form-control\">";
            str += "<option value=\"0\" selected>" + Tag(" 请选择 ") + "</option>";
            foreach (Lebi_Pro_Type model in models)
            {
                if (id == model.id)
                    str += "<option value=\"" + model.id + "\" selected>" + Lang(model.Name) + "</option>";
                else
                    str += "<option value=\"" + model.id + "\">" + Lang(model.Name) + "</option>";
            }
            str += "</select>";
            str = CreateProductTypeSelect(Parentid) + str;
            return str;
        }
        private string CreateProductTypeSelect(int id)
        {
            string str = "<select id=\"ProductType_" + id + "\" name=\"ProductType_" + id + "\" shop=\"true\" onchange=\"SelectProductType('ProductType_" + id + "');\" class=\"form-control\">";
            Lebi_Pro_Type area = B_Lebi_Pro_Type.GetModel(id);
            if (area == null)
                return "";
            List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList("Parentid=" + area.Parentid + "", "Sort desc");
            if (models.Count == 0)
            {
                return "";
            }
            foreach (Lebi_Pro_Type model in models)
            {
                if (id == model.id)
                    str += "<option value=\"" + model.id + "\" selected>" + Lang(model.Name) + "</option>";
                else
                    str += "<option value=\"" + model.id + "\">" + Lang(model.Name) + "</option>";
            }
            str += "</select> ";
            str = CreateProductTypeSelect(area.Parentid) + str;
            return str;
        }
        /// <summary>
        /// 生成供应商商品分类选择框
        /// </summary>
        public void GetSupplierProductTypeList()
        {
            string ids = RequestTool.RequestString("id");
            int id = EX_Product.SuplierTypeid(ids);
            int supplierid = RequestTool.RequestInt("supplierid", 0);
            if (supplierid == 0)
            {
                Response.Write("");
                Response.End();
            }
            int Parentid = 0;
            string Types = "";
            Thread thread = new Thread(() => { Types = ThreadGetSupplierProductTypeList(ids, id, supplierid, Parentid); });
            thread.IsBackground = true;
            thread.Start();
            thread.Join();
            Response.Write(Types);
        }
        public string ThreadGetSupplierProductTypeList(string ids,int id,int supplierid,int Parentid)
        {
            List<Lebi_Supplier_ProductType> models = B_Lebi_Supplier_ProductType.GetList("parentid=" + id + " and Supplier_id=" + supplierid + "", "Sort desc");
            Lebi_Supplier_ProductType area = B_Lebi_Supplier_ProductType.GetModel(id);
            if (models.Count == 0)
            {
                if (area == null)
                    Parentid = 0;
                else
                    Parentid = area.parentid;
                models = B_Lebi_Supplier_ProductType.GetList("parentid=" + Parentid + " and Supplier_id=" + supplierid + "", "Sort desc");

            }
            else
            {
                Parentid = id;
            }
            string str = "<select id=\"Supplier_ProductType_ids\" name=\"Supplier_ProductType_ids\" shop=\"true\" onchange=\"SelectSupplierProductType(" + supplierid + ",'Supplier_ProductType_ids');\" class=\"form-control\">";
            str += "<option value=\"0\" selected>" + Tag(" 请选择 ") + "</option>";
            foreach (Lebi_Supplier_ProductType model in models)
            {
                if (id == model.id)
                    str += "<option value=\"" + model.id + "\" selected>" + Lang(model.Name) + "</option>";
                else
                    str += "<option value=\"" + model.id + "\">" + Lang(model.Name) + "</option>";
            }
            str += "</select>";
            str = CreateSupplierProductTypeSelect(supplierid,Parentid) + str;
            return str;
        }
        private string CreateSupplierProductTypeSelect(int supplierid,int id)
        {
            string str = "<select id=\"Supplier_ProductType_ids" + id + "\" name=\"Supplier_ProductType_ids" + id + "\" shop=\"true\" onchange=\"SelectSupplierProductType(" + supplierid + ",'Supplier_ProductType_ids" + id + "');\" class=\"form-control\">";
            Lebi_Supplier_ProductType area = B_Lebi_Supplier_ProductType.GetModel(id);
            if (area == null)
                return "";
            List<Lebi_Supplier_ProductType> models = B_Lebi_Supplier_ProductType.GetList("Parentid=" + area.parentid + " and Supplier_id=" + supplierid + "", "Sort desc");
            if (models.Count == 0)
            {
                return "";
            }
            foreach (Lebi_Supplier_ProductType model in models)
            {
                if (id == model.id)
                    str += "<option value=\"" + model.id + "\" selected>" + Lang(model.Name) + "</option>";
                else
                    str += "<option value=\"" + model.id + "\">" + Lang(model.Name) + "</option>";
            }
            str += "</select> ";
            str = CreateSupplierProductTypeSelect(supplierid,area.parentid) + str;
            return str;
        }
        /// <summary>
        /// 晒单加载更多
        /// </summary>
        /// <returns></returns>
        public void OrderShare()
        {
            int page = RequestTool.RequestInt("page", 1);
            int wap = RequestTool.RequestInt("wap", 0);
            List<Lebi_Comment> models = B_Lebi_Comment.GetList("TableName='Product' and Images!='' and Status=281", "id desc", 10, page);
            string str = "";
            foreach (Lebi_Comment model in models)
            {
                int Count = B_Lebi_Comment.Counts("TableName='Product' and Parentid = " + model.id);
                string[] images = model.Images.Split('@');
                str += "<div class=\"item\">";
                str += "<div class=\"itembox\">";
                str += "<div class=\"item_img\">";
                str += "<p><a href=\"" + URL("P_ProductCommentDetails", model.id) + "\"";
                if (wap == 0)
                {
                    str += " target=\"_blank\"";
                }
                str += ">";
                str += "<img src=\"" + Image(images[1]) + "\"></a></p>";
                str += "</div>";
                str += "<div class=\"item_content\">";
                str += "<h5>" + model.User_UserName + "</h5>";
                str += "<p>" + model.Content + "</p>";
                str += "</div>";
                str += "<div class=\"item_bottom\">";
                str += "<a class=\"leaveAMsg\" href=\"" + URL("P_ProductCommentDetails", model.id) + "\"";
                if (wap == 0)
                {
                    str += " target=\"_blank\"";
                }
                str += ">" + Count + "</a>";
                str += "</div>";
                str += "</div>";
                str += "</div>";
            }
            Response.Write(str);
        }

        /// <summary>
        /// 发送邮件验证码
        /// </summary>
        public void GetEmailCheckCode()
        {
            string email = RequestTool.RequestSafeString("email");
            CurrentUser.Email = email;
            CurrentUser.Language = CurrentLanguage.Code;
            string mcode = RequestTool.RequestString("m");
            try
            {
                int emailcount = B_Lebi_User.Counts("Email=lbsql{'" + email + "'} and IsCheckedEmail=1");
                if (emailcount > 0)
                {
                    Response.Write("{\"msg\":\"" + Tag("此邮箱已经注册") + "\"}");
                    return;
                }
                int Count = 0;
                string Count_ = CookieTool.GetCookie("GetEmailCheckCode_" + CurrentUser.Email).Get("Count");
                int.TryParse(Count_, out Count);
                if (Count > 5)
                {
                    Response.Write("{\"msg\":\"" + Tag("已超出当日使用次数") + "\"}");
                    return;
                }
                string servermcode = Session["mcode"] == null ? "" : (string)Session["mcode"];
                if (servermcode != mcode || servermcode == "")
                {
                    Response.Write("{\"msg\":\"" + Tag("发送失败，请刷新页面后重试") + "\"}");
                    return;
                }
                Email.SendEmail_checkcode(CurrentUser);
                Count++;
                NameValueCollection nvs = new NameValueCollection();
                nvs.Add("Count", Count.ToString());
                CookieTool.WriteCookie("GetEmailCheckCode_" + CurrentUser.Email, nvs, 1);
                Response.Write("{\"msg\":\"OK\"}");
            }
            catch
            {
                Response.Write("{\"msg\":\"" + Tag("发送失败，请联系客服") + "\"}");
            }

        }
        /// <summary>
        /// 发送手机验证码
        /// </summary>
        public void GetPhoneCheckCode()
        {
            string phone = RequestTool.RequestSafeString("phone");
            CurrentUser.MobilePhone = phone;
            CurrentUser.Language = CurrentLanguage.Code;
            string mcode = RequestTool.RequestString("m");
            try
            {
                int phonecount = B_Lebi_User.Counts("MobilePhone=lbsql{'" + phone + "'} and IsCheckedMobilePhone=1");
                if (phonecount > 0)
                {
                    Response.Write("{\"msg\":\"" + Tag("此手机号已经注册") + "\"}");
                    return;
                }
                int Count = 0;
                string Count_ = CookieTool.GetCookie("GetPhoneCheckCode_" + CurrentUser.MobilePhone).Get("Count");
                int.TryParse(Count_, out Count);
                if (Count > 5)
                {
                    Response.Write("{\"msg\":\"" + Tag("已超出当日使用次数") + "\"}");
                    return;
                }
                string servermcode = Session["mcode"] == null ? "1" : (string)Session["mcode"];
                if (servermcode != mcode || servermcode == "")
                {
                    Response.Write("{\"msg\":\"" + Tag("发送失败，请刷新页面后重试") + ""+ servermcode + "\"}");
                    return;
                }
                SMS.SendSMS_checkcode(CurrentUser);
                Count++;
                NameValueCollection nvs = new NameValueCollection();
                nvs.Add("Count", Count.ToString());
                CookieTool.WriteCookie("GetPhoneCheckCode_" + CurrentUser.MobilePhone, nvs, 1);
                Response.Write("{\"msg\":\"OK\"}");
            }
            catch
            {
                Response.Write("{\"msg\":\"" + Tag("发送失败，请联系客服") + "\"}");
            }
        }
        /// <summary>
        /// 商品价格
        /// </summary>
        public void ProducePrice()
        {
            int id = RequestTool.RequestInt("id");
            int count = RequestTool.RequestInt("count", 1);
            Lebi_Product product = GetProduct(id);
            decimal price = EX_Product.ProductPrice(product, CurrentUserLevel, CurrentUser, count);
            Response.Write(FormatMoney(price));
        }

        /// <summary>
        /// 侧边栏-购物车
        /// </summary>
        public void sidebar_basket()
        {
            Basket basket = new Basket(0);
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\\\"ibar_plugin_content\\\">");
            sb.Append(" <div class=\\\"ibar_cart_group ibar_cart_product\\\">");
            sb.Append("     <div class=\\\"ibar_cart_group_header\\\">");
            sb.Append("         <span class=\\\"ibar_cart_group_title\\\"></span><a href=\\\"" + URL("P_Basket", "") + "\\\">" + Tag("我的购物车") + "</a>");
            sb.Append("     </div>");
            sb.Append("     <ul>");
            foreach (Lebi_User_Product pro in basket.Products)
            {
                Lebi_Product model = GetProduct(pro.Product_id);
                string price = "";
                if (model.Type_id_ProductType == 323 && model.Time_Expired > System.DateTime.Now)
                    price = Tag("积分") + ":" + model.Price_Sale;
                else
                    price = FormatMoney(pro.Product_Price);

                sb.Append("         <li class=\\\"cart_item\\\">");
                sb.Append("             <div class=\\\"cart_item_pic\\\"><a href=\\\"" + URL("P_Product", pro.Product_id) + "\\\"><img src=\\\"" + Image(model.ImageOriginal) + "\\\" /></a></div>");
                sb.Append("             <div class=\\\"cart_item_desc\\\">");
                sb.Append("                 <a href=\\\"" + URL("P_Product", pro.Product_id) + "\\\" class=\\\"cart_item_name\\\">" + Lang(model.Name) + "</a>");
                sb.Append("                 <div class=\\\"cart_item_sku\\\"><span>" + Shop.Bussiness.EX_Product.ProPertyNameStr(model, CurrentLanguage.Code) + "</span></div>");
                sb.Append("                 <div class=\\\"cart_item_price\\\"><span class=\\\"cart_price\\\">" + price + "</span> ×"+ pro.count +"</div>");
                sb.Append("             </div>");

                sb.Append("         </li>");
            }
            sb.Append("     </ul>");
            sb.Append(" <div class=\\\"cart_handler\\\">");
            sb.Append("     <div class=\\\"cart_handler_header\\\"><span class=\\\"cart_handler_left\\\">" + Tag("商品数量") + "：<span class=\\\"cart_price\\\">" + basket.Count + "</span></span><span class=\\\"cart_handler_right\\\">" + FormatMoney(basket.Money_Product) + "</span></div>");
            sb.Append("     <a href=\\\"" + URL("P_Basket", "") + "\\\" class=\\\"cart_go_btn\\\" target=\\\"_blank\\\">" + Tag("去购物车结算") + "</a>");
            sb.Append(" </div>");
            sb.Append("</div>");
            sb.Append(" </div>");
            Response.Write("{\"title\":\"" + Tag("购物车") + "\",\"content\":\"" + sb.ToString() + "\"}");

        }

        /// <summary>
        /// 侧边栏-浏览历史
        /// </summary>
        public void sidebar_viewhistory()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\\\"ibar_plugin_content\\\">");
            sb.Append(" <div class=\\\"ibar-history-head\\\"><a href=\\\"javascript:void(0);\\\" onclick=\\\"UserProduct_Del('all',143);\\\">" + Tag("清空") + "</a></div>");
            sb.Append(" <div class=\\\"ibar-moudle-product\\\">");
            foreach (Lebi_User_Product pro in History_Product(10))
            {
                Lebi_Product productmodel = GetProduct(pro.Product_id);
                if (productmodel.id == 0)
                    continue;
                sb.Append("     <div class=\\\"imp_item\\\">");
                sb.Append("         <a href=\\\"" + URL("P_Product", pro.Product_id) + "\\\" class=\\\"pic\\\"><img src=\\\"" + Image(productmodel.ImageOriginal) + "\\\" width=\\\"100\\\" height=\\\"100\\\" /></a>");
                sb.Append("         <p class=\\\"tit\\\"><a href=\\\"" + URL("P_Product", pro.Product_id) + "\\\">" + Lang(productmodel.Name) + "</a></p>");
                sb.Append("         <p class=\\\"price\\\">" + FormatMoney(ProductPrice(productmodel)) + "</p>");
                sb.Append("         <a href=\\\"javascript:void(0);\\\" onclick=\\\"UserProduct_Edit(" + productmodel.id + ",142,1,'','');\\\" class=\\\"imp-addCart\\\">" + Tag("加入购物车") + "</a>");
                sb.Append("     </div>");
            }

            sb.Append(" </div>");
            sb.Append("</div>");


            Response.Write("{\"title\":\"" + Tag("我的足迹") + "\",\"content\":\"" + sb.ToString() + "\"}");
        }
        /// <summary>
        /// 侧边栏-收藏夹
        /// </summary>
        public void sidebar_userlike()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div class=\\\"ibar_plugin_content\\\">");
            //sb.Append(" <div class=\\\"ibar-history-head\\\">共3件产品<a href=\\\"#\\\">清空</a></div>");
            sb.Append(" <div class=\\\"ibar-moudle-product\\\">");
            List<Lebi_User_Product> pros = B_Lebi_User_Product.GetList("User_id=" + CurrentUser.id + " and Type_id_UserProductType=141", "id desc", 10, 1);
            foreach (Lebi_User_Product pro in pros)
            {
                Lebi_Product productmodel = GetProduct(pro.Product_id);
                if (productmodel.id == 0)
                    continue;
                sb.Append("     <div class=\\\"imp_item\\\">");
                sb.Append("         <a href=\\\"" + URL("P_Proudct", pro.Product_id) + "\\\" class=\\\"pic\\\"><img src=\\\"" + Image(productmodel.ImageOriginal) + "\\\" width=\\\"100\\\" height=\\\"100\\\" /></a>");
                sb.Append("         <p class=\\\"tit\\\"><a href=\\\"" + URL("P_Proudct", pro.Product_id) + "\\\">" + Lang(productmodel.Name) + "</a></p>");
                sb.Append("         <p class=\\\"price\\\">" + FormatMoney(ProductPrice(productmodel)) + "</p>");
                sb.Append("         <a href=\\\"javascript:void(0);\\\" onclick=\\\"UserProduct_Edit(" + productmodel.id + ",142,1,'','');\\\" class=\\\"imp-addCart\\\">" + Tag("加入购物车") + "</a>");
                sb.Append("     </div>");
            }

            sb.Append(" </div>");
            sb.Append("</div>");


            Response.Write("{\"title\":\"" + Tag("我的收藏") + "\",\"content\":\"" + sb.ToString() + "\"}");
        }

        /// <summary>
        /// 检查一个订单是否已经支付
        /// </summary>
        public void IsOrderPaid()
        {
            int id = RequestTool.RequestInt("id");
            Lebi_Order order = B_Lebi_Order.GetModel(id);
            if (order != null)
            {
                if (order.IsPaid == 1)
                {
                    Response.Write("{\"msg\":\"OK\"}");
                    return;
                }
            }
            Response.Write("{\"msg\":\"NO\"}");
        }
        /// <summary>
        /// 文章阅读一次
        /// </summary>
        public void PageView()
        {
            int id = RequestTool.RequestInt("id");
            Lebi_Page model = B_Lebi_Page.GetModel(id);
            if (model != null)
            {

                model.Count_Views++;
                B_Lebi_Page.Update(model);
            }
            Response.Write("{\"msg\":\"NO\"}");
        }
    }


}