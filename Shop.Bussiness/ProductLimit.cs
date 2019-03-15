using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using Shop.Model;using DB.LebiShop;

namespace Shop.Bussiness
{
    public static class ProductLimit
    {
        public static Dictionary<int, string> UserProductLimit = new Dictionary<int, string>();
        public static Dictionary<int, string> UserLevelProductLimit = new Dictionary<int, string>();
        
        public static string UserLimit(int id)
        {
            if (UserProductLimit.ContainsKey(id))
                return UserProductLimit[id];

            DataTable dt = Common.GetDataSet("select distinct(Product_id) from Lebi_Product_Limit  where IsShow=1 and User_id=" + id + "").Tables[0];
            List<string> ids = new List<string>();

            foreach (DataRow dr in dt.Rows)
            {
                ids.Add(dr["Product_id"].ToString());

            }
            string ret = string.Join(",", ids);
            if (ret == "")
                ret = "0";
            UserProductLimit.Add(id, ret);
            return ret;

        }
        public static void UserLimitRemove(int id)
        {
            if (UserProductLimit.ContainsKey(id))
                UserProductLimit.Remove(id);
        }

        public static string UserLevelLimit(int id)
        {
            if (UserLevelProductLimit.ContainsKey(id))
                return UserLevelProductLimit[id];
            DataTable dt = Common.GetDataSet("select distinct(Product_id) from Lebi_Product_Limit  where IsShow=1 and UserLevel_id=" + id + "").Tables[0];
            List<string> ids = new List<string>();
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                ids.Add(dr["Product_id"].ToString());
                i++;
                //if (i > 500)
                //    break;
            }
            string ret = string.Join(",", ids);
            if (ret == "")
                ret = "0";
            UserLevelProductLimit.Add(id, ret);
            return ret;
        }
        public static void UserLevelLimitRemove(int id)
        {
            if (UserLevelProductLimit.ContainsKey(id))
                UserLevelProductLimit.Remove(id);
        }
    }
}
