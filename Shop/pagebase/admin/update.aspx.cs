using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.IO;
namespace Shop.Admin
{
    public partial class update : AdminAjaxBase
    {
        public Lebi_Version model;
        public bool IsDown = false;
        protected void Page_Load(object sender, EventArgs e)
        {

            int id = RequestTool.RequestInt("id");
            model = B_Lebi_Version.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }

            string ServerPath = System.Web.HttpContext.Current.Server.MapPath("~/");
            string Path = ServerPath + model.Path_rar;
            if (System.IO.File.Exists(Path))  
                IsDown=true;

        }
        
    }
}