using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class User
    {
        private string strTable = "dbo.[User]";

        #region 执行新增操作 + public int Add(MODEL.User model, out int newID)
        /// <summary>
        /// 执行新增操作
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="newID">返回新记录的主键值</param>
        /// <returns></returns>
        public int Add(MODEL.User model, out int newID)
        {
            return BuildSql.Add(strTable, model, out newID);
        }
        #endregion

        #region 执行硬删除操作 + public int Del(string ids)
        /// <summary>
        /// 执行硬删除操作
        /// </summary>
        /// <param name="ids">要删除的ID</param>
        /// <returns></returns>
        public int Del(string ids)
        {
            return BuildSql.Del(strTable, ids);
        }
        #endregion

        #region 执行更新操作 + public int UpdateWithModify(MODEL.User model)
        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <returns></returns>
        public int UpdateWithModify(MODEL.User model)
        {
            return BuildSql.UpdateWithModify(strTable, model);
        }
        #endregion

        #region 根据ID查询实体对象 + public MODEL.User GetModelByID(string id)
        /// <summary>
        /// 根据ID查询实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MODEL.User GetModelByID(string id)
        {
            return BuildSql.GetModelById<MODEL.User>(strTable, id);
        }
        #endregion

        #region 根据简单分页返回泛型集合 + public List<MODEL.User> GetListForSimplePager(string pageSize, string currentPage, string sqlWhere = "")
        /// <summary>
        /// 根据简单分页返回泛型集合
        /// </summary>
        /// <param name="pageSize">页容量</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="sqlWhere">条件</param>
        /// <returns></returns>
        public List<MODEL.User> GetListForSimplePager(string pageSize, string currentPage, string sqlWhere = "")
        {
            return BuildSql.SimplePager<MODEL.User>(strTable, pageSize, currentPage, sqlWhere);
        }
        #endregion

        #region 根据指定条件返回总记录数 + public int GetCountForWhere(string sqlWhere = "")
        /// <summary>
        /// 根据指定条件返回总记录数
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int GetCountForWhere(string sqlWhere = "")
        {
            return BuildSql.GetCount(strTable, sqlWhere);
        }
        #endregion

        #region 根据页容量返回总页数 + public int GetPageCount(int pageSize, string sqlWhere = "")
        /// <summary>
        /// 根据页容量返回总页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int GetPageCount(int pageSize, string sqlWhere = "")
        {
            return BuildSql.GetPageCount(strTable, pageSize, sqlWhere);
        }
        #endregion

        #region 根据指定字段更新值 + public int UpdateFieldValue(string ids, string fieldName, string fieldValue)
        /// <summary>
        /// 根据指定字段更新值
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public int UpdateFieldValue(string ids, string fieldName, string fieldValue)
        {
            return BuildSql.UpdateFieldValue(strTable, ids, fieldName, fieldValue);
        }
        #endregion

        #region 根据用户名返回实体对象 + public MODEL.User GetModelByName(string name)
        /// <summary>
        /// 根据用户名返回实体对象
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        public MODEL.User GetModelByName(string name)
        {
            return BuildSql.GetModelByWhere<MODEL.User>(strTable, "uLoginName", name);
        }
        #endregion
    }
}
