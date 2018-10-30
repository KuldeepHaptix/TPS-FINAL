<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ManageDesignation.aspx.cs" Inherits="EmployeeManagement.ManageDesignation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <section class="content-header">
        <h1>Manage Designation
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>
        <%--<ol class="breadcrumb">
           
            <li class="active">Manage Designation</li>
        </ol>--%>
    </section>
        <section class="content">
        <div class="box box-primary">
            <div class="box-header">
                <div class="row">

                    <%--<div class="col-md-8"><i class="fa fa-plus" style="font-size: x-large"></i></div>--%>
                    <br />
                    <div class="col-md-12"></div>
                    <div class="col-md-1">
                    </div>
                    <div class="col-md-2">
                          <asp:Label ID="lblId" class="control-label" runat="server" Text="Designation Name"></asp:Label>
                    </div>
                    <div class="col-md-3">
                     <asp:TextBox ID="txtdesignames" runat="server" class="form-control" AutoPostBack="false"></asp:TextBox>
                    </div>
                    
                    <div class="col-md-4" style="width: auto">
                         <asp:Button ID="btnsave" runat="server" class="btn btn-primary" Text="Add New Designation" OnClick="btnsave_Click"  OnClientClick=" return validate()" />
                 
                    
                        <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                            <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                        </span>
                    </div>
                </div>
            </div>
        </div>

        <div class="box box-primary">
            <div class="box-header">
                <span style="font-size: large">Designation List</span>
                <br />
                <br />
                <div class="row  table-responsive">
                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowCommand="grd_RowCommand" OnRowDataBound="grd_RowDataBound" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="designation_id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesignation_id" runat="server" Visible="false" Text='<%# Eval("designation_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="IsMasterData" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsMasterData" runat="server" Visible="false" Text='<%# Eval("IsMasterData") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesignation_Name" runat="server" Text='<%# Eval("Designation_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDesignation_Name" runat="server" Text='<%# Eval("Designation_Name") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%# Eval("designation_id")+ ":" + Container.DataItemIndex  %>' CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="btnupdate" runat="server" CommandName="Update" Text="Update" CommandArgument='<%# Eval("designation_id")+ ":" + Container.DataItemIndex  %>'></asp:LinkButton>
                                        <asp:LinkButton ID="btncancel" runat="server" CommandName="Cancel" Text="Cancel" CommandArgument='<%# Eval("designation_id")+ ":" + Container.DataItemIndex  %>'></asp:LinkButton>
                                    </EditItemTemplate>

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this Designation?');" CommandName="del" CommandArgument='<%#Eval("designation_id")+":"+Eval("designation_id")%>'>Delete</asp:LinkButton>
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
            <script type="text/javascript">
                function validate() {
                    if (document.getElementById("<%=txtdesignames.ClientID%>").value == "") {
                    alert("Designation Name Feild can not be blank");
                    document.getElementById("<%=txtdesignames.ClientID%>").focus();
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
