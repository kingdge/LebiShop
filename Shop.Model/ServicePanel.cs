using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Model
{
    [Serializable]
    public class ServicePanel
    {
        public ServicePanel() { }

        public string X
        {
            get;
            set;
        }
        public string Y
        {
            get;
            set;
        }
        public string Theme
        {
            get;
            set;
        }
        public string Status
        {
            get;
            set;
        }

        public string IsFloat
        {
            get;
            set;
        }

        public string Style
        {
            get;
            set;
        }
        
    }
}
