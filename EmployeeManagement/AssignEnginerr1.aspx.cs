﻿using RestSharp;
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
    public partial class WebForm2 : System.Web.UI.Page
    {
        int user_id = 0;
        int comp_id = 0;
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
                        header.Text = "Assign  Enginerr To Complaint";
                        ViewState["user"] = user_id;

                    }
                    else
                    {
                        Response.Redirect("~/login.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/login.aspx", false);
                }

                if (!Page.IsPostBack)
                {
                    getAllComplaint();
                    GetEnginerrDetails();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Assign Enginerr1 49: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        DataTable dtStatus = new DataTable();
        public void getAllComplaint()
        {
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "getComplaint";
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtStatus);
                if (dtStatus != null && dtStatus.Rows.Count > 0)
                {
                    dlComplaint.DataValueField = "comp_id";
                    dlComplaint.DataTextField = "comp_number";
                    dlComplaint.DataSource = dtStatus;
                    dlComplaint.DataBind();
                    dlComplaint.Items.Insert(0, new ListItem("Select Compalint", "-1"));
                }
                else
                {
                    dlComplaint.Items.Insert(0, new ListItem("Select Compalint", "-1"));

                }

            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Assign Enginerr1 83: exception:" + ex.Message + "::::::::" + ex.StackTrace);

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
           
        }
        public void GetCOmplaintDetailsById(int Comp_id)
        {
              String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "getComplaintBasicDetail";
                cmd.Parameters.AddWithValue("@comp_id", comp_id);
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtAllCOmplaint = new DataTable();
                adp.Fill(dtAllCOmplaint);
                if (dtAllCOmplaint != null && dtAllCOmplaint.Rows.Count > 0)
                {
                    DataRow[] drComp = dtAllCOmplaint.Select("comp_id=" + comp_id);
                    if (drComp.Length > 0)
                    {

                        DataTable dtComp = drComp.CopyToDataTable();
                        for (int j = 0; j < dtComp.Rows.Count; j++)
                        {
                            string Contact_no = dtComp.Rows[j]["mobile_no"].ToString();
                            //if (Contact_no.Equals(""))
                            //{
                            Contact_no = dtComp.Rows[j]["mobile_no"].ToString();

                            if (!Contact_no.Equals(""))
                            {
                                txtcontactNo.Text = Contact_no;
                            }

                            //}
                            string name = dtComp.Rows[j]["customer_name"].ToString();
                            if (name != null && !name.Equals(""))
                            {
                                txtcustname.Text = name;

                            }
                            string resi_add = dtComp.Rows[j]["resi_add"].ToString();
                            if (resi_add != null && !resi_add.Equals(""))
                            {

                                txtresiadd.Text = resi_add;
                            }
                        }


                    }


                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Assign Enginerr1 83: exception:" + ex.Message + "::::::::" + ex.StackTrace);

            }
            finally
            {
                con.Close();
                con.Dispose();
            
            }


        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
           
            if (comp_id > 0)
            {
                GetCOmplaintDetailsById(comp_id);
                GetEnginerrDetails();
            }
        }
        public void GetEnginerrDetails()
        {
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = " select [eng_id],[Name] ,[mobile_no] from [dbo].[Engineer-Details];";
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtEng = new DataTable();
                adp.Fill(dtEng);
                if (dtEng != null && dtEng.Rows.Count > 0)
                {

                    GrdEmpList.DataSource = dtEng;
                    GrdEmpList.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
                con.Dispose();
            }

        }
        public void sendtoclient(string Comp_no, string cust_Contact_no, string Eng_Contact_no, string empname,string date)
        {
            var client = new RestClient("http://api.msg91.com/api/v2/sendsms?campaign=&response=&afterminutes=&schtime=&unicode=&flash=&message=&encrypt=&authkey=&mobiles=&route=&sender=&country=91");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authkey", "241515A8p8I240V25bb9896e");
            request.AddParameter("application/json", "{ \"sender\": \"SOCKET\", \"route\": \"4\", \"country\": \"91\", \"sms\": [ { \"message\": \"Your Complaint has been book On date:" + date+ " and Complaint:" + Comp_no + " Assigned to" + empname + "" + Eng_Contact_no + "  FROM TPS\", \"to\": [ \"" + cust_Contact_no + "\" ] }, { \"message\": \"You Have been Assignrd to "+Comp_no+" on Date:"+date+" \", \"to\": [ \"" + Eng_Contact_no + "\" ] } ] }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Response.Write("status" + response.Content);
        }
        protected void dlComplaint_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(dlComplaint.SelectedValue.ToString(), out comp_id);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {

                string Eng_ids = "";
                string Eng_Contact_no = "";
                string empname = "";
                foreach (GridViewRow gvrow in GrdEmpList.Rows)
                {

                    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");

                    if (chk.Checked && chk != null)
                    {
                        Label emp = (Label)gvrow.FindControl("lblemp_id");
                        Label EngContactNo1 = (Label)gvrow.FindControl("lblengcontact");
                        Label ename = (Label)gvrow.FindControl("lblempName");
                        Eng_ids += emp.Text.ToString() + ",";
                        empname = ename.Text.ToString();
                        //Eng_Contact_no=Eng_Contact_no.ToString();
                        Eng_Contact_no = EngContactNo1.Text.ToString();

                    }
                }
                Eng_ids = Eng_ids.TrimEnd(',');
                string cust_Contact_no =txtcontactNo.Text.ToString();
                string[] arrempid = Eng_ids.Split(',');
                arrempid = Eng_ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (arrempid != null && !arrempid.Equals("") && arrempid.Length > 0)
                {
                    if (arrempid.Length == 1)
                    {
                        //int comp_id = 2;
                        string Comp_no = dlComplaint.SelectedValue.ToString();
                        // int.TryParse(dlComplaint.SelectedValue.ToString(), out comp_id);

                        string date = txtassignDate.Text.ToString();
                        string value2 = string.Format("{0:dd-MM-yyyy}", date);
                        string[] date1 = value2.Split(new char[]
                                            {
                                                 '/'
                                            }, StringSplitOptions.RemoveEmptyEntries);
                        int day = 0;
                        int mon = 0;
                        // int day = 0;
                        int year = 0;
                        if (date1.Length == 3)
                        {
                            int.TryParse(date1[0].ToString(), out day);
                            int.TryParse(date1[1].ToString(), out mon);
                            int.TryParse(date1[2].ToString(), out year);

                        }
                        else
                        {
                            ltrErr.Text = "Date Cannot be Blank";
                            return;
                        }
                        DateTime dt = new DateTime(year, mon, day);
                        long AssignTicks = dt.Ticks;
                        // string date1=txtAssignDate.Text.ToString();
                        // string[] date = date1.Split('/');
                        SqlCommand cmd = new SqlCommand();
                        int comp_id1 = int.Parse(dlComplaint.SelectedValue.ToString());
                        cmd.CommandText = "select  assigndate,eng_id,comp_id from comp_assign  where  [comp_id]=" + comp_id1 + " and [eng_id]=" + Eng_ids + " and  [assigndate]=" + AssignTicks + "; ";
                        cmd.Connection = con;
                        con.Open();
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataTable dtAssignDetails = new DataTable();
                        adp.Fill(dtAssignDetails);
                        if (dtAssignDetails != null && dtAssignDetails.Rows.Count > 0)
                        {
                            SqlCommand cmdInsert = new SqlCommand();
                            cmdInsert.CommandType = CommandType.StoredProcedure;
                            cmdInsert.CommandText = "UpdateCompDetails";
                            cmdInsert.Connection = con;
                           // con.Open();
                            cmdInsert.Parameters.AddWithValue("@eng_id", Eng_ids);
                            cmdInsert.Parameters.AddWithValue("@assigndate", AssignTicks);
                            cmdInsert.Parameters.AddWithValue("@eng_contact_no", Eng_Contact_no);
                            cmdInsert.Parameters.AddWithValue("@comp_no", Comp_no);
                            cmdInsert.Parameters.AddWithValue("@cust_contact_no", cust_Contact_no);
                            cmdInsert.Parameters.AddWithValue("@comp_id", comp_id1);
                            cmdInsert.Parameters.AddWithValue("@DOC", DateTime.Now.Ticks.ToString());
                            cmdInsert.Parameters.AddWithValue("@DOM", DateTime.Now.Ticks.ToString());
                            cmdInsert.Parameters.AddWithValue("@IsDelete", 0);
                            cmdInsert.Parameters.AddWithValue("@user_id", user_id);
                            int Success = cmdInsert.ExecuteNonQuery();
                            if (Success > 0)
                            {
                                ltrErr.Text = "Enginerr Assigned Successfully......";
                                //sendtoclient(Comp_no, cust_Contact_no, Eng_Contact_no, empname);

                            }


                        }
                        else
                        {
                            SqlCommand cmdInsert = new SqlCommand();
                            cmdInsert.CommandType = CommandType.StoredProcedure;
                            cmdInsert.CommandText = "spSaveCompAssignDetails";
                            cmdInsert.Connection = con;
                            //con.Open();
                           // Comp_no = "2";
                            cmdInsert.Parameters.AddWithValue("@eng_id", Eng_ids);
                            cmdInsert.Parameters.AddWithValue("@assigndate", AssignTicks);
                            cmdInsert.Parameters.AddWithValue("@eng_contact_no", Eng_Contact_no);
                            cmdInsert.Parameters.AddWithValue("@comp_no", Comp_no);
                            cmdInsert.Parameters.AddWithValue("@cust_contact_no", cust_Contact_no);
                            cmdInsert.Parameters.AddWithValue("@comp_id", comp_id1);
                            cmdInsert.Parameters.AddWithValue("@DOC", DateTime.Now.Ticks.ToString());
                            cmdInsert.Parameters.AddWithValue("@DOM", DateTime.Now.Ticks.ToString());
                            cmdInsert.Parameters.AddWithValue("@IsDelete", 0);
                            cmdInsert.Parameters.AddWithValue("@user_id", user_id);
                            int Success = cmdInsert.ExecuteNonQuery();
                            if (Success > 0)
                            {
                                ltrErr.Text = "Enginerr Assigned Successfully......";
                                sendtoclient(Comp_no, cust_Contact_no, Eng_Contact_no, empname,value2);
                            }

                        }
                    }
                    else
                    {

                        ltrErr.Text = "Select Only One Enginerr";
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Assign Enginerr1 83: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
    }
}