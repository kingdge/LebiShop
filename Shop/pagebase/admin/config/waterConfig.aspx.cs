using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;

namespace Shop.Admin.storeConfig
{
    public partial class WaterConfig_Edit : AdminPageBase
    {
        protected WaterConfig model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("watermark_edit", "水印设置"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            B_WaterConfig bconfig = new B_WaterConfig();
            model = bconfig.LoadConfig();
        }

        public string getfont(string sel)
        {

            string str = "";
            System.Drawing.Text.InstalledFontCollection font;
            font = new System.Drawing.Text.InstalledFontCollection();
            foreach (System.Drawing.FontFamily family in font.Families)
            {
                //ddlfont.Items.Add(family.Name);
                str += "<option value=\"" + family.Name + "\">" + family.Name + "</option>";
            }
            return str;

        }

        public string getcolor(string sel)
        {

            string str = "";
            //Type type = typeof(System.Drawing.Color);
            //foreach (System.Reflection.PropertyInfo m in type.GetProperties())
            //{
            //    str += "<option value=\"" + m.Name + "\">" + m.Name + "</option>";
            //}
            Array colors = System.Enum.GetValues(typeof(colors));
            foreach (object m in colors)
            {
                str += "<option style=\"background-color:" + m.ToString() + "\" value=\"" + m.ToString() + "\">" + m.ToString() + "</option>";
            }
            return str;

        }
        enum colors
        {
            AliceBlue,
            AntiqueWhite,
            Aqua,
            Aquamarine,
            Azure,
            Bisque,
            Black,
            BlanchedAlmond,
            Blue,
            BlueViolet,
            Brown,
            BurlyWood,
            CadetBlue,
            Chartreuse,
            Chocolate,
            Coral,
            CornflowerBlue,
            Cornsilk,
            Crimson,
            Cyan,
            DarkBlue,
            DarkCyan,
            DarkGoldenrod,
            DarkGray,
            DarkGreen,
            DarkKhaki,
            DarkMagenta,
            DarkOliveGreen,
            DarkOrange,
            DarkOrchid,
            DarkRed,
            DarkSalmon,
            DarkSeaGreen,
            DarkSlateBlue,
            DarkSlateGray,
            DarkTurquoise,
            DarkViolet,
            DeepPink,
            DeepSkyBlue,
            DimGray,
            DodgerBlue,
            Firebrick,
            FloralWhite,
            ForestGreen,
            Fuchsia,
            Gainsboro,
            GhostWhite,
            Gold,
            Goldenrod,
            Gray,
            Green,
            GreenYellow,
            Honeydew,
            HotPin,
            IndianRed,
            ndigo,
            Ivory,
            Khaki,
            Lavender,
            LavenderBlush,
            LawnGreen,
            LemonChiffon,
            LightBlue,
            LightCoral,
            LightCyan,
            LightGoldenrodYellow,
            LightGray,
            LightGreen,
            LightPink,
            LightSalmon,
            LightSeaGreen,
            LightSkyBlue,
            LightSlateGray,
            LightSteelBlue,
            LightYellow,
            Lime,
            LimeGreen,
            Linen,
            Magenta,
            Maroon,
            MediumAquamarine,
            MediumBlue,
            MediumOrchid,
            MediumPurple,
            MediumSeaGreen,
            MediumSlateBlue,
            MediumSpringGreen,
            MediumTurquoise,
            MediumVioletRed,
            MidnightBlue,
            MintCream,
            MistyRose,
            Moccasin,
            NavajoWhite,
            Navy,
            OldLace,
            Olive,
            OliveDrab,
            Orange,
            OrangeRed,
            Orchid,
            PaleGoldenrod,
            PaleGreen,
            PaleTurquoise,
            PaleVioletRed,
            PapayaWhip,
            PeachPuff,
            Peru,
            Pink,
            Plum,
            PowderBlue,
            Purple,
            Red,
            RosyBrown,
            RoyalBlue,
            SaddleBrown,
            Salmon,
            SandyBrown,
            SeaGreen,
            SeaShell,
            Sienna,
            Silver,
            SkyBlue,
            SlateBlue,
            SlateGray,
            Snow,
            SpringGreen,
            SteelBlue,
            Tan,
            Teal,
            Thistle,
            Tomato,
            Transparent,
            Turquoise,
            Violet,
            Wheat,
            White,
            WhiteSmoke,
            Yellow,
            YellowGreen
        }
    }
}