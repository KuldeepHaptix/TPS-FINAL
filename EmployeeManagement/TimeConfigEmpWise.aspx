<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="~/TimeConfigEmpWise.aspx.cs" Inherits="EmployeeManagement.TimeConfigForEmp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>DateWise Time Config For Employee  
            <small>
                <asp:Label runat="server" ID="lblDateTime"> </asp:Label>

            </small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="EmpWiseTimeConfig.aspx"><i></i>Time Config For Employee</a></li>
            <li class="active">Manage Attendance TimeGroup</li>
        </ol>

    </section>
    <br />

    <br />
    <section class="content">
        <div class="box box-default">
            <div class="box-header with-border">
                Assign Time For Date:<asp:Label runat="server" ID="lblDate"> </asp:Label>
                <div class="box-tools pull-right">


                    <br />
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label1" class="control-label" runat="server" Text="In Time"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtinTime" runat="server" Style="width: 150px" class="form-control timepicker" onkeypress="return isNumberKey(event)" AutoPostBack="false" TabIndex="1"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label2" class="control-label" runat="server" Text="Out Time"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtoutTime" runat="server" Style="width: 150px" class="form-control timepicker" onkeypress="return isNumberKey(event)" AutoPostBack="false" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label3" class="control-label" runat="server" Text="Extreme Early"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtExtremeEarly" runat="server" Style="width: 150px" class="form-control timepicker" onkeypress="return isNumberKey(event)" AutoPostBack="false" TabIndex="3"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label4" class="control-label" runat="server" Text="Extreme Late"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtExtremeLate" class="form-control timepicker" onkeypress="return isNumberKey(event)" runat="server" Style="width: 150px" AutoPostBack="false" TabIndex="4"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<div class="row">
            <div class="col-md-4">
                <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                    <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                </span>
                <div class="col-md-5">
                    <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary"
                        Text="Save" OnClick="BtnSave_Click" OnClientClick="return ValidateData()" />
                </div>
            </div>
        </div>--%>
        <div class="box box-primary">
            <div class="box-header">
                <div class="row">
                    <div class="col-md-4">
                        <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                            <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                        </span>
                        <div class="col-md-5">
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary"
                                Text="Save" OnClick="BtnSave_Click" OnClientClick="return ValidateData()" />
                        </div>
                    </div>
                </div>
                <span style="font-size: large">Select Employee to Assign Time</span>

                <div class="row  table-responsive">
                    <div style="width: 80%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="GridView" Style="width: 20%; margin-left: 2%; margin-right: 0%; text-align: center;" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8"  AutoGenerateColumns="False" EnableViewState="true"  DataKeyNames="empid"  OnRowDataBound="GridView_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAll(this); " AutoPostBack="false" />
                                        <%--<asp:Label ID="lblisshow" runat="server" Text='<%# Eval("empid") %>' Visible="false"></asp:Label>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="emp_id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblemp_id" runat="server" Visible="false" DataField="empid" Text='<%# Eval("empid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblempName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>
                    <%--</div>--%>
                </div>
            </div>
            </div>
            <script type="text/javascript">
                // for check all checkbox  
                function CheckAll(Checkbox) {
                    var GridVwHeaderCheckbox = document.getElementById("<%=GridView.ClientID %>");
                    for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                        GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                    }
                }
            </script>
            <%--<script type="text/javascript">
            function Check_Click(objRef) {
                //Get the Row based on checkbox
                var row = objRef.parentNode.parentNode.parentNode.parentNode;
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
                var GridView = objRef.parentNode.parentNode.parentNode.parentNode.parentNode;
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
        </script>--%>
            <script type="text/javascript">
                $(document).ready(function () {
                    $('#ContentPlaceHolder1_dataTable').DataTable({
                        "fixedHeader": true,
                        "paging": false,
                        "lengthChange": true,
                        "searching": true,
                        "ordering": true,
                        "info": false,
                        "autoWidth": true
                    });
                });
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
                    if (document.getElementById("<%=txtinTime.ClientID%>").value == "") {
                        alert("In-Time  Value can not be blank");
                        document.getElementById("<%=txtinTime.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtoutTime.ClientID%>").value == "") {
                        alert("Out-Time Value can not be blank");
                        document.getElementById("<%=txtoutTime.ClientID%>").focus();
                    return false;
                }

                if (document.getElementById("<%=txtExtremeEarly.ClientID%>").value == "") {
                        alert("ExtremeEarly Value can not be blank");
                        document.getElementById("<%=txtExtremeEarly.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtExtremeLate.ClientID%>").value == "") {
                        alert("ExtremeLate Value can not be blank");
                        document.getElementById("<%=txtExtremeLate.ClientID%>").focus();
                    return false;
                }

            }

            </script>
    </section>
</asp:Content>
