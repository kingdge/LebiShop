using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Shop.Admin.Ajax
{
    /// <summary>
    /// 营销管理相关的操作
    /// </summary>
    public partial class Ajax_sale : AdminAjaxBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }
        /// <summary>
        /// 编辑优惠券类型
        /// </summary>
        public void CardType_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_CardOrder model = B_Lebi_CardOrder.GetModel(id);
            if (model == null)
            {
                if (!EX_Admin.Power("cardtype_add", "添加优惠券"))
                {
                    AjaxNoPower();
                    return;
                }
                model = new Lebi_CardOrder();
                model = B_Lebi_CardOrder.BindForm(model);
                model.Pro_Type_ids = RequestTool.RequestString("Pro_Type_ids");
                model.Name = Language.RequestString("Name");
                model.Length = model.NO_End.ToString().Length;
                if (model.Type_id_CardType == 311)
                {
                    model.IsPayOnce = 0;
                    model.IsCanOtherUse = 1;
                    model.Money_Buy = 0;
                    model.Pro_Type_ids = "";
                }
                else
                {
                    model.IsPayOnce = 1;
                }
                B_Lebi_CardOrder.Add(model);
                string description = Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN");
                Log.Add(Tag("添加优惠券"), "CardType", id.ToString(), CurrentAdmin, description);
            }
            else
            {
                if (!EX_Admin.Power("cardtype_edit", "编辑优惠券"))
                {
                    AjaxNoPower();
                    return;
                }
                model = B_Lebi_CardOrder.BindForm(model);
                model.Pro_Type_ids = RequestTool.RequestString("Pro_Type_ids");
                model.Name = Language.RequestString("Name");
                model.Length = model.NO_End.ToString().Length;
                if (model.Type_id_CardType == 311)
                {
                    model.IsPayOnce = 0;
                    model.IsCanOtherUse = 1;
                    model.Money_Buy = 0;
                    model.Pro_Type_ids = "";
                }
                else
                    model.IsPayOnce = 1;
                B_Lebi_CardOrder.Update(model);
                string description = Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN");
                Log.Add(Tag("编辑优惠券"), "CardType", id.ToString(), CurrentAdmin, description);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 生成优惠券
        /// </summary>
        public void Card_Create()
        {
            if (!EX_Admin.Power("cardtype_add", "添加优惠券"))
            {
                AjaxNoPower();
                return;
            }
            int CardType = RequestTool.RequestInt("CardType", 0);
            Lebi_CardOrder ct = B_Lebi_CardOrder.GetModel(CardType);
            if (ct == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            int end = ct.NO_Now + 20;
            end = end > ct.NO_End ? ct.NO_End : end;
            ct.NO_Now = ct.NO_Now < ct.NO_Start ? ct.NO_Start : ct.NO_Now;
            Lebi_Card model = new Lebi_Card();
            int leng = ct.Length;
            string no = "";
            string finish = "";
            for (int i = ct.NO_Now + 1; i < end + 1; i++)
            {
                no = i.ToString().PadLeft(leng, '0');
                model.CardOrder_id = ct.id;
                model.Code = ct.IndexCode + no;
                model.IsCanOtherUse = ct.IsCanOtherUse;
                model.IsPayOnce = model.IsPayOnce;
                model.Money = ct.Money;
                model.Money_Last = ct.Money;
                model.Money_Used = 0;
                model.Password = GetRnd(6, true, false, false, false, "");
                model.Pro_Type_ids = ct.Pro_Type_ids;
                model.Time_Begin = ct.Time_Begin;
                model.Time_End = ct.Time_End;
                model.Type_id_CardStatus = 200;
                model.Money_Buy = ct.Money_Buy;
                model.Type_id_CardType = ct.Type_id_CardType;
                model.number = i;
                model.IndexCode = ct.IndexCode;
                B_Lebi_Card.Add(model);
            }
            if (end == ct.NO_End)
            {
                finish = "OK";
            }
            ct.NO_Now = end;
            B_Lebi_CardOrder.Update(ct);
            Response.Write("{\"msg\":\"OK\",\"code\":\"" + model.Code + "\",\"status\":\"" + finish + "\"}");
        }
        /// <summary>
        /// 删除卡券
        /// </summary>
        public void Card_Del()
        {
            if (!EX_Admin.Power("card_del", "删除卡券"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("id");
            if (id != "")
            {
                B_Lebi_Card.Delete("id in (lbsql{" + id + "})");
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量修改卡券
        /// </summary>
        public void Cards_Edit()
        {
            if (!EX_Admin.Power("card_edit", "修改卡券"))
            {
                AjaxNoPower();
                return;
            }
            string card_ids = RequestTool.RequestString("card_ids");
            string card_codes = RequestTool.RequestString("card_codes");
            SearchCard su = new SearchCard(CurrentAdmin, CurrentLanguage.Code);
            int CardStatus = RequestTool.RequestInt("CardStatus", 0);
            if (CardStatus == 0)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            string where = "";
            if (card_ids != "")
            {
                where = "id in (" + card_ids + ")";
            }
            else
            {
                //where = "id>0 " + su.SQL;
                Response.Write("{\"msg\":\"" + Tag("没有选择任何数据") + "\"}");
                return;
            }
            string sql = "update [Lebi_Card] set Type_id_CardStatus=" + CardStatus + " where " + where;
            Common.ExecuteSql(sql);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="length">目标字符串的长度</param>
        /// <param name="useNum">是否包含数字，1=包含，默认为包含</param>
        /// <param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        /// <param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        /// <param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        /// <param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        /// <returns>指定长度的随机字符串</returns>
        private string GetRnd(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;

            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }

            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }

            return s;
        }
        /// <summary>
        /// 编辑保存促销
        /// </summary>
        public void Promotion_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            int tid = RequestTool.RequestInt("tid", 0);
            Lebi_Promotion model = B_Lebi_Promotion.GetModel(id);
            Lebi_Promotion_Type pt;
            bool addflag = false;
            if (model == null)
            {
                if (!EX_Admin.Power("promotion_add", "添加促销活动"))
                {
                    AjaxNoPower();
                    return;
                }
                pt = B_Lebi_Promotion_Type.GetModel(tid);
                model = new Lebi_Promotion();
                addflag = true;
            }
            else
            {
                if (!EX_Admin.Power("promotion_edit", "编辑促销活动"))
                {
                    AjaxNoPower();
                    return;
                }
                pt = B_Lebi_Promotion_Type.GetModel(model.Promotion_Type_id);
            }
            if (pt == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            B_Lebi_Promotion.BindForm(model);
            model.IsCase801 = RequestTool.RequestInt("IsCase801", 0);
            model.IsCase802 = RequestTool.RequestInt("IsCase802", 0);
            model.IsCase803 = RequestTool.RequestInt("IsCase803", 0);
            model.IsCase804 = RequestTool.RequestInt("IsCase804", 0);
            model.IsCase805 = RequestTool.RequestInt("IsCase805", 0);
            model.IsCase806 = RequestTool.RequestInt("IsCase806", 0);
            model.IsRule901 = RequestTool.RequestInt("IsRule901", 0);
            model.IsRule902 = RequestTool.RequestInt("IsRule902", 0);
            model.IsRule903 = RequestTool.RequestInt("IsRule903", 0);
            model.IsRule904 = RequestTool.RequestInt("IsRule904", 0);
            model.IsRule905 = RequestTool.RequestInt("IsRule905", 0);
            model.IsRule906 = RequestTool.RequestInt("IsRule906", 0);
            model.IsRule907 = RequestTool.RequestInt("IsRule907", 0);
            model.IsRule908 = RequestTool.RequestInt("IsRule908", 0);
            model.IsRule909 = RequestTool.RequestInt("IsRule909", 0);
            model.IsRule910 = RequestTool.RequestInt("IsRule910", 0);
            model.IsRule911 = RequestTool.RequestInt("IsRule911", 0);
            model.IsRule912 = RequestTool.RequestInt("IsRule912", 0);
            model.Case804 = RequestTool.RequestString("Case804"); ;
            //处理包含商品
            string Case805 = RequestTool.RequestString("Case805");
            Case805 = Case805.Replace(",", "','");
            Case805 = "'" + Case805 + "'";
            List<Lebi_Product> pros = B_Lebi_Product.GetList("Number in (" + Case805 + ")", "");
            string ids = "";
            foreach (Lebi_Product pro in pros)
            {
                if (ids == "")
                    ids = pro.id.ToString();
                else
                    ids = ids + "," + pro.id.ToString();
            }
            model.Case805 = ids;
            model.Admin_id = CurrentAdmin.id;
            model.Admin_UserName = CurrentAdmin.UserName;
            model.Time_Start = pt.Time_Start;
            model.Time_End = pt.Time_End;
            model.Type_id_PromotionStatus = pt.Type_id_PromotionStatus;
            model.Remark = RequestTool.RequestString("Remark");
            model.Sort = RequestTool.RequestInt("Sort", 0);
            model.Promotion_Type_id = pt.id;
            if (model.IsCase801 > 0 || model.IsCase802 > 0 || model.IsCase803 > 0 || model.IsCase804 > 0 || model.IsCase805 > 0)
                model.IsSetCase = 1;
            else
                model.IsSetCase = 0;
            if (addflag)
            {
                B_Lebi_Promotion.Add(model);
                model.id = B_Lebi_Promotion.GetMaxId();
            }
            else
            {
                B_Lebi_Promotion.Update(model);
            }
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + model.id + "\"}");
        }
        /// <summary>
        /// 编辑促销
        /// </summary>
        public void PromotionType_Edit()
        {

            int id = RequestTool.RequestInt("id", 0);
            Lebi_Promotion_Type model = B_Lebi_Promotion_Type.GetModel(id);
            bool addfalg = false;

            if (model == null)
            {
                if (!EX_Admin.Power("promotion_add", "添加促销活动"))
                {
                    AjaxNoPower();
                    return;
                }
                model = new Lebi_Promotion_Type();
                addfalg = true;
            }


            model = B_Lebi_Promotion_Type.BindForm(model);
            model.Name = Language.RequestString("Name");
            model.Content = Language.RequestString("Content");
            if (addfalg)
            {
                model.Admin_id = CurrentAdmin.id;
                model.Admin_UserName = CurrentAdmin.UserName;
                B_Lebi_Promotion_Type.Add(model);
                model.id = B_Lebi_Promotion_Type.GetMaxId();
            }
            else
            {
                if (!EX_Admin.Power("promotion_edit", "编辑促销活动"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Promotion_Type.Update(model);
                //更新规则的状态
                List<Lebi_Promotion> ps = B_Lebi_Promotion.GetList("Promotion_Type_id=" + model.id + "", "");
                foreach (Lebi_Promotion p in ps)
                {
                    p.Type_id_PromotionStatus = model.Type_id_PromotionStatus;
                    p.Time_End = model.Time_End;
                    p.Time_Start = model.Time_Start;
                    B_Lebi_Promotion.Update(p);
                }
            }
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + model.id + "\"}");
        }

        /// <summary>
        /// 删除规则
        /// </summary>
        public void Promotion_Del()
        {
            if (!EX_Admin.Power("promotion_del", "删除促销活动"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("id");
            if (id != "")
            {
                B_Lebi_Promotion.Delete("id in (lbsql{" + id + "})");
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除促销
        /// </summary>
        public void PromotionType_Del()
        {
            if (!EX_Admin.Power("promotion_del", "删除促销活动"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("id");
            if (id != "")
            {
                B_Lebi_Promotion_Type.Delete("id in (lbsql{" + id + "})");
                B_Lebi_Promotion.Delete("Promotion_Type_id in (lbsql{" + id + "})");
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        ///编辑群发邮件
        /// </summary>
        public void EmailTask_Edit()
        {
            if (!EX_Admin.Power("emailtask_edit", "群发邮件"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_EmailTask model = B_Lebi_EmailTask.GetModel(id);
            if (model == null)
            {
                model = new Lebi_EmailTask();
            }
            model.EmailContent = Language.RequestString("EmailContent");
            model.EmailTitle = Language.RequestString("EmailTitle");
            model.UserLevel_ids = RequestTool.RequestString("UserLevel_ids");
            model.Time_task = RequestTool.RequestTime("Time_task");
            model.Admin_id = CurrentAdmin.id;
            model.Admin_UserName = CurrentAdmin.UserName;


            if (model.id == 0)
            {
                B_Lebi_EmailTask.Add(model);
                model.id = B_Lebi_EmailTask.GetMaxId();
            }
            else
            {
                B_Lebi_EmailTask.Update(model);
            }
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + model.id + "\"}");
        }
        /// <summary>
        ///生成邮件任务
        /// </summary>
        public void EmailTask_Create()
        {
            if (!EX_Admin.Power("emailtask_edit", "群发邮件"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_EmailTask model = B_Lebi_EmailTask.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            model.EmailContent = Language.RequestString("EmailContent");
            model.EmailTitle = Language.RequestString("EmailTitle");
            model.UserLevel_ids = RequestTool.RequestString("UserLevel_ids");
            model.Time_task = RequestTool.RequestTime("Time_task");
            B_Lebi_EmailTask.Update(model);
            //=================
            //处理图片
            string content = model.EmailContent;
            Regex r = new Regex(@"[sS][rR][cC]=\\\"".*?\\\""", RegexOptions.Singleline);
            MatchCollection mc = r.Matches(content);
            string temp = "";
            string src = "";
            foreach (Match m in mc)
            {
                temp = m.Value;

                src = RegexTool.GetRegValue(temp, @"[sS][rR][cC]=\\\""(.*?)\\\""");
                if (!Regex.IsMatch(src.ToLower(), @"http://.*?", RegexOptions.IgnoreCase))
                //if (!src.ToLower().Contains("http://"))
                {
                    src = SYS.Domain + "/" + src;
                    src = ThemeUrl.CheckURL(src);
                    content = content.Replace(temp, "src=\\\"" + src + "\"");
                }
            }
            //处理图片结束
            //====================
            if (model.UserLevel_ids == "")
            {
                model.UserLevel_ids = "0";
            }
            List<Lebi_User> users = B_Lebi_User.GetList("UserLevel_id in (" + model.UserLevel_ids + ")", "");
            Lebi_Email email = new Lebi_Email();
            foreach (Lebi_User user in users)
            {
                if (Validator.IsEmail(user.Email)) { 
                    email.Content = Language.Content(content, user.Language);
                    email.Title = Language.Content(model.EmailTitle, user.Language);
                    email.Time_Task = model.Time_task;
                    email.User_id = user.id;
                    email.User_Name = user.UserName;
                    email.EmailTask_id = model.id;
                    email.Email = user.Email;
                    email.Count_send = 0;
                    email.Type_id_EmailStatus = 270;//排队中的邮件
                    B_Lebi_Email.Add(email);
                }
            }
            model.IsSubmit = 1;
            B_Lebi_EmailTask.Update(model);
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 删除邮件任务
        /// </summary>
        public void EmailTask_Del()
        {
            if (!EX_Admin.Power("emailtask_del", "删除群发邮件"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("id");
            if (id != "")
            {
                B_Lebi_EmailTask.Delete("id in (lbsql{" + id + "})");
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
    }
}