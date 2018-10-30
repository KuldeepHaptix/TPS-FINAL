<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Manage_TimeGroup.aspx.cs" Inherits="EmployeeManagement.Manage_TimeGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1>Manage Attendance TimeGroup
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>
       <ol class="breadcrumb">           
            <li><a href="../Time_Group_List.aspx"><i></i>Attendance Time Group List</a></li>
            <li class="active">Manage Attendance TimeGroup</li>
        </ol>
    </section>
    <section class="content">

        <div class="box box-primary">
            <div class="box-header">
                <span style="font-size: large">Group Wise Time Config</span>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">

                            <div class="col-md-2">
                                <asp:CheckBox ID="chkall" runat="server" class="control-label" Text="&nbsp; Applay All Time" />
                            </div>

                            <div class="col-md-2">
                                <asp:Label ID="Label1" class="control-label" runat="server" Text="Group Name"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtgroup" runat="server" class="form-control" AutoPostBack="false"></asp:TextBox>
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
                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Day Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldayid" runat="server" Visible="false" Text='<%# Eval("Day_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday" runat="server" Text='<%# Eval("day") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="IN Time">
                                    <ItemTemplate>
                                        <div class="bootstrap-timepicker">
                                            <asp:TextBox ID="txtIntime" runat="server" AutoPostBack="false" class="form-control timepicker" Text='<%# Eval("Intime") %>' Onchange="return Add(this)" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Out Time">

                                    <ItemTemplate>
                                        <div class="bootstrap-timepicker">
                                            <asp:TextBox ID="txtoutTime" runat="server" AutoPostBack="false" class="form-control timepicker" Text='<%# Eval("outtime") %>' Onchange="return Add(this)" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                        </div>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Extreme Early">
                                    <ItemTemplate>
                                        <div class="bootstrap-timepicker">
                                            <asp:TextBox ID="txtearly" runat="server" AutoPostBack="false" class="form-control timepicker" Text='<%# Eval("early") %>' Onchange="return Add(this)" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Extreme Late">
                                    <ItemTemplate>
                                        <div class="bootstrap-timepicker">
                                            <asp:TextBox ID="txtlate" runat="server" AutoPostBack="false" class="form-control timepicker" Text='<%# Eval("late") %>' Onchange="return Add(this)" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#EDE8E8"></HeaderStyle>
                        </asp:GridView>
                         <br />
                <br /><br /><br /><br />
                    </div>
                </div>
               

                <div class="row">
                    <div class="col-md-12">
                        <br />
                        <span style="color: black; margin-left: 15px; font-size: 12pt" id="val">Send absent SMS after  &nbsp; &nbsp;
                            <input type="text" id="SMSintime" class="form-control" runat="server" style="width: 5%; display: inline-block" onkeypress="return isNumberKey(event);" />
                            &nbsp; &nbsp;  Min. of Intime. </span> <br /><br /> <span style="color: black; margin-left: 15px; font-size: 12pt" id="val">Send forgot Outpunch SMS after&nbsp; &nbsp;
                            <input type="text" id="smsout" class="form-control" runat="server" style="width: 5%; display: inline-block" onkeypress="return isNumberKey(event);" />&nbsp; &nbsp;
                            Min. of Outtime.</span> <br /><br /> <span style="color: black; margin-left: 15px; font-size: 12pt" id="val">Applicable this change from &nbsp; &nbsp;
                            <asp:TextBox runat="server" class="form-control dates" ID="date" TabIndex="6" Style="width: 15%; display: inline-block" />.</span>
                       
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">

                        <div class="col-md-5">
                        </div>
                        <div class="col-md-2" style="margin-top: 20px; margin-bottom: 10px; margin-bottom: 10px">
                            <asp:Button ID="btnsave" CssClass="btn btn-primary" Style="width: 150px"
                                runat="server" Text="Save" OnClientClick=" return validateSaveDetails()" OnClick="btnsave_Click" />
                        </div>



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
       <%-- <script type="text/javascript">
            $(document).ready(function () {
                $('#ContentPlaceHolder1_grd').DataTable({
                    "fixedHeader": true,
                    "paging": false,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": false,
                    "info": false,
                    "autoWidth": false
                });
            });
        </script>--%>
        <style>
            .dates {
            }

            .times {
            }
        </style>

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
            function validateSaveDetails() {
                if (document.getElementById("<%=txtgroup.ClientID%>").value == "") {
                     alert("Group name can not be blank");
                     document.getElementById("<%=txtgroup.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=SMSintime.ClientID%>").value == "") {
                alert("sent absent SMS after time can not be blank");
                     document.getElementById("<%=SMSintime.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=smsout.ClientID%>").value == "") {
                alert("Send forgot Outpunch SMS after time can not be blank");
                     document.getElementById("<%=smsout.ClientID%>").focus();
                     return false;
                 }
                 if (document.getElementById("<%=date.ClientID%>").value == "") {
                     alert("Applicable this change date can not be blank");
                     document.getElementById("<%=date.ClientID%>").focus();
                     return false;
                 }
                 return true;
             }
        </script>

            <script type="text/javascript">
                function Add(index) {
                    var checkbox_status = document.getElementById("<%=chkall.ClientID%>");
                    var txtboxids = index.id.split('_');
                    if (checkbox_status.checked && txtboxids[3] == "0" && index.value != "") {
                        for (var rowId = 1; rowId < 7; rowId++)
                        {
                           
                            document.getElementById(txtboxids[0] + "_" + txtboxids[1] + "_" + txtboxids[2] + "_" + rowId).value = index.value;
                            //document.getElementById("ContentPlaceHolder1_grd_txtIntime_" + rowId).value = document.getElementById("ContentPlaceHolder1_grd_txtIntime_0").value;
                            //document.getElementById("ContentPlaceHolder1_grd_txtoutTime_" + rowId).value = document.getElementById("ContentPlaceHolder1_grd_txtoutTime_0").value;
                            //document.getElementById("ContentPlaceHolder1_grd_txtearly_" + rowId).value = document.getElementById("ContentPlaceHolder1_grd_txtearly_0").valuee;
                            //document.getElementById("ContentPlaceHolder1_grd_txtlate_" + rowId).value = document.getElementById("ContentPlaceHolder1_grd_txtlate_0").value;
                        }
         
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

    </section>
</asp:Content>
