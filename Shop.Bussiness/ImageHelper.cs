using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Configuration;
using System.Web;
using System.Text.RegularExpressions;
using System.Text;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Shop.Bussiness
{
    /// <summary>
    /// ImageHelper 的摘要说明
    /// </summary>
    public class ImageHelper
    {
        public static string rootpath(string path)
        {
            //return System.Web.HttpContext.Current.Server.MapPath("~/" + path.TrimStart('/'));
            return System.Web.HttpRuntime.AppDomainAppPath.TrimEnd('/') + "/" + path.TrimStart('/');
        }
        static string ServerPath = System.Web.HttpContext.Current.Server.MapPath("~/");
        /// <summary>
        /// 保存原始图片的方法
        /// 290 成功
        /// 291 文件已经存在，请重命名后上传
        /// 292 没有可上传的文件
        /// 293 格式不支持
        /// 294 修剪尺寸不能是0
        /// 295 文件不存在
        /// 296 异常
        /// 297 长度超限
        /// </summary>
        /// <param name="FileUpload"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static int UPLoad(HttpPostedFile FileUpload, string Path, string FileName)
        {
            Path = ServerPath + Path + "/";
            if (!File.Exists(Path))   //如果路径不存在，则创建
            {
                System.IO.Directory.CreateDirectory(Path);
            }
            if (FileUpload.ContentLength > 1)   //如果是true，则表示该控件有文件要上传
            {
                string fileContentType = FileUpload.ContentType;
                string name = FileUpload.FileName;                         //返回文件在客户端的完全路径（包括文件名全称）
                FileInfo file = new FileInfo(name);                        //FileInfo对象表示磁盘或网络位置上的文件。提供文件的路径，就可以创建一个FileInfo对象：                                       
                string webFilePath = Path + FileName;                      //完整的存储路径
                if (FileUpload.ContentLength > Convert.ToDecimal(ShopCache.GetBaseConfig().UpLoadLimit) * 1024 * 1024)
                {
                    return 297;
                }
                if (!File.Exists(webFilePath))
                {
                    try
                    {
                        FileUpload.SaveAs(webFilePath);
                        bool flag = CheckPictureSafe(webFilePath);
                        if (flag)
                            return 290;
                        else
                            return 296;
                    }
                    catch (Exception ex)
                    {
                        return 296;
                        //Msg = ex.Message;
                    }
                }
                else
                {
                    return 291;
                    //Msg = "文件已经存在，请重命名后上传!";
                }
            }
            else
            {
                return 292;
                //Msg = "没有可上传的文件";
            }


        }
        /// <summary>
        /// 保存图片
        /// 保存原始图片的方法
        /// 290 成功
        /// 291 文件已经存在，请重命名后上传
        /// 292 没有可上传的文件
        /// 293 格式不支持
        /// 294 修剪尺寸不能是0
        /// 295 文件不存在
        /// 296 异常
        /// </summary>
        /// <param name="file"></param>
        /// <param name="Path"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static int UPLoad(byte[] file, string Path, string FileName)
        {
            Path = ServerPath + Path + "/";
            if (!File.Exists(Path))   //如果路径不存在，则创建
            {
                System.IO.Directory.CreateDirectory(Path);
            }
            if (file.Length > 0)   //如果是true，则表示有文件要上传
            {
                string webFilePath = Path + FileName;                      //完整的存储路径
                if (!File.Exists(webFilePath))
                {
                    try
                    {
                        System.IO.FileStream fs = new System.IO.FileStream(webFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                        fs.Write(file, 0, file.Length);
                        fs.Flush();
                        fs.Close();
                        return 290;
                    }
                    catch (Exception ex)
                    {
                        return 296;
                        //Msg = ex.Message;
                    }
                }
                else
                {
                    return 291;
                }
            }
            else
            {
                return 292;
            }

        }
        /// <summary>
        /// 保存缩略图片的方法
        /// </summary>
        /// 290 成功
        /// 291 文件已经存在，请重命名后上传
        /// 292 没有可上传的文件
        /// 293 格式不支持
        /// 294 修剪尺寸不能是0
        /// 295 文件不存在
        /// 296 异常
        /// <param name="FileUpload"></param>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static int UPLoad(string FilePath, string Path, string FileName, int Width, int Height, string model = "NHW")
        {
            //参数说明：
            //FilePath = System.Web.HttpContext.Current.Server.MapPath("~/") + FilePath;
            //Path = ServerPath + Path + "/";
            //如果保存图片的目录不存在，则创建它

            //if (!File.Exists(rootpath(Path)))   //如果路径不存在，则创建
            //{
            //    System.IO.Directory.CreateDirectory(rootpath(Path));
            //}

            if (File.Exists(rootpath(FilePath)))
            {
                try
                {
                    //if (Width != 0 && Height != 0)
                    //{
                    MakeThumbnail(FilePath, Path, FileName, Width, Height, model);//全部采用不变形的修剪方式
                    return 290;
                    //}
                    //else
                    //{
                    //    return 294;
                    //    //Msg = "修剪尺寸不能是0";
                    //}
                }
                catch
                {
                    return 296;
                    //Msg = ex.Message;
                }
            }
            else
            {
                return 295;
                //Msg = "文件不存在";
            }
        }
        #region 裁剪图片
        /// 〈summary>
        /// 生成缩略图
        /// 〈/summary>
        /// 〈param name="originalImagePath">源图路径（物理路径）〈/param>
        /// 〈param name="thumbnailPath">缩略图路径（物理路径）〈/param>
        /// 〈param name="width">缩略图宽度〈/param>
        /// 〈param name="height">缩略图高度〈/param>
        /// 〈param name="mode">生成缩略图的方式〈/param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, string thumbnailname, int width, int height, string mode)
        {
            string FilePath = rootpath(thumbnailPath);
            if (!File.Exists(FilePath))   //如果路径不存在，则创建
            {
                System.IO.Directory.CreateDirectory(FilePath);
            }
            System.Drawing.Image bitmap = MakeThumbnail(originalImagePath, width, height, mode);
            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(rootpath(thumbnailPath + "/" + thumbnailname), System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                bitmap.Dispose();
            }
        }
        /// 〈summary>
        /// 生成缩略图
        /// 〈/summary>
        /// 〈param name="originalImagePath">源图路径（物理路径）〈/param>
        /// 〈param name="thumbnailPath">缩略图路径（物理路径）〈/param>
        /// 〈param name="width">缩略图宽度〈/param>
        /// 〈param name="height">缩略图高度〈/param>
        /// 〈param name="mode">生成缩略图的方式〈/param>    
        public static System.Drawing.Image MakeThumbnail(string originalImagePath, int width, int height, string mode)
        {

            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(rootpath(originalImagePath));
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            width = width == 0 ? originalImage.Width : width;
            height = height == 0 ? originalImage.Height : height;

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;


            switch (mode.ToLower())
            {
                case "hw"://指定高宽缩放（可能变形）                
                    break;
                case "w"://指定宽，高按比例   
                    if (ow < towidth)
                    {
                        towidth = ow;
                        toheight = oh;
                    }
                    else
                    {
                        toheight = originalImage.Height * width / originalImage.Width;
                    }
                    break;
                case "h"://指定高，宽按比例
                    if (oh < toheight)
                    {
                        towidth = ow;
                        toheight = oh;
                    }
                    else
                    {
                        towidth = originalImage.Width * height / originalImage.Height;
                    }
                    break;
                case "nhw"://生成不超过尺寸的不变型的缩略图
                    if (toheight - oh > towidth - ow)
                    {
                        //按照设定宽度压缩
                        toheight = originalImage.Height * width / originalImage.Width;
                    }
                    else
                    {
                        //按照设定高度压缩
                        towidth = originalImage.Width * height / originalImage.Height;
                    }
                    //if (oh < toheight)
                    //{
                    //    if (ow < towidth)
                    //    {
                    //        towidth = ow;
                    //        toheight = oh;
                    //    }
                    //    else
                    //    {
                    //        toheight = originalImage.Height * width / originalImage.Width;
                    //    }
                    //}
                    //else
                    //{
                    //    if (ow < towidth)
                    //    {
                    //        toheight = originalImage.Height * width / originalImage.Width;
                    //    }
                    //    else
                    //    {
                    //        if (originalImage.Height * width / originalImage.Width > toheight)
                    //        {
                    //            towidth = originalImage.Width * height / originalImage.Height;
                    //        }
                    //        else
                    //        {
                    //            toheight = originalImage.Height * width / originalImage.Width; ;
                    //        }
                    //    }

                    //}
                    break;
                case "cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        //扁图
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            ////设置高质量插值法
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            ////设置高质量,低速度呈现平滑程度
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            ////清空画布并以透明背景色填充
            //g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);
            originalImage.Dispose();
            g.Dispose();
            return bitmap;

        }

        #endregion
        #region 生成水印并保存
        public static string MakeWater(string Oldimage, string NewPath, string NewName, WaterConfig mx)
        {
            string Msg = "OK";
            if (mx.OnAndOff == "1")
            {
                if (mx.PicAndText.ToLower() == "logo")
                    Msg = MakeWaterPic(Oldimage, NewPath, NewName, mx);
                else
                    Msg = MakeWaterTxt(Oldimage, NewPath, NewName, mx);

            }
            return Msg;
        }
        /// 〈summary>
        /// 在图片上增加文字水印
        /// 〈/summary>
        /// 〈param name="oldimage">原图片位置〈/param>
        /// 〈param name="Path_sy">生成的带文字水印的图片路径〈/param>
        protected static string MakeWaterTxt(string Oldimage, string NewPath, string NewName, WaterConfig mx)
        {
            try
            {
                NewPath = ServerPath + NewPath + "/";
                if (!File.Exists(NewPath))   //如果路径不存在，则创建
                {
                    System.IO.Directory.CreateDirectory(NewPath);
                }
                System.Drawing.Image image = System.Drawing.Image.FromFile(Oldimage);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
                g.DrawImage(image, 0, 0, image.Width, image.Height);

                System.Drawing.FontStyle s = System.Drawing.FontStyle.Regular;
                switch (mx.WM_FontForm)
                {
                    case "Bold":
                        s = System.Drawing.FontStyle.Bold;
                        break;
                    case "Underline":
                        s = System.Drawing.FontStyle.Underline;
                        break;
                    case "Italic":
                        s = System.Drawing.FontStyle.Italic;
                        break;
                    case "Strikeout":
                        s = System.Drawing.FontStyle.Strikeout;
                        break;
                }
                System.Drawing.Font f = new System.Drawing.Font(mx.WM_FontForm, Convert.ToInt32(mx.WM_FontSize), s);    //字体
                System.Drawing.Color c = System.Drawing.Color.FromName(mx.WM_FontColor);
                System.Drawing.Brush b = new System.Drawing.SolidBrush(c);

                if (mx.WM_Height == "0" || mx.WM_Width == "0")
                {
                    mx.WM_Height = "100";
                    mx.WM_Width = "200";
                }
                Rectangle rf = new System.Drawing.Rectangle(image.Width - Convert.ToInt32(mx.WM_PlaceX), image.Height - Convert.ToInt32(mx.WM_PlaceY), Convert.ToInt32(mx.WM_Width), Convert.ToInt32(mx.WM_Height));
                switch (mx.WM_Location)
                {
                    case "LeftTop":
                        rf = new System.Drawing.Rectangle(Convert.ToInt32(mx.WM_PlaceX), Convert.ToInt32(mx.WM_PlaceY), Convert.ToInt32(mx.WM_Width), Convert.ToInt32(mx.WM_Height));
                        break;
                    case "LeftBottom":
                        rf = new System.Drawing.Rectangle(Convert.ToInt32(mx.WM_PlaceX), image.Height - Convert.ToInt32(mx.WM_PlaceY), Convert.ToInt32(mx.WM_Width), Convert.ToInt32(mx.WM_Height));
                        break;
                    case "RightTop":
                        rf = new System.Drawing.Rectangle(image.Width - Convert.ToInt32(mx.WM_PlaceX), Convert.ToInt32(mx.WM_PlaceY), Convert.ToInt32(mx.WM_Width), Convert.ToInt32(mx.WM_Height));
                        break;
                    //case "RightBottom":
                    //    rf = new System.Drawing.Rectangle(image.Width - Convert.ToInt32(mx.WM_PlaceX), image.Height - Convert.ToInt32(mx.WM_PlaceY), Convert.ToInt32(mx.WM_Width), Convert.ToInt32(mx.WM_Height));
                    //    break;
                }
                g.DrawString(mx.WM_Text, f, b, rf);    //字体位置20X20
                //g.DrawString(mx.WM_Text, f, b, Convert.ToInt32(mx.WM_PlaceX), Convert.ToInt32(mx.WM_PlaceY));    //字体位置20X20
                g.Dispose();
                if (File.Exists(NewPath + NewName))//检查同名文件，如存在，删除
                    File.Delete(NewPath + NewName);
                image.Save(NewPath + NewName);
                image.Dispose();
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /**/
        /// 〈summary>
        /// 在图片上生成图片水印
        /// 〈/summary>
        /// 〈param name="Path">原服务器图片路径〈/param>
        /// 〈param name="Path_syp">生成的带图片水印的图片路径〈/param>
        /// 〈param name="Path_sypf">水印图片路径〈/param>
        protected static string MakeWaterPic(string Oldimage, string NewPath, string NewName, WaterConfig mx)
        {

            try
            {
                NewPath = ServerPath + NewPath + "/";
                if (!File.Exists(NewPath))   //如果路径不存在，则创建
                {
                    System.IO.Directory.CreateDirectory(NewPath);
                }
                System.Drawing.Image image = System.Drawing.Image.FromFile(Oldimage);
                System.Drawing.Image copyImage = System.Drawing.Image.FromFile(ServerPath + mx.WM_PicPath);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
                if (mx.WM_Height == "0" || mx.WM_Width == "0")
                {
                    mx.WM_Height = copyImage.Height.ToString();
                    mx.WM_Width = copyImage.Width.ToString();
                }
                Rectangle rf = new System.Drawing.Rectangle(image.Width - Convert.ToInt32(mx.WM_PlaceX), image.Height - Convert.ToInt32(mx.WM_PlaceY), Convert.ToInt32(mx.WM_Width), Convert.ToInt32(mx.WM_Height));
                switch (mx.WM_Location)
                {
                    case "LeftTop":
                        rf = new System.Drawing.Rectangle(Convert.ToInt32(mx.WM_PlaceX), Convert.ToInt32(mx.WM_PlaceY), Convert.ToInt32(mx.WM_Width), Convert.ToInt32(mx.WM_Height));
                        break;
                    case "LeftBottom":
                        rf = new System.Drawing.Rectangle(Convert.ToInt32(mx.WM_PlaceX), image.Height - Convert.ToInt32(mx.WM_PlaceY), Convert.ToInt32(mx.WM_Width), Convert.ToInt32(mx.WM_Height));
                        break;
                    case "RightTop":
                        rf = new System.Drawing.Rectangle(image.Width - Convert.ToInt32(mx.WM_PlaceX), Convert.ToInt32(mx.WM_PlaceY), Convert.ToInt32(mx.WM_Width), Convert.ToInt32(mx.WM_Height));
                        break;
                    //case "RightBottom":
                    //    rf = new System.Drawing.Rectangle(image.Width - Convert.ToInt32(mx.WM_PlaceX), image.Height - Convert.ToInt32(mx.WM_PlaceY), Convert.ToInt32(mx.WM_Width), Convert.ToInt32(mx.WM_Height));
                    //    break;
                }
                g.DrawImage(copyImage, rf, 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
                g.Dispose();

                if (File.Exists(NewPath + NewName))//检查同名文件，如存在，删除
                    File.Delete(NewPath + NewName);
                image.Save(NewPath + NewName);
                image.Dispose();
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region 添加水印-返回图片
        public static Image MakeWater(Image Oldimage, WaterConfig mx)
        {
            //B_WaterConfig bc = new B_WaterConfig();
            //WaterConfig mx = bc.LoadConfig();
            Image res = null;
            if (mx.OnAndOff == "1")
            {
                if (mx.PicAndText == "Logo")
                    res = MakeWaterPic(Oldimage, mx);
                else
                    res = MakeWaterTxt(Oldimage, mx);

            }
            return res;
        }
        /// 〈summary>
        /// 在图片上增加文字水印
        /// 〈/summary>
        /// 〈param name="oldimage">原图片位置〈/param>
        /// 〈param name="Path_sy">生成的带文字水印的图片路径〈/param>
        protected static Image MakeWaterTxt(Image image, WaterConfig mx)
        {

            try
            {
                int Width = 0;
                int Height = 0;
                int PlaceX = 0;
                int PlaceY = 0;
                int.TryParse(mx.WM_Height, out Height);
                int.TryParse(mx.WM_Width, out Width);
                int.TryParse(mx.WM_PlaceX, out PlaceX);
                int.TryParse(mx.WM_PlaceY, out PlaceY);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(image, 0, 0, image.Width, image.Height);

                System.Drawing.FontStyle s = System.Drawing.FontStyle.Regular;
                switch (mx.WM_FontForm)
                {
                    case "Bold":
                        s = System.Drawing.FontStyle.Bold;
                        break;
                    case "Underline":
                        s = System.Drawing.FontStyle.Underline;
                        break;
                    case "Italic":
                        s = System.Drawing.FontStyle.Italic;
                        break;
                    case "Strikeout":
                        s = System.Drawing.FontStyle.Strikeout;
                        break;
                }
                System.Drawing.Font f = new System.Drawing.Font(mx.WM_Font, Convert.ToInt32(mx.WM_FontSize), s);    //字体
                System.Drawing.Color c = System.Drawing.Color.FromName(mx.WM_FontColor);
                System.Drawing.Brush b = new System.Drawing.SolidBrush(c);

                //if (mx.WM_Height == "0" || mx.WM_Width == "0")
                //{
                //    mx.WM_Height = "100";
                //    mx.WM_Width = "200";
                //}
                Rectangle rf = new System.Drawing.Rectangle(image.Width - PlaceX, image.Height - PlaceY, Width, Height);
                switch (mx.WM_Location)
                {
                    case "LeftTop":
                        rf = new System.Drawing.Rectangle(PlaceX, PlaceY, Width, Height);
                        break;
                    case "LeftBottom":
                        rf = new System.Drawing.Rectangle(PlaceX, image.Height - PlaceY, Width, Height);
                        break;
                    case "RightTop":
                        rf = new System.Drawing.Rectangle(image.Width - PlaceX, PlaceY, Width, Height);
                        break;
                    case "RightBottom":
                        rf = new System.Drawing.Rectangle(image.Width - PlaceX, image.Height - PlaceY, Width, Height);
                        break;
                }
                g.DrawString(mx.WM_Text, f, b, rf);    //字体位置20X20
                //g.DrawString(mx.WM_Text, f, b, Convert.ToInt32(mx.WM_PlaceX), Convert.ToInt32(mx.WM_PlaceY));    //字体位置20X20
                g.Dispose();
                return image;
            }
            catch(Exception ex)
            {
                SystemLog.Add(ex.ToString());
                return image;
            }
        }
        /// 〈summary>
        /// 在图片上生成图片水印
        /// 〈/summary>
        /// 〈param name="Path">原服务器图片路径〈/param>
        /// 〈param name="Path_syp">生成的带图片水印的图片路径〈/param>
        /// 〈param name="Path_sypf">水印图片路径〈/param>
        protected static Image MakeWaterPic(Image image, WaterConfig mx)
        {
            try
            {

                System.Drawing.Image copyImage = System.Drawing.Image.FromFile(rootpath(mx.WM_PicPath));
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                int Width = 0;
                int Height = 0;
                int PlaceX = 0;
                int PlaceY = 0;
                int.TryParse(mx.WM_Height, out Height);
                int.TryParse(mx.WM_Width, out Width);
                int.TryParse(mx.WM_PlaceX, out PlaceX);
                int.TryParse(mx.WM_PlaceY, out PlaceY);

                if (Height == 0 || Width == 0)
                {
                    Height = copyImage.Height;
                    Width = copyImage.Width;
                }
                if (mx.WM_Location == "Tile")
                {
                    //平铺水印
                    int x = image.Width / Width + 1;
                    int y = image.Height / Height + 1;
                    for (int i = 0; i < y; i++)
                    {
                        for (int j = 0; j < x; j++)
                        {
                            Rectangle rf = new System.Drawing.Rectangle(j * Width, i * Height, Width, Height);
                            g.DrawImage(copyImage, rf, 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);

                        }
                    }
                }
                else
                {
                    Rectangle rf = new System.Drawing.Rectangle(image.Width - PlaceX, image.Height - PlaceY, Width, Height);
                    switch (mx.WM_Location)
                    {
                        case "LeftTop":
                            rf = new System.Drawing.Rectangle(PlaceX, PlaceY, Width, Height);
                            break;
                        case "LeftBottom":
                            rf = new System.Drawing.Rectangle(PlaceX, image.Height - PlaceY, Width, Height);
                            break;
                        case "RightTop":
                            rf = new System.Drawing.Rectangle(image.Width - PlaceX, PlaceY, Width, Height);
                            break;
                        //case "RightBottom":
                        //    rf = new System.Drawing.Rectangle(image.Width - Convert.ToInt32(mx.WM_PlaceX), image.Height - Convert.ToInt32(mx.WM_PlaceY), Convert.ToInt32(mx.WM_Width), Convert.ToInt32(mx.WM_Height));
                        //    break;
                    }
                    g.DrawImage(copyImage, rf, 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
                }
                g.Dispose();
                //image.Dispose();
                return image;
            }
            catch
            {
                return null;
            }
        }
        #endregion
        #region 删除
        public static void DeleteImage(string imgUrl)
        {
            try
            {
                Regex reg = new Regex("(http://([^/]*))", RegexOptions.IgnoreCase);
                string url = reg.Replace(imgUrl, "");
                string fileUrl = rootpath(url);
                if (File.Exists(fileUrl))
                {
                    File.Delete(fileUrl);
                }
            }
            catch
            {

            }
        }
        public static bool IsExists(string imgUrl)
        {
            string fileUrl = ServerPath + imgUrl;
            if (File.Exists(fileUrl))
                return true;
            return false;
        }

        #endregion


        #region 保存一张图片
        /// <summary>
        /// 保存一张图片
        /// 保存原始图片的方法
        /// 290 成功
        /// 291 文件已经存在，请重命名后上传
        /// 292 没有可上传的文件
        /// 293 格式不支持
        /// 294 修剪尺寸不能是0
        /// 295 文件不存在
        /// 296 异常

        /// 
        /// </summary>
        /// <param name="postedFile"></param>
        /// <param name="savepath"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int SaveImage(HttpPostedFile postedFile, string savepath, string name)
        {
            string UpFileType = postedFile.ContentType;
            string fileName = System.IO.Path.GetFileName(postedFile.FileName);
            string ext = System.IO.Path.GetExtension(postedFile.FileName).ToLower();
            bool isUpload = false;
            /*
            switch (postedFile.ContentType.ToLower())
            {
                case "image/pjpeg":
                    UpFileType = ".jpg"; isUpload = true; break;
                case "image/jpeg":
                    UpFileType = ".jpg"; isUpload = true; break;
                case "image/gif":
                    UpFileType = ".gif"; isUpload = true; break;
                case "image/bmp":
                    UpFileType = ".bmp"; isUpload = true; break;
                case "image/png":
                    UpFileType = ".png"; isUpload = true; break;
                case "image/tiff":
                    UpFileType = ".tif"; isUpload = true; break;
                case "image/x-png":
                    UpFileType = ".png"; isUpload = true; break;
                default:
                    break;
            }
            */
            switch (ext)
            {
                case ".jpg":
                    UpFileType = ".jpg"; isUpload = true; break;
                case ".jpeg":
                    UpFileType = ".jpeg"; isUpload = true; break;
                case ".gif":
                    UpFileType = ".gif"; isUpload = true; break;
                case ".bmp":
                    UpFileType = ".bmp"; isUpload = true; break;
                case ".png":
                    UpFileType = ".png"; isUpload = true; break;
                case ".tif":
                    UpFileType = ".tif"; isUpload = true; break;
                default:
                    break;
            }
            if (isUpload == false)
            {
                //msg = Language.Tag("格式不支持", "") + UpFileType;
                return 293;
            }


            if (fileName != "")
            {
                return UPLoad(postedFile, savepath, name);
            }
            return 292;
        }

        #endregion

        /// <summary>
        /// 判断文件格式
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsAllowedExtension(string filePath)
        {
 
            FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);
            string fileclass = "";
           // byte buffer;
            try
            {
                
                //buffer = reader.ReadByte();
                //fileclass = buffer.ToString();
                //buffer = reader.ReadByte();
                //fileclass += buffer.ToString();
 
                for (int i = 0; i < 2; i++)
                {
                    fileclass += reader.ReadByte().ToString();
                }
 
            }
            catch (Exception)
            {
 
                throw;
            }
            if (fileclass == "7173" || fileclass == "255216" || fileclass == "13780" || fileclass == "6677")
            {
                return true;
            }
            else
            {
                //Shop.Bussiness.SystemLog.Add("无效上传文件格式："+ filePath +"|"+ fileclass);
                return false;
            }
            /*文件扩展名说明
             * 255216 jpg
             * 208207 doc xls ppt wps
             * 8075 docx pptx xlsx zip
             * 5150 txt
             * 8297 rar
             * 7790 exe
             * 3780 pdf      
             * 
             * 4946/104116 txt
             * 7173        gif 
             * 255216      jpg
             * 13780       png
             * 6677        bmp
             * 239187      txt,aspx,asp,sql
             * 208207      xls.doc.ppt
             * 6063        xml
             * 6033        htm,html
             * 4742        js
             * 8075        xlsx,zip,pptx,mmap,zip
             * 8297        rar   
             * 01          accdb,mdb
             * 7790        exe,dll
             * 5666        psd 
             * 255254      rdp 
             * 10056       bt种子 
             * 64101       bat 
             * 4059        sgf    
             */
        }
        /// <summary>
        /// C#检测上传图片是否安全函数
        /// </summary>
        /// <param name="strPictureFilePath"></param>
        public static bool CheckPictureSafe(string strPictureFilePath)
        {
            bool strReturn = true;
            if (File.Exists(strPictureFilePath))
            {
                if (!IsAllowedExtension(strPictureFilePath))
                {
                    File.Delete(strPictureFilePath);
                    return false;
                }
                StringBuilder str_Temp = new StringBuilder();
                try
                {
                    using (StreamReader sr = new StreamReader(strPictureFilePath))    //按文本文件方式读取图片内容
                    {
                        String line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            str_Temp.Append(line);
                        }
                        //检测是否包含危险字符串
                        if (str_Temp == null)
                        {
                            strReturn = false;
                        }
                        else
                        {
                            string DangerString = "<script|iframe|.getfolder|.createfolder|.deletefolder|.createdirectory|.deletedirectory|.saveas|wscript.shell|script.encode|server.|.createobject|execute|activexobject|language=|include|filesystemobject|shell.application";
                            strReturn = RegexTool.Check(str_Temp.ToString().ToLower(), DangerString);
                            if (strReturn)
                            {
                                Shop.Bussiness.SystemLog.Add(str_Temp.ToString().ToLower() +"|"+ DangerString);
                            }

                        }
                        sr.Close();
                    }
                    if (strReturn)
                    {
                        File.Delete(strPictureFilePath);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }
            }
            return true;
        }

        /// <summary>
        /// 将传入图片与数据绑定
        /// </summary>
        public static void LebiImagesUsed(string images, string tablename, int keyid)
        {
            string where = "";
            string sql = "";
            if (tablename != "config")
            {
                sql = "update [Lebi_Image] set Keyid=0 where TableName='" + tablename + "' and Keyid=" + keyid;
                Common.ExecuteSql(sql);
            }
            if (images.Contains("[{"))//多语言的JSON格式
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();
                List<LanguageContent> langs = jss.Deserialize<List<LanguageContent>>(images);
                if (langs == null)
                    return;
                foreach (LanguageContent m in langs)
                {
                    if (m.C == "")
                        continue;
                    if (where == "")
                        where = "Image='" + m.C + "'";
                    else
                        where += " or Image='" + m.C + "'";
                }

            }
            else
            {
                string[] arr = images.Split('@');

                foreach (string image in arr)
                {
                    if (image == "")
                        continue;
                    if (where == "")
                        where = "Image='" + image + "'";
                    else
                        where += " or Image='" + image + "'";
                }
            }
            if (where != "")
            {
                where = "(" + where + ") and Keyid=0";
                sql = "update [Lebi_Image] set TableName='" + tablename + "',Keyid=" + keyid + " where " + where;
                Common.ExecuteSql(sql);
            }
        }
        /// <summary>
        /// 删除一个数据的图片
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="keyid"></param>
        public static void LebiImagesDelete(string tablename, int keyid)
        {
            string where = "TableName='" + tablename + "' and Keyid=" + keyid + "";
            List<Lebi_Image> images = B_Lebi_Image.GetList(where, "");
            foreach (Lebi_Image image in images)
            {
                if (image.Size != "")
                {
                    string[] sizes = image.Size.Split(',');
                    foreach (string size in sizes)
                    {
                        string simage = image.Image.Replace("w$h", size);
                        DeleteImage(simage);
                    }
                    DeleteImage(image.Image);
                }
            }
            B_Lebi_Image.Delete(where);
        }
        public static void LebiImagesDelete(string tablename, string keyids)
        {
            if (keyids == null)
                return;
            string where = "TableName='" + tablename + "' and Keyid in (" + keyids + ")";
            List<Lebi_Image> images = B_Lebi_Image.GetList(where, "");
            foreach (Lebi_Image image in images)
            {
                if (image.Size != "")
                {
                    string[] sizes = image.Size.Split(',');
                    foreach (string size in sizes)
                    {
                        string simage = image.Image.Replace("w$h", size);
                        DeleteImage(simage);
                    }
                    DeleteImage(image.Image);
                }
            }
            B_Lebi_Image.Delete(where);
        }
        /// <summary>
        /// 下载一张远程图片并生成系统尺寸
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static LBimage DownLoadImage(string url, Lebi_Product pro)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            System.Net.WebClient myWebClient = new System.Net.WebClient();
            string ServerPath = System.Web.HttpContext.Current.Server.MapPath("~/");
            string savepath = ShopCache.GetBaseConfig().UpLoadPath + "/Product/" + DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MMdd") + "/";
            savepath = ThemeUrl.CheckURL(savepath);
            if (!Directory.Exists(ServerPath + savepath))   //如果路径不存在，则创建
            {
                Directory.CreateDirectory(ServerPath + savepath);
            }
            string[] exnamearr = url.Split('.');
            string exname = exnamearr[exnamearr.Length - 1];
            string name = DateTime.Now.ToString("yyMMddssfff") + "_w$h_." + exname;
            string OldImage = savepath + name;
            if (File.Exists(OldImage))
            {
                File.Delete(OldImage);
            }
            myWebClient.DownloadFile(url, ServerPath + OldImage);


            //写入数据库
            Lebi_Image model = new Lebi_Image();
            model.Image = OldImage;
            model.Keyid = pro.id;
            model.Size = "";
            model.TableName = "Product";
            B_Lebi_Image.Add(model);

            LBimage img = new LBimage();
            img.original = OldImage;
            img.big = OldImage;
            img.medium = OldImage;
            img.small = OldImage;
            return img;
        }
        /// <summary>
        /// 生成淘宝图片
        /// </summary>
        /// <param name="url">本地图片路径</param>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static LBimage CreateTaobaoImage(string url, Lebi_Product pro)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            string ServerPath = System.Web.HttpContext.Current.Server.MapPath("~/");
            string savepath = ShopCache.GetBaseConfig().UpLoadPath + "/Product/" + DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MMdd") + "/";
            savepath = ThemeUrl.CheckURL(savepath);
            if (!Directory.Exists(ServerPath + savepath))   //如果路径不存在，则创建
            {
                Directory.CreateDirectory(ServerPath + savepath);
            }
            string exname = "jpg";
            string name = DateTime.Now.ToString("yyMMddssfff") + "_w$h_." + exname;
            string OldImage = savepath + name;
            //if (File.Exists(OldImage))
            //{
            //    File.Delete(OldImage);
            //}
            FileTool.CopyFile(url, ServerPath + OldImage, true);

            //写入数据库
            Lebi_Image model = new Lebi_Image();
            model.Image = OldImage;
            model.Keyid = pro.id;
            model.Size = "";
            model.TableName = "Product";
            B_Lebi_Image.Add(model);

            LBimage img = new LBimage();
            img.original = OldImage;
            img.big = OldImage;
            img.medium = OldImage;
            img.small = OldImage;
            return img;
        }

        /// <summary>
        /// 从远程图片服务器上传结果中提取图片地址
        /// </summary>
        /// <returns></returns>
        public static string GetImageByServerResult(string str)
        {
            string rel = RegexTool.GetRegValue(str, "img:\"(.*?)\"");
            return rel;
        }
    }

}
