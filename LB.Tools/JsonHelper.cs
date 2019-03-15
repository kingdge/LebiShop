
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using System.Web.Script.Serialization;

namespace LB.Tools
{
    public class JsonHelper
    {
        public static string ToJson(object obj)
        {
            string json = "";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            json = jss.Serialize(obj);
            return json;
        }

        public static object ToObject(string con, Type t)
        {
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //try
            //{
            //   var  model = jss.Deserialize<t.GetType>(con);
            //    return model;

            //}
            //catch (Exception)
            //{
            //    return null;
            //}
            return null;
        }
    }
}