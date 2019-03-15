using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;

namespace Shop.inc
{
    public partial class product_related : Bussiness.ShopPage
    {
        protected List<Lebi_Product> products;
        protected string tag;
        protected int id;
        protected int Count;
        public void LoadPage()
        {
            LoadTheme();
            PageSize = RequestTool.RequestInt("Count", 20);
            id = RequestTool.RequestInt("id", 0);
            tag = RequestTool.RequestString("tag");
            tag = tag.Replace(",", "，");
            if (tag == "" || tag == null)
                tag = "NO TAGS";
            string where = "";
            if (tag.IndexOf("，") > -1)
            {
                string[] tagsArr;
                tagsArr = tag.Split(new char[1] { '，' });
                foreach (string tags in tagsArr)
                {
                    if (where == "")
                        where = "Name like lbsql{'%" + tags + "%'}";
                    else
                        where += " or Name like lbsql{'%" + tags + "%'}";
                }
            }
            else
            {
                where = "Name like lbsql{'%" + tag + "%'}";
            }
            where = " (" + where + ") and " + ProductWhere;
            //Response.Write(where);

            //Response.End();
            //return;

            LB.DataAccess.SQLPara sp = new LB.DataAccess.SQLPara(where, " Sort desc,id desc", "*");
            products = B_Lebi_Product.GetList(sp, PageSize, pageindex);
        }
    }
}