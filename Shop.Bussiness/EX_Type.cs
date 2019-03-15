using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.Script.Serialization;

namespace Shop.Bussiness
{
    public class EX_Type
    {
        /// <summary>
        /// 返回类型名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string TypeName(int id)
        {
            Lebi_Type model = B_Lebi_Type.GetModel(id);
            if (model != null)
            {
                if (model.Color != "")
                    return "<font style=\"color:" + model.Color + "\">" + model.Name + "</font>";
                else
                    return model.Name;
            }
            return "";
        }
        public static string TypeName(int id, string lang)
        {
            Lebi_Type model = B_Lebi_Type.GetModel(id);
            if (model != null)
            {
                if (model.Color != "")
                    return "<font style=\"color:" + model.Color + "\">" + Language.Tag(model.Name, lang) + "</font>";
                else
                    return Language.Tag(model.Name, lang);
            }
            return "";
        }
        public static string TypeName(int id, Lebi_Language_Code lang)
        {
            return TypeName(id, lang.Code);
        }
        /// <summary>
        /// 返回类型下拉框内容
        /// </summary>
        /// <param name="class_"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string TypeOption(string class_, int id)
        {
            string str = "";
            foreach (Lebi_Type model in GetTypes(class_))
            {
                string sel = "";
                if (id == model.id)
                    sel = "selected";
                str += "<option value=\"" + model.id + "\" " + sel + ">" + model.Name + "</option>";
            }
            return str;

        }
        public static string TypeOption(string class_, int id, string lang)
        {
            string str = "";
            foreach (Lebi_Type model in GetTypes(class_))
            {
                string sel = "";
                if (id == model.id)
                    sel = "selected";
                str += "<option value=\"" + model.id + "\" " + sel + ">" + Language.Tag(model.Name, lang) + "</option>";
            }
            return str;
        }
        public static string TypeOption(string class_, int id, Lebi_Language_Code lang)
        {
            return TypeOption(class_, id, lang.Code);
        }
        /// <summary>
        /// 返回复选框内容
        /// </summary>
        /// <param name="class_"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string TypeCheckbox(string class_, string name, string id, string ext, string lang)
        {
            string str = "";
            foreach (Lebi_Type model in GetTypes(class_))
            {
                string sel = "";
                if (("," + id + ",").Contains("," + model.id + ","))
                    sel = "checked";
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-checkbox m-r-20\"><input type=\"checkbox\" id=\"" + name + "" + model.id + "\" name=\"" + name + "\" value=\"" + model.id + "\" class=\"custom-control-input\" shop=\"true\" " + sel + " " + ext + "><span class=\"custom-control-label\">" + Language.Tag(model.Name, lang) + "</span></label>";
                }
                else
                {
                    str += "<label><input type=\"checkbox\" name=\"" + name + "\" value=\"" + model.id + "\" " + sel + " " + ext + "/>" + Language.Tag(model.Name, lang) + "</label>";
                }
            }
            return str;

        }
        public static string TypeCheckbox(string class_, string name, string id, string ext)
        {
            string str = "";
            foreach (Lebi_Type model in GetTypes(class_))
            {
                string sel = "";
                if (("," + id + ",").Contains("," + model.id + ","))
                    sel = "checked";
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-checkbox m-r-20\"><input type=\"checkbox\" id=\"" + name + "" + model.id + "\" name=\"" + name + "\" value=\"" + model.id + "\" class=\"custom-control-input\" shop=\"true\" " + sel + " " + ext + "><span class=\"custom-control-label\">" + model.Name + "</span></label>";
                }
                else
                {
                    str += "<label><input type=\"checkbox\" name=\"" + name + "\" value=\"" + model.id + "\" " + sel + " " + ext + "/>" + model.Name + "</label>";
                }
            }
            return str;

        }
        /// <summary>
        /// 返回单选内容
        /// </summary>
        /// <param name="class_"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string TypeRadio(string class_, string name, int id, string ext, string lang, string wheresql = "")
        {
            string str = "";
            foreach (Lebi_Type model in GetTypes(class_, wheresql))
            {
                string sel = "";
                if (id == model.id)
                    sel = "checked";
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-radio m-r-20\"><input type=\"radio\" id=\"" + name + "" + model.id + "\" name=\"" + name + "\" value=\"" + model.id + "\" class=\"custom-control-input\" shop=\"true\" " + sel + " " + ext + "><span class=\"custom-control-label\">" + Language.Tag(model.Name, lang) + "</span></label>";
                }
                else
                {
                    str += "<label><input type=\"radio\" name=\"" + name + "\" value=\"" + model.id + "\" " + sel + " " + ext + "/>" + Language.Tag(model.Name, lang) + "</label>";
                }
            }
            return str;

        }
        public static string TypeRadio(string class_, string name, int id, string ext)
        {
            string str = "";
            foreach (Lebi_Type model in GetTypes(class_))
            {
                string sel = "";
                if (id == model.id)
                    sel = "checked";
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-radio m-r-20\"><input type=\"radio\" id=\"" + name + "" + model.id + "\" name=\"" + name + "\" value=\"" + model.id + "\" class=\"custom-control-input\" shop=\"true\" " + sel + " " + ext + "><span class=\"custom-control-label\">" + model.Name + "</span></label>";
                }
                else
                {
                    str += "<label><input type=\"radio\" name=\"" + name + "\" value=\"" + model.id + "\" " + sel + " " + ext + "/>" + model.Name + "</label>";
                }
            }
            return str;
        }
        /// <summary>
        /// 返回一组类型并排除授权之外的类型
        /// </summary>
        /// <param name="class_"></param>
        /// <returns></returns>
        public static List<Lebi_Type> GetTypes(string class_,string wheresql = "")
        {
            string where = "Class='" + class_ + "' " + wheresql;
            if (!Shop.LebiAPI.Service.Instanse.Check("plugin_gongyingshang"))
                where += " and id !=102 and id !=103";
            List<Lebi_Type> models = B_Lebi_Type.GetList(where, "Sort desc");
            return models;

        }
    }

}

