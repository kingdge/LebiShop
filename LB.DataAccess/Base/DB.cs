using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using LB.Tools;
using BeIT.MemCached;

namespace LB.DataAccess
{
    public class DB : BaseUtils
    {

        public DB()
        {

        }


        #region ¾²Ì¬ÊµÀý-Ä¬ÈÏ
        private static IDB _Instance;
        public static IDB Instance
        {
            get
            {
                if (_Instance == null)
                {
                    if (BaseUtils.BaseUtilsInstance.DBType == "mysql")
                        _Instance = new MySQLUtils();
                    else
                    {
                        _Instance = new SqlUtils();
                    }
                }
                return _Instance;
            }
            set
            {
                _Instance = value;
            }
        }
        #endregion






        #region ¾²Ì¬ÊµÀý-mysql   xxx
        private static IDB _xxx;
        public static IDB xxx
        {
            get
            {
                if (_xxx == null)
                {
                    string conn = GetConnString("_xxx");
                    _xxx = new MySQLUtils(conn);
                }
                return _xxx;
            }
            set
            {
                _xxx = value;
            }
        }
        #endregion

        
    }
}
