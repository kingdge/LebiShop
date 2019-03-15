using System;
namespace Shop.Model
{
    [Serializable]
    public class Lebi_Table
    {
        #region Model
        private int _id = 0;
        private string _name = "";
        private string _type = "";
        private int _char_length = 0;
        private int _numeric_length = 0;
        private int _numeric_scale = 0;
        private int _isidentity = 0;
        private int _ispk = 0;
        private string _defaultval = "";
        private int _isnullable = 0;
        private int _parentid = 0;
        private string _parentname = "";
        private string _remark = "";
        private Lebi_Table _model;
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
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int char_length
        {
            set { _char_length = value; }
            get { return _char_length; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int numeric_length
        {
            set { _numeric_length = value; }
            get { return _numeric_length; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int numeric_scale
        {
            set { _numeric_scale = value; }
            get { return _numeric_scale; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int isidentity
        {
            set { _isidentity = value; }
            get { return _isidentity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ispk
        {
            set { _ispk = value; }
            get { return _ispk; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string defaultval
        {
            set { _defaultval = value; }
            get { return _defaultval; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int isnullable
        {
            set { _isnullable = value; }
            get { return _isnullable; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int parentid
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string parentname
        {
            set { _parentname = value; }
            get { return _parentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion

    }
}

