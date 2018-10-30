<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LeftEmployee.aspx.cs" Inherits="EmployeeManagement.LeftEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Left Employee
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>
        <ol class="breadcrumb">           
            <li><a href="../Search_Employee.aspx"><i></i>Search Employee</a></li>
            <li class="active">Left Employee</li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-header">
                <div class="row">

                    <div class="col-md-12"></div>
                    <div class="col-md-1">
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label5" class="control-label" runat="server" Text="Left Date"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtleftDate" runat="server" class="form-control" AutoPostBack="false"></asp:TextBox>
                    </div>
                    <div class="col-md-4" id="val_msg_wrap">
                        <span style="color: red; margin-left: 15px">
                            <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                        </span>
                    </div>

                </div>
                <br />
                <div class="row">
                    <div class="col-md-12"></div>
                    <div class="col-md-1">
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label6" class="control-label" runat="server" Text="Left Reason"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtreason" runat="server" class="form-control" AutoPostBack="false" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="col-md-4">

                        <asp:Button ID="btnsave" runat="server" class="btn btn-primary" Text="Left Employee"  OnClientClick=" return validate()" OnClick="btnsave_Click" />
                    </div>

                </div>

            </div>
        </div>

           <div class="box box-primary">
            <div class="box-header">
                <span style="font-size: large">Left Employee List</span>
                <br />
                <br />

                <div class="row  table-responsive">
                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowCommand="grd_RowCommand" OnRowDataBound="grd_RowDataBound" OnRowEditing="grd_RowEditing" >
                            <Columns>
                                <asp:TemplateField HeaderText="Left_Employee_id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLeft_Employee_id" runat="server" Visible="false" Text='<%# Eval("Left_Employee_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Emp_id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmp_id" runat="server" Visible="false" Text='<%# Eval("Emp_Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Full Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblemp__Name" runat="server" Text='<%# Eval("EmpFullName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Left Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldate" runat="server" Text='<%# Eval("Left_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Left Reason">
                                    <ItemTemplate>
                                        <asp:Label ID="lblreason" runat="server" Text='<%# Eval("Left_Reason") %>'></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>

                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdelete" runat="server" OnClientClick="return confirm('Are you sure you want to Revert this Employee?');" CommandName="status" CommandArgument='<%#Eval("Left_Employee_id")+":"+Eval("Emp_Id")%>'>Revert</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>

                </div>

              <div style="margin-top: 20px; margin-bottom: 10px; margin-bottom: 10px">
                        <asp:Button ID="btnExport" CssClass="btn btn-primary" 
                            runat="server" Text="Export To Excel" Visible="false" OnClick="btnExport_Click" />
                    </div>
            </div>

        </div>
        <asp:HiddenField ID="HiddenField1" runat="server" />

        <script type="text/javascript">
            function validate() {
                if (document.getElementById("<%=txtleftDate.ClientID%>").value == "") {
                    alert("Left Date can not be blank");
                    document.getElementById("<%=txtleftDate.ClientID%>").focus();
                    return false;
                }
                return true;
            }
        </script>
        

        <script>
            $(function () {
                $('#ContentPlaceHolder1_txtleftDate').datepicker({
                    autoclose: true
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
