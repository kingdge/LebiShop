using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Text.RegularExpressions;

using Shop.Model;using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;

namespace Shop.Bussiness
{
    public class Pager2
    {

        public static int GetPageCount(int pageSize, int totalItemSize)
        {

            int pageCount = totalItemSize / pageSize;
            if (totalItemSize % pageSize > 0)
            {

                pageCount += 1;
            }
            return pageCount;


        }
        #region 后台分页
        /// <summary>
        /// 分页控件，不显示数据条数
        /// </summary>
        /// <param name="pageUrlFormat">翻页时需要的参数</param>
        /// <param name="page">当前页</param>
        /// <param name="pageTotalCount">总页数</param>
        /// <returns></returns>
        public static string GetPaginationString(string pageUrlFormat, int page, int pageTotalCount)
        {
            string lang = Language.CurrentLanguageFlag();
            StringBuilder builder = new StringBuilder();

            if (pageTotalCount < 1)
            {
                pageTotalCount = 1;
            }
            int i = 1;

            if (page > pageTotalCount)
            {
                page = pageTotalCount;
            }
            else
            {
                if (page <= 1)
                {
                    page = 1;
                }
            }
            if (pageTotalCount > 1)
            {
                /*
                builder.Append("				<div class=\"right\">  \r\n");
                builder.Append(string.Format("					<label class=\"pgText\">" + Language.Tag("转到") + "<input class=\"pgField\" id=\"txtPaginationpage\" type=\"text\" maxlength=\"3\" value=\"{0}\"/>" + Language.Tag("页") + "</label>  \r\n", page));
                builder.Append("<script type=\"text/javascript\">  \r\n");
                builder.Append("					function PaginationGo(pageUrl)  \r\n");
                builder.Append("					{  \r\n");
                builder.Append("					  var page=document.getElementById(\"txtPaginationpage\").value;  \r\n");
                builder.Append("					  window.location=pageUrl.replace(\"{_}\",page);  \r\n");
                builder.Append("					}  \r\n");
                builder.Append("</script>  \r\n");
                builder.Append(string.Format("					<button class=\"pgGo\" type=\"button\" onclick=\"PaginationGo('{0}')\">" + Language.Tag("确定") + "</button>  \r\n", pageUrlFormat.Replace("{0}", "{_}")));
                builder.Append("				</div>  \r\n");
                */
                builder.Append("<nav aria-label=\"Page navigation\"><ul class=\"pagination\">");
                if (page <= 1)
                {
                    builder.Append("<li class=\"page-item disabled\"><a class=\"page-link\" href=\"#\" aria-label=\"Previous\"><span aria-hidden=\"true\">«</span><span class=\"sr-only\">" + Language.Tag("上一页", lang) + "</span></a></li>");
                }
                else
                {
                    builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\" " + string.Format(pageUrlFormat, (page - 1).ToString()) + "\" aria-label=\"Previous\"><span aria-hidden=\"true\">«</span><span class=\"sr-only\">" + Language.Tag("上一页", lang) + "</span></a></li>");
                }

                if (pageTotalCount < 11)
                {
                    for (i = 1; i <= pageTotalCount; i++)
                    {
                        if (i == page)
                        {
                            builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", i));
                        }
                        else
                        {
                            builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a></li>");
                        }
                    }
                }
                else
                {
                    if (page < 5)
                    {
                        for (i = 1; i <= 7; i++)
                        {
                            if (i == page)
                            {
                                builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", i));
                            }
                            else
                            {
                                builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a></li>");
                            }
                        }
                        builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"#\">...</a></li>");

                        if (page == pageTotalCount - 2)
                        {
                            builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                        }
                        else
                        {
                            builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a></li>");
                        }

                        if (page == pageTotalCount - 1)
                        {
                            builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                        }
                        else
                        {
                            builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a></li>");
                        }
                        if (page == pageTotalCount)
                        {
                            builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                        }
                        else
                        {
                            builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount).ToString()) + "\">" + (pageTotalCount).ToString() + "</a></li>");
                        }

                    }
                    else
                    {
                        builder.Append("<li class=\"page-item\"><a class=\"page-link\" ref=\"" + string.Format(pageUrlFormat, "1") + "\">1</a></li>");
                        builder.Append("<li class=\"page-item\"><a class=\"page-link\" ref=\"" + string.Format(pageUrlFormat, "2") + "\">2</a></li>");
                        builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"#\">...</a></li>");
                        if (page > (pageTotalCount - 7))
                        {

                            for (i = pageTotalCount - 7; i <= pageTotalCount - 3; i++)
                            {
                                if (i == page)
                                {
                                    builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", i));
                                }
                                else
                                {
                                    builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a></li>");
                                }
                            }
                            if (page == pageTotalCount - 2)
                            {
                                builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                            }
                            else
                            {
                                builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a></li>");
                            }

                            if (page == pageTotalCount - 1)
                            {
                                builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                            }
                            else
                            {
                                builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a></li>");
                            }
                            if (page == pageTotalCount)
                            {
                                builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                            }
                            else
                            {
                                builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount).ToString()) + "\">" + (pageTotalCount).ToString() + "</a></li>");
                            }

                        }
                        else
                        {
                            for (i = page - 2; i <= page + 2; i++)
                            {
                                if (i == page)
                                {
                                    builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", i));
                                }
                                else
                                {
                                    builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a></li>");
                                }
                            }
                            builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"#\">...</a></li>");

                            if (page == pageTotalCount - 2)
                            {
                                builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                            }
                            else
                            {
                                builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a></li>");
                            }

                            if (page == pageTotalCount - 1)
                            {
                                builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                            }
                            else
                            {
                                builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a></li>");
                            }
                            if (page == pageTotalCount)
                            {
                                builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                            }
                            else
                            {
                                builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount).ToString()) + "\">" + (pageTotalCount).ToString() + "</a></li>");
                            }

                        }
                    }
                }
                if (page < pageTotalCount)
                {
                    builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\" " + string.Format(pageUrlFormat, (page + 1).ToString()) + "\" aria-label=\"Next\"><span aria-hidden=\"true\">»</span><span class=\"sr-only\">" + Language.Tag("下一页", lang) + "</span></a></li>");
                }
                else if (page == pageTotalCount)
                {
                    builder.Append("<li class=\"page-item\"><a class=\"page-link\" aria-label=\"Next\"><span aria-hidden=\"true\">»</span><span class=\"sr-only\">" + Language.Tag("下一页", lang) + "</span></a></li>");
                }
                builder.Append("</ul></nav>");
            }
            return builder.ToString();
        }

        /// <summary>
        ///  /// 分页控件，显示数据条数
        /// </summary>
        /// <param name="pageUrlFormat">翻页时需要的参数</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageTotalItemSize">总数据条数</param>
        /// <returns></returns>
        public static string GetPaginationString(string pageUrlFormat, int page, int pageSize, int pageTotalItemSize)
        {
            string lang = Language.AdminCurrentLanguage().Code;
            int pageTotalCount = GetPageCount(pageSize, pageTotalItemSize);
            StringBuilder builder = new StringBuilder();
            if (LB.Tools.CookieTool.GetCookieString("pageSize") != "")
            {
                pageSize = int.Parse(LB.Tools.CookieTool.GetCookieString("pageSize"));
            }
            if (pageSize > 100)
            {
                pageSize = 20;
            }
            if (pageTotalCount < 1)
            {
                pageTotalCount = 1;
            }
            int i = 1;

            if (page > pageTotalCount)
            {
                page = pageTotalCount;
            }
            else
            {
                if (page <= 1)
                {
                    page = 1;
                }
            }
            //if (pageTotalCount > 1)
            //{
            /*
            builder.Append("				<div class=\"right\">  \r\n");
            builder.Append(string.Format("					<label class=\"pgText\">" + Language.Tag("转到") + "<input class=\"pgField\" id=\"txtPaginationpage\" type=\"text\" maxlength=\"3\" value=\"{0}\"/>" + Language.Tag("页") + "</label>  \r\n", page));
            builder.Append("<script type=\"text/javascript\">  \r\n");
            builder.Append("					function PaginationGo(pageUrl)  \r\n");
            builder.Append("					{  \r\n");
            builder.Append("					  var page=document.getElementById(\"txtPaginationpage\").value;  \r\n");
            builder.Append("					  window.location=pageUrl.replace(\"{_}\",page);  \r\n");
            builder.Append("					}  \r\n");
            builder.Append("</script>  \r\n");
            builder.Append(string.Format("					<button class=\"pgGo\" type=\"button\" onclick=\"PaginationGo('{0}')\">" + Language.Tag("确定") + "</button>  \r\n", pageUrlFormat.Replace("{0}", "{_}")));
            builder.Append("				</div>  \r\n");
            */
            builder.Append("<script type=\"text/javascript\">  \r\n");
            builder.Append("					function PageSizeGo(pageSize)  \r\n");
            builder.Append("					{  \r\n");
            builder.Append("					  SetCookie('pageSize', pageSize, 1);window.location='" + string.Format(pageUrlFormat, "1") + "';  \r\n");
            builder.Append("					}  \r\n");
            builder.Append("</script>  \r\n");
            builder.Append("<ul class=\"pagination pagination-sm justify-content-center\">");
            builder.Append("<li class=\"page-item m-r-10\" id=\"pagination-size\"><select class=\"form-control form-control-sm\"><option>" + pageSize + "</option><option value=\"20\" onchange=\"PageSizeGo(20);\">20</option><option value=\"40\" onchange=\"PageSizeGo(40);\">40</option><option value=\"60\" onchange=\"PageSizeGo(60);\">60</option><option value=\"80\" onchange=\"PageSizeGo(80);\">80</option><option value=\"100\" onchange=\"PageSizeGo(100);\">100</option></select></li>");
            builder.Append("<li class=\"page-item m-r-10 disabled\" id=\"pagination-total\"><a class=\"page-link\">" + Language.Tag("共", lang) + " " + pageTotalItemSize + "" + Language.Tag("条", lang) + "</a></li>");
            if (page <= 1)
            {
                builder.Append("<li class=\"page-item disabled\"><a class=\"page-link\" href=\"#\" aria-label=\"Previous\" title=\"" + Language.Tag("首页", lang) + "\">«</a></li>");
            }
            else
            {
                builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\" " + string.Format(pageUrlFormat, "1") + "\" aria-label=\"Previous\" title=\"" + Language.Tag("首页", lang) + "\">«</a></li>");
            }
            if (pageTotalCount < 11)
            {
                for (i = 1; i <= pageTotalCount; i++)
                {
                    if (i == page)
                    {
                        builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", i));
                    }
                    else
                    {
                        builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a></li>");
                    }
                }
            }
            else
            {
                if (page < 5)
                {
                    for (i = 1; i <= 7; i++)
                    {
                        if (i == page)
                        {
                            builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", i));
                        }
                        else
                        {
                            builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a></li>");
                        }
                    }
                    builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"#\">...</a></li>");

                    //if (page == pageTotalCount - 2)
                    //{
                    //    builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                    //}
                    //else
                    //{
                    //    builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a></li>");
                    //}

                    //if (page == pageTotalCount - 1)
                    //{
                    //    builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                    //}
                    //else
                    //{
                    //    builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a></li>");
                    //}
                    if (page == pageTotalCount)
                    {
                        builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                    }
                    else
                    {
                        builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount).ToString()) + "\">" + (pageTotalCount).ToString() + "</a></li>");
                    }
                }
                else
                {
                    //builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, "1") + "\">1</a></li>");
                    ////builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, "2") + "\">2</a></li>");
                    //builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"#\">...</a></li>");
                    if (page > (pageTotalCount - 7))
                    {

                        for (i = pageTotalCount - 7; i <= pageTotalCount - 3; i++)
                        {
                            if (i == page)
                            {
                                builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", i));
                            }
                            else
                            {
                                builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a></li>");
                            }
                        }
                        //if (page == pageTotalCount - 2)
                        //{
                        //    builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                        //}
                        //else
                        //{
                        //    builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a></li>");
                        //}

                        //if (page == pageTotalCount - 1)
                        //{
                        //    builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                        //}
                        //else
                        //{
                        //    builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a></li>");
                        //}
                        if (page == pageTotalCount)
                        {
                            builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                        }
                        else
                        {
                            builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount).ToString()) + "\">" + (pageTotalCount).ToString() + "</a></li>");
                        }
                    }
                    else
                    {
                        for (i = page - 3; i <= page + 3; i++)
                        {
                            if (i == page)
                            {
                                builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", i));
                            }
                            else
                            {
                                builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a></li>");
                            }
                        }
                        builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"#\">...</a></li>");
                        //if (page == pageTotalCount - 2)
                        //{
                        //    builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                        //}
                        //else
                        //{
                        //    builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a></li>");
                        //}

                        //if (page == pageTotalCount - 1)
                        //{
                        //    builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                        //}
                        //else
                        //{
                        //    builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a></li>");
                        //}
                        if (page == pageTotalCount)
                        {
                            builder.Append(string.Format("<li class=\"page-item active\"><a class=\"page-link\" href=\"#\">{0}</a></li>", page));
                        }
                        else
                        {
                            builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\"" + string.Format(pageUrlFormat, (pageTotalCount).ToString()) + "\">" + (pageTotalCount).ToString() + "</a></li>");
                        }
                    }
                }
            }
            if (page < pageTotalCount)
            {
                builder.Append("<li class=\"page-item\"><a class=\"page-link\" href=\" " + string.Format(pageUrlFormat, pageTotalCount.ToString()) + "\" aria-label=\"Next\" title=\"" + Language.Tag("尾页", lang) + "\">»</a></li>");
            }
            else if (page == pageTotalCount)
            {
                builder.Append("<li class=\"page-item disabled\"><a class=\"page-link\" aria-label=\"Next\" title=\"" + Language.Tag("尾页", lang) + "\">»</a></li>");
            }
            builder.Append("</ul>");
            return builder.ToString();
        }

        /// <summary>
        ///  /// JS分页控件，显示数据条数
        /// </summary>
        /// <param name="pageUrlFormat">翻页时需要的参数</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageTotalItemSize">总数据条数</param>
        /// <returns></returns>
        public static string GetPaginationStringForJS(string pageUrlFormat, int page, int pageSize, int pageTotalItemSize)
        {
            string lang = Language.AdminCurrentLanguage().Code;
            int pageTotalCount = GetPageCount(pageSize, pageTotalItemSize);
            StringBuilder builder = new StringBuilder();
            if (LB.Tools.CookieTool.GetCookieString("pageSize") != "")
            {
                pageSize = int.Parse(LB.Tools.CookieTool.GetCookieString("pageSize"));
            }
            if (pageSize > 100)
            {
                pageSize = 20;
            }
            if (pageTotalCount < 1)
            {
                pageTotalCount = 1;
            }
            int i = 1;

            if (page > pageTotalCount)
            {
                page = pageTotalCount;
            }
            else
            {
                if (page <= 1)
                {
                    page = 1;
                }
            }

            //builder.Append("<script type=\"text/javascript\">  \r\n");
            //builder.Append("					function PageSizeGo(pageSize)  \r\n");
            //builder.Append("					{  \r\n");
            //builder.Append("					  SetCookie('pageSize', pageSize, 1);window.location='" + string.Format(pageUrlFormat, "1") + "';  \r\n");
            //builder.Append("					}  \r\n");
            //builder.Append("</script>  \r\n");
            builder.Append("				<div class=\"pg\">  \r\n");
            //builder.Append("			    <label class=\"pgText\">" + Language.Tag("每页显示", lang) + " </label> <label class=\"pgSize\"><ul class=\"dropmenu\"><li class=\"menu_li\"><a class=\"noclick\">" + pageSize + "</a><dl class=\"menu_li_content\"><dd><a onclick=\"PageSizeGo(20);\">20</a></dd><dd><a onclick=\"PageSizeGo(40);\">40</a></dd><dd><a onclick=\"PageSizeGo(60);\">60</a></dd><dd><a onclick=\"PageSizeGo(80);\">80</a></dd><dd class=\"last\"><a onclick=\"PageSizeGo(100);\">100</a></dd></dl></li></ul></label><label class=\"pgText\">" + Language.Tag("共", lang) + " </label> <label class=\"pgNum\">" + pageTotalItemSize + "" + Language.Tag("条", lang) + "</label>\r\n");
            if (page <= 1)
            {
                builder.Append("					<a class=\"pgDisabled\">" + Language.Tag("上一页", lang) + "</a>");
            }
            else
            {
                builder.Append("<a href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, (page - 1).ToString()) + "\">" + Language.Tag("上一页", lang) + "</a>");
            }

            if (pageTotalCount < 11)
            {
                for (i = 1; i <= pageTotalCount; i++)
                {
                    if (i == page)
                    {
                        builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", i));
                    }
                    else
                    {
                        builder.Append("<a href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
                    }
                }
            }
            else
            {
                if (page < 5)
                {
                    for (i = 1; i <= 7; i++)
                    {
                        if (i == page)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", i));
                        }
                        else
                        {
                            builder.Append("<a href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
                        }
                    }
                    builder.Append("<span>...</span>");

                    if (page == pageTotalCount - 2)
                    {
                        builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                    }
                    else
                    {
                        builder.Append("<a href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
                    }

                    if (page == pageTotalCount - 1)
                    {
                        builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                    }
                    else
                    {
                        builder.Append("<a href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
                    }
                    if (page == pageTotalCount)
                    {
                        builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                    }
                    else
                    {
                        builder.Append("<a href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, (pageTotalCount).ToString()) + "\">" + (pageTotalCount).ToString() + "</a>");
                    }

                }
                else
                {
                    builder.Append("<a href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, "1") + "\">1</a>");
                    builder.Append("<a href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, "2") + "\">2</a>");
                    builder.Append("<span>...</span>");
                    if (page > (pageTotalCount - 7))
                    {

                        for (i = pageTotalCount - 7; i <= pageTotalCount - 3; i++)
                        {
                            if (i == page)
                            {
                                builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", i));
                            }
                            else
                            {
                                builder.Append("<a href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
                            }
                        }

                        if (page == pageTotalCount - 2)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                        }
                        else
                        {
                            builder.Append("<a href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
                        }

                        if (page == pageTotalCount - 1)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                        }
                        else
                        {
                            builder.Append("<a href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
                        }
                        if (page == pageTotalCount)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                        }
                        else
                        {
                            builder.Append("<a href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, (pageTotalCount).ToString()) + "\">" + (pageTotalCount).ToString() + "</a>");
                        }

                    }
                    else
                    {
                        for (i = page - 2; i <= page + 2; i++)
                        {
                            if (i == page)
                            {
                                builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", i));
                            }
                            else
                            {
                                builder.Append("<a href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
                            }
                        }
                        builder.Append("<span>...</span>");

                        if (page == pageTotalCount - 2)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                        }
                        else
                        {
                            builder.Append("<a href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
                        }

                        if (page == pageTotalCount - 1)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                        }
                        else
                        {
                            builder.Append("<a href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
                        }
                        if (page == pageTotalCount)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                        }
                        else
                        {
                            builder.Append("<a href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, (pageTotalCount).ToString()) + "\">" + (pageTotalCount).ToString() + "</a>");
                        }

                    }
                }
            }


            if (page < pageTotalCount)
            {
                builder.Append("<a class=\"next\" href=\"javascript:void(0)\" onclick=\"" + string.Format(pageUrlFormat, Convert.ToString(page + 1)) + "\">" + Language.Tag("下一页", lang) + "</a>");
            }
            else if (page == pageTotalCount)
            {
                builder.Append("<a class=\"pgDisabled\">" + Language.Tag("下一页", lang) + "</a>");
            }
            builder.Append("				</div>  \r\n");


            //}

            return builder.ToString();
        }
        #endregion

        #region 前台台分页
        /// <summary>
        /// 分页控件，不显示数据条数
        /// </summary>
        /// <param name="pageUrlFormat">翻页时需要的参数</param>
        /// <param name="page">当前页</param>
        /// <param name="pageTotalCount">总页数</param>
        /// <returns></returns>
        public static string GetPaginationStringForWeb(string pageUrlFormat, int page, int pageTotalCount)
        {
            string lang = Language.CurrentLanguageFlag();
            StringBuilder builder = new StringBuilder();

            if (pageTotalCount < 1)
            {
                pageTotalCount = 1;
            }
            int i = 1;

            if (page > pageTotalCount)
            {
                page = pageTotalCount;
            }
            else
            {
                if (page <= 1)
                {
                    page = 1;
                }
            }
            if (pageTotalCount > 1)
            {

                builder.Append("				<div class=\"pg\">  \r\n");
                if (page <= 1)
                {
                    builder.Append("					<a class=\"pgDisabled\">" + Language.Tag("上一页", lang) + "</a>");
                }
                else
                {
                    builder.Append("<a href=\" " + string.Format(pageUrlFormat, (page - 1).ToString()) + "\">" + Language.Tag("上一页", lang) + "</a>");
                }

                if (pageTotalCount < 11)
                {
                    for (i = 1; i <= pageTotalCount; i++)
                    {
                        if (i == page)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", i));
                        }
                        else
                        {
                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
                        }
                    }
                }
                else
                {
                    if (page < 5)
                    {
                        for (i = 1; i <= 7; i++)
                        {
                            if (i == page)
                            {
                                builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", i));
                            }
                            else
                            {
                                builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
                            }
                        }
                        builder.Append("<span>...</span>");

                        if (page == pageTotalCount - 2)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                        }
                        else
                        {
                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
                        }

                        if (page == pageTotalCount - 1)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                        }
                        else
                        {
                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
                        }
                        if (page == pageTotalCount)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                        }
                        else
                        {
                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount).ToString()) + "\">" + (pageTotalCount).ToString() + "</a>");
                        }

                    }
                    else
                    {
                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, "1") + "\">1</a>");
                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, "2") + "\">2</a>");
                        builder.Append("<span>...</span>");
                        if (page > (pageTotalCount - 7))
                        {

                            for (i = pageTotalCount - 7; i <= pageTotalCount - 3; i++)
                            {
                                if (i == page)
                                {
                                    builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", i));
                                }
                                else
                                {
                                    builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
                                }
                            }

                            if (page == pageTotalCount - 2)
                            {
                                builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                            }
                            else
                            {
                                builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
                            }

                            if (page == pageTotalCount - 1)
                            {
                                builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                            }
                            else
                            {
                                builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
                            }
                            if (page == pageTotalCount)
                            {
                                builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                            }
                            else
                            {
                                builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount).ToString()) + "\">" + (pageTotalCount).ToString() + "</a>");
                            }

                        }
                        else
                        {
                            for (i = page - 2; i <= page + 2; i++)
                            {
                                if (i == page)
                                {
                                    builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", i));
                                }
                                else
                                {
                                    builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString()) + "\">" + i.ToString() + "</a>");
                                }
                            }
                            builder.Append("<span>...</span>");

                            if (page == pageTotalCount - 2)
                            {
                                builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                            }
                            else
                            {
                                builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString()) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
                            }

                            if (page == pageTotalCount - 1)
                            {
                                builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                            }
                            else
                            {
                                builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString()) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
                            }
                            if (page == pageTotalCount)
                            {
                                builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                            }
                            else
                            {
                                builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount).ToString()) + "\">" + (pageTotalCount).ToString() + "</a>");
                            }

                        }
                    }
                }


                if (page < pageTotalCount)
                {
                    builder.Append("<a class=\"next\" href=\" " + string.Format(pageUrlFormat, Convert.ToString(page + 1)) + "\">" + Language.Tag("下一页", lang) + "</a>");
                }
                else if (page == pageTotalCount)
                {
                    builder.Append(" <a class=\"pgDisabled\">" + Language.Tag("下一页", lang) + "</a>");
                }
                builder.Append("				</div>  \r\n");


            }

            return builder.ToString();
        }
        /// <summary>
        ///  /// 分页控件，显示数据条数
        /// </summary>
        /// <param name="pageUrlFormat">翻页时需要的参数</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageTotalItemSize">总数据条数</param>
        /// <returns></returns>
        public static string GetPaginationStringForWeb(string pageUrlFormat, int page, int pageSize, int pageTotalItemSize, Lebi_Language language)
        {
            return GetPaginationStringForWeb(pageUrlFormat, page, pageSize, pageTotalItemSize, language.Code);
        }
        public static string GetPaginationStringForWeb(string pageUrlFormat, int page, int pageSize, int pageTotalItemSize, string lang)
        {
            int pageTotalCount = GetPageCount(pageSize, pageTotalItemSize);
            StringBuilder builder = new StringBuilder();

            if (pageTotalCount < 1)
            {
                pageTotalCount = 1;
            }
            int i = 1;

            if (page > pageTotalCount)
            {
                page = pageTotalCount;
            }
            else
            {
                if (page <= 1)
                {
                    page = 1;
                }
            }

            builder.Append("				<div class=\"pg\">  \r\n");
            if (page <= 1)
            {
                builder.Append("					<a class=\"pgDisabled\">" + Language.Tag("上一页", lang) + "</a>");
            }
            else
            {
                builder.Append("<a href=\"" + string.Format(pageUrlFormat, (page - 1).ToString(), pageSize) + "\">" + Language.Tag("上一页", lang) + "</a>");
            }

            if (pageTotalCount < 11)
            {
                for (i = 1; i <= pageTotalCount; i++)
                {
                    if (i == page)
                    {
                        builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", i));
                    }
                    else
                    {
                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString(), pageSize) + "\">" + i.ToString() + "</a>");
                    }
                }
            }
            else
            {
                if (page < 5)
                {
                    for (i = 1; i <= 7; i++)
                    {
                        if (i == page)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", i));
                        }
                        else
                        {
                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString(), pageSize) + "\">" + i.ToString() + "</a>");
                        }
                    }
                    builder.Append("<span>...</span>");

                    if (page == pageTotalCount - 2)
                    {
                        builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                    }
                    else
                    {
                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString(), pageSize) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
                    }

                    if (page == pageTotalCount - 1)
                    {
                        builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                    }
                    else
                    {
                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString(), pageSize) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
                    }
                    if (page == pageTotalCount)
                    {
                        builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                    }
                    else
                    {
                        builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount).ToString(), pageSize) + "\">" + (pageTotalCount).ToString() + "</a>");
                    }

                }
                else
                {
                    builder.Append("<a href=\"" + string.Format(pageUrlFormat, "1", pageSize) + "\">1</a>");
                    builder.Append("<a href=\"" + string.Format(pageUrlFormat, "2", pageSize) + "\">2</a>");
                    builder.Append("<span>...</span>");
                    if (page > (pageTotalCount - 7))
                    {

                        for (i = pageTotalCount - 7; i <= pageTotalCount - 3; i++)
                        {
                            if (i == page)
                            {
                                builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", i));
                            }
                            else
                            {
                                builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString(), pageSize) + "\">" + i.ToString() + "</a>");
                            }
                        }

                        if (page == pageTotalCount - 2)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                        }
                        else
                        {
                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString(), pageSize) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
                        }

                        if (page == pageTotalCount - 1)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                        }
                        else
                        {
                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString(), pageSize) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
                        }
                        if (page == pageTotalCount)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                        }
                        else
                        {
                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount).ToString(), pageSize) + "\">" + (pageTotalCount).ToString() + "</a>");
                        }

                    }
                    else
                    {
                        for (i = page - 2; i <= page + 2; i++)
                        {
                            if (i == page)
                            {
                                builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", i));
                            }
                            else
                            {
                                builder.Append("<a href=\"" + string.Format(pageUrlFormat, i.ToString(), pageSize) + "\">" + i.ToString() + "</a>");
                            }
                        }
                        builder.Append("<span>...</span>");

                        if (page == pageTotalCount - 2)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                        }
                        else
                        {
                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 2).ToString(), pageSize) + "\">" + (pageTotalCount - 2).ToString() + "</a>");
                        }

                        if (page == pageTotalCount - 1)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                        }
                        else
                        {
                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount - 1).ToString(), pageSize) + "\">" + (pageTotalCount - 1).ToString() + "</a>");
                        }
                        if (page == pageTotalCount)
                        {
                            builder.Append(string.Format("<a class=\"pgCurr\">{0}</a>", page));
                        }
                        else
                        {
                            builder.Append("<a href=\"" + string.Format(pageUrlFormat, (pageTotalCount).ToString(), pageSize) + "\">" + (pageTotalCount).ToString() + "</a>");
                        }

                    }
                }
            }


            if (page < pageTotalCount)
            {
                builder.Append("<a class=\"next\" href=\" " + string.Format(pageUrlFormat, Convert.ToString(page + 1), pageSize) + "\">" + Language.Tag("下一页", lang) + "</a>");
            }
            else if (page == pageTotalCount)
            {
                builder.Append("<a class=\"pgDisabled\">" + Language.Tag("下一页", lang) + "</a>");
            }
            builder.Append("				</div>  \r\n");


            //}

            return builder.ToString();
        }
        /// <summary>
        /// 分页控件-简洁模式，不显示数据条数
        /// </summary>
        /// <param name="pageUrlFormat">翻页时需要的参数</param>
        /// <param name="page">当前页</param>
        /// <param name="pageTotalCount">总页数</param>
        /// <returns></returns>
        public static string GetPaginationStringForWebSimple(string pageUrlFormat, int page, int pageTotalCount, Lebi_Language language)
        {
            return GetPaginationStringForWebSimple(pageUrlFormat, page, pageTotalCount, language.Code);
        }
        public static string GetPaginationStringForWebSimple(string pageUrlFormat, int page, int pageTotalCount, string lang)
        {

            StringBuilder builder = new StringBuilder();
            if (pageTotalCount < 1)
            {
                pageTotalCount = 1;
            }
            int i = 1;

            if (page > pageTotalCount)
            {
                page = pageTotalCount;
            }
            else
            {
                if (page <= 1)
                {
                    page = 1;
                }
            }
            if (pageTotalCount > 1)
            {

                builder.Append("				<div class=\"pg\">  \r\n");
                if (page <= 1)
                {
                    builder.Append("					<a class=\"pgDisabled\">" + Language.Tag("上一页", lang) + "</a>");
                }
                else
                {
                    builder.Append("<a href=\" " + string.Format(pageUrlFormat, (page - 1).ToString()) + "\">" + Language.Tag("上一页", lang) + "</a>");
                }
                if (page < pageTotalCount)
                {
                    builder.Append("<a class=\"next\" href=\" " + string.Format(pageUrlFormat, Convert.ToString(page + 1)) + "\">" + Language.Tag("下一页", lang) + "</a>");
                }
                else if (page == pageTotalCount)
                {
                    builder.Append(" <a class=\"pgDisabled\">" + Language.Tag("下一页", lang) + "</a>");
                }
                builder.Append("				</div>  \r\n");
            }
            return builder.ToString();
        }
        /// <summary>
        ///  /// 分页控件-简洁模式，显示数据条数
        /// </summary>
        /// <param name="pageUrlFormat">翻页时需要的参数</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageTotalItemSize">总数据条数</param>
        /// <returns></returns>
        public static string GetPaginationStringForWebSimple(string pageUrlFormat, int page, int pageSize, int pageTotalItemSize, Lebi_Language language)
        {

            return GetPaginationStringForWebSimple(pageUrlFormat, page, pageSize, pageTotalItemSize, language.Code);
        }
        public static string GetPaginationStringForWebSimple(string pageUrlFormat, int page, int pageSize, int pageTotalItemSize, string lang)
        {
            int pageTotalCount = GetPageCount(pageSize, pageTotalItemSize);
            StringBuilder builder = new StringBuilder();

            if (pageTotalCount < 1)
            {
                pageTotalCount = 1;
            }
            int i = 1;

            if (page > pageTotalCount)
            {
                page = pageTotalCount;
            }
            else
            {
                if (page <= 1)
                {
                    page = 1;
                }
            }

            builder.Append("				<div class=\"pg\">  \r\n");
            if (page <= 1)
            {
                builder.Append("					<a class=\"pgDisabled\">" + Language.Tag("上一页", lang) + "</a>");
            }
            else
            {
                builder.Append("<a href=\"" + string.Format(pageUrlFormat, (page - 1).ToString(), pageSize) + "\">" + Language.Tag("上一页", lang) + "</a>");
            }
            if (page < pageTotalCount)
            {
                builder.Append("<a class=\"next\" href=\" " + string.Format(pageUrlFormat, Convert.ToString(page + 1), pageSize) + "\">" + Language.Tag("下一页", lang) + "</a>");
            }
            else if (page == pageTotalCount)
            {
                builder.Append("<a class=\"pgDisabled\">" + Language.Tag("下一页", lang) + "</a>");
            }
            builder.Append("				</div>  \r\n");
            return builder.ToString();
        }
        #endregion

    }
}