using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Linq;


namespace WebApiHBM.Models
{
    public enum DBConnection
    {
        DBErp = 1,
        DBTargeta = 2,
        DBAlmacen = 3,
        DBDigitalizacion = 4,
        DBSitioCNS = 5
    }

    public class DBToolsDinamic
    {
        private DBConnection dBConnection { get; set; }
        public DBConnection ConnectionKey { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Sql { get; set; }
        public string StrcnnUser { get; set; }
        public string StrcnnDefault { get; set; }
        public string ErrorMessage { get; set; }
        TextWriterTraceListener ErrorLog { get; set; }
        public SqlConnection Cnn { get; set; }
        public int RowCount { get; set; }
        public int RowAffected { get; set; }
        public string LastSqlCommand { get; set; }
        public int CmdTimeout { get; set; }
        public enum FormatType
        {
            XML,
            CSV,
            Script
        }

        #region Constructor

        public DBToolsDinamic()
        {
            UserName = (string)HttpContext.Current.Session["userDB"];
            Password = StringUtils.Desencriptar(ConfigurationManager.AppSettings["DefaultPassDBErp"].ToString());
            ConnectionKey = DBConnection.DBErp;
            StrcnnUser = string.Empty;
            StrcnnDefault = string.Empty;
            ErrorMessage = string.Empty;
            ErrorLog = null;
            Cnn = null;
            RowAffected = -1;
            LastSqlCommand = string.Empty;
            this.CmdTimeout = 30;
        }

        public DBToolsDinamic(DBConnection key)
        {
            UserName = (string)HttpContext.Current.Session["userDB"];
            Password = StringUtils.Desencriptar(ConfigurationManager.AppSettings["DefaultPassDBErp"].ToString());
            ConnectionKey = key;
            StrcnnDefault = string.Empty;
            ErrorMessage = string.Empty;
            ErrorLog = null;
            Cnn = null;
            RowAffected = -1;
            LastSqlCommand = string.Empty;
        }

        #endregion

        #region ConnectionStringConfigutation

        private void GetConnectionString()
        {
            try
            {
                switch (ConnectionKey)
                {
                    case DBConnection.DBErp:
                        GetConnectionConfig(DBConnection.DBErp.ToString());
                        break;
                    case DBConnection.DBTargeta:
                        GetConnectionConfig(DBConnection.DBTargeta.ToString());
                        break;
                    case DBConnection.DBAlmacen:
                        GetConnectionConfig(DBConnection.DBAlmacen.ToString());
                        break;
                    case DBConnection.DBDigitalizacion:
                        GetConnectionConfig(DBConnection.DBDigitalizacion.ToString());
                        break;
                    case DBConnection.DBSitioCNS:
                        GetConnectionConfig(DBConnection.DBSitioCNS.ToString());
                        break;
                    default:
                        GetConnectionConfig(DBConnection.DBErp.ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            string errorLogPath = string.Format("{0}{1}.log", ConfigurationManager.AppSettings["PathErrorLog"].ToString(), DateTime.Now.ToString("ddMMyyyy"));
            ErrorLog = new TextWriterTraceListener(errorLogPath);
            Trace.Listeners.Clear();
            Trace.Listeners.Add(ErrorLog);
            Trace.AutoFlush = true;
        }

        private void GetConnectionConfig(string key)
        {
            string[] connString = ConfigurationManager.ConnectionStrings[key].ToString().Split(';');
            StrcnnUser = string.Format("{0};{1};{2};{3};{4}", connString[0], connString[1], connString[2], string.Format("User ID={0}", UserName), string.Format("Password={0}", StringUtils.Desencriptar(ConfigurationManager.AppSettings["DefaultPassDBErp"].ToString())));
            /* if ((ConfigurationManager.AppSettings["BuildVersion"].Equals(""))) 
             {
                 connString[4] = string.Format("Password={0}", StringUtils.Desencriptar(ConfigurationManager.AppSettings[string.Format("access{0}", key)].ToString()));
             }*/
            StrcnnDefault = string.Format("{0};{1};{2};{3};{4}", connString[0], connString[1], connString[2], connString[3], connString[4]);
        }

        #endregion

        #region Querys

        public DataTable GetQuery()
        {
            DataTable dt = new DataTable();
            try
            {
                GetConnectionString();
                SqlConnection cnnUser = new SqlConnection(StrcnnUser);
                SqlConnection cnnDef = new SqlConnection(StrcnnDefault);
                try
                {
                    if (AllowDDLCommands(Sql))
                    {
                        AllowDMLCommands(Sql);
                        try
                        {
                            Cnn = cnnUser;
                            Cnn.Open();
                        }
                        catch (Exception)
                        {
                            Cnn = cnnDef;
                            Cnn.Open();
                        }
                        SqlCommand cmd = new SqlCommand(Sql, Cnn);
                        cmd.CommandTimeout = this.CmdTimeout;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
                catch (SqlException ex)
                {
                    ErrorMessage = ex.Message;
                    WriteToLog(ex);
                }
                finally
                {
                    Cnn.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                WriteToLog(ex);
            }
            return dt;
        }

        public int GetValorIdentity(string nombreTabla)
        {
            DataTable dt = new DataTable();
            ErrorMessage = string.Empty;
            string sql = string.Format("SELECT IDENT_CURRENT('{0}') as ValorIdentity", nombreTabla);
            int ValorIdentity = -1;
            try
            {
                GetConnectionString();
                SqlConnection cnnUser = new SqlConnection(StrcnnUser);
                SqlConnection cnnDef = new SqlConnection(StrcnnDefault);
                try
                {
                    try
                    {
                        Cnn = cnnUser;
                        Cnn.Open();
                    }
                    catch (Exception)
                    {
                        Cnn = cnnDef;
                        Cnn.Open();
                    }
                    SqlCommand cmd = new SqlCommand(Sql, Cnn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    ValorIdentity = Convert.ToInt32(dt.Rows[0]["ValorIdentity"]);
                }
                catch (SqlException ex)
                {
                    ErrorMessage = ex.Message;
                    WriteToLog(ex);
                }
                finally
                {
                    Cnn.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                WriteToLog(ex);
            }
            return ValorIdentity;
        }

        public DataTable GetQueryCount(string columnCount)
        {
            string sqlAux = Sql;
            Sql = Sql.ToUpper();
            int ord = (Sql.IndexOf(" ORDER BY ") > 0) ? Sql.IndexOf(" ORDER BY ") : Sql.Length;
            int leng = ord - Sql.IndexOf(" FROM ");
            Sql = Sql.Substring(Sql.IndexOf(" FROM "), leng);
            Sql = string.Format("SELECT COUNT({0}) as ColCount {1}", columnCount, Sql);
            DataTable dt = GetQuery();
            RowCount = (dt.Rows.Count > 0) ? Convert.ToInt32(dt.Rows[0]["ColCount"]) : 0;
            Sql = sqlAux;
            return GetQuery();
        }

        public void ExecuteQuery()
        {
            string[] queries = { Sql };
            ExecuteQueries(queries);
        }

        private void ExecuteQueries(string[] queries)
        {
            //string LastSqlCommand = string.Empty;
            try
            {
                GetConnectionString();
                SqlConnection cnnUser = new SqlConnection(StrcnnUser);
                SqlConnection cnnDef = new SqlConnection(StrcnnDefault);
                SqlCommand cmd = new SqlCommand();
                try
                {
                    try
                    {
                        Cnn = cnnUser;
                        Cnn.Open();
                    }
                    catch (Exception)
                    {
                        Cnn = cnnDef;
                        Cnn.Open();
                    }

                    RowAffected = 0;
                    foreach (string query in queries)
                    {
                        if (AllowDDLCommands(query))
                        {
                            AllowDMLCommands(query);
                            LastSqlCommand += query;
                            cmd.CommandText = query;
                            cmd.Connection = Cnn;
                            RowAffected += cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    ErrorMessage = ex.Message;
                    WriteToLog(ex);
                    throw;
                }
                finally
                {
                    Cnn.Close();
                    Cnn.Dispose();
                }
            }
            catch (Exception ex)
            {
                WriteToLog(ex);
                throw;
            }
        }

        public void ExecuteQuery(List<SqlParameter> parametros)
        {
            try
            {
                GetConnectionString();
                SqlConnection cnnUser = new SqlConnection(StrcnnUser);
                SqlConnection cnnDef = new SqlConnection(StrcnnDefault);
                SqlCommand cmd = new SqlCommand();
                try
                {
                    if (AllowDDLCommands(Sql))
                    {
                        AllowDMLCommands(Sql);
                        try
                        {
                            Cnn = cnnUser;
                            Cnn.Open();
                        }
                        catch (Exception)
                        {
                            Cnn = cnnDef;
                            Cnn.Open();
                        }
                        cmd.CommandText = Sql;
                        cmd.Parameters.AddRange(parametros.ToArray());
                        cmd.Connection = Cnn;
                        cmd.ExecuteNonQuery();
                        RowAffected = 1;
                    }
                }
                catch (SqlException ex)
                {
                    ErrorMessage = ex.Message;
                    WriteToLog(ex);
                    throw;
                }
                finally
                {
                    Cnn.Close();
                    Cnn.Dispose();
                }
            }
            catch (Exception ex)
            {
                WriteToLog(ex);
                throw;
            }
        }

        public int ExecuteQueriesGetId()
        {
            LastSqlCommand = Sql;
            if (!Sql.Contains(";@@IDENTITY"))
            {
                Sql += "; SELECT @@IDENTITY";
            }
            try
            {
                GetConnectionString();
                SqlConnection cnnUser = new SqlConnection(StrcnnUser);
                SqlConnection cnnDef = new SqlConnection(StrcnnDefault);
                try
                {
                    try
                    {
                        Cnn = cnnUser;
                        Cnn.Open();
                    }
                    catch (Exception err)
                    {
                        Cnn = cnnDef;
                        Cnn.Open();
                    }
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = Sql;
                    cmd.Connection = Cnn;
                    object o = cmd.ExecuteScalar();
                    return Convert.ToInt32(o);

                }
                catch (SqlException ex)
                {
                    ErrorMessage = ex.Message; WriteToLog(ex);
                    throw;
                }
                finally
                {
                    Cnn.Close();
                    Cnn.Dispose();
                }
            }
            catch (Exception ex)
            {
                WriteToLog(ex);
                throw;
            }
        }

        #endregion

        #region Log's

        private static void AllowDMLCommands(string sql)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["AllowDMLCommands"]))
            {
                string dmlCommands = "INSERT,UPDATE,DELETE";
                string[] commands = dmlCommands.Split(',');
                foreach (string cmd in commands)
                {
                    commands = dmlCommands.Split(',');
                    if (sql.ToUpper().IndexOf(cmd) > -1)
                    {
                        WriteLogFile(sql, "DMLCommands");
                    }
                }
            }
        }

        private static bool AllowDDLCommands(string sql)
        {
            bool resp = true;
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["AllowDDLCommands"]))
            {
                string[] sentencias = sql.Split(';');
                string r2, r3, a;
                string ddlCommands = "CREATE ,ALTER ,TRUNCATE ,COMMENT ,RENAME ,MERGE ,CALL ,LOCK TABLE ,GRANT ,REVOKE ,COMMIT ,SAVEPOINT ,ROLLBACK ,SET TRANSACTION ,DROP ";
                string[] commands = ddlCommands.Split(',');
                foreach (string st in sentencias)
                {
                    foreach (string cmd in commands)
                    {
                        if (st.ToUpper().IndexOf(cmd) > -1)
                        {
                            string cadenaTexto = st.ToUpper();
                            r2 = string.Empty; r3 = string.Empty;
                            r2 = cadenaTexto.Substring(0, cadenaTexto.IndexOf(cmd) - 1);
                            r3 = cadenaTexto.Substring(r2.Length, cmd.Length + 1);
                            a = r3.TrimStart();
                            if (a == cmd)
                            {
                                WriteLogFile(sql, "DDLCommands");
                                resp = false;
                            }
                        }
                    }
                }
            }
            return resp;
        }

        private static void WriteLogFile(string sql, string name)
        {
            string userDB = (string)HttpContext.Current.Session["userDB"];
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            string logLine = string.Format("{0:yyyyMMddhhmmss}|{1}|{2}|{3}", DateTime.Now, VisitorsIPAddr, userDB, sql);
            string path = ConfigurationManager.AppSettings["DownloadERP"];
            string fileName = string.Format("{0}\\LOGs\\{1} {2:dd-MM-yyyy}.txt", path, name, DateTime.Now.Date);
            StreamWriter writer = File.AppendText(fileName);
            writer.WriteLine(logLine);
            writer.Close();

        }

        #endregion

        #region WriteLog's

        private void WriteToLog(Exception ex)
        {
            string[] removedItems = { "\n", "\r\n" };
            string[] stackTrace = ex.StackTrace.Split(removedItems, StringSplitOptions.RemoveEmptyEntries);
            Trace.WriteLine(string.Format("{0} {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString()));
            Trace.WriteLine(string.Format("Message:{0}", ex.Message));
            Trace.WriteLine(string.Format("ConnectionString:{0}", StrcnnDefault));
            Trace.WriteLine(string.Format("StackTrace:{0}\n", stackTrace[stackTrace.Length - 1].Trim()));
            Trace.Close();
        }

        #endregion

        #region More

        public static Double GetMaxValueInt(DataTable dt, string fieldname)
        {
            int max = -1;
            List<int> list = (from row in dt.AsEnumerable() select row.Field<int>(fieldname)).ToList<int>();
            max = list.Max();
            return max;
        }

        #endregion

    }
}
