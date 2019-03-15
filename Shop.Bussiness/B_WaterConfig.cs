using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Collections.Generic;
using LB.Tools;
using Shop.Model;using DB.LebiShop;

namespace Shop.Bussiness
{
    /// <summary>
    /// BaseConfigXML 的摘要说明
    /// </summary>
    public class B_WaterConfig : Page
    {
        private string xmlpath;
        public B_WaterConfig()
        {
            xmlpath = Server.MapPath("~/config/WaterConfig.xml");
        }


        public string XMLRead(string Value)
        {

            XmlDocument xd = new XmlDocument();
            xd.Load(this.xmlpath);

            XmlNodeList xnl = xd.GetElementsByTagName(Value);
            if (xnl.Count == 0)
                return "";
            else
            {
                XmlNode mNode = xnl[0];
                return mNode.InnerText;
            }
        }

        public WaterConfig LoadConfig() 
        {

            WaterConfig model = new WaterConfig();
            model = (WaterConfig)SerializationHelper.Load(model.GetType(), this.xmlpath);
            return model;
        }
        public bool SaveConfig(WaterConfig model)
        {
            return SerializationHelper.Save(model, this.xmlpath);
        }

    }
}