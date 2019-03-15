using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.Script.Serialization;

namespace Shop.Bussiness
{
    public class B_ExpressPrint
    {

        /// <summary>
        /// 返回数据实体
        /// </summary>
        /// <param name="con"></param>
        /// <returns></returns>
        public static ExpressPrint GetModel(string con)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ExpressPrint model;
            try
            {
                model = jss.Deserialize<ExpressPrint>(con);
                if (model == null)
                    model = new ExpressPrint();
            }
            catch (Exception)
            {
                model = new ExpressPrint();
            }
            return model;
        }


        /// <summary>
        /// 序列化面板内容
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static string ToJson(ServicePanel sp)
        {
            string json = "";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            json = jss.Serialize(sp);
            return json;
        }



    }

}

