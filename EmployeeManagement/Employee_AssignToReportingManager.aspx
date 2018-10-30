<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Employee_AssignToReportingManager.aspx.cs" Inherits="EmployeeManagement.Employee_AssignToReportingManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Employee Assign To Reporting Manager
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>

    </section>
    <section class="content">

        <div class="box box-default">
            <div class="box-header with-border">
                <h3 class="box-title">Reporting Manager</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">

                        <div class="col-md-3">
                            <asp:Label ID="lblprofile" runat="server" Text="Select Reporting Manager"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddlmanager" tabindex="1">
                            </select>
                        </div>

                    </div>
                    <br />
                    <br />
                    <br />

                    <div class="col-md-12">

                        <div class="col-md-2"></div>
                        <div class="col-md-2" style="margin-left: 90px">
                            <asp:Button ID="BtnSearch" runat="server" class="btn btn-primary" Text="Search" TabIndex="2" Width="110px" OnClick="BtnSearch_Click" />
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-4" style="width: auto"></div>
                        <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                            <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                        </span>
                    </div>
                </div>


            </div>
        </div>


        <%-- <div class="box box-primary">
            <div class="box-header">--%>

        <div class="row  table-responsive">


            <div class="col-md-5">
                <div class="box box-primary">
                    <div class="box-header">
                        <span style="font-size: large">UnAssign Employee List</span>
                        <br />
                        <br />
                        <%--<div class="col-md-6">
                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddlSelect">
                            </select>
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox ID="txtsearch" runat="server" class="form-control" AutoPostBack="false" onkeyup="Search_GridviewTest(this)"></asp:TextBox>
                        </div>--%>

                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound" Style="overflow: scroll">
                            <Columns>
                                <asp:TemplateField >
                                     <HeaderTemplate>
                                        <%--<asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true"
                                            OnCheckedChanged="chkHeader_CheckedChanged"></asp:CheckBox>--%>
                                          <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);" />

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%--  <asp:CheckBox ID="cbSelect" runat="server" AutoPostBack="true"
                                            OnCheckedChanged="cbSelect_CheckedChanged"></asp:CheckBox>--%>
                                       <%-- <asp:CheckBox ID="cbSelect" runat="server" AutoPostBack="false"></asp:CheckBox>--%>
                                         <asp:CheckBox ID="cbSelect" runat="server" onclick = "Check_Click(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Emp_id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmp_id" runat="server" Visible="false" Text='<%# Eval("empId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmempName" runat="server" Text='<%# Eval("Fullname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact">
                                    <ItemTemplate>
                                        <asp:Label ID="lblempPhno" runat="server" Text='<%# Eval("EmpPhone") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#EDE8E8"></HeaderStyle>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="col-md-2">

                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <div>
                    <asp:Button ID="btnassign" runat="server" class="btn btn-primary" Text=">>" Width="100px"  OnClick="btnassign_Click" />
                </div>
                <br />
                <br />
                <br />
                <div>
                    <asp:Button ID="btnUnAssign" runat="server" class="btn btn-primary" Text="<<" Width="100px" OnClick="btnUnAssign_Click" />
                </div>

                <br />
                <br />
                <br />
                <div>
                    <asp:Button ID="btnsave" runat="server" class="btn btn-primary" Text="Save" Width="100px" OnClick="btnsave_Click" />
                </div>

            </div>


            <div class="col-md-5">
                <div class="box box-primary">
                    <div class="box-header">
                        <span style="font-size: large">Assign Employee List</span>
                        <br />
                        <br />
                        <asp:GridView ID="GridAssign" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowDataBound="GridAssign_RowDataBound">
                            <Columns>
                                <asp:TemplateField >
                                     <HeaderTemplate>
                                        <%--<asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true"
                                            OnCheckedChanged="chkHeader_CheckedChanged"></asp:CheckBox>--%>
                                          <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);" />

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%--  <asp:CheckBox ID="cbSelect" runat="server" AutoPostBack="true"
                                            OnCheckedChanged="cbSelect_CheckedChanged"></asp:CheckBox>--%>
                                       <%-- <asp:CheckBox ID="cbSelect" runat="server" AutoPostBack="false"></asp:CheckBox>--%>
                                         <asp:CheckBox ID="cbSelect" runat="server" onclick = "Check_Click(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Emp_id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmp_id" runat="server" Visible="false" Text='<%# Eval("empId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmempName" runat="server" Text='<%# Eval("Fullname") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact">
                                    <ItemTemplate>
                                        <asp:Label ID="lblempPhno" runat="server" Text='<%# Eval("EmpPhone") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#EDE8E8"></HeaderStyle>
                        </asp:GridView>
                    </div>
                </div>
            </div>


        </div>

        <%--            </div>
        </div>--%>


        <%--  <script type="text/javascript">
            function Search_GridviewTest(strKey) {
                var strData = strKey.value.toLowerCase().split(" ");
                var tblData = document.getElementById("<%=grd.ClientID %>");
                var rowData;
                for (var i = 1; i < tblData.rows.length; i++) {
                    rowData = tblData.rows[i].innerHTML;
                    var styleDisplay = 'none';
                    for (var j = 0; j < strData.length; j++) {
                        if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                            styleDisplay = '';
                        else {
                            styleDisplay = 'none';
                            break;
                        }
                    }
                    tblData.rows[i].style.display = styleDisplay;
                }
            }
        </script>--%>

        <script type="text/javascript">
            $(document).ready(function () {
                $('#ContentPlaceHolder1_grd').DataTable({
                    "fixedHeader": false,
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
            function Check_Click(objRef) {
                //Get the Row based on checkbox
                var row = objRef.parentNode.parentNode.parentNode;
                //Get the reference of GridView
                var GridView = row.parentNode;

                //Get all input elements in Gridview
                var inputList = GridView.getElementsByTagName("input");

                for (var i = 0; i < inputList.length; i++) {
                    //The First element is the Header Checkbox
                    var headerCheckBox = inputList[0];

                    //Based on all or none checkboxes
                    //are checked check/uncheck Header Checkbox
                    var checked = true;
                    if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                        if (!inputList[i].checked) {
                            checked = false;
                            break;
                        }
                    }
                }
                headerCheckBox.checked = checked;

            }
        </script>
        <script type="text/javascript">
            function checkAll(objRef) {
                var GridView = objRef.parentNode.parentNode.parentNode.parentNode;
                var inputList = GridView.getElementsByTagName("input");
                for (var i = 0; i < inputList.length; i++) {
                    //Get the Cell To find out ColumnIndex
                    var row = inputList[i].parentNode.parentNode;
                    if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                        if (objRef.checked) {
                            inputList[i].checked = true;
                        }
                        else {
                            inputList[i].checked = false;
                        }
                    }
                }
            }
        </script>

        <script type="text/javascript">
            $(document).ready(function () {
                $('#ContentPlaceHolder1_GridAssign').DataTable({
                    "fixedHeader": true,
                    "paging": false,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false
                });
            });</script>

    </section>
</asp:Content>
