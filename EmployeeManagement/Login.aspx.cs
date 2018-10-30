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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                    {
                        Response.Redirect("~/Search_Employee.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Login 27: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
       
        protected void Login_Click(object sender, EventArgs e)
        {
            lblmsg.Text = "";
            string username = tb_UserName.Value.ToLower().ToString().Trim();
            string pwd = tb_password.Value.ToString().Trim();
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
               

                if (username != "" && pwd != "")
                {
                    DataTable dt = new DataTable();
                    string qr="select user_id as User_id,user_nm as User_Name,password as User_Password from User_master where user_nm='" + username + "' and password='" + pwd + "'";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = qr;
                    cmd.Connection = con;
                    con.Open();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                      
                        if (pwd.ToString().Equals(dt.Rows[0]["User_Password"].ToString()))
                        {
                            HttpCookie cookie = new HttpCookie("LoginCookies");
                            cookie["UserId"] = dt.Rows[0]["User_id"].ToString();
                            cookie["User_Name"] = dt.Rows[0]["User_Name"].ToString();
                            cookie.Expires.Add(new TimeSpan(0, 1, 0));
                            Response.Cookies.Add(cookie);
                            if (cookie != null)
                            {
                                int usertid = 0;
                                int.TryParse(dt.Rows[0]["User_id"].ToString(), out usertid);                          
                                Response.Redirect("~/Search_Employee.aspx", false);
                            }
                        }
                        else
                        {
                            lblmsg.Text = "Wrong Login Details.";
                            lblmsg.Visible = true;
                        }
                    }
                    else
                    {
                        lblmsg.Text = "Wrong Login Details.";
                        lblmsg.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Login 78: exception:" + ex.Message + "::::::::" + ex.StackTrace);
                Logger.WriteCriticalLog("Login 78: exception:" + ex.Message + "::::::::" + ex.StackTrace);
                lblmsg.Text = "Please Try Again.";
                lblmsg.Visible = true;
            }
            finally
            {
                con.Close();
            }
        }
    }
}