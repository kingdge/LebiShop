using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Model
{

    public class LBAPI
    {
        /// <summary>
        /// 基本格式
        /// </summary>
        public string msg
        {
            get;
            set;
        }
        public string data
        {
            get;
            set;
        }

    }
    /// <summary>
    /// 授权
    /// </summary>
    public class LBAPI_Licencse
    {
        public LBAPI_Licencse()
        {
            msg = "";
            data = "";
            md5 = "";
            domain = "";
            servicepackage = "";
        }
        public string msg
        {
            get;
            set;
        }
        public string data
        {
            get;
            set;
        }
        public string md5
        {
            get;
            set;
        }
        public string domain
        {
            get;
            set;
        }
        public string servicepackage
        {
            get;
            set;
        }
        public string services
        {
            get;
            set;
        }
        public string spreadcode
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 在线主题
    /// </summary>
    public class LBAPI_ThemeOnline
    {
        public string ImageSmallUrl
        {
            get;
            set;
        }
        public string ImageUrl
        {
            get;
            set;
        }
        public string LebiUser
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public string Code
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public int id
        {
            get;
            set;
        }
        public int Version
        {
            get;
            set;
        }
        public string Language
        {
            get;
            set;
        }
        public DateTime Time_Add
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 主题内容
    /// </summary>
    public class LBAPI_Theme
    {
        public string theme
        {
            get;
            set;
        }
        public string skin
        {
            get;
            set;
        }
        /// <summary>
        /// 广告位
        /// </summary>
        public string advert
        {
            get;
            set;
        }
        /// <summary>
        /// 广告图片
        /// </summary>
        public string advert_image
        {
            get;
            set;
        }
        public string filepath
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 主题版本
    /// </summary>
    public class LBAPI_ThemeVersion
    {
        public string code
        {
            get;
            set;
        }
        public int version
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 数据库结构
    /// </summary>
    public class LBAPI_DB_Struct
    {
        public string colname { set; get; }
        public string type { set; get; }
        public string tablename { set; get; }
        
    }

    
    
}
