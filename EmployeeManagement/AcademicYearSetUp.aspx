<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" CodeBehind="AcademicYearSetUp.aspx.cs" Inherits="EmployeeManagement.AcademicYearSetUp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">

        <h1>SetUp Academic Year
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>

    </section>
    <section class="content">
        <div class="panel box-default" id="panel1">
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="lblprofile" CssClass="control-label" runat="server" Text="Select Year"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="dlYear" CssClass="form-control select2 " runat="server" AutoPostBack="false">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-4" style="width: auto"></div>
                        <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                            <asp:Literal ID="ltrErr" runat="server"></asp:Literal></span>
                    </div>
                </div>

                <br />

                <%--  <div class="clearfix"></div>--%>
                <div class="row">

                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="lbl1" Text="Select StartDate" CssClass="control-label" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtstartdate" class="form-control dates" AutoPostBack="true" runat="server" OnTextChanged="txtstartdate_TextChanged"></asp:TextBox>
                        </div>

                        <div class="col-md-1">
                            <asp:Label ID="lblTotal" runat="server" Text="Total Days" Width="70px" CssClass="control-label"></asp:Label>
                        </div>
                        <div class="col-md-1">
                            <asp:TextBox ID="txttotaldays" runat="server" CssClass="form-control " Enabled="false"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lbl2" CssClass="control-label" Text="Select EndDate" runat="server"></asp:Label>
                        </div>

                        <div class="col-md-3">
                            <asp:TextBox ID="txtenddate" class="form-control dates" AutoPostBack="true" runat="server" OnTextChanged="txtenddate_TextChanged"></asp:TextBox>
                        </div>
                    </div>

                </div>


                <div class="row">
                    <div class="col-md-12 text-center" style="padding: 10px">
                        <asp:Button ID="btnSave" CssClass="btn btn-primary"
                            runat="server" Text="Save" Style="width: 150px" OnClick="btnSave_Click" OnClientClick="return ValidateData()" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>
                </div>
            </div>
        </div>

        <div class="box box-primary">
            <div class="box-header">
                <span style="font-size: large">Edit - Delete Academic Year</span>

                <div class="row  table-responsive">
                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="grdyearlist" OnRowDataBound="grdyearlist_RowDataBound" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowCommand="grdyearlist_RowCommand" OnRowCancelingEdit="grdyearlist_RowCancelingEdit" OnRowEditing="grdyearlist_RowEditing" OnRowUpdating="grdyearlist_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="year_id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblyearid" runat="server" Visible="false" Text='<%# Eval("year_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AcademicYear">
                                    <ItemTemplate>
                                        <asp:Label ID="txtAcademicYear1" Enabled="false" runat="server" Text='<%# Eval("Academic_Year") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="StartDate">
                                    <ItemTemplate>
                                        <asp:Label ID="txtStartdate1" runat="server" Text='<%# Eval("Start_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtStartdate" class="form-control dates" runat="server" Text='<%# Eval("Start_Date") %>'></asp:TextBox>
                                    </EditItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EndDate">
                                    <ItemTemplate>
                                        <asp:Label ID="txtEnddate1" runat="server" Text='<%# Eval("End_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEnddate" runat="server" class="form-control dates" Text='<%# Eval("End_Date") %>'></asp:TextBox>
                                    </EditItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%# Eval("year_id")+ ":" + Container.DataItemIndex  %>' CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="btnupdate" runat="server" CommandName="Update" Text="Update" CommandArgument='<%# Eval("year_id")+ ":" + Container.DataItemIndex  %>'></asp:LinkButton>
                                        <asp:LinkButton ID="btncancel" runat="server" CommandName="Cancel" Text="Cancel" CommandArgument='<%# Eval("year_id")+ ":" + Container.DataItemIndex  %>'></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this Year?');" CommandName="del" CommandArgument='<%#Eval("year_id")+":"+Eval("year_id")%>'>Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <%-- <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">

                        <div class="col-md-3"></div>
                        <div style="margin-top: 20px; margin-bottom: 10px; margin-bottom: 10px" class="col-md-3">
                            <asp:Button ID="Button1" CssClass="btn btn-primary"
                                runat="server" Text="Save" style="width:150px" OnClick="btnsave_Click"/>
                        </div>
                            <div class="col-md-4" style="width: auto"></div>
                        <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        </span>
                    </div>
                            </div>--%>
            </div>

        </div>


    </section>

    <script type="text/javascript">

        function ValidateData() {
            if (document.getElementById("<%=txtstartdate.ClientID%>").value == "") {
                alert("Start Date can not be blank");
                document.getElementById("<%=txtstartdate.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtenddate.ClientID%>").value == "") {
                alert("End date can not be blank");
                document.getElementById("<%=txtenddate.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=dlYear.ClientID%>").value == "-2") {
                alert("AcademicYear can not be blank");
                document.getElementById("<%=dlYear.ClientID%>").focus();
                return false;
            }

        }

    </script>
    <script>
        $(function () {
            $('.dates').datepicker({
                autoclose: true
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentPlaceHolder1_grdyearlist').DataTable({
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

    <script type="text/javascript">
        function callfootercss() {
            var options = $('[id*=ltrErr] + .btn-group ul li.active');
            if (options.length == 0) {
                $("#val_msg_wrap").show();
                setTimeout(function () {
                    $("#val_msg_wrap").hide();
                }, 2000);
                return false;
            }
        }
        callfootercss();

    </script>
</asp:Content>
