using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.storeConfig
{
    public partial class Area : AdminPageBase
    {
        protected string key;
        protected List<Lebi_Area> models;
        protected int pid;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("area_list", "地区列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            pid = RequestTool.RequestInt("pid",0);
            key = RequestTool.RequestString("key");
            PageSize = RequestTool.getpageSize(25);
            string where = "Parentid=" + pid;
            //if (pid > 0)
            //    where += " and Parentid="+pid;
            if (key != "")
                where += " and [Name] like lbsql{'%" + key + "%'}";
            //PageSize = 5;
            models = B_Lebi_Area.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Area.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&pid=" + pid + "&key=" + key, page, PageSize, recordCount);
            
        }

        protected int Count(int id)
        {
            int count = 0;
            count = B_Lebi_Area.Counts("Parentid="+id);
            return count;
        }

        protected string Getpath(int id)
        {
            string str = "";
            Lebi_Area area = B_Lebi_Area.GetModel(id);
            if (area != null) {
                str = " > <a href=\"?pid="+id+"\">"+area.Name+"</a>";
                if (area.Parentid > 0)
                {
                    str = Getpath(area.Parentid) + str;
                }
            }
            return str;
        }

    }
}