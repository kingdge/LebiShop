using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Model
{
    [Serializable]
    public class KuaiDi100
    {
        /**
         * {
         * "message":"ok","status":"1","state":"3",
         * "data":[
         * {"time":"2012-07-07 13:35:14","context":"客户已签收"},
         * {"time":"2012-07-07 09:10:10","context":"离开 [北京_房山营业所_石景山营业厅] 派送中，递送员[温]，电话[]"},
         * {"time":"2012-07-06 19:46:38","context":"到达 [北京_房山营业所_石景山营业厅]"},
         * {"time":"2012-07-06 15:22:32","context":"离开 [北京_房山营业所_石景山营业厅] 派送中，递送员[温]，电话[]"},
         * {"time":"2012-07-06 15:05:00","context":"到达 [北京_房山营业所_石景山营业厅]"},
         * {"time":"2012-07-06 13:37:52","context":"离开 [北京_同城中转站] 发往 [北京_房山营业所_石景山营业厅]"},
         * {"time":"2012-07-06 12:54:41","context":"到达 [北京_同城中转站]"},
         * {"time":"2012-07-06 11:11:03","context":"离开 [北京运转中心_航空_驻站班组] 发往 [北京_同城中转站]"},
         * {"time":"2012-07-06 10:43:21","context":"到达 [北京运转中心_航空_驻站班组]"},
         * {"time":"2012-07-05 21:18:53","context":"离开 [福建_厦门支公司] 发往 [北京运转中心_航空]"},
         * {"time":"2012-07-05 20:07:27","context":"已取件，到达 [福建_厦门支公司]"}
         * ]
         * }
         */
       
        public string message
        {
            get;
            set;
        }
        //查询的结果状态。0：运单暂无结果，1：查询成功，2：接口出现异常，408：验证码出错（仅适用于APICode url，可忽略) 。
        //遇到其他情况，请按获得身份授权key的邮件中的方法获得技术支持
        public string status
        {
            get;
            set;
        }
        //快递单当前的状态 。0：在途中,1：已发货，2：疑难件，3： 已签收 ，4：已退货。该状态还在不断完善中
        public string state
        {
            get;
            set;
        }
        //数据体
        public List<KuaiDi100data> data
        {
            get;
            set;
        }
        [Serializable]
        public class KuaiDi100data
        {
            public string time
            {
                get;
                set;
            }
            public string context
            {
                get;
                set;
            }
        }
    }
    
}
