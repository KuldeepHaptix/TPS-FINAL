<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DisplayColumnConfigurationForSearchEmployee.aspx.cs" Inherits="EmployeeManagement.DisplayColumnConfigurationForSearchEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>DisplayColumnConfigurationForSearchEmployee
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="../Search_Employee.aspx"><i></i>Search Employee</a></li>
            <li class="active">DisplayColumnConfigurationForSearchEmployee</li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-header">
                <span style="font-size: large">Employee Search Fields List</span>

                <div class="row  table-responsive">
                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound" EnablePersistedSelection="true" DataKeyNames="Employee_Field_id">
                            <Columns>
                                <asp:TemplateField HeaderText="Show" ControlStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelect" ssClass="gridCB" runat="server"></asp:CheckBox>
                                        <asp:Label ID="lblisshow" runat="server" Text='<%# Eval("Is_Show") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee_Field_id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmployee_Field_id" runat="server" Visible="false" Text='<%# Eval("Employee_Field_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Field Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFieldName" runat="server" Text='<%# Eval("FieldName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Field Display Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDisplay_Name" runat="server" Text='<%# Eval("FieldDisplayName") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sort Number">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtsortnumber" runat="server" onchange="return IsValidTest(this);" Text='<%# Eval("SortNumber") %>' onkeypress="return isNumberKey(event);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <div style="margin-top: 20px; margin-bottom: 10px; margin-left: 500px">
                    <asp:Button ID="btnsave" runat="server" class="btn btn-primary" Text="Save" OnClick="btnsave_Click" Width="100px" OnClientClick=" return validate()" />
                    <span style="color: red; margin-left: 15px; width: auto" id="val_msg_wrap">
                        <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                    </span>
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
            function validate(sender, args) {
                var gridView = document.getElementById("<%=grd.ClientID %>");
                var checkBoxes = gridView.getElementsByTagName("input");
                for (var i = 0; i < checkBoxes.length; i++) {
                    if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                        args.IsValid = true;
                        return;

                    }
                }
                alert("Atleast select 1 Checkbox");
                return false;
                args.IsValid = false;

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
        <script>

            function IsValidTest(Control) {

                name = Control.name;

                var list = document.getElementById("txtsortnumber");

                var contents = $("[id=txtsortnumber]");

                var TextboxId = name;

                var Txtbox = document.getElementsByName(name);
                var TextboxValue = Txtbox[0].value;

                for (i = 0; i < contents.length; i++) {

                    var currenttextboxName = contents[i].name;
                    var currenttextboxValue = contents[i].value;


                    //Compare currently changed value with existing values in other textboxes
                    if (name != currenttextboxName && currenttextboxValue == TextboxValue) {
                        alert('Employee Code has already been entered');
                        Txtbox[0].value = '';

                        return false;
                    }
                }

                return true;
            }

        </script>


    </section>
</asp:Content>
