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
using System.IO;
using System.Text.RegularExpressions;

namespace Shop.admin.plugin.taobao
{
    /// <summary>
    /// 淘宝同步
    /// </summary>
    public partial class ajax : AdminAjaxBase
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
        /// 检查授权信息
        /// </summary>
        public void Taobao_CheckUser()
        {
            LBAPI api = Shop.LebiAPI.Service.Instanse.Taobao_CheckUser();
            if (api == null)
                Response.Write("{\"msg\":\"系统异常\"}");
            else
            {
                if (api.msg == "OK")
                {
                    B_BaseConfig.Set("platform_taobao_shopnick", api.data);
                    Response.Write("{\"msg\":\"OK\",\"nick\":\"" + api.data + "\"}");
                }
                else
                {
                    Response.Write("{\"msg\":\"" + api.msg + "\"}");
                }
            }
        }
        /// <summary>
        /// 下载商品分类
        /// </summary>
        public void Taobao_category()
        {
            List<Lebi_Pro_Type> ts = Shop.LebiAPI.Service.Instanse.Taobao_ProType();
            if (ts == null)
                Response.Write("{\"msg\":\"系统异常\"}");
            else
            {
                List<Lebi_Pro_Type> parents = (from m in ts
                                               where m.Parentid == 0
                                               select m).ToList();
                foreach (Lebi_Pro_Type t in parents)
                {
                    int pid = t.id;
                    Lebi_Pro_Type model = DoOneCategory(t);
                    List<Lebi_Pro_Type> sons = (from m in ts
                                                where m.Parentid == pid
                                                select m).ToList();
                    foreach (Lebi_Pro_Type sont in sons)
                    {
                        sont.Parentid = model.id;
                        DoOneCategory(sont);
                    }
                }
                ShopCache.SetProductType();
                Response.Write("{\"msg\":\"OK\"}");
            }
        }
        /// <summary>
        /// 处理一个商品分类
        /// </summary>
        private Lebi_Pro_Type DoOneCategory(Lebi_Pro_Type t)
        {
            Lebi_Pro_Type model = B_Lebi_Pro_Type.GetModel("taobaoid='" + t.taobaoid + "'");
            if (model == null)
            {
                model = t;
                if (t.ProPerty131 != "")
                {
                    List<Lebi_ProPerty> ps131 = B_Lebi_ProPerty.GetList("taobaoid in (" + t.ProPerty131 + ")", "");
                    if (ps131 != null)
                    {
                        string pid131 = "";
                        foreach (Lebi_ProPerty p131 in ps131)
                        {
                            if (pid131 == "")
                                pid131 = p131.id.ToString();
                            else
                                pid131 += "," + p131.id.ToString(); ;
                        }
                        model.ProPerty131 = pid131;
                    }
                }
                if (t.ProPerty132 != "")
                {
                    List<Lebi_ProPerty> ps132 = B_Lebi_ProPerty.GetList("taobaoid in (" + t.ProPerty132 + ")", "");
                    if (ps132 != null)
                    {
                        string pid132 = "";
                        foreach (Lebi_ProPerty p132 in ps132)
                        {
                            if (pid132 == "")
                                pid132 = p132.id.ToString();
                            else
                                pid132 += "," + p132.id.ToString(); ;
                        }
                        model.ProPerty132 = pid132;
                    }
                }
                B_Lebi_Pro_Type.Add(model);
                model.id = B_Lebi_Pro_Type.GetMaxId();
            }
            return model;
        }
        /// <summary>
        /// 下载商品属性规格
        /// </summary>
        public void Taobao_property()
        {
            List<Lebi_ProPerty> ps = Shop.LebiAPI.Service.Instanse.Taobao_ProPerty();
            if (ps == null)
                Response.Write("{\"msg\":\"系统异常\"}");
            else
            {
                List<Lebi_ProPerty> parents = (from m in ps
                                               where m.parentid == 0
                                               select m).ToList();
                foreach (Lebi_ProPerty p in parents)
                {
                    if (B_Lebi_ProPerty.Counts("taobaoid='" + p.taobaoid + "'") == 0)
                    {
                        B_Lebi_ProPerty.Add(p);
                        int pid = B_Lebi_ProPerty.GetMaxId();
                        List<Lebi_ProPerty> sons = (from m in ps
                                                    where m.parentid == p.id
                                                    select m).ToList();
                        foreach (Lebi_ProPerty son in sons)
                        {
                            son.parentid = pid;
                            B_Lebi_ProPerty.Add(son);
                        }
                    }
                }
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 下载商品
        /// </summary>
        public void Taobao_Product()
        {
            List<Lebi_Product> ps = Shop.LebiAPI.Service.Instanse.Taobao_Product();
            if (ps == null)
                Response.Write("{\"msg\":\"系统异常\"}");
            else
            {
                List<Lebi_Product> parents = (from m in ps
                                              where m.Product_id == 0
                                              select m).ToList();
                foreach (Lebi_Product p in parents)
                {
                    if (B_Lebi_Product.Counts("taobaoid='" + p.taobaoid + "'") == 0)
                    {
                        int pid = p.id;
                        Lebi_Product model = DoOneProduct(p);
                        List<Lebi_Product> sons = (from m in ps
                                                   where m.Product_id == pid
                                                   select m).ToList();
                        foreach (Lebi_Product son in sons)
                        {
                            son.Product_id = model.id;
                            DoOneProduct(son);
                        }
                    }
                }
                Response.Write("{\"msg\":\"OK\"}");
            }
        }
        /// <summary>
        /// 处理添加一个商品
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private Lebi_Product DoOneProduct(Lebi_Product pro)
        {
            Lebi_Pro_Type t=new Lebi_Pro_Type();
            string ids = pro.taobaoid_type;
            if (ids != "")
            {
                ids = "'0" + ids.Replace(",", "','") + "0'";
                List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList("taobaoid in (lbsql{" + ids + "})", "");
                t = models.FirstOrDefault();
            }

            if (t == null)
                t = new Lebi_Pro_Type();
            string p131ids = pro.ProPerty131;
            string p132ids = pro.ProPerty132;
            if (p131ids != "")
            {
                p131ids = "'" + p131ids.Replace(",", "','") + "'";
                List<Lebi_ProPerty> p131s = B_Lebi_ProPerty.GetList("taobaoid in (" + p131ids + ")", "");
                p131ids = "";
                foreach (Lebi_ProPerty p in p131s)
                {
                    if (p131ids == "")
                        p131ids = p.id.ToString();
                    else
                        p131ids += "," + p.id.ToString();
                }
            }
            if (p132ids != "")
            {
                p132ids = "'" + p132ids.Replace(",", "','") + "'";
                List<Lebi_ProPerty> p132s = B_Lebi_ProPerty.GetList("taobaoid in (" + p132ids + ")", "");
                p132ids = "";
                foreach (Lebi_ProPerty p in p132s)
                {
                    if (p132ids == "")
                        p132ids = p.id.ToString();
                    else
                        p132ids += "," + p.id.ToString();
                }
            }
            pro.ProPerty131 = p131ids;
            pro.ProPerty132 = p132ids;
            pro.Pro_Type_id = t.id;
            B_Lebi_Product.Add(pro);
            pro.id = B_Lebi_Product.GetMaxId();

            return pro;
        }

    }
}