using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.order
{
    public partial class Express_Print : AdminAjaxBase
    {
        protected Lebi_Express model;
        protected Lebi_Express_Shipper express_shipper;
        protected List<Lebi_Order_Product> pros;
        protected string id;
        protected int Tid;
        protected int Eid;
        protected string ProductList;
        protected List<Lebi_Order> models;
        protected Shop.Model.ExpressPrint sp;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("express_log_print", "打印打印清单"))
            {
                PageNoPower();
            }
            id = RequestTool.RequestString("id");
            Tid = RequestTool.RequestInt("Tid", 0);
            Eid = RequestTool.RequestInt("Eid", 0);
            model = B_Lebi_Express.GetModel("Transport_Id = " + Tid);
            if (model == null)
            {
                model = new Lebi_Express();
            }
            express_shipper = B_Lebi_Express_Shipper.GetModel("Status = 1");
            if (express_shipper == null)
            {
                express_shipper = new Lebi_Express_Shipper();
            }
            models = B_Lebi_Order.GetList("id in (lbsql{" + id + "})", "id desc", PageSize, page);

        }

        public void PrintTable()
        {
            ProductList = "";
            foreach (Lebi_Order order in models)
            {
                if (model.Pos == null || model.Pos == "")
                {
                    Response.Write(Tag("请先在快递单模板中关联配送方式"));
                    Response.End();
                }

                Response.Write("<div class=\"box\">");

                string pos;
                string pos_name;
                string pos_body;
                string strTest;
                foreach (Lebi_Order_Product pro in B_Lebi_Order_Product.GetList("Order_id=" + order.id + "", ""))
                {
                    ProductList += pro.Product_Number + " " + Shop.Bussiness.Language.Content(pro.Product_Name, CurrentLanguage) + " × " + pro.Count + "\n";
                }
                pos = model.Pos;
                pos = pos.Substring(1);
                pos = pos.Substring(0, pos.Length - 1);
                if (pos.IndexOf("},") > -1)
                {
                    string[] arr_pos;
                    arr_pos = pos.Replace("},", "$").Split(new char[] { '$' });
                    foreach (string arr_pos_i in arr_pos)
                    {
                        pos_name = arr_pos_i.Replace(":{", "$").Split(new char[] { '$' })[0];
                        pos_name = pos_name.Substring(1);
                        pos_name = pos_name.Substring(0, pos_name.Length - 1);
                        strTest = "{";
                        pos_body = arr_pos_i.Replace(":{", "$").Split(new char[] { '$' })[1];
                        strTest += pos_body;
                        if (pos_body.Substring(pos_body.Length - 1) == "}")
                        {
                        }
                        else
                        {
                            strTest += "}";
                        }
                        sp = Shop.Bussiness.B_ExpressPrint.GetModel(strTest);
                        Response.Write("<div style=\"POSITION: absolute;left:" + sp.x + "px;top:" + sp.y + "px;font-family:'" + sp.font + "';color:" + sp.color + ";font-size:" + sp.size + "px;width:" + sp.width + "px;height:" + sp.height + "px;text-align:" + sp.align + ";");
                        if (sp.underline == "true")
                        {
                            Response.Write("text-decoration:underline;");
                        }
                        else
                        {
                            Response.Write("text-decoration:none;");
                        }
                        if (sp.isbold == "true")
                        {
                            Response.Write("font-weight:bold;");
                        }
                        else
                        {
                            Response.Write("font-weight:normal;");
                        }
                        if (sp.italic == "true")
                        {
                            Response.Write("font-style:italic;");
                        }
                        else
                        {
                            Response.Write("font-style:normal;");
                        }
                        Response.Write("overflow:hidden\">");
                        if (sp.text == "收货人-姓名")
                        {
                            Response.Write(order.T_Name);
                        }
                        else if (sp.text == "收货人-地区（一级）")
                        {
                            Response.Write(Shop.Bussiness.EX_Area.GetAreaName(order.T_Area_id, 0, 1));
                        }
                        else if (sp.text == "收货人-地区（二级）")
                        {
                            Response.Write(Shop.Bussiness.EX_Area.GetAreaName(order.T_Area_id, 0, 2));
                        }
                        else if (sp.text == "收货人-地区" || sp.text == "收货人-地区（三级）")
                        {
                            Response.Write(Shop.Bussiness.EX_Area.GetAreaName(order.T_Area_id, 0, 3));
                        }
                        else if (sp.text == "收货人-地区（四级）")
                        {
                            Response.Write(Shop.Bussiness.EX_Area.GetAreaName(order.T_Area_id, 0, 4));
                        }
                        else if (sp.text == "收货人-地址")
                        {
                            Response.Write(Shop.Bussiness.EX_Area.GetAreaName(order.T_Area_id) +" "+order.T_Address);
                        }
                        else if (sp.text == "收货人-手机")
                        {
                            Response.Write(order.T_MobilePhone);
                        }
                        else if (sp.text == "收货人-电话")
                        {
                            Response.Write(order.T_Phone);
                        }
                        else if (sp.text == "收货人-邮编")
                        {
                            Response.Write(order.T_Postalcode);
                        }
                        else if (sp.text == "收货人-备注")
                        {
                            Response.Write(order.Remark_Admin);
                        }
                        else if (sp.text == "发货人-姓名")
                        {
                            Response.Write(express_shipper.UserName);
                        }
                        else if (sp.text == "发货人-地区")
                        {
                            Response.Write(express_shipper.City);
                        }
                        else if (sp.text == "发货人-地址")
                        {
                            Response.Write(express_shipper.Address);
                        }
                        else if (sp.text == "发货人-电话")
                        {
                            Response.Write(express_shipper.Tel);
                        }
                        else if (sp.text == "发货人-邮编")
                        {
                            Response.Write(express_shipper.ZipCode);
                        }
                        else if (sp.text == "发货人-手机")
                        {
                            Response.Write(express_shipper.Mobile);
                        }
                        else if (sp.text == "网店名称")
                        {
                            Response.Write(express_shipper.SiteName);
                        }
                        else if (sp.text == "自定义内容")
                        {
                            Response.Write(express_shipper.Remark);
                        }
                        else if (sp.text == "订单-总金额")
                        {
                            Response.Write(order.Money_Order);
                        }
                        else if (sp.text == "订单-总重量")
                        {
                            Response.Write(order.Weight);
                        }
                        else if (sp.text == "订单-订单编号")
                        {
                            Response.Write(order.Code);
                        }
                        else if (sp.text == "订单-商品明细")
                        {
                            Response.Write(ProductList);
                        }
                        else if (sp.text == "订单-备注")
                        {
                            Response.Write(order.Remark_Admin);
                        }
                        else if (sp.text == "√")
                        {
                            Response.Write("√");
                        }
                        Response.Write("</div>");
                    }
                }
                Response.Write("</div>");
            }

        }
    }
}