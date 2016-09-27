using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;

namespace UI.myadmin.check
{
    /// <summary>
    /// upload 的摘要说明
    /// </summary>
    public class upload : IHttpHandler, IRequiresSessionState
    {
        ImgJson ij = new ImgJson();
        JavaScriptSerializer json = new JavaScriptSerializer();
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                HttpPostedFile upImg = context.Request.Files["upImg"];
                string ext = System.IO.Path.GetExtension(upImg.FileName);
                if (upImg.ContentLength > 2097152)
                {
                    ij.ResultMsg = "big";
                    context.Response.Write(json.Serialize(ij));
                    return;
                }
                List<string> filetypes = new List<string>();
                filetypes.Add(".png");
                filetypes.Add(".jpg");
                filetypes.Add(".gif");
                filetypes.Add(".jpeg");

                if (!filetypes.Contains(ext))
                {
                    ij.ResultMsg = "nosupport";
                    context.Response.Write(json.Serialize(ij));
                    return;
                }

                string files = Guid.NewGuid() + ext;
                string sPath = context.Server.MapPath("/upload/user/"); //存储路径
                string savePath = sPath + files;
                if (!System.IO.Directory.Exists(sPath))
                {
                    System.IO.Directory.CreateDirectory(sPath);
                }
                if (!System.IO.Directory.Exists(sPath + "thumb/"))
                {
                    System.IO.Directory.CreateDirectory(sPath + "thumb/");
                }

                upImg.SaveAs(savePath);
                Common.UIHelper.MakeThumbnail(savePath, sPath + "thumb/thumb_" + files, 100, 100, "W");
                ij.ImgSrc = files;
                ij.ResultMsg = "ok";
                context.Response.Write(json.Serialize(ij));
            }
            catch (Exception)
            {
                ij.ResultMsg = "err";
                context.Response.Write(json.Serialize(ij));
            }
            finally
            {
                context.Response.End();
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
    public class ImgJson
    {
        public string ImgSrc { get; set; }
        public string ResultMsg { get; set; }
    }

}