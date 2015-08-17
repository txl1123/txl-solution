using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;

namespace TxlMvc.Helper
{
    /// <summary>
    /// 数据访问层操作类
    /// </summary>
    public class SqlHelper
    {
        public static readonly string ConnectionString = ConfigurationSettings.AppSettings["sqlcon"].ToString().Trim();
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        #region //ExecteNonQuery方法
        /// <summary>
        /// 执行一个不需要返回值的SqlCommand命令
        /// </summary>
        /// <param name="connectionString">有效的数据库连接字符串</param>
        /// <param name="cmdTpye">sqlcommand命令类型</param>
        /// <param name="cmdText">sql语句或存储过程名</param>
        /// <param name="cmdParameter">参数，可为null</param>
        /// <returns>返回命令执行后影响的行数</returns>
        private static int ExecteNonQuery(string connectionString,CommandType cmdTpye,string cmdText,params SqlParameter[] cmdParameter)
        {
            SqlCommand cmd = new SqlCommand();
            using(SqlConnection conn=new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdTpye, cmdText, cmdParameter);
                int result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return result;
            }
        }
        /// <summary>
        /// 执行一个不需要返回值的命令
        /// </summary>
        /// <param name="connectionString">有效的数据库连接字符串</param>
        /// <param name="cmdTpye">sqlcommand命令类型</param>
        /// <param name="cmdText">sql语句或存储过程名</param>
        /// <param name="cmdParameter">参数，可为null</param>
        /// <returns>返回命令执行后影响的行数</returns>
        public static int ExecteNonQuery( CommandType cmdTpye, string cmdText, params SqlParameter[] cmdParameter)
        {
            return ExecteNonQuery(ConnectionString, cmdTpye, cmdText, cmdParameter);
        }
        /// <summary>
        ///存储过程专用
        /// </summary>
        /// <param name="cmdText">存储过程的名字</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecteNonQueryProducts(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecteNonQuery(CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        ///Sql语句专用
        /// </summary>
        /// <param name="cmdText">T_Sql语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecteNonQueryText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecteNonQuery(CommandType.Text, cmdText, commandParameters);
        }
        #endregion

        #region //ExecuteDataSet
        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="connectionString">数据库链接字符串</param>
        /// <param name="cmdType">Sqlcommand命令类型</param>
        /// <param name="cmdText">存储过程或T-Sql</param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        private static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, String cmdText, params SqlParameter[] cmdParams)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParams);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

         /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="cmdType">Sqlcommand命令类型</param>
        /// <param name="cmdText">存储过程或T-Sql</param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(CommandType cmdType, String cmdText, params SqlParameter[] cmdParams)
        {
            return ExecuteDataSet(ConnectionString, cmdType, cmdText, cmdParams);
        }
        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="cmdText">存储过程的名字</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSetProducts(string cmdText, params SqlParameter[] cmdParameters)
        {
            return ExecuteDataSet(ConnectionString, CommandType.StoredProcedure, cmdText, cmdParameters);
        }

        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="cmdText">T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSetText(string cmdText, params SqlParameter[] cmdParameters)
        {
            return ExecuteDataSet(ConnectionString, CommandType.Text, cmdText, cmdParameters);
        }
        #endregion

        #region //GetTable
        /// <summary>
        /// 返回Table集合
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        private static DataTableCollection GetTable(string connectionString,CommandType cmdType,string cmdText,params SqlParameter[] cmdParms)
        {
            SqlCommand cmd=new SqlCommand();
            SqlConnection conn=new SqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                DataTableCollection tables = ds.Tables;
                return tables;
            }
            catch {
                conn.Close();
                throw;
            }
        }

        public static DataTableCollection GetTable(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            return GetTable(ConnectionString, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 存储过程专用
        /// </summary>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public static DataTableCollection GetTableProducts(string cmdText, SqlParameter[] cmdParms)
        {
            return GetTable(CommandType.StoredProcedure, cmdText, cmdParms);
        }

        /// <summary>
        /// Sql语句专用
        /// </summary>
        /// <param name="cmdText"> T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个表集合(DataTableCollection)表示查询得到的数据集</returns>
        public static DataTableCollection GetTableText(string cmdText, SqlParameter[] cmdParms)
        {
            return GetTable(CommandType.Text, cmdText, cmdParms);
        }
        #endregion

        #region // ExecuteScalar方法


        /// <summary>
        /// 返回第一行的第一列
        /// </summary>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个对象</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(ConnectionString, cmdType, cmdText, commandParameters);
        }

        /// <summary>
        /// 返回第一行的第一列存储过程专用
        /// </summary>
        /// <param name="cmdText">存储过程的名字</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个对象</returns>
        public static object ExecuteScalarProducts(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(ConnectionString, CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        /// 返回第一行的第一列Sql语句专用
        /// </summary>
        /// <param name="cmdText">者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个对象</returns>
        public static object ExecuteScalarText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(ConnectionString, CommandType.Text, cmdText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        #endregion

        /// <summary>
        /// 为执行命令准备参数
        /// </summary>
        /// <param name="cmd">sqlcommand命令</param>
        /// <param name="conn">数据库连接字符</param>
        /// <param name="trans">数据库事务处理</param>
        /// <param name="cmdTpye">sqlcommand命令类型(存储过程， T-SQL语句 等等)</param>
        /// <param name="cmdText">执行的命令文本（T-SQL）</param>
        /// <param name="cmdParms">返回带参数的命令</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdTpye, string cmdText, SqlParameter[] cmdParms)
        {
            //判断数据库连接状态
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            //判断是否需要事务处理
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdTpye;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// 执行命令，返回一个在连接字符串中指定的数据库结果集
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="cmdTpye">SqlCommand命令类型</param>
        /// <param name="cmdText">执行的命令文本（T-SQL）</param>
        /// <param name="cmdParams"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdTpye, string cmdText, params SqlParameter[] cmdParams)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdTpye, cmdText, cmdParams);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        /// <summary>
        /// DataSetToList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_DataSet"></param>
        /// <param name="p_TableIndex">表index</param>
        /// <returns></returns>
        public static IList<T> DataSetToIList<T>(DataSet p_DataSet, int p_TableIndex)
        {
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return null;
            if (p_TableIndex > p_DataSet.Tables.Count - 1)
                return null;
            if (p_TableIndex < 0)
                p_TableIndex = 0;

            DataTable p_Data = p_DataSet.Tables[p_TableIndex];
            // 返回值初始化 
            IList<T> result = new List<T>();
            for (int j = 0; j < p_Data.Rows.Count; j++)
            {
                T _t = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    for (int i = 0; i < p_Data.Columns.Count; i++)
                    {
                        // 属性与字段名称一致的进行赋值 
                        //不区分大小写
                        if ((pi.Name.ToUpper()).Equals(p_Data.Columns[i].ColumnName.ToUpper()))
                        {
                            // 数据库NULL值单独处理 
                            if (p_Data.Rows[j][i] != DBNull.Value)
                                pi.SetValue(_t, p_Data.Rows[j][i], null);
                            else
                                pi.SetValue(_t, null, null);
                            break;
                        }
                    }
                }
                result.Add(_t);
            }
            return result;
        }
    }
}