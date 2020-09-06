using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Command
{
    public static class DbHelperSQLParam
    {
        /// <summary>
        /// 数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.
        /// </summary>
        private static string connectionString = ConfigurationManager.ConnectionStrings["param"].ConnectionString;
        

        /// <summary>
        /// 获取请求内容(方法1) 适合 .Net
        /// </summary>
        /// <param name="methodName">方法名称</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public static byte[] getRequestData(string methodName, Dictionary<string, string> param)
        {
            StringBuilder requestData = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>")
                .Append("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">")
                .Append("  <soap:Body>")
                .Append("<").Append(methodName)
                .Append(" xmlns=\"http://tempuri.org/\">");
            foreach (KeyValuePair<string, string> item in param)
            {
                requestData.Append("<").Append(item.Key).Append(">")
                .Append(item.Value)
                .Append("</").Append(item.Key).Append(">");
            }
            requestData.Append("</").Append(methodName).Append(">")
            .Append("</soap:Body>")
            .Append("</soap:Envelope>");
            string val = requestData.ToString();
            byte[] data = Encoding.UTF8.GetBytes(val);
            return data;
        }

        #region 公用方法

        /// <summary>
        /// 判断是否存在某表的某个字段
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="columnName">列名称</param>
        /// <returns>是否存在</returns>
        public static bool ColumnExists(string tableName, string columnName)
        {
            string sql = "select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'";
            object res = GetSingle(sql);
            if (res == null)
            {
                return false;
            }

            return Convert.ToInt32(res) > 0;
        }

        /// <summary>
        /// 得到最大值 + 1
        /// </summary>
        /// <param name="fieldName">字段</param>
        /// <param name="tableName">表明</param>
        /// <returns>最大值 + 1</returns>
        public static int GetMaxID(string fieldName, string tableName)
        {
            string strsql = "select max(" + fieldName + ")+1 from " + tableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>是否</returns>
        public static bool Exists(string sql)
        {
            object obj = GetSingle(sql);
            int cmdresult;
            if (object.Equals(obj, null) || object.Equals(obj, System.DBNull.Value))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }

            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 表是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>是否存在</returns>
        public static bool TabExists(string tableName)
        {
            string strsql = "select count(*) from sysobjects where id = object_id(N'[" + tableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";

            // string strsql = "SELECT count(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + TableName + "]') AND type in (N'U')";
            object obj = GetSingle(strsql);
            int cmdresult;
            if (object.Equals(obj, null) || object.Equals(obj, System.DBNull.Value))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }

            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>是否</returns>
        public static bool Exists(string sql, params SqlParameter[] cmdParms)
        {
            object obj = GetSingle(sql, cmdParms);
            int cmdresult;
            if (object.Equals(obj, null) || object.Equals(obj, System.DBNull.Value))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }

            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion 公用方法

        #region 执行简单SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string sql)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="times">超时时间</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteSqlByTime(string sql, int times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = times;
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        /*   /// <summary>
           /// 执行Sql和Oracle滴混合事务
           /// </summary>
           /// <param name="list">SQL命令行列表</param>
           /// <param name="oracleCmdSqlList">Oracle命令行列表</param>
           /// <returns>执行结果 0-由于SQL造成事务失败 -1 由于Oracle造成事务失败 1-整体事务执行成功</returns>
           public static int ExecuteSqlTran(List<CommandInfo> list, List<CommandInfo> oracleCmdSqlList)
           {
               using (SqlConnection conn = new SqlConnection(this.connectionString))
               {
                   conn.Open();
                   SqlCommand cmd = new SqlCommand();
                   cmd.Connection = conn;
                   SqlTransaction tx = conn.BeginTransaction();
                   cmd.Transaction = tx;
                   try
                   {
                       foreach (CommandInfo myDE in list)
                       {
                           string cmdText = myDE.CommandText;
                           SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;
                           PrepareCommand(cmd, conn, tx, cmdText, cmdParms);
                           if (myDE.EffectNextType == EffectNextType.SolicitationEvent)
                           {
                               if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                               {
                                   tx.Rollback();

                                   // return 0;
                                   throw new Exception("违背要求" + myDE.CommandText + "必须符合select count(..的格式");
                               }

                               object obj = cmd.ExecuteScalar();
                               bool isHave = false;
                               if (obj == null && obj == DBNull.Value)
                               {
                                   isHave = false;
                               }

                               isHave = Convert.ToInt32(obj) > 0;
                               if (isHave)
                               {
                                   // 引发事件
                                   myDE.OnSolicitationEvent();
                               }
                           }

                           if (myDE.EffectNextType == EffectNextType.WhenHaveContine || myDE.EffectNextType == EffectNextType.WhenNoHaveContine)
                           {
                               if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                               {
                                   tx.Rollback();

                                   // return 0;
                                   throw new Exception("SQL:违背要求" + myDE.CommandText + "必须符合select count(..的格式");
                               }

                               object obj = cmd.ExecuteScalar();
                               bool isHave = false;
                               if (obj == null && obj == DBNull.Value)
                               {
                                   isHave = false;
                               }

                               isHave = Convert.ToInt32(obj) > 0;

                               if (myDE.EffectNextType == EffectNextType.WhenHaveContine && !isHave)
                               {
                                   tx.Rollback();

                                   // return 0;
                                   throw new Exception("SQL:违背要求" + myDE.CommandText + "返回值必须大于0");
                               }

                               if (myDE.EffectNextType == EffectNextType.WhenNoHaveContine && isHave)
                               {
                                   tx.Rollback();

                                   // return 0;
                                   throw new Exception("SQL:违背要求" + myDE.CommandText + "返回值必须等于0");
                               }

                               continue;
                           }

                           int val = cmd.ExecuteNonQuery();
                           if (myDE.EffectNextType == EffectNextType.ExcuteEffectRows && val == 0)
                           {
                               tx.Rollback();

                               // return 0;
                               throw new Exception("SQL:违背要求" + myDE.CommandText + "必须有影响行");
                           }

                           cmd.Parameters.Clear();
                       }

                       string oraConnectionString = PubConstant.GetConnectionString("ConnectionStringPPC");
                       bool res = OracleHelper.ExecuteSqlTran(oraConnectionString, oracleCmdSqlList);
                       if (!res)
                       {
                           tx.Rollback();

                           // return -1;
                           throw new Exception("Oracle执行失败");
                       }

                       tx.Commit();
                       return 1;
                   }
                   catch (System.Data.SqlClient.SqlException e)
                   {
                       tx.Rollback();
                       throw e;
                   }
                   catch (Exception e)
                   {
                       tx.Rollback();
                       throw e;
                   }
               }
           }*/

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sqlList">多条SQL语句</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteSqlTran(List<string> sqlList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < sqlList.Count; n++)
                    {
                        string strsql = sqlList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }

                    tx.Commit();
                    return count;
                }
                catch
                {
                    tx.Rollback();
                    return 0;
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string sql, string content)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行带一个存储过程参数的的SQL语句。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="content">参数内容,比如一个字段是格式复杂的文章，有特殊符号，可以通过这个方式添加</param>
        /// <returns>影响的记录数</returns>
        public static object ExecuteSqlGet(string sql, string content)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if (object.Equals(obj, null) || object.Equals(obj, System.DBNull.Value))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 向数据库里插入图像格式的字段(和上面情况类似的另一种实例)
        /// </summary>
        /// <param name="strSQL">SQL语句</param>
        /// <param name="fs">图像字节,数据库的字段类型为image的情况</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(strSQL, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sql">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string sql)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if (object.Equals(obj, null) || object.Equals(obj, System.DBNull.Value))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sql">计算查询结果语句</param>
        /// <param name="times">超时时间</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string sql, int times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = times;
                        object obj = cmd.ExecuteScalar();
                        if (object.Equals(obj, null) || object.Equals(obj, System.DBNull.Value))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string strSQL)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 执行查询语句，返回XmlReader
        /// </summary>
        /// <param name="strSQL">查询语句</param>
        /// <returns>XmlReader</returns>
        public static XmlReader ExecuteXmlReader(string strSQL)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(strSQL, connection))
                {
                    try
                    {
                        connection.Open();
                        XmlReader myReader = cmd.ExecuteXmlReader();
                        return myReader;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string sql)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sql, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }

                return ds;
            }
        }

        /// <summary>
        /// 查集合
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="times">超时时间</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string sql, int times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sql, connection);
                    command.SelectCommand.CommandTimeout = times;
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }

                return ds;
            }
        }

        #endregion 执行简单SQL语句

        #region 执行带参数的SQL语句

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string sql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sql, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sqlList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTran(Hashtable sqlList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        // 循环
                        foreach (DictionaryEntry myDE in sqlList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
        /*
                /// <summary>
                /// 执行多条SQL语句，实现数据库事务。
                /// </summary>
                /// <param name="cmdList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
                /// <returns>受影响行数</returns>
                public static int ExecuteSqlTran(List<CommandInfo> cmdList)
                {
                    using (SqlConnection conn = new SqlConnection(this.connectionString))
                    {
                        conn.Open();
                        using (SqlTransaction trans = conn.BeginTransaction())
                        {
                            SqlCommand cmd = new SqlCommand();
                            try
                            {
                                int count = 0;

                                // 循环
                                foreach (CommandInfo myDE in cmdList)
                                {
                                    string cmdText = myDE.CommandText;
                                    SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;
                                    PrepareCommand(cmd, conn, trans, cmdText, cmdParms);

                                    if (myDE.EffectNextType == EffectNextType.WhenHaveContine || myDE.EffectNextType == EffectNextType.WhenNoHaveContine)
                                    {
                                        if (myDE.CommandText.ToLower().IndexOf("count(") == -1)
                                        {
                                            trans.Rollback();
                                            return 0;
                                        }

                                        object obj = cmd.ExecuteScalar();
                                        bool isHave = false;
                                        if (obj == null && obj == DBNull.Value)
                                        {
                                            isHave = false;
                                        }

                                        isHave = Convert.ToInt32(obj) > 0;

                                        if (myDE.EffectNextType == EffectNextType.WhenHaveContine && !isHave)
                                        {
                                            trans.Rollback();
                                            return 0;
                                        }

                                        if (myDE.EffectNextType == EffectNextType.WhenNoHaveContine && isHave)
                                        {
                                            trans.Rollback();
                                            return 0;
                                        }

                                        continue;
                                    }

                                    int val = cmd.ExecuteNonQuery();
                                    count += val;
                                    if (myDE.EffectNextType == EffectNextType.ExcuteEffectRows && val == 0)
                                    {
                                        trans.Rollback();
                                        return 0;
                                    }

                                    cmd.Parameters.Clear();
                                }

                                trans.Commit();
                                return count;
                            }
                            catch
                            {
                                trans.Rollback();
                                throw;
                            }
                        }
                    }
                }*/
        /*
                /// <summary>
                /// 执行多条SQL语句，实现数据库事务。
                /// </summary>
                /// <param name="sqlList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
                public static void ExecuteSqlTranWithIndentity(System.Collections.Generic.List<CommandInfo> sqlList)
                {
                    using (SqlConnection conn = new SqlConnection(this.connectionString))
                    {
                        conn.Open();
                        using (SqlTransaction trans = conn.BeginTransaction())
                        {
                            SqlCommand cmd = new SqlCommand();
                            try
                            {
                                int indentity = 0;

                                // 循环
                                foreach (CommandInfo myDE in sqlList)
                                {
                                    string cmdText = myDE.CommandText;
                                    SqlParameter[] cmdParms = (SqlParameter[])myDE.Parameters;
                                    foreach (SqlParameter q in cmdParms)
                                    {
                                        if (q.Direction == ParameterDirection.InputOutput)
                                        {
                                            q.Value = indentity;
                                        }
                                    }

                                    PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                                    int val = cmd.ExecuteNonQuery();
                                    foreach (SqlParameter q in cmdParms)
                                    {
                                        if (q.Direction == ParameterDirection.Output)
                                        {
                                            indentity = Convert.ToInt32(q.Value);
                                        }
                                    }

                                    cmd.Parameters.Clear();
                                }

                                trans.Commit();
                            }
                            catch
                            {
                                trans.Rollback();
                                throw;
                            }
                        }
                    }
                }*/

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sqlList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public static void ExecuteSqlTranWithIndentity(Hashtable sqlList)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int indentity = 0;

                        // 循环
                        foreach (DictionaryEntry myDE in sqlList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            foreach (SqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.InputOutput)
                                {
                                    q.Value = indentity;
                                }
                            }

                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            foreach (SqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.Output)
                                {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }

                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="sql">计算查询结果语句</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle(string sql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sql, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if (object.Equals(obj, null) || object.Equals(obj, System.DBNull.Value))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, sql, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
            ////finally
            ////{
            ////  cmd.Dispose();
            ////  connection.Close();
            ////}
        }

        /// <summary>
        /// 执行查询语句，返回XmlReader
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>XmlReader</returns>
        public static XmlReader ExecuteXmlReader(string sql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sql, cmdParms);
                        XmlReader myReader = cmd.ExecuteXmlReader();
                        cmd.Parameters.Clear();
                        return myReader;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="cmdParms">参数列表</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string sql, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, sql, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    return ds;
                }
            }
        }

        #endregion 执行带参数的SQL语句

        #region 存储过程操作

        /// <summary>
        /// 执行存储过程，返回SqlDataReader  ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataReader returnReader;
                connection.Open();
                SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
                command.CommandType = CommandType.StoredProcedure;
                returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return returnReader;
            }
        }

        /// <summary>
        /// 执行存储过程，返回XmlReader
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>XmlReader</returns>
        public static XmlReader RunProcedureXml(string storedProcName, IDataParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                XmlReader returnReader;
                connection.Open();
                using (SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    returnReader = command.ExecuteXmlReader();
                    return returnReader;
                }
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        /// <summary>
        /// 运行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">参数数组</param>
        /// <param name="tableName">表名</param>
        /// <param name="times">超时时间</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int times)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.SelectCommand.CommandTimeout = times;
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        /// <summary>
        /// 执行存储过程，返回影响的行数
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="rowsAffected">影响的行数</param>
        /// <returns>返回值</returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                int result;
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;

                // Connection.Close();
                return result;
            }
        }

        #endregion 存储过程操作

        #region jinmax 扩展执行存储过程方法 返回多个输出参数

        /// <summary>
        /// 执行存储过程,对象target的第一个参数 Name表示存储过程名字
        /// 从第二个属性开始就是参数了
        /// 建议，先是输入参数，然后输出参数
        /// 输入参数按照存储过程参数命名
        /// 输出参数以o_作为开头
        /// outs这个输出参数作为存储过程输出参数值
        /// outs的string代表输出参数名,object代表输出参数值
        /// 调用举例: var target=new {Name="proc_name",inparar1=123,inpara2="aa",o_para3=0,o_para4="aa"};
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="target">存储过程对象</param>
        /// <param name="outs">输出的参数</param>
        /// <returns>返回存储过程返回值</returns>
        public static object RunProcedure(string connectionString, object target, out Dictionary<string, object> outs)
        {
            // 思路：使用反射的方式来获取所有属性，属性名就是参数名，属性值为参数值，属性类型为参数类型
            List<SqlParameter> paras = new List<SqlParameter>();
            SqlParameter para = null;

            // 获得类型
            Type t = target.GetType();

            // 获得name属性
            PropertyInfo proc = t.GetProperty("Name");

            // 得到属性值
            string procName = proc.GetValue(target, null).ToString();

            // 得到所有公有实例属性
            var props = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var p in props)
            {
                // p.Name //属性名
                if (p.Name.ToLower() != "name")
                {
                    // 判断是输入参数还是输出参数
                    if (p.Name.Substring(0, 2).ToLower() != "o_")
                    {
                        para = new SqlParameter("@" + p.Name.Substring(2), p.GetValue(target, null));
                        para.Direction = ParameterDirection.Output;
                    }
                    else
                    {
                        para = new SqlParameter("@" + p.Name, p.GetValue(target, null));
                    }

                    paras.Add(para);
                }
            }

            int rowsaffected;
            object ret = RunProcedure(procName, paras.ToArray(), out rowsaffected);

            // 接下来处理输出参数，输出参数的值在paras
            outs = new Dictionary<string, object>();
            foreach (var p in paras)
            {
                if (p.Direction == ParameterDirection.Output)
                {
                    outs.Add(p.ParameterName.Substring(1), p.Value);
                }
            }

            return ret;
        }

        #endregion jinmax 扩展执行存储过程方法 返回多个输出参数

        /// <summary>
        /// 准备SqlComman
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <param name="conn">连接对象</param>
        /// <param name="trans">SqlTransaction</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="cmdParms">参数</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }

            cmd.CommandType = CommandType.Text;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }

                    cmd.Parameters.Add(parameter);
                }
            }
        }

        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }

                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        /// <summary>
        /// 创建 SqlCommand 对象实例(用来返回一个整数值)
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand 对象实例</returns>
        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter(
                "ReturnValue",
                SqlDbType.Int,
                4,
                ParameterDirection.ReturnValue,
                false,
                0,
                0,
                string.Empty,
                DataRowVersion.Default,
                null));
            return command;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetTableSql(string sql)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, connection);
                sda.Fill(dt);
            }
            return dt;

        }

        public static  int GetNonQuery(string sql)
        {
            int result;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                result = cmd.ExecuteNonQuery();
            }
            return result;
        }
    }
}
