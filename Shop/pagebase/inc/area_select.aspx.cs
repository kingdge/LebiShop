using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;

namespace Shop.inc
{
    public partial class area_select : Bussiness.ShopPage
    {
        protected int topid;
        protected int area_id;
        //protected List<Lebi_Area> models;
        public void LoadPage()
        {
            topid = RequestTool.RequestInt("topid", 0);
            area_id = RequestTool.RequestInt("area_id", 0);

            int Parentid = 0;
            area_id = area_id == 0 ? topid : area_id;
            List<Lebi_Area> models = B_Lebi_Area.GetList("Parentid=" + area_id + "", "Sort asc");
            Lebi_Area area = B_Lebi_Area.GetModel(area_id);
            if (models.Count == 0)
            {
                Parentid = area.Parentid;
                models = B_Lebi_Area.GetList("Parentid=" + Parentid + "", "Sort asc");

            }
            else
            {
                Parentid = area_id;
            }
            string str = "<select id=\"Area_id\" onchange=\"SelectAreaList(" + topid + ",'Area_id');\">";
            str += "<option value=\"0\" selected>" + Tag(" 请选择 ") + "</option>";
            foreach (Lebi_Area model in models)
            {
                if (area_id == model.id)
                    str += "<option value=\"" + model.id + "\" selected>" + model.Name + "</option>";
                else
                    str += "<option value=\"" + model.id + "\">" + model.Name + "</option>";
            }
            str += "</select> ";
            //if (models.Count > 0)
            //    str += CreatSelect(models.FirstOrDefault().id);
            str = CreatSelect(Parentid) + str;
            Response.Write(str);
        }

        private string CreatSelect(int id)
        {
            string str = "<select id=\"Area_" + id + "\"  onchange=\"SelectAreaList(" + topid + ",'Area_" + id + "');\">";
            Lebi_Area area = B_Lebi_Area.GetModel(id);
            if (area == null)
                return "";
            if (topid == area.id)
            {
                return "";
            }
            List<Lebi_Area> models = B_Lebi_Area.GetList("Parentid=" + area.Parentid + "", "Sort asc");
            if (models.Count == 0)
            {
                return "";
            }
            foreach (Lebi_Area model in models)
            {
                if (id == model.id)
                    str += "<option value=\"" + model.id + "\" selected>" + model.Name + "</option>";
                else
                    str += "<option value=\"" + model.id + "\">" + model.Name + "</option>";
            }
            str += "</select> ";
            str = CreatSelect(area.Parentid) + str;
            return str;
        }

    }
}