<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Time_Group_List.aspx.cs" Inherits="EmployeeManagement.Time_Group_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Attendance Time Group List
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
                <span style="font-size: large">Time Group List</span>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">

                            <div class="col-md-2">
                                   <asp:ImageButton ID="btnadd" runat="server" Height="47px" ImageUrl="~/plus-2-256.png" Width="47px" OnClick="btnadd_Click"/>
                            </div>

                            <div class="col-md-2">
                            </div>
                            <asp:HiddenField ID="time_grp" runat="server" />
                            <div class="col-md-4" style="width: auto"></div>
                            <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                                <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                            </span>


                        </div>
                    </div>
                </div>

                <div class="row  table-responsive">
                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound" OnRowCommand="grd_RowCommand" OnRowEditing="grd_RowEditing">
                            <Columns>
                                <asp:TemplateField HeaderText="Group_id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrp_id" runat="server" Visible="false" Text='<%# Eval("Group_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblname" runat="server" Text='<%# Eval("Group_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sent Absent SMS After">
                                    <ItemTemplate>
                                        <asp:Label ID="lblabsent" runat="server" Text='<%# Eval("Absent_SMS_After") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sent Forget OutPunch SMS After">

                                    <ItemTemplate>
                                        <asp:Label ID="lblout" runat="server" Text='<%# Eval("OutPuch_SMS_After") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Applicable Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldate" runat="server" Text='<%# Eval("date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%# Eval("Group_id") %>' CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this Group?');" CommandName="del" CommandArgument='<%#Eval("Group_id")%>'>Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#EDE8E8"></HeaderStyle>
                        </asp:GridView>
                    </div>
                </div>
                <div style="margin-top: 20px; margin-bottom: 10px; margin-bottom: 10px">
                    <asp:Button ID="btnExport" CssClass="btn btn-primary"
                        runat="server" Text="Export To Excel" Visible="false" OnClick="btnExport_Click" />
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
