using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.product
{
    public partial class product_Edit_Window : AdminAjaxBase
    {
        protected Lebi_Product model;
        protected string PageReturnMsg = "";
        protected int action;
        protected string t = "";
        protected int id;
        protected bool wap;
        protected List<ProductStepPrice> StepPrices;
        protected List<ProductUserLevelPrice> UserLevelPrices;
        protected List<ProductUserLevelCount> UserLevelCounts;
        protected List<Lebi_UserLevel> userlevels;
        protected List<Lebi_Product_Combo> comboProducts;
        protected void Page_Load(object sender, EventArgs e)
        {
            Random Random = new Random();
            t = RequestTool.RequestString("t");
            id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Product.GetModel(id);
            int Type_id_ProductType = RequestTool.RequestInt("Type_id_ProductType", 320);
            if (id == 0 || (id > 0 && t == "copy"))
            {
                if (!EX_Admin.Power("product_add", "添加商品"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
                ////如果添加商品时随机数小于9位数 重定向生成随机数 防止破坏已有数据
                //if (randnum.ToString().Length < 9)
                //{
                //    Response.Redirect(site.AdminPath + "/product/product_edit.aspx?id=" + id + "&t=" + t + "&randnum=" + Random.Next(100000000, 999999999));
                //    Response.End();
                //    return;
                //}
            }
            else
            {
                if (!EX_Admin.Power("product_edit", "编辑商品"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
                if (site.SiteCount > 1 && EX_Admin.Project().Site_ids != "" && model.Site_ids != "")
                {
                    string[] psids = model.Site_ids.Split(',');
                    bool flag = false;
                    foreach (string pdis in psids)
                    {
                        if (("," + EX_Admin.Project().Site_ids + ",").Contains("," + pdis + ","))
                        {
                            flag = true;
                        }
                    }
                    if (!flag)
                    {
                        PageReturnMsg = PageErrorMsg();
                    }
                }
                if (!string.IsNullOrEmpty(EX_Admin.Project().Supplier_ids))
                {
                    bool flag = false;
                    if (("," + EX_Admin.Project().Supplier_ids + ",").Contains("," + model.Supplier_id + ","))
                    {
                        flag = true;
                    }
                    if (!flag)
                    {
                        PageReturnMsg = PageErrorMsg();
                    }
                }
            }
            action = RequestTool.RequestInt("action", 1);
            if (model == null)
            {
                model = new Lebi_Product();
                model.Site_ids = site.Sitesid();
                model.Type_id_ProductType = Type_id_ProductType;
                Lebi_Product modellast = B_Lebi_Product.GetModel("(IsDel!=1 or IsDel is null) and Type_id_ProductType = " + Type_id_ProductType + " and Supplier_id = 0 order by Time_Edit desc,id desc");
                if (modellast != null)
                    model.Pro_Type_id = modellast.Pro_Type_id;
            }
            else
            {
                if (t == "copy")
                    model.id = 0;
            }
            if (model.Images != "")
            {
                if (model.Images.Substring(model.Images.Length-1, 1) == "@")
                {
                    model.Images = model.Images.Substring(0,model.Images.Length-1);
                }
                if (model.Images.Substring(0, 1) != "@")
                {
                    model.Images = "@" + model.Images;
                }
            }
            StepPrices = EX_Product.StepPrice(model.StepPrice);
            if (StepPrices == null)
                StepPrices = new List<ProductStepPrice>();
            UserLevelPrices = EX_Product.UserLevelPrice(model.UserLevelPrice);
            if (UserLevelPrices == null)
                UserLevelPrices = new List<ProductUserLevelPrice>();
            UserLevelCounts = EX_Product.UserLevelCount(model.UserLevelCount);
            if (UserLevelCounts == null)
                UserLevelCounts = new List<ProductUserLevelCount>();
            userlevels = B_Lebi_UserLevel.GetList("", "Grade asc");
            wap = Ishavewap();

            if (model.IsCombo == 1)
                comboProducts = B_Lebi_Product_Combo.GetList("Product_id=" + model.id + "", "");
            else
                comboProducts = new List<Lebi_Product_Combo>();
        }
        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Lebi_Supplier GetSupplier(int id)
        {
            Lebi_Supplier user = B_Lebi_Supplier.GetModel("id = " + id);
            if (user == null)
                user = new Lebi_Supplier();
            return user;
        }
        private bool Ishavewap()
        {
            int count = B_Lebi_Site.Counts("IsMobile=1");
            if (count > 0)
                return true;
            return false;
        }
        public int CountSon(int pid)
        {
            return B_Lebi_Product.Counts("(IsDel!=1 or IsDel is null) and Product_id=" + pid + " and Product_id<>0");
        }
        public decimal GetUserLevelPrice(int id)
        {
            try
            {
                return (from m in UserLevelPrices
                        where m.UserLevel_id == id
                        select m).ToList().FirstOrDefault().Price;
            }
            catch
            {
                return 0;
            }
        }
        public int GetUserLevelCount(int id)
        {
            try
            {
                return (from m in UserLevelCounts
                        where m.UserLevel_id == id
                        select m).ToList().FirstOrDefault().Count;
            }
            catch
            {
                return 1;
            }
        }
        public string userlevels_checkbox(string name, string ids, string ex = "")
        {
            string str = "";
            List<Lebi_UserLevel> ms = B_Lebi_UserLevel.GetList("", "Grade asc");
            foreach (Lebi_UserLevel m in ms)
            {
                string sel = "";
                if (("," + ids + ",").Contains("," + m.id + ","))
                    sel = "checked";
                str += "<label><input type=\"checkbox\" name=\"" + name + "\" value=\"" + m.id + "\" " + sel + " " + ex + ">" + Lang(m.Name) + "</label> &nbsp;&nbsp;";
            }
            return str;
        }
    }
}