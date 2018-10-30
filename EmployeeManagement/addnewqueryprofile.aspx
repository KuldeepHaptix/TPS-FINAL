<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="addnewqueryprofile.aspx.cs" Inherits="EmployeeManagement.addnewqueryprofile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Add New Query Profile
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="../addeditprofile.aspx"><i></i>Add Edit Profile</a></li>
            <li class="active">Add Profile</li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-header">
                <div class="row">
                    <br />
                    <div class="col-md-12"></div>
                    <div class="col-md-2">
                        <asp:Label ID="lblprofile" class="control-label" runat="server" Text="Profile Name"></asp:Label>
                        <asp:HiddenField ID="Hidden1" runat="server" /> 
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtprofile" runat="server" class="form-control" AutoPostBack="false"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <div id="val_msg_wrap" class="abc">
                            <span style="color: red; margin-left: 15px">
                                <asp:Literal ID="ltrErr" runat="server"></asp:Literal>

                            </span>
                        </div>
                    </div>
                    <br />
                    <br />
                    <br />
                    <div class="col-md-12"></div>

                    <div class="col-md-2">
                        <asp:Label ID="lblquery" class="control-label" runat="server" Text="Query"></asp:Label>
                        <asp:HiddenField ID="profile_idHidden" runat="server" />
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtquery" runat="server" class="form-control" AutoPostBack="false" TextMode="MultiLine" Width="700px" Height="200px"></asp:TextBox>
                    </div>



                    <div class="col-md-4" style="width: auto; margin-left: 770px; margin-top: 20px">
                        <asp:Button ID="btnsave" runat="server" class="btn btn-primary" Text="Run This Query" OnClick="btnsave_Click" OnClientClick=" return validate()" />
                    </div>
                </div>
            </div>
        </div>
        <div class="box box-primary">
            <div class="box-header">
                <span style="font-size: large">Query Result</span>
                <br />
                <br />
                <div class="row  table-responsive">
                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="true" OnRowDataBound="grd_RowDataBound">
                        </asp:GridView>
                    </div>

                </div>
                <div style="margin-top: 20px; margin-bottom: 10px; margin-bottom: 10px">
                    <asp:Button ID="btnExport" CssClass="btn btn-primary"
                        runat="server" Text="Export To Excel" OnClick="btnExport_Click" />
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
            function validate() {
                if (document.getElementById("<%=txtprofile.ClientID%>").value == "") {
                   alert("Profile Name Feild can not be blank.");
                   document.getElementById("<%=txtprofile.ClientID%>").focus();
                   return false;
               }
               if (document.getElementById("<%=txtquery.ClientID%>").value == "") {
                   alert("Query Feild can not be blank.");
                   document.getElementById("<%=txtquery.ClientID%>").focus();
                   return false;
               }
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

