<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Employee_Attendance.aspx.cs" Inherits="EmployeeManagement.Employee_Attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1>Employee Attendance Entry
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>

    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-header">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-1">
                        </div>

                        <div class="col-md-2">
                            <asp:Label ID="lblId" class="control-label" runat="server" Text="Attendance Date" Style="font-size: large"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtattDate" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtattDateChange"></asp:TextBox>
                        </div>

                        <div class="col-md-4">
                            <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                                <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                            </span>
                        </div>
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
                                <asp:TemplateField HeaderText="Emp_id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmp_id" runat="server" Text='<%# Eval("EmpId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="override" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbloverride" runat="server" Visible="false" Text='<%# Eval("override") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Employee Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmp_Name" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Phone">
                                    <ItemTemplate>
                                        <asp:Label ID="lblemp_phone" runat="server" Text='<%# Eval("empphone") %>'></asp:Label>
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
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeaderPresent" runat="server" data-id="1" CssClass="chkHead_1" AutoPostBack="false" onclick="checkAll(this);"
                                            Text="&nbsp;&nbsp;Present"></asp:CheckBox>
                                        

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      
                                        <%--<asp:CheckBox ID="cbSelectPresent" runat="server" AutoPostBack="false" CssClass="chk_1" data-id="1" onclick="Check_Click(this)"
                                            Text=""></asp:CheckBox>--%>
                                        <asp:RadioButton runat="server" id="cbSelectPresent" AutoPostBack="false" CssClass="chk_1" data-id="1" onclick="GridSelectAllColumn(this, 'chkHigh');"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeaderHalfday" runat="server" data-id="2" CssClass="chkHead_2" AutoPostBack="false" onclick="checkAll(this);"
                                            Text="&nbsp;&nbsp;Halfday"></asp:CheckBox>
                                   <%--     <asp:RadioButton runat="server" id="chkHeaderHalfday" AutoPostBack="false" CssClass="chkHead_2" data-id="2" onclick="checkAll(this);"/>--%>

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       
                                     <%--   <asp:CheckBox ID="cbSelectHalfday" runat="server" AutoPostBack="false" CssClass="chk_2" data-id="2" onclick="Check_Click(this)"
                                            Text=""></asp:CheckBox>--%>
                                        <asp:RadioButton runat="server" id="cbSelectHalfday" AutoPostBack="false" CssClass="chk_2" data-id="2" onclick="GridSelectAllColumn(this, 'chkMid');"/>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeaderabsent" runat="server" AutoPostBack="false" data-id="3" CssClass="chkHead_3" onclick="checkAll(this);"
                                            Text="&nbsp;&nbsp;Absent"></asp:CheckBox>

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       <%-- <asp:CheckBox ID="cbSelectabsent" runat="server" AutoPostBack="false" CssClass="chk_3" data-id="3" onclick="Check_Click(this)"
                                            Text=""></asp:CheckBox>--%>
                                         <asp:RadioButton runat="server" id="cbSelectabsent" AutoPostBack="false" CssClass="chk_3" data-id="3" onclick="GridSelectAllColumn(this, 'chkLow');"/>
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

        <script>
            $(function () {
                $('#ContentPlaceHolder1_txtattDate').datepicker({
                    autoclose: true
                });
            });
        </script>

        <script type="text/javascript">
            function checkAll(objRef) {

                var dataid = objRef.parentNode.getAttribute("data-id");
                
                if (objRef.checked) {
                    var chks = document.getElementsByClassName("chk_" + dataid);
                    if (dataid == 1) {
                        var chks2 = document.getElementsByClassName("chk_2");
                        for (i = 0; i < chks2.length; i++) {
                            chks2[i].childNodes[0].checked = false;
                        }
                        var chks3 = document.getElementsByClassName("chk_3");
                        for (i = 0; i < chks3.length; i++) {
                            chks3[i].childNodes[0].checked = false;
                        }
                        var head2 = document.getElementById("ContentPlaceHolder1_grd_chkHeaderHalfday");
                        head2.checked = false;
                        var head3 = document.getElementById("ContentPlaceHolder1_grd_chkHeaderabsent");
                        head3.checked = false;
                    }
                    if (dataid == 2) {
                        var chks1 = document.getElementsByClassName("chk_1");
                        for (i = 0; i < chks1.length; i++) {
                            chks1[i].childNodes[0].checked = false;
                        }
                        var chks3 = document.getElementsByClassName("chk_3");
                        for (i = 0; i < chks3.length; i++) {
                            chks3[i].childNodes[0].checked = false;
                        }
                        var head1 = document.getElementById("ContentPlaceHolder1_grd_chkHeaderPresent");
                        head1.checked = false;
                        var head3 = document.getElementById("ContentPlaceHolder1_grd_chkHeaderabsent");
                        head3.checked = false;
                    }
                    if (dataid == 3) {
                        var chks1 = document.getElementsByClassName("chk_1");
                        for (i = 0; i < chks1.length; i++) {
                            chks1[i].childNodes[0].checked = false;
                        }
                        var chks2 = document.getElementsByClassName("chk_2");
                        for (i = 0; i < chks2.length; i++) {
                            chks2[i].childNodes[0].checked = false;
                        }
                        var head1 = document.getElementById("ContentPlaceHolder1_grd_chkHeaderPresent");
                        head1.checked = false;
                        var head2 = document.getElementById("ContentPlaceHolder1_grd_chkHeaderHalfday");
                        head2.checked = false;
                    }


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
                //alert(objRef);
                var dataid = objRef.parentNode.getAttribute("data-id");
                //var falg = true;
                //if (dataid == 1)
                //{
                //    var half = objRef.parebtElement.nextSibling;
                //    alert(half);
                //}
                //if (falg==false)
                //{
                //    return;
                //}
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
            function GridSelectAllColumn(objType, chkName) {
                var oItem = objType.children;
                var theBox = (objType.type == "radio") ? objType : objType.children.item[0];
                var strPart = theBox.id.split("_");
                xState = theBox.checked;
                elm = theBox.form.elements;
                for (var i = 0; i < elm.length; i++)
                {
                    var elmpart = elm[i].id.split("_");
                    if (elm[i].type == "radio" && elm[i].id != theBox.id && elmpart[3] == strPart[3]) {
                        elm[i].checked = !xState;
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
