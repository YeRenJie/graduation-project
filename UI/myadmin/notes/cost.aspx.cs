using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UI.myadmin.notes
{
    public partial class cost : System.Web.UI.Page
    {
        BLL.Good bll = new BLL.Good();
        BLL.Cost bll2 = new BLL.Cost();
        protected DateTime firstDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);  //第一天
        protected int monthDays = 30;
        protected List<decimal> dcs;
        protected decimal countPay;
        protected MODEL.Cost model;
        protected void Page_Load(object sender, EventArgs e)
        {
            countPay = bll.GetCountForPay(DateTime.Parse(firstDate.ToString("yyyy-MM-dd")), DateTime.Parse(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")));
            model = bll2.GetCountForPay(DateTime.Parse(firstDate.ToString("yyyy-MM-dd")), DateTime.Parse(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")));
            if (model.CID == 0)
            {
                MODEL.Cost mol = new MODEL.Cost();
                mol.CMoney = 1000;
                mol.CDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                mol.CTime = DateTime.Now;
                mol.CDes = "无";
                mol.CIsDel = false;
                mol.CBlack = 0;
                int newID;
                bll2.Add(mol, out newID);
                model = bll2.GetModelByID(newID.ToString());
            }
            //计算每一天的收入
            DateTime dt = string.IsNullOrEmpty(Request.QueryString["dt"]) ? firstDate : DateTime.Parse(Request.QueryString["dt"]);
            TimeSpan tt = dt.AddMonths(1) - dt;
            monthDays = (int)tt.TotalDays;  //获取月的天数
            DateTime lastDate = dt.AddMonths(1).AddDays(-1); //某个月的最后一天

            dcs = bll.GetCountForDay(dt, lastDate);

        }
    }
}