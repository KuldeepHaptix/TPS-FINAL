using System;
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
using System.Data.SqlClient;
using System.Configuration;


namespace EmployeeManagement
{

    public partial class ManageDepartment : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        DataTable dtdeptList = new DataTable();
        DataTable dtdeptDetails = new DataTable();
        DataTable dtmaster = new DataTable();


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
                        header.Text = "Manage City";
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
                if (!IsPostBack)
                {
                    txtdept.Focus();
                    showgrid();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Department 56: exception:" + ex.Message + "::::::::" + ex.StackTrace);

            }
        }

        public void showgrid()
        {
            //objmysqldb.ConnectToDatabase();
            try
            {
                String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(strConnString);
                ltrErr.Text = "";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetCities";
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable diCity = new DataTable();
                adp.Fill(diCity);
                //dtdeptList = objmysqldb.GetData("select department_id,Department_Name,IsMasterData from department_master  where IsDelete=0 order by department_id");
               // objmysqldb.disposeConnectionObj();
                if (diCity != null)
                {
                    grd.DataSource = diCity;
                    grd.DataBind();
                    btnExport.Visible = true;


                }
                else
                {
                    ltrErr.Text = "No Data Found.";
                    btnExport.Visible = false;
                    grd.DataSource = null;
                }
                //UpdatePanel1.Update();
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Department 87: exception:" + ex.Message + "::::::::" + ex.StackTrace);

            }
        }



        protected void btnsave_Click(object sender, EventArgs e)
        {

            ltrErr.Text = "";
            DataTable dtSearch = new DataTable();
            try
            {
                if (!txtdept.Text.ToString().Equals(""))
                {

                    //objmysqldb.ConnectToDatabase();
                  //ConfigurationManager cm=
                    dtSearch = objmysqldb.GetData("Select * from cities WHERE IsDelete=0 and (Department_Name like '" + txtdept.Text.ToString() + "') ");


                    if (dtSearch != null && dtSearch.Rows.Count > 0)
                    {

                        ltrErr.Text = " Department Is Already Exist.";
                        return;

                    }
                    DateTime currenttime = Logger.getIndiantimeDT();
                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    objmysqldb.AddCommandParameter("deptnm", txtdept.Text.ToString());

                    string query = "Insert into   department_master (Department_Name,modify_datetime,DOC,UserID,IsDelete,IsUpdate,IsMasterData) values (?deptnm," + currenttime.Ticks + "," + currenttime.Ticks + "," + user_id + ",0,1,1) ";

                    objmysqldb.OpenSQlConnection();
                    int res = objmysqldb.InsertUpdateDeleteData(query);
                    if (res != 1)
                    {
                        ltrErr.Text = "Please Try Again.";

                        Logger.WriteCriticalLog("Manage_Department 128 Update error.");
                    }
                    else
                    {
                        grd.EditIndex = -1;
                        showgrid();
                        ltrErr.Text = "Department Save Successfully.";
                        txtdept.Text = "";
                    }
                }
                else
                {
                  
                    ltrErr.Text = "Please Enter All Details.";
                    return;
                }

            }

            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Department 149: exception:" + ex.Message + "::::::::" + ex.StackTrace);

            }


        }




        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                if (e.CommandName == "del")
                {
                    string[] arg = e.CommandArgument.ToString().Split(':');

                    if (arg != null && arg.Length == 2)
                    {
                        int deptId = 0;
                        int.TryParse(arg[1], out deptId);
                        objmysqldb.ConnectToDatabase();
                        dtdeptList = objmysqldb.GetData("Select department_id from department_master  WHERE department_id  =" + deptId + "  ");
                        string qu = "Select empId from employee_master WHERE EmpDeptID=" + deptId + "";
                        DataTable dtemp = objmysqldb.GetData(qu);

                       
                        if (dtemp != null && dtemp.Rows.Count > 0)
                        {
                            ltrErr.Text = "Employee Assign to selected Department so you can't delete it";
                            return;
                        }
                        else
                        {
                            int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                            DateTime currenttime = Logger.getIndiantimeDT();
                            string query = "Update department_master set IsDelete=1,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where department_id=" + deptId + " ";
                            objmysqldb.OpenSQlConnection();
                            int res = objmysqldb.InsertUpdateDeleteData(query);
                            if (res != 1)
                            {
                                ltrErr.Text = "Please Try Again.";

                                Logger.WriteCriticalLog("Manage_Department 194 Update error.");
                            }
                            else
                            {
                                grd.EditIndex = -1;
                                showgrid();
                                ltrErr.Text = "Department Delete Successfully.";
                            }
                        }
                    }
                    else
                    {
                        showgrid();
                        ltrErr.Text = "Please Try Again.";
                    }
                    grd.EditIndex = -1;
                }
                else if (e.CommandName == "Edit")
                {

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Department 218: exception:" + ex.Message + "::::::::" + ex.StackTrace);

            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.TableSection = TableRowSection.TableHeader;
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                { }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (((DataRowView)e.Row.DataItem)["IsMasterData"].ToString() == "0")
                    {
                        e.Row.Enabled = false;
                        var linkButton = e.Row.FindControl("lnkdelete") as LinkButton;
                        linkButton.Enabled = false;
                        linkButton.OnClientClick = "";
                    }
                }
            }
            catch (Exception aa)
            {
                Logger.WriteCriticalLog("Manage_Department 251: exception:" + aa.Message + "::::::::" + aa.StackTrace);
            }
        }

        
        protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                TextBox txtdeptnm = (TextBox)grd.Rows[e.RowIndex].FindControl("txtdept");
                Label lbid = (Label)grd.Rows[e.RowIndex].FindControl("lbldept_ID");
                string deptId = lbid.Text;
                string deptName = txtdeptnm.Text.ToString().Trim();
                int dept_Id = 0;
                objmysqldb.ConnectToDatabase();
                int.TryParse(lbid.Text.ToString().Trim(), out dept_Id);
                DateTime currenttime = Logger.getIndiantimeDT();
                if (deptName != "" && dept_Id > 0)
                {
                    objmysqldb.ConnectToDatabase();

                    dtdeptList = objmysqldb.GetData("Select * from department_master WHERE IsDelete=0 and department_Name like '" + deptName + "' and  department_id<>" + deptId + "  ");

                    if (dtdeptList != null && dtdeptList.Rows.Count > 0)
                    {

                        ltrErr.Text = "Same Department Name Is Already Exist.";
                        return;

                    }
                    string qw = "select department_id,department_name from  department_master where department_id=" + dept_Id + " And IsMasterData=0";

                    dtmaster = objmysqldb.GetData(qw);



                    string query = "Select * from department_master WHERE department_id=" + dept_Id + "  ";
                    dtdeptDetails = objmysqldb.GetData(query);

                    if (dtdeptDetails != null && dtdeptDetails.Rows.Count > 0)
                    {
                        objmysqldb.OpenSQlConnection();
                        int.TryParse(dtdeptDetails.Rows[0]["department_id"].ToString(), out dept_Id);
                        objmysqldb.AddCommandParameter("deptname", deptName);
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        string q = "Update department_master  set Department_Name=?deptname,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where department_id=" + deptId + " ";

                        int res = objmysqldb.InsertUpdateDeleteData(q);
                        if (res != 1)
                        {
                            ltrErr.Text = "Please Try Again.";

                            Logger.WriteCriticalLog("Manage_Departmrnt 304 Update error.");
                            return;
                        }                     
                        grd.EditIndex = -1;
                        showgrid();
                        ltrErr.Text = "Department Name Updated Successfully.";
                    }
                    else
                    {
                        ltrErr.Text = "Please Try Again.";
                        return;
                    }

                }
                else
                {
                    //showgrid();
                    ltrErr.Text = "Please Enter All Details.";

                }

                grd.EditIndex = -1;

            }
            catch (Exception ex)
            {
                ltrErr.Text = "Please Try Again.";
                Logger.WriteCriticalLog("Manage_Department 331: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }

            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                string command = e.ToString();
                grd.EditIndex = e.NewEditIndex;
                showgrid();
                //UpdatePanel1.Update();
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Department 352: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grd.EditIndex = -1;
                showgrid();
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Department 365: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                objmysqldb.ConnectToDatabase();
                DataTable dtdepartment = objmysqldb.GetData("Select department_id,Department_Name,IsMasterData from department_master where IsDelete=0 order by department_id");
                objmysqldb.disposeConnectionObj();
                if (dtdepartment != null && dtdepartment.Rows.Count > 0)
                {
                    ExportToExcel kg = new ExportToExcel();
                    string exportedfile = kg.ExportDataTableToExcel(dtdepartment, "List_Of_Department");
                    Response.Redirect(ExportToExcel.EXPORT_URL + exportedfile, false);
                }
                else
                {
                    ltrErr.Text = "No data exists";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Department 391: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
       }       
    }
}




