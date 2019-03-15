using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Configuration;
using LB.Tools;
namespace LB.DataAccess
{
    public class MongoObj
    {
        public ObjectId _id { get; set; }

        /// <summary>
        /// 将对象属性转成字典
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, Object> ToMap()
        {
            Dictionary<String, Object> map = new Dictionary<string, object>();

            Type t = this.GetType();

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in pi)
            {
                MethodInfo mi = p.GetGetMethod();

                if (mi != null && mi.IsPublic)
                {
                    map.Add(p.Name, mi.Invoke(this, new Object[] { }));
                }
            }

            return map;
        }

        /// <summary>
        /// 将对象属性转成字典，并去掉字典中的_id.
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, Object> ToMapWithoutId()
        {
            var map = ToMap();

            if (map != null && map.Keys.Contains("_id"))
            {
                map.Remove("_id");
            }

            return map;
        }
    }
    public sealed class MongoDataBase
    {
        #region 获取数据库
        public IMongoDatabase DB;
        public string conn_str = "";
        public string conn_database = "";
        public IMongoDatabase GetMongoDatabase()
        {

            return GetMongoDatabase(conn_str, conn_database);
        }

        public IMongoDatabase GetMongoDatabase(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var dbExists = client.ListDatabases().ToList().Any(o => o.Elements.Any(p => "name".Equals(p.Name) && databaseName.Equals(p.Value.AsString)));
            if (!dbExists)
            {
                throw new Exception("所请求的数据库不存在！");
            }

            return client.GetDatabase(databaseName);
        }
        public bool ConnTest()
        {
            var client = new MongoClient(conn_str);
            var dbExists = client.ListDatabases().ToList().Any(o => o.Elements.Any(p => "name".Equals(p.Name) && conn_database.Equals(p.Value.AsString)));
            if (!dbExists)
            {
                return false;
            }

            return true;
        }
        public MongoDataBase(string connflag)
        {
            string conn = ConfigurationManager.ConnectionStrings[connflag].ToString();
            conn_str = conn.Split(';')[0];
            conn_database = conn.Split(';')[1].Split('=')[1];
            DB = GetMongoDatabase(conn_str, conn_database);
        }
        public MongoDataBase(string conn, string db)
        {
            conn_str = conn;
            conn_database = db;
            DB = GetMongoDatabase(conn_str, conn_database);
        }
        #endregion
        #region 静态实例
        //private static MongoDB FB_;
        //public static MongoDB FB
        //{
        //    get {
        //        if (FB_ == null)
        //            FB_ = new MongoDB("mongo_facebook");
        //        return FB_;
        //    }
        //}
    }

    #endregion

    public class MongoTool<T> where T : MongoObj
    {
        private IMongoDatabase database;
        private IMongoCollection<T> myCollection = null;

        public MongoTool(IMongoDatabase database_, string collectionName)
        {
            database = database_;
            myCollection = database.GetCollection<T>(collectionName);
        }

        //#region 获取集合

        //public IMongoCollection<T> GetMongoCollection<T>(string collectionName)
        //{
        //    var myCollection = GetMongoCollection<T>(connstr, conn_database, collectionName);

        //    return myCollection;
        //}

        //public IMongoCollection<T> GetMongoCollection<T>(string connectionString, string databaseName, string collectionName)
        //{
        //    //var database = GetMongoDatabase(connectionString, databaseName);

        //    //var collectionFilter = new BsonDocument("name", collectionName);
        //    //var collections = database.ListCollections(new ListCollectionsOptions { Filter = collectionFilter });
        //    //if (!collections.ToList().Any())
        //    //{
        //    //    throw new Exception("所请求的集合不存在！");
        //    //}
        //    var myCollection = database.GetCollection<T>(collectionName);
        //    return myCollection;
        //}
        //#endregion

        #region 新增



        public void InsertOne(T entity)
        {
            if (null == entity) return;
            myCollection.InsertOne(entity);
        }
        public void InsertMany(IEnumerable<T> entitys)
        {
            if (null == entitys) return;
            myCollection.InsertMany(entitys);
        }

        #endregion

        #region 修改

        public void UpdateOrCreateOne(T entity)
        {
            if ("000000000000000000000000".Equals(entity._id.ToString()))
            {
                InsertOne(entity);
            }
            else
            {
                UpdateOne(entity);
            }
        }

        /// <summary>
        /// 更新集合属性,支持复杂类型的集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <param name="collectionName"></param>
        /// <param name="entity"></param>
        public void UpdateOne(T entity) //where T : MongoObj
        {
            // var myCollection = GetMongoCollection<T>(connectionString, databaseName, collectionName);

            var filter = Builders<T>.Filter.Eq("_id", entity._id);
            var fieldList = GetUpdateDefinitions(entity);

            myCollection.UpdateOne(filter, Builders<T>.Update.Combine(fieldList));
        }

        #region 递归获取字段更新表达式

        private List<UpdateDefinition<T>> GetUpdateDefinitions(T entity)
        {
            var type = typeof(T);
            var fieldList = new List<UpdateDefinition<T>>();

            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                GenerateRecursion<T>(fieldList, property, property.GetValue(entity), entity, "");
            }

            return fieldList;
        }

        private void GenerateRecursion<TEntity>(
              List<UpdateDefinition<TEntity>> fieldList,
              PropertyInfo property,
              object propertyValue,
              TEntity item,
              string father)
        {
            //复杂类型
            if (property.PropertyType.IsClass && property.PropertyType != typeof(string) && propertyValue != null)
            {
                //集合
                if (typeof(IList).IsAssignableFrom(propertyValue.GetType()))
                {
                    foreach (var sub in property.PropertyType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {
                        if (sub.PropertyType.IsClass && sub.PropertyType != typeof(string))
                        {
                            var arr = propertyValue as IList;
                            if (arr != null && arr.Count > 0)
                            {
                                for (int index = 0; index < arr.Count; index++)
                                {
                                    foreach (var subInner in sub.PropertyType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                                    {
                                        if (string.IsNullOrWhiteSpace(father))
                                            GenerateRecursion(fieldList, subInner, subInner.GetValue(arr[index]), item, property.Name + "." + index);
                                        else
                                            GenerateRecursion(fieldList, subInner, subInner.GetValue(arr[index]), item, father + "." + property.Name + "." + index);
                                    }
                                }
                            }
                        }
                    }
                }
                //实体
                else
                {
                    foreach (var sub in property.PropertyType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                    {

                        if (string.IsNullOrWhiteSpace(father))
                            GenerateRecursion(fieldList, sub, sub.GetValue(propertyValue), item, property.Name);
                        else
                            GenerateRecursion(fieldList, sub, sub.GetValue(propertyValue), item, father + "." + property.Name);
                    }
                }
            }
            //简单类型
            else
            {
                if (property.Name != "_id")//更新集中不能有实体键_id
                {
                    if (string.IsNullOrWhiteSpace(father))
                        fieldList.Add(Builders<TEntity>.Update.Set(property.Name, propertyValue));
                    else
                        fieldList.Add(Builders<TEntity>.Update.Set(father + "." + property.Name, propertyValue));
                }
            }
        }

        /// <summary>
        /// 构建Mongo的更新表达式
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private List<UpdateDefinition<T>> GeneratorMongoUpdate<T>(T item)
        {
            var fieldList = new List<UpdateDefinition<T>>();
            foreach (var property in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                GenerateRecursion<T>(fieldList, property, property.GetValue(item), item, string.Empty);
            }
            return fieldList;
        }

        #endregion

        /// <summary>
        /// 更新指定条件的对象,更新结果为0，则新增一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="entity"></param>
        /// <param name="query"></param>
        public void UpdateAllOrCreateOne(T entity, Expression<Func<T, bool>> query)
        {
            var updateResult = UpdateAll(entity, query);
            if (updateResult.MatchedCount == 0)
            {
                InsertOne(entity);
            }
        }

        /// <summary>
        /// 更新所有对象更新为同一的对象值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="entity">更新对象。 </param>
        /// <param name="query">条件查询。 调用示例：o=> o.UserName == "TestUser" 等等</param>
        /// <returns></returns>
        //public UpdateResult UpdateAll( T entity, Expression<Func<T, bool>> query) 
        //{
        //    return UpdateAll(entity, query);
        //}

        /// <summary>
        /// 更新所有对象更新为同一的对象值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <param name="collectionName"></param>
        /// <param name="entity">更新对象。 </param>
        /// <param name="query">条件查询。 调用示例：o=> o.UserName == "TestUser" 等等</param>
        /// <returns></returns>
        public UpdateResult UpdateAll(T entity, Expression<Func<T, bool>> query = null)
        {
            var fieldList = GetUpdateDefinitions(entity);
            return myCollection.UpdateMany<T>(query, Builders<T>.Update.Combine(fieldList));
        }

        #endregion

        #region 删除

        //public void Delete(MongoObj entity)
        //{
        //    Delete(entity._id);
        //}

        //public void Delete(ObjectId _id)
        //{
        //    var filter = Builders<T>.Filter.Eq("_id", _id);
        //    myCollection.DeleteOne(filter);
        //}

        public void Delete(Expression<Func<T, bool>> query)
        {
            if (query != null)
            {
                myCollection.DeleteManyAsync<T>(query);
            }
        }

        //public void Delete<T1>(Expression<Func<T, bool>> query)
        //{


        //    if (query != null)
        //    {
        //        myCollection.DeleteManyAsync<T>(query);
        //    }
        //}

        #endregion

        #region 获取单条信息
        public T FindOne(string _id)
        {

            ObjectId id;
            if (!ObjectId.TryParse(_id, out id)) return default(T);

            var filter = Builders<T>.Filter.Eq("_id", id);
            return myCollection.Find<T>(filter).First();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <param name="collectionName"></param>
        /// <param name="query">条件查询。 调用示例：o=> o.UserName == "username" 等等</param>
        /// <returns></returns>
        public T FindOne(Expression<Func<T, bool>> query)
        {

            T result = default(T);
            if (query != null)
            {
                result = myCollection.Find<T>(query).FirstOrDefault();
            }
            return result;

        }

        #endregion

        public List<T> FindALL(Expression<Func<T, bool>> query)
        {
            List<T> result = new List<T>();
            if (query != null)
            {
                result = myCollection.Find<T>(query).ToList();
            }
            return result;

        }
        public List<T> FindALL(Expression<Func<T, bool>> query, int pagesize, int pageindex)
        {
           
            List<T> result = new List<T>();
            if (query != null)
            {
                result = myCollection.Find<T>(query).Skip(pageindex * pagesize).Limit(pagesize).ToList();
            }
            return result;

        }
        public long Count(Expression<Func<T, bool>> query)
        {

            long result = 0;
            if (query != null)
            {
                //result = myCollection.Count<T>(query);
                result = myCollection.Find<T>(query).ToList().Count();
            }
            return result;

        }
    }


    /// <summary>
    /// mongodb的封装类的拓展方法。
    /// </summary>
    public static class MongoRepositoryExt
    {
        /// <summary>
        /// 获取结合的首条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static T FirstOne<T>(this IMongoCollection<T> collection)
        {
            return collection.AsQueryable().Take(1).FirstOrDefault();
        }
    }
}