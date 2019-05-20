using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.supplier.product
{
    public partial class product_Edit : SupplierPageBase
    {
        protected Lebi_Product model;
        protected int action;
        protected int id;
        protected bool wap;
        protected bool addflag=false;
        protected List<ProductStepPrice> StepPrices;
        protected List<ProductUserLevelPrice> UserLevelPrices;
        protected List<ProductUserLevelCount> UserLevelCounts;
        protected List<Lebi_UserLevel> userlevels;
        protected void Page_Load(object sender, EventArgs e)
        {

            id = RequestTool.RequestInt("id",0);
            if (id == 0)
            {
                if (!Power("supplier_product_add", "添加商品"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
                addflag = true;
            }
            else
            {
                if (!Power("supplier_product_edit", "编辑商品"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
            }
            action = RequestTool.RequestInt("action", 1);
            model = B_Lebi_Product.GetModel("(IsDel!=1 or IsDel is null) and Supplier_id = " + CurrentSupplier.id + " and id = "+ id +"");
            if (model == null){
                model = new Lebi_Product();
                Lebi_Product modellast = B_Lebi_Product.GetModel("(IsDel!=1 or IsDel is null) and Supplier_id = " + CurrentSupplier.id + " order by Time_Edit desc,id desc");
                if (modellast != null)
                    model.Pro_Type_id = modellast.Pro_Type_id;
            }
            if (model.Images != "")
            {
                if (model.Images.Substring(model.Images.Length - 1, 1) == "@")
                {
                    model.Images = model.Images.Substring(0, model.Images.Length - 1);
                }
                if (model.Images.Substring(0, 1) != "@")
                {
                    model.Images = "@" + model.Images;
                }
            }
            wap = Ishavewap();
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