using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Platform.Model.Facebook
{

    public class userinfo
    {
        //{"id":"100004513833280","name":"\u5f20\u4e16","first_name":"\u4e16","last_name":"\u5f20","link":"https:\/\/www.facebook.com\/profile.php?id=100004513833280","gender":"male","timezone":8,"locale":"zh_CN","verified":true,"updated_time":"2012-10-08T06:33:02+0000"}
        public string id
        {
            get;
            set;
        }
        public string link
        {
            get;
            set;
        }
        public string gender
        {
            get;
            set;
        }
        public string username
        {
            get;
            set;
        }
        public string first_name
        {
            get;
            set;
        }
        public string last_name
        {
            get;
            set;
        }
        public picture_ picture
        {
            get;
            set;
        }
        public class picture_
        {
            public data_ data
            {
                get;
                set;
            }
            public class data_
            {
                public string url
                {
                    get;
                    set;
                }
            }
        }
    }


}
