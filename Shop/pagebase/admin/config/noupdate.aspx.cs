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

namespace Shop.Admin.config
{
    public partial class noupdate : AdminPageBase
    {
        protected string SkinContent;
        protected string FileName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("fileedit", "文件编辑"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            FileName = "config/noupdate.txt";
            SkinContent = GetSkinStr(FileName);

        }

        /// <summary>
        /// 取得一个文件的内容
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="skin"></param>
        /// <returns></returns>
        private string GetSkinStr(string filename)
        {
            string str = "";
            string path = "/" + filename;
            if (filename.ToLower().Contains("web.config"))
                str = "";
            else
                str = HtmlEngine.ReadTxt(path);

            return str;

        }
    }
}