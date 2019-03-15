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

namespace Shop.Admin.Ajax
{
    /// <summary>
    /// 快递单模板定位数据的操作
    /// </summary>
    public partial class Ajax_express : AdminAjaxBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = RequestTool.RequestString("str");
            int id = int.Parse(str.Split(new char[] { '|' })[0]);
            string pos;
            string pos_name;
            string pos_body;
            Lebi_Express model = B_Lebi_Express.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"参数错误" + id + "\"}");
                return;
            }
            pos = model.Pos;
            Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?><data>");
            Response.Write("<mobid>" + id + "</mobid><mobname>" + model.Name + "</mobname><mobwidth>" + model.Width + "</mobwidth><mobheight>" + model.Height + "</mobheight><bgsrc>swf/expressmenu.png</bgsrc><orderc>" + site.WebPath + model.Background + "</orderc><saveurl>" + site.AdminPath + "/ajax/ajax_order.aspx?__Action=Express_Edit&amp;id=" + id + "</saveurl>");
            if (pos != "" && pos != "{}")
            {
                pos = pos.Substring(1);
                pos = pos.Substring(0, pos.Length - 1);
                Response.Write("<ipos>");
                if (pos.IndexOf("},") > -1)
                {
                    string[] arr_pos;
                    arr_pos = pos.Replace("},", "$").Split(new char[] { '$' });
                    foreach (string arr_pos_i in arr_pos)
                    {
                        pos_name = arr_pos_i.Replace(":{", "$").Split(new char[] { '$' })[0];
                        pos_name = pos_name.Substring(1);
                        pos_name = pos_name.Substring(0, pos_name.Length - 1);
                        Response.Write("<item name='" + pos_name + "' text='" + pos_name + "'><![CDATA[{");
                        pos_body = arr_pos_i.Replace(":{", "$").Split(new char[] { '$' })[1];
                        Response.Write("" + pos_body + "");
                        if (pos_body.Substring(pos_body.Length - 1) == "}")
                        {
                        }
                        else
                        {
                            Response.Write("}");
                        }
                        Response.Write("]]></item>");
                    }
                }
                else
                {
                    pos_name = pos.Replace(":{", "$").Split(new char[] { '$' })[0];
                    pos_name = pos_name.Substring(1);
                    pos_name = pos_name.Substring(0, pos_name.Length - 1);
                    Response.Write("<item name='" + pos_name + "' text='" + pos_name + "'><![CDATA[{");
                    pos_body = pos.Replace(":{", "$").Split(new char[] { '$' })[1];
                    Response.Write("" + pos_body + "]]></item>");
                }
                Response.Write("</ipos>");
            }
            Response.Write("</data>");
        }
    }
}