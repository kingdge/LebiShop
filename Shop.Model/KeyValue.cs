using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Model
{
    
    public class KeyValue
    {

        public KeyValue() {
            _K = "";
            _V = "";
        }

        private string _K;
        /**
         * ¼ükey
         */
        public string K
        {
            get { return _K; }
            set { _K = value; }
        }
        private string _V;
        /**
         * Öµvalue
         */
        public string V
        {
            get { return _V; }
            set { _V = value; }
        }
        
    }
}
