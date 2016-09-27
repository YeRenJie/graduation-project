using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.myadmin.notes
{
    public partial class note : System.Web.UI.Page
    {
        BLL.Good bll = new BLL.Good();
        BLL.User bll2 = new BLL.User();
        protected decimal countPay = 0;
        string msg = string.Empty;
        protected int pageCount = 1;  //总页数
        protected string nextHtml = string.Empty;  //下一个
        protected string prevHtml = string.Empty; //上一个
        protected string totalSize = string.Empty;
        protected string cp = string.Empty;
        protected string pageHtml = string.Empty;
        protected string usname = string.Empty;
        string keyword = string.Empty;
        protected DateTime firstDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        protected string[] arr;
        protected string us = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Request.QueryString["type"] == "新增")
                {
                    MODEL.Good model = new MODEL.Good();
                    model.GBuyTime = DateTime.Parse(Request.Form["gBuyTime"]);
                    model.GName = Request.Form["gName"];
                    model.GPayWay = Request.Form["gBuyWay"];
                    model.GPrice = decimal.Parse(Request.Form["gPrice"]);
                    model.GTime = DateTime.Now;
                    model.GCount = int.Parse(Request.Form["gCount"]);
                    model.GIsDel = false;
                    model.GDes = Request.Form["gDes"];
                    model.GBuyPlace = Request.Form["gPlace"];
                    model.GBuyUser = int.Parse(Session["uID"].ToString());

                    int newID;
                    if (bll.Add(model, out newID))
                    {
                        Response.Write("ok");
                    }
                    else
                    {
                        Response.Write("busy");
                    }
                    Response.End();
                }
                else if (Request.QueryString["type"] == "修改")
                {
                    MODEL.Good mosd = bll.GetModelByID(Request.Form["uisd"]);
                    mosd.GBuyTime = DateTime.Parse(Request.Form["gBuyTime"]);
                    mosd.GName = Request.Form["gName"];
                    mosd.GPayWay = Request.Form["gBuyWay"];
                    mosd.GPrice = decimal.Parse(Request.Form["gPrice"]);
                    mosd.GCount = int.Parse(Request.Form["gCount"]);
                    mosd.GDes = Request.Form["gDes"];
                    mosd.GBuyPlace = Request.Form["gPlace"];
                    mosd.GBuyUser = int.Parse(Session["uID"].ToString());
                    if (bll.UpdateWithModify(mosd))
                    {
                        Response.Write("ok");
                    }
                    else
                    {
                        Response.Write("busy");
                    }
                    Response.End();
                }
                else
                {
                }
            }
            else
            {
                if (Session["uID"] != null)
                {
                    usname = bll2.GetModelByID(Session["uID"].ToString()).UNick;
                }
                if (!string.IsNullOrEmpty(Request.QueryString["kw"]))
                {
                    keyword = " WHERE gName LIKE '%" + Request.QueryString["kw"] + "%' or gBuyPlace LIKE '%" + Request.QueryString["kw"] + "%'  or gPayWay LIKE '%" + Request.QueryString["kw"] + "%' ";
                    countPay = bll.GetCountForWhere(keyword);
                }
                else
                {
                    countPay = bll.GetCountForWhere();
                }
                if (!string.IsNullOrEmpty(Request.QueryString["ts"]))
                {
                    arr = Request.QueryString["ts"].Split('|');

                    keyword = " WHERE gBuyTime BETWEEN '" + arr[0] + "' AND '" + arr[1] + "' ";
                    countPay = bll.GetCountForPay(DateTime.Parse(arr[0]), DateTime.Parse(arr[1]));
                }
                cp = Request.QueryString["page"];
                pageCount = bll.GetPageCount(20, keyword);  //总页数
                totalSize = bll.GetCount(keyword).ToString();


                string currentU = Request.RawUrl;  //当前网址
                if (currentU.IndexOf("ts") > -1)
                {
                    us = "&ts=" + Request.QueryString["ts"];
                }
                if (currentU.IndexOf("kw") > -1)
                {
                    us += "&kw=" + Request.QueryString["kw"];
                }
                //Response.Write(currentU);
                if (string.IsNullOrEmpty(cp))
                {
                    cp = "1";
                    prevHtml = "javascript:void(0)";
                    if (int.Parse(cp) < pageCount)
                    {
                        nextHtml = "?page=" + (int.Parse(cp) + 1) + us;
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
                        nextHtml = "?page=" + (int.Parse(cp) + 1) + us;
                    }
                    else
                    {
                        nextHtml = "javascript:void(0)";
                    }
                    if (int.Parse(cp) - 1 > 0)
                    {
                        prevHtml = "?page=" + (int.Parse(cp) - 1) + us;
                    }
                    else
                    {
                        prevHtml = "javascript:void(0)";
                    }
                }
                List<MODEL.Good> list = bll.GetSimplePageList("20", cp, keyword);
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
        /// 总计
        /// </summary>
        /// <param name="count"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        protected decimal Total(int count, decimal price)
        {
            return count * price;
        }
    }
}