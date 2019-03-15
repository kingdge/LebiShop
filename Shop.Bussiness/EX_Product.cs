using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;
using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.Script.Serialization;
using System.Threading;
namespace Shop.Bussiness
{
    public delegate void StockChangeEventHandler(Lebi_Product product, int count, int Freeze, string log);
    public class EX_Product
    {
        //#region 库存变动事件
        public static event StockChangeEventHandler StockChangeEvent;
        public static List<Lebi_Pro_Type> proCategorys = new List<Lebi_Pro_Type>();
        public static void StockChange(Lebi_Product product, int count, int Freeze, string log)
        {
            if (StockChangeEvent != null)
            {
                StockChangeEvent(product, count, Freeze, log);
            }
        }
        //#endregion
        /// <summary>
        /// 生成商品分类选择框
        /// </summary>
        public static string GetProductTypeList(int id, string lang, string Pro_Type_ids, string classname="")
        {
            string Types = "";
            Thread thread = new Thread(() => { Types = ThreadGetProductTypeList(id, lang, Pro_Type_ids, classname); });
            thread.IsBackground = true;
            thread.Start();
            thread.Join();
            return Types;
        }
        public static string ThreadGetProductTypeList(int id, string lang, string Pro_Type_ids, string classname = "")
        {
            int Parentid = 0;
            if (proCategorys.Count == 0)
            {
                proCategorys = B_Lebi_Pro_Type.GetList("", "Sort desc");
            }
            List<Lebi_Pro_Type> models = proCategorys.FindAll(o => o.Parentid == id);
            Lebi_Pro_Type area = proCategorys.Find(o => o.id == id);
            if (models.Count == 0)
            {
                if (area == null)
                    Parentid = 0;
                else
                    Parentid = area.Parentid;
                models = proCategorys.FindAll(o => o.Parentid == Parentid);

            }
            else
            {
                Parentid = id;
            }
            string str = "<select id=\"Pro_Type_id\" name=\"Pro_Type_id\" class=\""+ classname + "\" shop=\"true\" onchange=\"SelectProductType('Pro_Type_id');\">";
            str += "<option value=\"0\" selected>" + Language.Tag("请选择", lang) + "</option>";
            foreach (Lebi_Pro_Type model in models)
            {
                bool show = true;
                if (model.Parentid == 0)
                {
                    if (!string.IsNullOrEmpty(Pro_Type_ids))
                    {
                        string _Pro_Type_ids = "," + Pro_Type_ids + ",";
                        if (!_Pro_Type_ids.Contains("," + model.id + ","))
                            show = false;
                    }
                }
                if (show)
                {
                    if (id == model.id)
                        str += "<option value=\"" + model.id + "\" selected>" + Language.Content(model.Name, lang) + "</option>";
                    else
                        str += "<option value=\"" + model.id + "\">" + Language.Content(model.Name, lang) + "</option>";
                }
            }
            str += "</select>";
            str = CreateProductTypeSelect(Parentid, lang, Pro_Type_ids, classname) + str;
            return str;
        }

        private static string CreateProductTypeSelect(int id,string lang, string Pro_Type_ids, string classname="")
        {
            if (proCategorys.Count == 0)
                proCategorys = B_Lebi_Pro_Type.GetList("", "Sort desc");
            string str = "<select id=\"ProductType_" + id + "\" name=\"ProductType_" + id + "\" class=\"" + classname + "\" shop=\"true\" onchange=\"SelectProductType('ProductType_" + id + "');\">";
            Lebi_Pro_Type area = proCategorys.Find(o => o.id == id);
            if (area == null)
                return "";
            List<Lebi_Pro_Type> models = proCategorys.FindAll(o => o.Parentid == area.Parentid);
            if (models.Count == 0)
            {
                return "";
            }
            foreach (Lebi_Pro_Type model in models)
            {
                bool show = true;
                if (model.Parentid == 0)
                {
                    if (!string.IsNullOrEmpty(Pro_Type_ids))
                    {
                        string _Pro_Type_ids = "," + Pro_Type_ids + ",";
                        if (!_Pro_Type_ids.Contains("," + model.id + ","))
                            show = false;
                    }
                }
                if (show)
                {
                    if (id == model.id)
                        str += "<option value=\"" + model.id + "\" selected>" + Language.Content(model.Name, lang) + "</option>";
                    else
                        str += "<option value=\"" + model.id + "\">" + Language.Content(model.Name, lang) + "</option>";
                }
            }
            str += "</select> ";
            str = CreateProductTypeSelect(area.Parentid, lang, Pro_Type_ids, classname) + str;
            return str;
        }
        /// <summary>
        /// 商品分类下拉框
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="id">当前id</param>
        /// <param name="depth"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string TypeOption(int parentID, int id, int depth, string lang)
        {
            string Types = "";
            Thread thread = new Thread(() => { Types = ThreadTypeOption(parentID, id, depth, lang); });
            thread.IsBackground = true;
            thread.Start();
            thread.Join();
            return Types;
        }
        public static string ThreadTypeOption(int parentID, int id, int depth, string lang)
        {
            StringBuilder builderTree = new StringBuilder();
            //List<Lebi_Pro_Type> nodes = B_Lebi_Pro_Type.GetList("parentid=" + parentID + "", "Sort desc");
            List<Lebi_Pro_Type> nodes = Types(parentID);
            foreach (Lebi_Pro_Type node in nodes)
            {
                builderTree.Append(string.Format("<option value=\"{0}\" {1}>{2}{3}</option>  \r\n", node.id, node.id == id ? "selected=\"selected\"" : "", GetPrefixString(depth), "├" + Language.Content(node.Name, lang)));
                builderTree.Append(ThreadTypeOption(node.id, id, depth + 1, lang));
            }
            return builderTree.ToString();
        }
        public static string TypeOption(int parentID, int id, int depth, string lang, string Pro_Type_ids)
        {
            string Types = "";
            Thread thread = new Thread(() => { Types = ThreadTypeOption(parentID, id, depth, lang, Pro_Type_ids); });
            thread.IsBackground = true;
            thread.Start();
            thread.Join();
            return Types;
        }
        public static string ThreadTypeOption(int parentID, int id, int depth, string lang, string Pro_Type_ids)
        {
            StringBuilder builderTree = new StringBuilder();
            //List<Lebi_Pro_Type> nodes = B_Lebi_Pro_Type.GetList("parentid=" + parentID + "", "Sort desc");
            List<Lebi_Pro_Type> nodes = new List<Lebi_Pro_Type>();
            if (parentID == 0)
            {
                if (!string.IsNullOrEmpty(Pro_Type_ids))
                {
                    nodes = B_Lebi_Pro_Type.GetList("Parentid = 0 and id in(" + Pro_Type_ids + ")", "Sort desc");
                }
                else
                {
                    nodes = Types(parentID);
                }
            }
            else
            {
                nodes = Types(parentID);
            }
            foreach (Lebi_Pro_Type node in nodes)
            {
                builderTree.Append(string.Format("<option value=\"{0}\" {1}>{2}{3}</option>  \r\n", node.id, node.id == id ? "selected=\"selected\"" : "", GetPrefixString(depth), "├" + Language.Content(node.Name, lang)));
                builderTree.Append(ThreadTypeOption(node.id, id, depth + 1, lang));
            }
            return builderTree.ToString();
        }
        public static string TypeOption(int parentID, string id, int depth, string lang, string Pro_Type_ids)
        {
            string Types = "";
            Thread thread = new Thread(() => { Types = ThreadTypeOption(parentID, id, depth, lang, Pro_Type_ids); });
            thread.IsBackground = true;
            thread.Start();
            thread.Join();
            return Types;
        }
        public static string ThreadTypeOption(int parentID, string id, int depth, string lang, string Pro_Type_ids)
        {
            StringBuilder builderTree = new StringBuilder();
            List<Lebi_Pro_Type> nodes = new List<Lebi_Pro_Type>();
            if (parentID == 0)
            {
                if (!string.IsNullOrEmpty(Pro_Type_ids))
                {
                    nodes = B_Lebi_Pro_Type.GetList("Parentid = 0 and id in(" + Pro_Type_ids + ")", "Sort desc");
                }
                else
                {
                    nodes = Types(parentID);
                }
            }
            else
            {
                nodes = Types(parentID);
            }
            foreach (Lebi_Pro_Type node in nodes)
            {
                builderTree.Append(string.Format("<option value=\"{0}\" {1}>{2}{3}</option>  \r\n", node.id, ("," + id + ",").Contains("," + node.id + ",") ? "selected=\"selected\"" : "", GetPrefixString(depth), "├" + Language.Content(node.Name, lang)));
                builderTree.Append(ThreadTypeOption(node.id, id, depth + 1, lang));
            }
            return builderTree.ToString();
        }
        public static string TypeOption(int parentID, string id, int depth, string lang)
        {
            string Types = "";
            Thread thread = new Thread(() => { Types = ThreadTypeOption(parentID, id, depth, lang); });
            thread.IsBackground = true;
            thread.Start();
            thread.Join();
            return Types;
        }
        public static string ThreadTypeOption(int parentID, string id, int depth, string lang)
        {
            StringBuilder builderTree = new StringBuilder();
            //List<Lebi_Pro_Type> nodes = B_Lebi_Pro_Type.GetList("parentid=" + parentID + "", "");
            List<Lebi_Pro_Type> nodes = Types(parentID);
            foreach (Lebi_Pro_Type node in nodes)
            {
                builderTree.Append(string.Format("<option value=\"{0}\" {1}>{2}{3}</option>  \r\n", node.id, ("," + id + ",").Contains("," + node.id + ",") ? "selected=\"selected\"" : "", GetPrefixString(depth), "├" + Language.Content(node.Name, lang)));
                builderTree.Append(ThreadTypeOption(node.id, id, depth + 1, lang));
            }
            return builderTree.ToString();
        }
        /// <summary>
        /// 商品分类的路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string TypePath(Lebi_Pro_Type model, string id)
        {
            if (!("," + id + ",").Contains("," + model.Parentid + ","))
            {
                id = model.Parentid + "," + id;
                if (model.Parentid > 0 && model.Parentid != model.id)
                {
                    Lebi_Pro_Type pmodel = B_Lebi_Pro_Type.GetModel(model.Parentid);
                    if (pmodel != null)
                    {
                        id = TypePath(pmodel, id);
                    }
                }
            }
            return id;
        }
        /// <summary>
        /// 供应商自定义商品分类的路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string SuplierTypePath(Lebi_Supplier_ProductType model, string id)
        {
            if (!("," + id + ",").Contains("," + model.parentid + ","))
            {
                id = model.parentid + "," + id;
                if (model.parentid > 0 && model.parentid != model.id)
                {
                    Lebi_Supplier_ProductType pmodel = B_Lebi_Supplier_ProductType.GetModel(model.parentid);
                    if (pmodel != null)
                    {
                        id = SuplierTypePath(pmodel, id);
                    }
                }
            }
            return id;
        }
        /// <summary>
        /// 从供应商的分类路径中找到最底层分类id
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static int SuplierTypeid(string ids)
        {
            string[] arr = ids.Split(',');
            int id = 0;
            if (arr.Length > 0)
            {
                for (int i = arr.Length - 1; i > -1; i--)
                {
                    int.TryParse(arr[i], out id);
                    int count = B_Lebi_Supplier_ProductType.Counts("parentid=" + id + "");
                    if (count == 0)
                        return id;
                }
            }
            return id;
        }
        /// <summary>
        /// 更新全部商品分类的路径
        /// </summary>
        public static void UpdateTypePath()
        {
            List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList("", "");
            foreach (Lebi_Pro_Type model in models)
            {
                model.Path = TypePath(model, "");
                B_Lebi_Pro_Type.Update(model);
            }
        }
        /// <summary>
        /// 更新某级商品分类的路径
        /// </summary>
        /// <param name="ptype"></param>
        public static void UpdateTypePath(Lebi_Pro_Type ptype)
        {
            ptype.Path = TypePath(ptype, "");
            B_Lebi_Pro_Type.Update(ptype);
            List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList("Parentid=" + ptype.id + "", "");
            foreach (Lebi_Pro_Type model in models)
            {
                UpdateTypePath(model);
            }
        }
        /// <summary>
        /// 验证一个分类的父路径是否正确
        /// </summary>
        /// <param name="ptype"></param>
        /// <returns></returns>
        public static bool PathIsOK(Lebi_Pro_Type ptype, string path)
        {
            if (ptype.Parentid == 0)
                return true;
            if (("," + path + ",").Contains("," + ptype.Parentid + ","))
                return false;
            path += "," + ptype.Parentid;
            Lebi_Pro_Type pptype = B_Lebi_Pro_Type.GetModel(ptype.Parentid);
            if (pptype == null)
                return false;
            return PathIsOK(pptype, path);
        }
        /// <summary>
        /// 生成树形类别列表，供TAB_CHILD使用 by 56770.kingdge 2013-07-26
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static string TypeList(int parentID, string id, int depth, string lang)
        {
            StringBuilder builderTree = new StringBuilder();
            //List<Lebi_Pro_Type> nodes = B_Lebi_Pro_Type.GetList("parentid=" + parentID + "", "");
            List<Lebi_Pro_Type> nodes = Types(parentID);
            foreach (Lebi_Pro_Type node in nodes)
            {
                builderTree.Append(string.Format("<tr class=\"list\"><td style=\"text-align:center\"><input id=\"Checkbox1\" type=\"checkbox\" name=\"tpid\" value=\"{0}\" {1}></td><td align=\"list\">{2}{3}</td></tr>  \r\n", node.id, ISselected(node.id, Int32.Parse(id)) ? "checked" : "", GetPrefixString(depth), "├" + Language.Content(node.Name, lang)));
                builderTree.Append(TypeList(node.id, id, depth + 1, lang));
            }
            return builderTree.ToString();
        }
        public static bool ISselected(int id, int tabid)
        {
            int count = B_Lebi_TabChild.Counts("tabid=" + tabid + " and protypeid=" + id);
            if (count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 根据父亲ID取出子商品分类
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static List<Lebi_Pro_Type> Types(int parentid)
        {
            List<Lebi_Pro_Type> models = ShopCache.GetProductType();
            models = (from m in models
                      where m.Parentid == parentid
                      select m).ToList();
            return models;

        }

        /// <summary>
        /// 根据父亲ID取出子商品分类
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static List<Lebi_Pro_Type> Types(int parentid, string showall)
        {
            List<Lebi_Pro_Type> models = ShopCache.GetProductType();
            if (showall == "all")
            {
                models = (from m in models where m.Parentid == parentid select m).ToList();
            }
            else
            {
                models = (from m in models where m.Parentid == parentid && m.IsShow == 1 select m).ToList();
            }
            return models;

        }
        /// <summary>
        /// 根据父亲ID取出可显示的子商品分类
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static List<Lebi_Pro_Type> ShowTypes(int parentid, int siteid = 0)
        {
            //List<Lebi_Pro_Type> models = ShopCache.GetProductType();
            //if (siteid == 0)
            //    models = (from m in models
            //              where m.Parentid == parentid && m.IsShow == 1 && m.IsIndexShow == 1
            //              select m).ToList();
            //else
            //    models = (from m in models
            //              where m.Parentid == parentid && m.IsShow == 1 && m.IsIndexShow == 1 && ("," + m.Site_ids + ",").Contains("," + siteid + ",")
            //              select m).ToList();
            string where = "Parentid=" + parentid + " and IsShow=1 and (IsDel!=1 or IsDel is null)";
            if (siteid > 0)
                where = "Parentid=" + parentid + " and IsShow=1 and ','+Site_ids+',' like '%," + siteid + ",%'";
            List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList(where, "Sort desc");
            return models;

        }
        public static List<Lebi_Pro_Type> ShowTypes(int parentid, int siteid, int top)
        {
            //List<Lebi_Pro_Type> models = ShopCache.GetProductType();
            //if (siteid == 0)
            //    models = (from m in models
            //              where m.Parentid == parentid && m.IsShow == 1 && m.IsIndexShow == 1
            //              select m).ToList();
            //else
            //    models = (from m in models
            //              where m.Parentid == parentid && m.IsShow == 1 && m.IsIndexShow == 1 && ("," + m.Site_ids + ",").Contains("," + siteid + ",")
            //              select m).ToList();
            string where = "Parentid=" + parentid + " and IsShow=1 and (IsDel!=1 or IsDel is null)";
            if (siteid > 0)
                where = "Parentid=" + parentid + " and IsShow=1 and ','+Site_ids+',' like '%," + siteid + ",%'";
            List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList(where, "Sort desc", top, 1);
            return models;

        }
        /// <summary>
        /// 返回,分隔的商品分类名称
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="LanguageCode"></param>
        /// <returns></returns>
        public static string TypeNames(string ids, string LanguageCode)
        {
            string str = "";
            if (ids == "")
                return "";
            List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList("id in (lbsql{" + ids + "}) and IsShow = 1", "");
            foreach (Lebi_Pro_Type model in models)
            {
                if (str == "")
                    str = Language.Content(model.Name, LanguageCode);
                else
                    str += "," + Language.Content(model.Name, LanguageCode);
            }
            return str;
        }
        private static string GetPrefixString(int depth)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < depth; i++)
            {
                builder.Append("│");

            }
            return builder.ToString();
        }
        /// <summary>
        /// 返回分类下商品数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int TypeProductCount(int id)
        {
            string where = "Product_id = 0 and (IsDel!=1 or IsDel is null)";

            if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "sqlserver")
            {
                where += " and (Pro_Type_id in (" + TypeIds(id) + ") or Charindex('," + id + ",',','+Pro_Type_id_other+',')>0)";
            }
            else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "access")
            {
                where += " and (Pro_Type_id in (" + TypeIds(id) + ") or Instr(','+Pro_Type_id_other+',','," + id + ",')>0)";
            }
            else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "mysql")
            {
                where += " and (Pro_Type_id in (" + TypeIds(id) + ") or Instr(','+Pro_Type_id_other+',','," + id + ",')>0)";
            }
            //<-{分销站点
            int DT_id = ShopPage.GetDT();
            if (DT_id > 0)
            {
                string DT_Product_ids = "";
                Lebi_DT CurrentDT = B_Lebi_DT.GetModel(DT_id);
                if (CurrentDT != null)
                {
                    DT_Product_ids = CurrentDT.Product_ids;
                    if (DT_Product_ids == "")
                        DT_Product_ids = "0";
                    where += " and id in(select dt_p.Product_id from [Lebi_DT_Product] as dt_p where dt_p.DT_id = " + DT_id + ")";
                }
            }
            //}->
            return B_Lebi_Product.Counts(where);
            //return TypeIds(id);
        }
        /// <summary>
        /// 返回供应商分类下上架商品数量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="supplierid"></param>
        /// <returns></returns>
        public static int OnSaleSupplierTypeProductCount(int id, int supplierid)
        {
            string where = "Product_id = 0 and (IsDel!=1 or IsDel is null) and Type_id_ProductStatus = 101 and Supplier_id=" + supplierid;
            if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "sqlserver")
            {
                where += " and (Charindex('," + id + ",',','+Supplier_ProductType_ids+',')>0)";
            }
            else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "access")
            {
                where += "  and (Instr(','+Supplier_ProductType_ids+',','," + id + ",')>0)";
            }
            else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "mysql")
            {
                where += "  and (instr(','+Supplier_ProductType_ids+',','," + id + ",')>0)";
            }
            //<-{分销站点
            int DT_id = ShopPage.GetDT();
            if (DT_id > 0)
            {
                string DT_Product_ids = "";
                Lebi_DT CurrentDT = B_Lebi_DT.GetModel(DT_id);
                if (CurrentDT != null)
                {
                    DT_Product_ids = CurrentDT.Product_ids;
                    if (DT_Product_ids == "")
                        DT_Product_ids = "0";
                    where += " and id in(select dt_p.Product_id from [Lebi_DT_Product] as dt_p where dt_p.DT_id = " + DT_id + ")";
                }
            }
            //}->
            return B_Lebi_Product.Counts(where);
        }
        /// <summary>
        /// 返回分类下上架商品数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int OnSaleTypeProductCount(int id)
        {
            string where = "Product_id = 0 and Type_id_ProductStatus = 101 and (IsDel!=1 or IsDel is null)";
            if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "sqlserver")
            {
                where += " and (Pro_Type_id in (" + TypeIds(id) + ") or Charindex('," + id + ",',','+Pro_Type_id_other+',')>0)";
            }
            else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "access")
            {
                where += "  and (Pro_Type_id in (" + TypeIds(id) + ") or Instr(','+Pro_Type_id_other+',','," + id + ",')>0)";
            }
            else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "mysql")
            {
                where += "  and (Pro_Type_id in (" + TypeIds(id) + ") or instr(','+Pro_Type_id_other+',','," + id + ",')>0)";
            }
            //<-{分销站点
            int DT_id = ShopPage.GetDT();
            if (DT_id > 0)
            {
                string DT_Product_ids = "";
                Lebi_DT CurrentDT = B_Lebi_DT.GetModel(DT_id);
                if (CurrentDT != null)
                {
                    DT_Product_ids = CurrentDT.Product_ids;
                    if (DT_Product_ids == "")
                        DT_Product_ids = "0";
                    where += " and id in(select dt_p.Product_id from [Lebi_DT_Product] as dt_p where dt_p.DT_id = " + DT_id + ")";
                }
            }
            //}->
            return B_Lebi_Product.Counts(where);
        }
        /// <summary>
        /// 返回分类下上架商品数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int OnSaleTypeProductCount(int id, int supplierid, string keyword = "")
        {
            string where = "Product_id = 0 and Type_id_ProductStatus = 101  and (IsDel!=1 or IsDel is null)";
            if (id > 0)
            {
                if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "sqlserver")
                {
                    where += " and (Pro_Type_id in (" + TypeIds(id) + ") or Charindex('," + id + ",',','+Pro_Type_id_other+',')>0)";
                }
                else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "access")
                {
                    where += "  and (Pro_Type_id in (" + TypeIds(id) + ") or Instr(','+Pro_Type_id_other+',','," + id + ",')>0)";
                }
                else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "mysql")
                {
                    where += "  and (Pro_Type_id in (" + TypeIds(id) + ") or instr(','+Pro_Type_id_other+',','," + id + ",')>0)";
                }
            }
            if (supplierid > 0)
                where += " and Supplier_id=" + supplierid;
            if (keyword != "")
            {
                //增加空格划词搜索 by kingdge 2013-09-18
                string wherekeyword = "";
                if (keyword.IndexOf(" ") > -1)
                {
                    string[] keywordsArr;
                    keywordsArr = keyword.Split(new char[1] { ' ' });
                    foreach (string keywords in keywordsArr)
                    {
                        if (keywords != "")
                            if (wherekeyword == "")
                                wherekeyword = "Name like lbsql{'%" + keywords + "%'}";
                            else
                                wherekeyword += " and Name like lbsql{'%" + keywords + "%'}";
                    }
                }
                else
                {
                    wherekeyword = "Name like lbsql{'%" + keyword + "%'}";
                }
                where += " and ((" + wherekeyword + ") or Number like lbsql{'%" + keyword + "%'} or Code like lbsql{'%" + keyword + "%'})";
            }
            //<-{分销站点
            int DT_id = ShopPage.GetDT();
            if (DT_id > 0)
            {
                string DT_Product_ids = "";
                Lebi_DT CurrentDT = B_Lebi_DT.GetModel(DT_id);
                if (CurrentDT != null)
                {
                    DT_Product_ids = CurrentDT.Product_ids;
                    if (DT_Product_ids == "")
                        DT_Product_ids = "0";
                    where += " and id in(select dt_p.Product_id from [Lebi_DT_Product] as dt_p where dt_p.DT_id = " + DT_id + ")";
                }
            }
            //}->
            return B_Lebi_Product.Counts(where);
        }
        /// <summary>
        /// 返回品牌下商品数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int BrandProductCount(int id)
        {
            return B_Lebi_Product.Counts("Product_id = 0 and Brand_id=" + id + " and (IsDel!=1 or IsDel is null)");
        }
        public static int BrandProductCount(string sql)
        {
            return B_Lebi_Product.Counts("Product_id = 0 and (IsDel!=1 or IsDel is null) and " + sql);
        }
        /// <summary>
        /// 返回分类下商品收藏数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int LikeProductCount(int id)
        {
            return B_Lebi_User_Product.Counts("Pro_Type_id in (" + TypeIds(id) + ")");
            //return TypeIds(id);
        }
        /// <summary>
        /// 返回分类下商品销量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int SalesProductCount(int id)
        {
            string ids = EX_Product.TypeIds(id);
            string sum = B_Lebi_Product.GetValue("sum(Count_Sales)", "Pro_Type_id in (lbsql{" + ids + "}) and (IsDel!=1 or IsDel is null)");
            int s = 0;
            int.TryParse(sum, out s);
            return s;
        }
        /// <summary>
        /// 返回分类下商品浏览量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int ViewsProductCount(int id)
        {
            string ids = EX_Product.TypeIds(id);
            string sum = B_Lebi_Product.GetValue("sum(Count_Views)", "Pro_Type_id in (lbsql{" + ids + "}) and (IsDel!=1 or IsDel is null)");
            int s = 0;
            int.TryParse(sum, out s);
            return s;
        }
        /// <summary>
        /// 一个商品分类下的所有商品分类ID
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public static string TypeIds(int tid)
        {
            string ids = tid.ToString();
            List<Lebi_Pro_Type> ts = B_Lebi_Pro_Type.GetList("Parentid=" + tid + " and IsShow = 1 and (IsDel!=1 or IsDel is null)", "");
            foreach (Lebi_Pro_Type t in ts)
            {
                ids += "," + TypeIds(t.id);
            }
            return ids;
        }
        /// <summary>
        /// 一个商品分类下的上一级分类ID
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public static int ParentTypeId(int tid)
        {
            Lebi_Pro_Type model = B_Lebi_Pro_Type.GetModel("id=" + tid + " ");
            if (model == null)
            {
                return tid;
            }
            if (model.Parentid == 0)
            {
                return tid;
            }
            else
            {
                return model.Parentid;
            }
        }
        /// <summary>
        /// 一个商品分类下的最终父级分类ID
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public static int FatherTypeId(int tid)
        {
            Lebi_Pro_Type model = B_Lebi_Pro_Type.GetModel("id=" + tid + " and (IsDel!=1 or IsDel is null)");
            if (model == null)
            {
                return tid;
            }
            else
            {
                if (model.Parentid == 0)
                {
                    return model.id;
                }
                else
                {
                    return FatherTypeId(model.Parentid);
                }
            }
        }
        /// <summary>
        /// 品牌下拉框
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Supplier_id"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string BrandOption(int id, string lang = "CN", int Supplier_id = 0)
        {
            StringBuilder builderTree = new StringBuilder();
            string sql = "Type_id_BrandStatus = 452";
            if (Supplier_id > 0)
                sql += "and (Supplier_id = 0 or Supplier_id = " + Supplier_id + ")";
            List<Lebi_Brand> nodes = B_Lebi_Brand.GetList(sql, "FirstLetter asc,Sort desc");
            foreach (Lebi_Brand node in nodes)
            {
                string BrandName = "";
                if (node.FirstLetter != "")
                    BrandName = node.FirstLetter + " - " + Language.Content(node.Name, lang);
                else
                    BrandName = Language.Content(node.Name, lang);
                builderTree.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>  \r\n", node.id, node.id == id ? "selected=\"selected\"" : "", BrandName));

            }
            return builderTree.ToString();
        }
        /// <summary>
        /// 单位下拉框
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string UnitOption(int id, string lang = "CN")
        {
            StringBuilder builderTree = new StringBuilder();
            List<Lebi_Units> nodes = B_Lebi_Units.GetList("", "");
            foreach (Lebi_Units node in nodes)
            {
                builderTree.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>  \r\n", node.id, node.id == id ? "selected=\"selected\"" : "", Language.Content(node.Name, lang)));

            }
            return builderTree.ToString();
        }
        /// <summary>
        /// 处理商品图片,
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static List<LBimage> ProductImages(Lebi_Product pro, Lebi_Theme theme)
        {
            List<LBimage> list = new List<LBimage>();
            if (pro.Images == "")
                return list;
            //LBimage model=new LBimage();
            string[] arr = pro.Images.Split('@');
            foreach (string str in arr)
            {
                if (str != "")
                {
                    LBimage model = new LBimage();
                    model.original = str;
                    model.small = str.Replace("w$h", theme.ImageSmall_Width + "$" + theme.ImageSmall_Height);
                    model.medium = str.Replace("w$h", theme.ImageMedium_Width + "$" + theme.ImageMedium_Height);
                    model.big = str.Replace("w$h", theme.ImageBig_Width + "$" + theme.ImageBig_Height);
                    list.Add(model);
                }

            }
            return list;
        }
        /// <summary>
        /// 取得商品实例
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="page"></param>
        /// <returns></returns>

        public static Lebi_Product GetProduct(int pid)
        {
            Lebi_Product model = B_Lebi_Product.GetModel(pid);
            if (model == null)
            {
                model = new Lebi_Product();
                return model;
            }
            if (model.Type_id_ProductStatus == 102)
            {
                model = new Lebi_Product();
                return model;
            }
            if (model.IsDel == 1)
            {
                model = new Lebi_Product();
                return model;
            }

            if (model.Product_id > 0)
            {
                Lebi_Product pmodel = B_Lebi_Product.GetModel(model.Product_id);
                if (pmodel == null)
                    pmodel = new Lebi_Product();
                model.Service = pmodel.Service;
                model.Brand_id = pmodel.Brand_id;
                //model.Description = pmodel.Description;
                model.Introduction = pmodel.Introduction;
                model.Specification = pmodel.Specification;
                model.Packing = pmodel.Packing;
                //model.MobileDescription = pmodel.MobileDescription;
                model.Pro_Tag_id = pmodel.Pro_Tag_id;
                model.Pro_Type_id = pmodel.Pro_Type_id;
                model.ProPerty132 = pmodel.ProPerty132;
                //model.ProPerty133 = pmodel.ProPerty133;
                model.ProPerty134 = pmodel.ProPerty134;
                model.Remarks = pmodel.Remarks;
                if (model.SEO_Description == "")
                    model.SEO_Description = pmodel.SEO_Description;
                if (model.SEO_Keywords == "")
                    model.SEO_Keywords = pmodel.SEO_Keywords;
                if (model.SEO_Title == "")
                    model.SEO_Title = pmodel.SEO_Title;
                model.Tags = pmodel.Tags;
                //model.Type_id_ProPerty = pmodel.Type_id_ProPerty;
                //model.Units_id = pmodel.Units_id;
                model.Count_Stock = EX_Product.ProductStock(model);
                model.ProPertyMain = pmodel.ProPertyMain;
                model.IsSupplierTransport = pmodel.IsSupplierTransport;
                model.Supplier_id = pmodel.Supplier_id;
                if (model.Images == "")
                    model.Images = pmodel.Images;
                return model;
            }
            else
            {
                Lebi_Product smodel = B_Lebi_Product.GetList("(IsDel!=1 or IsDel is null) and Product_id=" + model.id + "", "").FirstOrDefault();
                if (smodel != null)
                {
                    if (smodel.Service == null)
                        smodel.Service = model.Service;
                    if (smodel.Specification == null)
                        smodel.Specification = model.Specification;
                    if (smodel.Packing == null)
                        smodel.Packing = model.Packing;
                    if (smodel.Brand_id == 0)
                        smodel.Brand_id = model.Brand_id;
                    if (smodel.Description == null)
                        smodel.Description = model.Description;
                    if (smodel.Introduction == null)
                        smodel.Introduction = model.Introduction;
                    if (smodel.MobileDescription == null)
                        smodel.MobileDescription = model.MobileDescription;
                    if (smodel.Pro_Tag_id == null)
                        smodel.Pro_Tag_id = model.Pro_Tag_id;
                    if (smodel.Pro_Type_id == 0)
                        smodel.Pro_Type_id = model.Pro_Type_id;
                    if (smodel.ProPerty132 == null)
                        smodel.ProPerty132 = model.ProPerty132;
                    if (smodel.ProPerty133 == null)
                        smodel.ProPerty133 = model.ProPerty133;
                    if (smodel.ProPerty134 == null)
                        smodel.ProPerty134 = model.ProPerty134;
                    if (smodel.Remarks == null)
                        smodel.Remarks = model.Remarks;
                    if (smodel.SEO_Description == null)
                        smodel.SEO_Description = model.SEO_Description;
                    if (smodel.SEO_Keywords == null)
                        smodel.SEO_Keywords = model.SEO_Keywords;
                    if (smodel.SEO_Title == null)
                        smodel.SEO_Title = model.SEO_Title;
                    if (smodel.Tags == null)
                        smodel.Tags = model.Tags;
                    ////smodel.Type_id_ProPerty = pmodel.Type_id_ProPerty;
                    if (smodel.Units_id == 0)
                        smodel.Units_id = model.Units_id;
                    //model.Count_Stock = EX_Product.ProductStock(model);
                    if (smodel.Number == null)
                        smodel.Number = model.Number;
                    if (smodel.ProPertyMain == null)
                        smodel.ProPertyMain = model.ProPertyMain;
                    smodel.IsSupplierTransport = model.IsSupplierTransport;
                    smodel.Supplier_id = model.Supplier_id;
                    if (smodel.Images == "")
                        smodel.Images = model.Images;
                    return smodel;
                }
                return model;
            }

        }
        /// <summary>
        /// 处理商品规格，返回类似如下格式数组
        /// 颜色-白色-166
        /// 颜色-黑色-169
        /// 尺码-M-166
        /// 尺码-L-167
        /// 尺码-XL-168
        /// </summary>
        /// <param name="model"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string[,] Property(Lebi_Product model, Lebi_Language lang)
        {

            if (model.Product_id == 0)
            {
                //无同款子商品
                return new string[0, 3];
            }
            List<Lebi_Product> pros = B_Lebi_Product.GetList("(IsDel!=1 or IsDel is null) and Product_id=" + model.Product_id + "", "");
            if (pros.Count == 0)
            {
                return new string[0, 3];
            }
            Lebi_Product pmodel = B_Lebi_Product.GetModel(model.Product_id);
            if (pmodel == null)
            {
                return new string[0, 3];
            }
            //Lebi_ProPerty_Type type = B_Lebi_ProPerty_Type.GetModel(model.Type_id_ProPerty);
            string property = ProductType_ProPertystr(model.Pro_Type_id);
            if (property == "")
            {
                return new string[0, 3];
            }

            List<Lebi_ProPerty> pps = B_Lebi_ProPerty.GetList("id in (" + property + ") and Type_id_ProPertyType=131", "id asc");
            //int i = 0;
            string shuxing = model.ProPerty131;
            string shuxing_ = "";
            List<string> list = new List<string>();
            foreach (Lebi_ProPerty pp in pps)
            {
                List<Lebi_ProPerty> ps = B_Lebi_ProPerty.GetList("parentid=" + pp.id + " and id in (" + pmodel.ProPerty131 + ")", "id asc");
                //替换当前属性
                foreach (Lebi_ProPerty p in ps)
                {
                    if (("," + model.ProPerty131 + ",").Contains("," + p.id + ","))
                    {
                        shuxing = ("," + model.ProPerty131 + ",").Replace("," + p.id + ",", ",$$,");
                        break;
                    }
                }
                //去除首尾的逗号
                if (shuxing == ",," || shuxing == "")
                    continue;
                shuxing = shuxing.Substring(1, shuxing.Length - 2);
                foreach (Lebi_ProPerty p in ps)
                {
                    shuxing_ = shuxing.Replace("$$", p.id.ToString());
                    foreach (Lebi_Product pro in pros)
                    {
                        if (pro.ProPerty131 == shuxing_)
                        {
                            list.Add(Language.Content(pp.Name, lang.Code) + "$" + Language.Content(p.Name, lang.Code) + "$" + pro.id.ToString());
                            break;
                        }
                    }


                }
            }
            string[,] arr = new string[list.Count, 3];
            int i = 0;
            foreach (string str in list)
            {
                string[] arr_tmp = str.Split('$');
                arr[i, 0] = arr_tmp[0];
                arr[i, 1] = arr_tmp[1];
                arr[i, 2] = arr_tmp[2];
                i++;
            }
            return arr;
        }
        /// <summary>
        /// 某个商品分类的全部绑定属性
        /// </summary>
        /// <param name="prptypeid"></param>
        /// <returns></returns>
        public static string ProductType_ProPertystr(int prptypeid)
        {
            string str = "";
            Lebi_Pro_Type type = B_Lebi_Pro_Type.GetModel("id=" + prptypeid + "");
            if (type == null)
            {
                return "0";
            }
            str = type.ProPerty131;
            if (str == "")
                str = type.ProPerty132;
            else
            {
                if (type.ProPerty132 != "")
                    str += "," + type.ProPerty132;
            }
            if (str == "")
                str = type.ProPerty133;
            else
            {
                if (type.ProPerty133 != "")
                    str += "," + type.ProPerty133;
            }
            if (str == "")
                str = type.ProPerty134;
            else
            {
                if (type.ProPerty134 != "")
                    str += "," + type.ProPerty134;
            }
            if (str != "")
                return str;
            return ProductType_ProPertystr(type.Parentid);

        }
        /// <summary>
        /// 某个商品分类的全部绑定属性
        /// </summary>
        /// <param name="prptypeid"></param>
        /// <returns></returns>
        public static string ProductType_ProPertystr(int prptypeid, int supplier_id = 0)
        {
            string str = "";
            Lebi_Pro_Type type = B_Lebi_Pro_Type.GetModel("id=" + prptypeid + "");
            if (type == null)
            {
                return "0";
            }
            string ids = TypePath(type, "");
            if (ids.LastIndexOf(",") > -1) { ids = ids.Remove(ids.Length - 1); }
            if (supplier_id > 0)
            {
                Lebi_Supplier_ProPerty property131 = B_Lebi_Supplier_ProPerty.GetModel("Supplier_id = " + supplier_id + " and Pro_Type_id in(" + ids + ") and Type_id_ProPertyType = 131");
                if (property131 != null)
                {
                    if (type.ProPerty131 != "")
                    {
                        type.ProPerty131 += "," + property131.ProPerty;
                    }
                    else
                    {
                        type.ProPerty131 = property131.ProPerty;
                    }
                }
                Lebi_Supplier_ProPerty property133 = B_Lebi_Supplier_ProPerty.GetModel("Supplier_id = " + supplier_id + " and Pro_Type_id in(" + ids + ") and Type_id_ProPertyType = 133");
                if (property133 != null)
                {
                    if (type.ProPerty133 != "")
                    {
                        type.ProPerty133 += "," + property133.ProPerty;
                    }
                    else
                    {
                        type.ProPerty133 = property133.ProPerty;
                    }
                }
            }
            str = type.ProPerty131;
            if (str == "")
                str = type.ProPerty132;
            else
            {
                if (type.ProPerty132 != "")
                    str += "," + type.ProPerty132;
            }
            if (str == "")
                str = type.ProPerty133;
            else
            {
                if (type.ProPerty133 != "")
                    str += "," + type.ProPerty133;
            }
            if (str == "")
                str = type.ProPerty134;
            else
            {
                if (type.ProPerty134 != "")
                    str += "," + type.ProPerty134;
            }
            if (str != "")
                return str;
            return ProductType_ProPertystr(type.Parentid, supplier_id);

        }
        /// <summary>
        /// 返回商品分类的 某个类型绑定属性
        /// </summary>
        /// <param name="prptypeid"></param>
        /// <param name="t"></param>
        /// <param name="supplier_id"></param>
        /// <returns></returns>
        public static string ProductType_ProPertystr(int prptypeid, int t, int supplier_id)
        {
            string str = "";
            Lebi_Pro_Type type = B_Lebi_Pro_Type.GetModel("id=" + prptypeid + "");
            if (type == null)
            {
                return "";
            }
            string ids = TypePath(type, "");
            if (ids.LastIndexOf(",") > -1) { ids = ids.Remove(ids.Length - 1); }
            if (supplier_id > 0 && t == 131)
            {
                Lebi_Supplier_ProPerty property131 = B_Lebi_Supplier_ProPerty.GetModel("Supplier_id = " + supplier_id + " and Pro_Type_id in(" + ids + ") and Type_id_ProPertyType = 131");
                if (property131 != null)
                {
                    if (type.ProPerty131 != "")
                    {
                        type.ProPerty131 += "," + property131.ProPerty;
                    }
                    else
                    {
                        type.ProPerty131 = property131.ProPerty;
                    }
                }
            }
            if (supplier_id > 0 && t == 133)
            {
                Lebi_Supplier_ProPerty property133 = B_Lebi_Supplier_ProPerty.GetModel("Supplier_id = " + supplier_id + " and Pro_Type_id in(" + ids + ") and Type_id_ProPertyType = 133");
                if (property133 != null)
                {
                    if (type.ProPerty133 != "")
                    {
                        type.ProPerty133 += "," + property133.ProPerty;
                    }
                    else
                    {
                        type.ProPerty133 = property133.ProPerty;
                    }
                }
            }
            switch (t)
            {
                case 131:
                    str = type.ProPerty131;
                    break;
                case 132:
                    str = type.ProPerty132;
                    break;
                case 133:
                    str = type.ProPerty133;
                    break;
                case 134:
                    str = type.ProPerty133;
                    break;
            }
            if (str != "")
                return str;
            return ProductType_ProPertystr(type.Parentid, supplier_id);

        }
        /// <summary>
        /// 返回商品分类的 某个类型绑定属性
        /// </summary>
        /// <param name="prptypeid"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ProductType_ProPertystr(Lebi_Product model, int t)
        {
            string str = "";
            Lebi_Pro_Type type = B_Lebi_Pro_Type.GetModel("id=" + model.Pro_Type_id + "");
            if (type == null)
                return "";
            if (model.Supplier_id > 0 && t == 131)
            {
                Lebi_Supplier_ProPerty property131 = B_Lebi_Supplier_ProPerty.GetModel("Supplier_id = " + model.Supplier_id + " and Pro_Type_id = " + model.Pro_Type_id + " and Type_id_ProPertyType = 131");
                if (property131 != null)
                {
                    if (type.ProPerty131 != "")
                    {
                        type.ProPerty131 += "," + property131.ProPerty;
                    }
                    else
                    {
                        type.ProPerty131 = property131.ProPerty;
                    }
                }
            }
            if (model.Supplier_id > 0 && t == 133)
            {
                Lebi_Supplier_ProPerty property133 = B_Lebi_Supplier_ProPerty.GetModel("Supplier_id = " + model.Supplier_id + " and Pro_Type_id = " + model.Pro_Type_id + " and Type_id_ProPertyType = 133");
                if (property133 != null)
                {
                    if (type.ProPerty133 != "")
                    {
                        type.ProPerty133 += "," + property133.ProPerty;
                    }
                    else
                    {
                        type.ProPerty133 = property133.ProPerty;
                    }
                }
            }
            switch (t)
            {
                case 131:
                    str = type.ProPerty131;
                    break;
                case 132:
                    str = type.ProPerty132;
                    break;
                case 133:
                    str = type.ProPerty133;
                    break;
                case 134:
                    str = type.ProPerty134;
                    break;
            }
            if (str != "")
                return str;
            return ProductType_ProPertystr(type.Parentid);

        }
        /// <summary>
        /// 某个商品分类的继承属性规格的父分类
        /// </summary>
        /// <param name="prptypeid"></param>
        /// <returns></returns>
        public static Lebi_Pro_Type ProductType_ProPerty(int prptypeid)
        {
            Lebi_Pro_Type type = B_Lebi_Pro_Type.GetModel("id=" + prptypeid + "");
            return ProductType_ProPerty(type);
        }
        /// <summary>
        /// 某个商品分类的继承属性规格的父分类
        /// </summary>
        /// <param name="prptypeid"></param>
        /// <returns></returns>
        public static Lebi_Pro_Type ProductType_ProPerty(Lebi_Pro_Type type)
        {
            //Lebi_Pro_Type type = B_Lebi_Pro_Type.GetModel("id=" + prptypeid + "");
            if (type == null)
                return new Lebi_Pro_Type();
            if (type.ProPerty132 != "" || type.ProPerty131 != "" || type.ProPerty133 != "")
                return type;
            return ProductType_ProPerty(type.Parentid);
        }
        /// <summary>
        /// 处理产品市场价格
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static decimal ProductMarketPrice(Lebi_Product product)
        {
            //<-{获取分销价格
            int DT_id = ShopPage.GetDT();
            if (DT_id > 0)
            {
                Lebi_DT_Product DT_product = B_Lebi_DT_Product.GetModel("DT_id = " + DT_id + " and Product_id = " + product.id);
                if (DT_product != null)
                {
                    return DT_product.Price_Market;
                }
            }
            return product.Price_Market;
        }
        /// <summary>
        /// 处理产品价格
        /// </summary>
        /// <param name="product"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static decimal ProductPrice(Lebi_Product product, Lebi_User user)
        {
            Lebi_UserLevel level = B_Lebi_UserLevel.GetModel(user.UserLevel_id);
            return ProductPrice(product, level, user);
        }
        public static decimal ProductPrice(Lebi_Product product, Lebi_UserLevel level, Lebi_User user)
        {
            Lebi_Product_Price product_price = B_Lebi_Product_Price.GetModel("Product_id = " + product.id + " and User_id = " + user.id);
            if (product_price != null)
            {
                return product_price.Price;
            }
            //<-{获取分销价格
            int DT_id = ShopPage.GetDT();
            if (DT_id > 0)
            {
                Lebi_DT_Product DT_product = B_Lebi_DT_Product.GetModel("DT_id = " + DT_id + " and Product_id = " + product.id);
                if (DT_product != null)
                {
                    return DT_product.Price / 100 * level.Price;
                }
            }
            //}->
            if ((product.Type_id_ProductType == 321 || product.Type_id_ProductType == 322) && product.Time_Expired > System.DateTime.Now)
                return product.Price_Sale;
            //if (product.Type_id_ProductType == 324)
            //    return product.Price_reserve;
            if ((product.Type_id_ProductType == 323) && product.Time_Expired > System.DateTime.Now)
                return 0;
            if (level == null)
                return product.Price;
            if (level.IsHidePrice == 1)
            {
                //SystemLog.Add("价格无权限-level.IsHidePrice" + level.IsHidePrice);
                return -9999999999;
            }
            if (Shop.LebiAPI.Service.Instanse.Check("plugin_productlimit"))
            {
                Lebi_Product_Limit limit = B_Lebi_Product_Limit.GetModel("User_id=" + user.id + " and (Product_id=" + product.id + " or Product_id=" + product.Product_id + ")");
                if (limit == null)
                {
                    limit = B_Lebi_Product_Limit.GetModel("UserLevel_id=" + level.id + " and (Product_id=" + product.id + " or Product_id=" + product.Product_id + ")");
                }
                if (limit != null)
                {
                    if ((limit.IsPriceShow == 1 && ShopCache.GetBaseConfig().ProductLimitType == "0") || (limit.IsPriceShow == 0 && ShopCache.GetBaseConfig().ProductLimitType == "1"))
                    {
                        //SystemLog.Add("价格无权限-limit.IsPriceShow" + limit.IsPriceShow);
                        return -9999999999;
                    }
                }
            }
            if (product.UserLevel_ids_priceshow != "" && !("," + product.UserLevel_ids_priceshow + ",").Contains("," + level.id + ","))
            {
                //SystemLog.Add("价格无权限-product.UserLevel_ids_priceshow" + product.UserLevel_ids_priceshow);
                return -9999999999;
            }
            if (product.UserLevelPrice != "")
            {
                List<ProductUserLevelPrice> UserLevelPrices = UserLevelPrice(product.UserLevelPrice);
                if (UserLevelPrices.Count > 0)
                {
                    foreach (ProductUserLevelPrice sprice in UserLevelPrices)
                    {
                        if (sprice.UserLevel_id == level.id)
                            return sprice.Price;
                    }
                }
            }
            return product.Price / 100 * level.Price;
        }
        public static decimal ProductPrice(Lebi_Product product, Lebi_UserLevel level, Lebi_User user, int count)
        {
            Lebi_Product_Price product_price = B_Lebi_Product_Price.GetModel("Product_id = " + product.id + " and User_id = " + user.id);
            if (product_price != null)
            {
                return product_price.Price;
            }
            if (product.Type_id_ProductType == 320)
            {
                if (product.StepPrice != "")
                {
                    List<ProductStepPrice> StepPrices = StepPrice(product.StepPrice);
                    if (StepPrices.Count > 0)
                    {
                        foreach (ProductStepPrice sprice in StepPrices)
                        {
                            if (count > sprice.Count && sprice.Count >= 1)
                                return sprice.Price;
                        }
                    }
                }

            }
            return ProductPrice(product, level, user);
        }
        /// <summary>
        /// 处理产品分组会员起订量
        /// </summary>
        /// <param name="product"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int ProductLevelCount(Lebi_Product product, Lebi_User user)
        {
            Lebi_UserLevel level = B_Lebi_UserLevel.GetModel(user.UserLevel_id);
            return ProductLevelCount(product, level, user);
        }
        public static int ProductLevelCount(Lebi_Product product, Lebi_UserLevel level, Lebi_User user)
        {
            if (product.UserLevelCount != "")
            {
                List<ProductUserLevelCount> UserLevelCounts = UserLevelCount(product.UserLevelCount);
                if (UserLevelCounts.Count > 0)
                {
                    foreach (ProductUserLevelCount levelcount in UserLevelCounts)
                    {
                        if (levelcount.UserLevel_id == level.id)
                        {
                            if ((product.Type_id_ProductType == 323 || product.Type_id_ProductType == 321 || product.Type_id_ProductType == 322) && product.Time_Expired > System.DateTime.Now)
                            {
                                if (product.Count_Limit > levelcount.Count)
                                {
                                    return levelcount.Count;
                                }
                                else
                                {
                                    return 1;
                                }
                            }
                            else
                            {
                                return levelcount.Count;
                            }
                        }
                    }
                }
            }
            return 1;
        }
        /// <summary>
        /// 计算商品库存-前台显示
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static int ProductStock(Lebi_Product product)
        {
            if (product.IsCombo == 1)
            {
                List<Lebi_Product_Combo> sons = B_Lebi_Product_Combo.GetList("Product_id=" + product.id + "", "");
                int res = 9999999;
                foreach (Lebi_Product_Combo son in sons)
                {
                    if (son.Product_id == son.Product_id_son)
                        return 0;
                    Lebi_Product p = B_Lebi_Product.GetModel(son.Product_id_son);
                    if (p == null)
                        return 0;
                    int p_stock = ProductStock(p);
                    p_stock = p_stock / son.Count;
                    if (res > p_stock)
                        res = p_stock;
                }
                return res - product.Count_Freeze;
            }
            return product.Count_Stock - product.Count_Freeze;
        }
        /// <summary>
        /// 计算商品库存-后台显示
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static int ProductStockForAdmin(Lebi_Product product)
        {
            if (product.IsCombo == 1)
            {
                List<Lebi_Product_Combo> sons = B_Lebi_Product_Combo.GetList("Product_id=" + product.id + "", "");
                int res = 9999999;
                foreach (Lebi_Product_Combo son in sons)
                {
                    if (son.Product_id == son.Product_id_son)
                        return 0;
                    Lebi_Product p = B_Lebi_Product.GetModel(son.Product_id_son);
                    if (p == null)
                        return 0;
                    int p_stock = ProductStockForAdmin(p);
                    p_stock = p_stock / son.Count;
                    if (res > p_stock)
                        res = p_stock;
                }
                return res;
            }
            return product.Count_Stock;
        }
        /// <summary>
        /// 冻结商品的库存
        /// </summary>
        /// <param name="product"></param>
        /// <param name="count"></param>
        public static void ProductStock_Freeze(Lebi_Product product, int count)
        {
            if (product == null)
                return;
            product = B_Lebi_Product.GetModel(product.id);//重新获取数据，防止商品属性被修改 20180830 zhangshijia
            if (product == null)
                return;
            if (product.IsCombo == 1)
                return;
            product.Count_Freeze = product.Count_Freeze + count;
            if (product.Product_id > 0)
            {
                //子商品库存变更，修改父商品库存
                Lebi_Product model = B_Lebi_Product.GetModel(product.Product_id);
                if (model != null)
                {
                    model.Count_Freeze += count;
                    B_Lebi_Product.Update(model);
                }
            }
            B_Lebi_Product.Update(product);
            //Reset_Count_Freeze(product);
            StockChange(product, 0, count, "");
        }
        /// <summary>
        /// 更新库存变动-zhangshijia
        /// </summary>
        /// <param name="product"></param>
        /// <param name="count"></param>
        /// <param name="type"></param> // 300库存变更 301退货单 302销售单
        /// <param name="ordercode"></param>
        /// <param name="orderid"></param>
        public static void ProductStock_Change(Lebi_Product product, int count, int type, Lebi_Order order, string Remark = "")
        {
            if (product == null)
                return;
            product = B_Lebi_Product.GetModel(product.id);//重新获取数据，防止商品属性被修改 20180830 zhangshijia
            if (product == null)
                return;
            //if (product.IsCombo == 1)
            //    return;
            Lebi_Product_Stock_Log log = new Lebi_Product_Stock_Log();
            log.Count = count;
            log.Order_Code = order.Code;
            log.Order_id = order.id;
            log.Product_id = product.id;
            log.Type_id_Stock = type;
            log.Remark = Remark + "；库存：" + (product.Count_Stock + count);
            B_Lebi_Product_Stock_Log.Add(log);

            product.Count_Stock += count;
            if (product.Count_Stock <= product.Count_StockCaution)
            {
                //库存预警
                Log.Add("库存预警", "StockCaution", Convert.ToString(product.id), "<" + product.Count_StockCaution);
            }
            if (type == 302)
            {
                ProductStock_Freeze(product, 0 - count);//减少冻结库存
            }
            if (product.Product_id > 0)
            {
                //子商品库存变更，修改父商品库存
                Lebi_Product model = B_Lebi_Product.GetModel(product.Product_id);
                if (model != null)
                {
                    model.Count_Stock = model.Count_Stock + count;
                    if (ShopCache.GetBaseConfig().IsNullStockDown == "1" && model.Count_Stock < 1)
                        model.Type_id_ProductStatus = 100;
                    B_Lebi_Product.Update(model);
                }
            }
            if (ShopCache.GetBaseConfig().IsNullStockDown == "1" && product.Count_Stock < 1)
                product.Type_id_ProductStatus = 100;
            //<-{更新商品销量  lebi.kingdge 2018-05-01
            if (type != 300)
            {
                product.Count_Sales -= count;
                if (order.DT_id > 0)
                {
                    string sql_dt = "update [Lebi_DT_Product] set Count_Sales=Count_Sales-" + count + " where DT_id = " + order.DT_id + " and Product_id=" + product.id + "";
                    Common.ExecuteSql(sql_dt);
                }
                //更新虚拟销量
                int num = 1;
                int RandCountSales = 0;
                if (ShopCache.GetBaseConfig().SalesFlag == "0")
                {
                    int.TryParse(ShopCache.GetBaseConfig().SalesNum1, out num);
                    RandCountSales = (count * num);
                }
                else
                {
                    int.TryParse(ShopCache.GetBaseConfig().SalesNum2, out num);
                    Random r = new Random();
                    int c = r.Next(1, num);
                    RandCountSales = (count - c);
                }
                if (RandCountSales < 0)
                {
                    product.Count_Sales_Show -= RandCountSales;
                }
                if (product.Product_id > 0)
                {
                    //子商品库存变更，修改父商品库存
                    Lebi_Product model = B_Lebi_Product.GetModel(product.Product_id);
                    if (model != null)
                    {
                        model.Count_Sales -= count;
                        if (RandCountSales < 0)
                        {
                            model.Count_Sales_Show -= RandCountSales;
                        }
                        B_Lebi_Product.Update(model);
                        if (order.DT_id > 0)
                        {
                            string sql_dt = "update [Lebi_DT_Product] set Count_Sales=Count_Sales-" + count + " where DT_id = " + order.DT_id + " and Product_id=" + product.Product_id + "";
                            Common.ExecuteSql(sql_dt);
                        }
                    }
                }
            }
            //}->
            B_Lebi_Product.Update(product);
            StockChange(product, count, 0 - count, "");
        }
        /// <summary>
        /// 计算指定日期及状态的商品数量
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static int GetCount_Product(string dateFrom, string dateTo, string status)
        {
            int count = 0;
            string where = "";
            if (dateFrom != "" && dateTo != "")
            {
                where = "Time_Add>='" + dateFrom + "' and Time_Add<='" + dateTo + "'";
            }
            if (status != "")
            {
                if (dateFrom != "" && dateTo != "")
                {
                    where += " and ";
                }
                where += status;
            }
            count = B_Lebi_Product.Counts(where);
            return count;
        }

        public static string Pro_TagOption(int tag, string lang)
        {
            List<Lebi_Pro_Tag> models = B_Lebi_Pro_Tag.GetList("", "");
            string str = "";
            foreach (Lebi_Pro_Tag model in models)
            {
                string sel = "";
                if (tag == model.id)
                    sel = "selected";
                str += "<option value=\"" + model.id + "\" " + sel + ">" + Language.Content(model.Name, lang) + "</option>";
            }
            return str;

        }
        /// <summary>
        /// 更新商品评分
        /// </summary>
        /// <param name="product"></param>
        public static void UpdateStar(Lebi_Product product)
        {
            string where = "Parentid = 0 and TableName='Product' and (Keyid=" + product.id + "";
            if (product.Product_id != 0)
            {
                where += " or Product_id = " + product.Product_id + "";
            }
            where += ")";
            string count_star = B_Lebi_Comment.GetValue("count(id)", where);
            int Count_Star = 0;
            int.TryParse(count_star, out Count_Star);
            Count_Star = Count_Star == 0 ? 1 : Count_Star;
            string star = B_Lebi_Comment.GetValue("sum(Star)", where);
            decimal Star = 0;
            Decimal.TryParse(star, out Star);
            product.Star_Comment = Star / Count_Star;
            product.Count_Comment = Count_Star;
            B_Lebi_Product.Update(product);
        }

        /// <summary>
        /// 是否拥有子产品
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static bool IsHaveSon(int pid)
        {
            if (pid == 0)
                return false;
            int count = B_Lebi_Product.Counts("Product_id=" + pid + "");
            if (count > 0)
                return true;
            return false;
        }

        /// <summary>
        /// 根据规格ID返回文字
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string ProPertyNameStr(string ids, Lebi_Language_Code lang)
        {
            return ProPertyNameStr(ids, lang.Code);
        }
        public static string ProPertyNameStr(string ids, Lebi_Language lang)
        {
            return ProPertyNameStr(ids, lang.Code);
        }
        public static string ProPertyNameStr(string ids, string lang)
        {
            if (ids == "")
                return "";
            List<Lebi_ProPerty> pros = B_Lebi_ProPerty.GetList("id in (lbsql{" + ids + "})", "parentSort desc");
            string str = "";
            foreach (Lebi_ProPerty p in pros)
            {
                if (str == "")
                    str = Language.Content(p.Name, lang);
                else
                    str += "，" + Language.Content(p.Name, lang);
            }
            return str;
        }
        public static string ProPertyNameStr(Lebi_Product pro, string lang)
        {
            if (pro.Product_id == 0)
                return "";
            return ProPertyNameStr(pro.ProPerty131, lang);
        }
        /// <summary>
        /// 根据规格值获取子商品
        /// propertyids 例如 1,2
        /// </summary>
        /// <param name="pros"></param>
        /// <param name="propertyids"></param>
        /// <returns></returns>
        public static Lebi_Product GetProduct_Son(List<Lebi_Product> pros, string propertyids)
        {
            string key = propertyids;
            if (propertyids.Contains(","))
                key = propertyids.Substring(0, propertyids.IndexOf(","));
            List<Lebi_Product> newpros = (from m in pros
                                          where m.ProPerty131.Contains(key)
                                          select m).ToList();
            if (newpros.Count > 1)
            {
                propertyids = propertyids.Substring(propertyids.IndexOf(",") + 1, (propertyids.Length - propertyids.IndexOf(",") - 1));
                return GetProduct_Son(newpros, propertyids);
            }
            else
                return newpros.FirstOrDefault();


        }
        public static string Categoryid(string id)
        {
            string str = id.ToString();
            List<Lebi_Pro_Type> ts = B_Lebi_Pro_Type.GetList("Parentid=" + id + " and IsShow = 1", "Sort desc");
            foreach (Lebi_Pro_Type t in ts)
            {
                str += "," + Categoryid("" + t.id + "");
            }
            return str;
        }
        /// <summary>
        /// 返回商品的顶级分类
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static Lebi_Pro_Type TopProductType(Lebi_Product pro)
        {
            if (pro == null)
                return new Lebi_Pro_Type();
            Lebi_Pro_Type t = B_Lebi_Pro_Type.GetModel(pro.Pro_Type_id);
            if (t == null)
                return new Lebi_Pro_Type();
            return TopProductType(t);

        }
        public static Lebi_Pro_Type TopProductType(Lebi_Pro_Type t)
        {
            if (t == null)
                return new Lebi_Pro_Type();
            if (t.Parentid == 0)
                return t;
            else
            {
                t = B_Lebi_Pro_Type.GetModel(t.Parentid);
                return TopProductType(t);
            }
        }
        /// <summary>
        /// 返回分类大图标
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ProductTypeImage(Lebi_Pro_Type type)
        {
            if (type == null)
                return "";
            if (type.ImageUrl != "")
            {
                return type.ImageUrl;
            }
            else
            {
                return "";
            }
            type = B_Lebi_Pro_Type.GetModel(type.Parentid);
            return ProductTypeImage(type);
        }
        /// <summary>
        /// 返回分类小图标
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ProductTypeIcon(Lebi_Pro_Type type)
        {
            if (type == null)
                return "";
            if (type.ImageUrl != "")
                return type.ImageSmall;
            type = B_Lebi_Pro_Type.GetModel(type.Parentid);
            return ProductTypeIcon(type);
        }
        /// <summary>
        /// 返回商品单位
        /// </summary>
        /// <param name="pr"></param>
        /// <param name="lang"></param>
        public static string ProductUnit(int id)
        {
            Lebi_Units model = B_Lebi_Units.GetModel(id);
            if (model == null)
                return "";
            return model.Name;
        }
        public static string ProductUnit(Lebi_Product pro, Lebi_Language lang)
        {
            Lebi_Units model = B_Lebi_Units.GetModel(pro.Units_id);
            if (model == null)
                return "";
            return Language.Content(model.Name, lang.Code);
        }
        /// <summary>
        /// 返回商品品牌 
        /// </summary>
        /// <param name="pr"></param>
        /// <param name="lang"></param>
        public static string ProductBrand(int id)
        {
            Lebi_Brand model = B_Lebi_Brand.GetModel(id);
            if (model == null)
                return "";
            return model.Name;
        }
        public static string ProductBrand(Lebi_Product pro, Lebi_Language lang)
        {
            Lebi_Brand model = B_Lebi_Brand.GetModel(pro.Brand_id);
            if (model == null)
                return "";
            return Language.Content(model.Name, lang.Code);
        }
        /// <summary>
        /// 反序列化阶梯价格
        /// </summary>
        /// <param name="pricestr"></param>
        /// <returns></returns>
        public static List<ProductStepPrice> StepPrice(string pricestr)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<ProductStepPrice> models = jss.Deserialize<List<ProductStepPrice>>(pricestr);
            return models;
        }
        /// <summary>
        /// 反序列化会员分组价格
        /// </summary>
        /// <param name="pricestr"></param>
        /// <returns></returns>
        public static List<ProductUserLevelPrice> UserLevelPrice(string pricestr)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<ProductUserLevelPrice> models = jss.Deserialize<List<ProductUserLevelPrice>>(pricestr);
            return models;
        }
        /// <summary>
        /// 反序列化会员分组起订量
        /// </summary>
        /// <param name="countstr"></param>
        /// <returns></returns>
        public static List<ProductUserLevelCount> UserLevelCount(string countstr)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<ProductUserLevelCount> models = jss.Deserialize<List<ProductUserLevelCount>>(countstr);
            return models;
        }
        #region 供应商相关
        /// <summary>
        /// 供应商商品分类
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="id"></param>
        /// <param name="depth"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string SupplierTypeOption(int supplierid, int parentID, int id, int depth, string lang)
        {
            StringBuilder builderTree = new StringBuilder();
            List<Lebi_Supplier_ProductType> nodes = B_Lebi_Supplier_ProductType.GetList("parentid=" + parentID + " and Supplier_id=" + supplierid + "", "Sort desc");
            foreach (Lebi_Supplier_ProductType node in nodes)
            {
                builderTree.Append(string.Format("<option value=\"{0}\" {1}>{2}{3}</option>  \r\n", node.id, ("," + id + ",").Contains("," + node.id + ",") ? "selected=\"selected\"" : "", GetPrefixString(depth), "├" + Language.Content(node.Name, lang)));
                builderTree.Append(SupplierTypeOption(supplierid, node.id, id, depth + 1, lang));
            }


            return builderTree.ToString();
        }
        public static string SupplierTypeOption(int supplierid, int parentID, string id, int depth, string lang)
        {
            StringBuilder builderTree = new StringBuilder();
            List<Lebi_Supplier_ProductType> nodes = B_Lebi_Supplier_ProductType.GetList("parentid=" + parentID + " and Supplier_id=" + supplierid + "", "Sort desc");
            foreach (Lebi_Supplier_ProductType node in nodes)
            {

                builderTree.Append(string.Format("<option value=\"{0}\" {1}>{2}{3}</option>  \r\n", node.id, ("," + id + ",").Contains("," + node.id + ",") ? "selected=\"selected\"" : "", GetPrefixString(depth), "├" + Language.Content(node.Name, lang)));
                builderTree.Append(SupplierTypeOption(supplierid, node.id, id, depth + 1, lang));
            }


            return builderTree.ToString();
        }
        #endregion
        /// <summary>
        /// 生成随机商品编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string RandomProductNumber(int id)
        {
            BaseConfig SYS = ShopCache.GetBaseConfig();
            Random ran = new Random();
            string RandStart = "10000000000000000000";
            string RandEnd = "99999999999999999999";
            int ProductNumberLength = 8;
            if (SYS.ProductNumberLength != "")
                ProductNumberLength = Convert.ToInt16(SYS.ProductNumberLength);
            int RandKey = ran.Next(Convert.ToInt32(RandStart.Substring(0, ProductNumberLength)), Convert.ToInt32(RandEnd.Substring(0, ProductNumberLength)));
            string Number = SYS.ProductNumberPrefix + RandKey;
            int count = B_Lebi_Product.Counts("Number=lbsql{'" + Number + "'} and id!=" + id + " and Product_id=0");
            if (count > 0)
            {
                return RandomProductNumber(id);
            }
            else
            {
                return Number;
            }
        }
        /// <summary>
        /// 获取商家商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Lebi_DT_Product GetDTProduct(int id, int dt_id)
        {
            Lebi_DT_Product product = B_Lebi_DT_Product.GetModel("Product_id = " + id + " and DT_id =" + dt_id + "");
            return product;
        }
        /// <summary>
        /// 重算冻结库存
        /// </summary>
        /// <param name="pro"></param>
        public static void Reset_Count_Freeze(Lebi_Product pro)
        {
            string ProductStockFreezeTime = ShopCache.GetBaseConfig().ProductStockFreezeTime;
            string count_ = "";
            if (ProductStockFreezeTime == "orderconfirm")
            {
                //已确认未完成的有效订单
                count_ = Common.GetValue("select sum(a.Count-a.Count_Shipped) from Lebi_Order_Product as a inner join Lebi_Order as b on a.Order_id=b.id where a.Product_id=" + pro.id + " and b.Type_id_OrderType=211 and b.IsVerified=1 and b.IsCompleted=0 and b.IsInvalid=0");
            }
            else
            {
                //未完成的有效订单
                count_ = Common.GetValue("select sum(a.Count-a.Count_Shipped) from Lebi_Order_Product as a inner join Lebi_Order as b on a.Order_id=b.id where a.Product_id=" + pro.id + " and b.Type_id_OrderType=211 and b.IsCompleted=0 and b.IsInvalid=0");
            }

            int count = 0;
            int.TryParse(count_, out count);
            pro.Count_Freeze = count;
            B_Lebi_Product.Update(pro);
            if (pro.Product_id > 0)
            {
                //Lebi_Product parent = B_Lebi_Product.GetModel(pro.Product_id);
                count_ = Common.GetValue("select sum(Count_Freeze) from Lebi_Product where Product_id = " + pro.Product_id + "");
                //int.TryParse(count_, out count);
                //parent.Count_Freeze = count;
                //B_Lebi_Product.Update(parent);
                Common.ExecuteSql("update Lebi_Product set Count_Freeze=" + count_ + " where id=" + pro.Product_id);
            }

        }
        public static int Count_Freeze(Lebi_Product pro)
        {
            string ProductStockFreezeTime = ShopCache.GetBaseConfig().ProductStockFreezeTime;
            string count_ = "";
            if (ProductStockFreezeTime == "orderconfirm")
            {
                //已确认未完成的有效订单
                count_ = Common.GetValue("select sum(a.Count-a.Count_Shipped) from Lebi_Order_Product as a inner join Lebi_Order as b on a.Order_id=b.id where a.Product_id=" + pro.id + " and b.Type_id_OrderType=211 and b.IsVerified=1 and b.IsCompleted=0 and b.IsInvalid=0");
            }
            else
            {
                //未完成的有效订单
                count_ = Common.GetValue("select sum(a.Count-a.Count_Shipped) from Lebi_Order_Product as a inner join Lebi_Order as b on a.Order_id=b.id where a.Product_id=" + pro.id + " and b.Type_id_OrderType=211 and b.IsCompleted=0 and b.IsInvalid=0");
            }

            int count = 0;
            int.TryParse(count_, out count);
            return count;

        }
    }
}