using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Platform.Model.Twitter
{

    public class userinfo
    {
       
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
