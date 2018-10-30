using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace EmployeeManagement
{
    public partial class forgotpassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string emailid = "";
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
                emailid = tb_UserName.Value.ToString();
                if (emailid != "")
                {
                   
                    string qr = "select user_id as User_id,user_nm as User_Name,password as User_Password from User_master where email='" + emailid + "'";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = qr;
                    cmd.Connection = con;
                    con.Open();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string body = "";
                        //string username = dt.Rows[0]["Reseller_Name"].ToString();
                        string Password = dt.Rows[0]["User_Password"].ToString();
                        string User_Name = dt.Rows[0]["User_Name"].ToString();
                        body = "Hello <b>" + User_Name + "</b>,<br/><br/> Your Login Details are mentioned below:<br /><br/> User Name: <b>" + User_Name + "</b> <br/><br /> Password :<b>" + Password + "</b><br /><br /><br/> Thank You\n";
                       Logger.SendForgotPwdEmail(emailid, "Registered User Details", body);
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Login Details has been Sent to your mail id.)", true);
                        //return;
                        lblmsg.Text = "Login Details has been Sent to your mail id";
                        Response.Redirect("Login.aspx", false);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Details for this E-Mail ID Doesn't Exist.Enter Valid E-Mail ID.')", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ForgotPassword " + emailid + ": exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                con.Close();
            }
        }
    }
}