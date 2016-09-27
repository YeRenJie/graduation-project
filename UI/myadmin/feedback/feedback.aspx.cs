using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.myadmin.feedback
{
    public partial class feedback : System.Web.UI.Page
    {
        BLL.Feedback bll = new BLL.Feedback();
        BLL.User bll2 = new BLL.User();
        string msg = string.Empty;
        protected int pageCount = 1;  //总页数
        protected string nextHtml = string.Empty;  //下一个
        protected string prevHtml = string.Empty; //上一个
        protected string totalSize = string.Empty;
        protected string cp = string.Empty;
        protected string eMail = string.Empty;

        protected string pageHtml = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                MODEL.Feedback model = new MODEL.Feedback();
                model.FThemb = Request.Form["txtThemb"];
                model.FEmail = Request.Form["txtEmail"];
                model.FMsg = Request.Form["txtMsg"];
                model.FIP = Common.UIHelper.GetIp();
                model.FTime = DateTime.Now;
                model.FIsDel = false;
                model.FUID = int.Parse(Session["uID"].ToString());
                int newID;
                if (bll.Add(model, out newID))
                {
                    msg = "ok";
                }
                else
                {
                    msg = "500";
                }
                Response.Write(msg);
                Response.End();
            }
            else
            {
                cp = Request.QueryString["page"];
                pageCount = bll.GetPageCount(8);  //总页数
                totalSize = bll.GetCount().ToString();

                if (Session["uID"] != null)
                {
                    eMail = bll2.GetModelByID(Session["uID"].ToString()).UEmail;
                }

                if (string.IsNullOrEmpty(cp))
                {
                    cp = "1";
                    prevHtml = "javascript:void(0)";
                    if (int.Parse(cp) < pageCount)
                    {
                        nextHtml = "?page=" + (int.Parse(cp) + 1);
                    }
                    else
                    {
                        nextHtml = "javascript:void(0)";
                    }
                }
                else
                {
                    if (int.Parse(cp) + 1 <= pageCount)
                    {
                        nextHtml = "?page=" + (int.Parse(cp) + 1);
                    }
                    else
                    {
                        nextHtml = "javascript:void(0)";
                    }
                    if (int.Parse(cp) - 1 > 0)
                    {
                        prevHtml = "?page=" + (int.Parse(cp) - 1);
                    }
                    else
                    {
                        prevHtml = "javascript:void(0)";
                    }
                }
                List<MODEL.Feedback> list = bll.GetSimplePageList("8", cp);
                rp.DataSource = list;
                rp.DataBind();
            }
        }

        /// <summary>
        /// 根据ID返回实体
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        protected MODEL.User GetUser(string uid)
        {
            return bll2.GetModelByID(uid);
        }

        /// <summary>
        /// 获取楼数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected int GetFloor(string id)
        {
            return bll.GetCountForWhere(id) + 1;
        }
    }
}