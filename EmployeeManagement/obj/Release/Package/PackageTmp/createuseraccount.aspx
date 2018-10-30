<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="createuseraccount.aspx.cs" Inherits="EmployeeManagement.createuseraccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Create User Account
                <small>
                    <asp:Label runat="server" ID="lblDateTime"> </asp:Label>

                </small>
        </h1>
        <%--<ol class="breadcrumb">
            <li class="active">Manage Religion</li>
        </ol>--%>
    </section>
    <section class="content">
        <div class="box box-default">
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="lblempname" runat="server" Text="Employee Name"></asp:Label><asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtname" runat="server" class="form-control" AutoPostBack="false"></asp:TextBox>

                        </div><span style="color: red; margin-left: 15px" id="val_msg_wrap">
                                        <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                                    </span>
                    </div>
                      <br />
                    <br />
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label4" runat="server" Text="ContactNo"></asp:Label><asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtcn" runat="server" class="form-control" AutoPostBack="false"></asp:TextBox>

                        </div>
                    </div>
                      <br />
                    <br />
                      <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label6" class="control-label" runat="server" Text="Gender"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:RadioButton ID="rdbmale" runat="server" Text="&nbsp;Male" Checked="true" GroupName="gender"  TabIndex="8"/>
                                        &nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbFemale" runat="server" Text="&nbsp;Female" GroupName="gender"  TabIndex="9"/>
                          </div>          </div>

                    <br />
                    <br />
                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="lblusername" runat="server" Text="Address"></asp:Label><asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtAddress" runat="server" class="form-control" AutoPostBack="false" TextMode="MultiLine"></asp:TextBox>

                        </div>
                    </div>
                    <br /><br /><br />
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="lblpassword" runat="server" Text="DateOfJoin" class="control-label"></asp:Label><asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtdoj" runat="server"  class="form-control dates"  AutoPostBack="false" ></asp:TextBox>

                        </div>
                        <div class="col-md-4" style="width: auto"></div>

                    </div>
                    <br />
                    <br />
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label7" runat="server" Text="Date of birth" class="control-label"></asp:Label><asp:Label ID="Label8" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtdob" runat="server"  class="form-control dates"  AutoPostBack="false" ></asp:TextBox>

                        </div>
                        <br />
                        
                        <div class="col-md-4" style="width: auto"></div>

                    </div>
                    <br />
                    
                    
                    <br />
                    
<div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label9" runat="server" Text="Specilazation" class="control-label"></asp:Label><asp:Label ID="Label10" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtspe" runat="server"  class="form-control"  AutoPostBack="false" ></asp:TextBox>

                        </div>
                        <div class="col-md-4" style="width: auto"></div>

                    </div>
                    
                    <br />

                    <br />
                   <div class="col-md-2">
                            <asp:Label ID="Label11" runat="server" Text="Email" class="control-label"></asp:Label><asp:Label ID="Label12" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </div>
                    <div class="col-md-3">
                            <asp:TextBox ID="txtemail" runat="server"  class="form-control"  AutoPostBack="false" ></asp:TextBox>

                        </div>
                    <div class="col-md-12">
                        <%--<div class="col-md-2">
                            <asp:Label ID="lblmodule" runat="server" Text="Select Module"></asp:Label><asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label><asp:HiddenField ID="grp_idHidden" runat="server" />
                        </div>--%>
                        <div class="col-md-3" style="width:400px">
                           
                                
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical">
                                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound" AutoPostBack="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="module_id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmoduleid" runat="server" Visible="false" Text='<%# Eval("rowid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Show" ControlStyle-Width="20px">
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblcheck" runat="server" Text="Select"></asp:Label>
                                                        <%--  <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="false"></asp:CheckBox>--%>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <asp:CheckBox ID="cbSelect" runat="server" AutoPostBack="false"></asp:CheckBox>
                                                        <asp:Label ID="lblisshow" runat="server" Text='<%# Eval("rowid") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Module Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmodulename" runat="server" Text='<%# Eval("modulename") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel><br />
                                    <div style="margin-left:50px"><asp:Button ID="btnsave" runat="server" class="btn btn-primary" Text="Create User" OnClick="btnsave_Click" />
                                   </div>
                               
                            </div>
                        
                    </div>
                </div>
            </div></div>
        
        <div class="box box-primary">
           
            <div class="box-header">
                <span style="font-size: large">User List</span>
                <br />
                <br />
                <div class="row  table-responsive">
                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                                <asp:GridView ID="grduser" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowDataBound="grduser_RowDataBound" OnRowCommand="grduser_RowCommand" OnRowEditing="grduser_RowEditing" AutoPostBack="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="module_id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbluserid" runat="server" Visible="false" Text='<%# Eval("User_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblempname" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblusername" runat="server" Text='<%# Eval("User_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Password">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpassword" runat="server" Text='<%# Eval("User_Password") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%# Eval("User_id") %>' CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this User?');" CommandName="del" CommandArgument='<%#Eval("User_id")%>'>Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="col-md-3"></div>
                                    <div style="margin-top: 20px; margin-bottom: 10px; margin-bottom: 10px" class="col-md-3">
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-2">
                    </div>
                </div>
           
             <script type="text/javascript">
                 $(document).ready(function () {
                     $('#ContentPlaceHolder1_grduser').DataTable({
                         "fixedHeader": true,
                         "paging": false,
                         "lengthChange": true,
                         "searching": true,
                         "ordering": true,
                         "info": true,
                         "autoWidth": false
                     });
                 });
        </script>

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
        <%--        <script type="text/javascript">
            function isNumberKey(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    if (charCode == 45) {
                        return true;
                    }
                    else {
                        return false;
                    }

                return true;
            }
        </script>--%>
        <script>
            $(function () {
                $('.dates').datepicker({
                    autoclose: true
                });
            });
    </script>
    </section>
</asp:Content>
