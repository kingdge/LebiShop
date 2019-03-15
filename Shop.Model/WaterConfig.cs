using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Model
{
    [Serializable]
    public class WaterConfig
    {
        public WaterConfig() { }

        public string OnAndOff
        {
            get;
            set;
        }
        public string PicAndText
        {
            get;
            set;
        }
        public string WM_PicPath
        {
            get;
            set;
        }
        public string WM_Text
        {
            get;
            set;
        }

        public string WM_Transparence
        {
            get;
            set;
        }

        public string WM_Font
        {
            get;
            set;
        }
        public string WM_FontSize
        {
            get;
            set;
        }
        public string WM_FontColor
        {
            get;
            set;
        }

        public string WM_FontForm
        {
            get;
            set;
        }


        public string WM_PlaceX
        {
            get;
            set;
        }
        public string WM_PlaceY
        {
            get;
            set;
        }



        public string WM_Width
        {
            get;
            set;
        }
        public string WM_Height
        {
            get;
            set;
        }
        /*
        public string Width_Big
        {
            get { return "800"; }
        }
        public string Height_Big
        {
            get { return "800"; }
        }
        public string Width_Medium
        {
            get { return "300"; }
        }
        public string Height_Medium
        {
            get { return "300"; }
        }
        public string Width_Small
        {
            get { return "100"; }
        }
        public string Height_Small
        {
            get { return "100"; }
        }
        */

        public string WM_Location
        {
            get;
            set;
        }



    }
}
