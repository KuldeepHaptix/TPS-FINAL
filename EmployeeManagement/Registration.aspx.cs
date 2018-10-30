using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagement
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cleardata();

        }
        public void cleardata()
        {
            txtconfpwd.Text = "";
            txtemail.Text = "";
            txtpwd.Text = "";
            txtname.Text = "";
            lblmsg.Text = "";

        }
        private bool ValidateEmail(string emailId)
        {
            bool Success;
            string email = emailId;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                lblmsg.Text = email + " is Valid Email Address";
                Success = true;
            }
            else
            {
                lblmsg.Text = email + " is Invalid Email Address";
                Success = false;
            }
            return Success;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
                string user_nm = txtname.Text.ToString();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT  [user_nm] FROM [TPS].[dbo].[User_master] where [user_nm]='" + user_nm + "'";
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtUserNm = new DataTable();
                adp.Fill(dtUserNm);
                if (dtUserNm != null && dtUserNm.Rows.Count > 0)
                {
                    lblmsg.Text = "UserName Is Already Exists:" + dtUserNm.Rows[0]["User_nm"].ToString();

                }
                string pwd = txtpwd.Text.ToString();
                string ConfPwd = txtconfpwd.Text.ToString();
                //if (pwd.Equals(ConfPwd))
                //{

                //    lblmsg.Text = "Passwod do not match..";
                //}
                //else
                //{
                string gender = "";
                if (rdbmale.Checked)
                {
                    gender = "Male";
                }
                else
                {

                    gender = "Female";
                }
                string mail = txtemail.Text.ToString();
                bool yes = ValidateEmail(mail);
                if (yes)
                {
                    SqlCommand cmdInsert = new SqlCommand("SaveuserDetails", con);
                    cmdInsert.CommandType = CommandType.StoredProcedure;
                    //cmdInsert.CommandText = "spsaveUserDetails ",con;
                    //cmdInsert.CommandText = CommandType.StoredProcedure;
                    cmdInsert.Connection = con;
                    //con.Open();
                    cmdInsert.Parameters.AddWithValue("@user_nm", user_nm);
                    cmdInsert.Parameters.AddWithValue("@password", pwd);
                    cmdInsert.Parameters.AddWithValue("@contact_no", txtcnno.Text.ToString());
                    cmdInsert.Parameters.AddWithValue("@email", mail);
                    cmdInsert.Parameters.AddWithValue("@gender", gender);

                    int y = cmdInsert.ExecuteNonQuery();
                    if (y > 0)
                    {
                        DataTable dt = new DataTable();
                        string qr = "select user_id as User_id,user_nm as User_Name,password as User_Password from User_master order by user_id desc ";
                        SqlCommand cmdgetData = new SqlCommand();
                        cmdgetData.CommandText = qr;
                        cmdgetData.Connection = con;
                        SqlDataAdapter dpt = new SqlDataAdapter(cmd);
                        dpt.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            HttpCookie cookie = new HttpCookie("LoginCookies");
                            cookie["UserId"] = dt.Rows[0]["User_id"].ToString();
                            cookie["User_Name"] = dt.Rows[0]["User_Name"].ToString();
                           
                            //lblmsg.Text = "Successfully Registered....";
                        }
                    }
                    else
                    {
                        lblmsg.Text = "Error While Saving Data...";


                    }
                }
                else
                {

                    lblmsg.Text = "Enter Valid Input";

                }

                //}
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();

            }
        }

    }
}