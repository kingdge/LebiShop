using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel;
using System.Data.Common;

namespace LB.DataAccess
{
    public class LBParameter : DbParameter
    {
        public LBParameter() { }
        public LBParameter(string parameterName, DbType dbType)
        {
            DbType = dbType;
            ParameterName=parameterName;
        }
        public LBParameter(string parameterName, object value)
        {
            ParameterName = parameterName;
            Value = value;
        }
        //public LBParameter(string parameterName, DbType dbType, int size);
        //public LBParameter(string parameterName, DbType dbType, int size, string sourceColumn);
        //public LBParameter(string parameterName, DbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value);

        public override DbType DbType { get; set; }
        [Category("Data")]
        public override ParameterDirection Direction { get; set; }
        [Browsable(false)]
        public override bool IsNullable { get; set; }
        [Category("Data")]
        [DbProviderSpecificTypeProperty(true)]
       
        public override string ParameterName { get; set; }
       
        [Category("Data")]
        public byte Precision { get; set; }
        [Category("Data")]
        public byte Scale { get; set; }
        [Category("Data")]
        public override int Size { get; set; }
        [Category("Data")]
        public override string SourceColumn { get; set; }
        public override bool SourceColumnNullMapping { get; set; }
        [Category("Data")]
        public override DataRowVersion SourceVersion { get; set; }
        [Category("Data")]
        [TypeConverter(typeof(StringConverter))]
        public override object Value { get; set; }


        public override void ResetDbType(){}
        //public override string ToString();
    }
}
