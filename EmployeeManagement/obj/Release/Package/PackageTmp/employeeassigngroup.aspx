<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="employeeassigngroup.aspx.cs" Inherits="EmployeeManagement.employeeassigngroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Assign Employee To Module Group
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>
       <%-- <ol class="breadcrumb">
         
            <li class="active">Employee Assign Group</li>
        </ol>--%>
    </section>
    <section class="content">
        <div class="box box-default">
            <div class="box-header with-border">
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="lblgroup" class="control-label" runat="server" Text="Employee Name"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <select class=" form-control select2   select2-selection--single" style="width: 110%;" runat="server" id="ddlemplist">
                            </select>
                        </div>
                        <div class="col-md-4" style="width: auto"></div>
                        <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                            <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                        </span>
                    </div>
                    <br />
                    <br />
                    <br />
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="lblgrpname" class="control-label" runat="server" Text="Group List"></asp:Label>
                            <asp:HiddenField ID="grp_idHidden" runat="server" />
                        </div>
                        <div class="col-md-3">
                            <select class=" form-control select2   select2-selection--single" style="width: 110%;" runat="server" id="ddlgrouplist">
                            </select>
                        </div>
                        <div class="col-md-4" style="margin-left:30px">
                            <asp:Button ID="BtnSave" runat="server" class="btn btn-primary" Text="Save" Width="100px" OnClick="BtnSave_Click" OnClientClick="return validate()" />
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="box box-primary">
            <div class="box-header">
                <span style="font-size: large">Employee Group List</span>
                <br />
                <br />
                <div class="row  table-responsive">
                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="false" OnRowCommand="grd_RowCommand" OnRowDataBound="grd_RowDataBound" OnRowEditing="grd_RowEditing" DataKeyNames="emp_module_id">
                            <Columns>
                                <asp:TemplateField HeaderText="Emp Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblempid" runat="server" Visible="false" Text='<%# Eval("emp_module_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgroupname" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmodulename" runat="server" Text='<%# Eval("group_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%# Eval("emp_module_id")  %>' CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this Employee?');" CommandName="del" CommandArgument='<%#Eval("emp_module_id")%>'>Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#EDE8E8"></HeaderStyle>
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
            function validate()
            {
                if (document.getElementById("<%=ddlemplist.ClientID%>").value == "-1") {
                    alert("Employee Name Feild can not be blank");
                    
                    return false;
                }
                if (document.getElementById("<%=ddlgrouplist.ClientID%>").value == "-1") {
                    alert("Group Name Feild can not be blank");

                    return false;
                }
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
