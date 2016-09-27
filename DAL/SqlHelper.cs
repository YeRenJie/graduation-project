using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.IO;

namespace DAL
{
    /// <summary>
    /// 数据库操作帮助类
    /// </summary>
    public class SqlHelper
    {
        //从配置文件读取 连接字符串
        static string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        #region 01.执行查询语句 返回数据表 +DataTable GetTable(string sqlStr, params SqlParameter[] paras)
        /// <summary>
        /// 执行查询语句 返回数据表
        /// </summary>
        /// <param name="sqlStr">查询语句</param>
        /// <param name="paras">参数集合</param>
        /// <returns></returns>
        public static DataTable GetTable(string sqlStr, params SqlParameter[] paras)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(sqlStr, conn);
                da.SelectCommand.Parameters.AddRange(paras);
                da.Fill(dt);
            }
            return dt;
        }
        #endregion

        #region 02.执行增删改操作，返回受影响行数+ int ExcuteNonQuery(string sql, params SqlParameter[] paras)
        /// <summary>
        /// 执行增删改操作，返回受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static int ExcuteNonQuery(string sql, params SqlParameter[] paras)
        {
            int res = -1;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(paras);
                conn.Open();
                res = cmd.ExecuteNonQuery();
            }
            return res;
        }
        #endregion

        #region 03.执行查询单个值操作，返回受结果集的第一行第一列+ int ExcuteScalar(string sql, params SqlParameter[] paras)
        /// <summary>
        /// 执行查询单个值操作，返回受结果集的第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static int ExcuteScalar(string sql, params SqlParameter[] paras)
        {
            int res = -1;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(paras);
                conn.Open();

                try
                {
                    object o = cmd.ExecuteScalar();
                    res = Convert.ToInt32(o);
                }
                catch (Exception)
                {
                    res = 0;
                }
                finally { }
            }
            return res;
        }
        #endregion

        #region 04.执行查询单个值操作，返回受结果集的第一行第一列,类型为Decimal+ public static decimal ExcuteScalarForDecimal(string sql, params SqlParameter[] paras)
        /// <summary>
        /// 执行查询单个值操作，返回受结果集的第一行第一列,类型为Decimal
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static decimal ExcuteScalarForDecimal(string sql, params SqlParameter[] paras)
        {
            decimal res = -1;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddRange(paras);
                conn.Open();

                try
                {
                    object o = cmd.ExecuteScalar();
                    res = Convert.ToDecimal(o);
                }
                catch (Exception)
                {
                    res = 0;
                }
                finally { }
            }
            return res;
        }
        #endregion

        #region 01.执行查询语句 返回数据表 +DataTable GetTable(string sqlStr, params SqlParameter[] paras)
        /// <summary>
        /// 执行查询语句 返回数据表
        /// </summary>
        /// <param name="paras">参数集合</param>
        /// <returns></returns>
        public static DataTable GetPagedTable(int pageIndex, int pageSize, out int rowCount, out int pageCount, bool isDel)
        {
            DataTable dt = new DataTable();
            rowCount = 0;
            pageCount = 0;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("up_GetPagedData2", conn);
                SqlParameter[] paras = { 
                                       new SqlParameter("@pageIndex",pageIndex),
                                       new SqlParameter("@pageSize",pageSize),
                                       new SqlParameter("@rowCount",rowCount),
                                       new SqlParameter("@pageCount",pageCount),
                                       new SqlParameter("@isDel",isDel)
                                       };
                //将两个输出参数的输出方向指定
                paras[2].Direction = ParameterDirection.Output;
                paras[3].Direction = ParameterDirection.Output;
                //将参数集合 加入到 查询命令对象中
                da.SelectCommand.Parameters.AddRange(paras);
                //设置 查询命令类型 为 存储过程
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //执行存储过程
                da.Fill(dt);
                //执行完后，将存储过程 获得的 两个输出参数值 赋给 此方法的两个输出参数
                rowCount = Convert.ToInt32(paras[2].Value);
                pageCount = Convert.ToInt32(paras[3].Value);
            }
            return dt;
        }
        #endregion

        #region 02.配合分页控件获得分页数据 +DataTable GetPagedTableByDataPage
        /// <summary>
        /// 配合分页控件获得分页数据
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPagedTableByDataPager(int rowIndex, int pageSize, out int rowCount, out int pageCount, bool isDel)
        {
            DataTable dt = new DataTable();
            rowCount = 0;
            pageCount = 0;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter("up_GetPagedDataByDataPager", conn);
                SqlParameter[] paras = { 
                                       new SqlParameter("@lastRowIndex",rowIndex),
                                       new SqlParameter("@pageSize",pageSize),
                                       new SqlParameter("@rowCount",rowCount),
                                       new SqlParameter("@pageCount",pageCount),
                                       new SqlParameter("@isDel",isDel)
                                       };
                //将两个输出参数的输出方向指定
                paras[2].Direction = ParameterDirection.Output;
                paras[3].Direction = ParameterDirection.Output;
                //将参数集合 加入到 查询命令对象中
                da.SelectCommand.Parameters.AddRange(paras);
                //设置 查询命令类型 为 存储过程
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //执行存储过程
                da.Fill(dt);
                //执行完后，将存储过程 获得的 两个输出参数值 赋给 此方法的两个输出参数
                rowCount = Convert.ToInt32(paras[2].Value);
                pageCount = Convert.ToInt32(paras[3].Value);
            }
            return dt;
        }
        #endregion

        #region 根据分页控件获取表 +public static DataTable GetTable(string sqlStr, int lastRowIndex, int pageSize, string table, params SqlParameter[] paras)
        /// <summary>
        /// 根据分页控件获取表
        /// </summary>
        /// <param name="sqlStr">SQL语句</param>
        /// <param name="lastRowIndex">上一页的最后一行的下标</param>
        /// <param name="pageSize">页容量</param>
        /// <param name="table">SQL表</param>
        /// <param name="paras">命令参数</param>
        /// <returns>表对象</returns>
        public static DataTable GetTable(string sqlStr, int lastRowIndex, int pageSize, string table, params SqlParameter[] paras)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(sqlStr, conn);
                da.SelectCommand.Parameters.AddRange(paras);
                da.Fill(ds, lastRowIndex, pageSize, table);
            }
            return ds.Tables[table];
        }
        #endregion

        #region 生成实体类成员字段，不包括构造方法 + public static string BuildModelField(string table)
        /// <summary>
        /// 生成实体类成员字段，不包括构造方法
        /// </summary>
        /// <param name="table">数据表</param>
        /// <returns>字段名称</returns>
        public static string BuildModelField(string table)
        {
            string _model = "";
            string txt1 = "";
            string txt2 = "";
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand("select * from " + table, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            for (int i = 0; i < dr.FieldCount; i++)
            {

                string sny = dr.GetName(i);
                txt1 += "private " + dr.GetFieldType(i).ToString() + " " + sny + ";\r\n";
                txt2 += "public " + dr.GetFieldType(i).ToString() + " " + BuildSql.FirstUpper(sny) + "\r\n{\r\n\t get \r\n\t{\r\n\t\t return this." + sny + ";\r\n\t}\r\n\t set \r\n\t{\r\n\t\t this." + sny + "=value;\r\n\t}\r\n}\r\n";

            }
            txt1 += "\r\n";
            _model += txt1 + txt2;
            return _model;
        }
        #endregion

        #region 执行后台数据库的安装 + public static void ExecuteSQLFile(string sqlFileName, string dataName)
        /// <summary>
        /// 执行后台数据库的安装
        /// </summary>
        /// <param name="sqlFileName">数据库路径</param>
        /// <param name="dataName">数据库名称</param>
        public static void ExecuteSQLFile(string sqlFileName, string dataName)
        {
            SqlConnection connecction = null;
            try
            {
                connecction = new SqlConnection(connStr);
                SqlCommand command = connecction.CreateCommand();
                connecction.Open();

                using (FileStream stream = new FileStream(sqlFileName, FileMode.Open))
                {
                    StreamReader reader = new StreamReader(stream);

                    StringBuilder builder = new StringBuilder();
                    String strLine = "";
                    while ((strLine = reader.ReadLine()) != null)
                    {
                        if (strLine.Trim().ToUpper() != @"GO")
                        {
                            builder.AppendLine(strLine);
                        }
                        else
                        {
                            command.CommandText = builder.ToString().Replace("winu4.0", dataName);
                            command.ExecuteNonQuery();
                            builder.Remove(0, builder.Length);
                        }
                    }
                    reader.Close();
                    stream.Close();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (connecction != null && connecction.State != ConnectionState.Closed)
                {
                    connecction.Close();
                }
            }
        #endregion
        }
    }
}
