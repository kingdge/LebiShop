using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using LB.DataAccess;
using System.Collections.Specialized;

namespace Shop.statis
{

    public partial class userlevel : AdminPageBase
    {
        protected List<Lebi_UserLevel> models;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("statis_userlevel", "分组统计"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            models = B_Lebi_UserLevel.GetList("Grade > 0", "Grade asc");
        }
    }
    
}