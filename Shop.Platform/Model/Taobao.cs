using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Platform.Model.Taobao
{

    public class token
    {
        //Access token
        public string access_token
        {
            get;
            set;
        }
        //Access token的类型目前只支持bearer
        public string token_type
        {
            get;
            set;
        }
        //Access token过期时间
        public string expires_in
        {
            get;
            set;
        }
        //Refresh token
        public string refresh_token
        {
            get;
            set;
        }
        //Refresh token过期时间
        public string re_expires_in
        {
            get;
            set;
        }
        //淘宝账号
        public string taobao_user_nick
        {
            get;
            set;
        }
        //淘宝帐号对应id
        public string taobao_user_id
        {
            get;
            set;
        }
        //淘宝子账号对应id
        public string sub_taobao_user_id
        {
            get;
            set;
        }
        //淘宝子账号
        public string sub_taobao_user_nick
        {
            get;
            set;
        }
    }

    public class userinfo
    {
        public user_buyer_get_response_ user_buyer_get_response
        {
            get;
            set;
        }

        [Serializable]
        public class user_buyer_get_response_
        {
            public user_ user
            {
                get;
                set;
            }
            [Serializable]
            public class user_
            {
                public string uid
                {
                    get;
                    set;
                }
                public string nick
                {
                    get;
                    set;
                }
                public string sex
                {
                    get;
                    set;
                }

                /// <summary>
                /// 位置
                /// </summary>
                public location_ location
                {
                    get;
                    set;
                }
                [Serializable]
                public class location_
                {
                    public string zip
                    {
                        get;
                        set;
                    }
                    public string address
                    {
                        get;
                        set;
                    }
                    public string city
                    {
                        get;
                        set;
                    }
                    public string state
                    {
                        get;
                        set;
                    }
                    public string country
                    {
                        get;
                        set;
                    }
                    public string district
                    {
                        get;
                        set;
                    }
                }
                public string birthday
                {
                    get;
                    set;
                }
                public string avatar
                {
                    get;
                    set;
                }
            }
        }
    }

}
