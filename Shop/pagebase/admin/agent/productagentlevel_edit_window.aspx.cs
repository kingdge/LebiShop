using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.agent
{
    public partial class productagentlevel_edit_window : AdminAjaxBase
    {
        protected Lebi_Agent_Product_Level model;
        protected List<Lebi_CardOrder> cos;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            //if (id == 0)
            //{
            //    if (!EX_Admin.Power("supplier_group_edit", "添加商家分组"))
            //    {
            //        WindowNoPower();
            //    }
            //}
            //else
            //{
            //    if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
            //    {
            //        WindowNoPower();
            //    }
            //}
            model = B_Lebi_Agent_Product_Level.GetModel(id);
            if (model == null)
                model = new Lebi_Agent_Product_Level();
            cos = B_Lebi_CardOrder.GetList("Type_id_CardType=312", "");
        }
        /// <summary>
        /// 卡券总数
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public int count_card(int oid)
        {
            int count = B_Lebi_Card.Counts("CardOrder_id=" + oid + "");
            return count;
        }
        public int count_card_no(int oid)
        {
            int count = B_Lebi_Card.Counts("CardOrder_id=" + oid + " and Type_id_CardStatus=200");
            return count;
        }
    }
}