using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
namespace UI.myadmin.check
{
    /// <summary>
    /// reg 的摘要说明
    /// </summary>
    public class reg : IHttpHandler
    {
        BLL.User bll = new BLL.User();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string msg = string.Empty;
            MODEL.User model = new MODEL.User();
            model.ULoginName =context.Request.Form["uLoginName"];
            model.ULoginPwd =Common.UIHelper.MD5(context.Request.Form["uLoginPwd"]).ToUpper();
            model.URegTime = DateTime.Now;
            model.URegIP = "127.0.0.1";
            model.UCurrentTime = DateTime.Now;
            model.UCurrentIP = "127.0.0.1";
            model.ULastTime = DateTime.Now;
            model.ULastIP = "127.0.0.1";
            model.UIsRoot = true;
            model.UIsNormal = true;
            model.ULevel = 0;
            model.ULoginCount = 0;
            model.UIsDel = false;
            model.UNick = context.Request.Form["uNick"];
            model.UPic = context.Request.Form["imgsrc"];
            model.URealName = context.Request.Form["uRealName"];
            model.USex = context.Request.Form["uSex"];
            model.UEmail = context.Request.Form["uEmail"];
            model.UPhone = context.Request.Form["uPhone"];
            model.UQQ = context.Request.Form["uQQ"];
            model.UAddress = context.Request.Form["uAddress"];
            
            MODEL.User model1 = bll.GetModelByName(model.ULoginName);
            int newId = 0;
     
            if (model1.ULoginName == null)
            {
                if (bll.Add(model, out newId))
                {
                    msg = "ok";
                }
                else 
                {
                    msg = "no";
                }
            }
            else
            {
          
                 msg = "usererr";
                     
                
            }
            context.Response.Write(msg);
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