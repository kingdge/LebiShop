using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Platform.Model.Weibo
{
    public class token
    {
        //{ "access_token":"SlAV32hkKG", "remind_in ":3600, "expires_in":3600 }
        public string access_token
        {
            get;
            set;
        }
        public string remind_in
        {
            get;
            set;
        }
        public string expires_in
        {
            get;
            set;
        }
    }
    public class tokeninfo
    {
       //"uid": 1073880650,
       //"appkey": 1352222456,
       //"scope": null,
       //"create_at": 1352267591,
       //"expire_in": 157679471
        public string uid
        {
            get;
            set;
        }
        public string appkey
        {
            get;
            set;
        }
        public string scope
        {
            get;
            set;
        }
        public string create_at
        {
            get;
            set;
        }
        public string expire_in
        {
            get;
            set;
        }
        
    }
    public class userinfo
    {
        public string screen_name
        {
            get;
            set;
        }
        public string profile_image_url
        {
            get;
            set;
        }
        public string gender
        {
            get;
            set;
        }
    }
  
}
