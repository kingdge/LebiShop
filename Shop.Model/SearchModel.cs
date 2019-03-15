using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Model
{
    #region 会员查询
    public class SearchUserModel
    {

        public SearchUserModel() { }
        public string UserName
        {
            get;
            set;
        }
        public string RealName
        {
            get;
            set;
        }
        public string NickName
        {
            get;
            set;
        }
        public string Sex
        {
            get;
            set;
        }
        public string Birthday1
        {
            get;
            set;
        }
        public string Birthday2
        {
            get;
            set;
        }
        public string Time_reg1
        {
            get;
            set;
        }
        public string Time_reg2
        {
            get;
            set;
        }
        public string Time_login1
        {
            get;
            set;
        }
        public string Time_login2
        {
            get;
            set;
        }
        public string MobilePhone
        {
            get;
            set;
        }
        public string Phone
        {
            get;
            set;
        }
        public string Fax
        {
            get;
            set;
        }
        public string QQ
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public string UserLevel_id
        {
            get;
            set;
        }
        public string Money1
        {
            get;
            set;
        }
        public string Money2
        {
            get;
            set;
        }
        public string Money_xiaofei1
        {
            get;
            set;
        }
        public string Money_xiaofei2
        {
            get;
            set;
        }
        public string Point1
        {
            get;
            set;
        }
        public string Point2
        {
            get;
            set;
        }
        public string Logins1
        {
            get;
            set;
        }
        public string Logins2
        {
            get;
            set;
        }
        public string Language
        {
            get;
            set;
        }
        public DateTime lbsql_Birthday1
        {
            get;
            set;
        }
        public DateTime lbsql_Birthday2
        {
            get;
            set;
        }
        public DateTime lbsql_Time_reg1
        {
            get;
            set;
        }
        public DateTime lbsql_Time_reg2
        {
            get;
            set;
        }
        public DateTime lbsql_Time_login1
        {
            get;
            set;
        }
        public DateTime lbsql_Time_login2
        {
            get;
            set;
        }
    }
    #endregion
    #region 卡券查询
    public class SearchCardModel
    {
        public SearchCardModel() { }
        public string Type_id_CardType
        {
            get;
            set;
        }
        public string Money
        {
            get;
            set;
        }
        public string Money_Used
        {
            get;
            set;
        }
        public string Code
        {
            get;
            set;
        }
        public string IndexCode
        {
            get;
            set;
        }
        public string number1
        {
            get;
            set;
        }
        public string number2
        {
            get;
            set;
        }
        public string Time_begin1
        {
            get;
            set;
        }
        public string Time_begin2
        {
            get;
            set;
        }
        public string Time_end1
        {
            get;
            set;
        }
        public string Time_end2
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string Money_Buy
        {
            get;
            set;
        }
        public string Type_id_CardStatus
        {
            get;
            set;
        }
        public DateTime lbsql_Time_begin1
        {
            get;
            set;
        }
        public DateTime lbsql_Time_begin2
        {
            get;
            set;
        }
        public DateTime lbsql_Time_end1
        {
            get;
            set;
        }
        public DateTime lbsql_Time_end2
        {
            get;
            set;
        }
    }
    #endregion
    #region 商品查询
    public class SearchProductModel
    {
        public SearchProductModel() { }
        public string Name
        {
            get;
            set;
        }
        public string Number
        {
            set;
            get;
        }
        public string Pro_Type_id
        {
            set;
            get;
        }
        public string Price1
        {
            set;
            get;
        }
        public string Price2
        {
            set;
            get;
        }
        /// <summary>
        /// 成本价
        /// </summary>
        public string Price_Cost1
        {
            set;
            get;
        }
        public string Price_Cost2
        {
            set;
            get;
        }
        public string Count_Stock1
        {
            set;
            get;
        }
        public string Count_Stock2
        {
            set;
            get;
        }
        /// <summary>
        /// 预警库存
        /// </summary>
        public string Count_StockCaution1
        {
            set;
            get;
        }
        public string Count_StockCaution2
        {
            set;
            get;
        }
        public string Brand_id
        {
            set;
            get;
        }
        public string Type_id_ProductStatus
        {
            set;
            get;
        }
        public string Type_id_ProductType
        {
            set;
            get;
        }
        public string Pro_Tag_id
        {
            set;
            get;
        }
        public string Time_OnSale1
        {
            get;
            set;
        }
        public string Time_OnSale2
        {
            get;
            set;
        }
        public string Time_Add1
        {
            get;
            set;
        }
        public string Time_Add2
        {
            get;
            set;
        }
        public string Count_Sales1
        {
            get;
            set;
        }
        public string Count_Sales2
        {
            get;
            set;
        }
        public string Count_Views1
        {
            get;
            set;
        }
        public string Count_Views2
        {
            get;
            set;
        }
        public string Site_ids
        {
            get;
            set;
        }
        public string Supplier_id
        {
            get;
            set;
        }
        public string IsSupplierTransport
        {
            get;
            set;
        }
        public string Supplier_ProductType_ids
        {
            get;
            set;
        }
        public DateTime lbsql_Time_OnSale1
        {
            get;
            set;
        }
        public DateTime lbsql_Time_OnSale2
        {
            get;
            set;
        }
        public DateTime lbsql_Time_Add1
        {
            get;
            set;
        }
        public DateTime lbsql_Time_Add2
        {
            get;
            set;
        }
        public string iscombo
        {
            get;
            set;
        }
    }
    #endregion

    #region 订单查询
    public class SearchOrderModel
    {
        public string Code { get; set; }
        public string UserName { get; set; }
        public int User_id { get; set; }
        public string T_Name { get; set; }
        public string T_Address { get; set; }
        public string T_MobilePhone { get; set; }
        public string T_Email { get; set; }
        public string Remark_User { get; set; }
        public string Remark_Admin { get; set; }
        public string IsSupplierCash { get; set; }
        public int Supplier_id { get; set; }
        public string Time_Add1 { get; set; }
        public string Time_Add2 { get; set; }
        public int Type_id_OrderType { get; set; }
        public decimal Money_Order1 { get; set; }
        public decimal Money_Order2 { get; set; }
        public string IsVerified { get; set; }
        public string IsPaid { get; set; }
        public string IsShipped { get; set; }
        public string IsShipped_All { get; set; }
        public string IsReceived { get; set; }
        public string IsReceived_All { get; set; }
        public string IsCompleted { get; set; }
        public string IsInvalid { get; set; }
        public int Product_id { get; set; }
        public string Product_Number { get; set; }
       
    }
    #endregion
}
