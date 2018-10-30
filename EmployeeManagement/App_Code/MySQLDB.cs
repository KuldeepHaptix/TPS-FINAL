using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace EmployeeManagement
{
    public class MySQLDB
    {
        MySqlConnection objcon = null;
        MySqlCommand objcmd = null;
        MySqlTransaction objtrans = null;
        MySqlDataAdapter mysqladap = null;
        //public const string MySqlConnectionStringCircular = "Server=localhost;Database=employee_management;Uid=root;Pwd=####";
        public void ConnectToDatabase()
        {
            try
            {
                if (objcon == null)
                {
                    string pwd = "MySQL@2018";
                     string MySqlConnectionStringCircular = "Server=localhost;Database=employee_management;Uid=root;Pwd="+pwd+"";
                    objcon = new MySqlConnection(MySqlConnectionStringCircular);
                    objcmd = new MySqlCommand();
                    objcmd.Connection = objcon;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string getMySQLPwd()
        {
            string mappath = HttpContext.Current.Server.MapPath("~");
            string[] aa = mappath.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            string path = ""; //DBPath + "\\dontremove.txt";
            for (int i = 0; i < aa.Length - 1; i++)
            {
                path += aa[i] + '\\';
            }
            path += "dontremove.txt";
            StreamReader srk = null;
            string DDBPWD = "";
            try
            {
                srk = new StreamReader(path, false);

                string readdata = srk.ReadToEnd();

                string filedate = EncryptionDecryption.EncryptionDecryption.Decrypt(readdata);

                string[] fileline = filedate.Split("\r\n".ToCharArray());
                string test4 = fileline[6].ToString();
                string[] test4split = test4.Split(':');
                DDBPWD = test4split[1].ToString().Trim();


            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("MySQLDB 58: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                if (srk != null)
                {
                    srk.Close();
                    srk.Dispose();
                }
            }
            return DDBPWD;
        }
        public void OpenSQlConnection()
        {
            try
            {
                if (objcon.State != ConnectionState.Open)
                {
                    objcon.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CloseSQlConnection()
        {
            try
            {
                if (objcmd != null) objcmd.Dispose();
                if (objcon != null) objcon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetData(string SelectStatement)
        {
            try
            {
                DataTable dt = new DataTable();
                if (objcmd == null) objcmd = new MySqlCommand();
                //objcmd.Connection = objcon;
                objcmd.CommandText = SelectStatement;
                mysqladap = new MySqlDataAdapter(objcmd);
                mysqladap.Fill(dt);
                objcmd.Parameters.Clear();
                objcmd.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object ExecuteSQLScaler(string sqlStatement)
        {
            object obj = null;
            try
            {
                if (objcmd == null) objcmd = new MySqlCommand();
                //objcmd.Connection = objcon;
                objcmd.CommandText = sqlStatement;
                obj = objcmd.ExecuteScalar();
                objcmd.Parameters.Clear();
                objcmd.Dispose();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MySqlDataReader ExecuteSQLReader(string sqlStatement)
        {
            try
            {
                if (objcmd == null) objcmd = new MySqlCommand();
                //objcmd.Connection = objcon;
                objcmd.CommandText = sqlStatement;
                MySqlDataReader objMySqlReader = objcmd.ExecuteReader();
                objcmd.Parameters.Clear();
                objcmd.Dispose();
                return objMySqlReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertUpdateDeleteData(string sqlStatement)
        {
            try
            {
                if (objcmd == null) objcmd = new MySqlCommand();
                // objcmd.Connection = objcon;
                objcmd.CommandText = sqlStatement;
                //objcmd.CommandTimeout = 120;
                int i = objcmd.ExecuteNonQuery();
                objcmd.Parameters.Clear();
                objcmd.Dispose();
                return i;
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("MySQLDB InsertUpdateDeleteData() 143 sqlstatement:" + sqlStatement + "\n \nException:" + ex.Message + ":::::\n\n" + ex.StackTrace);
                throw ex;
            }
        }

        public int UpdateTableStructure(string sqlStatement)
        {
            try
            {
                if (objcmd == null) objcmd = new MySqlCommand();
                // objcmd.Connection = objcon;
                objcmd.CommandText = sqlStatement;
                int i = objcmd.ExecuteNonQuery();
                objcmd.Parameters.Clear();
                objcmd.Dispose();
                return i;
            }
            catch (Exception ex)
            {
                //Logger.WriteCriticalLog("MySQLDB InsertUpdateDeleteData() 143 sqlstatement:+" + sqlStatement + "::::::::" + ex.Message + ":::::" + ex.StackTrace);
                return 0;
            }
        }

        public void AddCommandParameter(string ParamName, object ParamValue)
        {
            try
            {
                objcmd.Parameters.Add(new MySqlParameter("?" + ParamName, ParamValue));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddCommandParameter(string ParamName, MySqlDbType MySqlType, object ParamValue)
        {
            try
            {
                objcmd.Parameters.Add(new MySqlParameter("?" + ParamName, MySqlType).Value = ParamValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BeginSQLTransaction()
        {
            try
            {
                objtrans = objcon.BeginTransaction();
                objcmd.Transaction = objtrans;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RollBackSQLTransaction()
        {
            try
            {
                objtrans.Rollback();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EndSQLTransaction()
        {
            try
            {
                objtrans.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetMySqlConnectionString()
        {
            try
            {
                string conStr = System.Configuration.ConfigurationManager.AppSettings["SMSService"].ToString();
                return conStr;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void disposeConnectionObj()
        {
            try
            {
                if (objcon != null)
                    objcon.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CopyDateToDateTicks(string tablename, string datecol, string datetickscol, string uniquecol)
        {
            int j = 0;
            try
            {
                InsertUpdateDeleteData("ALTER TABLE " + tablename + " ADD COLUMN " + datetickscol + " DECIMAL(28) NULL");
                DataTable dt = GetData("select " + datecol + "," + uniquecol + " from " + tablename + "");
                if (dt != null)
                {
                    long dateticks = 0;
                    DateTime datefrommysql;
                    string[] arrdate;
                    string[] arrtime;
                    string[] splittime;
                    string querybuilding = "";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (!dt.Rows[i][datecol].ToString().Trim().Equals(string.Empty))
                        {
                            int uniqecolid = int.Parse(dt.Rows[i][uniquecol].ToString());
                            string datefromtemp = dt.Rows[i][datecol].ToString().Trim();
                            try
                            {
                                datefrommysql = DateTime.ParseExact(dt.Rows[i][datecol].ToString().Trim(), "dd/MM/yyyy H:mm:ss tt", System.Globalization.CultureInfo.InstalledUICulture);
                                dateticks = datefrommysql.Ticks;
                            }
                            catch (Exception er)
                            {

                                try
                                {
                                    datefrommysql = DateTime.ParseExact(dt.Rows[i][datecol].ToString().Trim(), "dd/MM/yyyy", System.Globalization.CultureInfo.InstalledUICulture);
                                    dateticks = datefrommysql.Ticks;
                                }
                                catch (Exception err)
                                {
                                    DateTime nonISD = DateTime.Now;

                                    //Change Time zone to ISD timezone
                                    TimeZoneInfo myTZ = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                                    DateTime ISDTime = TimeZoneInfo.ConvertTime(nonISD, TimeZoneInfo.Local, myTZ);

                                    dateticks = ISDTime.Ticks;

                                }

                            }

                            //querybuilding += "update " + tablename + " set " + datetickscol + "=" + dateticks + " where " + uniquecol + "=" + uniqecolid + ";";
                            sb.Append("update " + tablename + " set " + datetickscol + "=" + dateticks + " where " + uniquecol + "=" + uniqecolid + ";");
                        }
                    }
                    //querybuilding = querybuilding.TrimEnd(',');
                    if (sb.Length > 0)
                    {
                        InsertUpdateDeleteData(sb.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
            return j;
        }

        public DataTable ConvertTicksToDateInDataTable(DataTable dt, string[] tickscol, string[] datecol)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (tickscol.Length == datecol.Length)
                    {
                        long longticks = 0;
                        DateTime datefromticks;
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            for (int l = 0; l < tickscol.Length; l++)
                            {
                                if (!dt.Columns.Contains(datecol[l]))
                                {
                                    DataColumn dc = new DataColumn(datecol[l]);
                                    dt.Columns.Add(dc);
                                }
                                if (!string.IsNullOrEmpty(dt.Rows[j][tickscol[l]].ToString()))
                                {
                                    longticks = long.Parse(dt.Rows[j][tickscol[l]].ToString());
                                    datefromticks = new DateTime(longticks);
                                    dt.Rows[j][datecol[l]] = datefromticks.ToString();
                                }

                            }
                        }
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string getIndiantime()
        {
            try
            {
                DateTime nonISD = DateTime.Now;

                //Change Time zone to ISD timezone
                TimeZoneInfo myTZ = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime ISDTime = TimeZoneInfo.ConvertTime(nonISD, TimeZoneInfo.Local, myTZ);
                string temp = ISDTime.ToString();
                return temp;
            }
            catch (Exception ex)
            {
                return DateTime.Now.ToString();
            }

        }
        public static DateTime GetIndianTime()
        {
            try
            {
                DateTime nonISD = DateTime.Now;

                //Change Time zone to ISD timezone
                TimeZoneInfo myTZ = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                //DateTime ISDTime = TimeZoneInfo.ConvertTime(nonISD, TimeZoneInfo.Local, myTZ);
                DateTime ISDTime = TimeZoneInfo.ConvertTime(nonISD, myTZ);
                //ISDTime = DateTime.ParseExact(ISDTime,"dd/MM/yyyy",System.Globalization.CultureInfo.InvariantCulture);
                return ISDTime;
            }
            catch (Exception ex)
            {

                return DateTime.Now;
            }
        }

        public long LastNo(string tableName, string colName, long ModeifyTime)
        {
            long rowid = 0;
            try
            {
                DataTable dt = GetData("Select " + colName + " from " + tableName + " where modify_datetime=" + ModeifyTime + " limit 1");
                if (dt != null && dt.Rows.Count > 0)
                {
                    long.TryParse(dt.Rows[0][colName].ToString(), out rowid);
                }
            }
            catch (Exception aa)
            {
                rowid = 0;
            }
            return rowid;
        }
    }
}