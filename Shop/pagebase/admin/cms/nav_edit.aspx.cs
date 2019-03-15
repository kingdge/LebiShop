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

namespace Shop.Admin.cms
{
    public partial class Nav_Edit : AdminPageBase
    {
        protected Lebi_Page page;
        protected Lebi_Node node;
        protected Lebi_Node pnode;
        protected void Page_Load(object sender, EventArgs e)
        {
            int Node_id = RequestTool.RequestInt("Node_id", 0);
            int Page_id = RequestTool.RequestInt("Page_id", 0);
            page = B_Lebi_Page.GetModel(Page_id);
            if (page != null)
            {
                if (!EX_Admin.Power("page_edit", "编辑结点内容"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
                
                Node_id = page.Node_id;
            }
            else
            {
                if (!EX_Admin.Power("page_add", "添加结点内容"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
                
                page = new Lebi_Page();
            }
            node = B_Lebi_Node.GetModel(Node_id);
            if (node == null)
            {
                node = new Lebi_Node();
            }
            pnode = B_Lebi_Node.GetModel(node.parentid);
            if (pnode == null)
                pnode = new Lebi_Node();
        }
        /// <summary>
        /// 递归生成下拉菜单
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="parentID"></param>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public string GetMenu(string url)
        {

            StringBuilder builderTree = new StringBuilder();
            List<Lebi_Node> nodes = new List<Lebi_Node>();
            Lebi_Node pnode;
            string[] arr = url.Split(':');
            string code = "";
            string value = "";
            if (arr.Length > 1)
            {
                code = arr[0];
                value = arr[1];
            }
            string sel = "";
            //添加自定义文章
            pnode = NodePage.GetNodeByCode("Info", 0);
            if (code == "P_Article" && value == pnode.id.ToString())
                sel = "selected";
            builderTree.Append("<option code=\"P_Article\" value=\"\" " + sel + ">" + pnode.Name + "</option>");
            nodes = B_Lebi_Node.GetList("parentid =" + pnode.id + "", "");
            if (nodes.Count > 0)
            {
                foreach (Lebi_Node node in nodes)
                {
                    sel = "";
                    if (code == "P_Article" && value == node.id.ToString())
                        sel = "selected";
                    builderTree.Append(string.Format("<option code=\"P_Article\" value=\"{0}\" " + sel + "> |- {1}</option>  \r\n", node.id, node.Name));
                }

            }
            //添加帮助中心
            pnode = NodePage.GetNodeByCode("Help", 0);
            sel = "";
            if (code == "P_Help" && value == pnode.id.ToString())
                sel = "selected";
            builderTree.Append("<option code=\"P_Help\" value=\"\" " + sel + ">" + pnode.Name + "</option>");
            nodes = B_Lebi_Node.GetList("parentid =" + pnode.id + "", "");
            if (nodes.Count > 0)
            {
                foreach (Lebi_Node node in nodes)
                {
                    sel = "";
                    if (code == "P_Help" && value == node.id.ToString())
                        sel = "selected";
                    builderTree.Append(string.Format("<option code=\"P_Help\" value=\"{0}\" " + sel + "> |- {1}</option>  \r\n", node.id, node.Name));
                }

            }
            //添加公司介绍
            pnode = NodePage.GetNodeByCode("About", 0);
            sel = "";
            if (code == "P_About" && value == "0")
                sel = "selected";
            builderTree.Append("<option code=\"P_About\" value=\"\" " + sel + ">" + pnode.Name + "</option>");
            List<Lebi_Page> ps = B_Lebi_Page.GetList("Node_id =" + pnode.id + "", "");
            if (ps.Count > 0)
            {
                foreach (Lebi_Page p in ps)
                {
                    sel = "";
                    if (code == "P_About" && value == p.id.ToString())
                        sel = "selected";
                    builderTree.Append(string.Format("<option code=\"P_About\" value=\"{0}\" " + sel + "> |- {1}</option>  \r\n", p.id, p.Name));
                }

            }
            //添加商城动态
            pnode = NodePage.GetNodeByCode("News", 0);
            sel = "";
            if (code == "P_News" && value == pnode.id.ToString())
                sel = "selected";
            builderTree.Append(string.Format("<option code=\"P_News\" value=\"{0}\" " + sel + ">{1}</option>  \r\n", pnode.id, pnode.Name));
            //添加商品专题
            //List<Lebi_Tab> ts = B_Lebi_Tab.GetList("", "Tsort desc");
            //if (ts.Count > 0)
            //{
            //    foreach (Lebi_Tab t in ts)
            //    {
            //        sel = "";
            //        if (code == "P_Tab" && value == t.id.ToString())
            //            sel = "selected";
            //        builderTree.Append(string.Format("<option code=\"P_Tab\" value=\"{0}\" " + sel + ">[" + Tag("商品专题") + "]{1}</option>  \r\n", t.id, Language.Content(t.Tname, CurrentLanguage)));
            //    }

            //}
            sel = "";
            if (code == "P_Index")
                sel = "selected";
            builderTree.Append("<option code=\"P_Index\" value=\"\" " + sel + ">" + Tag("首页") + "</option>");
            sel = "";
            if (code == "P_Inquiry")
                sel = "selected";
            builderTree.Append("<option code=\"P_Inquiry\" value=\"\" " + sel + ">" + Tag("留言反馈") + "</option>");
            sel = "";
            if (code == "P_LimitBuy")
                sel = "selected";
            builderTree.Append("<option code=\"P_LimitBuy\" value=\"\" " + sel + ">" + Tag("限时抢购") + "</option>");
            sel = "";
            if (code == "P_GroupPurchase")
                sel = "selected";
            builderTree.Append("<option code=\"P_GroupPurchase\" value=\"\" " + sel + ">" + Tag("团购") + "</option>");
            sel = "";
            if (code == "P_Exchange")
                sel = "selected";
            builderTree.Append("<option code=\"P_Exchange\" value=\"\" " + sel + ">" + Tag("积分换购") + "</option>");
            sel = "";
            if (code == "P_AllProductCategories")
                sel = "selected";
            builderTree.Append("<option code=\"P_AllProductCategories\" value=\"\" " + sel + ">" + Tag("全部商品分类") + "</option>");
            sel = "";
            if (code == "P_Basket")
                sel = "selected";
            builderTree.Append("<option code=\"P_Basket\" value=\"\" " + sel + ">" + Tag("购物车") + "</option>");
            sel = "";
            if (code == "P_FriendLink")
                sel = "selected";
            builderTree.Append("<option code=\"P_FriendLink\" value=\"\" " + sel + ">" + Tag("友情链接") + "</option>");
            sel = "";
            if (code == "P_ProductCommentIndex")
                sel = "selected";
            builderTree.Append("<option code=\"P_ProductCommentIndex\" value=\"\" " + sel + ">" + Tag("商品晒单") + "</option>");
            sel = "";
            if (code == "P_Register")
                sel = "selected";
            builderTree.Append("<option code=\"P_Register\" value=\"\" " + sel + ">" + Tag("会员注册") + "</option>");
            sel = "";
            if (code == "P_Login")
                sel = "selected";
            builderTree.Append("<option code=\"P_Login\" value=\"\" " + sel + ">" + Tag("会员登录") + "</option>");
            sel = "";
            if (code == "P_UserCenter")
                sel = "selected";
            builderTree.Append("<option code=\"P_UserCenter\" value=\"\" " + sel + ">" + Tag("会员中心") + "</option>");
            sel = "";
            if (code == "P_BrandList")
                sel = "selected";
            builderTree.Append("<option code=\"P_BrandList\" value=\"\" " + sel + ">" + Tag("品牌列表") + "</option>");
            sel = "";
            if (code == "P_SupplierRegister")
                sel = "selected";
            builderTree.Append("<option code=\"P_SupplierRegister\" value=\"\" " + sel + ">" + Tag("供应商注册") + "</option>");
            sel = "";
            if (url.Contains("/"))
                sel = "selected";
            builderTree.Append("<option value=\"0\" " + sel + ">" + Tag("自定义") + "</option>");
            return builderTree.ToString();
        }
    }
}