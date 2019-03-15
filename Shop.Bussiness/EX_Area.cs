using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.Script.Serialization;

namespace Shop.Bussiness
{
    public class EX_Area
    {
        /// <summary>
        /// 返回一个AREA的所有父id（不含自己）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string Parentids_Get(int area_id)
        {
            string ids = "";
            Lebi_Area area = B_Lebi_Area.GetModel(area_id);
            if (area != null)
            {
                ids = area.Parentid.ToString();
                if (area.Parentid > 0)
                {
                    ids += "," + Parentids_Get(area.Parentid);
                }
            }
            return ids;
        }
        /// <summary>
        /// 返回一个AREA的最后一个父id（不含自己）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int Parentids_Get(int area_id, int path)
        {
            int id = 0;
            Lebi_Area area = B_Lebi_Area.GetModel(area_id);
            if (area == null)
            {
                return area_id;
            }
            else
            {
                id = area.Parentid;
                if (area.Parentid > 0)
                {
                    id = Parentids_Get(area.Parentid, path);
                }
            }
            return id;
        }
        /// <summary>
        /// 根据配送地区取出运输公司并计算相应运费
        /// </summary>
        public static List<Lebi_Transport_Price> TransportPrices_Get(int area_id, int supplierid)
        {

            Lebi_Area area = B_Lebi_Area.GetModel(area_id);
            if (area == null)
            {
                return new List<Lebi_Transport_Price>();
            }
            List<Lebi_Transport_Price> trans = null;//配送地区含当前地区的运输公司以及价格
            trans = TransportPrice(area, trans, supplierid);
            foreach (Lebi_Transport_Price p in trans)
            {
                Lebi_Transport t = B_Lebi_Transport.GetModel(p.Transport_id);
                if (t != null)
                    p.Sort = t.Sort;
            }
            trans = trans.OrderByDescending(a => a.Sort).ToList();
            return trans;

        }
        /// <summary>
        /// 递归处理运费价格
        /// </summary>
        /// <param name="area"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        private static List<Lebi_Transport_Price> TransportPrice(Lebi_Area area, List<Lebi_Transport_Price> trans, int supplierid)
        {
            if (trans == null)
            {
                trans = B_Lebi_Transport_Price.GetList("Area_id=" + area.id + " and Supplier_id=" + supplierid + "", "");
            }
            else
            {
                string pids = "";
                List<Lebi_Transport_Price> models = B_Lebi_Transport_Price.GetList("Area_id=" + area.id + " and Supplier_id=" + supplierid + "", "");
                foreach (Lebi_Transport_Price model in models)
                {
                    //排除包含的关系
                    //跳过儿子，孙子在列表中的情况
                    bool flag = false;
                    foreach (Lebi_Transport_Price tran in trans)
                    {
                        pids = EX_Area.Parentids_Get(tran.Area_id);
                        pids = "," + pids + ",";
                        if (pids.Contains("," + model.Area_id + ",") && model.Transport_id == tran.Transport_id)
                            flag = true;
                    }
                    if (!flag)
                        trans.Add(model);
                }
            }
            if (area.Parentid > 0)
            {
                area = B_Lebi_Area.GetModel(area.Parentid);
                if (area != null)
                    trans = TransportPrice(area, trans, supplierid);
            }
            return trans;
        }
        /// <summary>
        /// 返回地区名称目录
        /// </summary>
        /// <param name="area_id"></param>
        /// <param name="deep">目录深度</param>
        /// <returns></returns>
        public static string GetAreaName(int area_id, int deep)
        {
            string res = "";
            deep--;
            Lebi_Area area = B_Lebi_Area.GetModel(area_id);
            if (area == null)
                return res;
            res = area.Name;
            if (deep > 0)
                res = GetAreaName(area.Parentid, deep) + " " + res;
            return res;
        }
        public static string GetAreaNameDesc(int area_id, int deep)
        {
            string res = "";
            deep--;
            Lebi_Area area = B_Lebi_Area.GetModel(area_id);
            if (area == null)
                return res;
            res = area.Name;
            if (deep > 0)
            {
                Lebi_Area areacheck = B_Lebi_Area.GetModel(area.Parentid);
                if (areacheck != null)
                    res = res + ",";
                res = res + GetAreaNameDesc(area.Parentid, deep);
            }
            return res;
        }
        public static string GetAreaNameNoPath(int area_id, int deep)
        {
            string res = "";
            deep--;
            Lebi_Area area = B_Lebi_Area.GetModel(area_id);
            if (area == null)
                return res;
            res = area.Name;
            if (area.Parentid > 0 && deep > 0)
            {
                res = GetAreaNameNoPath(area.Parentid, deep);
                return res;
            }
            DateTime dt = System.DateTime.Now.Date.AddDays(0 - System.DateTime.Now.Day);
            return res;
        }
        /// <summary>
        /// 返回全部地名目录
        /// </summary>
        /// <param name="area_id"></param>
        /// <returns></returns>
        public static string GetAreaName(int area_id)
        {
            return GetAreaName(area_id, 999);
        }
        /// <summary>
        /// 返回地区名称目录
        /// </summary>
        /// <param name="area_id"></param>
        /// <param name="deep">目录深度 0</param>
        /// <param name="loop">显示层级</param>
        /// <returns></returns>
        public static string GetAreaName(int area_id, int deep, int loop)
        {
            string res = "";
            loop++;
            Lebi_Area area = B_Lebi_Area.GetModel(area_id);
            if (area == null)
                return res;
            res = area.Name;
            if (area.Parentid > 0 && loop < 5)
            {
                res = GetAreaName(area.Parentid, deep, loop);
            }
            return res;
        }
        /// <summary>
        /// 获取指定运输公司，指定区域的价格
        /// </summary>
        /// <returns></returns>
        public static Lebi_Transport_Price GetAreaPrice(int transport_id, int area_id, int supplierid)
        {
            Lebi_Transport_Price price = B_Lebi_Transport_Price.GetModel("Transport_id=" + transport_id + " and Area_id=" + area_id + " and Supplier_id=" + supplierid + "");
            if (price == null)
            {
                Lebi_Area area = B_Lebi_Area.GetModel(area_id);
                if (area != null)
                {
                    if (area.Parentid > 0)
                        return GetAreaPrice(transport_id, area.Parentid, supplierid);
                    return null;
                }
                return null;
            }
            return price;
        }
        /// <summary>
        /// 验证运输区域与运费区域是否匹配
        /// </summary>
        /// <returns></returns>
        public static bool CheckAreaPrice(Lebi_Transport_Price price, int area_id)
        {
            if (price == null)
                return false;
            Lebi_Transport_Price p = GetAreaPrice(price.Transport_id, area_id, price.Supplier_id);
            if (p != null)
            {
                if (p.id == price.id)
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// 计算运费
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="volume"></param>
        /// <param name="price"></param>
        /// <param name="ordermoney"></param>
        /// <returns></returns>
        public static decimal GetYunFei(decimal weight, decimal volume, Lebi_Transport_Price price, decimal ordermoney)
        {
            if (price == null)
                price = new Lebi_Transport_Price();
            decimal res = 0;
            if (ordermoney >= price.OrderMoney && price.OrderMoney > 0)//满足订单金额要求
                return price.Price_OrderMoneyOK;
            if (price.IsOnePrice == 1)//定额运费
                return price.Price;
            else
            {
                Lebi_Transport transport = B_Lebi_Transport.GetModel(price.Transport_id);
                if (transport == null)
                    return 0;
                decimal money = price.Price;
                if (transport.Type_id_TransportType == 331)
                {

                    //货柜方式计算
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    List<KeyValue> kvs = jss.Deserialize<List<KeyValue>>(price.Container);
                    string ids = "";
                    foreach (KeyValue kv in kvs)
                    {
                        if (ids == "")
                            ids = kv.K;
                        else
                            ids += "," + kv.K;
                    }
                    int count = 0;//需要的货柜数量
                    decimal cprice = 0;//使用的货柜价格
                    if (ids != "")
                    {
                        List<Lebi_Transport_Container> conts = B_Lebi_Transport_Container.GetList("id in (lbsql{" + ids + "})", "Volume desc");
                        //判断使用哪个货柜
                        Lebi_Transport_Container UseCont = new Lebi_Transport_Container();
                        foreach (Lebi_Transport_Container cont in conts)
                        {
                            if (volume / 100 / 100 / 100 > cont.Volume)
                            {
                                UseCont = cont;
                                break;
                            }
                        }
                        if (UseCont.id == 0)
                            UseCont = conts.FirstOrDefault();
                        UseCont.Weight = UseCont.Weight * 1000 * 1000;//单位转换为克
                        UseCont.Volume = UseCont.Volume * 100 * 100 * 100;//单位转换为立方厘米
                        KeyValue kv = (from m in kvs
                                       where m.K == UseCont.id.ToString()
                                       select m).ToList().FirstOrDefault();
                        cprice = Convert.ToDecimal(kv.V);
                        if (weight > UseCont.Weight)
                        {
                            //按重量计算
                            count = (int)(weight / UseCont.Weight);
                            if (weight % UseCont.Weight > 0)
                                count++;

                        }
                        else
                        {
                            //按体积计算
                            count = (int)(volume / UseCont.Volume);
                            if (volume % UseCont.Volume > 0)
                                count++;
                        }
                    }
                    count = count == 0 ? 1 : count;
                    money = money + cprice * count;
                }
                else
                { //包裹方式计算
                    int wei = (int)weight + 1;
                    if (weight > price.Weight_Start)
                    {
                        try
                        {
                            decimal step = (weight - price.Weight_Start) / price.Weight_Step;
                            step = Math.Ceiling(step);
                            money = money + step * price.Price_Step;
                        }
                        catch (DivideByZeroException)
                        {
                            // money = money; 
                        }
                    }
                }

                res = money;
            }
            return res;
        }
        public static decimal GetYunFei(List<Lebi_User_Product> pros, Lebi_Transport_Price price)
        {
            return GetYunFei(pros, price, 0);
        }
        public static decimal GetYunFei(List<Lebi_User_Product> pros, Lebi_Transport_Price price, decimal ordermoney = 0)
        {
            decimal weight = 0;
            decimal tiji = 0;
            foreach (Lebi_User_Product pro in pros)
            {
                Lebi_Product product = B_Lebi_Product.GetModel(pro.Product_id);
                if (product == null)
                    continue;
                weight = weight + product.Weight * pro.count;//总重量：单位：克
                tiji = tiji + product.VolumeL * product.VolumeH * product.VolumeW * pro.count;//总体积：单位：立方厘米
            }
            return GetYunFei(weight, tiji, price, ordermoney);
        }
        /// <summary>
        /// 根据订单商品计算运费
        /// </summary>
        /// <param name="pros"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal GetYunFei(List<Lebi_Order_Product> pros, Lebi_Transport_Price price)
        {
            decimal weight = 0;
            decimal tiji = 0;
            decimal money = 0;
            foreach (Lebi_Order_Product pro in pros)
            {
                Lebi_Product product = B_Lebi_Product.GetModel(pro.Product_id);
                if (product == null)
                    continue;
                weight = weight + product.Weight * pro.Count;
                tiji = tiji + product.VolumeL * product.VolumeH * product.VolumeW * pro.Count;//总体积：单位：立方厘米
                money = money + pro.Price * pro.Count;
            }
            return GetYunFei(weight, tiji, price, money);
        }

        /// <summary>
        /// 获得运费备注
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="volume"></param>
        /// <param name="price"></param>
        /// <param name="ordermoney"></param>
        /// <returns></returns>
        public static string GerYunFeiMark(decimal weight, decimal volume, Lebi_Transport_Price price, decimal ordermoney = 0)
        {
            string res = "";
            if (price.IsOnePrice == 1 && ordermoney >= price.OrderMoney)//满足订单金额要求
                return "";
            else
            {
                Lebi_Transport transport = B_Lebi_Transport.GetModel(price.Transport_id);
                decimal money = price.Price;
                if (transport.Type_id_TransportType == 331)
                {

                    //货柜方式计算
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    List<KeyValue> kvs = jss.Deserialize<List<KeyValue>>(price.Container);
                    string ids = "";
                    foreach (KeyValue kv in kvs)
                    {
                        if (ids == "")
                            ids = kv.K;
                        else
                            ids += "," + kv.K;
                    }
                    int count = 0;//需要的货柜数量
                    decimal cprice = 0;//使用的货柜价格
                    if (ids != "")
                    {
                        List<Lebi_Transport_Container> conts = B_Lebi_Transport_Container.GetList("id in (lbsql{" + ids + "})", "Volume desc");
                        //判断使用哪个货柜
                        Lebi_Transport_Container UseCont = new Lebi_Transport_Container();
                        foreach (Lebi_Transport_Container cont in conts)
                        {
                            if (volume / 100 / 100 / 100 > cont.Volume)
                            {
                                UseCont = cont;
                                break;
                            }
                        }
                        if (UseCont.id == 0)
                            UseCont = conts.FirstOrDefault();
                        UseCont.Weight = UseCont.Weight * 1000 * 1000;//单位转换为克
                        UseCont.Volume = UseCont.Volume * 100 * 100 * 100;//单位转换为立方厘米
                        KeyValue kv = (from m in kvs
                                       where m.K == UseCont.id.ToString()
                                       select m).ToList().FirstOrDefault();
                        cprice = Convert.ToDecimal(kv.V);
                        if (weight > UseCont.Weight)
                        {
                            //按重量计算
                            count = (int)(weight / UseCont.Weight);
                            if (weight % UseCont.Weight > 0)
                                count++;

                        }
                        else
                        {
                            //按体积计算
                            count = (int)(volume / UseCont.Volume);
                            if (volume % UseCont.Volume > 0)
                                count++;
                        }
                        count = count == 0 ? 1 : count;
                        res = UseCont.Name + "：" + count;
                    }
                }
                else
                {
                }
            }
            return res;
        }
        /// <summary>
        /// 返回运输方式的下拉选项
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public static string TransportOption(int sid)
        {
            string res = "";
            List<Lebi_Transport> trans = B_Lebi_Transport.GetList("", "Sort desc");
            foreach (Lebi_Transport model in trans)
            {
                res += "<option value=\"" + model.id + "\" " + (sid == model.id ? "selected" : "") + " >" + model.Name + "</option>";
            }
            return res;
        }
        /// <summary>
        /// 返回快递100物流数据
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public static KuaiDi100 GetKuaiDi100(Lebi_Transport_Order torder)
        {
            string json;
            KuaiDi100 log = new KuaiDi100();
            BaseConfig conf = ShopCache.GetBaseConfig();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (torder.Type_id_TransportOrderStatus == 223)
            {
                json = torder.Log;
                log = jss.Deserialize<KuaiDi100>(json);
            }
            else
            {

                try
                {
                    string url = "http://www.kuaidi100.com/api?id=" + conf.KuaiDi100 + "&com=" + torder.Transport_Code + "&nu=" + torder.Code + "&show=0&muti=1";
                    json = HtmlEngine.CetHtml(url);
                    log = jss.Deserialize<KuaiDi100>(json);
                    switch (log.state)
                    {
                        case "1":
                            torder.Type_id_TransportOrderStatus = 221;
                            break;
                        case "2":
                            torder.Type_id_TransportOrderStatus = 222;
                            break;
                        case "3":
                            torder.Type_id_TransportOrderStatus = 223;
                            try
                            {
                                torder.Time_Received = Convert.ToDateTime(log.data.FirstOrDefault().time);
                            }
                            catch
                            {
                                torder.Time_Received = System.DateTime.Now;
                            }
                            break;
                        case "4":
                            torder.Type_id_TransportOrderStatus = 224;
                            break;
                        //default:
                        //    //torder.Type_id_TransportOrderStatus = 220;
                        //    break;

                    }
                    if (log.message != "ok")
                    {
                        //没有成功获取到json数据
                        url = "http://www.kuaidi100.com/applyurl?key=" + conf.KuaiDi100 + "&com=" + torder.Transport_Code + "&nu=" + torder.Code;
                        string html = HtmlEngine.CetHtml(url);
                        //html = HtmlEngine.CetHtml(html);
                        torder.HtmlLog = html;
                    }
                    torder.Log = json;
                    B_Lebi_Transport_Order.Update(torder);
                    UpdateShouHuoCount(torder);
                }
                catch (Exception)
                {
                    log = new KuaiDi100();
                }
            }
            if (log == null)
                log = new KuaiDi100();
            if (log.data == null)
                log.data = new List<KuaiDi100.KuaiDi100data>();
            return log;
        }
        /// <summary>
        /// 收货确认后，更新订单产品的收货数量
        /// </summary>
        /// <param name="torder"></param>
        public static void UpdateShouHuoCount(Lebi_Transport_Order torder)
        {
            if (torder.Type_id_TransportOrderStatus != 223)
                return;
            Lebi_Order order = B_Lebi_Order.GetModel(torder.Order_id);
            if (order == null)
                return;
            List<Lebi_Order_Product> ops = B_Lebi_Order_Product.GetList("Order_id=" + torder.Order_id + "", "");
            List<Lebi_Transport_Order> torders = B_Lebi_Transport_Order.GetList("Order_id=" + torder.Order_id + " and Type_id_TransportOrderStatus=223", "");
            //bool shouhuoall = true;
            foreach (Lebi_Order_Product op in ops)
            {
                //op.Count_Received = 0;
                foreach (Lebi_Transport_Order to in torders)
                {
                    foreach (TransportProduct p in GetTransportProduct(to))
                    {
                        if (p.Product_id == op.Product_id)
                        {
                            op.Count_Received = op.Count_Received + p.Count;
                            if (op.Count_Shipped < op.Count_Received)
                                op.Count_Received = op.Count_Shipped;
                            break;
                        }
                    }
                }
                //if (op.Count_Received < op.Count_Shipped)
                //    shouhuoall = false;
                B_Lebi_Order_Product.Update(op);//更新收货数量
            }
            order.IsReceived = 1;
            order.IsReceived_All = 1;
            ops = B_Lebi_Order_Product.GetList("Order_id=" + torder.Order_id + "", "");
            foreach (Lebi_Order_Product op in ops)
            {
                if (op.Count_Received < op.Count_Shipped)
                    order.IsReceived_All = 0;
            }
            order.Time_Received = System.DateTime.Now;
            B_Lebi_Order.Update(order);
            if (order.IsReceived_All == 1)//全部收货完成
                Order.Received(order);
        }

        /// <summary>
        /// 返回货运单中的商品数据
        /// </summary>
        /// <param name="torder"></param>
        /// <returns></returns>
        public static List<TransportProduct> GetTransportProduct(Lebi_Transport_Order torder)
        {
            List<TransportProduct> tps = new List<TransportProduct>();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                tps = jss.Deserialize<List<TransportProduct>>(torder.Product);
            }
            catch (Exception)
            {
                tps = new List<TransportProduct>();
            }
            return tps;
        }
        /// <summary>
        /// 返回一个地区下的所有子地区
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public static string Area_ids(int tid)
        {
            string ids = tid.ToString();
            List<Lebi_Area> ts = B_Lebi_Area.GetList("Parentid=" + tid + "", "");
            foreach (Lebi_Area t in ts)
            {
                ids += "," + Area_ids(t.id);
            }
            return ids;
        }
    }

}

