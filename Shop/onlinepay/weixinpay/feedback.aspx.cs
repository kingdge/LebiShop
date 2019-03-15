using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
namespace weixinpay
{
    //=================================
    //维权接口
    //=================================
    public partial class feedback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //创建支付应答对象
            ResponseHandler resHandler = new ResponseHandler(Context);
            resHandler.init();
            resHandler.setKey(TenpayUtil.key, TenpayUtil.appkey);

            //判断签名
            if (resHandler.isWXsignfeedback())
            {
                //回复服务器处理成功
                Response.Write("OK");
                Response.Write("OK:" + resHandler.getDebugInfo());
            }
            else
            {
                //sha1签名失败
                Response.Write("fail");
                Response.Write("fail:" + resHandler.getDebugInfo());
            }
            Response.End();
        }
    }
}