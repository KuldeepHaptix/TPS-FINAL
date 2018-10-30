using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagement
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
            {
                lbl_User.Text = Request.Cookies["LoginCookies"]["User_Name"].ToString();
                //string usertype = Request.Cookies["LoginCookies"]["User_Type"].ToString();
                
                int userid=0;
                int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out userid);
                
            }
            else
            {
                Response.Redirect("~/login.aspx", false);
            }
        }

        protected void Signout_Click(object sender, EventArgs e)
        {
            if (Request.Cookies.AllKeys.Contains("LoginCookies"))
            {
                if (Request.Cookies["LoginCookies"] != null)
                {
                    var user = new HttpCookie("LoginCookies")
                    {
                        Expires = MySQLDB.GetIndianTime().AddDays(-1),
                        Value = null
                    };
                    Response.Cookies.Add(user);
                    Response.Redirect("~/login.aspx", false);
                }
            }
        }

        protected void lnkButton_Click(object sender, EventArgs e)
        {
            try
            {
                string mappath = HttpContext.Current.Server.MapPath("~");
                string[] aa = mappath.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                string path = "";
                for (int i = 0; i < aa.Length - 1; i++)
                {
                    path += aa[i] + '\\';
                }

                string pwd = path + "dontremove.txt";
                StreamReader srk = null;
                string DDBPWD = "";

                srk = new StreamReader(pwd, false);

                string readdata = srk.ReadToEnd();

                string filedate = EncryptionDecryption.EncryptionDecryption.Decrypt(readdata);

                string[] fileline = filedate.Split("\r\n".ToCharArray());
                string test4 = fileline[6].ToString();
                string[] test4split = test4.Split(':');
                DDBPWD = test4split[1].ToString().Trim();

                if (srk != null)
                {
                    srk.Close();
                    srk.Dispose();
                }

                string filepath = path + "VariableDetails.txt";

                string textboxdata1 = "";
                string serevrpath = "";
                string DBBackupPath = "";
                string WinraraFolderPath = "";
                string DbName = "";

                if (File.Exists(filepath))
                {
                    StreamReader sr1 = new StreamReader(filepath);
                    textboxdata1 = sr1.ReadToEnd();
                    sr1.Dispose();
                    sr1.Close();
                    string encrypteddata = EncryptionDecryption.EncryptionDecryption.Decrypt(textboxdata1);
                    string[] splt = encrypteddata.Split(new string[] { "##@@" }, StringSplitOptions.RemoveEmptyEntries);

                    if (splt.Length > 3)
                    {
                        serevrpath = splt[0].Replace("\r\n", ""); ;
                        DBBackupPath = splt[1].Replace("\r\n", ""); ;
                        WinraraFolderPath = splt[2].Replace("\r\n", ""); ;
                        //DbName = splt[3].Replace("\r\n", ""); ;
                    }
                    DbName = "employee_management";

                    if (serevrpath.Trim() != "" && DBBackupPath.Trim() != "" && WinraraFolderPath.Trim() != "" && DbName.Trim() != "")
                    {
                        string textboxdata = "";
                        string dbna = DbName.Trim();
                        dbna = dbna.TrimEnd(',');
                        string[] dbname = dbna.Split(',');
                        if (dbname.Length > 0)
                        {
                            for (int i = 0; i < dbname.Length; i++)
                            {
                                string database = dbname[i].ToString();
                                string filepathbat = path + "DB_Backup.bat";
                                if (File.Exists(filepathbat))
                                {
                                    StreamReader sr = new StreamReader(filepathbat);
                                    textboxdata = sr.ReadToEnd();
                                    sr.Dispose();
                                    sr.Close();
                                    string decrypteddata = EncryptionDecryption.EncryptionDecryption.Decrypt(textboxdata);
                                    string batchdata = decrypteddata;
                                    string driveletter = serevrpath.Trim().Substring(0, 2);
                                    //string batchdata = textboxdata;
                                    batchdata = batchdata.Replace("##MYSQLSERVERPATH##", serevrpath.Trim());
                                    batchdata = batchdata.Replace("##DBBACKUPPATH##", DBBackupPath.Trim());
                                    batchdata = batchdata.Replace("##WINRARFOLDER##", WinraraFolderPath.Trim());
                                    batchdata = batchdata.Replace("##DBNAME##", database);
                                    batchdata = batchdata.Replace("##DBPWD##", DDBPWD);
                                    batchdata = batchdata.Replace("##DRIVE##", driveletter);

                                    string batchfile = path + "execute.bat";

                                    WriteData(batchfile, batchdata);

                                    if (File.Exists(batchfile))
                                    {
                                        //System.Diagnostics.Process.Start(batchfile);
                                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                                        p.StartInfo.FileName = batchfile;
                                        p.Start();
                                        p.WaitForExit();
                                        //System.Threading.Thread.Sleep(2000);

                                        File.Delete(batchfile);
                                    }
                                }
                            }
                        }

                        //Check Backup size
                    }
                }
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                Response.Redirect(url, false);

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Main.Master 165: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }

        }

        public void WriteData(string Filepath, string Data)
        {
            try
            {
                string pathCritical = Filepath;

                StreamWriter wrSMSCritical = new StreamWriter(pathCritical, false);
                wrSMSCritical.WriteLine(Data);
                wrSMSCritical.Close();
                wrSMSCritical.Dispose();
                wrSMSCritical = null;
            }
            catch (Exception ex)
            {
            }
        }
    }
}