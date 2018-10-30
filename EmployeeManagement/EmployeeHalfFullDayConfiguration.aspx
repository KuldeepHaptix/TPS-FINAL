<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeHalfFullDayConfiguration.aspx.cs" Inherits="EmployeeManagement.EmployeeHalfFullDayConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1>Employee Half/Full Day Configuration    
            <small>
                <asp:Label runat="server" ID="lblDateTime"> </asp:Label>
            </small>
        </h1>
    </section>
    <br />
    <div class="box box-primary">
        <div class="box-header">
            <span style="font-size: large">Employee List</span>
            <br />
            <asp:Button ID="Button1" CssClass="btn btn-primary"
                runat="server" Text="Save" Style="width: 70px; float: right" OnClick="Button1_Click"/>
            <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
            </span>
            <br />
            <br />
            <div class="row  table-responsive">
                <div style="width: 85%; margin-left: 2%; margin-right: 20%;">
                    <asp:GridView ID="grdEmplist" CssClass="table table-bordered table-hover grdEmplist" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="false" OnRowDataBound="grdEmplist_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="EMPID">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmp_id" runat="server" Text='<%# Eval("EmpId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="FPID" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFpId" runat="server" Visible="true" Text='<%# Eval("FP_Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmp_Name" Width="150px" onkeypress="return isNumberKey(event);" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mon Half">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtmonhalf" Width="150" CssClass="form-control" runat="server" onkeypress="return isNumberKey(event);" Text='<%# Eval("Mon_Half") %>'></asp:TextBox>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mon Full">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtmonfull" Width="120" CssClass="form-control" onkeypress="return isNumberKey(event);" runat="server" Text='<%# Eval("Mon_Full") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tue Half">
                                <ItemTemplate>
                                    <asp:TextBox ID="txttuehalf" Width="150" CssClass="form-control" runat="server" onkeypress="return isNumberKey(event);" Text='<%# Eval("Tue_Half") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tue Full">
                                <ItemTemplate>
                                    <asp:TextBox ID="txttuefull" Width="150" CssClass="form-control" onkeypress="return isNumberKey(event);" runat="server" Text='<%# Eval("Tue_Full") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Wed Half">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtwedhalf" Width="150" CssClass="form-control" onkeypress="return isNumberKey(event);" runat="server" Text='<%# Eval("Wed_Half") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Wed Full">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtwedfull" Width="150" CssClass="form-control" onkeypress="return isNumberKey(event);" runat="server" Text='<%# Eval("Wed_Full") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Thu Half">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtthuhalf" Width="150" CssClass="form-control" onkeypress="return isNumberKey(event);" runat="server" Text='<%# Eval("Thu_Half") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Thu Full">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtthufull" Width="150" CssClass="form-control" onkeypress="return isNumberKey(event);" runat="server" Text='<%# Eval("Thu_Full") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fri Half">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtfrihalf" Width="150" CssClass="form-control" onkeypress="return isNumberKey(event);" runat="server" Text='<%# Eval("Fri_Half") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fri Full">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtfrifull" Width="150" CssClass="form-control" onkeypress="return isNumberKey(event);" runat="server" Text='<%# Eval("Fri_Full") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sat Half">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtsathalf" Width="150" CssClass="form-control" onkeypress="return isNumberKey(event);" runat="server" Text='<%# Eval("Sat_Half") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sat Full">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtsatfull" Width="150" CssClass="form-control" onkeypress="return isNumberKey(event);" runat="server" Text='<%# Eval("Sat_Full") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sun Half">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtsunhalf" Width="150" CssClass="form-control" onkeypress="return isNumberKey(event);" runat="server" Text='<%# Eval("Sun_Half") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sun Full">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtsunfull" Width="150" CssClass="form-control" onkeypress="return isNumberKey(event);" runat="server" Text='<%# Eval("Sun_Full") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.grdEmplist').DataTable({
                "fixedHeader": true,
                "paging": true,
                "lengthChange": true,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": true
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
    <%--<script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                if (charCode == 46) {
                    return true;
                }
                else {
                    return false;
                }

            return true;
        }
    </script>--%>

</asp:Content>
