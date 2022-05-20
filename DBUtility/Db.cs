using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace WF_Calc.DBUtility {
    public class Db
    {
        private System.Data.IDataReader Idr;

        /// <summary>
        /// ��ʾ��Ĭ��ϵͳ���ݿ��һ���򿪵����ӡ�
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

            //    throw new System.Exception("���ݿ����������ļ� " + ConnectionConfigFile + " ������");
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
        /// ��ʾ��ָ�����������ݿ��һ���򿪵����ӡ�
        /// </summary>
        /// <param name="connectionString">
        /// ���ڴ�ϵͳ���ݿ���ַ���
        /// ��SQL��"Server=��������ַ;uid=�ʺ�;pwd=����;database=���ݿ���"��"Server=��������ַ;Integrated Security=SSPI;database=���ݿ���"��
        /// ��OLEDB Access��"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=���ݿ�·��;Jet OLEDB:database password=����"��
        /// ��OLEDB Oracle��"Provider=MSDAORA; Data Source=ORACLE8i7;Persist Security Info=False;Integrated Security=yes"��
        /// ��ODBC Sql Server��"Driver={SQL Server};Server=MyServer;Trusted_Connection=yes;Database=���ݿ���;"��
        /// ��ORACLE��"Data Source=Oracle8i;Integrated Security=yes"��
        /// </param>
        /// <param name="connectionType">���򿪵�ϵͳ���ݿ�����ݿ�����</param>
        public Db(string connectionString, ConnectType connectionType)
        {
            this.m_ConnectionString = connectionString;
            this.m_ConnectionType = connectionType;

            OpenDb();
        }

        //�����ݿ�
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
        /// ��ʾ������Դ�����Ӷ���
        /// </summary>
        public System.Data.IDbConnection DbConnection
        {
            get { return m_DbConnection; }
        }

        //�����������ͷ������Ӷ���
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
        /// �����ַ���
        /// </summary>
        public string ConnectionString
        {
            get { return m_ConnectionString; }
        }

        private ConnectType m_ConnectionType;
        /// <summary>
        /// ��������
        /// </summary>
        public ConnectType ConnectionType
        {
            get { return m_ConnectionType; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public enum ConnectType
        {
            Sql,
            OleDb,
            Odbc
            //Oracle
        }

        /// <summary>
        /// ִ�� SQL ��ѯ�������� DataReader ����
        /// </summary>
        /// <param name="sql">��Ҫִ�е� SQL ���</param>
        /// <returns></returns>
        public System.Data.IDataReader DataReader(string sql)
        {
            return DataReader(sql, 30);
        }

        /// <summary>
        /// ִ�� SQL ��ѯ�������� DataReader ����
        /// </summary>
        /// <param name="sql">��Ҫִ�е� SQL ���</param>
        /// <param name="CommandTimeout">����ִֹ������ĳ��Բ����ɴ���֮ǰ�ĵȴ�ʱ��(��)��</param>
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
        /// ִ�� SQL ��䣬��������Ӱ�������
        /// </summary>
        /// <param name="sql">��Ҫִ�е� SQL ���</param>
        /// <returns></returns>
        public int Command(string sql)
        {
            return Command(sql, 30);
        }

        /// <summary>
        /// ִ�� SQL ��䣬��������Ӱ�������
        /// </summary>
        /// <param name="sql">��Ҫִ�е� SQL ���</param>
        /// <param name="commandTimeout">����ִֹ������ĳ��Բ����ɴ���֮ǰ�ĵȴ�ʱ��(��)��</param>
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
        /// ִ�� SQL ��ѯ�������� DataTable ����
        /// </summary>
        /// <param name="sql">��Ҫִ�е� SQL ���</param>
        /// <returns></returns>

        public System.Data.DataTable DataTable(string sql)
        {
            return DataTable(sql, 30);
        }

        /// <summary>
        /// ִ�� SQL ��ѯ�������� DataTable ����
        /// </summary>
        /// <param name="sql">��Ҫִ�е� SQL ���</param>
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
        /// ��ȡ IDbCommand ����ͨ���ڲ����������������ʱ�� DbDataParameter ���ʹ�á�
        /// </summary>
        /// <returns></returns>
        public System.Data.IDbCommand DbCommand()
        {
            return DbCommand("", 30);
        }

        /// <summary>
        /// ��ȡ IDbCommand ����ͨ���ڲ����������������ʱ�� DbDataParameter ���ʹ�á�
        /// </summary>
        /// <param name="sql">��Ҫִ�е� SQL ���</param>
        /// <returns></returns>
        public System.Data.IDbCommand DbCommand(string sql)
        {
            return DbCommand(sql, 30);
        }

        /// <summary>
        /// ��ȡ IDbCommand ����ͨ���ڲ����������������ʱ�� DbDataParameter ���ʹ�á�
        /// </summary>
        /// <param name="sql">��Ҫִ�е� SQL ���</param>
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
        /// ��ȡ����������������͵Ľӿڡ�ͨ����Ҫ�� DbCommand ���ʹ�á�
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
        /// �ر����ݿ�����
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
        /// �������ݿ�������Ϣ����������Ϣ�������ڡ�ConnectionConfigFile������ָ���������ļ��ڣ����û�������ļ�����Ĭ�����ӷ�ʽ�������ӡ�
        /// </summary>
        /// <param name="connectionString">���ݿ������ַ���</param>
        /// <param name="connectionType">���ݿ���������</param>
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
        /// ����ʱ��¼������Ϣ
        /// </summary>
        /// <param name="ConnectionString">����ʱ�����ݿ������ַ���</param>
        /// <param name="sql">����ʱ����ִ�е� SQL ���</param>
        /// <param name="exMessage">������Ϣ</param>
        public void ErrorLog(string sql, string exMessage)
        {
            try
            {
                //�˴�������ӳ���ʱ��¼������Ϣ�Ĵ���

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
        /// ��ҳ����
        /// </summary>
        /// <param name="PageIndex">Ҫת����ҳ��</param>
        /// <param name="PageSize">ÿһҳ�ļ�¼����</param>
        /// <param name="TableName">����</param>
        /// <param name="mKey">��������</param>
        /// <param name="strFields">Ҫ��ʾ���ֶ���</param>
        /// <param name="strConditions">��ѯ�������ô� where </param>
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
        /// ��ʾ������Դ������
        /// </summary>
        public Db Db
        {
            get { return m_Db; }
            set { m_Db = value; }
        }

        private string m_Table;
        /// <summary>
        /// ��ѯ�ı������� Table1 Ҳ������ Table1,Table2
        /// </summary>
        public string Table
        {
            get { return m_Table; }
            set { m_Table = value; }
        }

        private string m_TableGroup;
        /// <summary>
        /// ͳ�Ƶı��� ���磺left join BuyPlanGoods on BuyPlanGoods.AccountId_PS= SupplyCompany.AccountId_PS 
        /// </summary>
        public string TableGroup
        {
            get { return m_TableGroup; }
            set { m_TableGroup = value; }
        }

        private string m_Where = "";
        /// <summary>
        /// ��ѯ�������� FieldName1=123 ���� Table1.FieldName=Table2.FieldName And Table1.FieldName2=Table2.FieldName2 Ҳ����Ϊ��û�в�ѯ����
        /// </summary>
        public string Where
        {
            get { return m_Where; }
            set { m_Where = value; }
        }

        private string m_Key;
        /// <summary>
        /// �����ֶΡ������Բ���Ϊ�գ���ֻ����һ��Ψһ��������
        /// </summary>
        public string Key
        {
            get { return m_Key; }
            set { m_Key = value; }
        }

        private string m_OrderBy = "";
        /// <summary>
        /// �����ֶΡ�������ֶβ�������Ĭ��Ϊ��Key�������ֶ�
        /// </summary>
        public string OrderBy
        {
            get { return m_OrderBy; }
            set { m_OrderBy = value; }
        }

        private bool m_OrderByIsDesc = false;
        /// <summary>
        /// �Ƿ��Ե�������Ĭ��Ϊ��������
        /// </summary>
        public bool OrderByIsDesc
        {
            get { return m_OrderByIsDesc; }
            set { m_OrderByIsDesc = value; }
        }

        private string m_SelectFieldGroup = "*";
        /// <summary>
        /// Ҫͳ�Ƶ��ֶΣ���Group By ������ֶ�
        /// </summary>
        public string SelectFieldGroup
        {
            get { return m_SelectFieldGroup; }
            set { m_SelectFieldGroup = value; }
        }

        private string m_DGJE = "";
        /// <summary>
        /// Ҫͳ�Ƶ��ֶΣ���Group By ������ֶ� �������
        /// </summary>
        public string DGJE
        {
            get { return m_DGJE; }
            set { m_DGJE = value; }
        }

        private string m_DHJE = "";
        /// <summary>
        /// Ҫͳ�Ƶ��ֶΣ���Group By ������ֶ� �������������
        /// </summary>
        public string DHJE
        {
            get { return m_DHJE; }
            set { m_DHJE = value; }
        }
        private string m_HKJE = "";
        /// <summary>
        /// Ҫͳ�Ƶ��ֶ� �ؿ���
        /// </summary>
        public string HKJE
        {
            get { return m_HKJE; }
            set { m_HKJE = value; }
        }

        private string m_bidprice = "";
        /// <summary>
        /// Ҫͳ�Ƶ��ֶ� �б���
        /// </summary>
        public string bidprice
        {
            get { return m_bidprice; }
            set { m_bidprice = value; }
        }

        private string m_bakeprice = "";
        /// <summary>
        /// Ҫͳ�Ƶ��ֶ� ���вɹ����
        /// </summary>
        public string bakeprice
        {
            get { return m_bakeprice; }
            set { m_bakeprice = value; }
        }

        private string m_Nolimtprice = "";
        /// <summary>
        /// Ҫͳ�Ƶ��ֶ� 22�ֽ���ҩƷ
        /// </summary>
        public string Nolimtprice
        {
            get { return m_Nolimtprice; }
            set { m_Nolimtprice = value; }
        }

        private string m_GMPprice = "";
        /// <summary>
        /// Ҫͳ�Ƶ��ֶ� ��Ѫ����
        /// </summary>
        public string GMPprice
        {
            get { return m_GMPprice; }
            set { m_GMPprice = value; }
        }

        private string m_Keyprice = "";
        /// <summary>
        /// Ҫͳ�Ƶ��ֶ� �ص���
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
        /// ѡ���¼��������ֶΣ������������Ϊ��*�����磺��*�����ߡ�FieldName1�����ߡ�Table1.FieldName , Table2.FieldName As FieldName3����
        /// </summary>
        public string SelectField
        {
            get { return m_SelectField; }
            set { m_SelectField = value; }
        }


        private int m_PageSize = 20;
        /// <summary>
        /// ����ÿҳ��ʾ��������¼
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
        /// ���û��ȡ��ǰ��ҳ��
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
        /// ��ȡ��ҳ��
        /// </summary>
        public int PageCount
        {
            get { return m_PageCount; }
        }

        private int m_RowCount;
        /// <summary>
        /// �ܵļ�¼��
        /// </summary>
        public int RowCount
        {
            get { return m_RowCount; }
        }

        /// <summary>
        /// ִ�з�ҳ��ѯ��
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

            string strWhere1 = "";                                  //���ڵ�1�� Top ɸѡ
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
            //��keyֵͳ����Ŀ ��sql������distinct ͳ��ʱ���д�����
            //�����ж�selectField��ֵ���8λ�Ƿ�Ϊdistinct,����selectfield����Ϊ��ѯ����,����ȡ����

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
        /// ִ�з�ҳ��ѯ����ָ����ѯ��ʱʱ�䡣
        /// </summary>
        /// <param name="CommandTimeout">����ִֹ������ĳ��Բ����ɴ���֮ǰ�ĵȴ�ʱ��(��)��</param>
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
        /// ���β�ѯ���õ��� SQL ��䡣
        /// </summary>
        private string m_Sql;

        public string Sql
        {
            get { return m_Sql; }
        }


        /// <summary>
        /// ���ҳ�����ӣ����� BidTableProductSelect.aspx?PG={PageNum} ���� {PageNum} ָ�滻Ϊҳ��ı��
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetPageLink(string url)
        {
            string strValue = "";

            if (this.PageIndex > 1)
            {
                strValue += "<a href='" + url.Replace("{PageNum}", SysFun.ToTrim(this.PageIndex - 1)) + "'>��һҳ</a> ";
            }
            else
            {
                strValue += "<font color='#C0C0C0'>��һҳ</font> ";
            }

            if (this.PageIndex < this.PageCount)
            {
                strValue += "<a href='" + url.Replace("{PageNum}", SysFun.ToTrim(this.PageIndex + 1)) + "'>��һҳ</a> ";
            }
            else
            {
                strValue += "<font color='#C0C0C0'>��һҳ</font> ";
            }

            strValue += "�۵�ǰ�� " + this.PageIndex.ToString() + " ҳ���� " + this.PageCount.ToString() + " ҳ���� " + this.RowCount + " ���� ";

            strValue += "ת����";
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
            strValue += "ҳ";
            return strValue;
        }
        private const string GroupCompanyPSByWhere = @"Select  Company.accountid,Company.CompanyName,Company.UserCode ,convert(numeric(20,2),sum(GoodsNum*TradePrice)) as DGJE ,convert(numeric(20,2),sum(inflownum*TradePrice)) as DHJE 
                        From SupplyCompany inner join Company on SupplyCompany.AccountId_PS=Company.AccountId 
                        inner join BuyPlanGoods on BuyPlanGoods.AccountId_PS= SupplyCompany.AccountId_PS and BuyPlanGoods.ProjectId=SupplyCompany.ProjectId 
                         where {0} Group By Company.accountid,Company.CompanyName,Company.UserCode  Order By Company.CompanyName";
        /// <summary>
        /// ͳ��������ҵ�����ͽ��
        /// </summary>
        /// <param name="CompanyNamePS">������ҵ����</param>
        /// <param name="UserCodePS">������ҵ����</param>
        /// <param name="StartDate">��ʼʱ�䣨ҽԺȷ�ϵ�����</param>
        /// <param name="EndDate">����ʱ�䣨ҽԺȷ�ϵ�����</param>
        /// <param name="AreaId">��ǰ��½�û��ĵ�������</param>
        /// <returns></returns>
        public System.Data.IDataReader ExecuteReaderGroupCompanyPSByWhere(string CompanyNamePS, string UserCodePS, string StartDate, string EndDate, int AreaId)
        {
            string _Where = "";

            m_Sql = String.Format(GroupCompanyPSByWhere, _Where);
            return this.Db.DataReader(this.Sql, 120);
        }
        /// <summary>
        /// ִ�з�ҳ��ѯ����ָ����ѯ��ʱʱ�䡣
        /// </summary>
        /// <param name="CommandTimeout">����ִֹ������ĳ��Բ����ɴ���֮ǰ�ĵȴ�ʱ��(��)��</param>
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

            string strWhere1 = "";                                  //���ڵ�1�� Top ɸѡ
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