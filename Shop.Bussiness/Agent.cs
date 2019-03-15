using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;
using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.Script.Serialization;
using System.Collections.Specialized;

namespace Shop.Bussiness
{
    public class Agent
    {
        public bool UsedAgent = false;
        public bool UsedAgentArea = false;
        public bool UsedAgentProduct = false;
        private Lebi_Order Order;
        private Lebi_User OrderUser;
        private BaseConfig bc;
        //private int EndDays;
        public class Model
        {
            public decimal Money { get; set; }
            public int UserCount { get; set; }
            public int UserCountmonth { get; set; }
            public int UserCountday { get; set; }
            public decimal Commission1 { get; set; }
            public decimal Commission2 { get; set; }
            public decimal Commission3 { get; set; }
        }
        public Agent(Lebi_Order order)
        {
            bc = ShopCache.GetBaseConfig();
            if (bc.IsUsedAgent == "1")
                UsedAgent = true;
            if (bc.IsUsedAgent_Area == "1")
                UsedAgentArea = true;
            if (bc.IsUsedAgent_Product == "1")
                UsedAgentProduct = true;
            Order = order;
            OrderUser = B_Lebi_User.GetModel(order.User_id);
            //EndDays = Convert.ToInt32(bc.AgentEndDays);
        }
        public Agent(Lebi_Order order, Lebi_User user)
        {
            bc = ShopCache.GetBaseConfig();
            if (bc.IsUsedAgent == "1")
                UsedAgent = true;
            if (bc.IsUsedAgent_Area == "1")
                UsedAgentArea = true;
            if (bc.IsUsedAgent_Product == "1")
                UsedAgentProduct = true;
            Order = order;
            OrderUser = user;
            //EndDays = Convert.ToInt32(bc.AgentEndDays);
        }
        public static Model Info(Lebi_User user)
        {
            BaseConfig bc = ShopCache.GetBaseConfig();
            Model m = new Model();
            decimal Money = 0;
            int d = 0;
            int.TryParse(bc.CommissionMoneyDays,out d);
            d = 0 - d;
            string money_ = Common.GetValue("select sum(Money) from Lebi_Agent_Money where User_id=" + user.id + " and Type_id_AgentMoneyStatus=382 and Time_add<'" + System.DateTime.Now.AddDays(d).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            decimal.TryParse(money_, out Money);
            int UserCount = 0;
            UserCount = B_Lebi_User.Counts("User_id_parent=" + user.id + "");
            int UserCountmonth = 0;
            UserCountmonth = B_Lebi_User.Counts("User_id_parent=" + user.id + " and Time_Reg>='" + System.DateTime.Now.Date.AddDays(0 - System.DateTime.Now.Day + 1).ToString("yyyy-MM-dd") + "' and Time_Reg<='" + System.DateTime.Now + "'");
            int UserCountday = 0;
            UserCountday = B_Lebi_User.Counts("User_id_parent=" + user.id + " and Time_Reg>='" + System.DateTime.Now.Date.ToString("yyyy-MM-dd") + "' and Time_Reg<='" + System.DateTime.Now + "'");
            decimal Commission1 = 0;
            decimal Commission2 = 0;
            decimal Commission3 = 0;
            decimal.TryParse(bc.Angent1_Commission, out Commission1);
            decimal.TryParse(bc.Angent2_Commission, out Commission2);
            decimal.TryParse(bc.Angent3_Commission, out Commission3);
            if (user.DT_id == 0)
            {
                Lebi_Agent_UserLevel agent_userlevel = B_Lebi_Agent_UserLevel.GetModel("UserLevel_id = " + user.UserLevel_id + "");
                if (agent_userlevel != null)
                {
                    if (agent_userlevel.Angent1_Commission != -1)
                        Commission1 = agent_userlevel.Angent1_Commission;
                    if (agent_userlevel.Angent2_Commission != -1)
                        Commission2 = agent_userlevel.Angent2_Commission;
                    if (agent_userlevel.Angent3_Commission != -3)
                        Commission3 = agent_userlevel.Angent3_Commission;
                }
                Lebi_Agent_User agent_user = B_Lebi_Agent_User.GetModel("User_id = " + user.id + "");
                if (agent_user != null)
                {
                    if (agent_user.Angent1_Commission != -1)
                        Commission1 = agent_user.Angent1_Commission;
                    if (agent_user.Angent2_Commission != -1)
                        Commission2 = agent_user.Angent2_Commission;
                    if (agent_user.Angent3_Commission != -1)
                        Commission3 = agent_user.Angent3_Commission;
                }
            }
            else
            {
                Lebi_DT_Agent dt_agent = B_Lebi_DT_Agent.GetModel("DT_id = " + user.DT_id + "");
                if (dt_agent != null)
                {
                    if (dt_agent.IsUsedAgent == 1)
                    {
                        Commission1 = dt_agent.Angent1_Commission;
                        Commission2 = dt_agent.Angent2_Commission;
                        Commission3 = dt_agent.Angent3_Commission;
                    }
                }
            }
            m.Money = Money;
            m.UserCount = UserCount;
            m.UserCountmonth = UserCountmonth;
            m.UserCountday = UserCountday;
            m.Commission1 = Commission1;
            m.Commission2 = Commission2;
            m.Commission3 = Commission3;
            Lebi_UserLevel userlevel = B_Lebi_UserLevel.GetModel(user.UserLevel_id);
            if (userlevel != null)
            {
                if (userlevel.IsUsedAgent == 0) //关闭注册
                {
                    m.Commission1 = 0;
                    m.Commission2 = 0;
                    m.Commission3 = 0;
                }
            }
            return m;
        }
        public static int UserCount(Lebi_User user)
        {
            int UserCount = 0;
            UserCount = B_Lebi_User.Counts("User_id_parent=" + user.id + "");
            return UserCount;
        }
        public static int UserCountmonth(Lebi_User user)
        {
            int UserCountmonth = 0;
            UserCountmonth = B_Lebi_User.Counts("User_id_parent=" + user.id + " and Time_Reg>'" + System.DateTime.Now.Date.AddDays(0 - System.DateTime.Now.Day).ToString("yyyy-MM-dd") + "'");
            return UserCountmonth;
        }
        public static int UserCountday(Lebi_User user)
        {
            int UserCountday = 0;
            UserCountday = B_Lebi_User.Counts("User_id_parent=" + user.id + " and Time_Reg>'" + System.DateTime.Now.Date.ToString("yyyy-MM-dd") + "'");
            return UserCountday;
        }
        /// <summary>
        /// 计算佣金
        /// </summary>
        public void Operation()
        {
            SystemLog.Add("计算佣金" + UsedAgent.ToString());
            if (UsedAgent)
                AgentMoney();
            //if (UsedAgentArea)
            //    AgentAreaMoney();
            //if (UsedAgentProduct)
            //    AgentProductMoney();
        }
        /// <summary>
        /// 推广佣金
        /// </summary>
        private void AgentMoney()
        {
            //SystemLog.Add("计算佣金orderid" + Order.id + "userid" + OrderUser.User_id_parent);
            SystemLog.Add("推广佣金-开始");
            if (OrderUser.User_id_parent == 0)
                return;
            //string Money_Product_Profit_ = Common.GetValue("select sum((Price - Price_Cost)*(Count-Count_Return)) from Lebi_Order_Product where Order_id = " + Order.id + "");
            string Money_Product_Profit_ = Common.GetValue("select sum((Price - Price_Cost)*Count) from Lebi_Order_Product where Order_id = " + Order.id + "");
            decimal Money_Product_Profit = 0;
            decimal.TryParse(Money_Product_Profit_, out Money_Product_Profit);
            if (Order.Type_id_OrderType == 212)
                Money_Product_Profit = 0 - Money_Product_Profit;//zhangshijia  退货时产生负数佣金
            //处理一级代理
            Lebi_User user1 = B_Lebi_User.GetModel(OrderUser.User_id_parent);
            if (user1 == null)
                return;
            decimal yongjin1 = 0;//1级佣金比例
            decimal yongjin2 = 0;//2级佣金比例
            decimal yongjin3 = 0;//3级佣金比例
            Lebi_DT_Agent dt_agent = B_Lebi_DT_Agent.GetModel(0);
            Lebi_User DT_User = new Lebi_User();
            Lebi_DT dt = new Lebi_DT();
            int CommissionLevel = 3;
            if (Order.Supplier_id > 0)
            {
                Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(Order.Supplier_id);
                if (supplier.IsSpread == 0)  //商家未开启
                {
                    SystemLog.Add("推广佣金-商家未开启");
                    return;
                }
            }
            if (Order.DT_id > 0)
            {
                Money_Product_Profit = Order.DT_Money;  //如果是分销商，利润为分销商佣金
                dt_agent = B_Lebi_DT_Agent.GetModel("DT_id=" + Order.DT_id + "");
                dt = B_Lebi_DT.GetModel(Order.DT_id);
                if (dt != null)
                {
                    CommissionLevel = dt.CommissionLevel;
                }
                DT_User = B_Lebi_User.GetModel(dt.User_id);
                if (dt_agent.IsUsedAgent == 0)  //分销商未开启
                {
                    SystemLog.Add("推广佣金-分销商未开启");
                    return;
                }
                //  Money_Product_Profit = Order.DT_Money; ///zhangshijia注释，未找到DT_Money来源
                //List<Lebi_Order_Product> order_pro = B_Lebi_Order_Product.GetModel(" Order_id = " + Order.id + "");
                //foreach (Lebi_Order_Product op in order_pro)
                //{

                //}
            }
            yongjin1 = GetAngentCommission1(dt_agent, user1);
            decimal money = Money_Product_Profit * yongjin1 / 100;
            if (Order.DT_id > 0)
            {
                if (DT_User.Money < money)
                {
                    SystemLog.Add("推广佣金-分销商["+ DT_User.NickName + "-"+ DT_User.id + "]金额不足，一级佣金生成失败");
                    return;
                }
            }
            AddMoney(money, user1, 391, 0, 0, 0);
            //SystemLog.Add("计算佣金" + user1.UserName + money.ToString());
            if (Order.DT_id > 0)
            {
                AddMoney(0 - money, DT_User, 391, 0, 0, 0);
            }
            //处理2级代理
            Lebi_User user2 = B_Lebi_User.GetModel(user1.User_id_parent);
            if (user2 == null)
            {
                user2 = new Lebi_User();
            }
            else { 
                if (CommissionLevel >= 2)
                {
                    yongjin2 = GetAngentCommission2(dt_agent, user2);
                    money = Money_Product_Profit * yongjin2 / 100;
                    if (Order.DT_id > 0)
                    {
                        if (DT_User.Money < money)
                        {
                            SystemLog.Add("推广佣金-分销商金额不足，二级佣金生成失败");
                            return;
                        }
                    }
                    AddMoney(money, user2, 391, 0, 0, 0);
                    if (Order.DT_id > 0)
                    {
                        AddMoney(0 - money, DT_User, 391, 0, 0, 0);
                    }
                }
            }
            //处理3级代理
            if (CommissionLevel == 2)
            {
                return;
            }
            Lebi_User user3 = B_Lebi_User.GetModel(user2.User_id_parent);
            if (user3 != null)
            {
                yongjin3 = GetAngentCommission3(dt_agent, user3);
                money = Money_Product_Profit * yongjin3 / 100;
                if (Order.DT_id > 0)
                {
                    if (DT_User.Money < money)
                    {
                        SystemLog.Add("推广佣金-分销商金额不足，三级佣金生成失败");
                        return;
                    }
                }
                AddMoney(money, user3, 391, 0, 0, 0);
                if (Order.DT_id > 0)
                {
                    AddMoney(0 - money, DT_User, 391, 0, 0, 0);
                }
            }
            ////处理条件佣金，佣金历史达到X时，可获得下线，下下线Y%的佣金
               //decimal Angent_Commission_require = 0;
               //decimal.TryParse(bc.Angent_Commission_require, out Angent_Commission_require);
               //decimal yongjin3 = 0;//条件佣金比例
               //decimal.TryParse(bc.Angent_Commission, out yongjin3);
               //money = Money_Product_Profit * yongjin3 / 100;
               //if (user1.AgentMoney_history > Angent_Commission_require)
               //    AddMoney(money, user1, 395, 0, 0, 0);
               //if (user2.AgentMoney_history > Angent_Commission_require)
               //    AddMoney(money, user2, 395, 0, 0, 0);
        }
        private decimal GetAngentCommission1(Lebi_DT_Agent dt_agent, Lebi_User user)
        {
            if (user != null)
            {
                Lebi_UserLevel userlevel = B_Lebi_UserLevel.GetModel(user.UserLevel_id);
                if (userlevel != null)
                {
                    if (userlevel.IsUsedAgent == 0) //关闭推广佣金
                    {
                        return 0;
                    }
                }
                if (dt_agent != null)
                {
                    if (dt_agent.IsUsedAgent == 0)
                    {
                        return 0;
                    }
                    if (dt_agent.Angent1_Commission != -1)
                    {
                        return dt_agent.Angent1_Commission;
                    }
                }
                Lebi_Agent_User agent_user = B_Lebi_Agent_User.GetModel("User_id = " + user.id + "");
                if (agent_user != null)
                {
                    if (agent_user.Angent1_Commission != -1)
                        return agent_user.Angent1_Commission;
                }
                Lebi_Agent_UserLevel agent_userlevel = B_Lebi_Agent_UserLevel.GetModel("UserLevel_id = " + user.UserLevel_id + "");
                if (agent_userlevel != null)
                {
                    if (agent_userlevel.Angent1_Commission != -1)
                        return agent_userlevel.Angent1_Commission;
                }
            }
            decimal yongjin = 0;//1级佣金比例
            decimal.TryParse(bc.Angent1_Commission, out yongjin);
            return yongjin;
        }
        private decimal GetAngentCommission2(Lebi_DT_Agent dt_agent, Lebi_User user)
        {
            if (user != null)
            {
                Lebi_UserLevel userlevel = B_Lebi_UserLevel.GetModel(user.UserLevel_id);
                if (userlevel != null)
                {
                    if (userlevel.IsUsedAgent == 0) //关闭推广佣金
                    {
                        return 0;
                    }
                }
                if (dt_agent != null)
                {
                    if (dt_agent.IsUsedAgent == 0)
                    {
                        return 0;
                    }
                    if (dt_agent.Angent2_Commission != -1)
                    {
                        return dt_agent.Angent2_Commission;
                    }
                }
                Lebi_Agent_User agent_user = B_Lebi_Agent_User.GetModel("User_id = " + user.id + "");
                if (agent_user != null)
                {
                    if (agent_user.Angent2_Commission != -1)
                        return agent_user.Angent2_Commission;
                }
                Lebi_Agent_UserLevel agent_userlevel = B_Lebi_Agent_UserLevel.GetModel("UserLevel_id = " + user.UserLevel_id + "");
                if (agent_userlevel != null)
                {
                    if (agent_userlevel.Angent2_Commission != -1)
                        return agent_userlevel.Angent2_Commission;
                }
            }
            decimal yongjin = 0;//2级佣金比例
            decimal.TryParse(bc.Angent2_Commission, out yongjin);
            return yongjin;
        }
        private decimal GetAngentCommission3(Lebi_DT_Agent dt_agent, Lebi_User user)
        {
            if (user != null)
            {
                Lebi_UserLevel userlevel = B_Lebi_UserLevel.GetModel(user.UserLevel_id);
                if (userlevel != null)
                {
                    if (userlevel.IsUsedAgent == 0) //关闭推广佣金
                    {
                        return 0;
                    }
                }
                if (dt_agent != null)
                {
                    if (dt_agent.IsUsedAgent == 0)
                    {
                        return 0;
                    }
                    if (dt_agent.Angent3_Commission != -1)
                    {
                        return dt_agent.Angent3_Commission;
                    }
                }
                Lebi_Agent_User agent_user = B_Lebi_Agent_User.GetModel("User_id = " + user.id + "");
                if (agent_user != null)
                {
                    if (agent_user.Angent3_Commission != -1)
                        return agent_user.Angent3_Commission;
                }
                Lebi_Agent_UserLevel agent_userlevel = B_Lebi_Agent_UserLevel.GetModel("UserLevel_id = " + user.UserLevel_id + "");
                if (agent_userlevel != null)
                {
                    if (agent_userlevel.Angent3_Commission != -1)
                        return agent_userlevel.Angent3_Commission;
                }
            }
            decimal yongjin = 0;//3级佣金比例
            decimal.TryParse(bc.Angent3_Commission, out yongjin);
            return yongjin;
        }
        /// <summary>
        /// 处理地区代理佣金
        /// </summary>
        private void AgentAreaMoney()
        {
            if (Order.T_Area_id > 0)
            {
                Lebi_Agent_Area agentarea = GetAgentArea(Order.T_Area_id);
                if (agentarea != null)
                {
                    if (agentarea.Time_end.Date > System.DateTime.Now.Date)
                    {
                        Lebi_User user = B_Lebi_User.GetModel(agentarea.User_id);
                        if (user != null)
                        {
                            //按籍贯的佣金
                            decimal yongjin = agentarea.Commission_1;
                            decimal money = Order.Money_Product * yongjin / 100;
                            AddMoney(money, user, 393, agentarea.Area_id, 0, 0);
                        }
                    }
                }
            }

            if (OrderUser.Area_id > 0)
            {
                Lebi_Agent_Area agentarea = GetAgentArea(OrderUser.Area_id);
                if (agentarea != null)
                {
                    if (agentarea.Time_end.Date > System.DateTime.Now.Date)
                    {
                        Lebi_User user = B_Lebi_User.GetModel(agentarea.User_id);
                        if (user != null)
                        {
                            //按籍贯的佣金
                            decimal yongjin = agentarea.Commission_2;
                            decimal money = Order.Money_Product * yongjin / 100;
                            AddMoney(money, user, 394, agentarea.Area_id, 0, 0);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 查询对于的代理资格设置
        /// </summary>
        /// <param name="areaid"></param>
        /// <returns></returns>
        private Lebi_Agent_Area GetAgentArea(int areaid)
        {
            Lebi_Agent_Area agentarea = B_Lebi_Agent_Area.GetModel("Area_id=" + areaid + " and IsFailure=0");
            if (agentarea != null)
                return agentarea;
            else
            {
                Lebi_Area area = B_Lebi_Area.GetModel(areaid);
                if (area != null)
                    return GetAgentArea(area.Parentid);
            }
            return null;
        }

        /// <summary>
        /// 单品代理佣金
        /// </summary>
        private void AgentProductMoney()
        {
            List<Lebi_Order_Product> Products = B_Lebi_Order_Product.GetList("Order_id=" + Order.id + "", "");
            if (Products == null)
                return;
            int pid = 0;
            decimal money = 0;
            Lebi_Agent_Product_User auser;
            foreach (Lebi_Order_Product model in Products)
            {
                Lebi_Product pro = B_Lebi_Product.GetModel(model.Product_id);
                pid = pro.Product_id == 0 ? pro.id : pro.Product_id;
                Lebi_Agent_Product apro = B_Lebi_Agent_Product.GetModel("Product_id=" + pid + "");
                if (apro == null)
                    continue;
                auser = B_Lebi_Agent_Product_User.GetModel("User_id=" + apro.User_id + " and IsFailure=0");
                if (auser == null)
                    continue;
                if (auser.Time_end.Date < System.DateTime.Now.Date)
                    continue;
                Lebi_User user = B_Lebi_User.GetModel(apro.User_id);
                if (user == null)
                    continue;
                money = model.Price * model.Count * auser.Commission / 100;
                AddMoney(money, user, 392, 0, model.Product_id, pid, pro.Number);
            }
        }
        /// <summary>
        /// 添加佣金记录
        /// </summary>
        /// <param name="money"></param>
        /// <param name="user"></param>
        /// <param name="order"></param>
        /// <param name="type"></param>
        /// <param name="status"></param>
        /// <param name="areaid"></param>
        /// <param name="productid"></param>
        /// <param name="productparentid"></param>
        private void AddMoney(decimal money, Lebi_User user, int type, int areaid, int productid, int productparentid, string productnumber = "")
        {
            if (money == 0)
            {
                SystemLog.Add("金额：" + money + "，会员：" + user.id + "，订单：" + Order.Code + "");
                return;
            }
            int IsSpread = 1;
            if (Order.Supplier_id > 0)
            {
                Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(Order.Supplier_id);
                if (supplier != null)
                    IsSpread = supplier.IsSpread;
            }
            if (IsSpread == 1) //判断是否开启推广佣金 by lebi.kingdge 2015-04-10
            {
                Lebi_Agent_Money model = new Lebi_Agent_Money();
                model.Area_id = areaid;
                model.Money = money;
                model.Type_id_AgentMoneyType = type;
                if (Order.Type_id_OrderType == 212)//退货单
                {
                    model.Money = 0 - money;
                }
                model.Order_Code = Order.Code;
                model.Order_id = Order.id;
                model.Product_id = productid;
                model.Product_id_parent = productparentid;
                model.Type_id_AgentMoneyStatus = 381;
                model.Product_Number = productnumber;
                model.User_id = user.id;
                model.User_UserName = user.UserName;
                model.Supplier_id = Order.Supplier_id;
                model.DT_id = user.DT_id;
                B_Lebi_Agent_Money.Add(model);
                UpdateUserMoney(user, model);
            }
        }

        /// <summary>
        /// 更新会员帐户的佣金金额
        /// </summary>
        /// <param name="user"></param>
        /// <param name="money"></param>
        private void UpdateUserMoney(Lebi_User user, Lebi_Agent_Money money)
        {
            if (money.Type_id_AgentMoneyStatus == 382)//已生效佣金
            {
                user.AgentMoney_history += money.Money;
                user.AgentMoney += money.Money;
                B_Lebi_User.Update(user);
            }
        }

        /// <summary>
        /// 使一个购买单的佣金生效
        /// 完结订单时使用
        /// </summary>
        /// <param name="order"></param>
        public void AgentMoneyOK()
        {
            //string sql = "update Lebi_Agent_Money set Type_id_AgentMoneyType=382 where Order_id=" + order.id + "";
            //Common.ExecuteSql(sql);
            List<Lebi_Agent_Money> models = B_Lebi_Agent_Money.GetList("Order_id=" + Order.id + "", "");
            foreach (Lebi_Agent_Money model in models)
            {
                model.Type_id_AgentMoneyStatus = 382;
                Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                if (user != null)
                {
                    UpdateUserMoney(user, model);
                    B_Lebi_Agent_Money.Update(model);
                }
            }

        }
        /// <summary>
        /// 使一个购买单的佣金失效
        /// 已完结订单取消完结时使用
        /// </summary>
        /// <param name="order"></param>
        public void AgentMoneyCancal()
        {
            //string sql = "update Lebi_Agent_Money set Type_id_AgentMoneyType=383 where Order_id=" + order.id + "";
            //Common.ExecuteSql(sql);
            List<Lebi_Agent_Money> models = B_Lebi_Agent_Money.GetList("Order_id=" + Order.id + "", "");
            foreach (Lebi_Agent_Money model in models)
            {
                model.Type_id_AgentMoneyStatus = 381;
                Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                if (user != null)
                {
                    B_Lebi_Agent_Money.Update(model);
                    user.AgentMoney_history -= model.Money;
                    user.AgentMoney -= model.Money;
                    B_Lebi_User.Update(user);
                    //model.Money = 0 - model.Money;
                    //UpdateUserMoney(user, model);
                }
            }
        }
        /// <summary>
        /// 删除一个订单的佣金记录
        /// 已审核订单取消审核的情况下使用
        /// </summary>
        /// <param name="order"></param>
        public static void AgentMoneyDelete(Lebi_Order order)
        {
            B_Lebi_Agent_Money.Delete("Order_id=" + order.id + "");
        }

        /// <summary>
        /// 处理区域代理过期
        /// </summary>
        public static void AgentAreaEnd()
        {
            int d = 0;
            int.TryParse(ShopCache.GetBaseConfig().AgentEndDays,out d);
            d = 0 - d;
            List<Lebi_Agent_Area> areas = B_Lebi_Agent_Area.GetList("User_id>0 and Time_end<'" + System.DateTime.Now.Date.AddDays(d) + "')", "");
            foreach (Lebi_Agent_Area area in areas)
            {
                area.User_id = 0;
                area.User_UserName = "";
                B_Lebi_Agent_Area.Update(area);
            }
        }
        /// <summary>
        /// 处理商品代理过期
        /// </summary>
        public static void AgentProductEnd()
        {
            int d = 0;
            int.TryParse(ShopCache.GetBaseConfig().AgentEndDays,out d);
            d = 0 - d;
            List<Lebi_Agent_Product_User> users = B_Lebi_Agent_Product_User.GetList("User_id>0 and Time_end<'" + System.DateTime.Now.Date.AddDays(d) + "'", "");
            foreach (Lebi_Agent_Product_User user in users)
            {
                //user.IsFailure = 1;
                B_Lebi_Agent_Product_User.Delete(user.id);
                //清除代理商品绑定
                Common.ExecuteSql("update [Lebi_Agent_Product] set User_id=0,User_UserName='' where User_id=" + user.id + "");
            }
        }
        public static Lebi_Agent_UserLevel UserLevel_Commission(int id)
        {
            Lebi_Agent_UserLevel model = B_Lebi_Agent_UserLevel.GetModel("UserLevel_id = " + id + "");
            if (model == null)
                model = new Lebi_Agent_UserLevel();
            return model;
        }
        public static Lebi_Agent_User User_Commission(int id)
        {
            Lebi_Agent_User model = B_Lebi_Agent_User.GetModel("User_id = " + id + "");
            if (model == null)
                model = new Lebi_Agent_User();
            return model;
        }
    }
}

