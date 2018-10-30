<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Manage_ReportingManager.aspx.cs" Inherits="EmployeeManagement.Manage_ReportingManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Manage Reporting Manager
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>

    </section>

    <section class="content">
        <div class="box box-primary">
            <div class="box-header">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblId" class="control-label" runat="server" Text="Add Reporting Manager"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddlEmployee" tabindex="1">
                            </select>
                        </div>
                        <br />
                    </div>

                    <div class="col-md-12">
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-6">
                            <asp:CheckBox ID="chksms" runat="server" class="form-group" Text="&nbsp;&nbsp;Send SMS" TabIndex="2" />
                        </div>

                    </div>

                    <div class="col-md-12">
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-6">
                            <asp:CheckBox ID="chkEmail" runat="server" class="form-group" Text="&nbsp;&nbsp;Send Email" TabIndex="3" />

                        </div>

                    </div>

                    <div class="col-md-12">
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-6">
                            <asp:CheckBox ID="chksinglesms" runat="server" class="form-group" Text="&nbsp;&nbsp;Send single SMS/Email to admin" TabIndex="4" />
                        </div>

                    </div>

                    <div class="col-md-12">
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-6">
                            <asp:CheckBox ID="chksms_Email" runat="server" class="form-group" Text="&nbsp;&nbsp;Send SMS/Email with punch time" TabIndex="5" />
                        </div>
                        <br />
                        <br />
                    </div>

                    <div class="col-md-12">
                        <div class="col-md-1">
                        </div>

                        <div class="col-md-3">
                            <asp:Button ID="btnsave" runat="server" class="btn btn-primary" Text="Add Reporting Manager" OnClientClick=" return validate()" OnClick="btnsave_Click" />

                        </div>

                        <div class="col-md-4">
                            <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                                <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="box box-primary">
            <div class="box-header">
                <span style="font-size: large">Reporting Manager List</span>
                <br />
                <br />
                <div class="row  table-responsive">
                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowCommand="grd_RowCommand" OnRowDataBound="grd_RowDataBound" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="Reporting_manager_id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmanager_id" runat="server" Visible="false" Text='<%# Eval("Reporting_manager_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Emp_id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmEmp_id" runat="server" Visible="false" Text='<%# Eval("Empid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reporting Manager Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmanager_Name" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Send SMS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsms" runat="server" Text='<%# Eval("Sms") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="cbSelectSMS" runat="server" AutoPostBack="false"></asp:CheckBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Send Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lblemail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="cbSelectemail" runat="server" AutoPostBack="false"></asp:CheckBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Send Single SMS/Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsms_email" runat="server" Text='<%# Eval("Single_Sms_Email") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="cbSelectsms_email" runat="server" AutoPostBack="false"></asp:CheckBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Send SMS/Email PunchTime">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsms_email_punch" runat="server" Text='<%# Eval("PunchTime_Sms_Email") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="cbSelectsms_email_punch" runat="server" AutoPostBack="false"></asp:CheckBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%# Eval("Reporting_manager_id")  %>' CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="btnupdate" runat="server" CommandName="Update" Text="Update" CommandArgument='<%# Eval("Reporting_manager_id") %>'></asp:LinkButton>
                                        <asp:LinkButton ID="btncancel" runat="server" CommandName="Cancel" Text="Cancel" CommandArgument='<%# Eval("Reporting_manager_id")  %>'></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this Reporting Manager?');" CommandName="del" CommandArgument='<%#Eval("Reporting_manager_id")%>'>Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div style="margin-top: 20px; margin-bottom: 10px; margin-bottom: 10px">
                    <asp:Button ID="btnExport" CssClass="btn btn-primary"
                        runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
                </div>
            </div>
        </div>


      
        <script type="text/javascript">
            function callfootercss() {
                var options = $('[id*=ltrErr] + .btn-group ul li.active');
                if (options.length == 0) {
                    $("#val_msg_wrap").show();
                    setTimeout(function () {
                        $("#val_msg_wrap").hide();
                    }, 3000);
                    return false;
                }
            }
            callfootercss();
        </script>
        <script type="text/javascript">
            function validate() {
                if (document.getElementById("<%=ddlEmployee.ClientID%>").value == "-1") {
                    alert("Reporting Manager Name  can not be blank");
                    document.getElementById("<%=ddlEmployee.ClientID%>").focus();
                    return false;
                }
                return true;
            }
        </script>
          <script type="text/javascript">
              $(document).ready(function () {
                  $('#ContentPlaceHolder1_grd').DataTable({
                      "fixedHeader": true,
                      "paging": true,
                      "lengthChange": true,
                      "searching": true,
                      "ordering": true,
                      "info": true,
                      "autoWidth": false
                  });
              });
        </script>

    </section>
</asp:Content>
