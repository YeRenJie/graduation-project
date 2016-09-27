using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Good
    {
        string strSql = "dbo.[Good]";

        #region 执行增加操作 + public int Add(MODEL.Good model, out int newID)
        /// <summary>
        /// 执行增加操作
        /// </summary>
        /// <param name="model"></param>
        /// <param name="newID"></param>
        /// <returns></returns>
        public int Add(MODEL.Good model, out int newID)
        {
            return BuildSql.Add(strSql, model, out newID);
        }
        #endregion

        #region 执行删除操作 + public int Del(string ids)
        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int Del(string ids)
        {
            return BuildSql.Del(strSql, ids);
        }
        #endregion

        #region 执行更新操作 + public int UpdateWithModify(MODEL.Good model)
        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateWithModify(MODEL.Good model)
        {
            return BuildSql.UpdateWithModify(strSql, model);
        }
        #endregion

        #region 根据ID查询实体对象 + public MODEL.Good GetModelByID(string id)
        /// <summary>
        /// 根据ID查询实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MODEL.Good GetModelByID(string id)
        {
            return BuildSql.GetModelById<MODEL.Good>(strSql, id);
        }
        #endregion

        #region 根据简单分页获取数据 +　public List<MODEL.Good> GetSimplePageList(string pageSize, string currentPage, string sqlWhere = "")
        /// <summary>
        /// 根据简单分页获取数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public List<MODEL.Good> GetSimplePageList(string pageSize, string currentPage, string sqlWhere = "")
        {
            return BuildSql.SimplePager<MODEL.Good>(strSql, pageSize, currentPage, sqlWhere);
        }
        #endregion

        #region 根据页容量条件返回总页数 + public int GetPageCount(int pageSize, string sqlWhere)
        /// <summary>
        /// 根据页容量条件返回总页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int GetPageCount(int pageSize, string sqlWhere = "")
        {
            return BuildSql.GetPageCount(strSql, pageSize, sqlWhere);
        }
        #endregion

        #region 获取总条数 + public int GetCount()
        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <returns></returns>
        public int GetCount(string sqlWhere = "")
        {
            return BuildSql.GetCount(strSql, sqlWhere);
        }
        #endregion

        #region 返回指定时间段的总支出 + public decimal GetCountForPay(DateTime dt1, DateTime dt2)
        /// <summary>
        /// 返回指定时间段的总支出
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public decimal GetCountForPay(DateTime dt1, DateTime dt2)
        {
            string sql = "SELECT SUM(gCount*gPrice) FROM " + strSql + " WHERE gBuyTime BETWEEN '" + dt1.ToString("yyyy-MM-dd") + "' AND '" + dt2.ToString("yyyy-MM-dd") + "'";
            return SqlHelper.ExcuteScalarForDecimal(sql);
        }
        #endregion

        #region 返回指定条件的总支出 + public decimal GetCountForWhere(string sqlWhere="")
        /// <summary>
        /// 返回指定条件的总支出
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public decimal GetCountForWhere(string sqlWhere = "")
        {
            string sql = "SELECT SUM(gCount*gPrice) FROM " + strSql + sqlWhere + " ";
            return SqlHelper.ExcuteScalarForDecimal(sql);
        }
        #endregion

        #region 返回月中每一天的费用 +  public List<decimal> GetCountForDay(DateTime dt1, DateTime dt2)
        /// <summary>
        /// 返回每一天的费用
        /// </summary>
        /// <param name="dt1"></param>
        /// <returns></returns>
        public List<decimal> GetCountForDay(DateTime dt1, DateTime dt2)
        {

            List<decimal> dcs = new List<decimal>();
            string sql = "SELECT ISNULL(SUM(gCount*gPrice),0) FROM dbo.Good WHERE gBuyTime='" + dt1.ToString("yyyy-MM-dd") + "'";

            while (dt1.CompareTo(dt2) < 0)
            {
                dt1 = dt1.AddDays(1);
                sql += " union all " + "SELECT ISNULL(SUM(gCount*gPrice),0) FROM dbo.Good WHERE gBuyTime='" + dt1.ToString("yyyy-MM-dd") + "'";
            }

            System.Data.DataTable dt = SqlHelper.GetTable(sql);

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                dcs.Add(Convert.ToDecimal(dr[0]));
            }
            return dcs;
        }
        #endregion

    }
}
