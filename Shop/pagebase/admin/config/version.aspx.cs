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
    public partial class Version : AdminPageBase
    {
        protected List<Lebi_Version> models;
        protected string PageString;
        protected bool power=false;//是否有权使用一键升级
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("version", "版本管理"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            string where = "1=1";

            PageSize = RequestTool.getpageSize(25);

            models = B_Lebi_Version.GetList(where, "Version desc,Version_Son desc", PageSize, page);
            int recordCount = B_Lebi_Language.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}", page, PageSize, recordCount);

            //if (Shop.LebiAPI.Service.Instanse.Check("zaixianshengji"))
            //{
            //    power = true;
            //}
        }
        /// <summary>
        /// 检测升级文件是否存在
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool IsDown(Lebi_Version v)
        {
            string ServerPath = System.Web.HttpContext.Current.Server.MapPath("~/");
            string Path = ServerPath + v.Path_rar;
            if (System.IO.File.Exists(Path))   //如果路径不存在，则创建
                return true;
            else
                return false;
        }
        public void LicenseWord()
        {
            //if (!power)
            //{
                Response.Write("<div class=\"licensealt\">");
                Response.Write("<p class=\"title\">手动升级步骤</p>1）下载升级文件；2）使用ftp工具上传升级文件至相应路径；3）刷新页面,出现“升级”按钮；4）点击“升级”按钮,升级完毕。");
                //Response.Write("<p class=\"title\">注册并在您的商城后台<a href=\"license.aspx\" target=\"_blank\">帐户设置</a>配置好帐户信息即可使用一键升级功能，<a href=\"" + Shop.LebiAPI.Service.Instanse.weburl + "/user/register.html\" target=\"_bank\">点此注册</a>。</p>");
                //Response.Write("<p>在增值-><a href=\"license.aspx\" target=\"_blank\">帐户设置</a>中配置好您的Lebi帐户信息即可使用一键升级功能，<a href=\"" + Shop.LebiAPI.Service.Instanse.weburl + "/user/register.html\" target=\"_bank\">点此注册</a>。</p>");
                //Response.Write("<p class=\"title\">立即免费激活我的在线升级服务<a href=\"" + Shop.LebiAPI.Service.Instanse.weburl + "/free/\" target=\"_bank\">" + Shop.LebiAPI.Service.Instanse.weburl + "/free/</a>。</p>");
                Response.Write("</div>");
            //}
        }
    }
}