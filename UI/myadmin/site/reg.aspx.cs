using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.myadmin.site
{
    public partial class reg : System.Web.UI.Page
    {
        BLL.User bll = new BLL.User();
        protected MODEL.User model = new MODEL.User();
        protected string select = "<option value=\"男\" selected=\"selected\">男</option><option value=\"女\">女</option><option value=\"保密\">保密</option>";
       
        protected void Page_Load(object sender, EventArgs e)
        {

            model.UPic = "up.jpg";
            
        
                
           
            
        }
    }
}