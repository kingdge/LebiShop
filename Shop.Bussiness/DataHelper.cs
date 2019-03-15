using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Reflection;
using Shop.Model;
using Shop.Tools;
using Shop.DataAccess;

namespace Shop.Bussiness
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    public class DataHelper : System.Web.UI.Page
    {

        private static string Ref;
        public DataHelper()
        {
        }

        #region 反射方式的数据更新方法
        public static string Execute(string Target, string KeyName, string KeyValue, string Action,bool IsAdmin)
        {
            Ref = RequestTool.GetUrlReferrerNoParas();
            string Msg = "";
            Action = Action.ToLower();
            if (Msg == "")
            {
                switch (Action)
                {
                    case "add":
                        Msg = Save(Target, KeyName, KeyValue, Action, IsAdmin);
                        break;
                    case "update":
                        Msg = Save(Target, KeyName, KeyValue, Action, IsAdmin);
                        break;
                    case "del":
                        Msg = Delete(Target, KeyName, KeyValue, IsAdmin);
                        break;
                }
            }
            return Msg;
        }

        public static string Execute(string Target, string KeyName, string KeyValue, string Action)
        {

            return Execute(Target, KeyName, KeyValue, Action, false);
        }
        public static string Execute(string Target, string KeyName, string Action)
        {

            return Execute(Target, KeyName, "", Action,false);
        }

        //删除对象
        public static string Delete(string TableName, string KeyName, string KeyValue, bool IsAdmin)
        {

            PropertyInfo property = null;
            List<string> ids = new List<string>();

            if (KeyValue != "")
            {
                ids = StringTool.SplitStringList(KeyValue, ",");
            }
            else
            {
                if (HttpContext.Current.Request[KeyName] != null)//提交了主键的相关值
                {
                    ids = StringTool.SplitStringList(RequestTool.GetFormString(KeyName), ",");
                }
                else
                {
                    return "参数不足";
                }
            }
            if (!string.IsNullOrEmpty(TableName) && ids.Count > 0)
            {
                Type modelType = BLLBase.GetObjectType(TableName);
                Type bllType = BLLBase.GetBLLObjectType(TableName);
                MethodInfo methodInfo = bllType.GetMethod("Delete");
                object Newobj = Activator.CreateInstance(bllType);
                if (methodInfo != null)
                {
                    object objid = null;
                    foreach (string id in ids)
                    {
                        property = modelType.GetProperty(KeyName);
                        if (property != null)
                        {
                            objid = GetTypeValue(property.PropertyType.Name, Convert.ToString(id));
                            //if (TableName != "CZ_Log" && IsAdmin)
                                //LOG.Instance.AddLog(TableName, id, "删除", "", Ref, 0);//添加操作记录
                        }
                        object[] paras = new object[] { objid };
                        var execute = methodInfo.Invoke(Newobj, paras);
                    }

                }
            }
            else
            {
                return "参数不足";
            }
            return "OK";
        }
        /// <summary>
        /// 保存对象
        /// </summary>
        /// <param name="KeyName">主键名</param>
        /// <param name="TableName">数据表名称</param>
        /// <returns></returns>
        public static string Save(string TableName, string KeyName, string KeyValue, string Action, bool IsAdmin)
        {
            string Msg = "";
            string Key = KeyValue;//主键ID
            Type bllType = BLLBase.GetBLLObjectType(TableName);
            MethodInfo methodInfo = null;
            Type modelType = BLLBase.GetObjectType(TableName);
            object Newobj = Activator.CreateInstance(bllType);
            PropertyInfo property = null;
            if (bllType == null || modelType == null)
            {
                Msg = "未找到合适的方法";

            }
            else
            {
                //-------------------------------------------------------------------------------

                bool FlagAdd = true;
                object model = null;

                object id = Key;

                if (Key == "")
                {
                    id = HttpContext.Current.Request[KeyName];
                    Key = (string)id;
                }


                if (Action == "update")//修改的方法
                {
                    //如果提交了主键值，就对MODEL赋值

                    methodInfo = bllType.GetMethod("GetModel");
                    if (methodInfo != null)
                    {
                        property = modelType.GetProperty(KeyName);
                        if (property != null)
                        {
                            id = GetTypeValue(property.PropertyType.Name, Convert.ToString(id));
                        }
                        model = methodInfo.Invoke(Newobj, new object[] { id });
                        FlagAdd = false;
                    }


                }
                else//if(Action == "Add")
                {
                    FlagAdd = true;
                }
                //------------------------------------------------------------------------------

                if (FlagAdd)
                    model = Activator.CreateInstance(modelType);
                if (model == null)
                {
                    Msg = "数据异常，未能绑定数据实体";
                }
                else
                {
                    //Set New value
                    var postData = HttpContext.Current.Request.Form.AllKeys;

                    foreach (var paras in postData)
                    {
                        property = modelType.GetProperty(paras);
                        if (property != null)
                        {

                            object v = GetFormTypeValue(property.PropertyType.Name, paras);
                            property.SetValue(model, Convert.ChangeType(v, property.PropertyType, CultureInfo.CurrentCulture), null);

                        }
                    }
                    if (FlagAdd)
                    {
                        methodInfo = bllType.GetMethod("Add");

                        object[] saveParas = new object[] { model };
                        var execute = methodInfo.Invoke(Newobj, saveParas);

                        string newid = Common.GetValue("select Max(" + KeyName + ") from " + TableName + "");//取得最大的ID当做新记录的ID
                        Key = newid;
                        //if (IsAdmin)
                        //LOG.Instance.AddLog(TableName, newid, "添加", "", Ref, 0);//添加操作记录
                    }
                    else
                    {
                        methodInfo = bllType.GetMethod("Update");
                        object[] saveParas = new object[] { model };
                        var execute = methodInfo.Invoke(Newobj, saveParas);
                        //if (IsAdmin)
                        //LOG.Instance.AddLog(TableName, id.ToString(), "更新", "", Ref, 0);//添加操作记录
                    }



                    Msg = "OK";
                }

            }
            Msg = "{err:'" + Msg + "',ID:'" + Key + "'}";
            return Msg;
        }


        /// <summary>
        /// 根据MODEL的数据类型，对参数进行相应转化
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        private static object GetTypeValue(string typeName, string Value)
        {
            typeName = typeName.ToLower();
            object v = null;
            switch (typeName)
            {
                case "int32":
                    v = TypeParseHelper.StrToInt(Value, 0);
                    break;
                case "int64":
                    v = TypeParseHelper.StrToLong(Value, 0);
                    break;
                case "boolean":
                    v = TypeParseHelper.StrToBool(Value, false);
                    break;
                case "decimal":
                    v = TypeParseHelper.StrToDecimal(Value, 0);
                    break;
                case "float":
                    v = TypeParseHelper.StrToFloat(Value, 0);
                    break;
                case "datetime":
                    v = TypeParseHelper.StrToDateTime(Value, DateTime.MinValue);
                    break;
                default:
                    v = Convert.ToString(Value);
                    break;
            }
            return v;
        }
        /// <summary>
        /// 根据MODEL的数据类型，获取参数，对参数进行相应转化
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        private static object GetFormTypeValue(string typeName, string paras)
        {
            typeName = typeName.ToLower();
            object v = null;
            switch (typeName)
            {
                case "int32":
                    v = RequestTool.GetFormInt(paras, 0);
                    break;
                case "int64":
                    v = RequestTool.GetFormLong(paras, 0);
                    break;
                case "boolean":
                    v = RequestTool.RequestBool(paras);
                    break;
                case "decimal":
                    v = RequestTool.GetFormDecimal(paras, 0);
                    break;
                case "float":
                    v = RequestTool.GetFormFloat(paras, 0);
                    break;
                case "datetime":
                    v = RequestTool.GetFormDataTime(paras, DateTime.MinValue);
                    //v = RequestTool.GetERPDataTime(paras, DateTime.MinValue);

                    break;
                default:
                    v = RequestTool.GetFormString(paras);
                    break;
            }
            return v;
        }
        #endregion
    }

}