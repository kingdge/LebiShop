using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.Config
{
    public partial class ImageSize : AdminPageBase
    {
        protected List<Lebi_ImageSize> models;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("CreatImageBySize", "生成图片规格"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            string where = "1=1";
            models = B_Lebi_ImageSize.GetList(where, "");
        }

        public int Count(Lebi_ImageSize m)
        {
            string where = "[TableName]='Product' and [Size] not like '%" + m.Width + "$" + m.Height + "%'";
            //string where = "TableName='Product' and Time_Update<'" + m.Time_Add + "'";
            int c = B_Lebi_Image.Counts(where);
            return c;
        }
    }
}