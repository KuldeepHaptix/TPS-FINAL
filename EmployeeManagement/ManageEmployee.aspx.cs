using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;
using RestSharp;


namespace EmployeeManagement
{
    public partial class ManageEmployee : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        int Comp_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                    {
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        Label header = Master.FindControl("lbl_pageHeader") as Label;
                        string temp = Request.QueryString.ToString();
                        if (Request.QueryString.Count > 0)
                        {
                        
                            string emp_id = Request.QueryString["Comp"].ToString();
                            int.TryParse(emp_id.ToString(), out Comp_id);
                            if (Comp_id > 0)
                            {
                                GetComplaintDetails();
                            }
                        }
                        if (Comp_id == 0)
                        {
                            header.Text = "Add Complaint Details";

                        }
                        else
                        {
                            btnAddComailnt.Text = "Update Complaint";
                            header.Text = "Update Complaint Details";
                        }

                    }
                    else
                    {
                        Response.Redirect("~/login.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/login.aspx", false);
                    Logger.WriteCriticalLog("ManageEmployee 87: exception:" + ex.Message + "::::::::" + ex.StackTrace);
                }

                if (!IsPostBack)
                {
                    bindStatus();
                    GetComplaintNo();
                    if (Comp_id > 0)
                    {
                        GetComplaintDetails();
                        

                    }
                }
                
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ManageEmployee 87: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }



        private void bindStatus()
        {
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllStatus";
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtStatus = new DataTable();
                adp.Fill(dtStatus);
                if (dtStatus != null && dtStatus.Rows.Count > 0)
                {

                    ddlStatus.DataSource = dtStatus;
                    ddlStatus.DataTextField = "Status";
                    ddlStatus.DataValueField = "Status_id";
                    ddlStatus.DataBind();
                    ddlStatus.Items.Insert(0, new ListItem("Select Status", "-1"));


                }
            }
            catch (Exception Ex)
            {
                Logger.WriteCriticalLog("ManageEmployee 87: exception:" + Ex.Message + "::::::::" + Ex.StackTrace);
            }
            finally
            {
                con.Close();

            }

        }


        private void updateComapaintDetails()
        {
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);

            try
            {
                if (txtname.Equals(""))
                {
                    //ltrErr.Text = "Customer name is mandatory";
                    return;
                }
                long Comp_date = 0;
                string Comp_dt = "";
                string Comp_Complete_dt = "";
                if (txtcomplaintDate.Text.Equals(""))
                {
                    //ltrErr.Text = "Complaint Date is mandatory";
                    return;
                }
                else
                {
                    string[] arrdob = txtcomplaintDate.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (arrdob.Length == 3)
                    {
                        Comp_dt = ((arrdob[0]).ToString().Length == 1 ? "0" + (arrdob[0]).ToString() : (arrdob[0]).ToString()) + "/" + ((arrdob[1]).ToString().Length == 1 ? "0" + (arrdob[1]).ToString() : (arrdob[1]).ToString()) + "/" + ((arrdob[2]).ToString());
                    }
                    else
                    {
                        //  ltrErr.Text = "Complaint Date is not in proper format.";
                        return;
                    }

                }
                string[] comp_date = txtcomplaintDate.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (comp_date.Length == 3)
                {
                    Comp_Complete_dt = ((comp_date[0]).ToString().Length == 1 ? "0" + (comp_date[0]).ToString() : (comp_date[0]).ToString()) + "/" + ((comp_date[1]).ToString().Length == 1 ? "0" + (comp_date[1]).ToString() : (comp_date[1]).ToString()) + "/" + ((comp_date[2]).ToString());
                }
                if (txtresiAdd.Text.Equals(""))
                {
                    //ltrErr.Text = "Complaint Date is not in proper format.";
                    return;
                }
                if (txtdesc.Text.Equals(""))
                {
                    //ltrErr.Text = "Descuition can't be blank.";
                    return;

                }
                int CancelFlag = 0;
                int status = 0;
                int.TryParse(ddlStatus.Items[ddlStatus.SelectedIndex].Value.ToString(), out status);
                if (status < 0)
                {

                    //ltrErr.Text = "Select Valid Status.";
                    return;

                }
                if (status==3)
                {
                    CancelFlag = 1;
                
                }


                SqlCommand cmdUpdate = new SqlCommand("UpdateComplaintDetails", con);
               //cmdUpdate.CommandText = ();
                cmdUpdate.CommandType = CommandType.StoredProcedure;
                cmdUpdate.Parameters.AddWithValue("@comp_id", Comp_id);
                cmdUpdate.Parameters.AddWithValue("@custome_name", txtname.Text.ToString());
                cmdUpdate.Parameters.AddWithValue("@mobile_no", txtcontactno.Text.ToString());
                cmdUpdate.Parameters.AddWithValue("@alternate_no ", txtotherno.Text.ToString());
               // cmdUpdate.Parameters.AddWithValue("@resi_city ",);
                cmdUpdate.Parameters.AddWithValue("@pin_code ", txtpincode.Text.ToString());
                cmdUpdate.Parameters.AddWithValue("@complaint_desc ", txtdesc.Text.ToString());
                //cmdUpdate.Parameters.AddWithValue("@addeby ", user_id);
               // cmdUpdate.Parameters.AddWithValue("@IsDelete ", 0);
               // cmdUpdate.Parameters.AddWithValue("@DOC", DateTime.Now.Ticks);
                cmdUpdate.Parameters.AddWithValue("@DOM ", DateTime.Now.Ticks);
                cmdUpdate.Parameters.AddWithValue("@status_id", status);
                cmdUpdate.Parameters.AddWithValue("@resi_city", txtcity.Text.ToString());
                long comp_date1 = 0;
                long comp_date2 = 0;
                long.TryParse(comp_date.ToString(), out comp_date1);
                long.TryParse(Comp_Complete_dt.ToString(), out comp_date2);
                cmdUpdate.Parameters.AddWithValue("@comp_date", comp_date1);
                cmdUpdate.Parameters.AddWithValue("@IsCancel",CancelFlag);
                cmdUpdate.Parameters.AddWithValue("@comp_complete_date", comp_date2);
           
                //cmdUpdate.Parameters.AddWithValue(" @IsCancel", 0);
                con.Open();
                int Success = cmdUpdate.ExecuteNonQuery();
                if (Success > 0)
                {
                    ltrErr.Text = "Complaint Updated Successfully......";
                }
                else
                {

                   ltrErr.Text = "Complaint Updated Successfully......";

                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Error While Upadte Comp Detail: exception:" + ex.Message + "::::::::" + ex.StackTrace);

            }
            finally
            {
                con.Close();
            }

        
        }
        public void GetComplaintNo()
        {
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "getComplaintID";
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtCompNo = new DataTable();
                adp.Fill(dtCompNo);
                string Comp_no_Append = "COMP";
                string FinalComp_no = "";
                if (dtCompNo != null && dtCompNo.Rows.Count == 0)
                {

                    FinalComp_no = "COMP01";
                }

                else
                {
                    string comp_no = dtCompNo.Rows[0]["comp_id"].ToString();
                    if (comp_no.Equals(""))
                    {

                        FinalComp_no = "COMP01";
                    }
                    else
                    {
                        int CONO = int.Parse(comp_no.ToString());
                        int fcno = CONO + 1;
                        FinalComp_no = Comp_no_Append + fcno.ToString();
                        txtcompno.Text = FinalComp_no.ToString();

                    }


                }
            }
            catch(Exception ex)
            {

                Logger.WriteCriticalLog("Error While Getting Complaint no: exception:" + ex.Message + "::::::::" + ex.StackTrace);

            }
            finally
            {
                con.Close();
                con.Dispose();
               
            }
        
        
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
        }

        protected void btnAddComailnt_Click(object sender, EventArgs e)
        {
            int comid=int.Parse(Comp_id.ToString());

            if (comid > 0)
            {
                if (!int.Parse(Comp_id.ToString()).Equals("0"))
                {
                    // GetComplaintDetails();
                    updateComapaintDetails();
                    return;
                }
            }
            string doc = " ";
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
                if (txtname.Equals(""))
                {
                    //ltrErr.Text = "Customer name is mandatory";
                    return;
                }
                long Comp_date = 0;
                string Comp_dt = "";
                string Comp_Complete_dt = "";
                if (txtcomplaintDate.Text.Equals(""))
                {
                    //ltrErr.Text = "Complaint Date is mandatory";
                    return;
                }
                else
                {
                    string[] arrdob = txtcomplaintDate.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (arrdob.Length == 3)
                    {
                        Comp_dt = ((arrdob[0]).ToString().Length == 1 ? "0" + (arrdob[0]).ToString() : (arrdob[0]).ToString()) + "/" + ((arrdob[1]).ToString().Length == 1 ? "0" + (arrdob[1]).ToString() : (arrdob[1]).ToString()) + "/" + ((arrdob[2]).ToString());
                    }
                    else
                    {
                        //  ltrErr.Text = "Complaint Date is not in proper format.";
                        return;
                    }

                }
                string[] comp_date = txtcomplaintDate.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (comp_date.Length == 3)
                {
                    Comp_Complete_dt = ((comp_date[0]).ToString().Length == 1 ? "0" + (comp_date[0]).ToString() : (comp_date[0]).ToString()) + "/" + ((comp_date[1]).ToString().Length == 1 ? "0" + (comp_date[1]).ToString() : (comp_date[1]).ToString()) + "/" + ((comp_date[2]).ToString());
                }
                if (txtresiAdd.Text.Equals(""))
                {
                    //ltrErr.Text = "Complaint Date is not in proper format.";
                    return;
                }
                if (txtdesc.Text.Equals(""))
                {
                    //ltrErr.Text = "Descuition can't be blank.";
                    return;

                }
                int status = 0;
                int.TryParse(ddlStatus.Items[ddlStatus.SelectedIndex].Value.ToString(), out status);
                if (status < 0)
                {

                    //ltrErr.Text = "Select Valid Status.";
                    return;

                }


                String FinalComp_no = txtcompno.Text.ToString();
                string name = txtname.Text.ToString();
                string contact_no = txtcontactno.Text.ToString();
                con.Open();
                SqlCommand cmdInsert = new SqlCommand("sp_save_compDetails", con);
                cmdInsert.CommandType = CommandType.StoredProcedure;
                cmdInsert.Parameters.AddWithValue("@comp_number", FinalComp_no);
                cmdInsert.Parameters.AddWithValue("@custome_name", txtname.Text.ToString());
                cmdInsert.Parameters.AddWithValue("@mobile_no", txtcontactno.Text.ToString());
                cmdInsert.Parameters.AddWithValue("@alternate_no ", txtotherno.Text.ToString());
                //cmdInsert.Parameters.AddWithValue("@resi_city ",);
                cmdInsert.Parameters.AddWithValue("@pin_code ", txtpincode.Text.ToString());
                cmdInsert.Parameters.AddWithValue("@complaint_desc ", txtdesc.Text.ToString());
                cmdInsert.Parameters.AddWithValue("@addeby ", user_id);
                cmdInsert.Parameters.AddWithValue("@IsDelete ", 0);
                cmdInsert.Parameters.AddWithValue("@DOC", DateTime.Now.Ticks);
                cmdInsert.Parameters.AddWithValue("@DOM ", DateTime.Now.Ticks);
                cmdInsert.Parameters.AddWithValue("@status_id", status);
                cmdInsert.Parameters.AddWithValue("@resi_city", txtcity.Text.ToString());
                long comp_date1 = 0;
                long comp_date2 = 0;
                long.TryParse(comp_date.ToString(), out comp_date1);
                long.TryParse(Comp_Complete_dt.ToString(),out comp_date2);
                cmdInsert.Parameters.AddWithValue("@comp_date",comp_date1);
                  cmdInsert.Parameters.AddWithValue("@comp_complete_date",comp_date2);
                  cmdInsert.Parameters.AddWithValue("@IsCancel", 0);
               
                int Success = cmdInsert.ExecuteNonQuery();
                if (Success > 0)
                {
                   ltrper.Text = "Complaint Saved Successfully......";
                   try
                   { 
                       string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };  
  
                        string sRandomOTP = GenerateRandomOTP(4, saAllowedCharacters);
                       string date=DateTime.Now.ToShortDateString();
                       string con_no=FinalComp_no.ToString();
                       sendtoclient(name, FinalComp_no, contact_no, sRandomOTP, date);
                   }
                    catch(Exception ex)
                   {
                       Logger.WriteCriticalLog("Message Sent Error: exception:" + ex.Message + "::::::::" + ex.StackTrace);

                   }
                }
                else
                {

                    ltrper.Text = "Complaint  Not Saved Successfully......";
                
                }


            }

            catch (Exception Ex)
            {
                Logger.WriteCriticalLog("Error while saving Comp Details: exception:" + Ex.Message + "::::::::" + Ex.StackTrace);


            }
            finally
            {

                con.Close();
            }
        }
        private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
        {
            string sOTP = String.Empty;

           string sTempChars = String.Empty;


            try
            {
                Random rand = new Random();

                for (int i = 0; i < iOTPLength; i++)
                {

                    int p = rand.Next(0, saAllowedCharacters.Length);

                    sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                    sOTP += sTempChars;

                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Error Generating OTO Line no 1352 " + ex.Message + "Error:=" + ex.StackTrace);
               
            }
            return sOTP;

        }
        public void sendtoclient(string name,string Comp_no, string cust_Contact_no, string OTP, string date)
        {
            var client = new RestClient("http://api.msg91.com/api/v2/sendsms?campaign=&response=&afterminutes=&schtime=&unicode=&flash=&message=&encrypt=&authkey=&mobiles=&route=&sender=&country=91");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authkey", "241515A8p8I240V25bb9896e");
           // request.AddParameter("application/json", "{"'sender'": "'SOCKET'","'route'": "'4'","'country'": "'91'","'sms'": [{"'message'":"+name + "Your Complaint has been book On date:" + date + " and Complaint #" + Comp_no + "  please share this OTP:" + OTP + " with the enginerr only only after when the work is complete  FROM TPS","to: ["+cust_Contact_no+"]]}", ParameterType.RequestBody);


             request.AddParameter("application/json", "{ \"sender\": \"SOCKET\", \"route\": \"4\", \"country\": \"91\", \"sms\": [ { \"message\": \"Your Complaint has been book On date:" + date+ " and Complaint:" + Comp_no + " Assigned to   FROM TPS\", \"to\": [ \"" + cust_Contact_no + "\" ] } ] }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Response.Write("status" + response.Content);
  
    
   
            
        }
        private void GetComplaintDetails()
        {
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
               
                SqlCommand cmd = new SqlCommand();

                if (Comp_id > 0)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GetAllComplaintDetailsBYComp_Id";
                    cmd.Parameters.AddWithValue("@comp_id", Comp_id);
                    cmd.Connection = con;
                    con.Open();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dtCompNo = new DataTable();
                    adp.Fill(dtCompNo);
                    if (dtCompNo != null && dtCompNo.Rows.Count > 0)
                    {
                        if (!dtCompNo.Rows[0]["customer_name"].ToString().Equals(""))
                        {
                            string Customer_Name = dtCompNo.Rows[0]["customer_name"].ToString();
                            if (!Customer_Name.Equals(""))
                            {

                                txtname.Text = Customer_Name;
                            }
                        }

                        if (!dtCompNo.Rows[0]["comp_number"].ToString().Equals(""))
                        {
                            string Comp_no = dtCompNo.Rows[0]["comp_number"].ToString();
                            if(!Comp_no.Equals(""))
                            {
                                txtcompno.Text = Comp_no;

                            }
                        
                        }

                        if (!dtCompNo.Rows[0]["mobile_no"].ToString().Equals(""))
                        {
                            string mobile_no = dtCompNo.Rows[0]["mobile_no"].ToString();
                            if (!mobile_no.Equals(""))
                            {
                               txtcontactno.Text = mobile_no;
                            }
                        
                        }
                        if (!dtCompNo.Rows[0]["alternate_no"].ToString().Equals(""))
                        {
                            string Alternate_no = dtCompNo.Rows[0]["alternate_no"].ToString();
                            if (!Alternate_no.Equals(""))
                            {
                                txtotherno.Text = Alternate_no;
                            }

                        }
                        if (!dtCompNo.Rows[0]["pin_code"].ToString().Equals(""))
                        {
                            string pin_code = dtCompNo.Rows[0]["pin_code"].ToString();
                            if (!pin_code.Equals(""))
                            {
                                txtpincode.Text = pin_code;
                            }

                        }
                        if (!dtCompNo.Rows[0]["complaint__desc"].ToString().Equals(""))
                        {
                            string complaint__desc = dtCompNo.Rows[0]["complaint__desc"].ToString();
                            if (!complaint__desc.Equals(""))
                            {
                                txtdesc.Text = complaint__desc;
                            }

                        }
                        if (!dtCompNo.Rows[0]["resi_add"].ToString().Equals(""))
                        {
                            string resi_add = dtCompNo.Rows[0]["resi_add"].ToString();
                            if (!resi_add.Equals(""))
                            {
                                txtresiAdd.Text = resi_add;
                            }

                        }
                        string Status_id = dtCompNo.Rows[0]["status_id"].ToString();
                        if (!Status_id.Equals("-1"))
                        {
                            ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(Status_id));
                        }
                        if (!dtCompNo.Rows[0]["comp_date"].ToString().Equals(""))
                        {
                            long Comp_Date = long.Parse(dtCompNo.Rows[0]["comp_date"].ToString());
                            string comp_Date = (String)Comp_Date.ToString();
                            string[] co_date = comp_Date.Split('/');
                            if (co_date.Length == 3)
                            {
                                string mon = co_date[0].ToString();
                                string date = co_date[1].ToString();
                                string yr = co_date[2].ToString();

                                string final_comp_date = mon + "/" + date + "/" + "/" + yr;
                                txtcompdate.Text = final_comp_date;
                            }
                        }
                        if (!dtCompNo.Rows[0]["comp_complete_date"].ToString().Equals(""))
                        {
                            long Comp_Complete_Date = long.Parse(dtCompNo.Rows[0]["comp_complete_date"].ToString());
                            string comp_Complete_Date = (String)Comp_Complete_Date.ToString();
                            string[] co_Complete_date = comp_Complete_Date.Split('/');
                            if (co_Complete_date.Length == 3)
                            {
                                string mon = co_Complete_date[0].ToString();
                                string date = co_Complete_date[1].ToString();
                                string yr = co_Complete_date[2].ToString();

                                string final_comp_date = mon + "/" + date + "/" + "/" + yr;
                                txtcompdate.Text = final_comp_date;
                            }
                        }
                        if (!dtCompNo.Rows[0]["resi_city"].ToString().Equals(""))
                        {
                            string city = dtCompNo.Rows[0]["resi_city"].ToString();
                            txtcity.Text = city;
                        }
                        if (!dtCompNo.Rows[0]["OTP"].ToString().Equals(""))
                        {
                            string OTP = dtCompNo.Rows[0]["OTP"].ToString();
                            txtotp.Text = OTP;
                        }
                    }
                }
               

            }
            catch (Exception ex)
            {
                //String PageName = "ManageEmployee.Aspx";
                Logger.WriteCriticalLog("Error GetComplaintDetails Line no 1352 "+ex.Message +"Error:="+ex.StackTrace);

            }
            finally
            {

                con.Close();
            }
        }
    }
}
