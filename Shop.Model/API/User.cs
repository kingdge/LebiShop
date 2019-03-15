using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Model
{
    [Serializable]
    public class apiUser
    {

        public apiUser()
        { }
        #region Model
        private int _id = 0;
        private string _username = "";
        private string _password = "";
        private string _email = "";
        private string _realname = "";
        private string _sex = "";
        private string _nickname = "";
        private DateTime _birthday = DateTime.Now;
        private decimal _point = 0;
        private string _city = "";
        private string _area = "";
        private string _address = "";
        private string _mobilephone = "";
        private string _phone = "";
        private string _qq = "";
        private string _fax = "";
        private string _postalcode = "";
        private string _msn = "";
        private decimal _money = 0;
        private decimal _money_xiaofei = 0;
        private int _count_login = 0;
        private string _ip_last = "";
        private string _ip_this = "";
        private DateTime _time_this = DateTime.Now;
        private DateTime _time_last = DateTime.Now;
        private DateTime _time_reg = DateTime.Now;
        private string _introduce = "";
        private DateTime _time_lastorder = DateTime.Now;
        private string _language = "";
        private string _currency_code = "";
        private string _face = "";
        private string _cashaccount_code = "";
        private string _cashaccount_name = "";
        private string _cashaccount_bank = "";
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RealName
        {
            set { _realname = value; }
            get { return _realname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NickName
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string City
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Area
        {
            set { _area = value; }
            get { return _area; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MobilePhone
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Postalcode
        {
            set { _postalcode = value; }
            get { return _postalcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Msn
        {
            set { _msn = value; }
            get { return _msn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Money
        {
            set { _money = value; }
            get { return _money; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Money_xiaofei
        {
            set { _money_xiaofei = value; }
            get { return _money_xiaofei; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Count_Login
        {
            set { _count_login = value; }
            get { return _count_login; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IP_Last
        {
            set { _ip_last = value; }
            get { return _ip_last; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string IP_This
        {
            set { _ip_this = value; }
            get { return _ip_this; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Time_This
        {
            set { _time_this = value; }
            get { return _time_this; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Time_Last
        {
            set { _time_last = value; }
            get { return _time_last; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Time_Reg
        {
            set { _time_reg = value; }
            get { return _time_reg; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Introduce
        {
            set { _introduce = value; }
            get { return _introduce; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Time_lastorder
        {
            set { _time_lastorder = value; }
            get { return _time_lastorder; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string Language
        {
            set { _language = value; }
            get { return _language; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public string Currency_Code
        {
            set { _currency_code = value; }
            get { return _currency_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Face
        {
            set { _face = value; }
            get { return _face; }
        }     
        /// <summary>
        /// 
        /// </summary>
        public string CashAccount_Code
        {
            set { _cashaccount_code = value; }
            get { return _cashaccount_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CashAccount_Name
        {
            set { _cashaccount_name = value; }
            get { return _cashaccount_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CashAccount_Bank
        {
            set { _cashaccount_bank = value; }
            get { return _cashaccount_bank; }
        }
        #endregion Model
    }

}
