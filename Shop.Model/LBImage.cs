using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Model
{
    
    public class LBimage
    {

        public LBimage() { }

        private string _small;
        public string small
        {
            get { return _small; }
            set { _small = value; }
        }
        private string _medium;
        public string medium
        {
            get { return _medium; }
            set { _medium = value; }
        }
        private string _big;
        public string big
        {
            get { return _big; }
            set { _big = value; }
        }
        private string _original;
        public string original
        {
            get { return _original; }
            set { _original = value; }
        }
        
        
    }
}
