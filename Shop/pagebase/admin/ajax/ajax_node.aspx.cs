using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;


namespace Shop.Admin.Ajax
{
    public partial class Ajax_node : AdminAjaxBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }

        /// <summary>
        /// 编辑结点
        /// </summary>
        public void Node_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Node model = B_Lebi_Node.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Node();
            }
            B_Lebi_Node.BindForm(model);
            if (model.id == 0)
            {
                if (!EX_Admin.Power("node_add", "添加结点"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Node.Add(model);
                id = B_Lebi_Node.GetMaxId();
                string action = Tag("添加结点");
                string description = model.Name;
                Log.Add(action, "Node", model.id.ToString(), CurrentAdmin, description);
            }
            else
            {
                if (!EX_Admin.Power("node_edit", "编辑结点"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Node.Update(model);
                string action = Tag("编辑结点");
                string description = model.Name;
                Log.Add(action, "Node", model.id.ToString(), CurrentAdmin, description);
            }
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }
        /// <summary>
        /// 编辑用户结点
        /// </summary>
        public void UserNode_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Node model = B_Lebi_Node.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Node();
            }
            B_Lebi_Node.BindForm(model);
            model.Language = Language.LanuageidsToCodes(model.Language_ids);
            if (model.id == 0)
            {
                if (!EX_Admin.Power("usernode_add", "添加自定义结点"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Node.Add(model);
                id = B_Lebi_Node.GetMaxId();
                string action = Tag("添加自定义结点");
                string description = model.Name;
                Log.Add(action, "Node", model.id.ToString(), CurrentAdmin, description);
            }
            else
            {
                if (!EX_Admin.Power("usernode_edit", "编辑自定义结点"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Node.Update(model);
                string action = Tag("编辑自定义结点");
                string description = model.Name;
                Log.Add(action, "Node", model.id.ToString(), CurrentAdmin, description);
            }
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }
        /// <summary>
        /// 编辑自定义结点
        /// </summary>
        public void CustomNode_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            string code = RequestTool.RequestString("parentcode");
            Lebi_Node topnode = NodePage.GetNodeByCode(code);
            Lebi_Node model = B_Lebi_Node.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Node();
            }
            B_Lebi_Node.BindForm(model);
            if (topnode.IsLanguages == 1)
                model.Name = Language.RequestString("Name");
            if (model.id == 0)
            {
                if (!EX_Admin.Power("usernode_add", "添加自定义结点"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Node.Add(model);
                id = B_Lebi_Node.GetMaxId();
                string action = Tag("添加自定义结点");
                string description = model.Name;
                Log.Add(action, "Node", model.id.ToString(), CurrentAdmin, description);
            }
            else
            {
                if (!EX_Admin.Power("usernode_edit", "编辑自定义结点"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Node.Update(model);
                string action = Tag("编辑自定义结点");
                string description = model.Name;
                Log.Add(action, "Node", model.id.ToString(), CurrentAdmin, description);
            }
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }
        /// <summary>
        /// 删除结点
        /// </summary>
        public void Node_Del()
        {
            if (!EX_Admin.Power("node_del", "删除结点"))
            {
                EX_Admin.NoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Node model = B_Lebi_Node.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            string action = Tag("删除结点");
            string description = Shop.Bussiness.Language.Content(model.Name, "CN");
            Log.Add(action, "Node", id.ToString(), CurrentAdmin, description);
            B_Lebi_Node.Delete(id);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除自定义结点
        /// </summary>
        public void UserNode_Del()
        {
            if (!EX_Admin.Power("node_del", "删除自定义结点"))
            {
                EX_Admin.NoPower();
                return;
            }
            string ids = RequestTool.RequestString("ids");
            string PageCode = RequestTool.RequestString("PageCode");
            if (ids != "")
            {
                List<Lebi_Node> nodes = B_Lebi_Node.GetList("id in (lbsql{" + ids + "})", "");
                foreach (Lebi_Node node in nodes)
                {
                    B_Lebi_Node.Delete("id=" + node.id + " or parentid=" + node.id + "");
                    B_Lebi_Page.Delete("Node_id=" + node.id + "");
                }

                string action = Tag("删除自定义结点");
                string description = "";
                Log.Add(action, "Node", ids, CurrentAdmin, description);
            }
            if (PageCode == "P_Help")
            {
                Lebi_Theme_Page themepage = B_Lebi_Theme_Page.GetModel("Code='P_Help'");
                if (themepage.Type_id_PublishType == 122)
                    PageStatic.Greate_Help(themepage);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑页面
        /// </summary>
        public void Page_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Page model = B_Lebi_Page.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Page();
            }
            B_Lebi_Page.BindForm(model);
            model.Language_ids = RequestTool.RequestSafeString("Language_ids");
            model.Language = Language.LanuageidsToCodes(model.Language_ids);
            if (model.id == 0)
            {
                if (!EX_Admin.Power("page_add", "添加结点内容"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Page.Add(model);
                id = B_Lebi_Page.GetMaxId();
                string action = Tag("添加结点内容");
                string description = model.Name;
                Log.Add(action, "Page", id.ToString(), CurrentAdmin, description);
            }
            else
            {
                if (!EX_Admin.Power("page_edit", "编辑结点内容"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Page.Update(model);
                string action = Tag("编辑结点内容");
                string description = model.Name;
                Log.Add(action, "Page", id.ToString(), CurrentAdmin, description);
            }
            //=========================================
            //处理静态页面
            Lebi_Node node = B_Lebi_Node.GetModel(model.Node_id);
            Lebi_Theme_Page themepage;
            if (node.Code == "About")
                themepage = B_Lebi_Theme_Page.GetModel("Code='P_About'");
            else if (node.Code == "News")
                themepage = B_Lebi_Theme_Page.GetModel("Code='P_NewsDetails'");
            else if (node.Code == "Help")
                themepage = B_Lebi_Theme_Page.GetModel("Code='P_Help'");
            else
                themepage = B_Lebi_Theme_Page.GetModel("Code='P_ArticleDetails'");
            if (themepage.Type_id_PublishType == 122)//静态发布页面
            {
                if (node.Code == "Help")
                    PageStatic.Greate_Help(themepage);
                else
                    PageStatic.Greate_InfoPage(model, themepage);
            }
            ImageHelper.LebiImagesUsed(model.ImageOriginal, "page", id);
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");
        }
        /// <summary>
        /// 删除页面
        /// </summary>
        public void Page_Del()
        {
            if (!EX_Admin.Power("page_del", "删除结点内容"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            if (id == "")
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            B_Lebi_Page.Delete("id in (lbsql{" + id + "})");
            //删除图片
            ImageHelper.LebiImagesDelete("page", id);
            string action = Tag("删除结点内容");
            string description = id;
            Log.Add(action, "Page", id, CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 导航子类
        /// </summary>
        public void NavSon()
        {
            int nodeid = RequestTool.RequestInt("nodeid", 0);
            List<Lebi_Page> models = B_Lebi_Page.GetList("Node_id=" + nodeid + " and Language='" + CurrentLanguage.Code + "'", "Sort desc,id desc");
            StringBuilder builderTree = new StringBuilder();
            foreach (Lebi_Page model in models)
            {
                // builderTree.Append(string.Format("<option value=\"{0}\">{1}</option>  \r\n", ThemeUrl.u , model.Name));
            }
        }
        /// <summary>
        /// 删除图片
        /// </summary>
        public void Image_Del()
        {
            if (!EX_Admin.Power("image_del", "删除图库"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            if (id == "")
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            List<Lebi_Image> models = B_Lebi_Image.GetList("id in (lbsql{" + id + "})", "");
            foreach (Lebi_Image model in models)
            {

                string[] arr = model.Size.Split(',');
                foreach (string img in arr)
                {
                    ImageHelper.DeleteImage(model.Image.Replace("w$h", img));
                }
                ImageHelper.DeleteImage(model.Image);
            }
            B_Lebi_Image.Delete("id in (lbsql{" + id + "})");
            Response.Write("{\"msg\":\"OK\"}");

        }
    }
}