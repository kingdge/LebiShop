using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;
using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.Script.Serialization;

namespace Shop.Bussiness
{
    /// <summary>
    /// 会员查询
    /// </summary>
    public class SearchUser
    {
        public SearchUserModel Model;
        public string Description;
        public string SQL;
        public string URL;
        public SearchUser(Lebi_Administrator admin, string lang)
        {
            Model = User_Set(admin);
            Description = User_DES(Model, lang);
            SQL = User_SQL(Model);
            URL = User_URL(Model);
        }
        /// <summary>
        /// 保存会员查询参数
        /// </summary>
        public SearchUserModel User_Set(Lebi_Administrator admin)
        {
            SearchUserModel su = new SearchUserModel();
            su.Birthday1 = RequestTool.RequestString("Birthday1");
            su.Birthday2 = RequestTool.RequestString("Birthday2");
            su.Email = RequestTool.RequestString("Email");
            su.Fax = RequestTool.RequestString("Fax");
            su.Language = RequestTool.RequestString("Language");
            su.Logins1 = RequestTool.RequestString("Logins1");
            su.Logins2 = RequestTool.RequestString("Logins2");
            su.MobilePhone = RequestTool.RequestString("MobilePhone");
            su.Money_xiaofei1 = RequestTool.RequestString("Money_xiaofei1");
            su.Money_xiaofei2 = RequestTool.RequestString("Money_xiaofei2");
            su.Money1 = RequestTool.RequestString("Money1");
            su.Money2 = RequestTool.RequestString("Money2");
            su.NickName = RequestTool.RequestString("NickName");
            su.Phone = RequestTool.RequestString("Phone");
            su.Point1 = RequestTool.RequestString("Point1");
            su.Point2 = RequestTool.RequestString("Point2");
            su.QQ = RequestTool.RequestString("QQ");
            su.RealName = RequestTool.RequestString("RealName");
            su.Sex = RequestTool.RequestString("Sex");
            su.Time_login1 = RequestTool.RequestString("Time_login1");
            su.Time_login2 = RequestTool.RequestString("Time_login2");
            su.Time_reg1 = RequestTool.RequestString("Time_reg1");
            su.Time_reg2 = RequestTool.RequestString("Time_reg2");
            su.UserLevel_id = RequestTool.RequestString("UserLevel_id");
            su.UserName = RequestTool.RequestString("UserName");
            su.lbsql_Birthday1 = RequestTool.RequestDate("Birthday1");
            su.lbsql_Birthday2 = RequestTool.RequestDate("Birthday2");
            su.lbsql_Time_login1 = RequestTool.RequestDate("Time_login1");
            su.lbsql_Time_login2 = RequestTool.RequestDate("Time_login2");
            su.lbsql_Time_reg1 = RequestTool.RequestDate("Time_reg1");
            su.lbsql_Time_reg2 = RequestTool.RequestDate("Time_reg2");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string json = jss.Serialize(su);
            //admin.SearchUser = json;
            //B_Lebi_Administrator.Update(admin);
            return su;
        }
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public string User_SQL(SearchUserModel su)
        {
            string sql = "";
            if (su.Birthday1 != "")
                sql += " and Birthday>='" + su.lbsql_Birthday1 + "'";
            if (su.Birthday2 != "")
                sql += " and Birthday<='" + su.lbsql_Birthday1 + "'";
            if (su.Email != "")
                sql += " and Email like lbsql{'%" + su.Email + "%'}";
            if (su.Fax != "")
                sql += " and Fax like lbsql{'%" + su.Fax + "%'}";
            if (su.Language != "")
                sql += " and Language in (lbsql{'" + su.Language.Replace(",", "','") + "'})";
            if (su.Logins1 != "")
                sql += " and Count_Login >=" + su.Logins1 + "";
            if (su.Logins2 != "")
                sql += " and Count_Login >=" + su.Logins2 + "";
            if (su.MobilePhone != "")
                sql += " and MobilePhone like lbsql{'%" + su.MobilePhone + "%'}";
            if (su.Money_xiaofei1 != "")
                sql += " and Money_xiaofei >=" + su.Money_xiaofei1 + "";
            if (su.Money_xiaofei2 != "")
                sql += " and Money_xiaofei <=" + su.Money_xiaofei2 + "";
            if (su.Money1 != "")
                sql += " and Money >=" + su.Money1 + "";
            if (su.Money2 != "")
                sql += " and Money <=" + su.Money2 + "";
            if (su.NickName != "")
                sql += " and NickName like lbsql{'%" + su.NickName + "%'}";
            if (su.Phone != "")
                sql += " and Phone like lbsql{'%" + su.Phone + "%'}";
            if (su.Point1 != "")
                sql += " and Point >=" + su.Point1 + "";
            if (su.Point2 != "")
                sql += " and Point <=" + su.Point2 + "";
            if (su.QQ != "")
                sql += " and QQ like lbsql{'%" + su.QQ + "%'}";
            if (su.RealName != "")
                sql += " and RealName like lbsql{'%" + su.RealName + "%'}";
            if (su.Sex != "")
                sql += " and Sex in (lbsql{'" + su.Sex.Replace(",", "','") + "'})";
            if (su.Time_login1 != "")
                sql += " and Time_This>='" + su.lbsql_Time_login1 + "'";
            if (su.Time_login2 != "")
                sql += " and Time_This<='" + su.lbsql_Time_login2 + " 23:59:59'";
            if (su.Time_reg1 != "")
                sql += " and Time_Reg>='" + su.lbsql_Time_reg1 + "'";
            if (su.Time_reg2 != "")
                sql += " and Time_Reg<='" + su.lbsql_Time_reg2 + " 23:59:59'";
            if (su.UserLevel_id != "")
                sql += " and UserLevel_id in (lbsql{" + su.UserLevel_id + "})";
            if (su.UserName != "")
                sql += " and UserName like lbsql{'%" + su.UserName + "%'}";
            return sql;
        }
        /// <summary>
        /// 生成查询条件文字
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public string User_DES(SearchUserModel su, string lang)
        {
            string str = "";
            if (su.UserName != "")
                str += "<span class=\"searchname\">" + Language.Tag("会员帐号", lang) + "：</span><span class=\"searchvalue\">" + su.UserName + "</span> ";
            if (su.NickName != "")
                str += "<span class=\"searchname\">" + Language.Tag("昵称", lang) + "：</span><span class=\"searchvalue\">" + su.UserName + "</span> ";
            if (su.RealName != "")
                str += "<span class=\"searchname\">" + Language.Tag("真实姓名", lang) + "：</span><span class=\"searchvalue\">" + su.RealName + "</span> ";
            if (su.Birthday1 != "" || su.Birthday2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("生日", lang) + "：</span><span class=\"searchvalue\">" + su.Birthday1 + "-" + su.Birthday2 + "</span> ";
            if (su.Email != "")
                str += "<span class=\"searchname\">" + "Email：" + su.Email + " ";
            if (su.Fax != "")
                str += "<span class=\"searchname\">" + Language.Tag("传真", lang) + "：</span><span class=\"searchvalue\">" + su.Fax + "</span> ";
            if (su.QQ != "")
                str += "<span class=\"searchname\">" + "QQ：" + su.QQ + " ";
            if (su.Sex != "")
                str += "<span class=\"searchname\">" + Language.Tag("性别", lang) + "：</span><span class=\"searchvalue\">" + su.Sex + "</span> ";
            if (su.Phone != "")
                str += "<span class=\"searchname\">" + Language.Tag("电话", lang) + "：</span><span class=\"searchvalue\">" + su.Phone + "</span> ";
            if (su.MobilePhone != "")
                str += "<span class=\"searchname\">" + Language.Tag("手机", lang) + "：</span><span class=\"searchvalue\">" + su.MobilePhone + "</span> ";
            if (su.UserLevel_id != "")
            {
                List<Lebi_UserLevel> ul = B_Lebi_UserLevel.GetList("id in (lbsql{" + su.UserLevel_id + "})", "");
                string lstr = "";
                foreach (Lebi_UserLevel l in ul)
                {
                    if (lstr == "")
                        lstr = Language.Content(l.Name, lang);
                    else
                        lstr += "," + Language.Content(l.Name, lang);
                }
                str += "<span class=\"searchname\">" + Language.Tag("会员类型", lang) + "：</span><span class=\"searchvalue\">" + lstr + "</span> ";
            }
            if (su.Language != "")
                str += "<span class=\"searchname\">" + Language.Tag("语言", lang) + "：</span><span class=\"searchvalue\">" + su.Language + "</span> ";
            if (su.Logins1 != "" || su.Logins2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("登录次数", lang) + "：</span><span class=\"searchvalue\">" + su.Logins1 + "-" + su.Logins2 + "</span> ";
            if (su.Money_xiaofei1 != "" || su.Money_xiaofei2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("消费", lang) + "：</span><span class=\"searchvalue\">" + su.Money_xiaofei1 + "-" + su.Money_xiaofei2 + "</span> ";
            if (su.Money1 != "" || su.Money2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("余额", lang) + "：</span><span class=\"searchvalue\">" + su.Money1 + "-" + su.Money2 + "</span> ";
            if (su.Point1 != "" || su.Point2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("积分", lang) + "：</span><span class=\"searchvalue\">" + su.Point1 + "-" + su.Point2 + "</span> ";
            if (su.Time_login1 != "" || su.Time_login2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("登录时间", lang) + "：</span><span class=\"searchvalue\">" + su.Time_login1 + "-" + su.Time_login2 + "</span> ";
            if (su.Time_reg1 != "" || su.Time_reg2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("注册时间", lang) + "：</span><span class=\"searchvalue\">" + su.Time_reg1 + "-" + su.Time_reg2 + "</span> ";
            return str;
        }

        /// <summary>
        /// 生成查询条件地址栏参数
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public string User_URL(SearchUserModel su)
        {
            string sql = "";
            if (su.Birthday1 != "")
                sql += "&Birthday1=" + su.Birthday1 + "";
            if (su.Birthday2 != "")
                sql += "&Birthday2=" + su.Birthday2 + "";
            if (su.Email != "")
                sql += "&Email=" + su.Email + "";
            if (su.Fax != "")
                sql += "&Fax=" + su.Fax + "";
            if (su.Language != "")
                sql += "&Language=" + su.Language;
            if (su.Logins1 != "")
                sql += "&Logins1=" + su.Logins1;
            if (su.Logins2 != "")
                sql += "&Logins2=" + su.Logins2;
            if (su.MobilePhone != "")
                sql += "&MobilePhone=" + su.MobilePhone;
            if (su.Money_xiaofei1 != "")
                sql += "&Money_xiaofei1=" + su.Money_xiaofei1;
            if (su.Money_xiaofei2 != "")
                sql += "&Money_xiaofei2=" + su.Money_xiaofei2;
            if (su.Money1 != "")
                sql += "&Money1=" + su.Money1;
            if (su.Money2 != "")
                sql += "&Money2=" + su.Money2;
            if (su.NickName != "")
                sql += "&NickName=" + su.NickName;
            if (su.Phone != "")
                sql += "&Phone=" + su.Phone;
            if (su.Point1 != "")
                sql += "&Point1=" + su.Point1;
            if (su.Point2 != "")
                sql += "&Point2=" + su.Point2;
            if (su.QQ != "")
                sql += "&QQ=" + su.QQ;
            if (su.RealName != "")
                sql += "&RealName=" + su.RealName;
            if (su.Sex != "")
                sql += "&Sex=" + su.Sex;
            if (su.Time_login1 != "")
                sql += "&Time_login1=" + su.Time_login1;
            if (su.Time_login2 != "")
                sql += "&Time_login2=" + su.Time_login2;
            if (su.Time_reg1 != "")
                sql += "&Time_reg1=" + su.Time_reg1;
            if (su.Time_reg2 != "")
                sql += "&Time_reg2=" + su.Time_reg2;
            if (su.UserLevel_id != "")
                sql += "&UserLevel_id=" + su.UserLevel_id;
            if (su.UserName != "")
                sql += "&UserName=" + su.UserName;
            if (sql.IndexOf("&") == 0 && sql.Length > 1)
            {
                sql = sql.TrimStart('&');
            }

            return sql;
        }
    }
    /// <summary>
    /// 卡券查询
    /// </summary>
    public class SearchCard
    {
        public SearchCardModel Model;
        public string Description;
        public string SQL;
        public string URL;
        public SearchCard(Lebi_Administrator admin, string lang)
        {
            Model = Card_Set(admin);
            Description = Card_DES(Model, lang);
            SQL = Card_SQL(Model);
            URL = Card_URL(Model);
        }
        /// <summary>
        /// 保存查询参数
        /// </summary>
        public SearchCardModel Card_Set(Lebi_Administrator admin)
        {
            SearchCardModel su = new SearchCardModel();
            su.Code = RequestTool.RequestString("Code");
            su.IndexCode = RequestTool.RequestString("IndexCode");
            su.Money = RequestTool.RequestString("Money");
            su.Money_Buy = RequestTool.RequestString("Money_Buy");
            su.Money_Used = RequestTool.RequestString("Money_Used");
            su.number1 = RequestTool.RequestString("number1");
            su.number2 = RequestTool.RequestString("number2");
            su.Time_begin1 = RequestTool.RequestString("Time_begin1");
            su.Time_begin2 = RequestTool.RequestString("Time_begin2");
            su.Time_end1 = RequestTool.RequestString("Time_end1");
            su.Time_end2 = RequestTool.RequestString("Time_end2");
            su.Type_id_CardStatus = RequestTool.RequestString("Type_id_CardStatus");
            su.Type_id_CardType = RequestTool.RequestString("type");
            su.UserName = RequestTool.RequestString("UserName");
            su.lbsql_Time_begin1 = RequestTool.RequestDate("Time_begin1");
            su.lbsql_Time_begin2 = RequestTool.RequestDate("Time_begin2");
            su.lbsql_Time_end1 = RequestTool.RequestDate("Time_end1");
            su.lbsql_Time_end2 = RequestTool.RequestDate("Time_end2");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            //string json = jss.Serialize(su);
            //admin.SearchUser = json;
            //B_Lebi_Administrator.Update(admin);
            return su;
        }
        /// <summary>
        /// 生成查询条件文字
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public string Card_DES(SearchCardModel su, string lang)
        {
            string str = "";
            if (su.Type_id_CardType != "")
            {
                List<Lebi_Type> ul = B_Lebi_Type.GetList("id in (lbsql{" + su.Type_id_CardType + "})", "");
                string lstr = "";
                foreach (Lebi_Type l in ul)
                {
                    if (lstr == "")
                        lstr = Language.Tag(l.Name, lang);
                    else
                        lstr += "," + Language.Tag(l.Name, lang);
                }
                str += "<span class=\"searchname\">" + Language.Tag("类型", lang) + "：</span><span class=\"searchvalue\">" + lstr + "</span> ";
            }
            if (su.Type_id_CardStatus != "")
            {
                List<Lebi_Type> ul = B_Lebi_Type.GetList("id in (lbsql{" + su.Type_id_CardStatus + "})", "");
                string lstr = "";
                foreach (Lebi_Type l in ul)
                {
                    if (lstr == "")
                        lstr = Language.Tag(l.Name, lang);
                    else
                        lstr += "," + Language.Tag(l.Name, lang);
                }
                str += "<span class=\"searchname\">" + Language.Tag("状态", lang) + "：</span><span class=\"searchvalue\">" + lstr + "</span> ";
            }
            if (su.Code != "")
                str += "<span class=\"searchname\">" + Language.Tag("编码", lang) + "：</span><span class=\"searchvalue\">" + su.Code + "</span> ";
            if (su.IndexCode != "")
                str += "<span class=\"searchname\">" + Language.Tag("头字符", lang) + "：</span><span class=\"searchvalue\">" + su.IndexCode + "</span> ";
            if (su.Money != "")
                str += "<span class=\"searchname\">" + Language.Tag("面值", lang) + "：</span><span class=\"searchvalue\">" + su.Money + "</span> ";
            if (su.Money_Buy != "")
                str += "<span class=\"searchname\">" + Language.Tag("最低消费", lang) + "：</span><span class=\"searchvalue\">" + su.Money_Buy + "</span> ";
            if (su.Money_Used != "")
                str += "<span class=\"searchname\">" + Language.Tag("已使用", lang) + "：</span><span class=\"searchvalue\">" + su.Money_Used + "</span> ";
            if (su.number1 != "" || su.number2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("号码", lang) + "：</span><span class=\"searchvalue\">" + su.number1 + "-" + su.number2 + "</span> ";
            if (su.Time_begin1 != "" || su.Time_begin2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("生效时间", lang) + "：</span><span class=\"searchvalue\">" + su.Time_begin1 + "-" + su.Time_begin2 + "</span> ";
            if (su.Time_end1 != "" || su.Time_end2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("失效时间", lang) + "：</span><span class=\"searchvalue\">" + su.Time_end1 + "-" + su.Time_end2 + "</span> ";
            if (su.UserName != "")
                str += "<span class=\"searchname\">" + Language.Tag("用户", lang) + "：</span><span class=\"searchvalue\">" + su.UserName + "</span> ";


            return str;
        }
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public string Card_SQL(SearchCardModel su)
        {
            string sql = "";
            if (su.Code != "")
                sql += " and Code like lbsql{'%" + su.Code + "%'}";
            if (su.IndexCode != "")
                sql += " and IndexCode=lbsql{'" + su.IndexCode + "'}";
            if (su.Money != "")
                sql += " and Money=" + su.Money + "";
            if (su.Money_Buy != "")
                sql += " and Money_Buy=" + su.Money_Buy + "";
            if (su.Money_Used != "")
                sql += " and Money_Used=" + su.Money_Used + "";
            if (su.number1 != "")
                sql += " and number >=" + su.number1 + "";
            if (su.number2 != "")
                sql += " and number <=" + su.number2 + "";
            if (su.Time_begin1 != "")
                sql += " and Time_Begin>='" + su.lbsql_Time_begin1 + "'";
            if (su.Time_begin2 != "")
                sql += " and Time_Begin<='" + su.lbsql_Time_begin2 + " 23:59:59'";
            if (su.Time_end1 != "")
                sql += " and Time_Begin>='" + su.lbsql_Time_end1 + "'";
            if (su.Time_end2 != "")
                sql += " and Time_End<='" + su.lbsql_Time_end2 + " 23:59:59'";
            if (su.Type_id_CardStatus != "")
                sql += " and Type_id_CardStatus in (lbsql{" + su.Type_id_CardStatus + "})";
            if (su.Type_id_CardType != "")
                sql += " and Type_id_CardType in (lbsql{" + su.Type_id_CardType + "})";
            if (su.UserName != "")
                sql += " and User_UserName like lbsql{'%" + su.UserName + "%'}";
            return sql;
        }
        /// <summary>
        /// 生成查询条件地址栏参数
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public string Card_URL(SearchCardModel su)
        {
            string sql = "";
            if (su.Code != "")
                sql += "&Code=" + su.Code + "";
            if (su.IndexCode != "")
                sql += "&IndexCode=" + su.IndexCode + "";
            if (su.Money != "")
                sql += "&Money=" + su.Money + "";
            if (su.Money_Buy != "")
                sql += "&Money_Buy=" + su.Money_Buy + "";
            if (su.Money_Used != "")
                sql += "&Money_Used=" + su.Money_Used + "";
            if (su.number1 != "")
                sql += "&number1=" + su.number1 + "";
            if (su.number2 != "")
                sql += "&number2=" + su.number2 + "";
            if (su.Time_begin1 != "")
                sql += "&Time_begin1=" + su.Time_begin1 + "";
            if (su.Time_begin2 != "")
                sql += "&Time_begin2=" + su.Time_begin2 + "";
            if (su.Time_end1 != "")
                sql += "&Time_end1=" + su.Time_end1 + "";
            if (su.Time_end2 != "")
                sql += "&Time_end2=" + su.Time_end2 + "";
            if (su.Type_id_CardStatus != "")
                sql += "&Type_id_CardStatus=" + su.Type_id_CardStatus + "";
            if (su.Type_id_CardType != "")
                sql += "&type=" + su.Type_id_CardType + "";
            if (su.UserName != "")
                sql += "&UserName=" + su.UserName;
            if (sql.IndexOf("&") == 0 && sql.Length > 1)
            {
                sql = sql.TrimStart('&');
            }

            return sql;
        }
    }
    /// <summary>
    /// 商品查询
    /// </summary>
    public class SearchProduct
    {
        public SearchProductModel Model;
        public string Description;
        public string SQL;
        public string URL;
        public SearchProduct(Lebi_Administrator admin, string lang)
        {
            Model = Model_Set();
            Description = Model_DES(Model, lang);
            SQL = Model_SQL(Model);
            URL = Model_URL(Model);
        }
        public SearchProduct(Lebi_Supplier admin, string lang)
        {
            Model = Model_Set();
            Description = Model_DES(Model, lang);
            SQL = Model_SQL(Model);
            URL = Model_URL(Model);
        }
        /// <summary>
        /// 保存查询参数
        /// </summary>
        public SearchProductModel Model_Set()
        {
            SearchProductModel sm = new SearchProductModel();
            sm.Brand_id = RequestTool.RequestString("Brand_id");
            sm.Count_Sales1 = RequestTool.RequestString("Count_Sales1");
            sm.Count_Sales2 = RequestTool.RequestString("Count_Sales2");
            sm.Count_Stock1 = RequestTool.RequestString("Count_Stock1");
            sm.Count_Stock2 = RequestTool.RequestString("Count_Stock2");
            sm.Count_StockCaution1 = RequestTool.RequestString("Count_StockCaution1");
            sm.Count_StockCaution2 = RequestTool.RequestString("Count_StockCaution2");
            sm.Count_Views1 = RequestTool.RequestString("Count_Views1");
            sm.Count_Views2 = RequestTool.RequestString("Count_Views2");
            sm.Name = RequestTool.RequestString("Name");
            sm.Number = RequestTool.RequestString("Number");
            sm.Price1 = RequestTool.RequestString("Price1");
            sm.Price2 = RequestTool.RequestString("Price2");
            sm.Price_Cost1 = RequestTool.RequestString("Price_Cost1");
            sm.Price_Cost2 = RequestTool.RequestString("Price_Cost2");
            sm.Pro_Tag_id = RequestTool.RequestString("Pro_Tag_id");
            sm.Pro_Type_id = RequestTool.RequestString("Pro_Type_id");
            sm.Time_Add1 = RequestTool.RequestString("Time_Add1");
            sm.Time_Add2 = RequestTool.RequestString("Time_Add2");
            sm.Time_OnSale1 = RequestTool.RequestString("Time_OnSale1");
            sm.Time_OnSale2 = RequestTool.RequestString("Time_OnSale2");
            sm.Type_id_ProductStatus = RequestTool.RequestString("Type_id_ProductStatus");
            sm.Type_id_ProductType = RequestTool.RequestString("Type_id_ProductType");
            sm.Site_ids = RequestTool.RequestString("Site_ids");
            sm.Supplier_id = RequestTool.RequestString("Supplier_id");
            sm.IsSupplierTransport = RequestTool.RequestString("IsSupplierTransport");
            sm.Supplier_ProductType_ids = RequestTool.RequestString("Supplier_ProductType_ids");
            sm.lbsql_Time_Add1 = RequestTool.RequestDate("Time_Add1");
            sm.lbsql_Time_Add2 = RequestTool.RequestDate("Time_Add2");
            sm.lbsql_Time_OnSale1 = RequestTool.RequestDate("Time_OnSale1");
            sm.lbsql_Time_OnSale2 = RequestTool.RequestDate("Time_OnSale2");
            sm.iscombo = RequestTool.RequestString("iscombo");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            //string json = jss.Serialize(su);
            //admin.SearchUser = json;
            //B_Lebi_Administrator.Update(admin);
            return sm;
        }
        /// <summary>
        /// 生成查询条件文字
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public string Model_DES(SearchProductModel sm, string lang)
        {
            string str = "";
            if (sm.Name != "")
            {
                str += "<span class=\"searchname\">" + Language.Tag("名称", lang) + "：</span><span class=\"searchvalue\">" + sm.Name + "</span> ";
            }
            if (sm.Number != "")
            {
                str += "<span class=\"searchname\">" + Language.Tag("商品编号", lang) + "：</span><span class=\"searchvalue\">" + sm.Number + "</span> ";
            }
            if (sm.Type_id_ProductType != "")
            {
                List<Lebi_Type> ul = B_Lebi_Type.GetList("id in (lbsql{" + sm.Type_id_ProductType + "})", "");
                string lstr = "";
                foreach (Lebi_Type l in ul)
                {
                    if (lstr == "")
                        lstr = Language.Tag(l.Name, lang);
                    else
                        lstr += "," + Language.Tag(l.Name, lang);
                }
                str += "<span class=\"searchname\">" + Language.Tag("商品类型", lang) + "：</span><span class=\"searchvalue\">" + lstr + "</span> ";
            }
            if (sm.Type_id_ProductStatus != "")
            {
                List<Lebi_Type> ul = B_Lebi_Type.GetList("id in (lbsql{" + sm.Type_id_ProductStatus + "})", "");
                string lstr = "";
                foreach (Lebi_Type l in ul)
                {
                    if (lstr == "")
                        lstr = Language.Tag(l.Name, lang);
                    else
                        lstr += "," + Language.Tag(l.Name, lang);
                }
                str += "<span class=\"searchname\">" + Language.Tag("商品状态", lang) + "：</span><span class=\"searchvalue\">" + lstr + "</span> ";
            }
            if (sm.Brand_id != "")
            {
                Lebi_Brand bar = B_Lebi_Brand.GetModel("id=" + sm.Brand_id + "");
                if (bar != null)
                    str += "<span class=\"searchname\">" + Language.Tag("品牌", lang) + "：</span><span class=\"searchvalue\">" + Language.Content(bar.Name, lang) + "</span> ";
            }
            if (sm.Count_Sales1 != "" || sm.Count_Sales2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("销量", lang) + "：</span><span class=\"searchvalue\">" + sm.Count_Sales1 + "-" + sm.Count_Sales2 + "</span> ";
            if (sm.Count_Stock1 != "" || sm.Count_Stock2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("库存", lang) + "：</span><span class=\"searchvalue\">" + sm.Count_Stock1 + "-" + sm.Count_Stock2 + "</span> ";
            if (sm.Count_StockCaution1 != "" || sm.Count_StockCaution2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("预警库存", lang) + "：</span><span class=\"searchvalue\">" + sm.Count_StockCaution1 + "-" + sm.Count_StockCaution2 + "</span> ";
            if (sm.Count_Views1 != "" || sm.Count_Views2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("浏览量", lang) + "：</span><span class=\"searchvalue\">" + sm.Count_Views1 + "-" + sm.Count_Views2 + "</span> ";
            if (sm.Price1 != "" || sm.Price2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("价格", lang) + "：</span><span class=\"searchvalue\">" + sm.Price1 + "-" + sm.Price2 + "</span> ";
            if (sm.Price_Cost1 != "" || sm.Price_Cost2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("成本价", lang) + "：</span><span class=\"searchvalue\">" + sm.Price_Cost1 + "-" + sm.Price_Cost2 + "</span> ";

            if (sm.Pro_Tag_id != "")
            {
                string temp = "";
                foreach (Lebi_Pro_Tag t in B_Lebi_Pro_Tag.GetList("id in (lbsql{" + sm.Pro_Tag_id + "})", ""))
                {
                    if (temp == "")
                        temp = Language.Content(t.Name, lang);
                    else
                        temp += "," + Language.Content(t.Name, lang);
                }
                str += "<span class=\"searchname\">" + Language.Tag("商品标签", lang) + "：</span><span class=\"searchvalue\">" + temp + "</span> ";
            }
            if (sm.Time_Add1 != "" || sm.Time_Add2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("添加时间", lang) + "：</span><span class=\"searchvalue\">" + sm.Time_Add1 + "-" + sm.Time_Add2 + "</span> ";
            if (sm.Time_OnSale1 != "" || sm.Time_OnSale2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("上架时间", lang) + "：</span><span class=\"searchvalue\">" + sm.Time_OnSale1 + "-" + sm.Time_OnSale2 + "</span> ";
            if (sm.Pro_Type_id != "")
            {
                if (sm.Pro_Type_id == "-1")
                {
                    str += "<span class=\"searchname\">" + Language.Tag("商品分类", lang) + "：</span><span class=\"searchvalue\">" + Language.Tag("无分类", lang) + "</span> ";
                }
                else
                {
                    string temp = "";
                    foreach (Lebi_Pro_Type t in B_Lebi_Pro_Type.GetList("id in (lbsql{" + sm.Pro_Type_id + "})", ""))
                    {
                        if (temp == "")
                            temp = Language.Content(t.Name, lang);
                        else
                            temp += "," + Language.Content(t.Name, lang);
                    }
                    str += "<span class=\"searchname\">" + Language.Tag("商品分类", lang) + "：</span><span class=\"searchvalue\">" + temp + "</span> ";
                }
            }
            if (sm.Supplier_ProductType_ids != "")
            {
                string temp = "";
                foreach (Lebi_Supplier_ProductType t in B_Lebi_Supplier_ProductType.GetList("id in (lbsql{" + sm.Supplier_ProductType_ids + "})", ""))
                {
                    if (temp == "")
                        temp = Language.Content(t.Name, lang);
                    else
                        temp += "," + Language.Content(t.Name, lang);
                }
                if (sm.Supplier_ProductType_ids == "0")
                    temp += Language.Tag("未分组", lang);
                str += "<span class=\"searchname\">" + Language.Tag("自定义类别", lang) + "：</span><span class=\"searchvalue\">" + temp + "</span> ";
            }
            if (sm.Site_ids != "")
            {
                string temp = "";
                foreach (Lebi_Site t in B_Lebi_Site.GetList("id in (lbsql{" + sm.Site_ids + "})", ""))
                {
                    if (temp == "")
                        temp = t.SubName;
                    else
                        temp += "," + t.SubName;
                }
                str += "<span class=\"searchname\">" + Language.Tag("站点", lang) + "：</span><span class=\"searchvalue\">" + temp + "</span> ";
            }
            if (sm.Supplier_id != "")
            {
                Lebi_Supplier bar = B_Lebi_Supplier.GetModel("id=" + sm.Supplier_id + "");
                if (bar != null)
                    str += "<span class=\"searchname\">" + Language.Tag("供应商", lang) + "：</span><span class=\"searchvalue\">" + Language.Content(bar.Name, lang) + "</span> ";
            }
            if (sm.IsSupplierTransport != "")
            {
                str += "<span class=\"searchname\">" + Language.Tag("发货方", lang) + "：</span><span class=\"searchvalue\">";
                if (sm.IsSupplierTransport == "0")
                {
                    str += Language.Tag("商城", lang);
                }
                else
                {
                    str += Language.Tag("供应商", lang);
                }
                str += "</span> ";
            }
            if (sm.iscombo != "")
            {

                str += "<span class=\"searchname\">" + Language.Tag("组合商品", lang) + "：</span><span class=\"searchvalue\">" + (sm.iscombo == "0" ? "No" : "Yes") + "</span> ";
            }
            return str;
        }
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public string Model_SQL(SearchProductModel sm)
        {
            string sql = "";
            if (sm.Brand_id != "" && sm.Brand_id != "0")
                sql += " and Brand_id =" + sm.Brand_id + "";
            if (sm.Count_Sales1 != "")
                sql += " and Count_Sales >=" + sm.Count_Sales1 + "";
            if (sm.Count_Sales2 != "")
                sql += " and Count_Sales <=" + sm.Count_Sales2 + "";
            if (sm.Count_Stock1 != "")
                sql += " and Count_Stock >=" + sm.Count_Stock1 + "";
            if (sm.Count_Stock2 != "")
                sql += " and Count_Stock <=" + sm.Count_Stock2 + "";
            if (sm.Count_StockCaution1 != "")
                sql += " and Count_StockCaution >=" + sm.Count_StockCaution1 + "";
            if (sm.Count_StockCaution2 != "")
                sql += " and Count_StockCaution <=" + sm.Count_StockCaution2 + "";
            if (sm.Count_Views1 != "")
                sql += " and Count_Views >=" + sm.Count_Views1 + "";
            if (sm.Count_Views2 != "")
                sql += " and Count_Views <=" + sm.Count_Views2 + "";
            if (sm.Name != "")
                sql += " and Name like lbsql{'%" + sm.Name + "%'}";
            if (sm.Number != "")
                sql += " and Number like lbsql{'%" + sm.Number + "%'}";
            if (sm.Price_Cost1 != "")
                sql += " and Price_Cost >=" + sm.Price_Cost1 + "";
            if (sm.Price_Cost2 != "")
                sql += " and Price_Cost <=" + sm.Price_Cost2 + "";
            if (sm.Price1 != "")
                sql += " and Price >=" + sm.Price1 + "";
            if (sm.Price2 != "")
                sql += " and Price >=" + sm.Price2 + "";

            if (sm.Pro_Tag_id != "")
            {
                string[] tags = sm.Pro_Tag_id.Split(',');

                string wheretag = "";
                sql += " and (";
                foreach (string tag in tags)
                {
                    if (wheretag == "")
                        wheretag += " ','+Pro_Tag_id+',' like lbsql{'%" + tag + "%'}";
                    else
                        wheretag += " or ','+Pro_Tag_id+',' like lbsql{'%" + tag + "%'}";
                }
                sql += wheretag + ")";

            }
            if (sm.Pro_Type_id != "")
            {
                if (sm.Pro_Type_id == "-1")
                {
                    sql += " and Pro_Type_id = 0";
                }
                else
                {
                    string wheretype = "";
                    if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "sqlserver")
                    {
                        wheretype += " or Charindex('," + sm.Pro_Type_id + ",',','+Pro_Type_id_other+',')>0";
                    }
                    else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "access")
                    {
                        wheretype += " or Instr(','+Pro_Type_id_other+',','," + sm.Pro_Type_id + ",')>0";
                    }
                    else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "mysql")
                    {
                        wheretype += " or instr(','+Pro_Type_id_other+',','," + sm.Pro_Type_id + ",')>0";
                    }
                    if (sm.Brand_id != "")
                        sql += " and Pro_Type_id in (lbsql{" + sm.Pro_Type_id + "})";
                    else
                        sql += " and (Pro_Type_id in (" + Shop.Bussiness.EX_Product.Categoryid(sm.Pro_Type_id) + ")" + wheretype + ")";
                }
            }
            if (sm.Supplier_ProductType_ids != "" && sm.Supplier_ProductType_ids != "0")
                sql += " and ','+Supplier_ProductType_ids+',' like lbsql{'%," + sm.Supplier_ProductType_ids + ",%'}";
            if (sm.Supplier_ProductType_ids == "0")
                sql += " and Supplier_ProductType_ids =''";
            if (sm.Time_Add1 != "")
                sql += " and Time_Add >='" + sm.Time_Add1 + "'";
            if (sm.Time_Add2 != "")
                sql += " and Time_Add <='" + sm.Time_Add2 + " 23:59:59'";
            if (sm.Time_OnSale1 != "")
                sql += " and Time_OnSale >='" + sm.Time_OnSale1 + "'";
            if (sm.Time_OnSale2 != "")
                sql += " and Time_OnSale <='" + sm.Time_OnSale2 + " 23:59:59'";
            if (sm.Type_id_ProductStatus != "")
                sql += " and Type_id_ProductStatus in (lbsql{" + sm.Type_id_ProductStatus + "})";
            if (sm.Type_id_ProductType != "")
                sql += " and Type_id_ProductType in (lbsql{" + sm.Type_id_ProductType + "})";
            if (sm.Site_ids != "")
            {
                string[] tags = sm.Site_ids.Split(',');
                string sonwhere = "";
                sql += " and (";
                foreach (string tag in tags)
                {
                    if (sonwhere == "")
                        sonwhere = "','+Site_ids+',' like lbsql{'%," + tag + ",%'}";
                    else
                        sonwhere += " or ','+Site_ids+',' like lbsql{'%," + tag + ",%'}";
                }
                sql += sonwhere + ")";

            }
            if (sm.Supplier_id != "" && sm.Supplier_id != "0")
                sql += " and Supplier_id =" + sm.Supplier_id + "";
            if (sm.IsSupplierTransport != "")
                sql += " and IsSupplierTransport =" + sm.IsSupplierTransport + "";
            if (sm.iscombo != "")
            {
                if (sm.iscombo == "0")
                    sql += " and (IsCombo!=1 or IsCombo is null)";
                else
                    sql += " and IsCombo =1";
            }

            return sql;
        }

        /// <summary>
        /// 生成查询条件地址栏参数
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public string Model_URL(SearchProductModel sm)
        {
            string str = "";
            if (sm.Brand_id != "")
                str += "&Brand_id=" + sm.Brand_id + "";
            if (sm.Count_Sales1 != "")
                str += "&Count_Sales1=" + sm.Count_Sales1 + "";
            if (sm.Count_Sales2 != "")
                str += "&Count_Sales2=" + sm.Count_Sales2 + "";
            if (sm.Count_Stock1 != "")
                str += "&Count_Stock1=" + sm.Count_Stock1 + "";
            if (sm.Count_Stock2 != "")
                str += "&Count_Stock2=" + sm.Count_Stock2 + "";
            if (sm.Count_StockCaution1 != "")
                str += "&Count_StockCaution1=" + sm.Count_StockCaution1 + "";
            if (sm.Count_StockCaution2 != "")
                str += "&Count_StockCaution2=" + sm.Count_StockCaution2 + "";
            if (sm.Count_Views1 != "")
                str += "&Count_Views1=" + sm.Count_Views1 + "";
            if (sm.Count_Views2 != "")
                str += "&Count_Views2=" + sm.Count_Views2 + "";
            if (sm.Name != "")
                str += "&Name=" + sm.Name + "";
            if (sm.Number != "")
                str += "&Number=" + sm.Number + "";
            if (sm.Price_Cost1 != "")
                str += "&Price_Cost1=" + sm.Price_Cost1 + "";
            if (sm.Price_Cost2 != "")
                str += "&Price_Cost2=" + sm.Price_Cost2 + "";
            if (sm.Price1 != "")
                str += "&Price1=" + sm.Price1 + "";
            if (sm.Price2 != "")
                str += "&Price2=" + sm.Price2 + "";
            if (sm.Pro_Tag_id != "")
                str += "&Pro_Tag_id=" + sm.Pro_Tag_id + "";
            if (sm.Pro_Type_id != "")
                str += "&Pro_Type_id=" + sm.Pro_Type_id + "";
            if (sm.Supplier_ProductType_ids != "")
                str += "&Supplier_ProductType_ids=" + sm.Supplier_ProductType_ids + "";
            if (sm.Time_Add1 != "")
                str += "&Time_Add1=" + sm.Time_Add1 + "";
            if (sm.Time_Add2 != "")
                str += "&Time_Add2=" + sm.Time_Add2 + "";
            if (sm.Time_OnSale1 != "")
                str += "&Time_OnSale1=" + sm.Time_OnSale1 + "";
            if (sm.Time_OnSale2 != "")
                str += "&Time_OnSale2=" + sm.Time_OnSale2 + "";
            if (sm.Type_id_ProductStatus != "")
                str += "&Type_id_ProductStatus=" + sm.Type_id_ProductStatus + "";
            if (sm.Type_id_ProductType != "")
                str += "&Type_id_ProductType=" + sm.Type_id_ProductType + "";
            if (sm.Site_ids != "")
                str += "&Site_ids=" + sm.Site_ids + "";

            if (sm.iscombo != "")
            {
                str += "&iscombo=" + sm.iscombo + "";
            }
            if (str.IndexOf("&") == 0 && str.Length > 1)
            {
                str = str.TrimStart('&');
            }
            return str;
        }
    }
    /// <summary>
    /// 订单查询
    /// </summary>
    public class SearchOrder
    {
        public SearchOrderModel Model;
        public string Description;
        public string SQL;
        public string URL;
        public SearchOrder(Lebi_Administrator admin, string lang)
        {
            Model = Order_Set(admin);
            Description = Order_DES(Model, lang);
            SQL = Order_SQL(Model);
            URL = Order_URL(Model);
        }
        /// <summary>
        /// 保存查询参数
        /// </summary>
        public SearchOrderModel Order_Set(Lebi_Administrator admin)
        {
            SearchOrderModel su = new SearchOrderModel();
            su.Code = RequestTool.RequestString("Code");
            su.UserName = RequestTool.RequestString("UserName");
            su.User_id = RequestTool.RequestInt("User_id");
            su.T_Name = RequestTool.RequestString("T_Name");
            su.T_Address = RequestTool.RequestString("T_Address");
            su.T_MobilePhone = RequestTool.RequestString("T_MobilePhone");
            su.T_Email = RequestTool.RequestString("T_Email");
            su.IsSupplierCash = RequestTool.RequestString("IsSupplierCash");
            su.Supplier_id = RequestTool.RequestInt("Supplier_id");
            su.Time_Add1 = RequestTool.RequestString("Time_Add1");
            su.Time_Add2 = RequestTool.RequestString("Time_Add2");
            su.Type_id_OrderType = RequestTool.RequestInt("Type_id_OrderType");
            su.Money_Order1 = RequestTool.RequestDecimal("Money_Order1");
            su.Money_Order2 = RequestTool.RequestDecimal("Money_Order2");
            su.IsVerified = RequestTool.RequestString("IsVerified");
            su.IsPaid = RequestTool.RequestString("IsPaid");
            su.IsShipped = RequestTool.RequestString("IsShipped");
            su.IsShipped_All = RequestTool.RequestString("IsShipped_All");
            su.IsReceived = RequestTool.RequestString("IsReceived");
            su.IsReceived_All = RequestTool.RequestString("IsReceived_All");
            su.IsCompleted = RequestTool.RequestString("IsCompleted");
            su.IsInvalid = RequestTool.RequestString("IsInvalid");
            su.Product_id = RequestTool.RequestInt("Product_id");
            su.Remark_User = RequestTool.RequestString("Remark_User");
            su.Remark_Admin = RequestTool.RequestString("Remark_Admin");
            su.Product_Number = RequestTool.RequestString("Product_Number");
            JavaScriptSerializer jss = new JavaScriptSerializer();
            //string json = jss.Serialize(su);
            //admin.SearchUser = json;
            //B_Lebi_Administrator.Update(admin);
            return su;
        }
        /// <summary>
        /// 生成查询条件文字
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public string Order_DES(SearchOrderModel su, string lang)
        {
            string str = "";
            if (su.Type_id_OrderType > 0)
            {
                str += "<span class=\"searchname\">" + Language.Tag("类型", lang) + "：</span><span class=\"searchvalue\">" + Language.Tag(EX_Type.TypeName(su.Type_id_OrderType)) + "</span> ";
            }

            if (su.Code != "")
                str += "<span class=\"searchname\">" + Language.Tag("订单号", lang) + "：</span><span class=\"searchvalue\">" + su.Code + "</span> ";
            if (su.UserName != "")
                str += "<span class=\"searchname\">" + Language.Tag("用户", lang) + "：</span><span class=\"searchvalue\">" + su.UserName + "</span> ";
            if (su.User_id > 0)
            {
                Lebi_User user = B_Lebi_User.GetModel(su.User_id);
                if (user != null)
                    str += "<span class=\"searchname\">" + Language.Tag("用户", lang) + "：</span><span class=\"searchvalue\">" + user.UserName + "</span> ";
            }
            if (su.Product_id > 0)
            {
                Lebi_Product pro = B_Lebi_Product.GetModel(su.Product_id);
                if (pro != null)
                    str += "<span class=\"searchname\">" + Language.Tag("商品", lang) + "：</span><span class=\"searchvalue\">" + pro.Number + "</span> ";
            }
            if (su.Product_Number != "")
                str += "<span class=\"searchname\">" + Language.Tag("商品编号", lang) + "：</span><span class=\"searchvalue\">" + su.Product_Number + "</span> ";
            if (su.T_Name != "")
                str += "<span class=\"searchname\">" + Language.Tag("收货人", lang) + "：</span><span class=\"searchvalue\">" + su.T_Name + "</span> ";
            if (su.T_Address != "")
                str += "<span class=\"searchname\">" + Language.Tag("收货地址", lang) + "：</span><span class=\"searchvalue\">" + su.T_Address + "</span> ";
            if (su.T_MobilePhone != "")
                str += "<span class=\"searchname\">" + Language.Tag("收货电话", lang) + "：</span><span class=\"searchvalue\">" + su.T_MobilePhone + "</span> ";
            if (su.T_Email != "")
                str += "<span class=\"searchname\">" + Language.Tag("收货EMAIL", lang) + "：</span><span class=\"searchvalue\">" + su.T_Email + "</span> ";
            if (su.IsSupplierCash != "")
            {
                string s = Language.Tag("全部", lang);
                if (su.IsSupplierCash == "0")
                    s = Language.Tag("否", lang);
                if (su.IsSupplierCash == "1")
                    s = Language.Tag("是", lang);
                str += "<span class=\"searchname\">" + Language.Tag("商家收款", lang) + "：</span><span class=\"searchvalue\">" + s + "</span> ";
            }
            if (su.Supplier_id > 0)
            {
                Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(su.Supplier_id);
                if (supplier != null)
                {
                    str += "<span class=\"searchname\">" + Language.Tag("商家", lang) + "：</span><span class=\"searchvalue\">" + supplier.SubName + "</span> ";
                }
            }
            if (su.Time_Add1 != "" || su.Time_Add2 != "")
                str += "<span class=\"searchname\">" + Language.Tag("下单时间", lang) + "：</span><span class=\"searchvalue\">" + su.Time_Add1 + "-" + su.Time_Add2 + "</span> ";
            if (su.Money_Order1 != 0 || su.Money_Order2 != 0)
                str += "<span class=\"searchname\">" + Language.Tag("金额", lang) + "：</span><span class=\"searchvalue\">" + su.Money_Order1 + "-" + su.Money_Order2 + "</span> ";

            if (su.IsVerified != "")
            {
                string s = Language.Tag("全部", lang);
                if (su.IsVerified == "0")
                    s = Language.Tag("否", lang);
                if (su.IsVerified == "1")
                    s = Language.Tag("是", lang);
                str += "<span class=\"searchname\">" + Language.Tag("已确认", lang) + "：</span><span class=\"searchvalue\">" + s + "</span> ";
            }
            if (su.IsPaid != "")
            {
                string s = Language.Tag("全部", lang);
                if (su.IsPaid == "0")
                    s = Language.Tag("否", lang);
                if (su.IsPaid == "1")
                    s = Language.Tag("是", lang);
                str += "<span class=\"searchname\">" + Language.Tag("已付款", lang) + "：</span><span class=\"searchvalue\">" + s + "</span> ";
            }
            if (su.IsShipped != "")
            {
                string s = Language.Tag("全部", lang);
                if (su.IsShipped == "0")
                    s = Language.Tag("否", lang);
                if (su.IsShipped == "1")
                    s = Language.Tag("是", lang);
                str += "<span class=\"searchname\">" + Language.Tag("已发货", lang) + "：</span><span class=\"searchvalue\">" + s + "</span> ";
            }
            if (su.IsShipped_All != "")
            {
                string s = Language.Tag("全部", lang);
                if (su.IsShipped_All == "0")
                    s = Language.Tag("否", lang);
                if (su.IsShipped_All == "1")
                    s = Language.Tag("是", lang);
                str += "<span class=\"searchname\">" + Language.Tag("已全部发货", lang) + "：</span><span class=\"searchvalue\">" + s + "</span> ";
            }
            if (su.IsReceived != "")
            {
                string s = Language.Tag("全部", lang);
                if (su.IsReceived == "0")
                    s = Language.Tag("否", lang);
                if (su.IsReceived == "1")
                    s = Language.Tag("是", lang);
                str += "<span class=\"searchname\">" + Language.Tag("已收货", lang) + "：</span><span class=\"searchvalue\">" + s + "</span> ";
            }
            if (su.IsReceived_All != "")
            {
                string s = Language.Tag("全部", lang);
                if (su.IsReceived_All == "0")
                    s = Language.Tag("否", lang);
                if (su.IsReceived_All == "1")
                    s = Language.Tag("是", lang);
                str += "<span class=\"searchname\">" + Language.Tag("已全部收货", lang) + "：</span><span class=\"searchvalue\">" + s + "</span> ";
            }
            if (su.IsCompleted != "")
            {
                string s = Language.Tag("全部", lang);
                if (su.IsCompleted == "0")
                    s = Language.Tag("否", lang);
                if (su.IsCompleted == "1")
                    s = Language.Tag("是", lang);
                str += "<span class=\"searchname\">" + Language.Tag("已完结", lang) + "：</span><span class=\"searchvalue\">" + s + "</span> ";
            }
            if (su.IsInvalid != "")
            {
                string s = Language.Tag("全部", lang);
                if (su.IsInvalid == "0")
                    s = Language.Tag("否", lang);
                if (su.IsInvalid == "1")
                    s = Language.Tag("是", lang);
                str += "<span class=\"searchname\">" + Language.Tag("无效", lang) + "：</span><span class=\"searchvalue\">" + s + "</span> ";
            }
            if (su.Remark_User != "")
                str += "<span class=\"searchname\">" + Language.Tag("用户留言", lang) + "：</span><span class=\"searchvalue\">" + su.Remark_User + "</span> ";
            if (su.Remark_Admin != "")
                str += "<span class=\"searchname\">" + Language.Tag("客服备注", lang) + "：</span><span class=\"searchvalue\">" + su.Remark_Admin + "</span> ";
            return str;
        }
        /// <summary>
        /// 生成查询条件
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public string Order_SQL(SearchOrderModel su)
        {
            string sql = "";
            if (su.Code != "")
                sql += " and Code like lbsql{'%" + su.Code + "%'}";
            if (su.User_id > 0)
                sql += " and User_id=" + su.User_id + "";
            if (su.UserName != "")
                sql += " and (User_UserName like lbsql{'%" + su.UserName + "%'} or User_NickName like lbsql{'%" + su.UserName + "%'})";
            if (su.T_Name != "")
                sql += " and T_Name like lbsql{'%" + su.T_Name + "%'}";
            if (su.T_Address != "")
                sql += " and T_Address like lbsql{'%" + su.T_Address + "%'}";
            if (su.T_MobilePhone != "")
                sql += " and T_MobilePhone like lbsql{'%" + su.T_MobilePhone + "%'}";
            if (su.T_Email != "")
                sql += " and T_Email like lbsql{'%" + su.T_Email + "%'}";
            if (su.IsSupplierCash != "")
                sql += " and IsSupplierCash in (" + su.IsSupplierCash + ")";
            if (su.Supplier_id > 0)
                sql += " and Supplier_id=" + su.Supplier_id + "";
            if (su.Time_Add1 != "")
                sql += " and Time_Add>='" + su.Time_Add1 + "'";
            if (su.Time_Add2 != "")
                sql += " and Time_Add<='" + su.Time_Add2 + " 23:59:59'";
            if (su.Type_id_OrderType > 0)
                sql += " and Type_id_OrderType=" + su.Type_id_OrderType + "";
            if (su.Money_Order1 != 0)
                sql += " and Money_Order>='" + su.Money_Order1 + "'";
            if (su.Money_Order2 != 0)
                sql += " and Money_Order<='" + su.Money_Order2 + "'";
            if (su.IsVerified != "")
                sql += " and IsVerified in (" + su.IsVerified + ")";
            if (su.IsPaid != "")
                sql += " and IsPaid in (" + su.IsPaid + ")";
            if (su.IsShipped != "")
                sql += " and IsShipped in (" + su.IsShipped + ")";
            if (su.IsShipped_All != "")
                sql += " and IsShipped_All in (" + su.IsShipped_All + ")";
            if (su.IsReceived != "")
                sql += " and IsReceived in (" + su.IsReceived + ")";
            if (su.IsReceived_All != "")
                sql += " and IsReceived_All in (" + su.IsReceived_All + ")";
            if (su.IsCompleted != "")
                sql += " and IsCompleted in (" + su.IsCompleted + ")";
            if (su.IsInvalid != "")
                sql += " and IsInvalid in (" + su.IsInvalid + ")";
            if (su.Remark_User != "")
                sql += " and Remark_User like lbsql{'%" + su.Remark_User + "%'}";
            if (su.Remark_Admin != "")
                sql += " and Remark_Admin like lbsql{'%" + su.Remark_Admin + "%'}";
            Lebi_Product pro = null;
            if (su.Product_id == 0)
            {
                if (su.Product_Number != "")
                    pro = B_Lebi_Product.GetModel("Number='" + su.Product_Number + "'");
            }
            else
            {
                pro = B_Lebi_Product.GetModel(su.Product_id);
            }
            if (pro != null)
            {
                if (pro.Product_id == 0)
                {
                    string pids = "";
                    List<Lebi_Product> ps = B_Lebi_Product.GetList("Product_id=" + pro.id + "", "");
                    if (ps.Count > 0)
                    {
                        foreach (Lebi_Product p in ps)
                        {
                            pids += p.id + ",";
                        }
                        pids = pids.TrimEnd(',');
                        sql += " and id in (select Order_id from Lebi_Order_Product where Product_id in (" + pids + "))";
                    }
                    else
                    {
                        sql += " and id in (select Order_id from Lebi_Order_Product where Product_id=" + pro.id + ")";
                    }
                }
                else
                {
                    sql += " and id in (select Order_id from Lebi_Order_Product where Product_id=" + pro.id + ")";
                }
            }

            return sql;
        }
        /// <summary>
        /// 生成查询条件地址栏参数
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public string Order_URL(SearchOrderModel su)
        {

            string sql = "";
            if (su.Code != "")
                sql += "&Code=" + su.Code + "";
            if (su.UserName != "")
                sql += "&UserName=" + su.UserName + "";
            if (su.User_id > 0)
                sql += "&User_id=" + su.User_id + "";
            if (su.T_Name != "")
                sql += "&T_Name=" + su.T_Name + "";
            if (su.T_Address != "")
                sql += "&T_Address=" + su.T_Address + "";
            if (su.T_MobilePhone != "")
                sql += "&T_MobilePhone=" + su.T_MobilePhone + "";
            if (su.T_Email != "")
                sql += "&T_Email=" + su.T_Email + "";
            if (su.IsSupplierCash != "")
                sql += "&IsSupplierCash=" + su.IsSupplierCash + "";
            if (su.Supplier_id > 0)
                sql += "&Supplier_id=" + su.Supplier_id + "";
            if (su.Time_Add1 != "")
                sql += "&Time_Add1=" + su.Time_Add1 + "";
            if (su.Time_Add2 != "")
                sql += "&Time_Add2=" + su.Time_Add2 + "";
            if (su.Type_id_OrderType > 0)
                sql += "&Type_id_OrderType=" + su.Type_id_OrderType + "";
            if (su.Money_Order1 > 0)
                sql += "&Money_Order1=" + su.Money_Order1 + "";
            if (su.Money_Order2 > 0)
                sql += "&Money_Order2=" + su.Money_Order2 + "";
            if (su.IsVerified != "")
                sql += "&IsVerified=" + su.IsVerified;
            if (su.IsPaid != "")
                sql += "&IsPaid=" + su.IsPaid;
            if (su.IsShipped != "")
                sql += "&IsShipped=" + su.IsShipped;
            if (su.IsShipped_All != "")
                sql += "&IsShipped_All=" + su.IsShipped_All;
            if (su.IsReceived != "")
                sql += "&IsReceived=" + su.IsReceived;
            if (su.IsReceived_All != "")
                sql += "&IsReceived_All=" + su.IsReceived_All;
            if (su.IsCompleted != "")
                sql += "&IsCompleted=" + su.IsCompleted;
            if (su.IsInvalid != "")
                sql += "&IsInvalid=" + su.IsInvalid;
            if (su.Product_id > 0)
                sql += "&Product_id=" + su.Product_id;
            if (su.Product_Number != "")
                sql += "&Product_Number=" + su.Product_Number;
            if (su.Remark_User != "")
                sql += "&Remark_User=" + su.Remark_User;
            if (su.Remark_Admin != "")
                sql += "&Remark_Admin=" + su.Remark_Admin;
            if (sql.IndexOf("&") == 0 && sql.Length > 1)
            {
                sql = sql.TrimStart('&');
            }
            return sql;
        }
    }
}

