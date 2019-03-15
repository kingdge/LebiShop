using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.Script.Serialization;
using System.IO;

namespace Shop.Bussiness
{
    public class BackUP
    {
        /// <summary>
        /// 添加数据备份
        /// </summary>
        public static bool Add()
        {

            try
            {

                string backpath = ShopCache.GetBaseConfig().DataBase_BackPath;
                string backname = ShopCache.GetBaseConfig().DataBase_BackName;
                if (backpath.IndexOf("/") == 0)
                {
                    backpath = System.Web.HttpRuntime.AppDomainAppPath + "/" + backpath;
                }
                if (!Directory.Exists(backpath))
                {
                    Directory.CreateDirectory(backpath);
                }
                if (!backname.Contains("."))
                {
                    backname = "." + backname;
                }
                backname = System.DateTime.Now.ToString("yyyyMMddhhmmss") + backname;
                backpath = backpath + "/" + backname;
                Regex r = new Regex(@"//*/", RegexOptions.Singleline);//目的：防止类似“///”“////”“//////”的情况
                backpath = r.Replace(backpath, "/");
                string sql = "exec p_backupdb @bkfname='" + backpath + "'";
                Common.ExecuteSql(sql);
                Log.Add("备份数据库");//添加操作记录
                
                return true;
            }
            catch (Exception ex)
            {
                Log.Add("备份数据库异常：", ex.Message);//添加操作记录
                return false;
            }
        }

    }

}

