using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Model
{

    public class ProductStepPrice
    {

        public ProductStepPrice() { }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 购买价格
        /// </summary>
        public decimal Price { get; set; }

    }
    public class ProductUserLevelPrice
    {

        public ProductUserLevelPrice() { }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int UserLevel_id { get; set; }
        /// <summary>
        /// 购买价格
        /// </summary>
        public decimal Price { get; set; }

    }
    public class ProductUserLevelCount
    {

        public ProductUserLevelCount() { }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int UserLevel_id { get; set; }
        /// <summary>
        /// 起订量
        /// </summary>
        public int Count { get; set; }

    }
}
