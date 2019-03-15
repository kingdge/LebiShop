using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Model
{
    [Serializable]
    public class TransportProduct : ICloneable
    {
       
        public int Product_id
        {
            get;
            set;
        }
        public string Product_Name
        {
            get;
            set;
        }
        public string Product_Number
        {
            get;
            set;
        }
        public int Count
        {
            get;
            set;
        }
        public string ImageSmall
        {
            get;
            set;
        }
        public string ImageMedium
        {
            get;
            set;
        }
        public string ImageBig
        {
            get;
            set;
        }
        public string ImageOriginal
        {
            get;
            set;
        }
        public List<TransportProduct> Son
        {
            get;
            set;
        }
        public TransportProduct()
        {
            Product_id = 0;
            Product_Name = "";
            Product_Number = "";
            Count = 0;
            ImageSmall = "";
            ImageMedium = "";
            ImageBig = "";
            ImageOriginal = "";
            Son = new List<TransportProduct>();
        }

        public object Clone()
        {
            //return this as object;          //引用同一个对象
            return this.MemberwiseClone();  //浅复制
            //(在C#中可以使用MemberwiseClone()方法来实现浅copy在C#中可以使用MemberwiseClone()方法来实现浅copy)
            //return new DrawBase() as object; //深复制
        }
    }
}
