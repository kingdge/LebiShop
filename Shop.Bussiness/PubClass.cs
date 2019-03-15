using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
///PubClass 的摘要说明
/// </summary>
public class PubClass
{
	public PubClass()
	{
		
	}

    #region##对给定的一个图片（Image对象）生成一个指定大小的缩略图
    ///<summary>
    /// 对给定的一个图片（Image对象）生成一个指定大小的缩略图。
    ///</summary>
    ///<param name="originalImage">原始图片</param>
    ///<param name="thumMaxWidth">缩略图的宽度</param>
    ///<param name="thumMaxHeight">缩略图的高度</param>
    ///<returns>返回缩略图的Image对象</returns>
    public static System.Drawing.Image GetThumbNailImage(System.Drawing.Image originalImage, int thumMaxWidth, int thumMaxHeight)
    {
        System.Drawing.Size thumRealSize = System.Drawing.Size.Empty;
        System.Drawing.Image newImage = originalImage;
        System.Drawing.Graphics graphics = null;
        try
        {
            thumRealSize = GetNewSize(thumMaxWidth, thumMaxHeight, originalImage.Width, originalImage.Height);
            newImage = new System.Drawing.Bitmap(thumRealSize.Width, thumRealSize.Height);
            graphics = System.Drawing.Graphics.FromImage(newImage);
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            graphics.Clear(System.Drawing.Color.Transparent);
            graphics.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, thumRealSize.Width, thumRealSize.Height), new System.Drawing.Rectangle(0, 0, originalImage.Width, originalImage.Height), System.Drawing.GraphicsUnit.Pixel);
        }
        catch { }
        finally
        {
            if (graphics != null)
            {
                graphics.Dispose();
                graphics = null;
            }
        }
        return newImage;
    }
    #endregion

    #region##获取一个图片按等比例缩小后的大小
    ///<summary>
    /// 获取一个图片按等比例缩小后的大小。
    ///</summary>
    ///<param name="maxWidth">需要缩小到的宽度</param>
    ///<param name="maxHeight">需要缩小到的高度</param>
    ///<param name="imageOriginalWidth">图片的原始宽度</param>
    ///<param name="imageOriginalHeight">图片的原始高度</param>
    ///<returns>返回图片按等比例缩小后的实际大小</returns>
    public static System.Drawing.Size GetNewSize(int maxWidth, int maxHeight, int imageOriginalWidth, int imageOriginalHeight)
    {
        double w = 0.0;
        double h = 0.0;
        double sw = Convert.ToDouble(imageOriginalWidth);
        double sh = Convert.ToDouble(imageOriginalHeight);
        double mw = Convert.ToDouble(maxWidth);
        double mh = Convert.ToDouble(maxHeight);
        if (sw < mw && sh < mh)
        {
            w = sw;
            h = sh;
        }
        else if ((sw / sh) > (mw / mh))
        {
            w = maxWidth;
            h = (w * sh) / sw;
        }
        else
        {
            h = maxHeight;
            w = (h * sw) / sh;
        }
        return new System.Drawing.Size(Convert.ToInt32(w), Convert.ToInt32(h));
    }
    #endregion

    #region##删除文件
    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="path"></param>
    public static void FileDel(string path)
    {
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
    }
    #endregion
}
