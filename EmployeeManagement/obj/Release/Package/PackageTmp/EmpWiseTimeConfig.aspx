<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EmpWiseTimeConfig.aspx.cs" Inherits="EmployeeManagement.EmpWiseTimeConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>EmployeeWise Time Config
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>
        <%-- <ol class="breadcrumb">
            <li><a href="../Time_Group_List.aspx"><i></i>Attendance Time Group List</a></li>
            <li class="active">Manage Attendance TimeGroup</li>
        </ol>--%>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-header">
                <div class="row">
                    <div class="col-md-1">
                        <asp:Label ID="lblmonth" CssClass="control-label" Text="SelectMonth" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlmonthlist" Style="width: 60%;" CssClass="form-control select2" OnSelectedIndexChanged="ddlmonthlist_SelectedIndexChanged" AutoPostBack="false" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="lblEmp" Text="Select Employee" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlEmp" Style="width: 70%;" CssClass="form-control select2" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged" AutoPostBack="false" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row">

                    <asp:Button ID="btnSearch" Style="margin-right: 50%;" CssClass="btn btn-primary center-block" Text="Search" runat="server" OnClick="btnSearch_Click" OnClientClick="return ValidateData()" />
                    <div class="col-md-4" style="width: auto"></div>
                </div>

            </div>
        </div>
        <div class="row  table-responsive">
            <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                <asp:GridView ID="grdData" CssClass="table table-bordered table-hover grdData" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowDataBound="grdData_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Day Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbldayid" runat="server" Visible="false" Text='<%# Eval("Day_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Emp Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblEmpId" runat="server" Visible="false" Text='<%# Eval("emp_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <div class="bootstrap-timepicker">
                                    <asp:Label ID="lblDate" runat="server" AutoPostBack="false" class="control-label" Text='<%# Eval("Date") %>' onkeypress="return isNumberKey(event);"></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IN Time">
                            <ItemTemplate>
                                <div class="bootstrap-timepicker">
                                    <asp:TextBox ID="txtIntime" runat="server" AutoPostBack="false" class="form-control timepicker" Text='<%# Eval("Intime") %>'> </asp:TextBox>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Out Time">
                            <ItemTemplate>
                                <div class="bootstrap-timepicker">
                                    <asp:TextBox ID="txtoutTime" runat="server" AutoPostBack="false" class="form-control timepicker" Text='<%# Eval("OutTime") %>'> </asp:TextBox>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Extreme Early">
                            <ItemTemplate>
                                <div class="bootstrap-timepicker">
                                    <asp:TextBox ID="txtearly" runat="server" AutoPostBack="false" class="form-control timepicker" Text='<%# Eval("ExtremeEarly") %>' onkeypress="return isNumberKey(event);"></asp:TextBox>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Extreme Late">
                            <ItemTemplate>
                                <div class="bootstrap-timepicker">
                                    <asp:TextBox ID="txtlate" runat="server" AutoPostBack="false" class="form-control timepicker" Text='<%# Eval("ExtremeLate") %>' onkeypress="return isNumberKey(event);"></asp:TextBox>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#EDE8E8"></HeaderStyle>
                </asp:GridView>
            </div>
        </div>
        <div class="box-body">
            <div class="row">
                <div style="margin-top: 20px; margin-bottom: 10px; margin-bottom: 10px" class="col-md-3">
                    <asp:Button ID="btnSave" CssClass="btn btn-primary"
                        runat="server" Text="Save" Style="width: 150px" OnClick="btnSave_Click" />
                </div>
                <div class="col-md-4">
                    <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                        <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                    </span>
                </div>
                <%--<div class="col-md-3">
                    <asp:Button ID="Button1" CssClass="btn btn-primary"
                        runat="server" Text="Export To Excel" Style="width: 150px" />
                </div>--%>
            </div>
        </div>
        <div id="divText" runat="server" >  <h3 style="text-align:center">Time Config Not Given Dates </h3></div>
        
        <div class="row  table-responsive">
            <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                <asp:GridView ID="GrdAttenConfig" CssClass="table table-bordered table-hover grdData" runat="server" HeaderStyle-BackColor="#ede8e8" OnRowDataBound="GrdAttenConfig_RowDataBound" AutoGenerateColumns="true" ShowHeader="false">
                    <Columns>

                        <asp:TemplateField HeaderText="Date" Visible="false">
                            <EditItemTemplate>
                                <asp:HyperLink ID="hl1" runat="server"></asp:HyperLink>
                                <%-- <a href="EmpWiseTimeConfig.aspx"> <asp:Label ID="lblDate" runat="server" Visible="True" Text='<%# Eval("Date") %>'></asp:Label> </a>--%>
                                <%--<asp:HyperLink runat="server" NavigateUrl='<%# Eval("Date", "#") %>'
                    Text='<%# Eval("Date") %>' />--%>
                            </EditItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </section>
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
    <script>
        $(function () {
            $('.dates').datepicker({
                autoclose: true
            });
            $(".timepicker").timepicker({
                showInputs: false
            });
        });
    </script>
    <script type="text/javascript">

        function ValidateData() {
            if (document.getElementById("<%=ddlEmp.ClientID%>").value == "-1") {
                 alert("Select Employee...");
                 document.getElementById("<%=ddlEmp.ClientID%>").focus();
                 return false;
             }
         }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentPlaceHolder1_grdData').DataTable({
                "fixedHeader": true,
                "paging": false,
                "lengthChange": true,
                "searching": true,
                "ordering": true,
                "info": false,
                "autoWidth": false
            });
        });
    </script>

</asp:Content>
