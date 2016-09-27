using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace UI.myadmin.notes
{
    /// <summary>
    /// del 的摘要说明
    /// </summary>
    public class del : IHttpHandler, IRequiresSessionState
    {
        BLL.Good bll = new BLL.Good();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string gid = context.Request.Form["usid"];
            if (bll.Del(gid))
            {
                context.Response.Write("ok");
            }
            else
            {
                context.Response.Write("busy");
            }
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}