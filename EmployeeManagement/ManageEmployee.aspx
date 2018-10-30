<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ManageEmployee.aspx.cs" Inherits="EmployeeManagement.ManageEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1>Add Customer Complaint Details
               
            <small>
                <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>
        <ol class="breadcrumb">
            <%-- <li><a href="../Dashboard.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li class="active">Manage Employee</li>--%>
        </ol>
    </section>
    <section class="content">
        <%-- <div class="box-body">
            <div class="box-group" id="accordion">

                <%--general--%>
        <%-- <div class="panel box box-default" id="panel_1">
                    <div class="box-header with-border" data-id="1">
                        <h3 class="box-title"><b>Customer Basic Details</b></h3>

                        <div class="box-tools pull-right">
                            <button data-id="1" type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="row col-md-8">
                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="lblId" class="control-label" runat="server" Text="Last Name"></asp:Label><label style="color: red">*</label>
                                        <asp:HiddenField ID="Emp_idHidden" runat="server" />
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtlname" runat="server" class="form-control" AutoPostBack="false" TabIndex="1"></asp:TextBox>
                                    </div>


                                    <div class="col-md-2">
                                        <asp:Label ID="Label1" class="control-label" runat="server" Text="Customer Name"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtname" runat="server" class="form-control" AutoPostBack="false" TabIndex="2"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />

                            <br />
                            <br />
                            <br />
                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label4" class="control-label" runat="server" Text="Date of Complaint"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox runat="server" class="form-control pull-right dates" ID="datepickerdob" TabIndex="6" />

                                    </div>

                                    <div class="col-md-2">
                                        <asp:Label ID="Label2" class="control-label" runat="server" Text="Res Address"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtresiadd" runat="server" Style="width: 100%" class="form-control" AutoPostBack="false" TextMode="MultiLine" Onchange="return Add()" TabIndex="23"></asp:TextBox>
                                    </div>


<%--                                    <div class="col-md-2">
                                        <asp:Label ID="Label5" class="control-label" runat="server" Text="Date of join"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox runat="server" class="form-control pull-right dates" ID="datepickerdoj" TabIndex="7" />


                                    </div>--%>
        <%--</div>--%>
        <%-- <br />
                                <br />
                                <br />
                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label6" class="control-label" runat="server" Text="Gender"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:RadioButton ID="rdbmale" runat="server" Text="&nbsp;Male" Checked="true" GroupName="gender"  TabIndex="8"/>
                                        &nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbFemale" runat="server" Text="&nbsp;Female" GroupName="gender"  TabIndex="9"/>
                                    </div>


                                    <div class="col-md-2">
                                        <asp:Label ID="Label7" class="control-label" runat="server" Text="Email"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtEmail" runat="server" class="form-control" AutoPostBack="false" TabIndex="10"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label9" class="control-label" runat="server" Text="Status"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <select class="form-control select2" runat="server" style="width: 100%" id="ddlStatus1" tabindex="11">
                                        </select>
                                    </div>


                                    <div class="col-md-2">
                                        <asp:Label ID="Label10" class="control-label" runat="server" Text="Category"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <select class="form-control select2" runat="server" style="width: 100%" id="ddlcategory" tabindex="12">
                                        </select>
                                    </div>
                                </div>

                                <br />
                                <br />
                                <br />--%>
        <%-- </div>--%>
        <div class="box box-default hide " id="panel_7">
            <div class="box-body">
                <div class="row col-md-4">
                    <div class="col-md-12">

                        <div class="col-md-5">
                            <asp:Label ID="Label15" class="control-label" runat="server" Text="Middle Name"></asp:Label><label style="color: red">*</label>
                        </div>
                        <div class="col-md-7">
                            <asp:TextBox ID="txtmname" runat="server" class="form-control" AutoPostBack="false" TabIndex="3"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <br />
                    <br />
                    <%--<div class="col-md-12">

                                    <div class="col-md-5">
                                        <asp:Label ID="Label13" class="control-label" runat="server" Text="Photo"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:Image ID="Image1" runat="server" Style="height: 150px; width: 150px;" ImageUrl="~/photo.jpg" />
                                        <asp:FileUpload ID="filePhotos" onchange="previewFile()" runat="server" accept=".jpg,.jpeg"  TabIndex="15"/>
                                        <br />
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                    <asp:CheckBox ID="chkphoto" runat="server" Text="&nbsp;Remove Employee Photo" TabIndex="16" />
                                    
                                </div>--%>
                </div>
                <div class="row col-md-8">

                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label11" class="control-label" runat="server" Text="DepartMent"></asp:Label><label style="color: red">*</label>
                        </div>
                        <div class="col-md-4">
                            <select class="form-control select2" runat="server" style="width: 100%" id="ddldepart" tabindex="13">
                            </select>
                        </div>


                        <div class="col-md-2">
                            <asp:Label ID="Label12" class="control-label" runat="server" Text="Designation"></asp:Label><label style="color: red">*</label>
                        </div>
                        <div class="col-md-4">
                            <select class="form-control select2" runat="server" style="width: 100%" id="ddldesignation" tabindex="14">
                            </select>
                        </div>
                    </div>
                    <br />
                    <br />
                    <br />
                    <div class="col-md-12">
                        <div class="col-md-6">
                            <div id="val_msg_wrap" class="abc">
                                <span style="color: red; margin-left: 15px">
                                    <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2"></div>
                        <div class="col-md-4">
                            <asp:Button ID="btnsave" runat="server" class="btn btn-primary" Text="Save" Style="width: 150px" OnClick="btnsave_Click" OnClientClick=" return validate()" TabIndex="19" />
                        </div>
                    </div>
                    <br />
                    <br />
                </div>
                <%--<div class="row col-md-4">
                                <div class="col-md-12">

                                    <div class="col-md-5">
                                        <asp:Label ID="Label14" class="control-label" runat="server" Text="Signature"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:Image ID="Image2" runat="server" Style="height: 60px; width: 150px;" ImageUrl="~/sign.jpg" />
                                        <asp:FileUpload ID="empsign" onchange="previewFile1()" runat="server" accept=".jpg,.jpeg" TabIndex="17" />
                                        <br />
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                    <asp:CheckBox ID="chksign" runat="server" Text="&nbsp;Remove Employee Signature" TabIndex="18" />                                    
                                </div>



                            </div>--%>
                <%--</div>
                    </div>
                </div>--%>

                <%--//Personal--%>
            </div>
        </div>
        <div class="box box-default " id="panel_2">
            <div class="box-header with-border" data-id="2">
                <h3 class="box-title"><b>Employee Personal Details</b></h3>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="row col-md-12">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div class="col-md-2">
                                    <asp:Label ID="Labe1l8" class="control-label" runat="server" Text="Name"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <%--<select class="form-control select2" style="width: 100%" runat="server" id="ddlreligion" tabindex="20">
                                        </select>--%>
                                    <asp:TextBox ID="txtname" runat="server" Style="width: 170%" class="form-control" AutoPostBack="false" TabIndex="1"></asp:TextBox>
                                </div>

                            </div>
                            <div class="col-md-6">

                                <div class="col-md-4">
                                    <asp:Label ID="Label116" class="control-label" runat="server" Text="Complaint No"></asp:Label>
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtcompno" ReadOnly="true" runat="server" class="form-control" AutoPostBack="false" TabIndex="22"></asp:TextBox>

                                </div>

                            </div>


                        </div>
                        <br />
                        <br />
                        <br />
                        <div class="col-md-12">

                            <div class="col-md-2">
                                <asp:Label ID="Label8" class="control-label" runat="server" Text="Contact No"></asp:Label><label style="color: red">*</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtcontactno" runat="server" Style="width: 100%" class="form-control" AutoPostBack="false" onkeypress="return isNumberKey(event)" TabIndex="23"></asp:TextBox>
                            </div>


                            <div class="col-md-2">
                                <asp:Label ID="Label16" class="control-label" runat="server" Text="Alternate No"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtotherno" runat="server" onkeypress="return isNumberKey(event)" Style="width: 100%" class="form-control" AutoPostBack="false" TabIndex="24"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label17" class="control-label" runat="server" Text="Res City"></asp:Label><label style="color: red">*</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtcity" runat="server" class="form-control" AutoPostBack="false" TabIndex="26"></asp:TextBox>
                            </div>

                        </div>
                        <br />
                        <br />
                        <br />
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <asp:Label ID="Label18" class="control-label" runat="server" Text="Pin Code"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtpincode" runat="server" onkeypress="return isNumberKey(event)" class="form-control" AutoPostBack="false" TabIndex="27"> </asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label19" class="control-label" runat="server" Text="Resi Address"></asp:Label><label style="color: red">*</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtresiAdd" runat="server" class="form-control" AutoPostBack="false" Onchange="return Add()" TextMode="MultiLine" TabIndex="29"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label20" class="control-label" runat="server" Text="Booking Date"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtcomplaintDate" runat="server"  class="form-control  dates" AutoPostBack="false" TabIndex="30"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <br />
                        <br />
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <asp:Label ID="Label21" class="control-label" runat="server" Text="CompleteDate"></asp:Label><label style="color: red">*</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtcompdate" runat="server"  class="form-control dates" AutoPostBack="false" TabIndex="32"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label22" class="control-label" runat="server" Text="Select Status"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <select class="form-control select2" style="width: 100%" runat="server" id="ddlStatus" tabindex="20">
                                </select>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label23" class="control-label" runat="server" Text="Description"></asp:Label><label style="color: red">*</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtdesc" runat="server" class="form-control" TextMode="MultiLine" AutoPostBack="false" TabIndex="35"></asp:TextBox>
                            </div>

                        </div>
                        <br />
                        <br />
                        <br />

                        <div class="col-md-12">
                            <div class="col-md-2">
                                <asp:Label ID="Label24" class="control-label" runat="server" Text="Per Pincode"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtPpin" runat="server" class="form-control" AutoPostBack="false" onkeypress="return isNumberKey(event);" TabIndex="36"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label31" class="control-label" runat="server" Text="OTP "></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtotp" runat="server" onkeypress="return isNumberKey(event)" class="form-control" AutoPostBack="false" TabIndex="37"></asp:TextBox>
                            </div>


                            <div class="col-md-2">
                                <asp:Label ID="Label32" class="control-label" runat="server" Text="PF No"></asp:Label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtpf" runat="server" class="form-control" AutoPostBack="false" onkeypress="return isNumberKey(event);" TabIndex="39"></asp:TextBox>
                            </div>


                        </div>
                        <br />
                        <br />
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <div id="val_msg_wrap_p" class="abc">
                                    <span style="color: red; margin-left: 15px">
                                        <asp:Literal ID="ltrper" runat="server"></asp:Literal>
                                    </span>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <asp:Button ID="btnAddComailnt" runat="server" class="btn btn-primary" Text="Save" Style="width: 150px" OnClientClick=" return validatePersonalDetails()" OnClick="btnAddComailnt_Click" TabIndex="51" />
                            </div>
                        </div>
                    </div>
                    <div class="row col-md-12">

                        <br />
                        
                    </div>
                </div>
            </div>
        </div>

        <%--Family--%>
        <div class="panel box box-default collapsed-box hide" id="panel_3">
            <div class="box-header with-border" data-id="3">
                <h3 class="box-title"><b>Employee Family Details</b></h3>

                <div class="box-tools pull-right">
                    <button data-id="3" type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">
                    <div class="row col-md-8">
                        <div class="col-md-12">

                            <div class="col-md-2">
                                <asp:Label ID="Label40" class="control-label" runat="server" Text="Relation"></asp:Label><label style="color: red">*</label>
                            </div>
                            <div class="col-md-4">
                                <select class="form-control select2" style="width: 100%" runat="server" id="ddlrelation" tabindex="52">
                                </select>
                            </div>

                            <div class="col-md-2">
                                <asp:Label ID="Label41" class="control-label" runat="server" Text="Reletive Name"></asp:Label><label style="color: red">*</label>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtRelationName" runat="server" class="form-control" AutoPostBack="false" TabIndex="53"></asp:TextBox>
                            </div>


                        </div>
                        <br />
                        <br />
                        <br />
                        <div class="col-md-12">

                            <div class="col-md-2">
                                <asp:Label ID="Label42" class="control-label" runat="server" Text="Description"></asp:Label>
                            </div>
                            <div class="col-md-10">
                                <asp:TextBox ID="txtFamilyDesc" runat="server" Style="width: 100%" class="form-control" AutoPostBack="false" TextMode="MultiLine" TabIndex="55"></asp:TextBox>
                            </div>

                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <div id="val_msg_wrap_F" class="abc">
                                    <span style="color: red; margin-left: 15px">
                                        <asp:Literal ID="lttF" runat="server"></asp:Literal>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-2"></div>
                            <div class="col-md-4">
                                <%-- <asp:Button ID="btnSaveFamily" runat="server" class="btn btn-primary" Text="ADD" Style="width: 150px" OnClientClick=" return validateFamilyDetails()" OnClick="btnSaveFamily_Click" TabIndex="57" />--%>
                            </div>
                        </div>
                    </div>
                    <div class="row col-md-4">
                        <div class="col-md-12">

                            <div class="col-md-5">
                                <asp:Label ID="Label43" class="control-label" runat="server" Text="Date of birth"></asp:Label>
                            </div>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtFamilyDob" runat="server" class="form-control dates" AutoPostBack="false" TabIndex="54"></asp:TextBox>
                            </div>

                        </div>
                        <br />
                        <br />
                        <br />
                        <div class="col-md-12">

                            <div class="col-md-5">
                                <asp:Label ID="Label44" class="control-label" runat="server" Text="Contact No"></asp:Label>
                            </div>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtFamilyPh_no" runat="server" class="form-control" AutoPostBack="false" onkeypress="return isNumberKey(event);" TabIndex="56"></asp:TextBox>
                            </div>

                        </div>
                    </div>

                </div>
                <br />

               
            </div>

        </div>
    </section>
    <script>
        $(function () {
            $('.dates').datepicker({
                autoclose: true
            });
        });
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
                $(".abc").show();
                setTimeout(function () {
                    $(".abc").hide();

                }, 3000);
                return false;
            }
        }
        callfootercss();
    </script>
    <style>
        .abc {
        }
    </style>
    <style>
        .dates {
        }

        .gridview {
        }
    </style>
    <script>
        $(document).ready(function () {
            var b = getCookie("collapsedPanelArr").split(",");
            $("#accordion .box-default").removeClass("collapsed-box");
            $("#accordion .box-default .box-header button i").removeClass("fa-plus");
            $("#accordion .box-default .box-header button i").addClass("fa-minus");
            for (var i = 0; i <= b.length - 1 ; i++) {
                $("#panel_" + b[i]).addClass("collapsed-box");
                $("#panel_" + b[i] + " .box-header button i").removeClass("fa-minus");
                $("#panel_" + b[i] + " .box-header button i").addClass("fa-plus");
                console.log(b[i]);
            }
        });
        $('#accordion').on('hidden.bs.collapse', function () {
            // do something…
        })
        function setCookie(cname, cvalue, exdays) {
            var d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            var expires = "expires=" + d.toUTCString();
            document.cookie = cname + "=" + cvalue + "; " + expires;
        }
        function getCookie(cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) == 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        }
	</script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.gridview').DataTable({
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
    function validatePersonalDetails() {
        
            if (document.getElementById("<%=txtname.ClientID%>").value == "") {
                alert("User Name can not be blank");
                document.getElementById("<%=txtname.ClientID%>").focus();
                return false;
            }
        if (document.getElementById("<%=txtcontactno.ClientID%>").value == "") {
            alert("Contact No can not be blank");
            document.getElementById("<%=txtcontactno.ClientID%>").focus();
            return false;
        }
        if (document.getElementById("<%=txtcity.ClientID%>").value == "") {
            alert("Residential City  can not be blank");
            document.getElementById("<%=txtcity.ClientID%>").focus();
            return false;
        }
        if (document.getElementById("<%=ddlStatus.ClientID%>").value == "-1") {
            alert("Select Valid Status");
            document.getElementById("<%=ddlStatus.ClientID%>").focus();
            return false;
        }

        if (document.getElementById("<%=txtresiAdd.ClientID%>").value == "") {
                alert("Residential  Address can not be blank");
                document.getElementById("<%=txtresiAdd.ClientID%>").focus();
                return false;
            }
            
            if (document.getElementById("<%=txtname.ClientID%>").value == "") {
                alert("Residential State  can not be blank");
                document.getElementById("<%=txtname.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtcontactno.ClientID%>").value == "") {
                alert("Contact No can not be blank");
                document.getElementById("<%=txtcontactno.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtpincode.ClientID%>").value == "") {
                alert("Residential pincode  can not be blank.");
                document.getElementById("<%=txtpincode.ClientID%>").focus();
                return false;
            }

            return true;
        }
    </script>

</asp:Content>
