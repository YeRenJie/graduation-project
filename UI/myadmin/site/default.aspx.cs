using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.myadmin.site
{
    public partial class _default : System.Web.UI.Page
    {
        protected MODEL.User model;
        BLL.User bll = new BLL.User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uID"] != null)
            {
                model = bll.GetModelByID(Session["uID"].ToString());
            }
        }
    }
}