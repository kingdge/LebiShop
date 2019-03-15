using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Collections;
using System.Text;
namespace INIpayWeb
{
    public partial class iframe_relay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                GeneratorForm();
        }

        /// <summary>
        /// 
        /// </summary>
        private void GeneratorForm()
        {
            NameValueCollection parameters = Request.Params;

            IEnumerator enumerator = parameters.GetEnumerator();

            StringBuilder sb = new StringBuilder("paramMap : ");

            while (enumerator.MoveNext())
            {
                // get the current query parameter
                string key = enumerator.Current.ToString();

                // insert the parameter into the url
                //string value = HttpUtility.UrlEncode(parameters[key]);
                string value = Server.HtmlEncode(parameters[key]);

                sb.Append(string.Format("{0}={1}&", key, value));

                TextBox iniTxt = new TextBox();
                iniTxt.ID = key;
                iniTxt.Text = value;

                PlaceHolder1.Controls.Add(iniTxt);

                //Response.Write(string.Format("{0}<input type='text' runat='server' name='{0}' value='{1}'/><br/>", key, value));
            }

            Response.Write(sb.ToString());

        }   //GeneratorForm

    }   //class
}