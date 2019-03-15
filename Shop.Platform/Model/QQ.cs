using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Platform.Model.QQ
{
    public class OpenID
    {
        public string client_id
        {
            get;
            set;
        }
        public string openid
        {
            get;
            set;
        }
    }

    public class userinfo
    {
        //ret 	返回码
        public string ret
        {
            get;
            set;
        }
        //msg 	如果ret<0，会有相应的错误信息提示，返回数据全部用UTF-8编码。
        public string msg
        {
            get;
            set;
        }
        //nickname 	用户在QQ空间的昵称。
        public string nickname
        {
            get;
            set;
        }
        //figureurl 	大小为30×30像素的QQ空间头像URL。
        public string figureurl
        {
            get;
            set;
        }
        //figureurl_1 	大小为50×50像素的QQ空间头像URL。
        public string figureurl_1
        {
            get;
            set;
        }
        //figureurl_2 	大小为100×100像素的QQ空间头像URL。
        public string figureurl_2
        {
            get;
            set;
        }
        //figureurl_qq_1 	大小为40×40像素的QQ头像URL。
        public string figureurl_qq_1
        {
            get;
            set;
        }
        //figureurl_qq_2 	大小为100×100像素的QQ头像URL。需要注意，不是所有的用户都拥有QQ的100x100的头像，但40x40像素则是一定会有。
        public string figureurl_qq_2
        {
            get;
            set;
        }
        //gender 	性别。 如果获取不到则默认返回"男"
        public string gender
        {
            get;
            set;
        }
        //is_yellow_vip 	标识用户是否为黄钻用户（0：不是；1：是）。
        public string is_yellow_vip
        {
            get;
            set;
        }
        //vip 	标识用户是否为黄钻用户（0：不是；1：是）
        public string vip
        {
            get;
            set;
        }
        //yellow_vip_level 	黄钻等级
        public string yellow_vip_level
        {
            get;
            set;
        }
        //level 	黄钻等级
        public string level
        {
            get;
            set;
        }
        //is_yellow_year_vip 	标识是否为年费黄钻用户（0：不是； 1：是）
        public string is_yellow_year_vip
        {
            get;
            set;
        }
    }
}
