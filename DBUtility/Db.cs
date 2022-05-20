using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace WF_Calc.DBUtility {
    public class Db
    {
        private System.Data.IDataReader Idr;

        /// <summary>
        /// 表示到默认系统数据库的一个打开的连接。
        /// </summary>
        public Db()
        {
            //if (System.IO.File.Exists(ConnectionConfigFile))
            //{
            //    System.IO.StreamReader sr = new System.IO.StreamReader(ConnectionConfigFile);
            //    this.m_ConnectionString = sr.ReadLine();
            //    this.m_ConnectionType = (ConnectType)Convert.ToInt32(sr.ReadLine());
            //    sr.Dispose();
            //    sr.Close();
            //}
            //else
            //{
            //    this.m_ConnectionString = "Server=jhl;uid=sa;pwd=;database=hubei;Integrated Security=SSPI";
            //    this.m_ConnectionType = ConnectType.Sql;

            //    throw new System.Exception("数据库连接配置文件 " + ConnectionConfigFile + " 不存在");
            //}
            //this.m_ConnectionString = "Server=10.0.0.4;uid=sa;pwd=;database=hubei";
            this.m_ConnectionString = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["dbType"]].ToString(); //"Server=10.0.0.218;uid=sa;pwd=;database=hubei";
            //this.m_ConnectionString = "Server=jhl;uid=sa;pwd=sa;database=hubei";
            /* Sql,
            OleDb,
            Odbc*/
            switch (ConfigurationManager.AppSettings["dbType"]) {
                case "Sql":
                    this.m_ConnectionType = ConnectType.Sql;
                    break;
                case "OleDb":
                    this.m_ConnectionType = ConnectType.OleDb;
                    break;
                case "Odbc":
                    this.m_ConnectionType = ConnectType.Odbc;
                    break;
                default:
                    this.m_ConnectionType = ConnectType.Sql;
                    break;
            }            
            OpenDb();
        }

        public Db(bool OpenDbFlag)
        {
            //this.m_ConnectionString = "Server=10.0.0.4;uid=sa;pwd=;database=hubei";
            this.m_ConnectionString = ConfigurationManager.ConnectionStrings[0].ToString();// "Server=10.0.0.218;uid=sa;pwd=;database=hubei";
            //this.m_ConnectionString = "Server=jhl;uid=sa;pwd=sa;database=hubei";
            switch (ConfigurationManager.AppSettings["dbType"]) {
                case "Sql":
                    this.m_ConnectionType = ConnectType.Sql;
                    break;
                case "OleDb":
                    this.m_ConnectionType = ConnectType.OleDb;
                    break;
                case "Odbc":
                    this.m_ConnectionType = ConnectType.Odbc;
                    break;
                default:
                    this.m_ConnectionType = ConnectType.Sql;
                    break;
            }
            if (OpenDbFlag)
                OpenDb();
        }

        public static string ConnectionConfigFile
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[0].ToString();
                //return System.Environment.SystemDirectory + "\\JobAnalysis.DB";
                //return this.m_ConnectionString;
            }
        }

        /// <summary>
        /// 表示到指定参数的数据库的一个打开的连接。
        /// </summary>
        /// <param name="connectionString">
        /// 用于打开系统数据库的字符串
        /// （SQL："Server=服务器地址;uid=帐号;pwd=密码;database=数据库名"、"Server=服务器地址;Integrated Security=SSPI;database=数据库名"）
        /// （OLEDB Access："Provider=Microsoft.Jet.OLEDB.4.0;Data Source=数据库路径;Jet OLEDB:database password=密码"）
        /// （OLEDB Oracle："Provider=MSDAORA; Data Source=ORACLE8i7;Persist Security Info=False;Integrated Security=yes"）
        /// （ODBC Sql Server："Driver={SQL Server};Server=MyServer;Trusted_Connection=yes;Database=数据库名;"）
        /// （ORACLE："Data Source=Oracle8i;Integrated Security=yes"）
        /// </param>
        /// <param name="connectionType">所打开的系统数据库的数据库类型</param>
        public Db(string connectionString, ConnectType connectionType)
        {
            this.m_ConnectionString = connectionString;
            this.m_ConnectionType = connectionType;

            OpenDb();
        }

        //打开数据库
        private void OpenDb()
        {
            this.m_DbConnection = GetIDbConnection();
            this.m_DbConnection.ConnectionString = this.m_ConnectionString;

            try
            {
                this.DbConnection.Open();
            }
            catch (Exception ex)
            {
                ErrorLog("", ex.Message);

                throw new System.Exception(ex.Message);
            }
        }

        private System.Data.IDbConnection m_DbConnection;
        /// <summary>
        /// 表示到数据源的连接对象
        /// </summary>
        public System.Data.IDbConnection DbConnection
        {
            get { return m_DbConnection; }
        }

        //根据连接类型返回连接对象
        private System.Data.IDbConnection GetIDbConnection()
        {
            switch (this.ConnectionType)
            {
                case ConnectType.OleDb:
                    return new System.Data.OleDb.OleDbConnection();
                case ConnectType.Odbc:
                    return new System.Data.Odbc.OdbcConnection();
                default:
                    return new System.Data.SqlClient.SqlConnection();
            }
        }

        private string m_ConnectionString;
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return m_ConnectionString; }
        }

        private ConnectType m_ConnectionType;
        /// <summary>
        /// 连接类型
        /// </summary>
        public ConnectType ConnectionType
        {
            get { return m_ConnectionType; }
        }

        /// <summary>
        /// 连接类型
        /// </summary>
        public enum ConnectType
        {
            Sql,
            OleDb,
            Odbc
            //Oracle
        }

        /// <summary>
        /// 执行 SQL 查询，并返回 DataReader 对象。
        /// </summary>
        /// <param name="sql">需要执行的 SQL 语句</param>
        /// <returns></returns>
        public System.Data.IDataReader DataReader(string sql)
        {
            return DataReader(sql, 30);
        }

        /// <summary>
        /// 执行 SQL 查询，并返回 DataReader 对象。
        /// </summary>
        /// <param name="sql">需要执行的 SQL 语句</param>
        /// <param name="CommandTimeout">在终止执行命令的尝试并生成错误之前的等待时间(秒)。</param>
        /// <returns></returns>
        public System.Data.IDataReader DataReader(string sql, int commandTimeout)
        {
            try
            {
                if (this.m_DbConnection.State != System.Data.ConnectionState.Open)
                {
                    this.m_DbConnection.Open();
                }

                switch (this.ConnectionType)
                {
                    case ConnectType.OleDb:
                        System.Data.OleDb.OleDbCommand CmdOleDb = new System.Data.OleDb.OleDbCommand(sql, (System.Data.OleDb.OleDbConnection)this.m_DbConnection);
                        CmdOleDb.CommandTimeout = commandTimeout;
                        Idr = CmdOleDb.ExecuteReader();
                        return Idr;

                    case ConnectType.Odbc:
                        System.Data.Odbc.OdbcCommand CmdOdbc = new System.Data.Odbc.OdbcCommand(sql, (System.Data.Odbc.OdbcConnection)this.m_DbConnection);
                        CmdOdbc.CommandTimeout = commandTimeout;
                        Idr = CmdOdbc.ExecuteReader();
                        return Idr;

                    default:
                        System.Data.SqlClient.SqlCommand CmdSql = new System.Data.SqlClient.SqlCommand(sql, (System.Data.SqlClient.SqlConnection)this.m_DbConnection);
                        CmdSql.CommandTimeout = commandTimeout;
                        Idr = CmdSql.ExecuteReader();
                        return Idr;
                }
            }
            catch (System.Exception ex)
            {
                ErrorLog(sql, ex.Message);

                throw new System.Exception(sql + "\n" + ex.Message);
            }
        }

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数
        /// </summary>
        /// <param name="sql">需要执行的 SQL 语句</param>
        /// <returns></returns>
        public int Command(string sql)
        {
            return Command(sql, 30);
        }

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数
        /// </summary>
        /// <param name="sql">需要执行的 SQL 语句</param>
        /// <param name="commandTimeout">在终止执行命令的尝试并生成错误之前的等待时间(秒)。</param>
        /// <returns></returns>
        public int Command(string sql, int commandTimeout)
        {
            try
            {
                if (this.m_DbConnection.State != System.Data.ConnectionState.Open)
                {
                    this.m_DbConnection.Open();
                }

                switch (this.ConnectionType)
                {
                    case ConnectType.OleDb:
                        System.Data.OleDb.OleDbCommand CmdOleDb = new System.Data.OleDb.OleDbCommand(sql, (System.Data.OleDb.OleDbConnection)this.m_DbConnection);
                        CmdOleDb.CommandTimeout = commandTimeout;
                        return CmdOleDb.ExecuteNonQuery();
                    case ConnectType.Odbc:
                        System.Data.Odbc.OdbcCommand CmdOdbc = new System.Data.Odbc.OdbcCommand(sql, (System.Data.Odbc.OdbcConnection)this.m_DbConnection);
                        CmdOdbc.CommandTimeout = commandTimeout;
                        return CmdOdbc.ExecuteNonQuery();

                    default:
                        System.Data.SqlClient.SqlCommand CmdSql = new System.Data.SqlClient.SqlCommand(sql, (System.Data.SqlClient.SqlConnection)this.m_DbConnection);
                        CmdSql.CommandTimeout = commandTimeout;
                        return CmdSql.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex)
            {
                ErrorLog(sql, ex.Message);

                throw new System.Exception(sql + "\n" + ex.Message);
            }
        }

        public bool ExecuteTransaction(string[] sql, int commandTimeout)
        {
            try
            {
                if (this.m_DbConnection.State != System.Data.ConnectionState.Open)
                {
                    this.m_DbConnection.Open();
                }
                int i = 0;
                switch (this.ConnectionType)
                {
                    case ConnectType.OleDb:
                        System.Data.OleDb.OleDbCommand CmdOleDb = new System.Data.OleDb.OleDbCommand();

                        CmdOleDb.Connection = (System.Data.OleDb.OleDbConnection)this.m_DbConnection;
                        CmdOleDb.CommandTimeout = commandTimeout;

                        try
                        {
                            CmdOleDb.Transaction.Begin();
                            for (i = 0; i < sql.Length - 1; i++)
                            {
                                CmdOleDb.CommandText = sql[i];
                                CmdOleDb.ExecuteNonQuery();
                            }
                            CmdOleDb.Transaction.Commit();
                        }
                        catch (System.Exception ex)
                        {
                            CmdOleDb.Transaction.Rollback();
                            throw new System.Exception(sql[i] + "\n" + ex.Message);
                        }
                        break;

                    default:
                        /*
                        System.Data.SqlClient.SqlCommand CmdSql = new System.Data.SqlClient.SqlCommand();
                        CmdSql.Connection = (System.Data.SqlClient.SqlConnection)this.m_DbConnection;
                        CmdSql.CommandTimeout = commandTimeout;
                        SqlTransaction trans = null;
                        try
                        {
                            trans = CmdSql.Connection.BeginTransaction();

                            for (i = 0; i < sql.Length - 1; i++)
                            {
                                CmdSql.CommandText = sql[i];
                                CmdSql.ExecuteNonQuery();
                            }

                            trans.Commit();
                        }
                        catch (System.Exception ex)
                        {
                            trans.Rollback();
                            throw new System.Exception(sql[i] + "\n" + ex.Message);
                        }
                         * */
                        Grove.ORM.ObjectOperator ObjectOperator = GetObjectOperator();
                        try
                        {
                            ObjectOperator.BeginTranscation();
                            for (i = 0; i < sql.Length; i++)
                            {
                                ObjectOperator.DbOperator.ExecNonQuery(sql[i]);
                            }
                            ObjectOperator.Commit();
                        }
                        catch (System.Exception ex)
                        {
                            ObjectOperator.Rollback();
                            throw new System.Exception(sql[i] + "\n" + ex.Message);
                        }
                        break;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public DataSet ExecuteProcedure(string ProcedureName, string CursorName, params SqlParameter[] commandParameters)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            IDbDataAdapter DbAdapter = new SqlDataAdapter();

            if (this.m_DbConnection.State != System.Data.ConnectionState.Open)
            {
                this.m_DbConnection.Open();
            }

            using (SqlConnection conn = (SqlConnection)this.m_DbConnection)
            {
                PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, ProcedureName, commandParameters);
                DbAdapter.SelectCommand = cmd;
                DbAdapter.Fill(ds);
                cmd.Parameters.Clear();
            }
            return ds;

        }

        public IDataReader ExecuteIDataReader(string ProcedureName, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr = null;

            if (this.m_DbConnection.State != System.Data.ConnectionState.Open)
            {
                this.m_DbConnection.Open();
            }

            using (SqlConnection conn = (SqlConnection)this.m_DbConnection)
            {
                PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, ProcedureName, commandParameters);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.CommandText = ProcedureName;
                dr = cmd.ExecuteReader();
                cmd.Parameters.Clear();

            }
            return dr;
        }

        public int ExecuteProcedure(string ProcedureName, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            if (this.m_DbConnection.State != System.Data.ConnectionState.Open)
            {
                this.m_DbConnection.Open();
            }


            using (SqlConnection conn = (SqlConnection)this.m_DbConnection)
            {
                PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, ProcedureName, commandParameters);
                int val = cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
                return val;
            }
        }
        public int ExecuteProcedure(string ProcedureName, int outputIndex, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            if (this.m_DbConnection.State != System.Data.ConnectionState.Open)
            {
                this.m_DbConnection.Open();
            }


            using (SqlConnection conn = (SqlConnection)this.m_DbConnection)
            {
                PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, ProcedureName, commandParameters);
                cmd.ExecuteNonQuery();
                int val = int.Parse(commandParameters[outputIndex].Value.ToString());
                cmd.Parameters.Clear();
                return val;
            }
        }
        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        /// <summary>
        /// 执行 SQL 查询，并返回 DataTable 对象。
        /// </summary>
        /// <param name="sql">需要执行的 SQL 语句</param>
        /// <returns></returns>

        public System.Data.DataTable DataTable(string sql)
        {
            return DataTable(sql, 30);
        }

        /// <summary>
        /// 执行 SQL 查询，并返回 DataTable 对象。
        /// </summary>
        /// <param name="sql">需要执行的 SQL 语句</param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public System.Data.DataTable DataTable(string sql, int commandTimeout)
        {
            try
            {
                if (this.m_DbConnection.State != System.Data.ConnectionState.Open)
                {
                    this.m_DbConnection.Open();
                }

                switch (this.ConnectionType)
                {
                    case ConnectType.OleDb:
                        System.Data.DataTable DataTableOleDb = new System.Data.DataTable();
                        System.Data.OleDb.OleDbCommand CmdOleDb = new System.Data.OleDb.OleDbCommand(sql, (System.Data.OleDb.OleDbConnection)this.m_DbConnection);
                        CmdOleDb.CommandTimeout = commandTimeout;
                        System.Data.OleDb.OleDbDataAdapter AdptOleDb = new System.Data.OleDb.OleDbDataAdapter(CmdOleDb);
                        AdptOleDb.Fill(DataTableOleDb);
                        return DataTableOleDb;

                    case ConnectType.Odbc:
                        System.Data.DataTable DataTableOdbc = new System.Data.DataTable();
                        System.Data.Odbc.OdbcCommand CmdOdbc = new System.Data.Odbc.OdbcCommand(sql, (System.Data.Odbc.OdbcConnection)this.m_DbConnection);
                        CmdOdbc.CommandTimeout = commandTimeout;
                        System.Data.Odbc.OdbcDataAdapter AdptOdbc = new System.Data.Odbc.OdbcDataAdapter(CmdOdbc);
                        AdptOdbc.Fill(DataTableOdbc);
                        return DataTableOdbc;

                    default:
                        System.Data.DataTable DataTableSql = new System.Data.DataTable();
                        System.Data.SqlClient.SqlCommand CmdSql = new System.Data.SqlClient.SqlCommand(sql, (System.Data.SqlClient.SqlConnection)this.m_DbConnection);
                        CmdSql.CommandTimeout = commandTimeout;
                        System.Data.SqlClient.SqlDataAdapter AdptSql = new System.Data.SqlClient.SqlDataAdapter(CmdSql);
                        AdptSql.Fill(DataTableSql);
                        return DataTableSql;
                }
            }
            catch (System.Exception ex)
            {
                ErrorLog(sql, ex.Message);

                throw new System.Exception(sql + "\n" + ex.Message);
            }
        }

        /// <summary>
        /// 获取 IDbCommand 对象，通常在操作大对象数据类型时与 DbDataParameter 配合使用。
        /// </summary>
        /// <returns></returns>
        public System.Data.IDbCommand DbCommand()
        {
            return DbCommand("", 30);
        }

        /// <summary>
        /// 获取 IDbCommand 对象，通常在操作大对象数据类型时与 DbDataParameter 配合使用。
        /// </summary>
        /// <param name="sql">需要执行的 SQL 语句</param>
        /// <returns></returns>
        public System.Data.IDbCommand DbCommand(string sql)
        {
            return DbCommand(sql, 30);
        }

        /// <summary>
        /// 获取 IDbCommand 对象，通常在操作大对象数据类型时与 DbDataParameter 配合使用。
        /// </summary>
        /// <param name="sql">需要执行的 SQL 语句</param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public System.Data.IDbCommand DbCommand(string sql, int commandTimeout)
        {
            try
            {
                if (this.m_DbConnection.State != System.Data.ConnectionState.Open)
                {
                    this.m_DbConnection.Open();
                }

                switch (this.ConnectionType)
                {
                    case ConnectType.OleDb:
                        System.Data.OleDb.OleDbCommand CmdOleDb = new System.Data.OleDb.OleDbCommand();
                        CmdOleDb.Connection = (System.Data.OleDb.OleDbConnection)this.m_DbConnection;
                        CmdOleDb.CommandTimeout = commandTimeout;
                        if (sql.Length > 0)
                        {
                            CmdOleDb.CommandText = sql;
                        }
                        return CmdOleDb;

                    case ConnectType.Odbc:
                        System.Data.Odbc.OdbcCommand CmdOdbc = new System.Data.Odbc.OdbcCommand();
                        CmdOdbc.Connection = (System.Data.Odbc.OdbcConnection)this.m_DbConnection;
                        CmdOdbc.CommandTimeout = commandTimeout;
                        if (sql.Length > 0)
                        {
                            CmdOdbc.CommandText = sql;
                        }
                        return CmdOdbc;

                    default:
                        System.Data.SqlClient.SqlCommand CmdSql = new System.Data.SqlClient.SqlCommand();
                        CmdSql.Connection = (System.Data.SqlClient.SqlConnection)this.m_DbConnection;
                        CmdSql.CommandTimeout = commandTimeout;
                        if (sql.Length > 0)
                        {
                            CmdSql.CommandText = sql;
                        }
                        return CmdSql;
                }
            }
            catch (System.Exception ex)
            {
                ErrorLog(sql, ex.Message);

                throw new System.Exception(sql + "\n" + ex.Message);
            }
        }

        /// <summary>
        /// 获取操作大对象数据类型的接口。通常需要与 DbCommand 配合使用。
        /// </summary>
        /// <returns></returns>
        public System.Data.IDbDataParameter DbDataParameter()
        {
            switch (this.ConnectionType)
            {
                case ConnectType.OleDb:
                    return new System.Data.OleDb.OleDbParameter();

                case ConnectType.Odbc:
                    return new System.Data.Odbc.OdbcParameter();

                default:
                    return new System.Data.SqlClient.SqlParameter();
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            try
            {
                if (null != Idr)
                {
                    Idr.Close();
                }

                if (m_DbConnection.State == ConnectionState.Open)
                {
                    this.m_DbConnection.Close();
                }
                Idr.Dispose();
                this.m_DbConnection.Dispose();
            }
            catch { }

        }

        /// <summary>
        /// 设置数据库连接信息。该连接信息将保存在“ConnectionConfigFile”属性指定的配置文件内，如果没有配置文件，则按默认连接方式进行连接。
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="connectionType">数据库连接类型</param>
        public static void SetConnection(string connectionString, ConnectType connectionType)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(ConnectionConfigFile, false);
            sw.WriteLine(connectionString);
            sw.WriteLine(Convert.ToInt32(connectionType));
            sw.Flush();
            sw.Dispose();
            sw.Close();
        }

        /// <summary>
        /// 出错时记录错误消息
        /// </summary>
        /// <param name="ConnectionString">出错时的数据库连接字符串</param>
        /// <param name="sql">出错时正在执行的 SQL 语句</param>
        /// <param name="exMessage">出错消息</param>
        public void ErrorLog(string sql, string exMessage)
        {
            try
            {
                //此处可以添加出错时记录错误消息的代码

                if (Idr != null)
                {
                    Idr.Close();
                }
            }
            catch
            {

            }
        }

        public Grove.ORM.ObjectOperator GetObjectOperator()
        {
            Grove.ORM.ObjectOperator ObjectOperator = new Grove.ORM.ObjectOperator(this.m_ConnectionString);
            return ObjectOperator;
        }
        /// <summary>
        /// 分页函数
        /// </summary>
        /// <param name="PageIndex">要转到的页号</param>
        /// <param name="PageSize">每一页的记录条数</param>
        /// <param name="TableName">表名</param>
        /// <param name="mKey">主键名称</param>
        /// <param name="strFields">要显示的字段名</param>
        /// <param name="strConditions">查询条件不用带 where </param>
        /// <returns></returns>
        public string Pagex(int PageIndex, int PageSize, string TableName, string mKey, string strFields, string strConditions)
        {
            string sql;
            if (PageIndex == 1)
            {
                if (strConditions == "")
                {
                    sql = "select top " + PageSize + "  " + strFields + " from  " + TableName + " order by " + mKey;
                }
                else
                {
                    sql = "select top " + PageSize + "  " + strFields + " from  " + TableName + "  where " + strConditions + " order by " + mKey;
                }
            }
            else
            {
                if (strConditions == "")
                {
                    sql = "select top " + PageSize + "  " + strFields + " from  " + TableName + "  " + " where  " + mKey + ">(select max(  " + mKey + ") from (select top " + ((PageIndex - 1) * PageSize) + "  " + mKey + " from  " + TableName + "  " + " order by  " + mKey + " ) tblTmp)  order by " + mKey;
                }
                else
                {
                    sql = "select top " + PageSize + "  " + strFields + " from  " + TableName + "  " + " where  " + strConditions + " and " + mKey + ">(select max(  " + mKey + ") from (select top " + ((PageIndex - 1) * PageSize) + "  " + mKey + " from  " + TableName + "   where " + strConditions + " order by  " + mKey + " ) tblTmp)  order by " + mKey;
                }
            }
            return sql;

        }
    }

    public class DbPage
    {

        public DbPage(Db db)
        {
            this.Db = db;
        }

        private Db m_Db;
        /// <summary>
        /// 表示到数据源的连接
        /// </summary>
        public Db Db
        {
            get { return m_Db; }
            set { m_Db = value; }
        }

        private string m_Table;
        /// <summary>
        /// 查询的表名。如 Table1 也可以是 Table1,Table2
        /// </summary>
        public string Table
        {
            get { return m_Table; }
            set { m_Table = value; }
        }

        private string m_TableGroup;
        /// <summary>
        /// 统计的表名 ，如：left join BuyPlanGoods on BuyPlanGoods.AccountId_PS= SupplyCompany.AccountId_PS 
        /// </summary>
        public string TableGroup
        {
            get { return m_TableGroup; }
            set { m_TableGroup = value; }
        }

        private string m_Where = "";
        /// <summary>
        /// 查询条件。如 FieldName1=123 或者 Table1.FieldName=Table2.FieldName And Table1.FieldName2=Table2.FieldName2 也可以为空没有查询条件
        /// </summary>
        public string Where
        {
            get { return m_Where; }
            set { m_Where = value; }
        }

        private string m_Key;
        /// <summary>
        /// 主键字段。该属性不能为空，且只能是一个唯一的主键。
        /// </summary>
        public string Key
        {
            get { return m_Key; }
            set { m_Key = value; }
        }

        private string m_OrderBy = "";
        /// <summary>
        /// 排序字段。如果该字段不设置则默认为“Key”主键字段
        /// </summary>
        public string OrderBy
        {
            get { return m_OrderBy; }
            set { m_OrderBy = value; }
        }

        private bool m_OrderByIsDesc = false;
        /// <summary>
        /// 是否以倒序排序。默认为正序排序
        /// </summary>
        public bool OrderByIsDesc
        {
            get { return m_OrderByIsDesc; }
            set { m_OrderByIsDesc = value; }
        }

        private string m_SelectFieldGroup = "*";
        /// <summary>
        /// 要统计的字段，即Group By 后面的字段
        /// </summary>
        public string SelectFieldGroup
        {
            get { return m_SelectFieldGroup; }
            set { m_SelectFieldGroup = value; }
        }

        private string m_DGJE = "";
        /// <summary>
        /// 要统计的字段，即Group By 后面的字段 定购金额
        /// </summary>
        public string DGJE
        {
            get { return m_DGJE; }
            set { m_DGJE = value; }
        }

        private string m_DHJE = "";
        /// <summary>
        /// 要统计的字段，即Group By 后面的字段 到货金额或入库金额
        /// </summary>
        public string DHJE
        {
            get { return m_DHJE; }
            set { m_DHJE = value; }
        }
        private string m_HKJE = "";
        /// <summary>
        /// 要统计的字段 回款金额
        /// </summary>
        public string HKJE
        {
            get { return m_HKJE; }
            set { m_HKJE = value; }
        }

        private string m_bidprice = "";
        /// <summary>
        /// 要统计的字段 中标金额
        /// </summary>
        public string bidprice
        {
            get { return m_bidprice; }
            set { m_bidprice = value; }
        }

        private string m_bakeprice = "";
        /// <summary>
        /// 要统计的字段 自行采购金额
        /// </summary>
        public string bakeprice
        {
            get { return m_bakeprice; }
            set { m_bakeprice = value; }
        }

        private string m_Nolimtprice = "";
        /// <summary>
        /// 要统计的字段 22种降价药品
        /// </summary>
        public string Nolimtprice
        {
            get { return m_Nolimtprice; }
            set { m_Nolimtprice = value; }
        }

        private string m_GMPprice = "";
        /// <summary>
        /// 要统计的字段 人血蛋白
        /// </summary>
        public string GMPprice
        {
            get { return m_GMPprice; }
            set { m_GMPprice = value; }
        }

        private string m_Keyprice = "";
        /// <summary>
        /// 要统计的字段 重点监控
        /// </summary>
        public string Keyprice
        {
            get { return m_Keyprice; }
            set { m_Keyprice = value; }
        }

        private string m_GroupHaving = "";
        public string GroupHaving
        {
            get { return m_GroupHaving; }
            set { m_GroupHaving = value; }
        }
        private string m_SelectField = "*";
        /// <summary>
        /// 选择记录集的输出字段，如果不输入则为“*”。如：“*”或者“FieldName1”或者“Table1.FieldName , Table2.FieldName As FieldName3”。
        /// </summary>
        public string SelectField
        {
            get { return m_SelectField; }
            set { m_SelectField = value; }
        }


        private int m_PageSize = 20;
        /// <summary>
        /// 设置每页显示多少条记录
        /// </summary>
        public int PageSize
        {
            get { return m_PageSize; }
            set
            {
                if (value < 1)
                {
                    m_PageSize = 1;
                }
                else
                {
                    m_PageSize = value;
                }
            }
        }

        private int m_PageIndex;
        /// <summary>
        /// 设置或获取当前的页面
        /// </summary>
        public int PageIndex
        {
            get
            {
                return m_PageIndex;
            }
            set { m_PageIndex = value; }
        }

        private int m_PageCount;
        /// <summary>
        /// 获取总页数
        /// </summary>
        public int PageCount
        {
            get { return m_PageCount; }
        }

        private int m_RowCount;
        /// <summary>
        /// 总的记录数
        /// </summary>
        public int RowCount
        {
            get { return m_RowCount; }
        }

        /// <summary>
        /// 执行分页查询。
        /// </summary>
        /// <returns></returns>
        public System.Data.IDataReader ExecuteReader()
        {
            return ExecuteReader(30);
        }
        public System.Data.IDataReader ExecuteReader(string strSql)
        {
            return this.Db.DataReader(strSql, 30);
        }
        private void Execute(int commandTimeout)
        {
            string strSql;
            System.Data.IDataReader dr;

            string strWhere1 = "";                                  //用于第1次 Top 筛选
            string strWhere = "";
            if (this.Where.Length > 0)
            {
                strWhere1 = "Where " + this.Where;
                strWhere = "Where " + this.Where + " And ";
            }
            else
            {
                strWhere = "Where ";
            }

            if (this.OrderBy.Length == 0)
            {
                this.OrderBy = this.Key;
            }

            string strOrderBy1 = "";
            string strOrderBy2 = "";
            string[] sArray = this.OrderBy.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            string OrderBy_First = sArray[0];
            string OrderBy_First_DESC = OrderBy_First + " DESC";
            if (this.OrderByIsDesc)
            {
                foreach (string strOrderField in this.OrderBy.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    strOrderBy1 += strOrderField + ",";
                    strOrderBy2 += strOrderField + " Desc,";
                }
            }
            else
            {
                foreach (string strOrderField in this.OrderBy.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (strOrderField.Length > 4)
                    {
                        if (strOrderField.Substring(strOrderField.Length - 4) == "desc")
                        {
                            strOrderBy1 += strOrderField.Substring(0, strOrderField.Length - 4) + ",";
                            strOrderBy2 += strOrderField + ",";
                        }
                        else
                        {
                            strOrderBy1 += strOrderField + " Desc,";
                            strOrderBy2 += strOrderField + ",";
                        }

                    }
                    else
                    {
                        strOrderBy1 += strOrderField + " Desc,";
                        strOrderBy2 += strOrderField + ",";
                    }

                }
            }
            strOrderBy1 = strOrderBy1.Substring(0, strOrderBy1.Length - 1);
            strOrderBy2 = strOrderBy2.Substring(0, strOrderBy2.Length - 1);
            //if (this.Key.IndexOf(",") > -1)
            //{
            //    strSql = "Select Count(*) COUNT_NUM From " + this.Table + " " + strWhere1;
            //}
            //else
            //{
            //    strSql = "Select Count(" + this.Key + ") COUNT_NUM From " + this.Table + " " + strWhere1;
            //}
            strSql = "select count(*) as COUNT_NUM from (Select distinct " + this.Key + "  From " + this.Table + " " + strWhere1 + ") a";
            //以key值统计数目 但sql里面有distinct 统计时会有错误发生
            //这里判断selectField的值左边8位是否为distinct,采用selectfield来作为查询条件,来获取总数

            bool haveDistinct = false;
            if (SelectField.Trim().Length > 8) haveDistinct = this.SelectField.Trim().Substring(0, 8).ToLower() == "distinct";
            bool haveGroupby = this.SelectFieldGroup != "*" && this.SelectFieldGroup != "";

            if (haveDistinct)
            {
                strSql = "select count(*) as COUNT_NUM from (Select  " + this.SelectField + "  From " + this.Table + " " + strWhere1 + ") a";
                if (haveGroupby) strSql = "select count(*) as COUNT_NUM from (Select  " + this.SelectField + "  From " + this.Table + " " + strWhere1 + "group by " + this.SelectFieldGroup + ") a";
            }
            dr = this.Db.DataReader(strSql, commandTimeout);
            if (dr.Read())
            {
                this.m_RowCount = SysFun.ToInt(dr["COUNT_NUM"]);
            }
            dr.Close();

            this.m_PageCount = this.RowCount / this.PageSize;
            if ((this.RowCount % this.PageSize) > 0)
            {
                this.m_PageCount++;
            }


            if (this.RowCount == 0)
            {
                this.PageIndex = 0;
            }
            else
            {
                if (this.PageIndex < 1)
                {
                    this.PageIndex = 1;
                }
                else if (this.PageIndex > this.PageCount)
                {
                    this.PageIndex = this.PageCount;
                }
            }

            switch (this.Db.ConnectionType)
            {
                //case ConnectType.OleDb:
                //    return Idr;

                //case ConnectType.Odbc:
                //    return Idr;

                default:
                    //if (this.PageIndex == 1)
                    //{
                    //    strSql = "Select   Top " + (this.PageSize * this.PageIndex) + " " + this.SelectField + " From " + this.Table + " " + strWhere1 + " Order By " + strOrderBy2;
                    //}
                    //else if (this.PageIndex == this.PageCount)
                    //{
                    //    if ((this.RowCount % this.PageSize) > 0)
                    //    {
                    //        strSql = "select * from (Select * from (Select   Top " + (this.RowCount % this.PageSize) + " " + this.SelectField + " From " + this.Table + " " + strWhere1 + "  Order By " + strOrderBy1 + ") a) b order by " + strOrderBy2;
                    //    }
                    //    else
                    //    {
                    //        strSql = "select * from (Select * from (Select   Top " + this.PageSize + " " + this.SelectField + " From " + this.Table + " " + strWhere1 + "  Order By " + strOrderBy1 + ") a) b order by " + strOrderBy2;
                    //    }
                    //}
                    //else
                    //{
                    //    strSql = "select * from (Select top " + this.PageSize + " * from (Select   Top " + (this.PageSize * this.PageIndex) + " " + this.SelectField + " From " + this.Table + " " + strWhere1 + "  Order By " + strOrderBy2 + ") a order by " + strOrderBy1 + ") b order by " + strOrderBy2;

                    //}
                    //break;
                    string strIn;
                    if (this.PageIndex == 1)
                    {
                        strIn = "Select Top " + (this.PageSize * this.PageIndex) + " " + this.Key + " From " + this.Table + " " + strWhere1 + " Order By " + strOrderBy2;
                        if (haveDistinct) strIn = "Select distinct Top " + (this.PageSize * this.PageIndex) + " " + OrderBy_First + " From " + this.Table + " " + strWhere1 + " Order By " + OrderBy_First_DESC;
                        //add by ws
                    }
                    else if (this.PageIndex == this.PageCount)
                    {
                        if ((this.RowCount % this.PageSize) > 0)
                        {
                            strIn = "Select Top " + (this.RowCount % this.PageSize) + " " + this.Key + " From " + this.Table + " " + strWhere1 + " Order By " + strOrderBy1;
                            if (haveDistinct) strIn = "Select distinct Top " + (this.RowCount % this.PageSize) + " " + OrderBy_First + " From " + this.Table + " " + strWhere1 + " Order By " + OrderBy_First;

                        }
                        else
                        {
                            strIn = "Select Top " + this.PageSize + " " + this.Key + " From " + this.Table + " " + strWhere1 + " Order By " + strOrderBy1;
                            if (haveDistinct) strIn = "Select distinct Top " + this.PageSize + " " + OrderBy_First + " From " + this.Table + " " + strWhere1 + " Order By " + OrderBy_First;

                        }
                    }
                    else
                    {
                        strIn = "Select Top " + this.PageSize + " " + this.Key + " From " + this.Table + " " + strWhere + " " + this.Key + " In (Select Top " + (this.PageSize * this.PageIndex) + " " + this.Key + " From " + this.Table + " " + strWhere1 + " Order By " + strOrderBy2 + ") Order By " + strOrderBy1;
                        if (haveDistinct) strIn = "Select distinct Top " + this.PageSize + " " + OrderBy_First + " From " + this.Table + " " + strWhere + " " + OrderBy_First + " In (Select distinct Top " + (this.PageSize * this.PageIndex) + " " + OrderBy_First + " From " + this.Table + " " + strWhere1 + " Order By " + strOrderBy2 + ") Order By " + strOrderBy1;

                    }
                    if (PageSize == 1)
                    {
                        strSql = "Select " + this.SelectField + " From " + this.Table + " " + strWhere1 + " " + " Order By " + strOrderBy2;
                        if (haveGroupby) strSql = "Select " + this.SelectField + " From " + this.Table + " " + strWhere1 + " group by " + this.SelectFieldGroup + " Order By " + strOrderBy2;
                    }
                    else
                    {
                        strSql = "Select " + this.SelectField + " From " + this.Table + " " + strWhere + " " + this.Key + " In (" + strIn + ") Order By " + strOrderBy2;
                        if (haveGroupby) strSql = "Select " + this.SelectField + " From " + this.Table + " " + strWhere + " " + OrderBy_First + " In (" + strIn + ") group by " + this.SelectFieldGroup + " Order By " + strOrderBy2;
                        if (haveDistinct)
                        {
                            strSql = "Select " + this.SelectField + " From " + this.Table + " " + strWhere + " " + OrderBy_First + " In (" + strIn + ") Order By " + strOrderBy2;
                            if (haveGroupby) strSql = "Select " + this.SelectField + " From " + this.Table + " " + strWhere + " " + OrderBy_First + " In (" + strIn + ") group by " + this.SelectFieldGroup + " Order By " + strOrderBy2;
                        }
                    }
                    break;
            }

            m_Sql = strSql;
        }

        /// <summary>
        /// 执行分页查询，并指定查询超时时间。
        /// </summary>
        /// <param name="CommandTimeout">在终止执行命令的尝试并生成错误之前的等待时间(秒)。</param>
        /// <returns></returns>
        public System.Data.IDataReader ExecuteReader(int commandTimeout)
        {
            Execute(commandTimeout);
            return this.Db.DataReader(this.Sql, commandTimeout);
        }

        public System.Data.DataTable DataTable(int commandTimeout)
        {
            Execute(commandTimeout);

            return this.Db.DataTable(this.Sql, commandTimeout);
        }

        /// <summary>
        /// 本次查询所用到的 SQL 语句。
        /// </summary>
        private string m_Sql;

        public string Sql
        {
            get { return m_Sql; }
        }


        /// <summary>
        /// 获得页面连接，例如 BidTableProductSelect.aspx?PG={PageNum} 其中 {PageNum} 指替换为页码的标记
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetPageLink(string url)
        {
            string strValue = "";

            if (this.PageIndex > 1)
            {
                strValue += "<a href='" + url.Replace("{PageNum}", SysFun.ToTrim(this.PageIndex - 1)) + "'>上一页</a> ";
            }
            else
            {
                strValue += "<font color='#C0C0C0'>上一页</font> ";
            }

            if (this.PageIndex < this.PageCount)
            {
                strValue += "<a href='" + url.Replace("{PageNum}", SysFun.ToTrim(this.PageIndex + 1)) + "'>下一页</a> ";
            }
            else
            {
                strValue += "<font color='#C0C0C0'>下一页</font> ";
            }

            strValue += "［当前第 " + this.PageIndex.ToString() + " 页　共 " + this.PageCount.ToString() + " 页　计 " + this.RowCount + " 条］ ";

            strValue += "转到第";
            strValue += "<select onchange=\"javascript:window.location='" + url.Replace("{PageNum}", "' + (this.selectedIndex + 1) + '") + "'\">";
            for (int intIndex = 1; intIndex <= this.PageCount; intIndex++)
            {
                if (intIndex == this.PageIndex)
                {
                    strValue += "<option selected>" + intIndex.ToString();
                }
                else
                {
                    strValue += "<option>" + intIndex.ToString();
                }
            }
            strValue += "</select>";
            strValue += "页";
            return strValue;
        }
        private const string GroupCompanyPSByWhere = @"Select  Company.accountid,Company.CompanyName,Company.UserCode ,convert(numeric(20,2),sum(GoodsNum*TradePrice)) as DGJE ,convert(numeric(20,2),sum(inflownum*TradePrice)) as DHJE 
                        From SupplyCompany inner join Company on SupplyCompany.AccountId_PS=Company.AccountId 
                        inner join BuyPlanGoods on BuyPlanGoods.AccountId_PS= SupplyCompany.AccountId_PS and BuyPlanGoods.ProjectId=SupplyCompany.ProjectId 
                         where {0} Group By Company.accountid,Company.CompanyName,Company.UserCode  Order By Company.CompanyName";
        /// <summary>
        /// 统计配送企业的配送金额
        /// </summary>
        /// <param name="CompanyNamePS">配送企业名称</param>
        /// <param name="UserCodePS">配送企业编码</param>
        /// <param name="StartDate">开始时间（医院确认到货）</param>
        /// <param name="EndDate">结束时间（医院确认到货）</param>
        /// <param name="AreaId">当前登陆用户的地区编码</param>
        /// <returns></returns>
        public System.Data.IDataReader ExecuteReaderGroupCompanyPSByWhere(string CompanyNamePS, string UserCodePS, string StartDate, string EndDate, int AreaId)
        {
            string _Where = "";

            m_Sql = String.Format(GroupCompanyPSByWhere, _Where);
            return this.Db.DataReader(this.Sql, 120);
        }
        /// <summary>
        /// 执行分页查询，并指定查询超时时间。
        /// </summary>
        /// <param name="CommandTimeout">在终止执行命令的尝试并生成错误之前的等待时间(秒)。</param>
        /// <returns></returns>
        public System.Data.IDataReader ExecuteReaderGroup(int commandTimeout)
        {
            ExecuteGroup(commandTimeout);
            return this.Db.DataReader(this.Sql, commandTimeout);
        }
        private void ExecuteGroup(int commandTimeout)
        {
            string strSql;
            System.Data.IDataReader dr;

            string strWhere1 = "";                                  //用于第1次 Top 筛选
            string strWhere = "";
            if (this.Where.Length > 0)
            {
                strWhere1 = "Where " + this.Where;
                strWhere = "Where " + this.Where + " And ";
            }
            else
            {
                strWhere = "Where ";
            }

            if (this.OrderBy.Length == 0)
            {
                this.OrderBy = this.Key;
            }

            string strOrderBy1 = "";
            string strOrderBy2 = "";
            if (this.OrderByIsDesc)
            {
                foreach (string strOrderField in this.OrderBy.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    strOrderBy1 += strOrderField + ",";
                    strOrderBy2 += strOrderField + " Desc,";
                }
            }
            else
            {
                foreach (string strOrderField in this.OrderBy.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    strOrderBy1 += strOrderField + " Desc,";
                    strOrderBy2 += strOrderField + ",";
                }
            }
            strOrderBy1 = strOrderBy1.Substring(0, strOrderBy1.Length - 1);
            strOrderBy2 = strOrderBy2.Substring(0, strOrderBy2.Length - 1);
            if (this.GroupHaving != "")
            {
                strSql = "Select Count(*) As COUNT_NUM From (Select " + this.SelectField + " From " + this.Table + " " + strWhere1 + " Group by " + this.SelectFieldGroup + " Having " + this.GroupHaving + ") As a";
                dr = this.Db.DataReader(strSql, commandTimeout);
                if (dr.Read())
                {
                    this.m_RowCount = SysFun.ToInt(dr["COUNT_NUM"]);
                }
                dr.Close();
            }
            else
            {
                //strSql = "select count(*) as COUNT_NUM from (Select distinct " + this.Key + "  From " + this.Table + " " + strWhere1 + ") a";
                strSql = "Select Count(*) As COUNT_NUM From (Select " + this.SelectField + " From " + this.Table + " " + strWhere1 + " Group by " + this.SelectFieldGroup + ") As a";
                dr = this.Db.DataReader(strSql, commandTimeout);
                if (dr.Read())
                {
                    this.m_RowCount = SysFun.ToInt(dr["COUNT_NUM"]);
                }
                dr.Close();
            }
            // convert(numeric(20,2),sum(GoodsNum*TradePrice)) as DGJE ,convert(numeric(20,2),sum(inflownum*TradePrice)) as DHJE

            //strSql = "select  convert(numeric(20,2),sum(GoodsNum*TradePrice)) as DGJE ,convert(numeric(20,2),sum(inflownum*TradePrice)) as DHJE from " + this.Table + " " + strWhere1;
            //dr = this.Db.DataReader(strSql, commandTimeout);
            //if (dr.Read())
            //{
            //    this.DGJE = dr["DGJE"].ToString();
            //    this.DHJE = dr["DHJE"].ToString();
            //}
            //dr.Close();
            int x = 0, y = 0, bid = 0, bak = 0, nolim = 0, nGmp = 0, nKey = 0;
            string _Sql = "Select  ";
            if (this.DGJE != "")
            {
                x = 1;
                _Sql += " " + this.DGJE;
                this.DGJE = "";
            }
            if (this.DHJE != "")
            {
                y = 1;
                if (_Sql != "Select  ")
                {
                    _Sql += " ," + this.DHJE;
                }
                else
                {
                    _Sql += " " + this.DHJE;
                }
                this.DHJE = "";
            }

            if (this.bidprice != "")
            {
                bid = 1;
                if (_Sql != "Select  ")
                {
                    _Sql += " ," + this.bidprice;
                }
                else
                {
                    _Sql += " " + this.bidprice;
                }
                this.bidprice = "";
            }

            if (this.bakeprice != "")
            {
                bak = 1;
                if (_Sql != "Select  ")
                {
                    _Sql += " ," + this.bakeprice;
                }
                else
                {
                    _Sql += " " + this.bakeprice;
                }
                this.bakeprice = "";
            }

            if (this.Nolimtprice != "")
            {
                nolim = 1;
                if (_Sql != "Select  ")
                {
                    _Sql += " ," + this.Nolimtprice;
                }
                else
                {
                    _Sql += " " + this.Nolimtprice;
                }
                this.Nolimtprice = "";
            }
            if (this.GMPprice != "")
            {
                nGmp = 1;
                if (_Sql != "Select  ")
                {
                    _Sql += " ," + this.GMPprice;
                }
                else
                {
                    _Sql += " " + this.GMPprice;
                }
                this.GMPprice = "";
            }
            if (this.Keyprice != "")
            {
                nKey = 1;
                if (_Sql != "Select  ")
                {
                    _Sql += " ," + this.Keyprice;
                }
                else
                {
                    _Sql += " " + this.Keyprice;
                }
                this.Keyprice = "";
            }


            if (_Sql != "Select  ")
            {
                _Sql += "  from " + this.Table + " " + strWhere1;
                dr = this.Db.DataReader(_Sql, commandTimeout);
                if (dr.Read())
                {
                    if (x == 1)
                    {
                        this.DGJE = dr["DGJE"].ToString();
                    }
                    if (y == 1)
                    {
                        this.DHJE = dr["DHJE"].ToString();
                    }
                    if (bid == 1)
                    {
                        this.bidprice = dr["bidprice"].ToString();
                    }
                    if (bak == 1)
                    {
                        this.bakeprice = dr["bakeprice"].ToString();
                    }
                    if (nolim == 1)
                    {
                        this.Nolimtprice = dr["Nolimtprice"].ToString();
                    }
                    if (nGmp == 1)
                    {
                        this.GMPprice = dr["GMPprice"].ToString();
                    }
                    if (nKey == 1)
                    {
                        this.Keyprice = dr["Keyprice"].ToString();
                    }
                }
                dr.Close();
            }
            if (this.HKJE != "")
            {
                _Sql = "select " + this.HKJE + " from (select distinct PaymentId,PaymentPrice from " + this.Table + " " + strWhere1 + " ) a";
                dr = this.Db.DataReader(_Sql, commandTimeout);
                if (dr.Read())
                {
                    this.HKJE = dr["HKJE"].ToString();
                }
                dr.Close();
            }

            this.m_PageCount = this.RowCount / this.PageSize;
            if ((this.RowCount % this.PageSize) > 0)
            {
                this.m_PageCount++;
            }


            if (this.RowCount == 0)
            {
                this.PageIndex = 0;
            }
            else
            {
                if (this.PageIndex < 1)
                {
                    this.PageIndex = 1;
                }
                else if (this.PageIndex > this.PageCount)
                {
                    this.PageIndex = this.PageCount;
                }
            }

            switch (this.Db.ConnectionType)
            {

                default:

                    if (this.PageIndex == 1)
                    {
                        if (this.GroupHaving == "")
                        {
                            strSql = "Select   Top " + (this.PageSize * this.PageIndex) + " " + this.SelectField + " From " + this.Table + " " + strWhere1 + " Group By " + this.SelectFieldGroup + " Order By " + strOrderBy2;
                        }
                        else
                        {
                            strSql = "Select   Top " + (this.PageSize * this.PageIndex) + " " + this.SelectField + " From " + this.Table + " " + strWhere1 + " Group By " + this.SelectFieldGroup + " Having " + this.GroupHaving + " Order By " + strOrderBy2;
                        }
                    }
                    else if (this.PageIndex == this.PageCount)
                    {
                        if ((this.RowCount % this.PageSize) > 0)
                        {
                            if (this.GroupHaving == "")
                            {
                                strSql = "select * from (Select * from (Select   Top " + (this.RowCount % this.PageSize) + " " + this.SelectField + " From " + this.Table + " " + strWhere1 + " Group By " + this.SelectFieldGroup + "  Order By " + strOrderBy1 + ") a) b order by " + strOrderBy2;
                            }
                            else
                            {
                                strSql = "select * from (Select * from (Select   Top " + (this.RowCount % this.PageSize) + " " + this.SelectField + " From " + this.Table + " " + strWhere1 + " Group By " + this.SelectFieldGroup + " Having " + this.GroupHaving + "  Order By " + strOrderBy1 + ") a) b order by " + strOrderBy2;
                            }
                        }
                        else
                        {
                            if (this.GroupHaving == "")
                            {
                                strSql = "select * from (Select * from (Select   Top " + this.PageSize + " " + this.SelectField + " From " + this.Table + " " + strWhere1 + " Group By " + this.SelectFieldGroup + "  Order By " + strOrderBy1 + ") a) b order by " + strOrderBy2;
                            }
                            else
                            {
                                strSql = "select * from (Select * from (Select   Top " + this.PageSize + " " + this.SelectField + " From " + this.Table + " " + strWhere1 + " Group By " + this.SelectFieldGroup + " Having " + this.GroupHaving + " Order By " + strOrderBy1 + ") a) b order by " + strOrderBy2;
                            }
                        }
                    }
                    else
                    {
                        if (this.GroupHaving == "")
                        {
                            strSql = "select * from (Select top " + this.PageSize + " * from (Select   Top " + (this.PageSize * this.PageIndex) + " " + this.SelectField + " From " + this.Table + " " + strWhere1 + " Group By " + this.SelectFieldGroup + "  Order By " + strOrderBy2 + ") a order by " + strOrderBy1 + ") b order by " + strOrderBy2;
                        }
                        else
                        {
                            strSql = "select * from (Select top " + this.PageSize + " * from (Select   Top " + (this.PageSize * this.PageIndex) + " " + this.SelectField + " From " + this.Table + " " + strWhere1 + " Group By " + this.SelectFieldGroup + " Having " + this.GroupHaving + " Order By " + strOrderBy2 + ") a order by " + strOrderBy1 + ") b order by " + strOrderBy2;
                        }
                    }

                    break;

            }

            m_Sql = strSql;
        }

    }

}