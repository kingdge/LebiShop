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
    public partial class subproduct_edit_all : AdminPageBase
    {
        protected Lebi_Product model;
        protected int action;
        protected int randnum;
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
            randnum = RequestTool.RequestInt("randnum",0);
            id = RequestTool.RequestInt("id",0);
            if (id == 0 || (id > 0 && randnum > 0))
            {
                if (!EX_Admin.Power("product_add", "添加商品"))
                {
                    PageNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("product_edit", "编辑商品"))
                {
                    PageNoPower();
                }
            }
            action = RequestTool.RequestInt("action", 1);
            model = B_Lebi_Product.GetModel(id);
            if (model == null){
                model = new Lebi_Product();
                model.Type_id_ProductType = 320;
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
            Lebi_Supplier user = B_Lebi_Supplier.GetModel(id);
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
    }
}