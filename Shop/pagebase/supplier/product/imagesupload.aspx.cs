using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Model;
using Shop.Tools;
using Shop.Bussiness;
namespace Shop.supplier.product
{
    public partial class imagesupload : SupplierAjaxBase
    {
        protected string inputname = "";
        protected string images = "";
        protected List<LBimage> list;
        protected int pid;
        protected void Page_Load(object sender, EventArgs e)
        {
            pid = RequestTool.RequestInt("pid", 0);
            inputname = RequestTool.RequestString("input");
            if (inputname == "")
                inputname = "Images";
            images = RequestTool.RequestString("images");
            list = new List<LBimage>();
            string[] arr = images.Split('@');
            foreach (string str in arr)
            {
                if (str != "")
                {
                    LBimage model = new LBimage();
                    model.original = str;
                    model.small = str + "&w=100&h=100";
                    model.medium = str + "&w=300&h=300";
                    model.big = str + "&w=800&h=800";
                    list.Add(model);
                }

            }
        }
    }
}