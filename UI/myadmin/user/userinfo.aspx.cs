using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.myadmin.site
{
    public partial class userinfo : System.Web.UI.Page
    {
        BLL.User bll = new BLL.User();
        protected MODEL.User model;
        protected string select = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["uID"] != null)
            {
                string uID = Session["uID"].ToString();
                model = bll.GetModelByID(uID);
                if (model.USex == "男")
                {
                    select = "<option value=\"男\" selected=\"selected\">男</option><option value=\"女\">女</option><option value=\"保密\">保密</option>";
                }
                else if (model.USex == "女")
                {
                    select = "<option value=\"男\" >男</option><option value=\"女\" selected=\"selected\">女</option><option value=\"保密\">保密</option>";
                }
                else
                {
                    select = "<option value=\"男\" >男</option><option value=\"女\" >女</option><option value=\"保密\" selected=\"selected\">保密</option>";
                }
            }
            if (IsPostBack)
            {
                model.UNick = Request.Form["uNick"];
                model.UPic = Request.Form["imgsrc"];
                model.ULoginPwd = Common.UIHelper.MD5(Request.Form["uLoginPwd"]).ToUpper();
                model.URealName = Request.Form["uRealName"];
                model.USex = Request.Form["uSex"];
                model.UEmail = Request.Form["uEmail"];
                model.UPhone = Request.Form["uPhone"];
                model.UQQ = Request.Form["uQQ"];
                model.UAddress = Request.Form["uAddress"];

                if (bll.UpdateWithModify(model))
                {
                    Response.Write("ok");
                }
                else
                {
                    Response.Write("busy");
                }
                Response.End();
            }
        }
    }
}