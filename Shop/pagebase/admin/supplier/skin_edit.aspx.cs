using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Admin.Supplier
{
    public partial class Skin_Edit : AdminPageBase
    {
        protected Lebi_Supplier_Skin model;
        protected string SkinContent = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            int id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("supplier_skin_add", "添加店铺皮肤"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
            }
            else
            {
                if (!EX_Admin.Power("supplier_skin_edit", "编辑店铺皮肤"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
            }
            
            model = B_Lebi_Supplier_Skin.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Supplier_Skin();
            }
            SkinContent = GetSkinStr(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="skin"></param>
        /// <returns></returns>
        private string GetSkinStr(Lebi_Supplier_Skin skin)
        {
            string str = "";
            string path = skin.Path.TrimEnd('/') + "/index.html";
            path = ThemeUrl.GetFullPath(path);
            str = HtmlEngine.ReadTxt(path);

            return str;

        }
    }
}