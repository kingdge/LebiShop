using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Model
{
    [Serializable]
    public class BaseConfig_Supplier
    {

        public BaseConfig_Supplier() { }
        #region 站点设置
        /// <summary>
        /// 网站名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 网站TITLE
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 网站关键字
        /// </summary>
        public string Keywords { get; set; }
        /// <summary>
        /// 网站描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 服务协议
        /// </summary>
        public string ServiceP { get; set; }
        /// <summary>
        /// 循环描述
        /// </summary>
        public string Loop { get; set; }
        /// <summary>
        /// 网站logo
        /// </summary>
        public string Logoimg { get; set; }
        /// <summary>
        /// 邮件发送服务器
        /// </summary>
        public string SmtpAddress { get; set; }
        /// <summary>
        /// 邮件帐号
        /// </summary>
        public string MailName { get; set; }
        /// <summary>
        /// 发送邮件邮箱
        /// </summary>
        public string AdminMailAddress { get; set; }
        public string MailAddress { get; set; }
        public string MailDisplayName { get; set; }
        public string MailPort { get; set; }
        public string MailIsSSL { get; set; }
        /// <summary>
        /// 邮件密码
        /// </summary>
        public string MailPassWord { get; set; }
        /// <summary>
        /// 邮件标记-记录那些动作需要自动发送邮件
        /// </summary>
        public string MailSign { get; set; }
        /// <summary>
        /// 邮件标记-记录那些动作需要自动发送邮件
        /// </summary>
        public string AdminMailSign { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 底部版权信息
        /// </summary>
        public string Copyright { get; set; }
        /// <summary>
        /// 底部HTML
        /// </summary>
        public string FootHtml { get; set; }
        /// <summary>
        /// 网站域名
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// 开启评论 1是0否
        /// </summary>
        public string CommFlag { get; set; }
        /// <summary>
        /// 禁止访问标志 0关闭1开启
        /// </summary>
        public string VisitTimeFlag { get; set; }
        /// <summary>
        /// 禁止访问时间段
        /// </summary>
        public string VisitTime { get; set; }
        /// <summary>
        /// 登陆验证标记 ？？？？
        /// </summary>
        public string OpenpwdFlag { get; set; }
        /// <summary>
        /// 登陆验证密码???
        /// </summary>
        public string Openpwd { get; set; }
        /// <summary>
        /// 点击标志 0规律增加1随机增加
        /// </summary>
        public string ClickFlag { get; set; }
        /// <summary>
        /// 点击数量规律增加数值
        /// </summary>
        public string ClickNum1 { get; set; }
        /// <summary>
        /// 点击数量随机增加数值
        /// </summary>
        public string ClickNum2 { get; set; }
        /// <summary>
        /// 销售数量标志 0规律增加1随机增加
        /// </summary>
        public string SalesFlag { get; set; }
        /// <summary>
        /// 销售数量规律增加数值
        /// </summary>
        public string SalesNum1 { get; set; }
        /// <summary>
        /// 销售数量随机增加数值
        /// </summary>
        public string SalesNum2 { get; set; }
        /// <summary>
        /// 过滤关键词
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// 贴士
        /// </summary>
        public string Tieshi { get; set; }
        /// <summary>
        /// 发票标记 0关闭1开启
        /// </summary>
        public string BillFlag { get; set; }
        /// <summary>
        /// 伪静态标记 0关闭1开启
        /// </summary>
        public string UrlFlag { get; set; }
        /// <summary>
        /// 静态标记 0关闭1开启
        /// </summary>
        public string HtmlFlag { get; set; }
        /// <summary>
        /// 维护标记 0关闭1开启
        /// </summary>
        public string WeiHuFlag { get; set; }
        /// <summary>
        /// 维护说明文字
        /// </summary>
        public string Wornings { get; set; }
        /// <summary>
        /// 客服面板配置
        /// </summary>
        public string ServicePanel { get; set; }
        /// <summary>
        /// 快递100KEY
        /// </summary>
        public string KuaiDi100 { get; set; }
        /// <summary>
        /// 快递100状态
        /// </summary>
        public string KuaiDi100Status { get; set; }
        /// <summary>
        /// 商品评分奖励积分
        /// </summary>
        public string CommentPoint { get; set; }
        /// <summary>
        /// 失败邮件重复次数
        /// </summary>
        public string Mail_SendTop { get; set; }
        /// <summary>
        /// 邮件重复任务提取时间间隔
        /// 单位为分钟
        /// </summary>
        public string Mail_SendTime { get; set; }
        /// <summary>
        /// 数据库备份时间间隔
        /// 单位为分钟
        /// </summary>
        public string DataBase_BackUpTime { get; set; }
        /// <summary>
        /// 数据库备份路径
        /// </summary>
        public string DataBase_BackPath { get; set; }
        /// <summary>
        /// 数据库备份文件扩展名
        /// </summary>
        public string DataBase_BackName { get; set; }

        public int ImageBigWidth { get; set; }
        public int ImageBigHeight { get; set; }
        public int ImageMediumWidth { get; set; }
        public int ImageMediumHeight { get; set; }
        public int ImageSmallWidth { get; set; }
        public int ImageSmallHeight { get; set; }
        /// <summary>
        /// 上传限制
        /// </summary>
        public string UpLoadLimit { get; set; }
        /// <summary>
        /// 上传路径
        /// </summary>
        public string UpLoadPath { get; set; }
        /// <summary>
        /// 上传文件命名类型
        /// </summary>
        public string UpLoadSaveName { get; set; }
        /// <summary>
        /// 上传文件前缀
        /// </summary>
        public string UpLoadRName { get; set; }
        /// <summary>
        /// 上传文件模式
        /// </summary>
        public string UpLoadMode { get; set; }
        /// <summary>
        /// 禁止IP
        /// </summary>
        public string IPLock { get; set; }
        /// <summary>
        /// 提现最低额度限制
        /// </summary>
        public string TakeMoneyLimit { get; set; }
        /// <summary>
        /// 提现手续费
        /// </summary>
        public string WithdrawalFeeRate { get; set; }
        /// <summary>
        /// 自动收货天数
        /// </summary>
        public string OrderReceivedDays { get; set; }
        /// <summary>
        /// 订单自动完结天数
        /// </summary>
        public string OrderCompleteDays { get; set; }
        /// <summary>
        /// api密码
        /// </summary>
        public string APIPassWord { get; set; }
        /// <summary>
        /// lebiAPI地址
        /// </summary>
        public string LebiAPI { get; set; }
        /// <summary>
        /// 购物车提示页 0关闭1开启
        /// </summary>
        public string IsBasketAction { get; set; }
        /// 商品编号前缀
        /// </summary>
        public string ProductNumberPrefix { get; set; }
        /// 商品编号长度
        /// </summary>
        public string ProductNumberLength { get; set; }
        /// 新事件间隔时间
        /// </summary>
        public string NewEventTimes { get; set; }
        /// 新事件播放音频
        /// </summary>
        public string NewEventPlayAudio { get; set; }
        /// HTTP类型
        /// </summary>
        public string HTTPServer { get; set; }
        /// 消费税率
        /// </summary>
        public string TaxRate { get; set; }
        #endregion
        #region 邮件模板
        /// <summary>
        /// 邮件模板
        /// 常购清单-标题
        /// </summary>
        public string EmailTPL_changgouqingdan_title { get; set; }
        /// <summary>
        /// 邮件模板
        /// 常购清单
        /// </summary>
        public string EmailTPL_changgouqingdan { get; set; }
        /// <summary>
        /// 邮件模板
        /// 新会员注册
        /// </summary>
        public string EmailTPL_newuser { get; set; }
        /// <summary>
        /// 邮件模板
        /// 找回密码
        /// </summary>
        public string EmailTPL_getpwd { get; set; }
        /// <summary>
        /// 邮件模板
        /// 订单提交
        /// </summary>
        public string EmailTPL_ordersubmit { get; set; }
        /// <summary>
        /// 邮件模板
        /// 订单发货
        /// </summary>
        public string EmailTPL_ordershipping { get; set; }
        /// <summary>
        /// 邮件模板-标题
        /// 新会员注册
        /// </summary>
        public string EmailTPL_newuser_title { get; set; }
        /// <summary>
        /// 邮件模板-标题
        /// 找回密码
        /// </summary>
        public string EmailTPL_getpwd_title { get; set; }
        /// <summary>
        /// 邮件模板-标题
        /// 订单提交
        /// </summary>
        public string EmailTPL_ordersubmit_title { get; set; }
        /// <summary>
        /// 邮件模板-标题
        /// 订单发货
        /// </summary>
        public string EmailTPL_ordershipping_title { get; set; }
        /// <summary>
        /// 邮件模板-管理员
        /// 新会员注册
        /// </summary>
        public string EmailTPL_Admin_newuser { get; set; }
        /// <summary>
        /// 邮件模板-管理员
        /// 订单提交
        /// </summary>
        public string EmailTPL_Admin_ordersubmit { get; set; }
        /// <summary>
        /// 邮件模板-管理员
        /// 订单留言
        /// </summary>
        public string EmailTPL_Admin_ordercomment { get; set; }
        /// <summary>
        /// 邮件模板-管理员
        /// 留言反馈
        /// </summary>
        public string EmailTPL_Admin_inquiry { get; set; }
        /// <summary>
        /// 邮件模板-管理员
        /// 商品评论
        /// </summary>
        public string EmailTPL_Admin_comment { get; set; }
        /// <summary>
        /// 邮件模板-管理员
        /// 商品咨询
        /// </summary>
        public string EmailTPL_Admin_ask { get; set; }
        /// <summary>
        /// 邮件模板-管理员
        /// 站内信
        /// </summary>
        public string EmailTPL_Admin_message { get; set; }
        /// <summary>
        /// 邮件模板-管理员-标题
        /// 新会员注册
        /// </summary>
        public string EmailTPL_Admin_newuser_title { get; set; }
        /// <summary>
        /// 邮件模板-管理员-标题
        /// 订单提交
        /// </summary>
        public string EmailTPL_Admin_ordersubmit_title { get; set; }
        /// <summary>
        /// 邮件模板-管理员-标题
        /// 订单留言
        /// </summary>
        public string EmailTPL_Admin_ordercomment_title { get; set; }
        /// <summary>
        /// 邮件模板-管理员-标题
        /// 留言反馈
        /// </summary>
        public string EmailTPL_Admin_inquiry_title { get; set; }
        /// <summary>
        /// 邮件模板-管理员-标题
        /// 商品评论
        /// </summary>
        public string EmailTPL_Admin_comment_title { get; set; }
        /// <summary>
        /// 邮件模板-管理员-标题
        /// 商品咨询
        /// </summary>
        public string EmailTPL_Admin_ask_title { get; set; }
        /// <summary>
        /// 邮件模板-管理员-标题
        /// 站内信
        /// </summary>
        public string EmailTPL_Admin_message_title { get; set; }
        /// <summary>
        /// 邮件模板-邮件验证码
        /// </summary>
        public string EmailTPL_checkcode { get; set; }
        /// <summary>
        /// 邮件模板-邮件验证码-标题
        /// </summary>
        public string EmailTPL_checkcode_title { get; set; }
        /// <summary>
        /// 邮件模板-邮件分享朋友
        /// </summary>
        public string EmailTPL_sendfriend { get; set; }
        /// <summary>
        /// 邮件模板-邮件分享朋友-标题
        /// </summary>
        public string EmailTPL_sendfriend_title { get; set; }

        /// <summary>
        /// 邮件模板-预定商品到货
        /// </summary>
        public string EmailTPL_reserveok { get; set; }
        /// <summary>
        /// 邮件模板-预定商品到货-标题
        /// </summary>
        public string EmailTPL_reserveok_title { get; set; }

        #endregion

        #region 鉴权
        public string LicenseUserName { get; set; }
        public string LicensePWD { get; set; }
        public string LicenseString { get; set; }
        public string LicensePackage { get; set; }
        public string LicenseMD5 { get; set; }
        public string InstallCode { get; set; }
        public string SpreadCode { get; set; }
        /// <summary>
        /// 捆绑域名
        /// </summary>
        public string LicenseDomain { get; set; }
        public string Version { get; set; }
        public string Version_Son { get; set; }
        #endregion;

        #region 第三方平台
        public string platform_login { get; set; }
        public string platform_qq_id { get; set; }
        public string platform_qq_key { get; set; }
        public string platform_qq_image { get; set; }
        public string platform_weibo_id { get; set; }
        public string platform_weibo_key { get; set; }
        public string platform_weibo_image { get; set; }
        public string platform_taobao_key { get; set; }
        public string platform_taobao_secret { get; set; }
        public string platform_taobao_image { get; set; }
        public string platform_facebook_id { get; set; }
        public string platform_facebook_secret { get; set; }
        public string platform_facebook_image { get; set; }
        public string platform_twitter_key { get; set; }
        public string platform_twitter_secret { get; set; }
        public string platform_twitter_image { get; set; }
        public string platform_weixin_number { get; set; }
        public string platform_weixin_id { get; set; }
        public string platform_weixin_secret { get; set; }
        public string platform_weixin_image { get; set; }
        public string platform_weixin_image_qrcode { get; set; }
        public string platform_weixin_custemtoken { get; set; }
        public string platform_weixin_subscribe_automsg { get; set; }
        /// <summary>
        /// 淘宝应该授权信息
        /// </summary>
        public string platform_taobao_sessionkey { get; set; }
        /// <summary>
        /// 淘宝店铺基本信息
        /// </summary>
        public string platform_taobao_shopnick { get; set; }
        #endregion

        #region 开关
        /// <summary>
        /// 不注册购买功能
        /// </summary>
        public string IsAnonymousUser { get; set; }
        /// <summary>
        /// 是否开启积分兑换资金
        /// </summary>
        public string IsPointToMoney { get; set; }
        /// <summary>
        /// 顶级收货地址
        /// </summary>
        public string TopAreaid { get; set; }
        /// <summary>
        /// 付款订单自动确认
        /// </summary>
        public string IsOpenPaidOrderConfirm { get; set; }
        /// <summary>
        /// 0库存自动下架
        /// </summary>
        public string IsNullStockDown { get; set; }
        /// <summary>
        /// 允许负库存销售
        /// </summary>
        public string IsNullStockSale { get; set; }
        /// <summary>
        /// 冻结库存时机
        /// </summary>
        public string ProductStockFreezeTime { get; set; }
        /// <summary>
        /// 后台语言
        /// </summary>
        public string AdminLanguages { get; set; }
        public string PluginUsed { get; set; }
        public string IsClosetuihuo { get; set; }
        /// <summary>
        /// 供应商自主收款
        /// </summary>
        public string IsSupplierCash { get; set; }
        /// <summary>
        /// 会员有效期启用开关
        /// </summary>
        public string IsOpenUserEnd { get; set; }
        /// <summary>
        /// 默认账号有效期
        /// </summary>
        public string DefaultUserEndDays { get; set; }
        /// <summary>
        /// 会员注册验证类型
        /// </summary>
        public string UserRegCheckedType { get; set; }
        /// <summary>
        /// 前台是否多币种展现金额
        /// </summary>
        public string IsMutiCurrencyShow { get; set; }

        /// <summary>
        /// 允许站外提交AJAX请求
        /// </summary>
        public string IsAllowOutSideAjax { get; set; }
        /// <summary>
        /// 信任IP
        /// </summary>
        public string SafeIPs { get; set; }
        /// <summary>
        /// 站内商品权限类型0勾选的权限表示拒绝1勾选的权限表示允许
        /// </summary>
        public string ProductLimitType { get; set; }
        #endregion

        #region 标记
        public bool IsMutiSite { get; set; }
        #endregion
        #region 推广/佣金相关设置
        /// <summary>
        /// 佣金冻结时间/天
        /// </summary>
        public string CommissionMoneyDays { get; set; }
        /// <summary>
        /// 代理过期后，可续费天数
        /// </summary>
        public string AgentEndDays { get; set; }
        /// <summary>
        /// 订单商品金额作为佣金的比例
        /// </summary>
        public string OrderProductCommission { get; set; }
        /// <summary>
        /// 一级会员佣金比例
        /// </summary>
        public string Angent1_Commission { get; set; }
        /// <summary>
        /// 二级会员佣金比例
        /// </summary>
        public string Angent2_Commission { get; set; }
        /// <summary>
        /// 会员佣金比例
        /// </summary>
        public string Angent_Commission { get; set; }
        /// <summary>
        /// 会员佣金比例-门槛
        /// </summary>
        public string Angent_Commission_require { get; set; }
        /// <summary>
        /// 推广代理开关
        /// </summary>
        public string IsUsedAgent { get; set; }
        /// <summary>
        /// 地区代理开关
        /// </summary>
        public string IsUsedAgent_Area { get; set; }
        /// <summary>
        /// 商品代理开关
        /// </summary>
        public string IsUsedAgent_Product { get; set; }
        #endregion
        #region 手机短信
        /// <summary>
        /// 模板-新用户注册
        /// </summary>
        public string SMSTPL_newuser { get; set; }
        /// <summary>
        /// 模板-订单提交
        /// </summary>
        public string SMSTPL_ordersubmit { get; set; }
        /// <summary>
        /// 模板-订单提交-自提确认
        /// </summary>
        public string SMSTPL_orderpickup { get; set; }
        /// <summary>
        /// 模板-订单发货
        /// </summary>
        public string SMSTPL_ordershipping { get; set; }
        /// <summary>
        /// 模板-余额提醒
        /// </summary>
        public string SMSTPL_account { get; set; }
        /// <summary>
        /// 模板-余额提醒
        /// </summary>
        public string SMSTPL_balance { get; set; }
        /// <summary>
        /// 模板-找回密码
        /// </summary>
        public string SMSTPL_getpwd { get; set; }
        /// <summary>
        /// 模板-获取新密码
        /// </summary>
        public string SMSTPL_getnewpwd { get; set; }
        /// <summary>
        /// 模板-商品评论
        /// </summary>
        public string SMSTPL_comment { get; set; }
        /// <summary>
        /// 模板-商品咨询
        /// </summary>
        public string SMSTPL_ask { get; set; }
        /// <summary>
        /// 模板-站内信
        /// </summary>
        public string SMSTPL_message { get; set; }
        /// <summary>
        /// 模板-预定商品到货
        /// </summary>
        public string SMSTPL_reserveok { get; set; }
        /// <summary>
        /// 模板-手机验证码
        /// </summary>
        public string SMSTPL_checkcode { get; set; }
        /// <summary>
        /// 模板-管理员-新用户注册
        /// </summary>
        public string SMSTPL_Admin_newuser { get; set; }
        /// <summary>
        /// 模板-管理员-订单提交
        /// </summary>
        public string SMSTPL_Admin_ordersubmit { get; set; }
        /// <summary>
        /// 模板-管理员-订单付款
        /// </summary>
        public string SMSTPL_Admin_orderpaid { get; set; }
        /// <summary>
        /// 模板-管理员-订单收货
        /// </summary>
        public string SMSTPL_Admin_orderrecive { get; set; }
        /// <summary>
        /// 模板-管理员-订单留言
        /// </summary>
        public string SMSTPL_Admin_ordercomment { get; set; }
        /// <summary>
        /// 模板-管理员-留言反馈
        /// </summary>
        public string SMSTPL_Admin_inquiry { get; set; }
        /// <summary>
        /// 模板-管理员-商品评论
        /// </summary>
        public string SMSTPL_Admin_comment { get; set; }
        /// <summary>
        /// 模板-管理员-商品咨询
        /// </summary>
        public string SMSTPL_Admin_ask { get; set; }
        /// <summary>
        /// 模板-管理员-站内信
        /// </summary>
        public string SMSTPL_Admin_message { get; set; }
        /// <summary>
        /// 短信帐号
        /// </summary>
        public string SMS_user { get; set; }
        /// <summary>
        /// 短信密码
        /// </summary>
        public string SMS_password { get; set; }
        /// <summary>
        /// 短信后缀
        /// </summary>
        public string SMS_lastmsg { get; set; }
        /// <summary>
        /// 短信平台
        /// </summary>
        public string SMS_server { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string SMS_state { get; set; }
        /// <summary>
        /// 发送通道
        /// </summary>
        public string SMS_apitype { get; set; }
        /// <summary>
        /// 发送设置
        /// </summary>
        public string SMS_sendmode { get; set; }
        /// <summary>
        /// 抄送手机
        /// </summary>
        public string SMS_reciveno { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public string SMS_serverport { get; set; }
        /// <summary>
        /// 最大字符
        /// </summary>
        public string SMS_maxlen { get; set; }
        /// <summary>
        /// 通用接口地址
        /// </summary>
        public string SMS_httpapi { get; set; }
        /// <summary>
        /// 是否允许同一手机号多次注册
        /// </summary>
        public string IsMobilePhoneMutiReg { get; set; }
        #endregion
        #region 退税
        /// <summary>
        /// 退税-最低购物总额
        /// </summary>
        public string Refund_MinMoney { get; set; }
        /// <summary>
        /// 退税-手续费函数率
        /// </summary>
        public string Refund_StepR { get; set; }
        /// <summary>
        /// 退税-增值税率
        /// </summary>
        public string Refund_VAT { get; set; }
        #endregion
        #region 新事件
        /// <summary>
        /// 订单-未确认
        /// </summary>
        public string NewEvent_Order_IsVerified { get; set; }
        /// <summary>
        /// 订单-已支付
        /// </summary>
        public string NewEvent_Order_IsPaid { get; set; }
        /// <summary>
        /// 订单-未发货
        /// </summary>
        public string NewEvent_Order_IsShipped { get; set; }
        #endregion
        #region 验证码开关
        /// <summary>
        /// 会员注册
        /// </summary>
        public string Verifycode_UserRegister { get; set; }
        /// <summary>
        /// 会员登录
        /// </summary>
        public string Verifycode_UserLogin { get; set; }
        /// <summary>
        /// 忘记密码
        /// </summary>
        public string Verifycode_ForgetPassword { get; set; }
        /// <summary>
        /// 商家注册
        /// </summary>
        public string Verifycode_SupplierRegister { get; set; }
        /// <summary>
        /// 商家登录
        /// </summary>
        public string Verifycode_SupplierLogin { get; set; }
        /// <summary>
        /// 管理登录
        /// </summary>
        public string Verifycode_AdminLogin { get; set; }
        #endregion
    }
}