using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace UI.myadmin.notes
{
    /// <summary>
    /// cModify 的摘要说明
    /// </summary>
    public class cModify : IHttpHandler, IRequiresSessionState
    {
        BLL.Cost bll = new BLL.Cost();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string cID = context.Request.Form["cID"];
            string cMoney = context.Request.Form["cIncome"];

            if (bll.UpdateForField(cID, "cMoney", cMoney))
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