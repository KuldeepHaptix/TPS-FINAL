<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Search_Employee.aspx.cs" Inherits="EmployeeManagement.Search_Employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1>Search Complaints
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>
        <ol class="breadcrumb">
            <%--  <li>Search Employee</li>--%>
        </ol>
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
                            <asp:Label ID="lblprofile" runat="server" Text="Select Profile"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddlprofile" onchange="changetextbox()" tabindex="1">
                            </select>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-2">
                            <asp:Label ID="lblyear" class="control-label" runat="server" Text="Select year" Visible="true"></asp:Label>

                        </div>
                        <div class="col-md-3">

                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddlyear" tabindex="2" visible="true" onchange="chckbox()">
                            </select>
                            <asp:CheckBox ID="cbSelect" runat="server" AutoPostBack="false" 
                onclick="Check_Click(this)" Text="Employees joined in selected Year." Enabled="false"> </asp:CheckBox>
                             <%--<input type="checkbox" name="Empjoinchk" value="">Employee Joined in selected Year.--%>
                        </div>

                    </div>
                    <br />
                    <br />
                    <br />
                    <div class="col-md-12">

                        <div class="col-md-2">
                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddlColumnList" tabindex="3">
                            </select>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtvalue" runat="server" class="form-control" AutoPostBack="false" TabIndex="4"></asp:TextBox>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-2">
                            <asp:Label ID="lblId" class="control-label" runat="server" Text="Assign Status"></asp:Label>
                        </div>
                        <div class="col-md-3">

                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddlstatusAssign" tabindex="5" onchange="assignemp()">
                            </select>
                        </div>

                    </div>
                    <br />
                    <br />
                    <br />

                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="Label1" class="control-label" runat="server" Text="Organization Name"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddlschList" tabindex="6">
                            </select>
                        </div>

                        <div class="col-md-1"></div>
                        <div class="col-md-2">
                            <asp:Label ID="Label2" class="control-label" runat="server" Text="Work Status"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddlStatus" tabindex="7">
                            </select>
                        </div>
                    </div>
                    <br />
                    <br />
                    <br />

                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="Label3" class="control-label" runat="server" Text="Department Name"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddldepart" tabindex="8">
                            </select>
                        </div>

                        <div class="col-md-1"></div>
                        <div class="col-md-2">
                            <asp:Label ID="Label4" class="control-label" runat="server" Text="Designation Name"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddldesig" tabindex="9">
                            </select>
                        </div>
                    </div>


                    <br />
                    <br />
                    <br />

                    <div class="col-md-12">

                        <div class="col-md-1"></div>
                        <div class="col-md-2" style="margin-left: 90px">
                            <asp:Button ID="BtnSearch" runat="server" class="btn btn-primary" Text="Search" OnClick="BtnSearch_Click" TabIndex="10" Width="110px" />
                        </div>
                        <div class="col-md-2" style="margin-left: 450px">

                            <%--<asp:ImageButton runat="server" ID="img1" PostBackUrl="~/DisplayColumnConfigurationForSearchEmployee.aspx" ImageUrl="~/dist/img/config.jpg" Height="100px" Width= />--%>
                            <asp:Button runat="server" class="btn btn-block btn-success" Width="225px" PostBackUrl="~/DisplayColumnConfigurationForSearchEmployee.aspx" target="_blank" Text="Display Column Configuration" ID="btnfield" TabIndex="11" />
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
                <span style="font-size: large">Compalaint List</span>
                <br />
                <br />
                <div class="row  table-responsive">
                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="true" OnRowDataBound="grd_RowDataBound" OnRowCommand="grd_RowCommand" OnRowEditing="grd_RowEditing" OnRowCreated="grd_RowCreated" >
                            <Columns>
                                <asp:TemplateField HeaderText="EmpId" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmpId" runat="server" Visible="false" Text='<%# Eval("EmpId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                               <%-- <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%# Eval("EmpId")  %>' CommandName="del" Text="Delete" OnClientClick="return confirm('Are you sure you want to Delete this Employee?');"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%# Eval("EmpId")  %>' CommandName="Edit" Text="Edit" ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("EmpId")+":"+Eval("EmpStatusFlag")  %>' CommandName="status" Text='<%# Eval("EmpStatusFlag") %>' OnClientClick="return confirm('Are you sure you want to Delete this Complaint?');"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
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

            function changetextbox() {
                if (document.getElementById("<%=ddlprofile.ClientID%>").value != "-1") {
                    document.getElementById("<%=ddlColumnList.ClientID%>").disabled = true;
                    document.getElementById("<%=ddlschList.ClientID%>").disabled = true;
                    document.getElementById("<%=ddlstatusAssign.ClientID%>").disabled = true;
                    document.getElementById("<%=ddldepart.ClientID%>").disabled = true;
                    document.getElementById("<%=ddldesig.ClientID%>").disabled = true;
                    document.getElementById("<%=txtvalue.ClientID%>").value = '';
                }
                else {
                    document.getElementById("<%=ddlColumnList.ClientID%>").disabled = false;
                    <%-- document.getElementById("<%=txtvalue.ClientID%>").disabled = false;--%>
                    <%--document.getElementById("<%=ddlschList.ClientID%>").disabled = false;--%>
                    document.getElementById("<%=ddlstatusAssign.ClientID%>").disabled = false;
                    document.getElementById("<%=ddldepart.ClientID%>").disabled = false;
                    document.getElementById("<%=ddldesig.ClientID%>").disabled = false;
                }
            }
        </script>
        <script type="text/javascript">
            function assignemp() {
                if (document.getElementById("<%=ddlstatusAssign.ClientID%>").value != "0") {
                    document.getElementById("<%=ddlschList.ClientID%>").disabled = true;
                }
                else {
                    document.getElementById("<%=ddlschList.ClientID%>").disabled = false;
                }
            }
        </script>

        <script type="text/javascript">
            function chckbox() {
                if (document.getElementById("<%=ddlyear.ClientID%>").value != "-1") {
                    document.getElementById("<%=cbSelect.ClientID%>").disabled = false;
                    var c = document.getElementById("<%=cbSelect.ClientID%>");
                    c.checked = false;
                   
                }
                else {
                    document.getElementById("<%=cbSelect.ClientID%>").disabled = true;
                    var c = document.getElementById("<%=cbSelect.ClientID%>");
                    c.checked = false;
                }
            }
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
