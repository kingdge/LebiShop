using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.user
{
    public partial class usercard_edit_window : AdminAjaxBase
    {

        protected List<Lebi_User> users;
        protected string User_Name;
        protected string user_ids;
        protected int user_id;
        protected SearchUser su;
        protected string mode = "scope";
        protected List<Lebi_CardOrder> cos;
        protected int cardtype = 311;//311购物卡312代金券
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("user_card_add", "发放卡券"))
            {
                WindowNoPower();
            }
            int user_id = RequestTool.RequestInt("user_id", 0);
            cardtype = RequestTool.RequestInt("cardtype", 311);
            Lebi_User user = B_Lebi_User.GetModel(user_id);
            if (user == null)
            {
                User_Name = RequestTool.RequestString("User_Name");
                user = B_Lebi_User.GetModel("UserName=lbsql{'" + User_Name + "'}");
            }
            if (user != null)
            {
                mode = "user";
                User_Name = user.UserName;
            }
            user_ids = RequestTool.RequestString("ids");
            if (user_ids != "")
            {
                users = B_Lebi_User.GetList("id in (lbsql{" + user_ids + "})", "id desc");
                mode = "users";
            }
            su = new SearchUser(CurrentAdmin, CurrentLanguage.Code);
            cos = B_Lebi_CardOrder.GetList("Type_id_CardType=" + cardtype + " and Time_End > '"+ DateTime.Now.Date +"'", "");
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