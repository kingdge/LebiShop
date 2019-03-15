using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;
using Shop.Bussiness;
using Shop.Tools;
using System.Reflection;
using System.Web.Script.Serialization;
using System.IO;
namespace Shop.Supplier.Ajax
{
    public partial class export : SupplierAjaxBase
    {
        protected Lebi_Language_Code CurrentLanguage;
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentLanguage = Language.CurrentLanguage();
            string action = Shop.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }
        /// <summary>
        /// 导入淘宝商品数据
        /// </summary>
        public void taobao_product_in()
        {
            if (!Power("supplier_product_datainout", "导入导出"))
            {
                AjaxNoPower();
                return;
            }
            int tb_typeid = RequestTool.RequestInt("tb_typeid", 0);
            string tb_file = RequestTool.RequestString("tb_file");
            string tb_folder = RequestTool.RequestString("tb_folder");
            string fileName = HttpContext.Current.Server.MapPath(@"~/" + WebPath + tb_file);
            int tb_split = RequestTool.RequestInt("tb_split", 0);
            string tbin_lang = RequestTool.RequestString("tbin_lang");
            int i = 0;
            if (File.Exists(fileName))
            {
                DataTable dt = new DataTable();
                char flag = '\t';
                if (tb_split == 2)
                    flag = ',';
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fs, Encoding.Default))
                    {
                        string text = string.Empty;
                        while (!reader.EndOfStream)
                        {
                            text = reader.ReadLine();
                            if (i == 1)
                            {
                                string[] arr = text.Split(flag);
                                foreach (string col in arr)
                                {
                                    dt.Columns.Add(col);
                                }
                            }
                            else if (i > 1)
                            {
                                string[] arr = text.Split(flag);
                                DataRow r = dt.NewRow();
                                int j = 0;
                                foreach (string col in arr)
                                {
                                    if (j >= dt.Columns.Count)
                                        break;
                                    try
                                    {
                                        r[j] = col.Replace("\"", "");
                                    }
                                    catch
                                    {
                                        r[j] = "";
                                    }
                                    j++;
                                }
                                dt.Rows.Add(r);
                            }
                            i++;
                        }
                    }
                }
                //======================================
                if (!dt.Columns.Contains("outer_id"))
                {
                    Response.Write("{\"msg\":\"请检查是否包含outer_id字段\"}");
                    return;
                }
                string number = "";
                int count = 0;
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    try
                    {
                        if (dt.Columns.Contains("outer_id"))
                            number = dt.Rows[m]["outer_id"].ToString();
                        bool addflag = false;
                        Lebi_Product pro = B_Lebi_Product.GetModel("Supplier_id =" + CurrentSupplier.id + " and Number=lbsql{'" + number + "'}");
                        if (pro == null)
                        {
                            pro = new Lebi_Product();
                            addflag = true;
                            pro.Number = dt.Rows[m]["outer_id"].ToString();
                            pro.Type_id_ProductStatus = 101;
                            pro.Type_id_ProductType = 320;
                            pro.Supplier_id = CurrentSupplier.id;
                            pro.IsSupplierTransport = CurrentSupplierGroup.IsSubmit;
                        }
                        pro.Pro_Type_id = tb_typeid;

                        if (dt.Columns.Contains("title"))
                            pro.Name = Language.GetString(dt.Rows[m]["title"].ToString(), pro.Name, tbin_lang);
                        if (dt.Columns.Contains("description"))
                            pro.Description = Language.GetString(dt.Rows[m]["description"].ToString(), pro.Description, tbin_lang);
                        if (dt.Columns.Contains("wireless_desc"))
                            pro.MobileDescription = Language.GetString(dt.Rows[m]["wireless_desc"].ToString(), pro.MobileDescription, tbin_lang);

                        if (dt.Columns.Contains("price"))
                        {

                            if (dt.Columns.Contains("price"))
                            {
                                string p = dt.Rows[m]["price"].ToString();
                                pro.Price = Convert.ToDecimal(p);
                            }
                            pro.Price_Market = pro.Price;
                            pro.Price_Cost = pro.Price;

                        }
                        if (dt.Columns.Contains("num"))
                        {
                            int Count_Stock = 0;
                            int.TryParse(Convert.ToString(dt.Rows[m]["num"]), out Count_Stock);
                            pro.Count_Stock = Count_Stock;
                        }
                        if (dt.Columns.Contains("item_weight"))
                        {
                            int Weight = 0;
                            int.TryParse(Convert.ToString(dt.Rows[m]["item_weight"]), out Weight);
                            pro.Weight = Weight;
                        }
                        if (dt.Columns.Contains("VolumeL"))
                        {
                            int VolumeL = 0;
                            int.TryParse(Convert.ToString(dt.Rows[m]["VolumeL"]), out VolumeL);
                            pro.VolumeL = VolumeL;
                        }
                        if (dt.Columns.Contains("VolumeW"))
                        {
                            int VolumeW = 0;
                            int.TryParse(Convert.ToString(dt.Rows[m]["VolumeW"]), out VolumeW);
                            pro.VolumeW = VolumeW;
                        }
                        if (dt.Columns.Contains("VolumeH"))
                        {
                            int VolumeH = 0;
                            int.TryParse(Convert.ToString(dt.Rows[m]["VolumeH"]), out VolumeH);
                            pro.VolumeH = VolumeH;
                        }
                        if (dt.Columns.Contains("PackageRate"))
                        {
                            int PackageRate = 1;
                            int.TryParse(Convert.ToString(dt.Rows[m]["PackageRate"]), out PackageRate);
                            pro.PackageRate = PackageRate;
                        }
                        if (addflag)
                        {
                            B_Lebi_Product.Add(pro);
                            pro.id = B_Lebi_Product.GetMaxId();
                        }
                        else
                        {
                            B_Lebi_Product.Update(pro);
                        }
                        if (dt.Columns.Contains("picture"))
                        {
                            try
                            {
                                string img = dt.Rows[m]["picture"].ToString();
                                string images = "";
                                if (tb_folder == "")
                                {
                                    //1f6edf58105f3abb1ef9665129eb6c52
                                    //1f6edf58105f3abb1ef9665129eb6c52:1:0:|http://img01.taobaocdn.com/bao/uploaded/i1/T1Oys1FgNeXXXXXXXX_!!0-item_pic.jpg;
                                    //5327022ebfe958090d3abff999c8c800:1:1:|http://img02.taobaocdn.com/bao/uploaded/i2/24197542/T2wnCFXtNXXXXXXXXX_!!24197542.jpg;
                                    //b7972e16753d746d77f2f769002268d5:1:2:|http://img01.taobaocdn.com/bao/uploaded/i1/24197542/T2n6BuXvhXXXXXXXXX_!!24197542.jpg;
                                    //06e5fd8cdb3babd4664b7b736258290a:1:3:|http://img03.taobaocdn.com/bao/uploaded/i3/24197542/T2yX1BXwhXXXXXXXXX_!!24197542.jpg;
                                    //d1a5c9c441a994310db75a83005628b7:1:4:|http://img03.taobaocdn.com/bao/uploaded/i3/24197542/T2I4XAXpJaXXXXXXXX_!!24197542.jpg;
                                    //处理图片
                                    string[,] arr = RegexTool.GetRegArray(img, @"\|(.*?);");
                                    for (int ai = 0; ai < arr.GetUpperBound(0); ai++)
                                    {
                                        string v = RegexTool.GetRegValue(arr[ai, 1], @"\|(.*?);");
                                        if (v == null || v == "")
                                            continue;
                                        LBimage lbimg = ImageHelper.DownLoadImage(v, pro);
                                        if (ai == 0)
                                        {
                                            pro.ImageBig = lbimg.big;
                                            pro.ImageMedium = lbimg.medium;
                                            pro.ImageOriginal = lbimg.original;
                                            pro.ImageSmall = lbimg.small;
                                        }
                                        else
                                        {
                                            images += "@" + lbimg.original;
                                        }
                                    }
                                }
                                else
                                {
                                    //上传文件夹导入图片的方式
                                    img = ";" + img;
                                    string[,] arr = RegexTool.GetRegArray(img, @";(.*?):");
                                    string ServerPath = System.Web.HttpContext.Current.Server.MapPath("~/");
                                    for (int ai = 0; ai < arr.GetUpperBound(0); ai++)
                                    {
                                        string v = RegexTool.GetRegValue(arr[ai, 1], @";(.*?):");
                                        if (v == null || v == "")
                                            continue;
                                        v = ServerPath + tb_folder + "/" + v + ".tbi";
                                        v = v.Replace("//", "/");
                                        LBimage lbimg = ImageHelper.CreateTaobaoImage(v, pro);
                                        if (ai == 0)
                                        {
                                            pro.ImageBig = lbimg.big;
                                            pro.ImageMedium = lbimg.medium;
                                            pro.ImageOriginal = lbimg.original;
                                            pro.ImageSmall = lbimg.small;
                                        }
                                        else
                                        {
                                            images += "@" + lbimg.original;
                                        }
                                    }
                                }
                                pro.Images = images;
                                B_Lebi_Product.Update(pro);
                            }
                            catch
                            {

                            }
                        }
                        count++;
                    }
                    catch
                    {
                        continue;
                    }
                }
                Log.Add("导入淘宝商品", "Product", "");
                Response.Write("{\"msg\":\"OK\",\"count\":\"" + count + "\"}");
            }
            else
            {
                Response.Write("{\"msg\":\"" + Tag("数据文件错误") + "\"}");
                return;
            }


        }
        /// <summary>
        /// 导出淘宝格式商品数据
        /// </summary>
        public void taobao_product_out()
        {
            if (!Power("supplier_product_datainout", "导入导出"))
            {
                AjaxNoPower();
                return;
            }
            string lang = RequestTool.RequestString("lang");
            SearchProduct sp = new SearchProduct(CurrentSupplier, CurrentLanguage.Code);
            string where = "Supplier_id ="+ CurrentSupplier.id +" and Product_id=0 " + sp.SQL;
            List<Lebi_Product> ps = B_Lebi_Product.GetList(where, "");


            string titles = "title,cid,seller_cids,stuff_status,location_state,location_city,item_type,price,auction_increment,num,valid_thru,freight_payer,post_fee,ems_fee,express_fee,has_invoice,has_warranty,approve_status,has_showcase,list_time,description,cateProps,postage_id,has_discount,modified,upload_fail_msg,picture_status,auction_point,picture,video,skuProps,inputPids,inputValues,outer_id,propAlias,auto_fill,num_id,local_cid,navigation_type,user_name,syncStatus,is_lighting_consigment,is_xinpin,foodparame,features,buyareatype,global_stock_type,global_stock_country,sub_stock_type,item_size,item_weight,sell_promise,custom_design_flag,wireless_desc,VolumeL,VolumeW,VolumeH,PackageRate";
            string[] arr = titles.Split(',');
            DataTable dt = new DataTable();
            foreach (string col in arr)
            {
                dt.Columns.Add(col);
            }
            DataRow r = dt.NewRow();
            dt.Rows.Add(r);

            StringBuilder sb = new StringBuilder();
            sb.Append("version 1.00,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,\r\n");
            sb.Append(titles + "\r\n");
            sb.Append("宝贝名称,宝贝类目,店铺类目,新旧程度,省,城市,出售方式,宝贝价格,加价幅度,宝贝数量,有效期,运费承担,平邮,EMS,快递,发票,保修,放入仓库,橱窗推荐,开始时间,宝贝描述,宝贝属性,邮费模版ID,会员打折,修改时间,上传状态,图片状态,返点比例,新图片,视频,销售属性组合,用户输入ID串,用户输入名-值对,商家编码,销售属性别名,代充类型,数字ID,本地ID,宝贝分类,用户名称,宝贝状态,闪电发货,新品,食品专项,尺码库,采购地,库存类型,国家地区,库存计数,物流体积,物流重量,退换货承诺,定制工具,无线详情,包装尺寸L,包装尺寸W,包装尺寸H,包装率\r\n");
            //sb.Append("title,stuff_status,price,num,description,modified,item_weight,picture,wireless_desc \r\n");
            //sb.Append("宝贝名称,新旧程度,宝贝价格,宝贝数量,宝贝描述,修改时间,物流重量,新图片,无线详情  \r\n");
            string domain = "http://" + RequestTool.GetRequestDomainPort();
            string images = "";
            string[] temparr;
            string temp;
            int i = 0;
            string name = "";
            string filename = System.DateTime.Now.ToString("yyyyMMddhhmmss");
            string serverpath = HttpContext.Current.Server.MapPath(@"~/" + WebPath);
            string savepath = serverpath + "/download/taobaoproduct" + filename + "/taobao";
            if (Directory.Exists(savepath))
            {
                Directory.Delete(savepath);
            }
            Directory.CreateDirectory(savepath);
            foreach (Lebi_Product pro in ps)
            {
                dt.Rows[0]["title"] = Lang(pro.Name, lang);
                dt.Rows[0]["stuff_status"] = "1";
                dt.Rows[0]["price"] = pro.Price;
                dt.Rows[0]["num"] = pro.Count_Stock;
                dt.Rows[0]["description"] = taobao_product_out_langtxt(pro.Description, lang);
                dt.Rows[0]["modified"] = pro.Time_Edit;
                dt.Rows[0]["item_weight"] = pro.Weight;
                dt.Rows[0]["outer_id"] = pro.Number;
                dt.Rows[0]["wireless_desc"] = taobao_product_out_langtxt(pro.MobileDescription, lang);
                dt.Rows[0]["VolumeL"] = pro.VolumeL;
                dt.Rows[0]["VolumeW"] = pro.VolumeW;
                dt.Rows[0]["VolumeH"] = pro.VolumeH;
                dt.Rows[0]["PackageRate"] = pro.PackageRate;
                //处理图片
                images = pro.ImageOriginal + pro.Images;
                temparr = images.Split('@');
                images = "";
                i = 0;
                foreach (string img in temparr)
                {
                    if (img != "")
                    {
                        name = MD5(img);
                        FileTool.CopyFile(serverpath + img, savepath + "/" + name + ".tbi", true);
                        temp = domain + img;
                        //temp = name + ":0:" + i + ":|;";
                        temp = name + ":1:" + i + ":|" + temp + ";";
                        images += temp;

                        i++;
                    }
                }
                dt.Rows[0]["picture"] = images;
                i = 0;
                foreach (DataColumn col in dt.Columns)
                {
                    if (i == 0)
                        sb.Append(dt.Rows[0][col.ToString()]);
                    else
                        sb.Append("," + dt.Rows[0][col.ToString()]);
                    i++;
                }
                sb.Append("\r\n");
            }
            HtmlEngine.Instance.WriteFile(serverpath + "/download/taobaoproduct" + filename + "/taobao.csv", sb.ToString());
            //ZipUtil.RAR(serverpath + "/download/taobaoproduct" + filename, serverpath + "/download", "taobaoproduct" + filename + ".rar");
            string err = "";
            ZipUtil.ZipFile(serverpath + "/download/taobaoproduct" + filename + "/taobao", serverpath + "/download/taobaoproduct" + filename + "/taobao.zip", out err);
            ZipUtil.ZipFile(serverpath + "/download/taobaoproduct" + filename, serverpath + "/download/taobaoproduct" + filename + ".zip", out err);
            Response.Write("{\"msg\":\"OK\",\"url\":\"" + WebPath + "/download/taobaoproduct" + filename + ".zip\"}");

        }
        public string MD5(string PWD)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            PWD = BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(PWD))).Replace("-", "").ToLower();
            return PWD;
        }
        private string taobao_product_out_langtxt(string instr, string lang)
        {
            instr = Lang(instr, lang);
            instr = instr.Replace("'", "''");
            instr = instr.Replace("\"", "\"\"");
            instr = instr.Replace(",", "，");
            instr = instr.Replace("\r", "");
            instr = instr.Replace("\n", "");
            instr = instr.Replace("\t", " ");
            return instr;
        }
        private string out_txt(string instr)
        {
            instr = instr.Replace("'", "''");
            instr = instr.Replace("\"", "\"\"");
            instr = instr.Replace(",", "，");
            instr = instr.Replace("\r", "");
            instr = instr.Replace("\n", "");
            instr = instr.Replace("\t", " ");
            return instr;
        }
        public string TagValue(Lebi_Language_Tag tag, string lang)
        {
            if (tag == null)
                return "";
            string res = "";
            Type type = tag.GetType();
            System.Reflection.PropertyInfo p = type.GetProperty(lang);
            if (p == null)
                return "";
            res = p.GetValue(tag, null).ToString();
            return res;
        }
        /// <summary>
        /// 导出供应商订单数据(商品)
        /// </summary>
        public void SupplierOrder_product_Export()
        {
            if (!EX_Admin.Power("statis_orderproduct", "订单报表"))
            {
                PageNoPower();
            }
            string where = RequestTool.RequestString("where");
            where = Server.UrlDecode(where).Replace("&#180;", "'");
            //SystemLog.Add(where);
            string titles = "" + Tag("序号") + "," + Tag("供应商") + "," + Tag("配送点") + "," + Tag("单号") + "," + Tag("订购日期") + "," + Tag("发货时间") + "," + Tag("支付状态") + "," + Tag("支付方式") + "," + Tag("配送方式") + "," + Tag("商品名称") + "," + Tag("条形码") + "," + Tag("商品条码") + "," + Tag("单位") + "," + Tag("数量") + "," + Tag("金额") + "";
            string[] arr = titles.Split(',');
            DataTable dt = new DataTable();
            foreach (string col in arr)
            {
                dt.Columns.Add(col);
            }
            DataRow r = dt.NewRow();
            dt.Rows.Add(r);
            //List<Lebi_Order_Product> models = B_Lebi_Order_Product.GetList("Order_id in (select id from Lebi_Order where " + where + ")", "id desc");
            List<Lebi_Order_Product> models = B_Lebi_Order_Product.GetList(where, "id desc");
            //Response.Write(models.Count);
            //Response.End();
            StringBuilder sb = new StringBuilder();
            if (!Shop.LebiAPI.Service.Instanse.Check("managelicenese"))
            {
                sb.Append("LebiShop V" + SYS.Version + "." + SYS.Version_Son + " (http://www.lebi.cn),\r\n");
            }
            sb.Append(titles + "\r\n");
            //sb.Append("标签,");
            //sb.Append(tag + "\r\n");
            int i = 0;
            Lebi_Supplier supplier;
            Lebi_Supplier_Delivery delivery;
            Lebi_Order order;
            foreach (Lebi_Order_Product model in models)
            {
                order = Shop.Bussiness.Order.GetOrder(model.Order_id);
                i++;
                supplier = B_Lebi_Supplier.GetModel(order.Supplier_id);
                if (supplier == null)
                    supplier = new Lebi_Supplier();
                delivery = B_Lebi_Supplier_Delivery.GetModel(order.Supplier_Delivery_id);
                if (delivery == null)
                    delivery = new Lebi_Supplier_Delivery();
                sb.Append(i + ",");
                sb.Append(out_txt(supplier.SubName) + ",");
                sb.Append(out_txt(delivery.Name) + ",");
                sb.Append(order.Code + ",");
                sb.Append(order.Time_Add.ToString("yyyy-MM-dd HH:mm:ss") + ",");
                if (order.IsShipped == 1)
                {
                    sb.Append(order.Time_Shipped.ToString("yyyy-MM-dd HH:mm:ss") + ",");
                }
                else
                {
                    sb.Append("-,");
                }

                sb.Append((order.IsPaid == 1 ? Tag("已支付") : Tag("未支付")) + ",");
                sb.Append(out_txt(Lang(order.Pay) + Lang(order.OnlinePay)) + ",");
                sb.Append(out_txt(order.Transport_Name) + ",");
                sb.Append(out_txt(Lang(model.Product_Name)) + ",");
                sb.Append(model.Product_Number + ",");
                sb.Append(Shop.Bussiness.EX_Product.GetProduct(model.Product_id).Code + ",");
                sb.Append(Lang(Shop.Bussiness.EX_Product.ProductUnit(Shop.Bussiness.EX_Product.GetProduct(model.Product_id).Units_id)) + ",");
                sb.Append(model.Count + ",");
                sb.Append(model.Price * model.Count);
                sb.Append("\r\n");
            }
            FileTool.StringTofile(sb.ToString(), "order", ".csv");
        }
    }
}