using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Web;
using System.IO;

using Shop.Model;
using Shop.Tools;
using Shop.DataAccess;

namespace Shop.Bussiness
{
    public class BLLBase
    {
        //#region 数据库访问相关
        ////数据库链接字符串
        //private static string m_connectionstring;
        //public static string ConnectionString
        //{
        //    get
        //    {
        //        m_connectionstring = BaseConfigs.GetDBConnectStringFormat;

        //        return m_connectionstring;
        //    }
        //}

        ////数据库访问助手
        //private static SqlHelper _dbhelper;
        //public static SqlHelper DBHelper
        //{
        //    get
        //    {
        //        _dbhelper = new SqlHelper(ConnectionString);
        //        return _dbhelper;
        //    }
        //}
        //#endregion

        //#region DataTable 与 泛型List 互相转换

        ///// <summary>
        ///// DataTable 转 List
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="dt"></param>
        ///// <returns></returns>
        //public static List<T> DataTableToList<T>(DataTable dt)
        //{
        //    List<T> list = new List<T>();
        //    if (dt != null)
        //    {
        //        T t = default(T);
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            t = Activator.CreateInstance<T>();
        //            foreach (DataColumn column in dt.Columns)
        //            {
        //                PropertyInfo prop = t.GetType().GetProperty(column.ColumnName);
        //                try
        //                {
        //                    object value = dr[column.ColumnName];
        //                    if (value.ToString().Length > 0)
        //                        prop.SetValue(t, Convert.ChangeType(value, prop.PropertyType, CultureInfo.CurrentCulture), null);
        //                }
        //                catch (Exception ex)
        //                {
        //                    throw new Exception("DataTable至List数据转换出现异常." + ex.Message);
        //                }
        //            }
        //            list.Add(t);
        //        }
        //    }
        //    return list;
        //}




        ///// <summary>
        ///// 根据list 转 datatbale
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="list"></param>
        ///// <returns></returns>
        //public static DataTable ListToDataTable<T>(IList<T> list)
        //{
        //    DataTable table = CreateTable<T>();
        //    Type entityType = typeof(T);
        //    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

        //    foreach (T item in list)
        //    {
        //        DataRow row = table.NewRow();

        //        foreach (PropertyDescriptor prop in properties)
        //        {
        //            row[prop.Name] = prop.GetValue(item);
        //        }

        //        table.Rows.Add(row);
        //    }

        //    return table;
        //}
        //private static DataTable CreateTable<T>()
        //{
        //    Type entityType = typeof(T);
        //    DataTable table = new DataTable(entityType.Name);
        //    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
        //    foreach (PropertyDescriptor prop in properties)
        //    {
        //        table.Columns.Add(prop.Name, prop.PropertyType);
        //    }

        //    return table;
        //}
        //#endregion


        #region 分页获取数据
        ///// <summary>
        ///// 分页获取数据
        ///// </summary>
        ///// <param name="tableName">要分页显示的表名</param>
        ///// <param name="fieldKey">用于定位记录的主键(惟一键)字段,可以是逗号分隔的多个字段</param>
        ///// <param name="fieldShow">以逗号分隔的要显示的字段列表,如果不指定,则显示所有字段</param>
        ///// <param name="fieldOrder">以逗号分隔的排序字段列表,可以指定在字段后面指定DESC/ASC用于指定排序顺序</param>
        ///// <param name="where">查询条件</param>
        ///// <param name="pageSize">每页的大小(记录数)</param>
        ///// <param name="page">要显示的页码</param>
        ///// <returns></returns>
        //public static DataTable GetPagesData(string tableName, string fieldKey, string fieldShow, string fieldOrder, string where, long pageSize, long page)
        //{
        //    string proc = "usp_CommonPagination";
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@TableName", SqlDbType.NVarChar,200),
        //            new SqlParameter("@FieldKey", SqlDbType.NVarChar,1000),
        //            new SqlParameter("@FieldShow", SqlDbType.NVarChar,1000),
        //            new SqlParameter("@FieldOrder", SqlDbType.NVarChar,1000),
        //            new SqlParameter("@Where", SqlDbType.NVarChar,1000),
        //            new SqlParameter("@PageSize", SqlDbType.Int,4),
        //            new SqlParameter("@page", SqlDbType.Int,4)};
        //    parameters[0].Value = tableName;
        //    parameters[1].Value = fieldKey;
        //    parameters[2].Value = fieldShow;
        //    parameters[3].Value = fieldOrder;
        //    parameters[4].Value = where;
        //    parameters[5].Value = pageSize;
        //    parameters[6].Value = page;
        //    return DBHelper.ExecuteDataTableProc(proc, parameters);
        //}


        ///// <summary>
        ///// 获取记录数
        ///// </summary>
        ///// <param name="tableName"></param>
        ///// <param name="where"></param>
        ///// <returns></returns>
        //public static int GetCount(string tableName, string where)
        //{
        //    string sql = "select count(*) from " + tableName;
        //    if (!string.IsNullOrEmpty(where))
        //    {
        //        sql += " where " + where;
        //    }
        //    return DBHelper.ExecuteScalarInt(sql, 0);
        //}

        ////搜索
        //public static List<T> GetPagesBySearch<T>(string where, int pageSize, int page, string orderby)
        //{
        //    Type entityType = typeof(T);
        //    string tableName = entityType.Name;// t.GetType().Name;
        //    return DataTableToList<T>(GetPagesData(tableName, tableName + "_ID", "", orderby, where, pageSize, page));
        //}

        ///// <summary>
        ///// 获取当前页面分页大小
        ///// </summary>
        ///// <returns></returns>
        //public static int GetPageSize()
        //{
        //    return TypeParseHelper.StrToInt(CookieUtils.GetCookieString("pagesize"), 20);
        //}


        ///// <summary>
        ///// 分页LIST
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="list"></param>
        ///// <param name="currentPage"></param>
        ///// <param name="pageSize"></param>
        ///// <returns></returns>
        //public static List<T> GetPageList<T>(List<T> list, int currentPage, int pageSize)
        //{
        //    List<T> listResult = new List<T>();
        //    if (currentPage < 1)
        //        currentPage = 1;

        //    int beginIndex = (currentPage - 1) * pageSize;
        //    int endIndex = currentPage * pageSize - 1;

        //    if (endIndex > list.Count - 1)
        //    {
        //        //超过最大
        //        endIndex = list.Count - 1;
        //    }
        //    if (beginIndex <= endIndex)
        //    {
        //        for (; beginIndex <= endIndex; beginIndex++)
        //        {
        //            listResult.Add(list[beginIndex]);
        //        }
        //    }
        //    return listResult;
        //}

        ///// <summary>
        ///// 计算出共有多少页
        ///// </summary>
        ///// <param name="pageSize"></param>
        ///// <param name="totalItemSize"></param>
        ///// <returns></returns>
        //public static int GetPageCount(int pageSize, int totalItemSize)
        //{

        //    int pageCount = totalItemSize / pageSize;
        //    if (totalItemSize % pageSize > 0)
        //    {

        //        pageCount += 1;
        //    }
        //    return pageCount;


        //}

        ///// <summary>
        ///// 获取分页 不允许用户自定义分页记录数量
        ///// </summary>
        ///// <param name="pageUrlName"></param>
        ///// <param name="page"></param>
        ///// <param name="pageTotalCount">总页数</param>
        ///// <returns></returns>
        ////public static string GetPaginationString(string pageUrlName, long page, long pageTotalCount)
        //public static string GetPaginationStringWithNoCustomer(string pageUrlName, long page, int pageSize, int recordCount)
        //{
        //    StringBuilder builder = new StringBuilder();

        //    string pageUrlFormat = WebHelper.GetRequestUrl();
        //    if (pageUrlFormat.IndexOf("?") == -1)
        //        pageUrlFormat += "?" + pageUrlName + "={0}";
        //    else
        //    {
        //        pageUrlFormat = WebHelper.GetRequestUrlNoParas() + "?";
        //        var qs = System.Web.HttpContext.Current.Request.QueryString.AllKeys;
        //        //bool hasOtherPara = false;
        //        foreach (var q in qs)
        //        {
        //            if (q != pageUrlName)
        //            {
        //                pageUrlFormat += q + "=" + WebHelper.GetQueryString(q) + "&";
        //                //hasOtherPara = true;
        //            }
        //        }
        //        pageUrlFormat += pageUrlName + "={0}";
        //    }

        //    long pageTotalCount = GetPageCount(pageSize, recordCount);
        //    if (pageTotalCount < 1)
        //    {
        //        pageTotalCount = 1;
        //    }
        //    long i = 1;

        //    if (page > pageTotalCount)
        //    {
        //        page = pageTotalCount;
        //    }
        //    else
        //    {
        //        if (page <= 1)
        //        {
        //            page = 1;
        //        }
        //    }
        //    if (pageTotalCount >= 1)
        //    {
        //        //指定跳转
        //        builder.Append("				<div class=\"right\">  \r\n");
        //        builder.Append("					<label class=\"pgText\">转到第<input autocomplete=\"off\" class=\"pgField\" onchange=\"PaginationGo('" + pageUrlFormat.Replace("{0}", "{_}") + "');\" id=\"txtPaginationpage\" type=\"text\" maxlength=\"4\" value=\"" + page + "\"/>页</label>  \r\n");

        //        //用户自定义页大小
        //        //builder.Append("<script language=\"javascript\">\r\n");
        //        //builder.Append("	//用户自定义页大小\r\n");
        //        //builder.Append("	function setCustomerPagesize(o)\r\n");
        //        //builder.Append("	{\r\n");
        //        //builder.Append("		var dat=new Date();\r\n");
        //        //builder.Append("		dat.setTime(dat.getTime()+365*24*3600*1000);\r\n");
        //        //builder.Append("		window.document.cookie=\"pagesize=\" + escape($(o).val())+\";expires=\"+ dat.toGMTString()+\";\";\r\n");
        //        //builder.Append("        window.location.href=window.location.href;");
        //        //builder.Append("	}\r\n");
        //        //builder.Append("    //初始用户自定义页大小\r\n");
        //        //builder.Append("    $(function() {\r\n");
        //        //builder.Append("        var p = getCookie(\"pagesize\");        \r\n");
        //        //builder.Append("        if (p != null) {\r\n");
        //        //builder.Append("            $(\"#ddlCustomerPageSize option[value='\" +p+ \"']\").attr(\"selected\",\"true\");\r\n");
        //        //builder.Append("        }\r\n");
        //        //builder.Append("    });\r\n");
        //        //builder.Append("</script>\r\n");
        //        //builder.Append("<select id=\"ddlCustomerPageSize\" onchange=\"setCustomerPagesize(this);\">\r\n");
        //        //builder.Append("	<option value=\"5\">5/页</option>\r\n");
        //        //builder.Append("	<option value=\"10\" selected>10/页</option>\r\n");
        //        //builder.Append("	<option value=\"20\">20/页</option>\r\n");
        //        //builder.Append("	<option value=\"50\">50/页</option>\r\n");
        //        //builder.Append("	<option value=\"100\">100/页</option>\r\n");
        //        //builder.Append("	<option value=\"200\">200/页</option>\r\n");
        //        //builder.Append("</select>\r\n");
        //        //快速定向脚本
        //        builder.Append("<script type=\"text/javascript\">  \r\n");
        //        builder.Append("					function PaginationGo(pageUrl)  \r\n");
        //        builder.Append("					{  \r\n");
        //        builder.Append("					  var page=document.getElementById(\"txtPaginationpage\").value;  \r\n");
        //        builder.Append("					  window.location=pageUrl.replace(\"{_}\",page);  \r\n");
        //        builder.Append("					}  \r\n");
        //        builder.Append("</script>  \r\n");
        //        // builder.Append(string.Format("					<button class=\"pgGo\" type=\"button\" onclick=\"PaginationGo('{0}')\">确定</button>  \r\n", pageUrlFormat.Replace("{0}", "{_}")));
        //        builder.Append("				</div>  \r\n");

        //        //分页代码
        //        builder.Append("				<div class=\"pg\">  \r\n");

        //        //统计数据
        //        long thisPageEndRow = pageSize * page > recordCount ? recordCount : pageSize * page;
        //        string s = "共" + recordCount + "条信息，";
        //        s += "当前显示第 " + (pageSize * (page - 1) + 1) + " - " + thisPageEndRow + " 条，";
        //        s += "共 " + (recordCount / pageSize + (recordCount % pageSize > 0 ? 1 : 0)) + " 页 ";
        //        if (recordCount == 0)
        //            s = "抱歉,暂时没有相关数据。";

        //        builder.Append(" <label class=\"left\">" + s + "</label>");
        //        if (page <= 1)
        //        {
        //            builder.Append("					<span class=\"pgDisabled\">上一页</span>  \r\n");
        //        }
        //        else
        //        {
        //            builder.Append("<a href=\" " + string.Format(pageUrlFormat, (page - 1).ToString()) + "\">上一页</a>");
        //        }

        //        if (pageTotalCount < 11)
        //        {
        //            for (i = 1; i <= pageTotalCount; i++)
        //            {
        //                if (i == page)
        //                {
        //                    builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", i));
        //                }
        //                else
        //                {
        //                    builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (page < 5)
        //            {
        //                for (i = 1; i <= 7; i++)
        //                {
        //                    if (i == page)
        //                    {
        //                        builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", i));
        //                    }
        //                    else
        //                    {
        //                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
        //                    }
        //                }
        //                builder.Append("<span>...</span>");

        //                if (page == pageTotalCount - 2)
        //                {
        //                    builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                }
        //                else
        //                {
        //                    builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
        //                }

        //                if (page == pageTotalCount - 1)
        //                {
        //                    builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                }
        //                else
        //                {
        //                    builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
        //                }


        //            }
        //            else
        //            {
        //                builder.Append("<a href=\"" + string.Format(pageUrlFormat, "1") + "\">1</a>");
        //                builder.Append("<a href=\"" + string.Format(pageUrlFormat, "2") + "\">2</a>");
        //                builder.Append("<span>...</span>");
        //                if (page > (pageTotalCount - 7))
        //                {

        //                    for (i = pageTotalCount - 7; i <= pageTotalCount - 3; i++)
        //                    {
        //                        if (i == page)
        //                        {
        //                            builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", i));
        //                        }
        //                        else
        //                        {
        //                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
        //                        }
        //                    }

        //                    if (page == pageTotalCount - 2)
        //                    {
        //                        builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                    }
        //                    else
        //                    {
        //                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
        //                    }

        //                    if (page == pageTotalCount - 1)
        //                    {
        //                        builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                    }
        //                    else
        //                    {
        //                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
        //                    }

        //                }
        //                else
        //                {
        //                    for (i = page - 2; i <= page + 2; i++)
        //                    {
        //                        if (i == page)
        //                        {
        //                            builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", i));
        //                        }
        //                        else
        //                        {
        //                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
        //                        }
        //                    }
        //                    builder.Append("<span>...</span>");

        //                    if (page == pageTotalCount - 2)
        //                    {
        //                        builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                    }
        //                    else
        //                    {
        //                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
        //                    }

        //                    if (page == pageTotalCount - 1)
        //                    {
        //                        builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                    }
        //                    else
        //                    {
        //                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
        //                    }

        //                }
        //            }
        //        }


        //        if (page < pageTotalCount)
        //        {
        //            builder.Append("<a class=\"next\" href=\" " + string.Format(pageUrlFormat, Convert.ToString(page + 1)) + "\">下一页</a>");
        //        }
        //        else if (page == pageTotalCount)
        //        {
        //            builder.Append(" <span class=\"pgDisabled\">下一页</span>");
        //        }
        //        builder.Append("				</div>  \r\n");


        //    }

        //    return builder.ToString();
        //}


        ///// <summary>
        ///// 获取分页
        ///// </summary>
        ///// <param name="pageUrlName"></param>
        ///// <param name="page"></param>
        ///// <param name="pageTotalCount">总页数</param>
        ///// <returns></returns>
        ////public static string GetPaginationString(string pageUrlName, long page, long pageTotalCount)
        //public static string GetPaginationString(string pageUrlName, int page, int pageSize, int recordCount)
        //{
        //    StringBuilder builder = new StringBuilder();

        //    string pageUrlFormat = WebHelper.GetRequestUrl();
        //    if (pageUrlFormat.IndexOf("?") == -1)
        //        pageUrlFormat += "?" + pageUrlName + "={0}";
        //    else
        //    {
        //        pageUrlFormat = WebHelper.GetRequestUrlNoParas() + "?";
        //        var qs = System.Web.HttpContext.Current.Request.QueryString.AllKeys;
        //        //bool hasOtherPara = false;
        //        foreach (var q in qs)
        //        {
        //            if (q.ToLower() != pageUrlName.ToLower())
        //            {
        //                pageUrlFormat += q + "=" + WebHelper.GetQueryString(q) + "&";
        //                //hasOtherPara = true;
        //            }
        //        }
        //        pageUrlFormat += pageUrlName + "={0}";
        //    }

        //    int pageTotalCount = GetPageCount(pageSize, recordCount);
        //    if (pageTotalCount < 1)
        //    {
        //        pageTotalCount = 1;
        //    }
        //    int i = 1;

        //    if (page > pageTotalCount)
        //    {
        //        page = pageTotalCount;
        //    }
        //    else
        //    {
        //        if (page <= 1)
        //        {
        //            page = 1;
        //        }
        //    }
        //    if (pageTotalCount >= 1)
        //    {
        //        //指定跳转
        //        builder.Append("				<div class=\"right\">  \r\n");
        //        builder.Append("					<label class=\"pgText\">转到第<input autocomplete=\"off\" class=\"pgField\" onchange=\"PaginationGo('" + pageUrlFormat.Replace("{0}", "{_}") + "');\" id=\"txtPaginationpage\" type=\"text\" maxlength=\"4\" value=\"" + page + "\"/>页</label>  \r\n");

        //        //用户自定义页大小
        //        builder.Append("<script language=\"javascript\">\r\n");
        //        builder.Append("	//用户自定义页大小\r\n");
        //        builder.Append("	function setCustomerPagesize(o)\r\n");
        //        builder.Append("	{\r\n");
        //        builder.Append("		var dat=new Date();\r\n");
        //        builder.Append("		dat.setTime(dat.getTime()+365*24*3600*1000);\r\n");
        //        builder.Append("		window.document.cookie=\"pagesize=\" + escape($(o).val())+\";expires=\"+ dat.toGMTString()+\";\";\r\n");
        //        builder.Append("        window.location.href=window.location.href;");
        //        builder.Append("	}\r\n");
        //        builder.Append("    //初始用户自定义页大小\r\n");
        //        builder.Append("    $(function() {\r\n");
        //        builder.Append("        var p = getCookie(\"pagesize\");        \r\n");
        //        builder.Append("        if (p != null) {\r\n");
        //        builder.Append("            $(\"#ddlCustomerPageSize option[value='\" +p+ \"']\").attr(\"selected\",\"true\");\r\n");
        //        builder.Append("        }\r\n");
        //        builder.Append("    });\r\n");
        //        builder.Append("</script>\r\n");
        //        builder.Append("<select id=\"ddlCustomerPageSize\" onchange=\"setCustomerPagesize(this);\">\r\n");
        //        builder.Append("	<option value=\"5\">5/页</option>\r\n");
        //        builder.Append("	<option value=\"10\" selected>10/页</option>\r\n");
        //        builder.Append("	<option value=\"20\">20/页</option>\r\n");
        //        builder.Append("	<option value=\"50\">50/页</option>\r\n");
        //        builder.Append("	<option value=\"100\">100/页</option>\r\n");
        //        builder.Append("	<option value=\"200\">200/页</option>\r\n");
        //        builder.Append("</select>\r\n");
        //        //快速定向脚本
        //        builder.Append("<script type=\"text/javascript\">  \r\n");
        //        builder.Append("					function PaginationGo(pageUrl)  \r\n");
        //        builder.Append("					{  \r\n");
        //        builder.Append("					  var page=document.getElementById(\"txtPaginationpage\").value;  \r\n");
        //        builder.Append("					  window.location=pageUrl.replace(\"{_}\",page);  \r\n");
        //        builder.Append("					}  \r\n");
        //        builder.Append("</script>  \r\n");
        //        // builder.Append(string.Format("					<button class=\"pgGo\" type=\"button\" onclick=\"PaginationGo('{0}')\">确定</button>  \r\n", pageUrlFormat.Replace("{0}", "{_}")));
        //        builder.Append("				</div>  \r\n");

        //        //分页代码
        //        builder.Append("				<div class=\"pg\">  \r\n");

        //        //统计数据
        //        long thisPageEndRow = pageSize * page > recordCount ? recordCount : pageSize * page;
        //        string s = "共" + recordCount + "条信息，";
        //        s += "当前显示第 " + (pageSize * (page - 1) + 1) + " - " + thisPageEndRow + " 条，";
        //        s += "共 " + (recordCount / pageSize + (recordCount % pageSize > 0 ? 1 : 0)) + " 页 ";
        //        if (recordCount == 0)
        //            s = "抱歉,暂时没有相关数据。";

        //        builder.Append(" <label class=\"left\">" + s + "</label>");
        //        if (page <= 1)
        //        {
        //            builder.Append("					<span class=\"pgDisabled\">上一页</span>  \r\n");
        //        }
        //        else
        //        {
        //            builder.Append("<a href=\" " + string.Format(pageUrlFormat, (page - 1).ToString()) + "\">上一页</a>");
        //        }

        //        if (pageTotalCount < 11)
        //        {
        //            for (i = 1; i <= pageTotalCount; i++)
        //            {
        //                if (i == page)
        //                {
        //                    builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", i));
        //                }
        //                else
        //                {
        //                    builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (page < 5)
        //            {
        //                for (i = 1; i <= 7; i++)
        //                {
        //                    if (i == page)
        //                    {
        //                        builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", i));
        //                    }
        //                    else
        //                    {
        //                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
        //                    }
        //                }
        //                builder.Append("<span>...</span>");

        //                if (page == pageTotalCount - 2)
        //                {
        //                    builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                }
        //                else
        //                {
        //                    builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
        //                }

        //                if (page == pageTotalCount - 1)
        //                {
        //                    builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                }
        //                else
        //                {
        //                    builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
        //                }


        //            }
        //            else
        //            {
        //                builder.Append("<a href=\"" + string.Format(pageUrlFormat, "1") + "\">1</a>");
        //                builder.Append("<a href=\"" + string.Format(pageUrlFormat, "2") + "\">2</a>");
        //                builder.Append("<span>...</span>");
        //                if (page > (pageTotalCount - 7))
        //                {

        //                    for (i = pageTotalCount - 7; i <= pageTotalCount - 3; i++)
        //                    {
        //                        if (i == page)
        //                        {
        //                            builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", i));
        //                        }
        //                        else
        //                        {
        //                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
        //                        }
        //                    }

        //                    if (page == pageTotalCount - 2)
        //                    {
        //                        builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                    }
        //                    else
        //                    {
        //                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
        //                    }

        //                    if (page == pageTotalCount - 1)
        //                    {
        //                        builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                    }
        //                    else
        //                    {
        //                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
        //                    }

        //                }
        //                else
        //                {
        //                    for (i = page - 2; i <= page + 2; i++)
        //                    {
        //                        if (i == page)
        //                        {
        //                            builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", i));
        //                        }
        //                        else
        //                        {
        //                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
        //                        }
        //                    }
        //                    builder.Append("<span>...</span>");

        //                    if (page == pageTotalCount - 2)
        //                    {
        //                        builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                    }
        //                    else
        //                    {
        //                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
        //                    }

        //                    if (page == pageTotalCount - 1)
        //                    {
        //                        builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                    }
        //                    else
        //                    {
        //                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
        //                    }

        //                }
        //            }
        //        }


        //        if (page < pageTotalCount)
        //        {
        //            builder.Append("<a class=\"next\" href=\" " + string.Format(pageUrlFormat, Convert.ToString(page + 1)) + "\">下一页</a>");
        //        }
        //        else if (page == pageTotalCount)
        //        {
        //            builder.Append(" <span class=\"pgDisabled\">下一页</span>");
        //        }
        //        builder.Append("				</div>  \r\n");


        //    }

        //    return builder.ToString();
        //}

        ///// <summary>
        ///// 全局附件列表分页
        ///// </summary>
        ///// <param name="pageUrlName"></param>
        ///// <param name="page"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="recordCount"></param>
        ///// <returns></returns>
        //public static string GetPaginationStringForGlobalDocList(string pageUrlFormat, long page, int pageSize, int recordCount)
        //{
        //    StringBuilder builder = new StringBuilder();
        //    long pageTotalCount = GetPageCount(pageSize, recordCount);
        //    if (pageTotalCount < 1)
        //    {
        //        pageTotalCount = 1;
        //    }
        //    long i = 1;

        //    if (page > pageTotalCount)
        //    {
        //        page = pageTotalCount;
        //    }
        //    else
        //    {
        //        if (page <= 1)
        //        {
        //            page = 1;
        //        }
        //    }
        //    if (pageTotalCount >= 1)
        //    {


        //        //分页代码
        //        builder.Append("				<div class=\"pg\">  \r\n");

        //        //统计数据
        //        long thisPageEndRow = pageSize * page > recordCount ? recordCount : pageSize * page;
        //        string s = "共" + recordCount + "条记录，";
        //        s += "当前显示第 " + (pageSize * (page - 1) + 1) + " - " + thisPageEndRow + " 条，";
        //        s += "共 " + (recordCount / pageSize + (recordCount % pageSize > 0 ? 1 : 0)) + " 页 ";
        //        if (recordCount == 0)
        //            s = "抱歉,暂时没有相关数据。";

        //        builder.Append(" <label class=\"left\">" + s + "</label>");
        //        if (page <= 1)
        //        {
        //            builder.Append("					<span class=\"pgDisabled\">上一页</span>  \r\n");
        //        }
        //        else
        //        {
        //            builder.Append("<a href=\" " + string.Format(pageUrlFormat, (page - 1).ToString()) + "\">上一页</a>");
        //        }

        //        if (pageTotalCount < 11)
        //        {
        //            for (i = 1; i <= pageTotalCount; i++)
        //            {
        //                if (i == page)
        //                {
        //                    builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", i));
        //                }
        //                else
        //                {
        //                    builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (page < 5)
        //            {
        //                for (i = 1; i <= 7; i++)
        //                {
        //                    if (i == page)
        //                    {
        //                        builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", i));
        //                    }
        //                    else
        //                    {
        //                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
        //                    }
        //                }
        //                builder.Append("<span>...</span>");

        //                if (page == pageTotalCount - 2)
        //                {
        //                    builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                }
        //                else
        //                {
        //                    builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
        //                }

        //                if (page == pageTotalCount - 1)
        //                {
        //                    builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                }
        //                else
        //                {
        //                    builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
        //                }


        //            }
        //            else
        //            {
        //                builder.Append("<a href=\"" + string.Format(pageUrlFormat, "1") + "\">1</a>");
        //                builder.Append("<a href=\"" + string.Format(pageUrlFormat, "2") + "\">2</a>");
        //                builder.Append("<span>...</span>");
        //                if (page > (pageTotalCount - 7))
        //                {

        //                    for (i = pageTotalCount - 7; i <= pageTotalCount - 3; i++)
        //                    {
        //                        if (i == page)
        //                        {
        //                            builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", i));
        //                        }
        //                        else
        //                        {
        //                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
        //                        }
        //                    }

        //                    if (page == pageTotalCount - 2)
        //                    {
        //                        builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                    }
        //                    else
        //                    {
        //                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
        //                    }

        //                    if (page == pageTotalCount - 1)
        //                    {
        //                        builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                    }
        //                    else
        //                    {
        //                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
        //                    }

        //                }
        //                else
        //                {
        //                    for (i = page - 2; i <= page + 2; i++)
        //                    {
        //                        if (i == page)
        //                        {
        //                            builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", i));
        //                        }
        //                        else
        //                        {
        //                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
        //                        }
        //                    }
        //                    builder.Append("<span>...</span>");

        //                    if (page == pageTotalCount - 2)
        //                    {
        //                        builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                    }
        //                    else
        //                    {
        //                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
        //                    }

        //                    if (page == pageTotalCount - 1)
        //                    {
        //                        builder.Append(string.Format("<strong class=\"pgCurr\">{0}</strong>", page));
        //                    }
        //                    else
        //                    {
        //                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
        //                    }

        //                }
        //            }
        //        }


        //        if (page < pageTotalCount)
        //        {
        //            builder.Append("<a class=\"next\" href=\" " + string.Format(pageUrlFormat, Convert.ToString(page + 1)) + "\">下一页</a>");
        //        }
        //        else if (page == pageTotalCount)
        //        {
        //            builder.Append(" <span class=\"pgDisabled\">下一页</span>");
        //        }
        //        builder.Append("				</div>  \r\n");


        //    }

        //    return builder.ToString();
        //}

        #endregion


        public static object CreateInstance(string entityName)
        {

            return null;
        }
        public static Type GetObjectType(string entityName)
        {
            Type type = Type.GetType(string.Format("CZ.Model.{1},CZ.Model", "", entityName), false, true);
            
            return type;
        }
        public static Type GetBLLObjectType(string entityName)
        {
            Type type = Type.GetType(string.Format("CZ.Bussiness.B_{1},CZ.Bussiness", "", entityName), false, true);
            return type;
        }

        /// <summary>
        /// 反射方法更新数据
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static bool ObjectEdit(string entityName)
        {
            bool res = false;
            bool addflag = false;
            int id = RequestTool.RequestInt("id", 0);
            try
            {
                Type bll = GetBLLObjectType(entityName);
                MethodInfo getmodel = bll.GetMethod("GetModel");
                var model = getmodel.Invoke(bll, new object[] { id });
                if (model == null)
                {
                    addflag = true;
                    Type tmodel = GetObjectType(entityName);
                    model = Activator.CreateInstance(tmodel);
                }
                MethodInfo bindform = bll.GetMethod("BindForm");
                bindform.Invoke(bll, new object[] { model });
                if (addflag)
                {
                    MethodInfo add = bll.GetMethod("Add");
                    add.Invoke(bll, new object[] { model });
                }
                else
                {
                    MethodInfo add = bll.GetMethod("Update");
                    add.Invoke(bll, new object[] { model });
                }
                res = true;
            }
            catch
            {
                res = false;
            }
            return res;
        }
        /// <summary>
        /// 反射方法删除数据
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static bool ObjectDel(string entityName)
        {
            bool res = false;
            string id = RequestTool.RequestString("id");
            try
            {
                Type bll = GetBLLObjectType(entityName);
                MethodInfo del = bll.GetMethod("Delete");
                del.Invoke(bll, new object[] { "id in (" + id + ")" });
                res = true;
            }
            catch
            {
                res = false;
            }
            return res;
        }

    }
}
