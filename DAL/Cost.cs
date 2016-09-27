using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Cost
    {
        string strTable = "dbo.[Cost]";

        #region 执行增加操作 + public int Add(MODEL.Cost model, out int newID)
        /// <summary>
        /// 执行增加操作
        /// </summary>
        /// <param name="model"></param>
        /// <param name="newID"></param>
        /// <returns></returns>
        public int Add(MODEL.Cost model, out int newID)
        {
            return BuildSql.Add(strTable, model, out newID);
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
            return BuildSql.Del(strTable, ids);
        }
        #endregion

        #region 执行更新操作 + public int UpdateWithModify(MODEL.Cost model)
        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateWithModify(MODEL.Cost model)
        {
            return BuildSql.UpdateWithModify(strTable, model);
        }
        #endregion

        #region 更新指定字段 + public int UpdateForField(string ids, string field, string value)
        /// <summary>
        /// 更新指定字段
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int UpdateForField(string ids, string field, string value)
        {
            return BuildSql.UpdateFieldValue(strTable, ids, field, value);
        }
        #endregion

        #region 根据ID查询一条记录 + public MODEL.Cost GetModelByID(string id)
        /// <summary>
        /// 根据ID查询一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MODEL.Cost GetModelByID(string id)
        {
            return BuildSql.GetModelById<MODEL.Cost>(strTable, id);
        }
        #endregion

        #region 根据简单分页获取数据 +　public List<MODEL.Cost> GetSimplePageList(string pageSize, string currentPage, string sqlWhere = "")
        /// <summary>
        /// 根据简单分页获取数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public List<MODEL.Cost> GetSimplePageList(string pageSize, string currentPage, string sqlWhere = "")
        {
            return BuildSql.SimplePager<MODEL.Cost>(strTable, pageSize, currentPage, sqlWhere);
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
            return BuildSql.GetPageCount(strTable, pageSize, sqlWhere);
        }
        #endregion

        #region 获取总条数 + public int GetCount()
        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <returns></returns>
        public int GetCount(string sqlWhere = "")
        {
            return BuildSql.GetCount(strTable, sqlWhere);
        }
        #endregion

        #region 返回指定时间段的总收入 + public MODEL.Cost GetCountForPay(DateTime dt1, DateTime dt2)
        /// <summary>
        /// 返回指定时间段的总收入
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public MODEL.Cost GetCountForPay(DateTime dt1, DateTime dt2)
        {
            string sql = "SELECT * from " + strTable + " WHERE cDate BETWEEN '" + dt1.ToString("yyyy-MM-dd") + "' AND '" + dt2.ToString("yyyy-MM-dd") + "'";
            System.Data.DataTable dt = SqlHelper.GetTable(sql);
            MODEL.Cost model = new MODEL.Cost();
            if (dt.Rows.Count > 0)
            {
                BuildSql.LoadModelData(dt.Rows[0], model, strTable);
            }
            return model;
        }
        #endregion

    }
}
