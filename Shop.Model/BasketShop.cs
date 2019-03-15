using System;
using System.Collections.Generic;
using System.Text;
using DB.LebiShop;
namespace Shop.Model
{
    /// <summary>
    /// 购物车店铺属性
    /// </summary>
    public class BasketShop
    {
        /// <summary>
        /// 商户信息，空值时表示自营
        /// </summary>
        public Lebi_Supplier Shop
        {
            get;
            set;
        }
        /// <summary>
        /// 商品件数
        /// </summary>
        public int Count
        {
            get;
            set;
        }
        /// <summary>
        /// 购物车属性
        /// </summary>
        public int ProPerty134
        {
            get;
            set;
        }
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight
        {
            get;
            set;
        }
        /// <summary>
        /// 体积
        /// </summary>
        public decimal Volume
        {
            get;
            set;
        }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal Money_Transport
        {
            get;
            set;
        }
        /// <summary>
        /// 定额运费
        /// </summary>
        public int Money_Transport_One
        {
            get;
            set;
        }
        /// <summary>
        /// 是否定额运费
        /// </summary>
        public bool IsTransportPriceOne
        {
            get;
            set;
        }
        /// <summary>
        /// 商品金额
        /// </summary>
        public decimal Money_Product
        {
            get;
            set;
        }
        /// <summary>
        /// 未计算促销前的商品总金额
        /// </summary>
        public decimal Money_Product_begin
        {
            get;
            set;
        }
        /// <summary>
        /// 属性价格（如延长保修）总和金额
        /// </summary>
        public decimal Money_Property
        {
            get;
            set;
        }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal Money_Cut
        {
            get;
            set;
        }
        /// <summary>
        /// 返还金额
        /// </summary>
        public decimal Money_Give
        {
            get;
            set;
        }
        /// <summary>
        /// 市场价
        /// </summary>
        public decimal Money_Market
        {
            set;
            get;
        }
        /// <summary>
        /// 税金
        /// </summary>
        public decimal Money_Tax
        {
            set;
            get;
        }
        /// <summary>
        ///  = 0;//获得积分总数
        /// </summary>
        public decimal Point
        {
            set;
            get;
        }
        /// <summary>
        /// = 0;//商品获得积分总数
        /// </summary>
        public decimal Point_Product
        {
            set;
            get;
        }
        /// <summary>
        ///  = 0;//赠送积分
        /// </summary>
        public decimal Point_Free
        {
            set;
            get;
        }
        /// <summary>
        ///  积分购买时所需积分
        /// </summary>
        public decimal Point_Buy
        {
            set;
            get;
        }
        /// <summary>
        /// ; //赠品
        /// </summary>
        public List<Lebi_User_Product> FreeProducts
        {
            set;
            get;
        }
        /// <summary>
        /// ;//满足条件的促销活动
        /// </summary>
        public List<Lebi_Promotion_Type> PromotionTypes
        {
            set;
            get;
        }
        /// <summary>
        /// 商品
        /// </summary>
        public List<Lebi_User_Product> Products 
        {
            get;
            set;
        }

    }


}
