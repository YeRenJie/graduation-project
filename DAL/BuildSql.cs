using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 数据库帮助类
    /// </summary>
    public class BuildSql
    {
        #region 获取数据库列名与参数化查询名称 + public static string GetSqlColumnAndSqlParams(string table, out string para, bool hasId)
        /// <summary>
        /// 获取数据库列名与参数化查询名称
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="para">命令参数</param>
        /// <param name="hasId">是否包含ID,true为包含，false为不包含</param>
        /// <returns>返回列名拼接字符串</returns>
        public static string GetSqlColumnAndSqlParams(string table, out string para, bool hasID)
        {
            para = string.Empty;
            string sbSql = string.Empty;
            string strSql = "SELECT Name FROM SysColumns WHERE id=OBJECT_ID('" + table + "') ORDER BY syscolumns.colid";
            DataTable dt = SqlHelper.GetTable(strSql);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sbSql += dr[0].ToString() + ",";
                    para += "@" + dr[0].ToString() + ",";
                }
            }
            para = para.Substring(0, para.Length - 1);
            sbSql = sbSql.Substring(0, sbSql.Length - 1);

            if (hasID == false)
            {
                para = para.Substring(para.IndexOf(",") + 1);
                sbSql = sbSql.Substring(sbSql.IndexOf(",") + 1);
            }

            return sbSql;
        }
        #endregion

        #region 获取类中的属性值 + public static string GetProValue(string FieldName, object obj)
        /// <summary>
        /// 获取类中的属性值
        /// </summary>
        /// <param name="FieldName">属性名称</param>
        /// <param name="obj">对象</param>
        /// <returns>返回属性值</returns>
        public static string GetProValue(string FieldName, object obj)
        {
            try
            {
                Type Ts = obj.GetType();
                object o = Ts.GetProperty(FieldName).GetValue(obj, null);
                string value = Convert.ToString(o);
                return value;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 设置类中的属性值 + public static bool SetProValue(string FieldName, string value, object obj)
        /// <summary>
        /// 设置类中的属性值
        /// </summary>
        /// <param name="FieldName">属性名称</param>
        /// <param name="value">值</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static bool SetProValue(string FieldName, string value, object obj)
        {
            try
            {
                Type Ts = obj.GetType();
                object v = Convert.ChangeType(value, Ts.GetProperty(FieldName).PropertyType);
                Ts.GetProperty(FieldName).SetValue(obj, v, null);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 设置字符串手写字母大写 + public static string FirstUpper(string str)
        /// <summary>
        ///设置字符串手写字母大写
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <returns>手写字母大写的字符串</returns>
        public static string FirstUpper(string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1);
        }
        #endregion

        #region 获取类中属性类型的值 + public static object GetProValue(object obj, string para)
        /// <summary>
        /// 获取类中属性类型的值
        /// </summary>
        /// <param name="obj">Model对象</param>
        /// <param name="para">参数命令</param>
        /// <returns></returns>
        public static object GetProValue(object obj, string para)
        {
            Type Ts = obj.GetType();
            string temp = para.Substring(para.IndexOf("@") + 1);  //获得属性名称
            string fieldValue = BuildSql.GetProValue(BuildSql.FirstUpper(temp), obj);  //获的属性值
            object v = Convert.ChangeType(fieldValue, Ts.GetProperty(BuildSql.FirstUpper(temp)).PropertyType);  //转化为属性的类型
            return v;
        }
        #endregion

        #region 执行插入操作 + public static int Add(string table, object obj, out int newID)
        /// <summary>
        /// 执行插入操作 
        /// </summary>
        /// <param name="table">要插入的表</param>
        /// <param name="obj">实体对象</param>
        /// <param name="newID">返回插入新的ID</param>
        /// <returns>受影响的行数</returns>
        public static int Add(string table, object obj, out int newID)
        {
            newID = 0;
            int res;
            string columnParas = string.Empty;
            string columnNames = BuildSql.GetSqlColumnAndSqlParams(table, out columnParas, false);
            string strSql = "INSERT " + table + "(" + columnNames + ") values(" + columnParas + ");select @@IDENTITY";
            string[] pas = columnParas.Split(',');

            List<SqlParameter> paras = new List<SqlParameter>();//参数集合
            for (int i = 0; i < pas.Length; i++)
            {
                object v = BuildSql.GetProValue(obj, pas[i]);  //返回属性类型的值
                SqlParameter pa = new SqlParameter(pas[i], v);  //创建集合对象元素
                paras.Add(pa); //加入集合
            }
            res = SqlHelper.ExcuteScalar(strSql, paras.ToArray());
            newID = res;
            return res;
        }
        #endregion

        #region 执行更新修改操作 + public static int UpdateWithModify(string table, object obj)
        /// <summary>
        /// 执行更新修改操作 
        /// </summary>
        /// <param name="table">要修改的表</param>
        /// <param name="obj">实体对象</param>
        /// <returns>受影响行数</returns>
        public static int UpdateWithModify(string table, object obj)
        {
            string columnParas = string.Empty;
            string columnNames = BuildSql.GetSqlColumnAndSqlParams(table, out columnParas, true);
            string strSql = "UPDATE " + table + " SET ";
            string[] pas = columnParas.Split(',');
            string[] names = columnNames.Split(',');

            List<SqlParameter> paras = new List<SqlParameter>();//参数集合
            for (int i = 0; i < pas.Length; i++)
            {
                if (i != 0)
                {
                    strSql += names[i] + "=" + pas[i] + ",";
                }
                object v = BuildSql.GetProValue(obj, pas[i]);  //返回属性类型的值
                SqlParameter pa = new SqlParameter(pas[i], v);  //创建集合对象元素
                paras.Add(pa); //加入集合
            }
            strSql = strSql.Substring(0, strSql.Length - 1);
            strSql += " where " + names[0] + "=" + pas[0];
            return SqlHelper.ExcuteNonQuery(strSql, paras.ToArray());
        }
        #endregion

        #region 执行硬删除操作，此操作将不可恢复 +public static int Del(string table, string ids)
        /// <summary>
        /// 执行硬删除操作，此操作将不可恢复
        /// </summary>
        /// <param name="table">数据库表</param>
        /// <param name="ids">要删除的id</param>
        /// <returns>返回受影响的行数</returns>
        public static int Del(string table, string ids)
        {
            string strSql = string.Empty;
            string columnParas = string.Empty;
            string columnNames = BuildSql.GetSqlColumnAndSqlParams(table, out columnParas, true);
            string[] names = columnNames.Split(',');

            strSql = "DELETE FROM " + table + " WHERE " + names[0] + " IN (" + ids + ")";
            return SqlHelper.ExcuteNonQuery(strSql);
        }
        #endregion

        #region 执行指定列值更新操作 +public static int UpdateFieldValue(string table, string ids, string sqlField, string sqlValue)
        /// <summary>
        /// 执行指定列值更新操作
        /// </summary>
        /// <param name="table">数据库表</param>
        /// <param name="ids">操作的ID</param>
        /// <param name="sqlField">数据库字段</param>
        /// <param name="sqlValue">设置的值</param>
        /// <returns>返回受影响的行数</returns>
        public static int UpdateFieldValue(string table, string ids, string sqlField, string sqlValue)
        {
            string strSql = string.Empty;
            string columnParas = string.Empty;
            string columnNames = BuildSql.GetSqlColumnAndSqlParams(table, out columnParas, true);
            string[] names = columnNames.Split(',');

            strSql = "update " + table + " set " + sqlField + "=" + sqlValue + " where " + names[0] + " in(" + ids + ") ";
            return SqlHelper.ExcuteNonQuery(strSql);
        }
        #endregion

        #region 加载实体对象的数据 +public static void LoadModelData(DataRow dr, ref MODEL.User user, string table)
        /// <summary>
        /// 加载实体对象
        /// </summary>
        /// <param name="dr">行数据</param>
        /// <param name="user">对象</param>
        /// <param name="table">表</param>
        public static void LoadModelData(DataRow dr, object obj, string table)
        {
            string columnParas = string.Empty;
            string columnNames = BuildSql.GetSqlColumnAndSqlParams(table, out columnParas, true);

            string[] names = columnNames.Split(',');

            for (int i = 0; i < names.Length; i++)
            {
                SetProValue(FirstUpper(names[i]), dr[names[i]].ToString(), obj);
            }
        }
        #endregion

        #region 返回分页返回数据表(基于AspPager控件） +  public static DataTable GetDataTableForPager(int lastRowIndex, int pageSize, string table, string uid, string sqlID, string sort = "desc")
        /// <summary>
        /// 返回分页返回数据表(基于AspPager控件）
        /// </summary>
        /// <param name="lastRowIndex">上一页最后一行下标</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="table">数据表</param>
        /// <param name="value">条件值</param>
        /// <param name="sqlID">对应值的数据列名</param>
        /// <param name="sort">排序方式</param>
        /// <param name="sortID">排序列名</param>
        /// <returns></returns>
        public static DataTable GetDataTableForSamplePager(int lastRowIndex, int pageSize, string table, string value, string sqlID, string sort = "desc", string sortID = "")
        {
            string columnParas = string.Empty;
            string columnNames = BuildSql.GetSqlColumnAndSqlParams(table, out columnParas, true);
            string strSql = string.Empty;

            if (sortID == "")
            {
                sortID = sqlID;
            }
            strSql = "select * from " + table + " where " + sqlID + "=" + value + " order by " + sortID + " " + sort;
            return SqlHelper.GetTable(strSql, lastRowIndex, pageSize, table);
        }
        #endregion

        #region 根据ID返回实体对象 + public static T GetModelById<T>(string table, string id) where T : new()
        /// <summary>
        /// 根据ID返回实体对象
        /// </summary>
        /// <typeparam name="T">返回数据的类型</typeparam>
        /// <param name="table">数据表</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static T GetModelById<T>(string table, string id) where T : new()
        {
            string columnParas = string.Empty;
            string columnNames = BuildSql.GetSqlColumnAndSqlParams(table, out columnParas, true);
            string strSql = "SELECT " + columnNames + " FROM " + table + " WHERE " + columnNames.Split(',')[0] + "=" + id;
            DataTable dt = SqlHelper.GetTable(strSql);
            T obj = new T();
            if (dt.Rows.Count > 0)
            {
                LoadModelData(dt.Rows[0], obj, table);
            }
            return obj;
        }
        #endregion

        #region 获取联表查询的数据 +  public static DataTable GetDataForJoinTables(string table1, string table1ID, string table2, string table2JoinID, string sqlWhere = "")
        /// <summary>
        /// 获取联表查询的数据
        /// </summary>
        /// <param name="table1">表1</param>
        /// <param name="table1ID">与表2关联的列名</param>
        /// <param name="table2">表2</param>
        /// <param name="table2JoinID">与表1关联的列名</param>
        /// <param name="sqlWhere">过滤条件（可拼接）</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataForJoinTables(string table1, string table1ID, string table2, string table2JoinID, string sqlWhere = "")
        {
            string strSql = "SELECT  * FROM " + table1 + " JOIN " + table2 + " ON " + table1 + "." + table1ID + " = " + table2 + "." + table2JoinID + " " + sqlWhere;
            return SqlHelper.GetTable(strSql);
        }
        #endregion

        #region 根据指定条件（指定列或值）返回实体对象 +   public static T GetModelByWhere<T>(string table, string sqlName, string value) where T : new()
        /// <summary>
        /// 根据指定条件（指定列或值）返回实体对象
        /// </summary>
        /// <typeparam name="T">返回的数据类型</typeparam>
        /// <param name="table">数据表</param>
        /// <param name="sqlName">数据库字段名称</param>
        /// <param name="value">字段对应的值</param>
        /// <returns></returns>
        public static T GetModelByWhere<T>(string table, string sqlName, string value) where T : new()
        {
            string columnParas = string.Empty;
            string columnNames = BuildSql.GetSqlColumnAndSqlParams(table, out columnParas, true);
            string strSql = "SELECT " + columnNames + " FROM " + table + " WHERE " + sqlName + "='" + value + "'";
            DataTable dt = SqlHelper.GetTable(strSql);
            T obj = new T();
            if (dt.Rows.Count > 0)
            {
                LoadModelData(dt.Rows[0], obj, table);
            }
            return obj;
        }
        #endregion

        #region 返回泛型集合对象 +  public static List<T> GetListByWhere<T>(string table, string desc = "desc", string sqlWhere = "") where T : new()
        /// <summary>
        /// 返回泛型集合对象
        /// </summary>
        /// <typeparam name="T">返回数据的类型</typeparam>
        /// <param name="table">数据表</param>
        /// <param name="desc">排序方法</param>
        /// <param name="sqlWhere">条件，默认为空</param>
        /// <returns></returns>
        public static List<T> GetListByWhere<T>(string table, string desc = "desc", string sqlWhere = "") where T : new()
        {
            string columnParas = string.Empty;
            string columnNames = BuildSql.GetSqlColumnAndSqlParams(table, out columnParas, true);
            string strSql = "SELECT " + columnNames + " FROM " + table + " " + sqlWhere + " " + " order by " + columnNames.Split(',')[0] + " " + desc;
            DataTable dt = SqlHelper.GetTable(strSql);

            List<T> list = new List<T>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    T obs = new T();
                    BuildSql.LoadModelData(dr, obs, table);
                    list.Add(obs);
                }
            }
            return list;
        }
        #endregion

        #region 简单分页返回泛型集合数据+   public static List<T> SimplePager<T>(string table, string paseSize, string currentPage, string sqlWhere = "") where T : new()
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">返回数据的类型</typeparam>
        /// <param name="table">数据表对象</param>
        /// <param name="paseSize">页容量</param>
        /// <param name="currentPage">当前页数</param>
        /// <param name="sqlWhere">分页条件</param>
        /// <returns></returns>
        public static List<T> SimplePager<T>(string table, string paseSize, string currentPage, string sqlWhere = "") where T : new()
        {
            string columnParas = string.Empty;
            string columnNames = BuildSql.GetSqlColumnAndSqlParams(table, out columnParas, true);
            string strSql = "select top " + paseSize + " * from ( select ROW_NUMBER() OVER (order by " + columnNames.Split(',')[0] + " desc) as RowNumber,* from " + table + " " + sqlWhere + " " + ") A where RowNumber>" + paseSize + "*(" + currentPage + "-1) ";
            DataTable dt = SqlHelper.GetTable(strSql);

            List<T> list = new List<T>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    T obs = new T();
                    BuildSql.LoadModelData(dr, obs, table);
                    list.Add(obs);
                }
            }
            return list;
        }
        #endregion

        #region 获取指定条件的总条数 + public static int GetCount(string table, string sqlWhere = "")
        /// <summary>
        /// 获取指定条件的总条数
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="sqlWhere">条件</param>
        /// <returns></returns>
        public static int GetCount(string table, string sqlWhere = "")
        {
            string strSql = "select count(*) from " + table + " " + sqlWhere + " ";
            return SqlHelper.ExcuteScalar(strSql);
        }
        #endregion

        #region 返回总页数 +  public static int GetPageCount(string table, int pageSize, string sqlWhere = "")
        /// <summary>
        /// 返回总页数
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="sqlWhere">条件</param>
        /// <returns>返回页数</returns>
        public static int GetPageCount(string table, int pageSize, string sqlWhere = "")
        {
            int page = 1;
            int count = GetCount(table, sqlWhere);
            if (count >= pageSize)
            {
                if (count % pageSize == 0)
                {
                    page = count / pageSize;
                }
                else
                {
                    page = count / pageSize + 1;
                }

            }
            return page;
        }
        #endregion
    }
}
