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

namespace Shop.Bussiness
{
    public class NodePage
    {

        public NodePage()
        {

        }
        /// <summary>
        /// 根据结点代码返回结点实体
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Lebi_Node GetNodeByCode(string code)
        {
            Lebi_Node model = B_Lebi_Node.GetList("code='" + code + "'", "Sort desc").FirstOrDefault();
            if (model == null)
                model = new Lebi_Node();
            return model;
        }
        public static Lebi_Node GetNodeByCode(string code, int Supplier_id)
        {
            Lebi_Node model = B_Lebi_Node.GetList("Supplier_id = " + Supplier_id + " and code='" + code + "'", "Sort desc").FirstOrDefault();
            if (model == null)
                model = new Lebi_Node();
            return model;
        }
        /// <summary>
        /// 根据结点代码返回默认页面实体
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Lebi_Page GetPageByCode(string code,int Supplier_id = 0)
        {
            Lebi_Node node = GetNodeByCode(code);
            return GetPageByNode(node,Supplier_id);
        }
        /// <summary>
        /// 根据结点实体返回默认页面实体
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Lebi_Page GetPageByNode(Lebi_Node node)
        {
            Lebi_Page model = B_Lebi_Page.GetList("Node_id=" + node.id + "", "Sort desc").FirstOrDefault();
            if (model == null)
                model = new Lebi_Page();
            return model;
        }
        public static Lebi_Page GetPageByNode(Lebi_Node node, int Supplier_id)
        {
            Lebi_Page model = B_Lebi_Page.GetList("Supplier_id = " + Supplier_id + " and Node_id=" + node.id + "", "Sort desc").FirstOrDefault();
            if (model == null)
                model = new Lebi_Page();
            return model;
        }
        /// <summary>
        /// 返回结点类型名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string NodeType(int id)
        {
            string str = "";
            switch (id)
            {
                case 1:
                    str = "页面分类";
                    break;
                case 2:
                    str = "结点分类";
                    break;
                case 3:
                    str = "独立内容";
                    break;
            }
            return str;
        }
        /// <summary>
        /// 结点管理地址
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string AdminIndexPage(Lebi_Node node)
        {
            Site site = new Site();
            string url = GetIndexPage(node, node.TypeFlag);
            switch (node.TypeFlag)
            {
                case 3:
                    Lebi_Page page = B_Lebi_Page.GetList("Node_id=" + node.id + "", "Sort desc").FirstOrDefault();
                    if (page == null)
                        page = new Lebi_Page();
                    url = site.AdminPath + "/" + url + "?Node_id=" + node.id + "&Page_id=" + page.id;
                    break;
                default:
                    url = site.AdminPath + "/" + url + "?Node_id=" + node.id;
                    break;

            }

            return url;
        }
        public static string AdminIndexPage(int nodeid)
        {
            Lebi_Node node = B_Lebi_Node.GetModel(nodeid);
            if (node == null)
                return "";
            return AdminIndexPage(node);
        }
        /// <summary>
        /// 取得管理首页，自动继承上级设置
        /// </summary>
        /// <param name="node"></param>
        /// <param name="TypeFlag"></param>
        /// <returns></returns>
        static string GetIndexPage(Lebi_Node node, int TypeFlag)
        {
            string url = "";
            switch (TypeFlag)
            {
                case 3:
                    if (node.AdminPage != "")
                        url = node.AdminPage;
                    else
                    {
                        node = B_Lebi_Node.GetModel(node.parentid);
                        url = GetIndexPage(node, TypeFlag);
                    }
                    break;
                default:
                    if (node.AdminPage != "")
                        url = node.AdminPage_Index;
                    else
                    {
                        node = B_Lebi_Node.GetModel(node.parentid);
                        url = GetIndexPage(node, TypeFlag);
                    }
                    break;

            }
            return url;
        }
        /// <summary>
        /// 结点内容编辑地址
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string AdminPage(Lebi_Node node, Lebi_Page page)
        {
            string adminpath = RequestTool.GetConfigKey("AdminPath");
            string url = GetPage(node);
            if (page == null)
                page = new Lebi_Page();
            url = adminpath +"/"+ url + "?Node_id=" + node.id + "&Page_id=" + page.id;
            return url;
        }
        public static string AdminPage(Lebi_Page page)
        {
            Lebi_Node node = B_Lebi_Node.GetModel(page.Node_id);
            return AdminPage(node, page);
        }
        public static string AdminPage(Lebi_Node node)
        {
            string adminpath = RequestTool.GetConfigKey("AdminPath");
            string url = GetPage(node);
            url = adminpath +"/"+ url + "?Node_id=" + node.id;
            return url;
        }
        /// <summary>
        /// 取得管理首页，自动继承上级设置
        /// </summary>
        /// <param name="node"></param>
        /// <param name="TypeFlag"></param>
        /// <returns></returns>
        static string GetPage(Lebi_Node node)
        {
            string url = "";

            if (node.AdminPage != "")
                url = node.AdminPage;
            else
            {
                node = B_Lebi_Node.GetModel(node.parentid);
                url = GetPage(node);
            }
            return url;
        }

    }

}

