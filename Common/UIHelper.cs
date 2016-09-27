using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Data;
using System.Data.SqlClient;


namespace Common
{
    /// <summary>
    /// 帮助类
    /// </summary>
    public class UIHelper
    {

        #region 构造方法 +  public Common()
        /// <summary>
        /// 构造方法
        /// </summary>
        public UIHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #endregion

        #region 获取当前的IP + public static string GetIp()
        /// <summary>
        /// 获取当前的IP
        /// </summary>
        /// <returns></returns>
        public static string GetIp()
        {
            string user_IP = null;
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                user_IP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                user_IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            if (user_IP == "::1")
            {
                user_IP = "127.0.0.1";
            }
            return user_IP;
        }
        #endregion

        #region 生成缩略图 +  public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式,取值：HW（指定宽高，可能变形），W（指定宽），H（指定高），Cut（指定宽高，不变形）</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
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
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        #endregion

        //#region 实现分层下拉 + public static void DDList(System.Web.UI.WebControls.DropDownList ddl)
        ///// <summary>
        ///// 实现分层下拉
        ///// </summary>
        ///// <param name="bigID">顶级类别</param>
        ///// <param name="ddl">下拉对象</param>
        //public static void DDList(string bigID, System.Web.UI.WebControls.DropDownList ddl)
        //{
        //    DataTable dt = new BLL.Channel().GetDataTableForChannel();
        //    DropDownListHelp ddlHelper = new DropDownListHelp();
        //    ddlHelper.createDropDownTree(dt, "cParentID", bigID, "cID", "cName", "cScore asc", ddl);
        //}
        //#endregion

        #region MD5加密 +  public static string MD5(string str)
        /// <summary>
        /// MD5加密 
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
        }
        #endregion

        #region 删除图片 + public static void DelFile(string imgSrc)
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="imgSrc">原图片路径</param>
        public static void DelFile(string imgSrc)
        {
            FileInfo file = new FileInfo(imgSrc);
            if (file.Exists)
            {
                file.Delete();
            }
        }
        #endregion

        #region 判断某个日期是否在某段日期范围内，返回布尔值 +   public static bool IsInDate(DateTime dt, DateTime dt1, DateTime dt2)
        /// <summary>
        /// 判断某个日期是否在某段日期范围内，返回布尔值
        /// </summary>
        /// <param name="dt">要判断的日期</param>
        /// <param name="dt1">开始日期</param>
        /// <param name="dt2">结束日期</param>
        /// <returns></returns>
        public static bool IsInDate(DateTime dt, DateTime dt1, DateTime dt2)
        {
            return dt.CompareTo(dt1) >= 0 && dt.CompareTo(dt2) <= 0;
        }
        #endregion
    }
}