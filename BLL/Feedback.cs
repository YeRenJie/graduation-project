using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Feedback
    {
        DAL.Feedback dal = new DAL.Feedback();

        #region 执行增加操作 + public bool Add(MODEL.Feedback model, out int newID)
        /// <summary>
        /// 执行增加操作
        /// </summary>
        /// <param name="model"></param>
        /// <param name="newID"></param>
        /// <returns></returns>
        public bool Add(MODEL.Feedback model, out int newID)
        {
            return dal.Add(model, out newID) > 0;
        }
        #endregion

        #region 执行删除操作 +public bool Del(string ids)
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

        #region 执行更新操作 + public bool UpdateWithModify(MODEL.Feedback model)
        /// <summary>
        /// 执行更新操作
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <returns></returns>
        public bool UpdateWithModify(MODEL.Feedback model)
        {
            return dal.UpdateWithModify(model) > 0;
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

        #region 根据ID返回实体对象 +  public MODEL.Feedback GetModelByID(string id)
        /// <summary>
        /// 根据ID返回实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MODEL.Feedback GetModelByID(string id)
        {
            return dal.GetModelByID(id);
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
            return dal.GetCountForWhere(id);
        }
        #endregion

        #region 获取总条数 + public int GetCount()
        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            return dal.GetCount();
        }
        #endregion

    }
}
