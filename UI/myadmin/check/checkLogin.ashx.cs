using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace UI.myadmin.check
{
    /// <summary>
    /// checkLogin 的摘要说明
    /// </summary>
    public class checkLogin : IHttpHandler, IRequiresSessionState
    {
        BLL.User bll = new BLL.User();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string msg = string.Empty;
            string txtName = context.Request.Form["txtName"];
            string txtPwd = Common.UIHelper.MD5(context.Request.Form["txtPwd"]).ToUpper();
            string isAutoLogin = context.Request.Form["isAutoLogin"];

            //string vCode = context.Request.Params["txtCode"];

            MODEL.User model = bll.GetModelByName(txtName);
            if (model.ULoginName == null)
            {
                msg = "usererr";
            }
            else
            {
                if (model.UIsNormal == false) { msg = "down"; return; }
                else
                {
                    if (txtPwd != model.ULoginPwd.ToUpper()) { msg = "pwderr"; }
                    else
                    {
                        msg = "ok";
                        context.Session["uID"] = model.UID;  //存入session
                        if (!string.IsNullOrEmpty(isAutoLogin))  //写入cookies
                        {
                            HttpCookie cookie = new HttpCookie("loginInfo");
                            cookie.Value = model.UID.ToString();
                            cookie.Expires = DateTime.Now.AddDays(3.0);
                            cookie.Path = context.Server.MapPath("/myadmin/");
                            context.Response.Cookies.Add(cookie);
                        }
                        //更新数据库
                        model.ULoginCount += 1;
                        model.ULastIP = model.UCurrentIP;
                        model.ULastTime = model.UCurrentTime;
                        model.UCurrentIP = Common.UIHelper.GetIp();
                        model.UCurrentTime = DateTime.Now;
                        bll.UpdateWithModify(model);
                    }
                }
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