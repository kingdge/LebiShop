using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.Web.Script.Serialization;
using System.IO;
namespace Shop.Admin.Ajax
{
    public partial class export : AdminAjaxBase
    {
        protected Lebi_Language_Code CurrentLanguage;
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentLanguage = Language.CurrentLanguage();
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }
        /// <summary>
        /// 导出卡券数据
        /// </summary>
        public void Card()
        {
            if (!EX_Admin.Power("cardtype_list", "优惠券列表"))
            {
                AjaxNoPower();
                return;
            }
            string where = "1=1";
            string key = RequestTool.RequestString("key");
            SearchCard sc = new SearchCard(CurrentAdmin, CurrentLanguage.Code);
            int user_id = RequestTool.RequestInt("user_id");
            if (key != "")
                where += " and Code like lbsql{'%" + key + "%'}";
            if (user_id > 0)
                where += " and user_id=" + user_id + "";
            where += sc.SQL;
            List<Lebi_Card> models = B_Lebi_Card.GetList(where, "id desc");
            StringBuilder sb = new StringBuilder();
            if (!Shop.LebiAPI.Service.Instanse.Check("managelicenese"))
            {
                sb.Append("LebiShop V" + SYS.Version + "." + SYS.Version_Son + " (http://www.lebi.cn),,,,,,,,,,,,\r\n");
            }
            else
            {
                sb.Append("V" + SYS.Version + "." + SYS.Version_Son + ",,,,,,,,,,,,\r\n");
            }
            sb.Append("类型,");
            sb.Append("名称,");
            sb.Append("编号,");
            sb.Append("密码,");
            sb.Append("面值,");
            sb.Append("最低消费,");
            sb.Append("开始时间,");
            sb.Append("结束时间,");
            sb.Append("不找零,");
            sb.Append("单独使用,");
            sb.Append("会员ID,");
            sb.Append("会员,");
            sb.Append("分类\r\n");
            foreach (Lebi_Card model in models)
            {
                Lebi_CardOrder order = B_Lebi_CardOrder.GetModel(model.CardOrder_id);
                if (order == null)
                    continue;
                sb.Append(EX_Type.TypeName(model.Type_id_CardType) + ",");
                sb.Append(Language.Content(order.Name, CurrentLanguage.Code) + ",");
                sb.Append(model.Code + ",");
                sb.Append(model.Password + ",");
                sb.Append(model.Money + ",");
                sb.Append(model.Money_Buy + ",");
                sb.Append(model.Time_Begin + ",");
                sb.Append(model.Time_End + ",");
                sb.Append(model.IsPayOnce + ",");
                int IsCanOtherUse = 1;
                if (model.IsCanOtherUse == 1)
                    IsCanOtherUse = 0;
                sb.Append(IsCanOtherUse + ",");
                sb.Append(model.User_id + ",");
                sb.Append(model.User_UserName + ",");
                sb.Append(Shop.Bussiness.EX_Product.TypeNames(model.Pro_Type_ids, CurrentLanguage.Code).Replace(",", "|"));
                sb.Append("\r\n");
            }
            FileTool.StringToCSV(sb.ToString(), "card");
        }
        /// <summary>
        /// 导入淘宝商品数据
        /// </summary>
        public void taobao_product_in()
        {
            if (!EX_Admin.Power("prodesc_datainout", "导入导出"))
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
                        Lebi_Product pro = B_Lebi_Product.GetModel("Number=lbsql{'" + number + "'}");
                        if (pro == null)
                        {
                            pro = new Lebi_Product();
                            addflag = true;
                            pro.Number = dt.Rows[m]["outer_id"].ToString();
                            pro.Type_id_ProductStatus = 101;
                            pro.Type_id_ProductType = 320;
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
            if (!EX_Admin.Power("prodesc_datainout", "导入导出"))
            {
                AjaxNoPower();
                return;
            }
            string lang = RequestTool.RequestString("lang");
            SearchProduct sp = new SearchProduct(CurrentAdmin, CurrentLanguage.Code);
            string where = "Product_id=0 " + sp.SQL;
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
        /// <summary>
        /// 导出语言标签
        /// </summary>
        public void LanguageTag_Export()
        {
            if (!EX_Admin.Power("language_tag_list", "语言标签列表"))
            {
                AjaxNoPower();
                return;
            }
            int Type = RequestTool.RequestInt("Type",0);
            string tag = RequestTool.RequestString("languagecode");
            string where = "Tag !=''";
            string titles = "Tag|" + tag + "";
            if (Type == 1)
                titles = "ID|" + tag + "";
            string[] arr = titles.Split(',');
            DataTable dt = new DataTable();
            foreach (string col in arr)
            {
                dt.Columns.Add(col);
            }
            DataRow r = dt.NewRow();
            dt.Rows.Add(r);
            List<Lebi_Language_Tag> models = B_Lebi_Language_Tag.GetList(where, "id desc");
            StringBuilder sb = new StringBuilder();
            if (!Shop.LebiAPI.Service.Instanse.Check("managelicenese"))
            {
                sb.Append("LebiShop V" + SYS.Version + "." + SYS.Version_Son + " (http://www.lebi.cn),\r\n");
            }
            else
            {
                sb.Append("V" + SYS.Version + "." + SYS.Version_Son + ",\r\n");
            }
            sb.Append(titles + "\r\n");
            //sb.Append("标签,");
            //sb.Append(tag + "\r\n");
            foreach (Lebi_Language_Tag model in models)
            {
                if (Type == 1)
                    sb.AppendFormat("{0}|{1}", model.id, TagValue(model, tag));
                else
                    sb.AppendFormat("{0}|{1}", model.tag.ToString(), TagValue(model, tag));
                //sb.Append("\""+model.tag + "\"","\""+ TagValue(model, tag)+"\""");
                sb.Append("\r\n");
            }
            FileTool.StringTofile(sb.ToString(), tag, ".txt");
        }
        /// <summary>
        /// 导入语言标签
        /// </summary>
        public void LanguageTag_Import()
        {
            if (!EX_Admin.Power("language_tag_list", "语言标签列表"))
            {
                AjaxNoPower();
                return;
            }
            int Type = RequestTool.RequestInt("Type", 0);
            string tag = RequestTool.RequestString("languagecode");
            string uploadfile = RequestTool.RequestString("uploadfile");
            string fileName = HttpContext.Current.Server.MapPath(@"~/" + WebPath + uploadfile);
            int i = 0;
            if (File.Exists(fileName))
            {
                DataTable dt = new DataTable();
                char flag = '|';
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fs, Encoding.Default))
                    {
                        string text = string.Empty;
                        bool ldata = false;
                        while (!reader.EndOfStream)
                        {
                            text = reader.ReadLine();
                            
                            if (i == 0)
                            {
                                if (text.ToLower().IndexOf("tag") == 0)
                                {
                                    string[] arr = text.Split(flag);
                                    foreach (string col in arr)
                                    {
                                        dt.Columns.Add(col);
                                    }
                                    ldata = true;
                                }
                               
                            }
                            else if (i == 1)
                            {
                                if (text.ToLower().IndexOf("tag") == 0)
                                {
                                    string[] arr = text.Split(flag);
                                    foreach (string col in arr)
                                    {
                                        dt.Columns.Add(col);
                                    }
                                    ldata = true;
                                }

                            }
                            if (ldata)
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
                if (Type == 0)
                {
                    if (!dt.Columns.Contains("Tag"))
                    {
                        Response.Write("{\"msg\":\"请检查是否包含Tag字段\"}");
                        return;
                    }
                }
                int id = 0;
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    try
                    {
                        string where = "";
                        string Tag2 = dt.Rows[m]["" + tag + ""].ToString();
                        if (Type == 0)
                        {
                            string Tag = dt.Rows[m]["Tag"].ToString().Trim();
                            where = "Tag = lbsql{'" + Tag + "'}";
                            if (Tag == "")
                                continue;
                        }
                        else
                        {
                            string Tagid_ = dt.Rows[m]["ID"].ToString().Trim();
                            int Tagid = -1;
                            int.TryParse(Tagid_, out Tagid);
                            where = "id = lbsql{" + Tagid + "}";
                            if (Tagid == -1)
                                continue;
                        }
                        Lebi_Language_Tag model = B_Lebi_Language_Tag.GetModel(where);

                        if (model == null)
                        {
                            model = new Lebi_Language_Tag();
                        }
                        Type type = model.GetType();
                        System.Reflection.PropertyInfo p = (from t in type.GetProperties()
                                                            where t.Name.ToLower() == tag.ToLower()
                                                            select t).ToList().FirstOrDefault();
                        p.SetValue(model, Tag2, null);
                        if (Type == 0)
                        {
                            if (model.id == 0)
                            {
                                model.tag = dt.Rows[m]["Tag"].ToString().Trim();
                                B_Lebi_Language_Tag.Add(model);
                            }
                            else
                            {
                                B_Lebi_Language_Tag.Update(model);
                            }
                        }
                        else
                        {
                            if (model.id > 0)
                            {
                                B_Lebi_Language_Tag.Update(model);
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                Log.Add("导入语言标签", "Language_Tag", "");
                Response.Write("{\"msg\":\"OK\"}");
            }
            else
            {
                Response.Write("{\"msg\":\"" + Tag("数据文件错误") + "\"}");
                return;
            }
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
            else
            {
                sb.Append("V" + SYS.Version + "." + SYS.Version_Son + ",\r\n");
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
        /// <summary>
        /// 导出供应商订单数据（订单）
        /// </summary>
        public void SupplierOrder_Export()
        {
            if (!EX_Admin.Power("statis_orderproduct", "订单报表"))
            {
                PageNoPower();
            }
            string where = RequestTool.RequestString("where");
            where = Server.UrlDecode(where).Replace("&#180;", "'");

            string titles = "" + Tag("序号") + "," + Tag("供应商") + "," + Tag("配送点") + "," + Tag("发货日期") + "," + Tag("单号") + "," + Tag("订购日期") + "," + Tag("订单金额") + "," + Tag("应付金额") + "," + Tag("支付方式") + "," + Tag("在线支付") + "," + Tag("订单状态");
            string[] arr = titles.Split(',');
            DataTable dt = new DataTable();
            foreach (string col in arr)
            {
                dt.Columns.Add(col);
            }
            DataRow r = dt.NewRow();
            dt.Rows.Add(r);
            List<Lebi_Order> models = B_Lebi_Order.GetList(where, "id desc");
            StringBuilder sb = new StringBuilder();
            if (!Shop.LebiAPI.Service.Instanse.Check("managelicenese"))
            {
                sb.Append("LebiShop V" + SYS.Version + "." + SYS.Version_Son + " (http://www.lebi.cn),\r\n");
            }
            else
            {
                sb.Append("V" + SYS.Version + "." + SYS.Version_Son + ",\r\n");
            }
            sb.Append(titles + "\r\n");
            //sb.Append("标签,");
            //sb.Append(tag + "\r\n");
            int i = 0;
            Lebi_Supplier supplier;
            Lebi_Supplier_Delivery delivery;
            foreach (Lebi_Order order in models)
            {
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
                if (order.IsShipped == 1)
                {
                    sb.Append(order.Time_Shipped.ToString("yyyy-MM-dd HH:mm:ss") + ",");
                }
                else
                {
                    sb.Append("-,");
                }
                sb.Append(order.Code + ",");
                sb.Append(order.Time_Add.ToString("yyyy-MM-dd HH:mm:ss") + ",");
                sb.Append(order.Money_Order + ",");
                sb.Append(order.Money_Pay + ",");
                sb.Append(out_txt(Lang(order.Pay))+ ",");
                if (Lang(order.Pay) == "在线支付")
                {
                    sb.Append(out_txt(Lang(order.OnlinePay)) + ",");
                }
                else
                {
                    sb.Append(",");
                }
                sb.Append(Shop.Bussiness.Order.OrderStatus(order,CurrentLanguage.Code) + ",");
                sb.Append("\r\n");
            }
            FileTool.StringTofile(sb.ToString(), "order", ".csv");
        }
        /// <summary>
        /// 导出资金统计
        /// </summary>
        public void Statis_Money()
        {
            if (!EX_Admin.Power("statis_money", "资金统计"))
            {
                AjaxNoPower();
                return;
            }
            StringBuilder sb = new StringBuilder();
            if (!Shop.LebiAPI.Service.Instanse.Check("managelicenese"))
            {
                sb.Append("LebiShop V" + SYS.Version + "." + SYS.Version_Son + " (http://www.lebi.cn),,,,,,,,,,,,\r\n");
            }
            else
            {
                sb.Append("V" + SYS.Version + "." + SYS.Version_Son + ",\r\n");
            }
            int time = RequestTool.RequestInt("time", 0);
            int type = RequestTool.RequestInt("type", 0);
            switch (time)
            {
                case 0:
                    sb.Append(Tag("时间") + ",");
                    break;
                case 1:
                    sb.Append(Tag("时间") + ",");
                    break;
                case 2:
                    sb.Append(Tag("日期") + ",");
                    break;
                case 3:
                    sb.Append(Tag("日期") + ",");
                    break;
            }
            foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
            {
                sb.Append(model.Name + ",");
            }
            sb.Append(Tag("合计") + "\r\n");
            switch (type)
            {
                case 0:
                    if (time == 0)
                    {
                        var i = 1; for (i = 1; i <= 24; i++)
                        {
                            sb.Append("" + (i - 1) + ":00,");
                            decimal total = 0; foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
                            {
                                sb.Append(Shop.Bussiness.EX_User.GetMoney_UserMoney(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181", i - 1) + ",");
                                total += Shop.Bussiness.EX_User.GetMoney_UserMoney(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181", i - 1);
                            }
                            sb.Append(total + "\r\n");
                        }
                        sb.Append(Tag("合计") + ",");
                        foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
                        {
                            sb.Append(Shop.Bussiness.EX_User.GetMoney_UserMoney(System.DateTime.Now.AddDays(-29).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181") + ",");
                        }
                        sb.Append(Shop.Bussiness.EX_User.GetMoney_UserMoney(System.DateTime.Now.AddDays(-29).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_MoneyStatus = 181") + "\r\n");
                    }
                    if (time == 1)
                    {
                        var i = 1; for (i = 1; i <= 24; i++)
                        {
                            sb.Append("" + (i - 1) + ":00,");
                            decimal total = 0; foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
                            {
                                sb.Append(Shop.Bussiness.EX_User.GetMoney_UserMoney(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181", i - 1) + ",");
                                total += Shop.Bussiness.EX_User.GetMoney_UserMoney(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181", i - 1);
                            }
                            sb.Append(total + "\r\n");
                        }
                        sb.Append(Tag("合计") + ",");
                        foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
                        {
                            sb.Append(Shop.Bussiness.EX_User.GetMoney_UserMoney(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181") + ",");
                        }
                        sb.Append(Shop.Bussiness.EX_User.GetMoney_UserMoney(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), "Type_id_MoneyStatus = 181") + "\r\n");
                    }
                    if (time == 2)
                    {
                        var i = 1; for (i = 1; i <= 7; i++)
                        {
                            sb.Append("" + System.DateTime.Now.AddDays(-7+i).Month.ToString() +"-"+ System.DateTime.Now.AddDays(-7 + i).Day.ToString() + ",");
                            decimal total = 0; foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
                            {
                                sb.Append(Shop.Bussiness.EX_User.GetMoney_UserMoney(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181") + ",");
                                total += Shop.Bussiness.EX_User.GetMoney_UserMoney(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181");
                            }
                            sb.Append(total + "\r\n");
                        }
                        sb.Append(Tag("合计") + ",");
                        foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
                        {
                            sb.Append(Shop.Bussiness.EX_User.GetMoney_UserMoney(System.DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181") + ",");
                        }
                        sb.Append(Shop.Bussiness.EX_User.GetMoney_UserMoney(System.DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_MoneyStatus = 181") + "\r\n");
                    }
                    if (time == 3)
                    {
                        var i = 1; for (i = 1; i <= 30; i++)
                        {
                            sb.Append("" + System.DateTime.Now.AddDays(-30+i).Month.ToString() +"-"+ System.DateTime.Now.AddDays(-30 + i).Day.ToString() + ",");
                            decimal total = 0; foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
                            {
                                sb.Append(Shop.Bussiness.EX_User.GetMoney_UserMoney(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181") + ",");
                                total += Shop.Bussiness.EX_User.GetMoney_UserMoney(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181");
                            }
                            sb.Append(total + "\r\n");
                        }
                        sb.Append(Tag("合计") + ",");
                        foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
                        {
                            sb.Append(Shop.Bussiness.EX_User.GetMoney_UserMoney(System.DateTime.Now.AddDays(-29).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181") + ",");
                        }
                        sb.Append(Shop.Bussiness.EX_User.GetMoney_UserMoney(System.DateTime.Now.AddDays(-29).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_MoneyStatus = 181") + "\r\n");
                    }
                    break;
                case 1:
                    if (time == 0)
                    {
                        var i = 1; for (i = 1; i <= 24; i++)
                        {
                            sb.Append("" + (i - 1) + ":00,");
                            decimal total = 0; foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
                            {
                                sb.Append(Shop.Bussiness.EX_User.GetCount_UserMoney(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181", i - 1) + ",");
                                total += Shop.Bussiness.EX_User.GetCount_UserMoney(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181", i - 1);
                            }
                            sb.Append(total + "\r\n");
                        }
                        sb.Append(Tag("合计") + ",");
                        foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
                        {
                            sb.Append(Shop.Bussiness.EX_User.GetCount_UserMoney(System.DateTime.Now.AddDays(-29).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181") + ",");
                        }
                        sb.Append(Shop.Bussiness.EX_User.GetCount_UserMoney(System.DateTime.Now.AddDays(-29).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_MoneyStatus = 181") + "\r\n");
                    }
                    if (time == 1)
                    {
                        var i = 1; for (i = 1; i <= 24; i++)
                        {
                            sb.Append("" + (i - 1) + ":00,");
                            decimal total = 0; foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
                            {
                                sb.Append(Shop.Bussiness.EX_User.GetCount_UserMoney(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181", i - 1) + ",");
                                total += Shop.Bussiness.EX_User.GetCount_UserMoney(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181", i - 1);
                            }
                            sb.Append(total + "\r\n");
                        }
                        sb.Append(Tag("合计") + ",");
                        foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
                        {
                            sb.Append(Shop.Bussiness.EX_User.GetCount_UserMoney(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181") + ",");
                        }
                        sb.Append(Shop.Bussiness.EX_User.GetCount_UserMoney(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), "Type_id_MoneyStatus = 181") + "\r\n");
                    }
                    if (time == 2)
                    {
                        var i = 1; for (i = 1; i <= 7; i++)
                        {
                            sb.Append("" + System.DateTime.Now.AddDays(-7+i).Month.ToString() +"-"+ System.DateTime.Now.AddDays(-7 + i).Day.ToString() + ",");
                            decimal total = 0; foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
                            {
                                sb.Append(Shop.Bussiness.EX_User.GetCount_UserMoney(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181") + ",");
                                total += Shop.Bussiness.EX_User.GetCount_UserMoney(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181");
                            }
                            sb.Append(total + "\r\n");
                        }
                        sb.Append(Tag("合计") + ",");
                        foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
                        {
                            sb.Append(Shop.Bussiness.EX_User.GetCount_UserMoney(System.DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181") + ",");
                        }
                        sb.Append(Shop.Bussiness.EX_User.GetCount_UserMoney(System.DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_MoneyStatus = 181") + "\r\n");
                    }
                    if (time == 3)
                    {
                        var i = 1; for (i = 1; i <= 30; i++)
                        {
                            sb.Append("" + System.DateTime.Now.AddDays(-30+i).Month.ToString() +"-"+ System.DateTime.Now.AddDays(-30 + i).Day.ToString() + ",");
                            decimal total = 0; foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
                            {
                                sb.Append(Shop.Bussiness.EX_User.GetCount_UserMoney(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181") + ",");
                                total += Shop.Bussiness.EX_User.GetCount_UserMoney(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181");
                            }
                            sb.Append(total + "\r\n");
                        }
                        sb.Append(Tag("合计") + ",");
                        foreach (Lebi_Type model in Shop.Bussiness.EX_Type.GetTypes("MoneyType"))
                        {
                            sb.Append(Shop.Bussiness.EX_User.GetCount_UserMoney(System.DateTime.Now.AddDays(-29).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_MoneyType = " + model.id + " and Type_id_MoneyStatus = 181") + ",");
                        }
                        sb.Append(Shop.Bussiness.EX_User.GetCount_UserMoney(System.DateTime.Now.AddDays(-29).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_MoneyStatus = 181") + "\r\n");
                    }
                    break;
            }
            FileTool.StringToCSV(sb.ToString(), "Statis_Money_" + type + "_" + time + "");
        }
        /// <summary>
        /// 导出订单统计
        /// </summary>
        public void Statis_Order()
        {
            if (!EX_Admin.Power("statis_order", "订单统计"))
            {
                AjaxNoPower();
                return;
            }
            StringBuilder sb = new StringBuilder();
            if (!Shop.LebiAPI.Service.Instanse.Check("managelicenese"))
            {
                sb.Append("LebiShop V" + SYS.Version + "." + SYS.Version_Son + " (http://www.lebi.cn),,,,,,,,,,,,\r\n");
            }
            else
            {
                sb.Append("V" + SYS.Version + "." + SYS.Version_Son + ",\r\n");
            }
            int time = RequestTool.RequestInt("time", 0);
            int type = RequestTool.RequestInt("type", 0);
            switch (time)
            {
                case 0:
                    sb.Append(Tag("时间") + ",");
                    break;
                case 1:
                    sb.Append(Tag("时间") + ",");
                    break;
                case 2:
                    sb.Append(Tag("日期") + ",");
                    break;
                case 3:
                    sb.Append(Tag("日期") + ",");
                    break;
            }
            sb.Append(Tag("未确认") + ",");
            sb.Append(Tag("未支付") + ",");
            sb.Append(Tag("已支付") + ",");
            sb.Append(Tag("未发货") + ",");
            sb.Append(Tag("已发货") + ",");
            sb.Append(Tag("已收货") + ",");
            sb.Append(Tag("已完成") + "\r\n");
            switch (type)
            {
                case 0:
                    if (time == 0){
                        var i = 1;for(i=1; i<=24; i++){
                            sb.Append(""+ (i-1) +":00,");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"),System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"),"Type_id_OrderType = 211 and IsVerified = 0",i-1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsPaid = 0", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsPaid = 1", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsShipped = 0", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsShipped = 1", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsReceived = 1", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsCompleted = 1", i - 1) + "\r\n");
                        }
                    }
                    if (time == 1)
                    {
                        var i = 1; for (i = 1; i <= 24; i++)
                        {
                            sb.Append("" + (i - 1) + ":00,");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsVerified = 0", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsPaid = 0", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsPaid = 1", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsShipped = 0", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsShipped = 1", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsReceived = 1", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsCompleted = 1", i - 1) + "\r\n");
                        }
                    }
                    if (time == 2)
                    {
                        var i = 1;for(i=1; i<=7; i++)
                        {
                            sb.Append("" + System.DateTime.Now.AddDays(-7+i).Month.ToString() +"-"+ System.DateTime.Now.AddDays(-7+i).Day.ToString() + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsVerified = 0") + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsPaid = 0") + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsPaid = 1") + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsShipped = 0") + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsShipped = 1") + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsReceived = 1") + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsCompleted = 1") + "\r\n");
                        }
                    }
                    if (time == 3)
                    {
                        var i = 1; for (i = 1; i <= 30; i++)
                        {
                            sb.Append("" + System.DateTime.Now.AddDays(-30 + i).Month.ToString() + "-" + System.DateTime.Now.AddDays(-30 + i).Day.ToString() + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsVerified = 0") + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsPaid = 0") + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsPaid = 1") + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsShipped = 0") + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsShipped = 1") + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsReceived = 1") + ",");
                            sb.Append(Shop.Bussiness.Order.GetMoney_Order(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsCompleted = 1") + "\r\n");
                        }
                    }
                    break;
                case 1:
                    if (time == 0){
                        var i = 1;for(i=1; i<=24; i++){
                            sb.Append(""+ (i-1) +":00,");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"),System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"),"Type_id_OrderType = 211 and IsVerified = 0",i-1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsPaid = 0", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsPaid = 1", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsShipped = 0", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsShipped = 1", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsReceived = 1", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsCompleted = 1", i - 1) + "\r\n");
                        }
                    }
                    if (time == 1)
                    {
                        var i = 1; for (i = 1; i <= 24; i++)
                        {
                            sb.Append("" + (i - 1) + ":00,");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsVerified = 0", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsPaid = 0", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsPaid = 1", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsShipped = 0", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsShipped = 1", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsReceived = 1", i - 1) + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsCompleted = 1", i - 1) + "\r\n");
                        }
                    }
                    if (time == 2)
                    {
                        var i = 1;for(i=1; i<=7; i++)
                        {
                            sb.Append("" + System.DateTime.Now.AddDays(-7+i).Month.ToString() +"-"+ System.DateTime.Now.AddDays(-7+i).Day.ToString() + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsVerified = 0") + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsPaid = 0") + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsPaid = 1") + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsShipped = 0") + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsShipped = 1") + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsReceived = 1") + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-7 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsCompleted = 1") + "\r\n");
                        }
                    }
                    if (time == 3)
                    {
                        var i = 1; for (i = 1; i <= 30; i++)
                        {
                            sb.Append("" + System.DateTime.Now.AddDays(-30 + i).Month.ToString() + "-" + System.DateTime.Now.AddDays(-30 + i).Day.ToString() + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsVerified = 0") + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsPaid = 0") + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsPaid = 1") + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsShipped = 0") + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsShipped = 1") + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsReceived = 1") + ",");
                            sb.Append(Shop.Bussiness.Order.GetCount_Order(System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), System.DateTime.Now.AddDays(-30 + i).ToString("yyyy-MM-dd"), "Type_id_OrderType = 211 and IsCompleted = 1") + "\r\n");
                        }
                    }
                    break;
            }
            FileTool.StringToCSV(sb.ToString(), "Statis_Order_" + type + "_" + time + "");
        }
        /// <summary>
        /// 导出类别统计
        /// </summary>
        public void Statis_Category()
        {
            if (!EX_Admin.Power("statis_category", "类别统计"))
            {
                AjaxNoPower();
                return;
            }
            StringBuilder sb = new StringBuilder();
            if (!Shop.LebiAPI.Service.Instanse.Check("managelicenese"))
            {
                sb.Append("LebiShop V" + SYS.Version + "." + SYS.Version_Son + " (http://www.lebi.cn),,,,,,,,,,,,\r\n");
            }
            else
            {
                sb.Append("V" + SYS.Version + "." + SYS.Version_Son + ",,,,,,,,,,,,\r\n");
            }
            sb.Append(Tag("分类名称") + ",");
            sb.Append(Tag("商品数量") + ",");
            sb.Append(Tag("收藏数量") + ",");
            sb.Append(Tag("销量") + ",");
            sb.Append(Tag("浏览量") + "\r\n");
            sb.Append(CreateTree(0,0));
            FileTool.StringToCSV(sb.ToString(), "Statis_Category");
        }
        /// <summary>
        /// 导出分组统计
        /// </summary>
        public void Statis_UserLevel()
        {
            if (!EX_Admin.Power("statis_userlevel", "分组统计"))
            {
                AjaxNoPower();
                return;
            }
            StringBuilder sb = new StringBuilder();
            if (!Shop.LebiAPI.Service.Instanse.Check("managelicenese"))
            {
                sb.Append("LebiShop V" + SYS.Version + "." + SYS.Version_Son + " (http://www.lebi.cn),,,,,,,,,,,,\r\n");
            }
            else
            {
                sb.Append("V" + SYS.Version + "." + SYS.Version_Son + ",,,,,,,,,,,,\r\n");
            }
            sb.Append(Tag("会员分组") + ",");
            sb.Append(Tag("会员数量") + ",");
            sb.Append(Tag("余额") + ",");
            sb.Append(Tag("积分") + ",");
            sb.Append(Tag("订单") + ",");
            sb.Append(Tag("消费") + "\r\n");
            List<Lebi_UserLevel> models = B_Lebi_UserLevel.GetList("Grade > 0", "Grade asc");
            foreach (Lebi_UserLevel model in models)
            {
                sb.Append(Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code) + ",");
                sb.Append(Shop.Bussiness.EX_User.UserCount(model.id) + ",");
                sb.Append(Shop.Bussiness.EX_User.MoneyCount(model.id) + ",");
                sb.Append(Shop.Bussiness.EX_User.PointCount(model.id) + ",");
                sb.Append(Shop.Bussiness.EX_User.OrderCount(model.id) + ",");
                sb.Append(Shop.Bussiness.EX_User.Money_xiaofeiCount(model.id) + "\r\n");
            }
            FileTool.StringToCSV(sb.ToString(), "Statis_UserLevel");
        }
        /// <summary>
        /// 导出销售报表
        /// </summary>
        public void Statis_Sales()
        {
            if (!EX_Admin.Power("statis_sales", "销售报表"))
            {
                AjaxNoPower();
                return;
            }
            StringBuilder sb = new StringBuilder();
            if (!Shop.LebiAPI.Service.Instanse.Check("managelicenese"))
            {
                sb.Append("LebiShop V" + SYS.Version + "." + SYS.Version_Son + " (http://www.lebi.cn),,,,,,,,,,,,\r\n");
            }
            else
            {
                sb.Append("V" + SYS.Version + "." + SYS.Version_Son + ",,,,,,,,,,,,\r\n");
            }
            sb.Append(",");
            sb.Append(Tag("销售数量") + ",");
            sb.Append(Tag("订单金额") + ",");
            sb.Append(Tag("商品金额") + ",");
            sb.Append(Tag("运费") + ",");
            sb.Append(Tag("税金") + ",");
            if (Shop.Bussiness.EX_Admin.CheckPower("product_price_cost"))
            {
                sb.Append(Tag("成本") + ",");
                sb.Append(Tag("利润") + "\r\n");
            }
            else
            {
                sb.Append("\r\n");
            }
            int Pay_id = RequestTool.RequestInt("Pay_id", 0);
            int Transport_id = RequestTool.RequestInt("Transport_id", 0);
            DateTime dateFrom = Convert.ToDateTime(RequestTool.RequestString("dateFrom"));
            DateTime dateTo = Convert.ToDateTime(RequestTool.RequestString("dateTo"));
            int Y_dateFrom = dateFrom.Year;
            int M_dateFrom = dateFrom.Month;
            int D_dateFrom = dateFrom.Day;
            int Y_dateTo = dateTo.Year;
            int M_dateTo = dateTo.Month;
            int D_dateTo = dateTo.Day;
            var year = 1; for (year = Y_dateFrom; year <= Y_dateTo; year++)
            {
                int M_From = 0; int M_To = 0; int D_From = 0; int D_To = 0;
                if (year == Y_dateFrom)
                {
                    M_From = M_dateFrom;
                }
                else
                {
                    M_From = 1;
                }
                if (year == Y_dateTo)
                {
                    M_To = M_dateTo;
                    D_dateTo = dateTo.Day;
                }
                else
                {
                    M_To = 12;
                    DateTime get_lastday;
                    get_lastday = Convert.ToDateTime((year + 1) + "-1-1");
                    D_dateTo = get_lastday.AddDays(-1).Day;
                }
                string where_year = "Type_id_OrderType=211 and IsPaid = 1";
                where_year += " and Time_Add>='" + year + "-" + M_From + "-" + D_dateFrom + "' and Time_Add<='" + year + "-" + M_To + "-" + D_dateTo + "'";
                if (Pay_id > 0)
                    where_year += " and Pay_id = " + Pay_id;
                if (Transport_id > 0)
                    where_year += " and Transport_id = " + Transport_id;
                sb.Append(year + " " + Tag("年") + ",");
                decimal Order_Money = Shop.Bussiness.Statis.MoneyCount(where_year);
                decimal Money_Product = Shop.Bussiness.Statis.Money_ProductCount(where_year);
                decimal Money_Transport = Shop.Bussiness.Statis.Money_TransportCount(where_year);
                decimal Money_Bill = Shop.Bussiness.Statis.Money_BillCount(where_year);
                sb.Append(Shop.Bussiness.Statis.ProductCount(where_year) + ",");
                sb.Append(FormatMoney(Order_Money) + ",");
                sb.Append(FormatMoney(Money_Product) + ",");
                sb.Append(FormatMoney(Money_Transport) + ",");
                sb.Append(FormatMoney(Money_Bill) + ",");
                if (Shop.Bussiness.EX_Admin.CheckPower("product_price_cost"))
                {
                    decimal Money_Cost = Shop.Bussiness.Statis.Money_CostCount(where_year);
                    sb.Append(FormatMoney(Money_Cost) + ",");
                    sb.Append(FormatMoney(Order_Money - Money_Transport - Money_Bill - Money_Cost) + "\r\n");
                }
                else
                {
                    sb.Append("\r\n");
                }
                var m_i = 1; for (m_i = M_From; m_i <= M_To; m_i++)
                {
                    if (m_i == M_dateFrom && year == Y_dateFrom)
                    {
                        D_From = D_dateFrom;
                    }
                    else
                    {
                        D_From = 1;
                    }
                    if (m_i == M_dateTo && year == Y_dateTo)
                    {
                        D_To = D_dateTo;
                    }
                    else
                    {
                        int day_i = 1; int str_y_i = 0; int str_m_i = 0;
                        DateTime get_lastday;
                        str_m_i = m_i + 1;
                        str_y_i = year;
                        if (str_m_i == 13)
                        {
                            str_m_i = 1;
                            str_y_i = year + 1;
                        }
                        get_lastday = Convert.ToDateTime(str_y_i + "-" + str_m_i + "-" + day_i);
                        D_To = get_lastday.AddDays(-1).Day;
                    }
                    string m_i_DateFrom; string m_i_DateTo;
                    m_i_DateFrom = FormatDate(Convert.ToDateTime(year + "-" + m_i + "-" + D_From));
                    m_i_DateTo = FormatDate(Convert.ToDateTime(year + "-" + m_i + "-" + D_To));
                    string where = "Type_id_OrderType=211 and IsPaid = 1";
                    where += " and Time_Add>='" + m_i_DateFrom + "' and Time_Add<='" + m_i_DateTo + "'";
                    if (Pay_id > 0)
                        where += " and Pay_id = " + Pay_id;
                    if (Transport_id > 0)
                        where += " and Transport_id = " + Transport_id;
                    Response.Write("<tr class=\"list\" name=\"tr" + year + "_" + m_i + "\">");
                    sb.Append("  " + m_i + " " + Tag("月") + ",");
                    Order_Money = Shop.Bussiness.Statis.MoneyCount(where);
                    Money_Product = Shop.Bussiness.Statis.Money_ProductCount(where);
                    Money_Transport = Shop.Bussiness.Statis.Money_TransportCount(where);
                    Money_Bill = Shop.Bussiness.Statis.Money_BillCount(where);
                    sb.Append(Shop.Bussiness.Statis.ProductCount(where) + ",");
                    sb.Append(FormatMoney(Order_Money) + ",");
                    sb.Append(FormatMoney(Money_Product) + ",");
                    sb.Append(FormatMoney(Money_Transport) + ",");
                    sb.Append(FormatMoney(Money_Bill) + ",");
                    if (Shop.Bussiness.EX_Admin.CheckPower("product_price_cost"))
                    {
                        decimal Money_Cost = Shop.Bussiness.Statis.Money_CostCount(where);
                        sb.Append(FormatMoney(Money_Cost) + ",");
                        sb.Append(FormatMoney(Order_Money - Money_Transport - Money_Bill - Money_Cost) + "\r\n");
                        //sb.Append(FormatMoney(Shop.Bussiness.Statis.Money_ProfitCount(where)) + "\r\n");
                    }
                    else
                    {
                        sb.Append("\r\n");
                    }
                    var d_i = 1; for (d_i = D_From; d_i <= D_To; d_i++)
                    {
                        string d_datefrom = FormatDate(Convert.ToDateTime(year + "-" + m_i + "-" + d_i));
                        string d_dateto = FormatDate(Convert.ToDateTime(year + "-" + m_i + "-" + d_i));
                        string where_day = "Type_id_OrderType=211 and IsPaid = 1";
                        where_day += " and Time_Add>='" + d_datefrom + "' and Time_Add<='" + d_dateto + "'";
                        if (Pay_id > 0)
                            where_day += " and Pay_id = " + Pay_id;
                        if (Transport_id > 0)
                            where_day += " and Transport_id = " + Transport_id;
                        sb.Append("    " + d_i + " " + Tag("日") + ",");
                        Order_Money = Shop.Bussiness.Statis.MoneyCount(where_day);
                        Money_Product = Shop.Bussiness.Statis.Money_ProductCount(where_day);
                        Money_Transport = Shop.Bussiness.Statis.Money_TransportCount(where_day);
                        Money_Bill = Shop.Bussiness.Statis.Money_BillCount(where_day);
                        sb.Append(Shop.Bussiness.Statis.ProductCount(where_day) + ",");
                        sb.Append(FormatMoney(Order_Money) + ",");
                        sb.Append(FormatMoney(Money_Product) + ",");
                        sb.Append(FormatMoney(Money_Transport) + ",");
                        sb.Append(FormatMoney(Money_Bill) + ",");
                        if (Shop.Bussiness.EX_Admin.CheckPower("product_price_cost"))
                        {
                            decimal Money_Cost = Shop.Bussiness.Statis.Money_CostCount(where_day);
                            sb.Append(FormatMoney(Money_Cost) + ",");
                            sb.Append(FormatMoney(Order_Money - Money_Transport - Money_Bill - Money_Cost) + "\r\n");
                        }
                        else
                        {
                            sb.Append("\r\n");
                        }
                    }
                }
            }
            FileTool.StringToCSV(sb.ToString(), "Statis_Sales");
        }
        private string findpath(int D_From, int D_To, string Name)
        {
            string str = "";
            var d_i = 1; for (d_i = D_From; d_i <= D_To; d_i++)
            {
                str += "," + Name + "_" + d_i;
            }
            return str;
        }
        public string CreateTree(int pid,int deep)
        {
            string str = "";
            List<Lebi_Pro_Type> types = EX_Product.Types(pid);
            foreach (Lebi_Pro_Type t in types)
            {
                str += deepstr(deep) + Language.Content(t.Name, CurrentLanguage.Code) + ",";
                str += Shop.Bussiness.EX_Product.TypeProductCount(t.id) + ",";
                str += Shop.Bussiness.EX_Product.LikeProductCount(t.id) + ",";
                str += Shop.Bussiness.EX_Product.SalesProductCount(t.id) + ",";
                str += Shop.Bussiness.EX_Product.ViewsProductCount(t.id) + "";
                str += "\r\n";
                int showson = B_Lebi_Pro_Type.Counts("Parentid = "+ t.id +"");
                if (showson > 0)
                    str += CreateTree(t.id, deep + 1);
            }
            return str;
        }
        private string deepstr(int deep)
        {
            string str = "";
            for (int i = 0; i < deep; i++)
            {
                str += "  ";
            }
            return str;
        }
    }
}