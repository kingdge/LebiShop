using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using ThoughtWorks.QRCode;
using System.Drawing;
using ThoughtWorks.QRCode.Codec;
namespace LB.Tools
{
    public class QRCode
    {

        #region 静态实例
        private static QRCode _Instance;
        public static QRCode Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new QRCode();
                }
                return _Instance;
            }
            set
            {
                _Instance = value;
            }
        }
        #endregion
        #region 二维码生成
        /// <summary>  
        /// 批量生成二维码图片 png格式 
        /// </summary>  
        public void CreateImage(string instr, string filename)
        {
            //生成图片  
            Bitmap image = Create_ImgCode(instr, 10);
            //保存图片  
            SaveImg(currentPath, filename, image);
        }
        public Bitmap CreateImage(string instr)
        {
            //生成图片  
            Bitmap image = Create_ImgCode(instr, 10);
            return image;
        }

        //程序路径  
        readonly string currentPath = System.Web.HttpContext.Current.Server.MapPath("~/") + @"\qrcode";

        /// <summary>  
        /// 保存图片  
        /// </summary>  
        /// <param name="strPath">保存路径</param>  
        /// <param name="img">图片</param>  
        private void SaveImg(string strPath, string filename, Bitmap img)
        {
            //保存图片到目录  
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            string guid = filename + ".png";
            img.Save(strPath + "/" + guid, System.Drawing.Imaging.ImageFormat.Png);

        }
        /// <summary>  
        /// 生成二维码图片  
        /// </summary>  
        /// <param name="codeNumber">要生成二维码的字符串</param>       
        /// <param name="size">大小尺寸</param>  
        /// <returns>二维码图片</returns>  
        private Bitmap Create_ImgCode(string codeNumber, int size)
        {
            //创建二维码生成类  
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置编码模式  
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置编码测量度  
            qrCodeEncoder.QRCodeScale = size;
            //设置编码版本  
            qrCodeEncoder.QRCodeVersion = 0;
            //设置编码错误纠正  
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //生成二维码图片  
            System.Drawing.Bitmap image = qrCodeEncoder.Encode(codeNumber);
            return image;
        }
        #endregion





    }
}
