using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// 帮助类
/// </summary>
public class SqlHelper
{
    /// <summary>
    /// 连接数据库字符串
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string ConnectionString__(string qianzhui, string ty)
    {
        string sql = string.Empty;
        string result = string.Empty;
        sql = "SELECT IP, CSIP from keyueagent where 前缀=@qianzhui";
        string conn = "server=127.0.0.1;uid=sa;pwd=king;database=" + qianzhui + ty;
        result = conn;
        return result;
    }
    /// <summary>
    /// 连接数据库字符串
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string ConnectionString(string name)
    {
        return ConfigurationManager.ConnectionStrings[name].ConnectionString;
    }
    /// <summary>
    /// 连接数据库字符串
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string ConnectionString_(string name)
    {
        string conn= ConfigurationManager.ConnectionStrings[name].ConnectionString;
        conn = string.Format(conn, PubConstant.sqlname);
        return conn;
    }

    /// <summary>
    /// 执行sql命令
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="commandType">命令类型</param>
    /// <param name="commandText">sql语句/参数化sql语句/存储过程名</param>
    /// <param name="commandParameters">参数</param>
    /// <returns>受影响的行数</returns>
    public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand cmd = new SqlCommand();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                PrepareCommand(cmd, commandType, conn, commandText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                return val;

            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
    }

    /// <summary>
    /// 执行Sql Server存储过程
    /// 注意：不能执行有out 参数的存储过程
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="spName">存储过程名</param>
    /// <param name="parameterValues">对象参数</param>
    /// <returns>受影响的行数</returns>
    public static int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                PrepareCommand(cmd, conn, spName, parameterValues);
                int val = cmd.ExecuteNonQuery();
                return val;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
    }

    /// <summary>
    ///  执行sql命令
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="commandType">命令类型</param>
    /// <param name="commandText">sql语句/参数化sql语句/存储过程名</param>
    /// <param name="commandParameters">参数</param>
    /// <returns>SqlDataReader 对象</returns>
    public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlConnection conn = new SqlConnection(connectionString);
       
        SqlCommand cmd = new SqlCommand();
        PrepareCommand(cmd, commandType, conn, commandText, commandParameters);
        SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        return rdr;
        
        
    }

    /// <summary>
    /// 执行Sql Server存储过程
    /// 注意：不能执行有out 参数的存储过程
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="spName">存储过程名</param>
    /// <param name="parameterValues">对象参数</param>
    /// <returns>受影响的行数</returns>
    public static SqlDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
    {
        SqlConnection conn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand();
        PrepareCommand(cmd, conn, spName, parameterValues);
        SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return rdr;
    }

    /// <summary>
    /// 执行Sql Server存储过程
    /// 注意：不能执行有out 参数的存储过程
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="spName">存储过程名</param>
    /// <param name="parameterValues">对象参数</param>
    /// <returns>DataSet对象</returns>
    public static DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, conn, spName, parameterValues);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                try
                {
                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    return ds;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }
    }

    /// <summary>
    /// 执行Sql 命令
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="commandType">命令类型</param>
    /// <param name="commandText">sql语句/参数化sql语句/存储过程名</param>
    /// <param name="commandParameters">参数</param>
    /// <returns>DataSet 对象</returns>
    public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, commandType, conn, commandText, commandParameters);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                try
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }
    }
    public static bool Exists(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand cmd = new SqlCommand();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                PrepareCommand(cmd, commandType, conn, commandText, commandParameters);
                object val = cmd.ExecuteScalar();
                if (val != DBNull.Value && val != null)
                {
                    if (Convert.ToInt32(val) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
    }
    /// <summary>
    /// 执行Sql 命令
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="commandType">命令类型</param>
    /// <param name="commandText">sql语句/参数化sql语句/存储过程名</param>
    /// <param name="commandParameters">参数</param>
    /// <returns>执行结果对象</returns>
    public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        SqlCommand cmd = new SqlCommand();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                PrepareCommand(cmd, commandType, conn, commandText, commandParameters);
                object val = cmd.ExecuteScalar();
                return val;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
    }

    /// <summary>
    /// 执行Sql Server存储过程
    /// 注意：不能执行有out 参数的存储过程
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="spName">存储过程名</param>
    /// <param name="parameterValues">对象参数</param>
    /// <returns>执行结果对象</returns>
    public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
    {
        SqlCommand cmd = new SqlCommand();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                PrepareCommand(cmd, conn, spName, parameterValues);
                object val = cmd.ExecuteScalar();
                return val;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
    }

    /// <summary>
    /// 设置一个等待执行的SqlCommand对象
    /// </summary>
    /// <param name="cmd">SqlCommand 对象，不允许空对象</param>
    /// <param name="conn">SqlConnection 对象，不允许空对象</param>
    /// <param name="commandText">Sql 语句</param>
    /// <param name="cmdParms">SqlParameters  对象,允许为空对象</param>
    private static void PrepareCommand(SqlCommand cmd, CommandType commandType, SqlConnection conn, string commandText, SqlParameter[] cmdParms)
    {
        //打开连接
        if (conn.State != ConnectionState.Open)
            conn.Open();

        //设置SqlCommand对象
        cmd.Connection = conn;
        cmd.CommandText = commandText;
        cmd.CommandType = commandType;

        if (cmdParms != null)
        {
            foreach (SqlParameter parm in cmdParms)
                cmd.Parameters.Add(parm);
        }
    }

    /// <summary>
    /// 设置一个等待执行存储过程的SqlCommand对象
    /// </summary>
    /// <param name="cmd">SqlCommand 对象，不允许空对象</param>
    /// <param name="conn">SqlConnection 对象，不允许空对象</param>
    /// <param name="spName">Sql 语句</param>
    /// <param name="parameterValues">不定个数的存储过程参数，允许为空</param>
    private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, string spName, params object[] parameterValues)
    {
        //打开连接
        if (conn.State != ConnectionState.Open)
            conn.Open();

        //设置SqlCommand对象
        cmd.Connection = conn;
        cmd.CommandText = spName;
        cmd.CommandType = CommandType.StoredProcedure;

        //获取存储过程的参数
        SqlCommandBuilder.DeriveParameters(cmd);

        //移除Return_Value 参数
        cmd.Parameters.RemoveAt(0);

        //设置参数值
        if (parameterValues != null)
        {
            for (int i = 0; i < cmd.Parameters.Count; i++)
            {
                cmd.Parameters[i].Value = parameterValues[i];
            }
        }
    }
    /// <summary>
    /// 返回一个表格
    /// </summary>
    /// <param name="connectionString">连接数据库</param>
    /// <param name="commandType">是不是用存储过程</param>
    /// <param name="commandText">sql语句</param>
    /// <param name="commandParameters">参数</param>
    /// <returns>表格</returns>
    public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, commandType, conn, commandText, commandParameters);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                try
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }
    }
    /// <summary>
    /// Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlTransaction. 
    /// </summary>
    /// <remarks>
    /// e.g.:  
    ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount");
    /// </remarks>
    /// <param name="transaction">A valid SqlTransaction</param>
    /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">The stored procedure name or T-SQL command</param>
    /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
    public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteScalar(transaction, commandType, commandText, (SqlParameter[])null);
    }
    /// <summary>
    /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlTransaction
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:  
    ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
    /// </remarks>
    /// <param name="transaction">A valid SqlTransaction</param>
    /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">The stored procedure name or T-SQL command</param>
    /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
    /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
    public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        if (transaction == null) throw new ArgumentNullException("transaction");
        if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

        // Create a command and prepare it for execution
        SqlCommand cmd = new SqlCommand();
        bool mustCloseConnection = false;
        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);

        // Execute the command & return the results
        object retval = cmd.ExecuteScalar();

        // Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();
        return retval;
    }
    /// <summary>
    /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlTransaction
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:  
    ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
    /// </remarks>
    /// <param name="transaction">A valid SqlTransaction</param>
    /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">The stored procedure name or T-SQL command</param>
    /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
    /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
    public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        if (transaction == null) throw new ArgumentNullException("transaction");
        if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

        // Create a command and prepare it for execution
        SqlCommand cmd = new SqlCommand();
        bool mustCloseConnection = false;
        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
        int retval = 0;
        // Execute the command & return the results
        retval = cmd.ExecuteNonQuery();

        // Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();
        return retval;
    }
    /// <summary>
    /// Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlTransaction
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:  
    ///  int orderCount = (int)ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24));
    /// </remarks>
    /// <param name="transaction">A valid SqlTransaction</param>
    /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">The stored procedure name or T-SQL command</param>
    /// <param name="commandParameters">An array of SqlParamters used to execute the command</param>
    /// <returns>An object containing the value in the 1x1 resultset generated by the command</returns>
    public static DataTable ExecuteDataTable(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        if (transaction == null) throw new ArgumentNullException("transaction");
        if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

        // Create a command and prepare it for execution
        SqlCommand cmd = new SqlCommand();
        bool mustCloseConnection = false;
        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out mustCloseConnection);
        DataTable dtReturn = null;
        object[] value = null;
        // Execute the command & return the results
        SqlDataReader dataReader = cmd.ExecuteReader();
        if (dataReader.HasRows)
        {
            dtReturn = CreateTableBySchemaTable(dataReader.GetSchemaTable());

            value = new object[dataReader.FieldCount];

            while (dataReader.Read())
            {
                dataReader.GetValues(value);
                dtReturn.LoadDataRow(value, true);
            }

            value = null;
        }

        // Detach the SqlParameters from the command object, so they can be used again
        dataReader.Close();
        cmd.Parameters.Clear();
        return dtReturn;
    }
    /// <summary>
    /// This method opens (if necessary) and assigns a connection, transaction, command type and parameters 
    /// to the provided command
    /// </summary>
    /// <param name="command">The SqlCommand to be prepared</param>
    /// <param name="connection">A valid SqlConnection, on which to execute this command</param>
    /// <param name="transaction">A valid SqlTransaction, or 'null'</param>
    /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">The stored procedure name or T-SQL command</param>
    /// <param name="commandParameters">An array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
    /// <param name="mustCloseConnection"><c>true</c> if the connection was opened by the method, otherwose is false.</param>
    private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
    {
        if (command == null) throw new ArgumentNullException("command");
        if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

        // If the provided connection is not open, we will open it
        if (connection.State != ConnectionState.Open)
        {
            mustCloseConnection = true;
            connection.Open();
        }
        else
        {
            mustCloseConnection = false;
        }

        // Associate the connection with the command
        command.Connection = connection;
        command.CommandTimeout = 180;
        // Set the command text (stored procedure name or SQL statement)
        command.CommandText = commandText;

        // If we were provided a transaction, assign it
        if (transaction != null)
        {
            if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
            command.Transaction = transaction;
        }

        // Set the command type
        command.CommandType = commandType;

        // Attach the command parameters if they are provided
        if (commandParameters != null)
        {
            AttachParameters(command, commandParameters);
        }
        return;
    }
    /// <summary>
    /// This method is used to attach array of SqlParameters to a SqlCommand.
    /// 
    /// This method will assign a value of DbNull to any parameter with a direction of
    /// InputOutput and a value of null.  
    /// 
    /// This behavior will prevent default values from being used, but
    /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
    /// where the user provided no input value.
    /// </summary>
    /// <param name="command">The command to which the parameters will be added</param>
    /// <param name="commandParameters">An array of SqlParameters to be added to command</param>
    private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
    {
        if (command == null) throw new ArgumentNullException("command");
        if (commandParameters != null)
        {
            foreach (SqlParameter p in commandParameters)
            {
                if (p != null)
                {
                    // Check for derived output value with no value assigned
                    if ((p.Direction == ParameterDirection.InputOutput ||
                      p.Direction == ParameterDirection.Input) &&
                      (p.Value == null))
                    {
                        p.Value = DBNull.Value;
                    }
                    command.Parameters.Add(p);
                }
            }
        }
    }
    //转DateTable
    protected static DataTable CreateTableBySchemaTable(DataTable pSchemaTable)
    {
        DataTable dtReturn = new DataTable();
        DataColumn dc = null;
        DataRow dr = null;

        for (int i = 0; i < pSchemaTable.Rows.Count; i++)
        {
            dr = pSchemaTable.Rows[i];
            dc = new DataColumn(dr["ColumnName"].ToString(), dr["DataType"] as Type);
            dtReturn.Columns.Add(dc);
        }

        dr = null;
        dc = null;

        return dtReturn;
    }

    /// <summary>
    /// 获取分页SQL
    /// </summary>
    /// <param name="tblName">表名</param>
    /// <param name="pageSize">每页显示条数</param>
    /// <param name="pageIndex">第几页</param>
    /// <param name="fldSort">排序字段（最后一个不需要填写正序还是倒序，例如：id asc, name）</param>
    /// <param name="fldDir">最后一个排序字段的正序或倒序（true为倒序，false为正序）</param>
    /// <param name="condition">条件</param>
    /// <returns>返回用于分页的SQL语句</returns>
    private static string GetPagerSQL(string tblName, int pageSize, int pageIndex, string fldSort, bool fldDir, string condition)
    {
        string strDir = fldDir ? " ASC" : " DESC";

        string idorder = fldSort.Trim().ToLower() != "id" ? ",id" : "";
        if (pageIndex == 1)
        {
            return "select top " + pageSize.ToString() + " * from " + tblName.ToString()
                + ((string.IsNullOrEmpty(condition)) ? string.Empty : (" where (1=1) " + condition))
                + " order by " + fldSort.ToString() + strDir + idorder;
        }
        else
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select top {0} * from {1} ", pageSize, tblName);
            strSql.AppendFormat(" where id not in (select top {0} id from {2} ", pageSize * (pageIndex - 1),
                (fldSort.Substring(fldSort.LastIndexOf(',') + 1, fldSort.Length - fldSort.LastIndexOf(',') - 1)), tblName);
            if (!string.IsNullOrEmpty(condition))
            {
                strSql.AppendFormat(" where (1=1) {0} order by {1}{2}{3}) {0}", condition, fldSort, strDir, idorder);
            }
            else
            {
                strSql.AppendFormat(" order by {0}{1}{2}) ", fldSort, strDir, idorder);
            }
            strSql.AppendFormat(" order by {0}{1}{2}", fldSort, strDir, idorder);
            return strSql.ToString();
        }
    }

    /// <summary>
    /// 分页获取数据
    /// </summary>
    /// <param name="connectionStringname">连接字符串的name值</param>
    /// <param name="tblName">表名</param>
    /// <param name="pageSize">页大小</param>
    /// <param name="pageIndex">第几页</param>
    /// <param name="fldSort">排序字段</param>
    /// <param name="fldDir">升序{False}/降序(True)</param>
    /// <param name="condition">条件(不需要where)</param>
    /// <returns>DataSet</returns>
    public static DataTable GetPageListInDataSet(string connectionStringname, string tblName, int pageSize, int pageIndex, string fldSort, bool fldDir, string condition)
    {
        string sql = GetPagerSQL(tblName, pageSize, pageIndex, fldSort, fldDir, condition);
        return ExecuteDataTable(connectionStringname, CommandType.Text, sql, null);
    }

    public static int GetCount(string connectionStringname, string tblName, string condition)
    {
        StringBuilder sql = new StringBuilder("select count(*) from " + tblName);
        if (!string.IsNullOrEmpty(condition))
            sql.Append(" where (1=1) " + condition);

        object count = ExecuteScalar(connectionStringname, CommandType.Text, sql.ToString(), null);
        return int.Parse(count.ToString());
    }
}
