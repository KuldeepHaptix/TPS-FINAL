<%@ Page Title="" Language="C#" MasterPageFile="~/MAIN.Master" AutoEventWireup="true" CodeBehind="AssignEnginerr1.aspx.cs" Inherits="EmployeeManagement.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style>
        .margint15 {
            margin-top: 15px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <section class="content-header">
        <h1>Assign Enginerr To Complaint
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>

    </section>

    <section class="content">
           <%--<asp:UpdatePanel ID="pnl" UpdateMode="Conditional" runat="server">--%>
                      <%--  <ContentTemplate>--%>
        <div class="box box-default">
            <div class="box-header with-border">
                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
                <br />
            </div>

            <div class="box-body">
                 <div class="row" id="r2">
                    <div class="col-md-12">
                         <div class="col-md-2">
                            <asp:Label ID="lblreseller" class="control-label" runat="server" Text="Select Complaint"></asp:Label>
                        </div>
                        <div class="col-md-3">                          
                                
                            <asp:DropDownList ID="dlComplaint" CssClass=" form-control select2"  AutoPostBack="false" OnSelectedIndexChanged="dlComplaint_SelectedIndexChanged" runat="server">
                            </asp:DropDownList>                               

                        </div>
                       <div class="col-md-1">
                        </div>
                        <div class="col-md-4"  >
                            <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" class="btn btn-primary" OnClientClick="return validateData()" Style="width: 111px;"/>
                        </div>
                    </div>
                </div>
                <div class="row" id="r1">
                    <div class="col-md-12">

                        <div class="col-md-2 margint15">
                            <asp:Label ID="lblsdate" class="control-label" runat="server" Text="Assign Date"></asp:Label><asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label><asp:Label ID="lblstart" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div class="col-md-3 margint15">
                            <asp:TextBox runat="server" class="form-control dates" ID="txtassignDate" />
                        </div>
                        <br />
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lbledate" runat="server" Text="Customer Name"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" class="form-control" ID="txtcustname" />
                        </div>

                    </div>
                </div>
                <br />
               

                <div class="row" id="r3">
                    <div class="col-md-12">

                        <%--<div class="col-md-2">
                            <asp:Label ID="lblstartdate" class="control-label" runat="server" Text="Start Dat"></asp:Label><asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label><asp:Label ID="lbls" runat="server" Visible="false"></asp:Label>
                        </div>--%>
                       <%-- <div class="col-md-3">
                            <asp:TextBox runat="server" class="form-control dates" ID="txtstartdate" />
                        </div>--%>
                       
                        <div class="col-md-1">
                        </div>
                        <%--<div class="col-md-2">
                            <asp:Label ID="lblenddate" runat="server" Text="End Date"></asp:Label><asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label><asp:Label ID="lble" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" class="form-control" ID="txtenddate" />
                        </div>--%>

                    </div>
                </div>
                 <div class="row" id="r3">
                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="Label5" class="control-label" runat="server" Text="Customer Contact_no"></asp:Label><asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label><asp:Label ID="Label7" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" class="form-control " ID="txtcontactNo" />
                        </div>
                       
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label8" runat="server" Text="Address"></asp:Label><asp:Label ID="Label9" runat="server" Text="*" ForeColor="Red"></asp:Label><asp:Label ID="Label10" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" class="form-control " ID="txtresiadd" />
                        </div>

                    </div>
                </div>
                <br />
                <div class="row" id="r4">
                    <div class="col-md-12">
                        <div style="margin-left:500px">
                            <asp:Button ID="btnsearchd" runat="server" Text="Search" class="btn btn-primary" Style="width: 111px;" OnClientClick="return validate1()"  />
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hidden" runat="server" />
            </div>

        </div>

        <div class="box box-primary">
            <div class="box-header">
                <span style="font-size: large">Engineer List</span>
            </div>
            <br />
            <br />
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
            <div class="row">
                    <div class="col-md-4">
                        <span style="color: red; margin-left: 15px">
                            <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                        </span>
                        <div class="col-md-5">
                            <div class="col-md-5">
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary"
                        Text="Assign" OnClick="Button1_Click" />
                                </div>
                        </div>
                    </div>
                </div>
            <div style="margin-top: 20px; margin-bottom: 10px; margin-left: 10px">
                <asp:Button ID="btnExport" CssClass="btn btn-primary"
                    runat="server" Text="Export To Excel" Visible="false"  />
            </div>
            <br />
        </div>

        <script type="text/javascript">

            $(document).ready(function () {
                rptdatatable();
                $(function () {
                    $('.dates').datepicker({
                        autoclose: true
                    });
                });

               
            });
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function (s, e) {
                rptdatatable();
                $(function () {
                    $('.dates').datepicker({
                        autoclose: true
                    });
                });
             
               
            });
            function rptdatatable() {
                $('.grd').DataTable({
                    "fixedHeader": true,
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": true
                });
            };

            
           
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
            function validate() {
                
                    if (document.getElementById("<%=txtassignDate.ClientID%>").value == "") {
                        alert("Start Date Feild can not be blank");
                        document.getElementById("<%=txtassignDate.ClientID%>").focus();
                        return false;

                    }
                   <%-- if (document.getElementById("<%=datepickerend.ClientID%>").value == "") {
                        alert("End Date Feild can not be blank");
                        document.getElementById("<%=datepickerend.ClientID%>").focus();
                        return false;
                    }--%>
                
            }
        </script>
        <script type="text/javascript">
            function validateData() {
                if (document.getElementById("<%=dlComplaint.ClientID%>").value == "-1") {
                    alert("Select Complaint No...");
                    document.getElementById("<%=dlComplaint.ClientID%>").focus();
                    return false;
                }
                <%--if (document.getElementById("<%=txtenddate.ClientID%>").value == "") {
                    alert("End Date Feild can not be blank");
                    document.getElementById("<%=txtenddate.ClientID%>").focus();
                    return false;
                }--%>
            }
            </script>
        <script>
            
        </script>
    </section>
    
</asp:Content>
    <%--</asp:ScriptManager>--%>
