using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Web.Script.Serialization;
namespace Shop.Admin.storeConfig
{
    public partial class Transport_Price_Edit_window : AdminAjaxBase
    {
        protected Lebi_Transport_Price model;
        protected Lebi_Transport tmodel;
        protected List<Lebi_Transport_Container> conts;
        protected List<KeyValue> kvs;
        protected string Containestr;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("transport_price_add", "添加配送区域"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("transport_price_edit", "编辑配送区域"))
                {
                    WindowNoPower();
                }
            }
            int tid = RequestTool.RequestInt("tid", 0);
            model = B_Lebi_Transport_Price.GetModel(id);
            if (model == null)
            {
                tmodel = B_Lebi_Transport.GetModel(tid);
                model = new Lebi_Transport_Price();
            }
            else
            {
                tmodel = B_Lebi_Transport.GetModel(model.Transport_id);
            }


            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                kvs = jss.Deserialize<List<KeyValue>>(model.Container);
            }
            catch
            {
                kvs = new List<KeyValue>();
            }
            //生成货柜价格编辑区
            conts = B_Lebi_Transport_Container.GetList("", "Volume desc");

            foreach (Lebi_Transport_Container cont in conts)
            {
                KeyValue kv;
                try
                {
                    kv = (from m in kvs
                                   where m.K == cont.id.ToString()
                                   select m).ToList().FirstOrDefault();
                }
                catch (System.ArgumentNullException ex)
                {
                    kv = null;
                }
                bool flag = false;
                if (kv != null)
                {
                    flag = true;
                }
                else
                {
                    kv = new KeyValue();
                    
                }
                string price_ = kv.V.ToString();
                decimal price = 0;
                decimal.TryParse(price_, out price);
                Containestr += "<tr class=\"list\">";
                Containestr += "<td><input type=\"checkbox\" name=\"Containerid\" shop=\"true\" value=\"" + cont.id + "\" " + (flag ? "checked" : "") + " /></td>";
                Containestr += "<td>" + cont.Name + "</td>";
                Containestr += "<td>";
                Containestr += " <input type=\"text\" class=\"input\" shop=\"true\" name=\"ContainerPrice" + cont.id + "\" id=\"ContainerPrice" + cont.id + "\" value=\"" + FormatMoney(price, "Number") + "\" style=\"width: 100px;\" onkeyup=\"value=value.replace(/[^\\d]/g,'')\" /> <span></span>";
                Containestr += "</td></tr>";
            }
        }
        public string AreaName(int id)
        {
            string str = "";
            Lebi_Area area = B_Lebi_Area.GetModel(id);
            if (area != null)
            {
                str = area.Name + "> ";
                if (area.Parentid > 0)
                {
                    str = AreaName(area.Parentid) + str;
                }
            }
            return str;
        }

    }
}