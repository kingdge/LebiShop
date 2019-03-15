using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.Script.Serialization;

namespace Shop.Bussiness
{
    public class Point
    {
        public static void AddPoint(Lebi_User user, decimal point, int type, Lebi_Administrator admin, string remark)
        {
            Random ran = new Random();
            string PayNo = Order.CreateOrderCode() + System.DateTime.Now.Millisecond + ran.Next(100000, 999999);
            AddPoint(user, point, type, null, admin, remark, PayNo);
        }
        public static void AddPoint(Lebi_User user, decimal point, int type, Lebi_Order order, Lebi_Administrator admin, string remark,string PayNo)
        {
            bool addflag = false;
            Lebi_User_Point model = new Lebi_User_Point();
            if (string.IsNullOrEmpty(PayNo))
            {
                Random ran = new Random();
                PayNo = Order.CreateOrderCode() + System.DateTime.Now.Millisecond + ran.Next(100000, 999999);
            }
            model = B_Lebi_User_Point.GetModel("Order_PayNo = lbsql{'" + PayNo + "'}");
            if (model == null)
            {
                model = new Lebi_User_Point();
                addflag = true;
            }
            if (admin != null)
            {
                model.Admin_id = admin.id;
                model.Admin_UserName = admin.UserName;
            }
            if (order != null)
            {
                model.Order_id = order.id;
                model.Order_Code = order.Code;
            }
            model.Order_PayNo = PayNo;
            model.Type_id_PointStatus = 171;
            model.Point = point;
            model.Remark = remark;
            model.Time_Update = DateTime.Now;
            model.User_id = user.id;
            model.User_RealName = user.RealName;
            model.User_UserName = user.UserName;
            if (addflag)
            {
                B_Lebi_User_Point.Add(model);
            }else
            {
                B_Lebi_User_Point.Update(model);
            }
            UpdateUserPoint(user);

        }
        /// <summary>
        /// 更新会员积分
        /// </summary>
        /// <param name="user"></param>
        public static void UpdateUserPoint(Lebi_User user)
        {
            string point = B_Lebi_User_Point.GetValue("sum(Point)", "User_id=" + user.id + " and Type_id_PointStatus=171");
            decimal Point = 0;
            Decimal.TryParse(point, out Point);
            Lebi_UserLevel userlev = B_Lebi_UserLevel.GetModel(user.UserLevel_id);
            if (userlev == null)
                userlev = new Lebi_UserLevel();
            List<Lebi_UserLevel> ls = B_Lebi_UserLevel.GetList("Grade > " + userlev.Grade +"", "Grade desc");
            //Lebi_UserLevel newlev=new Lebi_UserLevel();
            if (ls.Count > 0)
            {
                foreach (Lebi_UserLevel l in ls)
                {
                    if (Point >= l.Lpoint)
                    {
                        user.UserLevel_id = l.id;
                        //newlev = l;
                        break;
                    }
                }
            }
            //if (userlev.Grade < newlev.Grade)
            //{
                user.Point = Point;
                B_Lebi_User.Update(user);
            //}
        }
    }
}

