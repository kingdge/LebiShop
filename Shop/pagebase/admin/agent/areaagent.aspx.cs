using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Text;
namespace Shop.Admin.agent
{
    public partial class areaagent : AdminPageBase
    {
        protected string key;
        //protected List<Lebi_Agent_Area> models;
        protected string PageString;
        protected int status;
        protected int TopAreaid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            status = RequestTool.RequestInt("status", 0);
            string where = "1=1";
            if (key != "")
                where += " and User_UserName like '%" + key + "%'";
            if (status > 0)
            {
                if (status == 1)
                    where += " and User_id>0";
                if (status == 2)
                    where += " and User_id=0";
            }
            //models = B_Lebi_Agent_Area.GetList(where, "id desc", PageSize, page);
            //int recordCount = B_Lebi_Agent_Area.Counts(where);
            //PageString = Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);
            TopAreaid = Convert.ToInt32(SYS.TopAreaid);
        }
        public string GetCOrder(int id)
        {
            Lebi_CardOrder order = B_Lebi_CardOrder.GetModel(id);
            if (order == null)
                order = new Lebi_CardOrder();
            return order.IndexCode + "-" + order.Money;
        }

        public string GetAreas(int pid, int deep, string tag = "")
        {
            StringBuilder sb = new StringBuilder();
            List<Lebi_Area> areas = B_Lebi_Area.GetList("Parentid=" + pid + "", "Sort desc");
            Lebi_Agent_Area model;
            string where = "";
            foreach (Lebi_Area area in areas)
            {
                if (!IsShow(area.id))
                    continue;
                where = "Area_id=" + area.id + "";
                //if (key != "")
                //    where += " and User_UserName like '%" + key + "%'";
                //if (status > 0)
                //{
                //    if (status == 1)
                //        where += " and User_id>0";
                //    if (status == 2)
                //        where += " and User_id=0";
                //}
                model = B_Lebi_Agent_Area.GetModel(where);
                if (model == null)
                {
                    sb.Append("<tr class=\"list\" " + tag + "><td>&nbsp;</td><td colspan=\"10\">" + deepstr(deep) + "<img id=\"img" + area.id + "\" onclick=\"ShowChild(" + area.id + ")\" src=\""+AdminImage("minus.gif")+"\"/> " + area.Name + "</td></tr>");
                    string newtag = "";
                    if (tag == "")
                        newtag = "t" + area.id + "=\"true\"";
                    else
                        newtag = tag + " t" + area.id + "=\"true\"";
                    sb.Append(GetAreas(area.id, deep + 1, newtag));
                }
                else
                {
                    string style="";
                    if (model.User_id > 0 && model.Time_end > System.DateTime.Now)
                        style = "style=\"color:red;\"";

                    sb.Append("<tr class=\"list\" " + tag + " ondblclick=\"Edit(" + model.id + ")\">");
                    sb.Append("<td class=\"center\"><input type=\"checkbox\" value=\"" + model.id + "\" name=\"sid\" /></td>");
                    sb.Append("<td " + style + ">" + deepstr(deep) + "<img id=\"img" + area.id + "\" src=\"" + AdminImage("minus.gif") + "\" onclick=\"ShowChild(" + area.id + ")\"/> " + Shop.Bussiness.EX_Area.GetAreaName(model.Area_id, 1) + "</td>");
                    sb.Append("<td>" + model.User_UserName + "</td>");
                    sb.Append("<td>" + DefaultCurrency.Msige + " " + model.Price + "</td>");
                    sb.Append("<td>" + model.Commission_1 + "</td>");
                    sb.Append("<td>" + model.Commission_2 + "</td>");
                    sb.Append("<td>" + GetCOrder(model.CardOrder_id) + "</td>");
                    sb.Append("<td>" + model.Time_add.ToShortDateString() + "</td>");
                    sb.Append("<td>" + model.Time_end.ToShortDateString() + "</td>");
                    sb.Append("<td>" + (model.IsFailure == 1 ? Tag("失效") : Tag("正常")) + "</td>");
                    sb.Append("<td><a href=\"javascript:void(0)\" onclick=\"Edit(" + model.id + ")\">" + Tag("编辑") + "</a>");
                    sb.Append(" | <a href=\"javascript:void(0)\" onclick=\"AreaAgentUser_Edit(" + model.id + ")\">" + Tag("绑定") + "</a>");
                    sb.Append(" | <a href=\"agentmoney.aspx?areaid=" + model.Area_id + "\">" + Tag("查看佣金") + "</a>");
                    sb.Append("</td></tr>");
                }
            }
            return sb.ToString();
        }


        public bool IsShow(int id)
        {
            bool flag = false;
            string sql1 = "Area_id =" + id;
            string sql2 = "select id from Lebi_Area where Parentid=" + id + "";
            string sql3 = "select id from Lebi_Area where Parentid in (" + sql2 + ")";
            string sql4 = "select id from Lebi_Area where Parentid in (" + sql3 + ")";
            int count = B_Lebi_Agent_Area.Counts(sql1 + " or Area_id in (" + sql2 + ")" + " or Area_id in (" + sql3 + ")" + " or Area_id in (" + sql4 + ")");
            if (count > 0)
                flag = true;
            return flag;
        }
        private string deepstr(int deep)
        {
            string str = "";
            for (int i = 0; i < deep; i++)
            {
                str += "&nbsp;&nbsp;&nbsp;";
            }
            return str;
        }
    }
}