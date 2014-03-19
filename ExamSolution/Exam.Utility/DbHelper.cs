using System.Configuration;
using System.Data.SQLite;
using System;
using System.Data.Common;
using System.Data;
using System.Collections.Generic;
using System.Collections;

namespace Exam.Utility
{
    public class DbHelper
    {
        private static string dbConnectionString = ConfigurationManager.AppSettings["DbHelperConnectionString"];

        private DbConnection connection;

        public DbHelper()
        {
            //Console.WriteLine("DbHelper(" + DbHelper.dbConnectionString + ") at " + DateTime.Now);
            this.connection = CreateConnection(DbHelper.dbConnectionString);
        }

        public DbHelper(string connectionString)
        {
            //Console.WriteLine("DbHelper(connectionString=" + connectionString + ") at " + DateTime.Now);
            this.connection = CreateConnection(connectionString);
        }

        public static DbConnection CreateConnection()
        {
            DbProviderFactory dbfactory = CreateFactory();
            DbConnection dbconn = dbfactory.CreateConnection();
            dbconn.ConnectionString = DbHelper.dbConnectionString;
            return dbconn;
        }

        public static DbConnection CreateConnection(string connectionString)
        {
            DbProviderFactory dbfactory = CreateFactory();
            DbConnection dbconn = dbfactory.CreateConnection();
            dbconn.ConnectionString = connectionString;
            return dbconn;
        }

        private static DbProviderFactory CreateFactory()
        {
            return SQLiteFactory.Instance;
        }

        /// <summary>
        /// 执行的存储过程
        /// </summary>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        public DbCommand GetStoredProcCommond(string storedProcedure)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = storedProcedure;
            dbCommand.CommandType = CommandType.StoredProcedure;
            return dbCommand;
        }

        /// <summary>
        /// 执行的sql语句
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public DbCommand GetSqlStringCommond(string sqlQuery)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbCommand.CommandType = CommandType.Text;
            return dbCommand;
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public void ExecuteSqlTran(ArrayList SQLStringList)
        {

            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.Connection.Open();
            DbTransaction tx = connection.BeginTransaction();
            dbCommand.Transaction = tx;
            try
            {

                for (int n = 0; n < SQLStringList.Count; n++)
                {
                    string strsql = SQLStringList[n].ToString();
                    if (strsql.Trim().Length > 1)
                    {
                        dbCommand.CommandText = strsql;
                        dbCommand.ExecuteNonQuery();
                    }
                }
                tx.Commit();
                connection.Close();
            }
            catch (System.Data.SqlClient.SqlException E)
            {
                tx.Rollback();
                throw new Exception(E.Message);
            }
            finally
            {
                dbCommand.Connection.Close();
            }
        }

        //增加参数#region 增加参数
        public void AddParameterCollection(DbCommand cmd, DbParameterCollection dbParameterCollection)
        {
            foreach (DbParameter dbParameter in dbParameterCollection)
            {
                cmd.Parameters.Add(dbParameter);
            }
        }

        /// <summary>
        /// 增加输出参数 适用于存储过程
        /// </summary>
        /// <param name="cmd">cmd对象</param>
        /// <param name="parameterName">参数名</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="size">参数大小</param>
        public void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType, int size)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Size = size;
            dbParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(dbParameter);
        }

        /// <summary>
        /// 增加参数列表
        /// </summary>
        /// <param name="cmd">CMD对象</param>
        /// <param name="parameterName">参数名</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="value">参数值</param>
        public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, object value)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;
            dbParameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParameter);
        }

        /// <summary>
        /// 增加返回参数 适用于存储过程
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        public void AddReturnParameter(DbCommand cmd, string parameterName, DbType dbType)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(dbParameter);
        }

        public DbParameter GetParameter(DbCommand cmd, string parameterName)
        {
            return cmd.Parameters[parameterName];
        }

        /// <summary>
        /// 执行SQL或者存储过程返回一个dataSet对象
        /// 所需参数cmd对象
        /// </summary>
        /// <param name="cmd">cmd对象</param>
        /// <returns>查询的结果 dataSet对象</returns>
        public DataSet ExecuteDataSet(DbCommand cmd)
        {
            DataSet ds = new DataSet();
            try
            {
                DbProviderFactory dbfactory = CreateFactory();
                DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
                dbDataAdapter.SelectCommand = cmd;
                dbDataAdapter.Fill(ds);
            }
            catch
            { }
            return ds;
        }

        /// <summary>
        /// 执行SQL或者存储过程返回一个dataTable对象
        /// 所需参数cmd对象
        /// </summary>
        /// <param name="cmd">cmd对象</param>
        /// <returns>查询的结果 DataReader对象</returns>
        public DataTable ExecuteDataTable(DbCommand cmd)
        {
            DbProviderFactory dbfactory = CreateFactory();
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable("tb");
            dbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public object[] ExecuteDataTable_Adapter(DbCommand cmd)
        {
            object[] fanhui = new object[2];
            DbProviderFactory dbfactory = CreateFactory();
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable("tb");
            dbDataAdapter.Fill(dataTable);
            fanhui[0] = dataTable;
            fanhui[1] = dbDataAdapter;
            return fanhui;
        }

        /// <summary>
        /// 执行sql语句或者存储过程返回一个datareader对象
        /// 所需参数CMD对象
        /// </summary>
        /// <param name="cmd">cmd对象</param>
        /// <returns>查询的结果 DataReader对象</returns>
        public DbDataReader ExecuteReader(DbCommand cmd)
        {
            DbDataReader reader = null;
            try
            {
                cmd.Connection.Open();
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reader;
        }

        /// <summary>
        /// 执行一条SQL语句或者存储过程，返回受影响的行数
        /// </summary>
        /// <param name="cmd">cmd对象</param>
        /// <returns></returns>
        public int ExecuteNonQuery(DbCommand cmd)
        {
            int ret = -1;
            cmd.Connection.Open();
            ret = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return ret;
        }

        /// <summary>
        /// 执行一条SQL语句或者存储过程，返回Object对象
        /// </summary>
        /// <param name="cmd">cmd对象</param>
        /// <returns></returns>
        public object ExecuteScalar(DbCommand cmd)
        {
            cmd.Connection.Open();
            object ret = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return ret;
        }

        //执行事务#region 执行事务
        public DataSet ExecuteDataSet(DbCommand cmd, Trans t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbProviderFactory dbfactory = CreateFactory();
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            dbDataAdapter.Fill(ds);
            return ds;
        }

        public DataTable ExecuteDataTable(DbCommand cmd, Trans t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbProviderFactory dbfactory = CreateFactory();
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            dbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DbDataReader ExecuteReader(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            return reader;
        }

        public int ExecuteNonQuery(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            int ret = cmd.ExecuteNonQuery();
            return ret;
        }

        public int ExecuteNonQuery(DbCommand cmd, List<DbParameter> paramlist)
        {
            cmd.Connection.Open();

            foreach (DbParameter dbp in paramlist)
            {
                cmd.Parameters.Add(dbp);
            }

            int ret = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return ret;
        }

        public object ExecuteScalar(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            object ret = cmd.ExecuteScalar();
            return ret;
        }
    }

    /// <summary>
    /// 事务执行操作
    /// </summary>
    public class Trans : IDisposable
    {
        private DbConnection conn;
        private DbTransaction dbTrans;
        public DbConnection DbConnection
        {
            get { return this.conn; }
        }
        public DbTransaction DbTrans
        {
            get { return this.dbTrans; }
        }

        public Trans()
        {
            conn = DbHelper.CreateConnection();
            conn.Open();
            dbTrans = conn.BeginTransaction();
        }
        public Trans(string connectionString)
        {
            conn = DbHelper.CreateConnection(connectionString);
            conn.Open();
            dbTrans = conn.BeginTransaction();
        }
        public void Commit()
        {
            dbTrans.Commit();
            this.Colse();
        }

        public void RollBack()
        {
            dbTrans.Rollback();
            this.Colse();
        }

        public void Dispose()
        {
            this.Colse();
        }

        public void Colse()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
