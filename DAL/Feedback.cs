using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Feedback
    {
        string strTable = "dbo.[Feedback]";

        #region 执行增加操作 + public int Add(MODEL.Feedback model, out int newID)
        /// <summary>
        /// 执行增加操作
        /// </summary>
        /// <param name="model"></param>
        /// <param name="newID"></param>
        /// <returns></returns>
        public int Add(MODEL.Feedback model, out int newID)
        {
            return BuildSql.Add(strTable, model, out newID);
        }
        #endregion

        #region 执行删除操作 +public int Del(string ids)
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

        #region 执行更新操作 + public int UpdateWithModify(MODEL.Feedback model)
        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <returns></returns>
        public int UpdateWithModify(MODEL.Feedback model)
        {
            return BuildSql.UpdateWithModify(strTable, model);
        }
        #endregion

        #region 根据简单分页获取数据 +　public List<MODEL.Feedback> GetSimplePageList(string pageSize, string currentPage, string sqlWhere = "")
        /// <summary>
        /// 根据简单分页获取数据
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public List<MODEL.Feedback> GetSimplePageList(string pageSize, string currentPage, string sqlWhere = "")
        {
            return BuildSql.SimplePager<MODEL.Feedback>(strTable, pageSize, currentPage, sqlWhere);
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

        #region 根据ID返回实体对象 +  public MODEL.Feedback GetModelByID(string id)
        /// <summary>
        /// 根据ID返回实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MODEL.Feedback GetModelByID(string id)
        {
            return BuildSql.GetModelById<MODEL.Feedback>(strTable, id);
        }
        #endregion

        #region 获取留言的楼数 + public int GetCountForWhere(string id)
        /// <summary>
        /// 获取留言的楼数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetCountForWhere(string id)
        {
            return BuildSql.GetCount(strTable, " where fID<" + id);
        }
        #endregion

        #region 获取总条数 + public int GetCount()
        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return BuildSql.GetCount(strTable);
        }
        #endregion
    }
}
