using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.admin
{
    public partial class Administrator_Edit : AdminPageBase
    {
        protected Lebi_Administrator model;
        protected List<Lebi_Admin_Group> groups;
        protected string ImageUrl;
        protected string AvatarUrl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("admin_edit", "编辑系统用户"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Administrator.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Administrator();
                model.Type_id_AdminStatus = 230;
            }
            if (model.Avatar != "")
            {
                ImageUrl = model.Avatar.Replace("small", "original");
                AvatarUrl = model.Avatar;
            }
            else
            {
                ImageUrl = WebPath + "/theme/system/systempage/admin/images/Avatar.jpg";
                AvatarUrl = WebPath + "/theme/system/systempage/admin/images/Avatar.jpg";
            }
            
            groups = B_Lebi_Admin_Group.GetList("","Sort desc");
            
        }
        /// <summary>
        ///  管理分类选择
        /// </summary>
        /// <param name="InputName"></param>
        /// <param name="ids"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public string Pro_TypeCheckbox(string InputName, string ids, string lang)
        {
            List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList("Parentid = 0", "Sort desc");
            string str = "";
            foreach (Lebi_Pro_Type model in models)
            {
                string sel = "";
                if (("," + ids + ",").IndexOf("," + model.id + ",") > -1)
                    sel = "checked";
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-checkbox m-r-20\"><input type=\"checkbox\" id=\"" + InputName + "" + model.id + "\" name=\"" + InputName + "\" value=\"" + model.id + "\" class=\"custom-control-input\" shop=\"true\" " + sel + "><span class=\"custom-control-label\">" + Lang(model.Name) + "</span></label>";
                }
                else
                {
                    str += "<label><input type=\"checkbox\" name=\"" + InputName + "\" id=\"" + InputName + "\" shop=\"true\" value=\"" + model.id + "\" " + sel + ">" + Lang(model.Name) + "&nbsp;</label>";
                }
            }
            return str;
        }
        /// <summary>
        ///  管理项目选择
        /// </summary>
        /// <param name="InputName"></param>
        /// <param name="ids"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public string ProjectCheckbox(string InputName, string ids)
        {
            List<Lebi_Project> models = B_Lebi_Project.GetList("", "Sort desc");
            string str = "";
            foreach (Lebi_Project model in models)
            {
                string sel = "";
                if (("," + ids + ",").IndexOf("," + model.id + ",") > -1)
                    sel = "checked";
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-checkbox m-r-20\"><input type=\"checkbox\" id=\"" + InputName + "" + model.id + "\" name=\"" + InputName + "\" value=\"" + model.id + "\" class=\"custom-control-input\" shop=\"true\" " + sel + "><span class=\"custom-control-label\">" + model.Name + "</span></label>";
                }
                else
                {
                    str += "<label><input type=\"checkbox\" name=\"" + InputName + "\" id=\"" + InputName + "\" shop=\"true\" value=\"" + model.id + "\" " + sel + ">" + model.Name + "&nbsp;</label>";
                }
            }
            return str;
        }
    }
}