using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Net;

namespace Shop.Admin.storeConfig
{
    public partial class BackUPConfig : AdminAjaxBase
    {
        protected BaseConfig model;
        protected bool IsEnable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("backup_config", "数据备份配置"))
            {
                WindowNoPower();
            }
            B_BaseConfig bconfig=new B_BaseConfig();
            //model = bconfig.LoadConfig();
            model = ShopCache.GetBaseConfig();
            IsEnable = CheckServer();
        }

        public bool CheckServer()
        {
            string sql =System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ToString();
            sql = RegexTool.GetRegValue(sql, "data source=(.*?);");
            if (sql == ".")
                sql = "localhost";
            sql = sql.Replace(@"\","/");
            if (sql.Contains("/"))
            {
                sql = sql.Substring(0,sql.IndexOf("/"));
            }
            IPAddress[] IPs = Dns.GetHostAddresses(sql);
            string webserverip = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[0].ToString();
            bool res = false;
            foreach (IPAddress ip in IPs)
            {
                if (ip.ToString().Trim() == webserverip.Trim())
                    res = true;
            }
            return res;
        }
    }
}