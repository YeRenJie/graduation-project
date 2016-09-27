using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Good
    {
        DAL.Good dal = new DAL.Good();

        #region 执行增加操作 + public bool Add(MODEL.Good model, out int newID)
        /// <summary>
        /// 执行增加操作
        /// </summary>
        /// <param name="model"></param>
        /// <param name="newID"></param>
        /// <returns></returns>
        public bool Add(MODEL.Good model, out int newID)
        {
            return dal.Add(model, out newID) > 0;
        }
        #endregion

        #region 执行删除操作 + public bool Del(string ids)
        /// <summary>
        /// 执行删除操作
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Del(string ids)
        {
            return dal.Del(ids) > 0;
        }
        #endregion

        #region 执行更新操作 + public bool UpdateWithModify(MODEL.Good model)
        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateWithModify(MODEL.Good model)
        {
            return dal.UpdateWithModify(model) > 0;
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
            return dal.GetModelByID(id);
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
            return dal.GetSimplePageList(pageSize, currentPage, sqlWhere);
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
            return dal.GetPageCount(pageSize, sqlWhere);
        }
        #endregion

        #region 获取总条数 + public int GetCount()
        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <returns></returns>
        public int GetCount(string sqlWhere = "")
        {
            return dal.GetCount(sqlWhere);
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
            return dal.GetCountForPay(dt1, dt2);
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
            return dal.GetCountForWhere(sqlWhere);
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
            return dal.GetCountForDay(dt1, dt2);
        }
        #endregion

    }
}
