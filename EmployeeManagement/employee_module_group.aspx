<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="employee_module_group.aspx.cs" Inherits="EmployeeManagement.employee_module_group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <section class="content-header">
        <h1>Manage Module Group
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>
        <%--<ol class="breadcrumb">
           <i class="fa fa-dashboard"></i>
            <li><a href="../Search_Employee.aspx"><i></i>Search Employee</a></li>
            <li class="active">Employee Module Group</li>
        </ol>--%>
    </section>
    <section class="content">
        <div class="box box-default">
            <div class="box-header with-border">
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="lblgroup" class="control-label" runat="server" Text="Group Name"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtgroup" runat="server" class="form-control" AutoPostBack="false"></asp:TextBox>
                        </div>
                        <div class="col-md-4" style="width: auto"></div>
                        <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                            <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                        </span>
                    </div>
                    <br />
                    <br />
                    <br />
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="lblgrpname" class="control-label" runat="server" Text="Module List" ></asp:Label>
                            <asp:HiddenField ID="grp_idHidden" runat="server" />
                        </div>
                        <div class="col-md-3">
                            <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="275" Width="190%">
                                <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" EnablePersistedSelection="true" DataKeyNames="module_id" OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Show" ControlStyle-Width="20px">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="false" 
                onclick="checkAll(this);"></asp:CheckBox>

                                            </HeaderTemplate>
                                            <ItemTemplate>
                                               <%-- <asp:CheckBox ID="cbSelect" CssClass="chkbox Group1" data-value="1" runat="server"></asp:CheckBox>--%>
                                                 <asp:CheckBox ID="cbSelect" runat="server" AutoPostBack="false" 
                onclick="Check_Click(this)"> </asp:CheckBox>
                                                <asp:Label ID="lblisshow" runat="server" Text='<%# Eval("module_id") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Module_Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModule_id" runat="server" Visible="false" Text='<%# Eval("module_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Module Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmoduledName" runat="server" Text='<%# Eval("module_name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sort Number">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsortnumber" runat="server" AutoPostBack="false" Text='<%# Eval("sort") %>' onkeypress="return isNumberKey(event);"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                        <div class="col-md-4" style="margin-top: 280px">
                            <asp:Button ID="BtnSave" runat="server" class="btn btn-primary" Text="Save" Width="100px" OnClick="BtnSave_Click" OnClientClick=" return validate()" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="box box-primary">
            <div class="box-header">
                <span style="font-size: large">Employee Module Group List</span>
                <br />
                <br />
                <div class="row  table-responsive">
                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound" EnablePersistedSelection="true" OnRowCommand="grd_RowCommand" OnRowEditing="grd_RowEditing" DataKeyNames="module_group_id">
                            <Columns>
                                <asp:TemplateField HeaderText="Group Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgroupid" runat="server" Visible="false" Text='<%# Eval("module_group_id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgroupname" runat="server" Text='<%# Eval("module_group_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Modules Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmodulename" runat="server" Text='<%# Eval("module_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" ShowHeader="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%# Eval("module_group_id")  %>' CommandName="Edit" Text="Edit"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this Module?');" CommandName="del" CommandArgument='<%#Eval("module_group_id")%>'>Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#EDE8E8"></HeaderStyle>
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
                if (document.getElementById("<%=txtgroup.ClientID%>").value == "") {
                    alert("Group Name Feild can not be blank");
                    document.getElementById("<%=txtgroup.ClientID%>").focus();
                    return false;
                }
                var gridView = document.getElementById("<%=GridView1.ClientID %>");
                var checkBoxes = gridView.getElementsByTagName("input");
                for (var i = 0; i < checkBoxes.length; i++) {
                    if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                        args.IsValid = true;
                        return;
                    }
                }
                alert("Atleast select 1 Module");
                return false;
                args.IsValid = false;
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

<%--        <script type="text/javascript">
            function selectall() {
                
                var gridView = document.getElementById("<%=GridView1.ClientID %>");
                var checkBoxes = gridView.getElementsByTagName("input");
                var headercheck = document.getElementById("ContentPlaceHolder1_GridView1_chkHeader");
                if (headercheck.checked) {
                    for (var i = 0; i < checkBoxes.length; i++) {
                        if (checkBoxes[i].type == "checkbox") {
                            checkBoxes[i].checked=true;
                            //return;
                        }
                    }
                }
                else {
                    for (var i = 0; i < checkBoxes.length; i++) {
                        if (checkBoxes[i].type == "checkbox") {
                            checkBoxes[i].checked=false;
                            //return;
                        }
                    }
                }
                //alert("Atleast select 1 Module");
                //return false;
                //args.IsValid = false;
            }
        </script>
        <script type="text/javascript">
            function cbSelect_CheckedChanged() {
                var count = 0;
                var totalRowCountGrid = 0;
                var headercheck = document.getElementById("ContentPlaceHolder1_GridView1_chkHeader");
                var gridView = document.getElementById("<%=GridView1.ClientID %>");
                var checkBoxes = gridView.getElementsByTagName("input");
               
                for (var i = 0; i < checkBoxes.length; i++) {
                    if (checkBoxes[i].type == "checkbox") {
                        totalRowCountGrid++;
                        // return;
                    }
                }
                for (var i = 0; i < checkBoxes.length; i++) {
                    if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                        count++;
                       // return;
                    }
                }
                if (count == totalRowCountGrid) {
                    headercheck.Checked = true;
                }
                else {
                    headercheck.Checked = false;
                }
            }
        </script>--%>

        
        <script type="text/javascript">
            function Check_Click(objRef) {
                //Get the Row based on checkbox
                var row = objRef.parentNode.parentNode.parentNode.parentNode;
                //Get the reference of GridView
                var GridView = row.parentNode;

                //Get all input elements in Gridview
                var inputList = GridView.getElementsByTagName("input");

                for (var i = 0; i < inputList.length; i++) {
                    //The First element is the Header Checkbox
                    var headerCheckBox = inputList[0];

                    //Based on all or none checkboxes
                    //are checked check/uncheck Header Checkbox
                    var checked = true;
                    if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                        if (!inputList[i].checked) {
                            checked = false;
                            break;
                        }
                    }
                }
                headerCheckBox.checked = checked;

            }
        </script>
        <script type="text/javascript">
            function checkAll(objRef) {
                var GridView = objRef.parentNode.parentNode.parentNode.parentNode.parentNode;
                var inputList = GridView.getElementsByTagName("input");
                for (var i = 0; i < inputList.length; i++) {
                    //Get the Cell To find out ColumnIndex
                    var row = inputList[i].parentNode.parentNode;
                    if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                        if (objRef.checked) {
                            inputList[i].checked = true;
                        }
                        else {
                            inputList[i].checked = false;
                        }
                    }
                }
            }
        </script>

    </section>
</asp:Content>
