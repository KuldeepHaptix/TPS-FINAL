<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Employee_AssignToTimeGroup.aspx.cs" Inherits="EmployeeManagement.Employee_AssignToTimeGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Employee Assign To Time Group
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>

    </section>
    <section class="content">

        <div class="box box-default">
            <div class="box-header with-border">
                <h3 class="box-title">Search Criteria</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="lblprofile" runat="server" Text="Select School"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddlschool" tabindex="1">
                            </select>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-2">
                            <asp:Label ID="lblyear" class="control-label" runat="server" Text="Select Department"></asp:Label>
                        </div>
                        <div class="col-md-3">

                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddldepartment" tabindex="2">
                            </select>
                        </div>

                    </div>
                    <br />
                    <br />
                    <br />
                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="Label5" class="control-label" runat="server" Text="Employee Name"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtemployee" runat="server" class="form-control" AutoPostBack="false" TabIndex="3"></asp:TextBox>

                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-2">
                            <asp:Label ID="lblId" class="control-label" runat="server" Text="Select Designtion"></asp:Label>
                        </div>
                        <div class="col-md-3">

                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddlDesignation" tabindex="4">
                            </select>
                        </div>

                    </div>
                    <br />
                    <br />
                    <br />

                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="Label1" class="control-label" runat="server" Text="Employee Phone"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtphone" runat="server" class="form-control" AutoPostBack="false" TabIndex="5" onkeypress="return isNumberKey(event);"></asp:TextBox>

                        </div>

                        <div class="col-md-1"></div>
                        <div class="col-md-2">
                            <asp:Label ID="Label2" class="control-label" runat="server" Text="Select TimeGroup"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddltimegrp" tabindex="6">
                            </select>
                        </div>
                    </div>
                    <br />
                    <br />
                    <br />


                    <div class="col-md-12">

                        <div class="col-md-1"></div>
                        <div class="col-md-2" style="margin-left: 90px">
                            <asp:Button ID="BtnSearch" runat="server" class="btn btn-primary" Text="Search" TabIndex="7" Width="110px" OnClick="BtnSearch_Click" />
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

        <div class="box box-primary">
            <div class="box-header">
                <span style="font-size: large">Employee  List</span>

                <asp:Button ID="Button1" CssClass="btn btn-primary"
                                runat="server" Text="Save" style="width:150px;float:right" OnClick="Button1_Click" />
                <br />
                <br />
                <div class="row  table-responsive">
                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Emp_id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmp_id" runat="server" Visible="false" Text='<%# Eval("EmpId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="grp_id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrp_id" runat="server" Visible="false" Text='<%# Eval("group_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmp_Name" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldepart" runat="server" Text='<%# Eval("Department_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesignation" runat="server" Text='<%# Eval("Designation_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Time Group">
                                    <ItemTemplate>
                                        <select class="form-control select2" style="width: 100%" runat="server" id="ddltime"></select>
                                        <%-- <asp:DropDownList ID="ddltime" runat="server">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeaderlate" runat="server" AutoPostBack="false" CssClass="chkHead_1" data-id="1" onclick = "checkAll(this);"
                                            Text="&nbsp;&nbsp;Send Late SMS" ></asp:CheckBox>

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%-- <asp:CheckBox ID="cbSelect" CssClass="chkbox Group1" data-value="1" runat="server"></asp:CheckBox>--%>
                                        <asp:CheckBox ID="cbSelectlate" runat="server" CssClass="chk_1" AutoPostBack="false" data-id="1" onclick = "Check_Click(this)"
                                            Text="&nbsp;&nbsp;Late SMS" ></asp:CheckBox>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeaderabsent" runat="server" AutoPostBack="false" CssClass="chkHead_2" data-id="2" onclick = "checkAll(this);"
                                            Text="&nbsp;&nbsp;Send Absent SMS"></asp:CheckBox>

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%-- <asp:CheckBox ID="cbSelect" CssClass="chkbox Group1" data-value="1" runat="server"></asp:CheckBox>--%>
                                        <asp:CheckBox ID="cbSelectabsent" CssClass="chk_2" runat="server" AutoPostBack="false" data-id="2"  onclick = "Check_Click(this)"
                                            Text="&nbsp;&nbsp;Absent SMS" ></asp:CheckBox>
                                      
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeaderOutpunch" runat="server" AutoPostBack="false" CssClass="chkHead_3"  data-id="3" onclick = "checkAll(this);"
                                            Text="&nbsp;&nbsp;Send Forget Outpunch SMS"></asp:CheckBox>

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%-- <asp:CheckBox ID="cbSelect" CssClass="chkbox Group1" data-value="1" runat="server"></asp:CheckBox>--%>
                                        <asp:CheckBox ID="cbSelectOutpunch" CssClass="chk_3" runat="server" AutoPostBack="false" data-id="3" onclick ="Check_Click(this)"
                                            Text="&nbsp;&nbsp;Forget Out SMS"></asp:CheckBox>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeaderEmail" runat="server" data-id="4" CssClass="chkHead_4"  onclick = "checkAll(this);"  AutoPostBack="false"  
                                            Text="&nbsp;&nbsp;Send Email"></asp:CheckBox>

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%-- <asp:CheckBox ID="cbSelect" CssClass="chkbox Group1" data-value="1" runat="server"></asp:CheckBox>--%>
                                        <asp:CheckBox ID="cbSelectEmail" CssClass="chk_4" runat="server" AutoPostBack="false" data-id="4" onclick = "Check_Click(this)"
                                            Text="&nbsp;&nbsp;Email"></asp:CheckBox>
                                    
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
                                runat="server" Text="Save" style="width:150px" OnClick="btnsave_Click"/>
                        </div>
                            </div>
                    </div>
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

        <style>
        .chk {
        }

        .gridview {
        }
    </style>

        <script type = "text/javascript">
            function checkAll(objRef) {
                var dataid = objRef.parentNode.getAttribute("data-id");
                if (objRef.checked) {
                    var chks = document.getElementsByClassName("chk_" + dataid);
                    for (i = 0; i < chks.length; i++) {
                        chks[i].childNodes[0].checked = true;
                    }
                }
                else {
                    var chks = document.getElementsByClassName("chk_" + dataid);
                    for (i = 0; i < chks.length; i++) {
                        chks[i].childNodes[0].checked = false;
                    }
                }
            }
            function Check_Click(objRef) {
                var dataid = objRef.parentNode.getAttribute("data-id");
                var chks = document.getElementsByClassName("chk_" + dataid);
                var count = 0;
                for (i = 0; i < chks.length; i++) {
                    if (chks[i].childNodes[0].checked) {
                        count++;
                    }
                }
                if (count == chks.length) {
                    var chksHead = document.getElementsByClassName("chkHead_" + dataid);
                    for (i = 0; i < chksHead.length; i++) {
                        chksHead[i].childNodes[0].checked = true;
                    }
                }
                else {
                    var chksHead = document.getElementsByClassName("chkHead_" + dataid);
                    for (i = 0; i < chksHead.length; i++) {
                        chksHead[i].childNodes[0].checked = false;
                    }
                }
            }
</script>
   
         <script type="text/javascript">
             $(document).ready(function () {
                 $('#ContentPlaceHolder1_grd').DataTable({
                     "fixedHeader": true,
                     "paging": false,
                     "lengthChange": true,
                     "searching": false,
                     "ordering": true,
                     "info": true,
                     "autoWidth": false
                 });
             });

        </script>
    </section>
</asp:Content>
