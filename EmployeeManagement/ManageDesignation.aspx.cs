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

namespace EmployeeManagement
{
    public partial class ManageDesignation : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        DataTable dtismaster = new DataTable();
        
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
                        header.Text = "Manage Designation";
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
                    txtdesignames.Focus();
                    showgrid();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ManageDesignation 54: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void showgrid()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                ltrErr.Text = "";
                DataTable dtdesigList = objmysqldb.GetData("Select designation_id,Designation_Name,IsMasterData from designation_master where IsDelete=0 order by designation_id");
                objmysqldb.disposeConnectionObj();
                if (dtdesigList != null)
                {
                    grd.DataSource = dtdesigList;
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
                Logger.WriteCriticalLog("ManageDesignation 82: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                if  (!txtdesignames.Text.ToString().Equals(""))
                {   
                    objmysqldb.ConnectToDatabase();
                    DataTable dtdesigList = objmysqldb.GetData("select designation_id,Designation_Name from designation_master where Designation_Name like '" + txtdesignames.Text.ToString() + "' and IsDelete=0  ;");
                  
                    if (dtdesigList != null && dtdesigList.Rows.Count > 0)
                    {
                        ltrErr.Text = " Designation Is Already Exist.";
                        return;
                    }

                    DateTime currenttime = Logger.getIndiantimeDT();
                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    objmysqldb.AddCommandParameter("designame", txtdesignames.Text.ToString());
                    string query = "Insert into  designation_master (Designation_Name,DOC,UserID,IsDelete,IsUpdate,modify_datetime,IsMasterData) values (?designame," + currenttime.Ticks + "," + user_id + ",0,1," + currenttime.Ticks + ",1) ";
                    objmysqldb.OpenSQlConnection();
                    int res = objmysqldb.InsertUpdateDeleteData(query);
                    if (res != 1)
                    {
                        ltrErr.Text = "Please Try Again.";
                        Logger.WriteCriticalLog("ManageDesignation 111 Update error.");
                    }
                    else
                    {
                        grd.EditIndex = -1;
                        showgrid();
                        ltrErr.Text = "Designation Save Successfully.";
                        txtdesignames.Text = "";
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
                Logger.WriteCriticalLog("ManageDesignation 128: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
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
                        int desigid = 0;
                        int.TryParse(arg[1], out desigid);
                        objmysqldb.ConnectToDatabase();
                        DataTable EmpList = objmysqldb.GetData("Select EmpId from employee_master  WHERE  EmpDesignId =" + desigid + "  ");                       
                        if (EmpList != null && EmpList.Rows.Count > 0)
                        {
                            ltrErr.Text = "Employee Assign to selected Designation so you can't delete it.";
                            return;
                        }
                        else
                        {  	
                            int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                            DateTime currenttime = Logger.getIndiantimeDT();
                            string query = "Update designation_master set IsDelete=1,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where designation_id=" + int.Parse(arg[0]) + " ";
                            objmysqldb.OpenSQlConnection();
                            int res = objmysqldb.InsertUpdateDeleteData(query);
                            if (res != 1)
                            {
                                ltrErr.Text = "Please Try Again.";
                                Logger.WriteCriticalLog("ManageDesignation 166 Update error.");
                            }
                            else
                            {
                                grd.EditIndex = -1;
                                showgrid();
                                ltrErr.Text = "Designation Deleted Successfully.";
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
                   string[] arg1 = e.CommandArgument.ToString().Split(':');
                   string argu1 = arg1[0].ToString();
                    ViewState["argu"] = argu1;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ManageDesignation 193: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
                  //GridViewRow row = grd.SelectedRow;
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
              catch (Exception ex)
              {
                  Logger.WriteCriticalLog("ManageDesignation 226: exception:" + ex.Message + "::::::::" + ex.StackTrace);
              }
          }
          protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
          {
              try
              {
                  ltrErr.Text = "";
                  TextBox txtdesignames = (TextBox)grd.Rows[e.RowIndex].FindControl("txtDesignation_Name");
                  Label lbid = (Label)grd.Rows[e.RowIndex].FindControl("lbldesignation_id");
                  string desigId = lbid.Text;
                  string DesigName = txtdesignames.Text.ToString().Trim();
                  int Desid = 0;
                  int.TryParse(desigId.ToString().Trim(), out Desid);
                  DateTime currenttime = Logger.getIndiantimeDT();
                  if (DesigName != "" && Desid > 0)
                  {
                      objmysqldb.ConnectToDatabase();


                     
                      DataTable dtdesigList = objmysqldb.GetData("Select * from designation_master WHERE  IsDelete=0 and Designation_Name like '" + DesigName + "' and designation_id<>" + Desid + " ");

                      DataTable dtdesigid = objmysqldb.GetData("Select designation_id from designation_master WHERE designation_id=" + Desid + " ");
                     

                      if (dtdesigList != null && dtdesigList.Rows.Count > 0)
                      {
                          ltrErr.Text = "Same Designation Name Is Already Exist";
                          return;
                      }

                      if (dtdesigid != null && dtdesigid.Rows.Count > 0)
                      {
                          objmysqldb.AddCommandParameter("Desiname", DesigName);
                          int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                          string query = " Update designation_master set Designation_Name=?Desiname,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + ",IsMasterData=1 WHERE designation_id=" + Desid + "";
                          objmysqldb.OpenSQlConnection();
                          int res = objmysqldb.InsertUpdateDeleteData(query);
                          if (res != 1)
                          {
                              ltrErr.Text = "Please Try Again.";
                              Logger.WriteCriticalLog("ManageDesignation 268 Update error.");
                              return;
                          }
                          grd.EditIndex = -1;
                          showgrid();
                          ltrErr.Text = "Designation Name Updated Successfully.";
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
                  Logger.WriteCriticalLog("ManageDesignation 292: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
                  Logger.WriteCriticalLog("ManageDesignation 312: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
                  Logger.WriteCriticalLog("ManageDesignation 324: exception:" + ex.Message + "::::::::" + ex.StackTrace);
              }
          }

          protected void btnExport_Click(object sender, EventArgs e)
          {
              try
              {
                  ltrErr.Text = "";
                  objmysqldb.ConnectToDatabase();
                  DataTable dtdesignation = objmysqldb.GetData("Select designation_id,Designation_Name,IsMasterData from designation_master where IsDelete=0 order by designation_id");
                  objmysqldb.disposeConnectionObj();
                  if (dtdesignation != null && dtdesignation.Rows.Count > 0)
                  {
                      ExportToExcel kg = new ExportToExcel();
                      string exportedfile = kg.ExportDataTableToExcel(dtdesignation, "List_Of_Designation");
                      Response.Redirect(ExportToExcel.EXPORT_URL + exportedfile, false);
                  }
                  else
                  {
                      ltrErr.Text = "No data exists.";
                  }
              }
              catch (Exception ex)
              {
                  Logger.WriteCriticalLog("Manage_Designation 349: exception:" + ex.Message + "::::::::" + ex.StackTrace);
              }
          }        
    }
}