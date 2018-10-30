<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="createuseraccount.aspx.cs" Inherits="EmployeeManagement.createuseraccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Create User Account
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
                            <asp:Label ID="lblempname" runat="server" Text="Employee Name"></asp:Label><asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtname" runat="server" class="form-control" AutoPostBack="false"></asp:TextBox>

                        </div><span style="color: red; margin-left: 15px" id="val_msg_wrap">
                                        <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                                    </span>
                    </div>
                      <br />
                    <br />
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label4" runat="server" Text="ContactNo"></asp:Label><asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtcn" runat="server" class="form-control" AutoPostBack="false"></asp:TextBox>

                        </div>
                    </div>
                      <br />
                    <br />
                      <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label6" class="control-label" runat="server" Text="Gender"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:RadioButton ID="rdbmale" runat="server" Text="&nbsp;Male" Checked="true" GroupName="gender"  TabIndex="8"/>
                                        &nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbFemale" runat="server" Text="&nbsp;Female" GroupName="gender"  TabIndex="9"/>
                          </div>          </div>

                    <br />
                    <br />
                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="lblusername" runat="server" Text="Address"></asp:Label><asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtAddress" runat="server" class="form-control" AutoPostBack="false" TextMode="MultiLine"></asp:TextBox>

                        </div>
                    </div>
                    <br /><br /><br />
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="lblpassword" runat="server" Text="DateOfJoin" class="control-label"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtdoj" runat="server"  class="form-control dates"  AutoPostBack="false" ></asp:TextBox>

                        </div>
                        <div class="col-md-4" style="width: auto"></div>

                    </div>
                    <br />
                    <br />
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label7" runat="server" Text="Date of birth" class="control-label"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtdob" runat="server"  class="form-control dates"  AutoPostBack="false" ></asp:TextBox>

                        </div>
                        <br />
                        
                        <div class="col-md-4" style="width: auto"></div>

                    </div>
                    <br />
                    
                    
                    <br />
                    
                        <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label9" runat="server" Text="Specilazation" class="control-label"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtspe" runat="server"  class="form-control"  AutoPostBack="false" ></asp:TextBox>

                        </div>
                        <div class="col-md-4" style="width: auto"></div>

                    </div>
                    
                    <br />

                    <br />
                   <div class="col-md-2">
                            <asp:Label ID="Label11" runat="server" Text="Email" class="control-label"></asp:Label>
                        </div>
                    <div class="col-md-3">
                            <asp:TextBox ID="txtemail" runat="server"  class="form-control"  AutoPostBack="false" ></asp:TextBox>

                        </div>
                    <br />
                    <br />

                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="Label13" runat="server" Text="Remarks"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtremarks" runat="server" class="form-control" AutoPostBack="false" TextMode="MultiLine"></asp:TextBox>

                        </div>
                    </div>
                  
                     <div class="row">
                    <div class="col-md-12 text-left" style="padding: 30px">
                        <asp:Button ID="btnSave" CssClass="btn btn-primary"
                            runat="server" Text="Save" Style="width: 150px" OnClientClick="return validateData()" OnClick="btnsave_Click" />
                     
                    </div>
                </div>
                </div>
            </div></div>
        
        
           
             <script type="text/javascript">
                 $(document).ready(function () {
                     $('#ContentPlaceHolder1_grduser').DataTable({
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
            function validateData() {

                if (document.getElementById("<%=txtname.ClientID%>").value == "") {
                    alert("User Name can not be blank");
                    document.getElementById("<%=txtname.ClientID%>").focus();
                    return false;
                }
                if (document.getElementById("<%=txtcn.ClientID%>").value == "") {
                    alert("Contact No can not be blank");
                    document.getElementById("<%=txtcn.ClientID%>").focus();
                    return false;
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
        <script>
            $(function () {
                $('.dates').datepicker({
                    autoclose: true
                });
            });
    </script>
    </section>
</asp:Content>
