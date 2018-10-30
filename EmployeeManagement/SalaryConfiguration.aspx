<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="SalaryConfiguration.aspx.cs" Inherits="EmployeeManagement.SalaryConfiguration" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Salary Configuration
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
                            <asp:Label ID="lblprofile" runat="server" Text="Select Group"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList class="form-control select2" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlgrouplist_SelectedIndexChanged" ID="ddlgrouplist" TabIndex="1">
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-4" style="width: auto"></div>
                        <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                            <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                        </span>
                    </div>
                </div>
            </div>
        </div>

        <div class="box box-primary">
            <div class="box-header">
                <span style="font-size: large">Employee List For SalaryConfiguration</span>
            
            <div class="col-md-12">
                <div class="col-md-7">
                    <div class="row  table-responsive">
                        <div style="width: 90%; margin-left: 2%; margin-right: 15%;">
                            <asp:GridView ID="grdEmplist" CssClass="table table-bordered table-hover DataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowDataBound="grdEmplist_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="EMPID" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmp_id" runat="server" Visible="true" Text='<%# Eval("EmpId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FPID" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFpId" runat="server" Visible="true" Text='<%# Eval("FP_Id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmp_Name" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PayScale">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtpayscale" CssClass="form-control" onkeypress="return isNumberKey(event);" runat="server" Text='<%# Eval("PayScale") %>'></asp:TextBox>
                                            <%--  <input type="text" ID="txtpayscale" CssClass="form-control" onkeypress="return isNumberKey(event);" runat="server" Text='<%# Eval("PayScale") %>' />--%>
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
                                    <asp:Button ID="btnsave" CssClass="btn btn-primary"
                                        runat="server" Text="Save" Style="width: 150px" OnClick="btnsave_Click" />
                                </div>
                                <%--<div class="col-md-4" style="width: auto"></div>
                            <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                                <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                            </span>
                        </div>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-2">
                    <asp:Button ID="Button1" CssClass="btn btn-primary"
                        runat="server" Text="Save" Style="width: 70px; float: left" OnClick="Button1_Click" />
                </div>
            </div>
            </div>
        </div>


        <script type="text/javascript">
            $(document).ready(function () {
                $('#ContentPlaceHolder1_grdEmplist ').DataTable({
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
    </section>
</asp:Content>
