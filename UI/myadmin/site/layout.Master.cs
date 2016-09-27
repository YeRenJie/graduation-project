using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.myadmin.site
{
    public partial class layout : System.Web.UI.MasterPage
    {
        BLL.User bll = new BLL.User();
        protected MODEL.User model;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.Cookies["loginInfo"] != null)
            //{
            //    HttpCookie aCookie = Request.Cookies["loginInfo"];
            //    string uID = aCookie.Value;
            //    Response.Write(uID);
            //}
            //else
            //{

            //}
            if (Session["uID"] == null)
            {
                Response.Redirect("/myadmin/site/login.aspx");
            }
            else
            {
                model = bll.GetModelByID(Session["uID"].ToString());
            }
        }
    }
}