<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Assign Enginerr.aspx.cs" Inherits="EmployeeManagement.Assign_Enginerr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1>Assign Enginerr to Complaint  
            <small>
                <asp:Label runat="server" ID="lblDateTime"> </asp:Label>
            </small>
        </h1>
       <%-- <ol class="breadcrumb">
            <li><a href="EmpWiseTimeConfig.aspx"><i></i>Time Config For Employee</a></li>
            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click1" Text="Time Config For Employee" Visible="false"/>
            <li class="active">Manage Attendance TimeGroup</li>
        </ol>--%>

    </section>
    <br />

    <br />
    <section class="content">
        <div class="box box-default">
            <div class="box-header with-border">
               <%-- Assign Time For Date: <b><asp:Label runat="server" ID="lblDate1"> </asp:Label></b>--%>
                <asp:TextBox ID="txtAssignDate" runat="server" placeholder="Assign Date" Style="width: 150px" class="form-control dates" AutoPostBack="false" TabIndex="1"></asp:TextBox>
                <div class="box-tools pull-right">


                    <br />
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label1" class="control-label" runat="server" Text="Select Complaint"></asp:Label><label style="color: red">*</label>
                             <%--<asp:TextBox ID="txtinTime" runat="server" placeholder="Assign Date" Style="width: 150px" class="form-control timepicker" onkeypress="return isNumberKey(event)" AutoPostBack="false" TabIndex="1"></asp:TextBox>--%>
                        </div>
                        <div class="col-md-4">
                           <%-- <asp:TextBox ID="txtinTime" runat="server" Style="width: 150px" class="form-control timepicker" onkeypress="return isNumberKey(event)" AutoPostBack="false" TabIndex="1"></asp:TextBox>--%>
                            <asp:DropDownList ID="dlComplaint" OnSelectedIndexChanged="dlComplaint_SelectedIndexChanged" CssClass="form-control select2 " runat="server" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label2" class="control-label" runat="server" AutoPostBack="false" Text="Resi Address"></asp:Label><label style="color: red"> </label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtoutTime" runat="server" Style="width: 150px" class="form-control"   AutoPostBack="false" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label3" class="control-label" runat="server" Text="Contact Number"></asp:Label><label style="color: red">*</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtExtremeEarly" runat="server" Style="width: 150px" class="form-control "  AutoPostBack="false" TabIndex="3"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label4" class="control-label" runat="server" Text="Extreme Late"></asp:Label><label style="color: red">*</label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtExtremeLate" class="form-control" runat="server" Style="width: 150px" TabIndex="4"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
<div class="box box-primary">
           <div class="box-header">
        <div class="row">
            <div class="col-md-4">
                <span style="color: red; margin-left: 15px" id="">
                    <asp:Literal ID="ltrErr1" runat="server"></asp:Literal>
                </span>
                <div class="col-md-5">
                    <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary"
                        Text="Save" OnClick="Button1_Click" OnClientClick="return ValidateData()" />
                </div>
            </div>
        </div>
      
                <div class="row">
                    <div class="col-md-4">
                        <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                            <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                        </span>
                        <div class="col-md-5">
                           
                        </div>
                    </div>
                </div>
                
                
                <div class="row  table-responsive">
                    <div class="col-md-5">
                        <span style="font-size:medium">Select Employee to Assign Time</span>
                    <div style="width: 41%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="GrdEmpList" Style="margin-left: 2%; margin-right: 0%; text-align: center;" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8"  AutoGenerateColumns="False" EnableViewState="true"  DataKeyNames="eng_id"   Width="250px">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAllSelect" runat="server" onclick="checkAll(this); " AutoPostBack="false" />
                                        <asp:Label ID="lblisshow" runat="server" Text='<%# Eval("eng_id") %>' Visible="false"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="eng_id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblemp_id" runat="server" Visible="false" DataField="eng_id" Text='<%# Eval("eng_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Enginerr Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblempName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblengcontact" runat="server" Text='<%# Eval("mobile_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                        </div>
                        </div>
        
                    </div>
                    </div>
                </div>
           

                
        <div class="row">
                    <div class="col-md-4">
                        <span style="color: red; margin-left: 15px">
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        </span>
                        <div class="col-md-5">
                            <div class="col-md-5">
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary"
                        Text="Assign" OnClick="Button1_Click" OnClientClick="return ValidateData()" />
                                </div>
                        </div>
                    </div>
                </div>
                   <%-- </div>
                    </div>
                    </div>
                </div>--%>
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
          
    </section>
    </asp:Content>