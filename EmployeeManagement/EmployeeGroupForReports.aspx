<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeGroupForReports.aspx.cs" Inherits="EmployeeManagement.EmployeeGroupForReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1>Employee Report's Group List
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>

    </section>

    <section class="content">

        <div class="box box-primary">
            <div class="box-header">
               
                        <%--<div style="margin-top: 20px; margin-bottom: 10px; margin-bottom: 10px; width: 200px">
                            <asp:Button ID="btngrp" CssClass="btn btn-primary btn-block margin-bottom"
                                runat="server" Text="Create Report Group" OnClick="btngrp_Click" />
                        </div>--%>
                   

                <span style="font-size: large">Report Group List</span>
               <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">

                            <div class="col-md-2">
                                   <asp:ImageButton ID="btnadd" runat="server" Height="47px" ImageUrl="~/plus-2-256.png" Width="47px" OnClick="btngrp_Click"/>
                            </div>

                            <div class="col-md-2">
                            </div>
                            <div class="col-md-4" style="width: auto"></div>
                            <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                                <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                            </span>


                        </div>
                    </div>
                </div>

                <div class="row  table-responsive">
                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="false" OnRowCommand="grd_RowCommand" OnRowDataBound="grd_RowDataBound" OnRowEditing="grd_RowEditing">
                            <Columns>
                                <asp:TemplateField HeaderText="grpid" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblreportgrp" runat="server" Visible="false" Text='<%# Eval("report_grp_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Report Group Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReportgrpname" runat="server" Text='<%# Eval("report_grp_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="empid" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblempid" runat="server" Visible="false" Text='<%# Eval("emp_ids") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("emp_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%# Eval("report_grp_id")  %>' CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this Report Group?');" CommandName="del" CommandArgument='<%#Eval("report_grp_id")%>'>Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Header Image" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnimg" runat="server" CommandArgument='<%# Eval("report_grp_id")  %>' CommandName="Img" Text="Header Image"></asp:LinkButton>
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
