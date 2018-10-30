<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="addeditprofile.aspx.cs" Inherits="EmployeeManagement.addeditprofile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Add/Edit Profile
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>
        <%--<ol class="breadcrumb">
           <i class="fa fa-dashboard"></i>
            <li><a href="../Search_Employee.aspx"><i></i>Search Employee</a></li>
            <li class="active">Employee Module Group</li>
        </ol>--%>
    </section>
    <section class="content">

        <div class="box box-primary">
            <div class="box-header">

                <span style="font-size: large">Add/Edit Employee Profile</span>
                <span style="color: red; margin-left: 150px; width: auto" id="val_msg_wrap">
                    <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                </span>
                <br />
                <br />
                <asp:ImageButton ID="btnadd" runat="server" Height="47px" ImageUrl="~/plus-2-256.png" Width="47px" OnClick="btnadd_Click" /><br />
                <div class="row  table-responsive">
                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound" OnRowCommand="grd_RowCommand" OnRowEditing="grd_RowEditing">
                            <Columns>
                                <asp:TemplateField HeaderText="Profile Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprofileid" runat="server" Visible="false" Text='<%# Eval("Profile_Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Profile Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprofilename" runat="server" Text='<%# Eval("Profile_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Profile Query Text">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmodulename" runat="server" Text='<%# Eval("Profile_Text") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%# Eval("Profile_Id")  %>' CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this Profile?');" CommandName="del" CommandArgument='<%#Eval("Profile_Id")%>'>Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <%--<HeaderStyle BackColor="#EDE8E8"></HeaderStyle>--%>
                        </asp:GridView>
                    </div>
                </div>

            </div>
        </div>
        <script type="text/javascript">
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
