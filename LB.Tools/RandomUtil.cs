using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LB.Tools
{
    public class RandomUtil
    {

        public static List<string> GetCard(int count, string Num01, string Num02, string Num03, string Num04)
        {
            List<string> list = new List<string>();
            string item = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";
            if ((((Num01.Length != 0) && (Num02.Length != 0)) && ((Num03.Length != 0) && (Num03 != "1"))) && ((Num04.Length != 0) && (Num04 != "1")))
            {
                item = Num01 + "-" + Num02 + "-" + Num03 + "-" + Num04;
                list.Add(item);
                return list;
            }
            for (int i = 1; (i <= count) && (i < 0x2710); i++)
            {
                str2 = Num01;
                if (Num01.Length == 0)
                {
                    str2 = RndString(4);
                }
                else if (Num01.Length != 4)
                {
                    return new List<string>();
                }
                str3 = Num02;
                if (Num02.Length == 0)
                {
                    str3 = RndString(4);
                }
                else if (Num02.Length != 4)
                {
                    return new List<string>();
                }
                str4 = Num03;
                str5 = Num04;
                if ((Num03 == "1") && (Num04 == "1"))
                {
                    item = str2 + "-" + str3 + "-0000-" + i.ToString().PadLeft(4, '0');
                }
                else if (Num03 == "1")
                {
                    if (Num04.Length == 0)
                    {
                        str5 = RndString(4);
                    }
                    else if ((Num04.Length != 4) && (Num04 != "1"))
                    {
                        return new List<string>();
                    }
                    item = str2 + "-" + str3 + "-" + i.ToString().PadLeft(4, '0') + "-" + str5;
                }
                else if (Num04 == "1")
                {
                    if (Num03.Length == 0)
                    {
                        str4 = RndString(4);
                    }
                    else if ((Num03.Length != 4) && (Num03 != "1"))
                    {
                        return new List<string>();
                    }
                    item = str2 + "-" + str3 + "-" + str4 + "-" + i.ToString().PadLeft(4, '0');
                }
                else
                {
                    if (Num03.Length == 0)
                    {
                        str4 = RndString(4);
                    }
                    else if ((Num03.Length != 4) && (Num03 != "1"))
                    {
                        return new List<string>();
                    }
                    if (Num04.Length == 0)
                    {
                        str5 = RndString(4);
                    }
                    else if ((Num04.Length != 4) && (Num04 != "1"))
                    {
                        return new List<string>();
                    }
                    item = str2 + "-" + str3 + "-" + str4 + "-" + str5;
                }
                if (list.Contains(item.ToUpper()))
                {
                    //重复
                    i = i - 1;
                    continue;
                }
                list.Add(item.ToUpper());
            }
            return list;
        }

        public static string RndNum(int n)
        {
            string str = "";
            int num = 0;
            HardTime time = new HardTime();
            Random random = new Random(time.GetAbsoluteIntTime());
            for (int i = 0; i < n; i++)
            {
                num = random.Next(10);
                str = str + num.ToString().Trim();
            }
            return str;
        }

        public static string RndString(int n)
        {
            string str = "";
            int num = 0;
            HardTime time = new HardTime();
            Random random = new Random(time.GetAbsoluteIntTime());
            for (int i = 0; i < n; i++)
            {
                num = random.Next(0x3e);
                if ((num >= 0) && (num < 10))
                {
                    str = str + num.ToString().Trim();
                }
                else if (num < 0x24)
                {
                    str = str + Convert.ToChar((int)(num + 0x37));
                }
                else if (num < 0x3e)
                {
                    str = str + Convert.ToChar((int)(num + 0x3d));
                }
                else
                {
                    i--;
                }
            }
            return str;
        }
   
    }
}
