using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;
using LB.Tools;
using LB.DataAccess;
using DB.LebiShop;
using System.Web.Script.Serialization;
using System.Collections.Specialized;

namespace Shop.Bussiness
{
    public class ShopCache
    {
        /// <summary>
        /// 基本设置信息
        /// </summary>
        /// <returns></returns>
        public static BaseConfig GetBaseConfig()
        {
            string cacheKey = "BaseConfig";
            BaseConfig model = CacheTool.Get(cacheKey) as BaseConfig;
            if (model == null)
            {
                B_BaseConfig bmodel = new B_BaseConfig();
                model = bmodel.LoadConfig();
                model.IsMutiSite = false;
                SetBaseConfig(model);
            }
            return model;
        }
        public static BaseConfig_Supplier GetBaseConfig_Supplier(int Supplier_id)
        {
            string 
            cacheKey = "BaseConfig" + Supplier_id;
            BaseConfig_Supplier model = CacheTool.Get(cacheKey) as BaseConfig_Supplier;
            if (model == null)
            {
                //Lebi.Supplier.B_BaseConfig_Supplier bmodel = new Lebi.Supplier.B_BaseConfig_Supplier();
                //model = bmodel.LoadConfig(Supplier_id);
                //model.IsMutiSite = false;
                SetBaseConfig(model, Supplier_id);
            }
            return model;
        }
        public static BaseConfig_DT GetBaseConfig_DT(int DT_id)
        {
            string
            cacheKey = "BaseConfig_DT" + DT_id;
            BaseConfig_DT model = CacheTool.Get(cacheKey) as BaseConfig_DT;
            if (model == null)
            {
                B_BaseConfig_DT bmodel = new B_BaseConfig_DT();
                model = bmodel.LoadConfig(DT_id);
                model.IsMutiSite = false;
                SetBaseConfig(model, DT_id);
            }
            return model;
        }
        /// <summary>
        /// 更新基本设置
        /// </summary>
        /// <param name="model"></param>
        public static void SetBaseConfig(BaseConfig model)
        {
           
            CacheTool.Permanent("BaseConfig", model);
            //if (Shop.LebiAPI.Service.Instanse.Check("plugin_gongyingshang"))
            //{
            //    model.Plugin_gongyingshang = true;
            //    CacheTool.Permanent("BaseConfig", model);
            //}
        }
        public static void SetBaseConfig(BaseConfig_Supplier model, int Supplier_id = 0)
        {
            CacheTool.Permanent("BaseConfig" + Supplier_id, model);
            //if (Shop.LebiAPI.Service.Instanse.Check("plugin_gongyingshang"))
            //{
            //    model.Plugin_gongyingshang = true;
            //    CacheTool.Permanent("BaseConfig", model);
            //}
        }
        public static void SetBaseConfig(BaseConfig_DT model, int DT_id = 0)
        {
            CacheTool.Permanent("BaseConfig_DT" + DT_id, model);
            //if (Shop.LebiAPI.Service.Instanse.Check("plugin_gongyingshang"))
            //{
            //    model.Plugin_gongyingshang = true;
            //    CacheTool.Permanent("BaseConfig", model);
            //}
        }
        public static void SetBaseConfig(int Supplier_id = 0)
        {
            if (Supplier_id == 0)
            {
                B_BaseConfig bmodel = new B_BaseConfig();
                BaseConfig model = bmodel.LoadConfig();
                SetBaseConfig(model);
            }
            else
            {
                //Lebi.Supplier.B_BaseConfig_Supplier bmodel = new Lebi.Supplier.B_BaseConfig_Supplier();
                //BaseConfig_Supplier model = bmodel.LoadConfig(Supplier_id);
                //SetBaseConfig(model);
            }
        }
        /// <summary>
        /// 商品分类缓存
        /// </summary>
        /// <returns></returns>
        public static List<Lebi_Pro_Type> GetProductType()
        {
            //List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList("", "Sort desc");
            //return models;
            string cacheKey = "ProductType";
            List<Lebi_Pro_Type> models = CacheTool.Get(cacheKey) as List<Lebi_Pro_Type>;
            if (models == null)
            {
                SetProductType();
                return GetProductType();
            }
            return models;
        }
        /// <summary>
        /// 设置语言标签缓存
        /// </summary>
        public static void SetLanguageTag()
        {
            List<Lebi_Language_Tag> models = B_Lebi_Language_Tag.GetList("", "");
            CacheTool.Permanent("LanguageTag", models);
        }
        /// <summary>
        /// 语言标签缓存
        /// </summary>
        /// <returns></returns>
        public static List<Lebi_Language_Tag> GetLanguageTag()
        {
            string cacheKey = "LanguageTag";
            List<Lebi_Language_Tag> models = CacheTool.Get(cacheKey) as List<Lebi_Language_Tag>;
            if (models == null)
            {
                SetLanguageTag();
                return GetLanguageTag();
            }
            return models;
        }
        /// <summary>
        /// 设置页面设置缓存
        /// </summary>
        public static void SetThemePage()
        {
            List<Lebi_Theme_Page> models = B_Lebi_Theme_Page.GetList("", "");
            CacheTool.Permanent("ThemePage", models);
        }
        /// <summary>
        /// 页面设置缓存
        /// </summary>
        /// <returns></returns>
        public static List<Lebi_Theme_Page> GetThemePage()
        {
            string cacheKey = "ThemePage";
            List<Lebi_Theme_Page> models = CacheTool.Get(cacheKey) as List<Lebi_Theme_Page>;
            if (models == null)
            {
                SetThemePage();
                return GetThemePage();
            }
            return models;
        }
        /// <summary>
        /// 设置商品分类缓存
        /// </summary>
        public static void SetProductType()
        {
            List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList("(IsDel!=1 or IsDel is null)", "Sort desc");
            CacheTool.Permanent("ProductType", models);
        }


        /// <summary>
        /// 当前所有活动缓存
        /// </summary>
        /// <returns></returns>
        public static List<Lebi_Promotion_Type> GetCurrentPromotionType()
        {
            string cacheKey = "CurrentPromotion";
            List<Lebi_Promotion_Type> models = CacheTool.Get(cacheKey) as List<Lebi_Promotion_Type>;
            if (models == null)
            {
                SetCurrentPromotionType();
                return GetCurrentPromotionType();
            }
            return models;
        }
        /// <summary>
        /// 设置当前所有活动缓存
        /// </summary>
        public static void SetCurrentPromotionType()
        {
            List<Lebi_Promotion_Type> models = B_Lebi_Promotion_Type.GetList("Type_id_PromotionStatus=241", "");
            CacheTool.Permanent("CurrentPromotion", models);
        }

        /// <summary>
        /// 当前授权
        /// </summary>
        /// <returns></returns>
        public static List<UserService> GetLicense()
        {
            string cacheKey = "CurrentLicense";
            List<UserService> models = CacheTool.Get(cacheKey) as List<UserService>;
            if (models == null)
            {
                SetLicense();
                return GetLicense();
            }
            return models;
        }
        /// <summary>
        /// 设置当前授权
        /// </summary>
        /// <returns></returns>
        public static void SetLicense()
        {
            List<UserService> models = new List<UserService>();
            try
            {
                string ls = ShopCache.GetBaseConfig().LicenseString;
                string[] arr = ls.Split(';');
                foreach (string ar in arr)
                {
                    UserService model = new UserService();
                    string[] aa = ar.Split('|');
                    model.C = aa[0];
                    model.T = Convert.ToDateTime(aa[1]);
                    models.Add(model);
                }
            }
            catch
            {
                models = new List<UserService>();
            }
            SetLicense(models);
        }
        public static void SetLicense(List<UserService> models)
        {
            CacheTool.Permanent("CurrentLicense", models);
        }

        #region 站点信息
        /// <summary>
        /// 设置主站点
        /// </summary>
        public static void SetMainSite()
        {
            Lebi_Site model = B_Lebi_Site.GetModel("1=1 order by Sort desc");
            if (model == null)
                model = new Lebi_Site();
            CacheTool.Permanent("MainSite", model);
        }
        public static Lebi_Site GetMainSite()
        {
            string cacheKey = "MainSite";
            Lebi_Site model = CacheTool.Get(cacheKey) as Lebi_Site;
            if (model == null)
            {
                SetMainSite();
                return GetMainSite();
            }
            return model;
        }
        #endregion

        #region 域名授权
        /// <summary>
        /// 设置域名状态
        /// 1正常0禁用
        /// </summary>
        public static void SetDomainStatus()
        {
            CacheTool.Permanent("DomainStatus", Shop.LebiAPI.Service.Instanse.CheckDomain());
        }
        public static string GetDomainStatus()
        {
            //return Shop.LebiAPI.Service.Instanse.CheckDomain();
            string cacheKey = "DomainStatus";
            string model = CacheTool.Get(cacheKey) as string;
            if (model == null)
            {
                //SetDomainStatus();
                //return GetDomainStatus();
                return "1";
            }
            return model;
        }
        #endregion

    }

}

