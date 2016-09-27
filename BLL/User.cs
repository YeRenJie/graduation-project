using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class User
    {
        DAL.User dal = new DAL.User();

        #region 执行新增操作 + public bool Add(MODEL.User model, out int newID)
        /// <summary>
        /// 执行新增操作
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="newID">返回新记录的主键值</param>
        /// <returns></returns>
        public bool Add(MODEL.User model, out int newID)
        {
            return dal.Add(model, out newID) > 0;
        }
        #endregion

        #region 执行硬删除操作 + public bool Del(string ids)
        /// <summary>
        /// 执行硬删除操作
        /// </summary>
        /// <param name="ids">要删除的ID</param>
        /// <returns></returns>
        public bool Del(string ids)
        {
            return dal.Del(ids) > 0;
        }
        #endregion

        #region 执行更新操作 + public bool UpdateWithModify(MODEL.User model)
        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <returns></returns>
        public bool UpdateWithModify(MODEL.User model)
        {
            return dal.UpdateWithModify(model) > 0;
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
            return dal.GetModelByID(id);
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
            return dal.GetListForSimplePager(pageSize, currentPage, sqlWhere);
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
            return dal.GetCountForWhere(sqlWhere);
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
            return dal.GetPageCount(pageSize, sqlWhere);
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
        public bool UpdateFieldValue(string ids, string fieldName, string fieldValue)
        {
            return dal.UpdateFieldValue(ids, fieldName, fieldValue) > 0;
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
            return dal.GetModelByName(name);
        }
        #endregion

    }
}
