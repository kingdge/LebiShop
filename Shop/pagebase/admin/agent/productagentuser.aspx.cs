using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.agent
{
    public partial class productagentuser : AdminPageBase
    {
        protected string key;
        protected List<Lebi_Agent_Product_User> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {

            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            string where = "1=1";
            if (key != "")
                where += " and User_UserName like '%" + key + "%'";
            models = B_Lebi_Agent_Product_User.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Agent_Product_User.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);
            
        }
        /// <summary>
        /// 返回代理级别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetLevelName(int id)
        {
            Lebi_Agent_Product_Level level = B_Lebi_Agent_Product_Level.GetModel(id);
            if (level == null)
                level = new Lebi_Agent_Product_Level();
            return level.Name;
        }

        public int UserAgentProductCount(int userid)
        {
            int count = B_Lebi_Agent_Product.Counts("User_id="+userid+"");
            return count;
        }
    }
}