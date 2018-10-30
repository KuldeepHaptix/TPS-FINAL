using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Drawing;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.Script.Serialization;


namespace EmployeeManagement
{
    public partial class Mange_Religion : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        
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
                        header.Text = "Manage Religion";
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
                    txtreligionnames.Focus();
                    showgrid();

                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Religion 50: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        protected void showgrid()
        {
            objmysqldb.ConnectToDatabase();
            try
            {
                ltrErr.Text = "";
                DataTable dtreligion = objmysqldb.GetData("Select religion_id,religion_name,IsMasterData from religion_master where IsDelete=0 order by religion_id");
                objmysqldb.disposeConnectionObj();
                if (dtreligion != null)
                {
                    grd.DataSource = dtreligion;
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
                Logger.WriteCriticalLog("Manage_Religion 79: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
                Logger.WriteCriticalLog("Manage_Religion 92: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
                        int relid = 0;
                        int masterdata = 0;
                        int.TryParse(arg[1], out relid);
                        objmysqldb.ConnectToDatabase();
                        DataTable dtreligion = objmysqldb.GetData("Select EmpReligion from employee_master  WHERE  EmpReligion =" + relid + "  ");
                        DataTable dt = objmysqldb.GetData("select * from religion_master where religion_id=" + relid + "");
                        if (dtreligion != null && dtreligion.Rows.Count > 0)
                        {

                            ltrErr.Text = "Employee Assign to select Category so can't delete it";
                            return;
                        }
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int.TryParse(dt.Rows[0]["IsMasterData"].ToString(), out masterdata);

                            if (masterdata == 0)
                            {
                                ltrErr.Text = "Select Religion is Masterdata,So can't delete it";
                                return;
                            }



                            else
                            {
                                int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                                DateTime currenttime = Logger.getIndiantimeDT();
                                string query = "Update religion_master set IsDelete=1,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + " where religion_id=" + int.Parse(arg[0]) + " ";
                                objmysqldb.OpenSQlConnection();
                                int res = objmysqldb.InsertUpdateDeleteData(query);
                                if (res != 1)
                                {
                                    ltrErr.Text = "Please Try Again.";

                                    Logger.WriteCriticalLog("Manage_Religion 140 Update error.");
                                }
                                else
                                {
                                    grd.EditIndex = -1;
                                    showgrid();
                                    ltrErr.Text = "Religion Delete Successfully";
                                    txtreligionnames.Text = "";
                                }
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
                Logger.WriteCriticalLog("Manage_Religion 166: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
                Logger.WriteCriticalLog("Manage_Religion 188: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                int masterdata = 0;
                TextBox txtreligionnames = (TextBox)grd.Rows[e.RowIndex].FindControl("txtreligion_name");

                Label lbid = (Label)grd.Rows[e.RowIndex].FindControl("lblreligion_id");
                string relListId = lbid.Text;
                string relName = txtreligionnames.Text.ToString().Trim();
                int relid = 0;
                int.TryParse(relListId, out relid);
                DateTime currenttime = Logger.getIndiantimeDT();
                if (relName != "" && relid > 0)
                {
                    objmysqldb.ConnectToDatabase();
                    DataTable dtreligion = objmysqldb.GetData("Select * from religion_master WHERE IsDelete=0 and religion_name like '" + relName + "' and religion_id<> " + int.Parse(relListId) + "  ");

                    DataTable dtrelDetails = objmysqldb.GetData("Select * from religion_master WHERE religion_id= " + int.Parse(relListId) + "  ");

                    if (dtreligion != null && dtreligion.Rows.Count > 0)
                    {

                        ltrErr.Text = "Same Religion Name Is Already Exist";
                        return;

                    }

                    if (dtrelDetails != null && dtrelDetails.Rows.Count > 0)
                    {
                        int.TryParse(dtrelDetails.Rows[0]["IsMasterData"].ToString(), out masterdata);

                        if (masterdata == 0)
                        {

                            ltrErr.Text = "Select Religion is Masterdata,So can't delete it";
                            return;
                        }
                        else
                        {
                            //int.TryParse(dtcatDetails.Rows[0]["category_id"].ToString(), out oldcatid);
                            objmysqldb.AddCommandParameter("religionname", relName);
                            int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                            string query = "Update religion_master set religion_name=?religionname,modify_datetime=" + currenttime.Ticks + ",IsUpdate=1,UserID=" + user_id + "  where religion_id=" + int.Parse(relListId) + " ";
                            objmysqldb.OpenSQlConnection();
                            int res = objmysqldb.InsertUpdateDeleteData(query);
                            if (res != 1)
                            {
                                ltrErr.Text = "Please Try Again.";

                                Logger.WriteCriticalLog("Manage_Religion 243 Update error.");
                                return;
                            }
                            grd.EditIndex = -1;
                            showgrid();
                            ltrErr.Text = "Religion Name Updated Successfully";
                        }

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
                Logger.WriteCriticalLog("Manage_Religion 272: exception:" + ex.Message + "::::::::" + ex.StackTrace);
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
                {

                }
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
                Logger.WriteCriticalLog("Manage_Religion 308: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
              
                #region demo store procedure code
                try
                {
                    string constr = "Server=localhost;Database=employee_management;Uid=root;Pwd=123456";
                    using (MySqlConnection con = new MySqlConnection(constr))
                    {
                        con.Open();
                        //objmysqldb.ConnectToDatabase();
                        //objmysqldb.OpenSQlConnection();


                        MySqlCommand objcmd = new MySqlCommand();
                        objcmd.Connection = con;


                        #region  Select
                       

                        objcmd.CommandText = "DROP PROCEDURE IF EXISTS GetReligion_data";
                        objcmd.ExecuteNonQuery();

                        //In parameter
                        objcmd.CommandText = "CREATE  PROCEDURE  GetReligion_data() BEGIN select * from  religion_master where IsDelete=0; END";
                        objcmd.ExecuteNonQuery();

                        //objmysqldb.InsertUpdateDeleteData("DROP PROCEDURE IF EXISTS GetReligion_data");
                        //objmysqldb.InsertUpdateDeleteData("CREATE  PROCEDURE  GetReligion_data() BEGIN select * from  religion_master where IsDelete=0; END");


                        using (MySqlCommand cmd = new MySqlCommand("GetReligion_data", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                            }
                        }
                        #endregion


                        #region Parameter rise Select In parameter
                        //objmysqldb.InsertUpdateDeleteData("DROP PROCEDURE IF EXISTS GetReligion_dataWithName");
                        //objmysqldb.InsertUpdateDeleteData("CREATE  PROCEDURE  GetReligion_dataWithName(IN Name Varchar(255)) BEGIN select * from  religion_master where IsDelete=0 and religion_name=Name; END");

                        objcmd.CommandText = "DROP PROCEDURE IF EXISTS GetReligion_dataWithName";
                        objcmd.ExecuteNonQuery();

                        //In parameter
                        objcmd.CommandText = "CREATE  PROCEDURE  GetReligion_dataWithName(IN Name Varchar(255)) BEGIN select * from  religion_master where IsDelete=0 and religion_name=Name; END";

                        objcmd.ExecuteNonQuery();

                        using (MySqlCommand cmd1 = new MySqlCommand("GetReligion_dataWithName", con))
                        {
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.Add("?Name", txtreligionnames.Text.ToString());
                            using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd1))
                            {
                                //In Parameter o/p
                                DataTable dt = new DataTable();
                                sda.Fill(dt);

                            }
                        }
                        #endregion


                        #region Parameter rise Select IN Out parameter

                        objcmd.CommandText = "DROP PROCEDURE IF EXISTS GetReligion_dataWithName1";
                        objcmd.ExecuteNonQuery();

                        // In  out parameter
                        objcmd.CommandText = "CREATE  PROCEDURE  GetReligion_dataWithName1(IN id int(11), out Name VARCHAR(130)) BEGIN select religion_name into  Name from  religion_master where IsDelete=0 and religion_id=id; END";
                        objcmd.ExecuteNonQuery();

                        using (MySqlCommand cmd1 = new MySqlCommand("GetReligion_dataWithName1", con))
                        {
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.Add("?id", 1);

                            cmd1.Parameters.Add("?Name", MySqlDbType.VarChar, 55);
                            cmd1.Parameters["?Name"].Direction = ParameterDirection.Output;
                            cmd1.ExecuteNonQuery();
                            string aaa = "Religion Name: " + (string)cmd1.Parameters["?Name"].Value;
                        }
                        #endregion

                        #region Insert In Parameter
                        //objmysqldb.InsertUpdateDeleteData("DROP PROCEDURE IF EXISTS GetReligion_dataWithName");
                        //objmysqldb.InsertUpdateDeleteData("CREATE  PROCEDURE  InsertReligion_data(IN Name Varchar(255)) BEGIN select * from  religion_master where IsDelete=0 and religion_name=Name; END");
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        objcmd.CommandText = "DROP PROCEDURE IF EXISTS InsertReligion_data";
                        objcmd.ExecuteNonQuery();

                        //in parameter
                        objcmd.CommandText = "CREATE  PROCEDURE  InsertReligion_data(IN Name Varchar(255), IN UserID INT(3), IN modify_datetime decimal(28,0), IN DOC decimal(28,0),IN IsUpdate INT(3),IN IsMasterData  INT(3),IN IsDelete  INT(3)) BEGIN Insert INTO religion_master (religion_name,UserID,modify_datetime,DOC,IsUpdate,IsMasterData,IsDelete) Values (Name,UserID,modify_datetime,DOC,IsUpdate,IsMasterData,IsDelete); END";


                        objcmd.ExecuteNonQuery();
                        using (MySqlCommand cmd1 = new MySqlCommand("InsertReligion_data", con))
                        {
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.Add("?Name", txtreligionnames.Text.ToString());
                            cmd1.Parameters.Add("?UserID", user_id);
                            cmd1.Parameters.Add("?modify_datetime", Logger.getIndiantimeDT().Ticks);
                            cmd1.Parameters.Add("?DOC", Logger.getIndiantimeDT().Ticks);
                            cmd1.Parameters.Add("?IsUpdate", 1);
                            cmd1.Parameters.Add("?IsMasterData", "0");
                            cmd1.Parameters.Add("?IsDelete", "0");
                            cmd1.ExecuteNonQuery();
                        }
                        #endregion

                        #region Insert In Out Parameter

                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        objcmd.CommandText = "DROP PROCEDURE IF EXISTS InsertReligion_data1";
                        objcmd.ExecuteNonQuery();

                        //OUT parameter
                        objcmd.CommandText = "CREATE  PROCEDURE  InsertReligion_data1(IN Name Varchar(255), IN UserID INT(3), IN modify_datetime decimal(28,0), IN DOC decimal(28,0),IN IsUpdate INT(3),IN IsMasterData  INT(3),IN IsDelete  INT(3),OUT lastinsertid INT(11),OUT cat  varchar(255),OUT rows  INT(11) ) BEGIN Insert INTO religion_master (religion_name,UserID,modify_datetime,DOC,IsUpdate,IsMasterData,IsDelete) Values (Name,UserID,modify_datetime,DOC,IsUpdate,IsMasterData,IsDelete); " + "select LAST_INSERT_ID() into lastinsertid; " + "select Name into cat; " + "select ROW_COUNT() into rows; " + "END;";
                        objcmd.ExecuteNonQuery();
                        using (MySqlCommand cmd1 = new MySqlCommand("InsertReligion_data1", con))
                        {
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.Add("?Name", txtreligionnames.Text.ToString());
                            cmd1.Parameters.Add("?UserID", user_id);
                            cmd1.Parameters.Add("?modify_datetime", Logger.getIndiantimeDT().Ticks);
                            cmd1.Parameters.Add("?DOC", Logger.getIndiantimeDT().Ticks);
                            cmd1.Parameters.Add("?IsUpdate", 1);
                            cmd1.Parameters.Add("?IsMasterData", "0");
                            cmd1.Parameters.Add("?IsDelete", "0");
                            cmd1.Parameters.Add("?lastinsertid", MySqlDbType.Int64);
                            cmd1.Parameters["?lastinsertid"].Direction = ParameterDirection.Output;

                            cmd1.Parameters.Add(new MySqlParameter("?cat", MySqlDbType.VarChar));
                            cmd1.Parameters["?cat"].Direction = ParameterDirection.Output;

                            cmd1.Parameters.Add(new MySqlParameter("?rows", MySqlDbType.Int32));
                            cmd1.Parameters["?rows"].Direction = ParameterDirection.Output;

                            int retval = cmd1.ExecuteNonQuery();
                            long lastinsertid = (Int64)cmd1.Parameters["?lastinsertid"].Value; //Get Last row insert id
                            int rows = (int)cmd1.Parameters["?rows"].Value; //Get no of row insert
                            string ans = (string)cmd1.Parameters["?cat"].Value; //Get data
                        }
                        #endregion


                        #region Update
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        objcmd.CommandText = "DROP PROCEDURE IF EXISTS UpdateReligion_data";
                        objcmd.ExecuteNonQuery();

                        objcmd.CommandText = "CREATE  PROCEDURE  UpdateReligion_data(IN Name Varchar(255), IN UserID INT(3), IN modify_datetime decimal(28,0),IN IsUpdate INT(3),IN IsMasterData INT(3),IN IsDelete INT(3),IN row  INT(11)) BEGIN Update religion_master set religion_name=Name,UserID=UserID,modify_datetime=modify_datetime,IsUpdate=IsUpdate,IsMasterData=IsMasterData,IsDelete=IsDelete where religion_id=row; END";
                        objcmd.ExecuteNonQuery();
                        using (MySqlCommand cmd1 = new MySqlCommand("UpdateReligion_data", con))
                        {
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.Add("?Name", txtreligionnames.Text.ToString());
                            cmd1.Parameters.Add("?UserID", user_id);
                            cmd1.Parameters.Add("?modify_datetime", Logger.getIndiantimeDT().Ticks);
                            cmd1.Parameters.Add("?IsUpdate", 1);
                            cmd1.Parameters.Add("?IsMasterData", "0");
                            cmd1.Parameters.Add("?IsDelete", "0");
                            cmd1.Parameters.Add("?row", 13);
                            cmd1.ExecuteNonQuery();
                        }
                        #endregion

                        con.Close();
                    }
                }
                catch (Exception aa)
                {

                }
                #endregion

                ltrErr.Text = "";
                if (!txtreligionnames.Text.ToString().Equals(""))
                {
                    objmysqldb.ConnectToDatabase();
                    DataTable dtreligion = objmysqldb.GetData("Select * from religion_master WHERE (religion_name like '" + txtreligionnames.Text.ToString() + "') and  IsDelete=0  ");



                    if (dtreligion != null && dtreligion.Rows.Count > 0)
                    {
                        ltrErr.Text = " Religion  Is Already Exist";
                        return;
                    }
                    DateTime currenttime = Logger.getIndiantimeDT();
                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    objmysqldb.AddCommandParameter("religionname", txtreligionnames.Text.ToString());
                    string query = "Insert into  religion_master (religion_name,UserID,modify_datetime,DOC,IsUpdate,IsMasterData,IsDelete) values (?religionname," + user_id + "," + currenttime.Ticks + "," + currenttime.Ticks + ",1,1,0) ";
                    objmysqldb.OpenSQlConnection();
                    int res = objmysqldb.InsertUpdateDeleteData(query);
                    if (res != 1)
                    {
                        ltrErr.Text = "Please Try Again.";

                        Logger.WriteCriticalLog("Manage_Religion 339 Update error.");
                    }
                    else
                    {
                        grd.EditIndex = -1;
                        showgrid();
                        txtreligionnames.Text = "";
                        ltrErr.Text = "Religion Save Successfully";

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
                Logger.WriteCriticalLog("Manage_Religion 358: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                ltrErr.Text = "";
                objmysqldb.ConnectToDatabase();
                DataTable dtreligion = objmysqldb.GetData("Select religion_id,religion_name,IsMasterData from religion_master where IsDelete=0 order by religion_id");
                objmysqldb.disposeConnectionObj();
                if (dtreligion != null && dtreligion.Rows.Count > 0)
                {
                    ExportToExcel kg = new ExportToExcel();
                    string exportedfile = kg.ExportDataTableToExcel(dtreligion, "List_Of_Religion");
                    Response.Redirect(ExportToExcel.EXPORT_URL + exportedfile, false);
                }
                else
                {
                    ltrErr.Text = "No data exists";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Manage_Category 388: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }

        public class test
        {
            public string id { get; set; }
            public string name { get; set; }
           public test()
            {
                id = "";
                name = "";
            }
        }
         [System.Web.Services.WebMethod]
        public static object binDdata(string id)
        {
            try
            {
                MySQLDB dbc= new MySQLDB();
                var objectdata = new List<test>();
                JavaScriptSerializer js = new JavaScriptSerializer();
                dbc.ConnectToDatabase();
                DataTable dt= dbc.GetData("Select religion_id,religion_name,IsMasterData from religion_master where IsDelete=0 order by religion_id");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var model = new test();
                    model.id = dt.Rows[i]["religion_id"].ToString();
                    model.name = dt.Rows[i]["religion_name"].ToString();
                    objectdata.Add(model);
                }

                string aa = js.Serialize(objectdata);
                return objectdata;
            }
            catch(Exception aa)
            {
                return "";
            }
        }
    }
}