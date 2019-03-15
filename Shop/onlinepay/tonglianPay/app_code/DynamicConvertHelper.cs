using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace tonglianPay
{
    public class DynamicConvertHelper
    {
        public static dynamic ConvertJson(string json)
        {
            return ConvertJson(json, true);
        }
        /// <summary>
        /// Json转化为动态变量
        /// </summary>
        /// <param name="json">json字符段</param>
        /// <param name="isConvertChild">是否转子节点</param>
        /// <returns></returns>
        public static dynamic ConvertJson(string json, bool isConvertChild = true)
        {
            if ((json + "").Length == 0) return new DynamicJsonObject();

            var jss = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };
            jss.RegisterConverters(isConvertChild
                ? new JavaScriptConverter[] { new DynamicJsonConverter() }
                : new JavaScriptConverter[] { new DynamicJsonConverter2() });
            var dy = jss.Deserialize(json, typeof(object)) as dynamic;
            return dy;
        }

        public class DynamicJsonConverter : JavaScriptConverter
        {
            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                if (dictionary == null)
                    throw new ArgumentNullException("dictionary");

                return type == typeof(object) ? new DynamicJsonObject(dictionary) : null;
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override IEnumerable<Type> SupportedTypes
            {
                get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(object) })); }
            }
        }
        public class DynamicJsonObject : DynamicObject
        {
            private IDictionary<string, object> Dictionary { get; set; }
            public DynamicJsonObject()
            {
                this.Dictionary = new Dictionary<string, object>();
            }
            public DynamicJsonObject(IDictionary<string, object> dictionary)
            {
                this.Dictionary = dictionary;
            }
            /// <summary>
            /// 移除节点
            /// </summary>
            /// <param name="name"></param>
            public void RemoveElement(string name)
            {
                if (Dictionary.ContainsKey(name))
                    Dictionary.Remove(name);
            }

            public IDictionary<string, object> GetDictionary()
            {
                return Dictionary;
            }

            //public string ToJsonString()
            //{
            //    return JsonUtil.GetJson(Dictionary);
            //}

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                if (this.Dictionary.ContainsKey(binder.Name))
                {
                    this.Dictionary[binder.Name] = value;
                }
                else
                {
                    this.Dictionary.Add(binder.Name, value);
                }
                return true;
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {

                if (this.Dictionary == null || !this.Dictionary.TryGetValue(binder.Name, out result))
                {
                    var dictionary = this.Dictionary;
                    if (dictionary != null) dictionary.Add(binder.Name, "");
                    result = "";
                    return true;
                }

                if (result is IDictionary<string, object>)
                {
                    result = new DynamicJsonObject(result as IDictionary<string, object>);
                }
                else if (result is ArrayList && (result as ArrayList) is IDictionary<string, object>)
                {
                    result =
                        new List<DynamicJsonObject>(
                            (result as ArrayList).ToArray()
                                .Select(x => new DynamicJsonObject(x as IDictionary<string, object>)));
                }
                else if (result is ArrayList)
                {
                    result = new List<DynamicJsonObject>(
                            (result as ArrayList).ToArray()
                                .Select(x => new DynamicJsonObject(x as IDictionary<string, object>)));
                }
                else if (result is int)
                {
                    result = result.ToString();
                }

                return this.Dictionary.ContainsKey(binder.Name);
            }
        }

        public class DynamicJsonConverter2 : JavaScriptConverter
        {
            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                if (dictionary == null)
                    throw new ArgumentNullException("dictionary");

                return type == typeof(object) ? new DynamicJsonObject2(dictionary) : null;
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override IEnumerable<Type> SupportedTypes
            {
                get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(object) })); }
            }
        }
        public class DynamicJsonObject2 : DynamicObject
        {
            private IDictionary<string, object> Dictionary { get; set; }
            public DynamicJsonObject2()
            {
                this.Dictionary = new Dictionary<string, object>();
            }
            public DynamicJsonObject2(IDictionary<string, object> dictionary)
            {
                this.Dictionary = dictionary;
            }
            /// <summary>
            /// 移除节点
            /// </summary>
            /// <param name="name"></param>
            public void RemoveElement(string name)
            {
                if (Dictionary.ContainsKey(name))
                    Dictionary.Remove(name);
            }

            public IDictionary<string, object> GetDictionary()
            {
                return Dictionary;
            }

            //public string ToJsonString()
            //{
            //    return JsonUtil.GetJson(Dictionary);
            //}

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                if (this.Dictionary.ContainsKey(binder.Name))
                {
                    this.Dictionary[binder.Name] = value;
                }
                else
                {
                    this.Dictionary.Add(binder.Name, value);
                }
                return true;
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {

                if (this.Dictionary == null || !this.Dictionary.TryGetValue(binder.Name, out result))
                {
                    var dictionary = this.Dictionary;
                    if (dictionary != null) dictionary.Add(binder.Name, "");
                    result = "";
                    return true;
                }

                if (result is IDictionary<string, object>)
                {
                    result = result as IDictionary<string, object>;
                }
                else if (result is ArrayList && (result as ArrayList) is IDictionary<string, object>)
                {
                    result =
                        new List<IDictionary<string, object>>(
                            (result as ArrayList).ToArray()
                                .Select(x => x as IDictionary<string, object>));
                }
                else if (result is ArrayList)
                {
                    result =
                        new List<IDictionary<string, object>>(
                            (result as ArrayList).ToArray()
                                .Select(x => x as IDictionary<string, object>));
                }
                else if (result is int)
                {
                    result = result.ToString();
                }

                return this.Dictionary.ContainsKey(binder.Name);
            }
        }
    }
}
