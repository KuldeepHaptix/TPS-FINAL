<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ManageEmployee.aspx.cs" Inherits="EmployeeManagement.ManageEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1>Add Complaint Details
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>
        <ol class="breadcrumb">
            <%-- <li><a href="../Dashboard.aspx"><i class="fa fa-dashboard"></i>Dashboard</a></li>
            <li class="active">Manage Employee</li>--%>
        </ol>
    </section>
    <section class="content">
        <div class="box-body">
            <div class="box-group" id="accordion">

                <%--general--%>
               <%-- <div class="panel box box-default" id="panel_1">
                    <div class="box-header with-border" data-id="1">
                        <h3 class="box-title"><b>Employee Basic Details</b></h3>

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
                                        <asp:Label ID="Label1" class="control-label" runat="server" Text="First Name"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtfname" runat="server" class="form-control" AutoPostBack="false" TabIndex="2"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />

                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label2" class="control-label" runat="server" Text="Finger Print"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtfpId" runat="server" class="form-control" AutoPostBack="false" onkeypress="return isNumberKey(event);" TabIndex="4"></asp:TextBox>
                                    </div>


                                    <div class="col-md-2">
                                        <asp:Label ID="Label3" class="control-label" runat="server" Text="Contact No"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtphone" runat="server" class="form-control" AutoPostBack="false" onkeypress="return isNumberKey(event);" TabIndex="5"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">

                                    <div class=f"col-md-2">
                                        <asp:Label ID="Label4" class="control-label" runat="server" Text="Date of birth"></asp:Label><label style="color: red">*</label>
                                    </div><div class="col-md-4">
                                        <asp:TextBox runat="server" class="form-control pull-right dates" ID="datepickerdob" TabIndex="6" />
                                    </div>


                                    <div class="col-md-2">
                                        <asp:Label ID="Label5" class="control-label" runat="server" Text="Date of join"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox runat="server" class="form-control pull-right dates" ID="datepickerdoj" TabIndex="7" />


                                    </div>
                                </div>
                                <br />
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
                                        <asp:Label ID="Label9" class="control-label" runat="server" Text="Caste"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <select class="form-control select2" runat="server" style="width: 100%" id="ddlcaste" tabindex="11">
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
                                <br />
                            </div>
                            <div class="row col-md-4">
                                <div class="col-md-12">

                                    <div class="col-md-5">
                                        <asp:Label ID="Label15" class="control-label" runat="server" Text="Middle Name"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtmname" runat="server" class="form-control" AutoPostBack="false" tabindex="3"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">

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
                                    
                                </div>
                            </div>
                            <div class="row col-md-8">

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label11" class="control-label" runat="server" Text="DepartMent"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <select class="form-control select2" runat="server" style="width: 100%" id="ddldepart" TabIndex="13">
                                        </select>
                                    </div>


                                    <div class="col-md-2">
                                        <asp:Label ID="Label12" class="control-label" runat="server" Text="Designation"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <select class="form-control select2" runat="server" style="width: 100%" id="ddldesignation" TabIndex="14">
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
                                        <asp:Button ID="btnsave" runat="server" class="btn btn-primary" Text="Save" Style="width: 150px" OnClick="btnsave_Click" OnClientClick=" return validate()"  TabIndex="19"/>
                                    </div>
                                </div>
                                <br />
                                <br />
                            </div>
                            <div class="row col-md-4">
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



                            </div>
                        </div>
                    </div>
                </div>--%>

                <%--//Personal--%>

               <div class="box box-default collapsed-box" id="panel_2">
					<div class="box-header with-border" data-id="2">
                        <h3 class="box-title"><b>Customer Complaint Details</b></h3>

                        <div class="box-tools pull-right">
                            <button data-id="2" type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="row col-md-8">
                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Labe1l8" class="control-label" runat="server" Text="Customer Name"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                      <asp:TextBox ID="txtname" runat="server" class="form-control" AutoPostBack="false" Onchange="return Add()" TabIndex="1"></asp:TextBox>

                                    </div>


                                    <div class="col-md-6">
                                         <asp:Label ID="lblcompno" class="control-label" runat="server" Text="Complaint ID"></asp:Label>
                                    </div>

                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label8" class="control-label" runat="server" Text=" Address"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtresiAdd" runat="server" Style="width: 100%" class="form-control" AutoPostBack="false" TextMode="MultiLine" Onchange="return Add()" TabIndex="23"></asp:TextBox>
                                    </div>


                                    <div class="col-md-2">
                                        <asp:Label ID="Label16" class="control-label" runat="server" Text="State"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtState" runat="server" Style="width: 100%" class="form-control" AutoPostBack="false" TabIndex="24" Enabled="false" placeholder="M.P"></asp:TextBox>
                                    </div>


                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label17" class="control-label" runat="server" Text="Pin Code"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtpincode" runat="server" class="form-control" AutoPostBack="false" Onchange="return Add()" TabIndex="26"></asp:TextBox>
                                    </div>


                                    <div class="col-md-2">
                                        <asp:Label ID="Label18" class="control-label" runat="server" Text="Complaint Description"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtdesc" runat="server" class="form-control" AutoPostBack="false" TabIndex="27" TextMode="MultiLine" > </asp:TextBox>
                                    </div>


                                </div>
                                <br />
                                <br />
                                <br />

                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label19" class="control-label" runat="server" Text="Complaint Date"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtcompdate" runat="server" class="form-control" AutoPostBack="false" Onchange="return Add()" TabIndex="29"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label20" class="control-label" runat="server" Text="Per State"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtPState" runat="server" class="form-control" AutoPostBack="false" TabIndex="30"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label21" class="control-label" runat="server" Text="ContactNo"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtxontactno" runat="server" class="form-control" AutoPostBack="false" Onchange="return Add()" TabIndex="32"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label22" class="control-label" runat="server" Text="Alternate Contact No"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtotherno" runat="server" class="form-control" AutoPostBack="false" TabIndex="33"></asp:TextBox>
                                    </div>


                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label23" class="control-label" runat="server" Text="Other Details"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtotherdetails" runat="server" class="form-control" AutoPostBack="false" onkeypress="return isNumberKey(event);" Onchange="return Add()" TabIndex="35" TextMode="MultiLine"></asp:TextBox>
                                    </div>


                                    <div class="col-md-2">
                                        <asp:Label ID="Label24" class="control-label" runat="server" Text="Per Pincode"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtPpin" runat="server" class="form-control" AutoPostBack="false" onkeypress="return isNumberKey(event);" TabIndex="36"></asp:TextBox>
                                    </div>


                                </div>

                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label31" class="control-label" runat="server" Text="Pan card No"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtpancard" runat="server" class="form-control" AutoPostBack="false" TabIndex="37"></asp:TextBox >
                                    </div>


                                    <div class="col-md-2">
                                        <asp:Label ID="Label32" class="control-label" runat="server" Text="PF No"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtpf" runat="server" class="form-control" AutoPostBack="false" onkeypress="return isNumberKey(event);" TabIndex="39"></asp:TextBox>
                                    </div>


                                </div>
                                <br />
                                <br />
                                <br /> <br />
                                <div class="col-md-12">

                                    <%--<div class="col-md-2">
                                        <asp:Label ID="Label34" class="control-label" runat="server" Text="Resignation Proof"></asp:Label>
                                    </div>--%>
                                    <div class="col-md-4">
                                        <%--<asp:LinkButton ID="Resign" runat="server" Width="100%" OnClick="Resign_Click"></asp:LinkButton>
                                        <asp:HiddenField ID="resignfeild" runat="server" />
                                        <br />
                                        <asp:FileUpload ID="resigneFile" onchange="previewResign()" runat="server" accept=".pdf"  TabIndex="41"/>--%>
                                      

                                        <br />

                                        <%--<asp:CheckBox ID="chkresign" runat="server" Text="&nbsp;Remove Resign Letter" TabIndex="42" />--%>
                                        
                                    </div>


                                    <%--<div class="col-md-2">
                                        <asp:Label ID="Label35" class="control-label" runat="server" Text="AppointMent Letter"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                       <%-- <asp:LinkButton ID="AppointMent" runat="server" Width="100%" OnClick="AppointMent_Click"></asp:LinkButton>--%>
                                       <%-- <asp:HiddenField ID="AppointMentfield" runat="server" />--%>
                                    <%--    <br />
                                        <asp:FileUpload ID="appointFile" onchange="previewAppoint()" runat="server" accept=".pdf" TabIndex="43" />
                                        <br />
                                        <asp:CheckBox ID="chkapoint" runat="server" Text="&nbsp;Remove AppointMent Letter" TabIndex="44" />
                                    </div>--%>


                                </div>
                                <br />
                                <br />
                                <br />
                               <%-- <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label38" class="control-label" runat="server" Text="Resignation Date"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtresiDate" runat="server" class="form-control dates" AutoPostBack="false" TabIndex="47"></asp:TextBox>
                                    </div>


                                    <div class="col-md-2">
                                        <asp:Label ID="Label39" class="control-label" runat="server" Text="Notice Peroid"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtNotice" runat="server" class="form-control" AutoPostBack="false" TabIndex="48"></asp:TextBox>
                                    </div>


                                </div>--%>
                                <br />
                                <br />
                                <br /><br /><br />
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div id="val_msg_wrap_p" class="abc">
                                            <span style="color: red; margin-left: 15px">
                                                <asp:Literal ID="ltrper" runat="server"></asp:Literal>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <asp:Button ID="btnAddComailnt" runat="server" class="btn btn-primary" Text="Add Complaint" Style="width: 150px" OnClientClick=" return validateComlaintDetails()" OnClick="btnAddComailnt_Click"  TabIndex="51"/>
                                    </div>
                                </div>
                            </div>
                            <div class="row col-md-4">
                                <div class="col-md-12">
                                    <div class="col-md-5">
                                        <asp:Label ID="Label116" class="control-label" runat="server" Text="Nationality"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtnational" runat="server" class="form-control" AutoPostBack="false" TabIndex="22"></asp:TextBox>
                                    </div>
                                </div>
                                <br />

                                <br />
                                <br />
                                <div class="col-md-12">

                                    <div class="col-md-5">
                                        <asp:Label ID="Label25" class="control-label" runat="server" Text="City"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtcity" runat="server" class="form-control" AutoPostBack="false" TabIndex="25"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">

                                    <div class="col-md-5">
                                        <asp:Label ID="Label26" class="control-label" runat="server" Text="Complaint Status"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <select class="form-control select2" style="width: 100%" runat="server" id="ddlStatus" tabindex="28">
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />

                                <div class="col-md-12">

                                    <div class="col-md-5">
                                        <asp:Label ID="Label27" class="control-label" runat="server" Text="Completion Date"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtcomplaintDate" runat="server" class="form-control pull-right dates" AutoPostBack="false" TabIndex="31"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">

                                    <div class="col-md-5">
                                        <asp:Label ID="Label28" class="control-label" runat="server" Text="Height"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtheight" runat="server" class="form-control pull-right" AutoPostBack="false" onkeypress="return isNumberKey(event);" TabIndex="34"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">

                                    <div class="col-md-5">
                                        <asp:Label ID="Label29" class="control-label" runat="server" Text="Weight"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtweight" runat="server" class="form-control pull-right" AutoPostBack="false" onkeypress="return isNumberKey(event);" TabIndex="37"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />

                               <%-- <div class="col-md-12">

                                    <div class="col-md-5">
                                        <asp:Label ID="Label33" class="control-label" runat="server" Text="Aadhar Card No"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtahdar" runat="server" class="form-control pull-right" AutoPostBack="false" onkeypress="return isNumberKey(event);" TabIndex="40"></asp:TextBox>
                                    </div>
                                </div>--%>
                              <%--  <br />
                                <br />
                                <br /> <br />
                                <div class="col-md-12">

                                    <div class="col-md-5">
                                        <asp:Label ID="Label36" class="control-label" runat="server" Text="Offer Letter"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <%--<asp:LinkButton ID="Offer" runat="server" Width="100%" OnClick="Offer_Click"></asp:LinkButton>--%>
                                                                            <%--    <asp:HiddenField ID="offerField" runat="server" />
                                        <br />
                                        <asp:FileUpload ID="offerLetterFile" onchange="previewoffer()" runat="server" accept=".pdf" TabIndex="45"/>
                                        <br />
                                        <asp:CheckBox ID="chkoffer" runat="server" Text="&nbsp;Remove Offer Letter"  TabIndex="46"/>
                                    </div>
                                </div>--%>
                              <%--  <div class="col-md-12">

                                    <div class="col-md-5">
                                        <asp:Label ID="Label30" class="control-label" runat="server" Text="Pay Scale"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtpayscale" runat="server" class="form-control pull-right" AutoPostBack="false" onkeypress="return isNumberKey(event);" TabIndex="49"></asp:TextBox>
                                    </div>
                                </div>--%>
                              <%--  <br />
                                <br />
                                <br /><br /><br />--%>
                                <div class="col-md-12">

                                    <div class="col-md-5">
                                        <asp:Label ID="Label37" class="control-label" runat="server" Text="OTP"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtotp" runat="server" class="form-control pull-right" AutoPostBack="false" onkeypress="return isNumberKey(event);" TabIndex="50"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <%--Family--%>
                <div class="panel box box-default collapsed-box" id="panel_3">
					<div class="box-header with-border" data-id="3">
						<h3 class="box-title"><b>Enginerr Assign Details</b></h3>

						<div class="box-tools pull-right">
							<button data-id="3" type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="row col-md-8">
                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label40" class="control-label" runat="server" Text="Select Enginerr"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <select class="form-control select2" style="width: 100%" runat="server" id="ddlrelation" tabindex="52">
                                        </select>
                                    </div>

                                    <div class="col-md-2">
                                        <asp:Label ID="Label41" class="control-label" runat="server" Text="Enginerr Name"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txengname" runat="server" class="form-control" AutoPostBack="false" TabIndex="53"></asp:TextBox>
                                    </div>


                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label42" class="control-label" runat="server" Text="Enginerr Description"></asp:Label>
                                    </div>
                                    <div class="col-md-10">
                                        <asp:TextBox ID="txtengdesc" runat="server" Style="width:38%" class="form-control" AutoPostBack="false" TextMode="MultiLine" TabIndex="55"></asp:TextBox>
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
                                        <%--<asp:Button ID="btnSaveFamily" runat="server" class="btn btn-primary" Text="Assign Enginerr" Style="width: 150px" OnClientClick=" return validateFamilyDetails()" OnClick="btnSaveFamily_Click" TabIndex="57" />--%>
                                    </div>
                                </div>
                            </div>
                            <div class="row col-md-4">
                                <div class="col-md-12">

                                    <div class="col-md-5">
                                        <asp:Label ID="Label43" class="control-label" runat="server" Text="Assign Date"></asp:Label>
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

                        <%--<div class="box box-primary">
                            <div class="box-header">
                                <span style="font-size: large">Family Member List</span>
                                <br />
                                <br />
                                <div class="row  table-responsive">
                                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                                        <asp:GridView ID="grdfamily" CssClass="table table-bordered table-hover dataTable gridview" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowCancelingEdit="grdfamily_RowCancelingEdit" OnRowCommand="grdfamily_RowCommand" OnRowEditing="grdfamily_RowEditing" OnRowUpdating="grdfamily_RowUpdating" OnRowDataBound="grdfamily_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Family_Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmployee_Family_Id" runat="server" Visible="false" Text='<%# Eval("Employee_Family_Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Relation_ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRelation_ID" runat="server" Visible="false" Text='<%# Eval("Relation_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Relation Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRelation" runat="server" Text='<%# Eval("Relation_Name") %>'></asp:Label>
                                                        <%-- <asp:DropDownList ID="ddlrelati" runat="server"  >
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <%--  <asp:TextBox ID="txtrelative_name" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
                                                        <select class="form-control select2" style="width: 100%" Text='<%# Eval("Name") %>' runat="server" id="ddlrelation">  </select>
                                                        <asp:DropDownList ID="ddlrelations" runat="server">
                                                        </asp:DropDownList>

                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reletive Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrelativeName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtrelative_name" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reletive D.O.B.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrelativeDOB" runat="server" Text='<%# Eval("DOB") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtrelative_DOB" runat="server" Text='<%# Eval("DOB") %>' CssClass="dates"></asp:TextBox>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reletive Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrelativeNo" runat="server" Text='<%# Eval("ContactNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtrelative_No" runat="server" Text='<%# Eval("ContactNo") %>' onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reletive Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrelativedesc" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtrelative_Desc" runat="server" Text='<%# Eval("Description") %>' TextMode="MultiLine"></asp:TextBox>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Edit" ShowHeader="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%# Eval("Employee_Family_Id") %>' CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="btnupdate" runat="server" CommandName="Update" Text="Update" CommandArgument='<%# Eval("Employee_Family_Id")  %>'></asp:LinkButton>
                                                        <asp:LinkButton ID="btncancel" runat="server" CommandName="Cancel" Text="Cancel" CommandArgument='<%# Eval("Employee_Family_Id") %>'></asp:LinkButton>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkdelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this Details?');" CommandName="del" CommandArgument='<%#Eval("Employee_Family_Id")%>'>Delete</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </div>

                            </div>

                        </div>
        
                    </div>
                </div>

                <%--Qualification--%>
               <%--<div class="panel box box-default collapsed-box" id="panel_4">--%>
					<%--<div class="box-header with-border" data-id="4">
						<h3 class="box-title"><b>Employee Qualification Details</b></h3>

						<div class="box-tools pull-right">
							<button data-id="4" type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="row col-md-8">
                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label45" class="control-label" runat="server" Text="QualificationType"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <select class="form-control select2" style="width: 100%" runat="server" id="ddlqualification" tabindex="58">
                                        </select>
                                    </div>

                                    <div class="col-md-2">
                                        <asp:Label ID="Label46" class="control-label" runat="server" Text="Qualification"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtQualification" runat="server" class="form-control" AutoPostBack="false" TabIndex="59"></asp:TextBox>
                                    </div>


                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label47" class="control-label" runat="server" Text="University"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtuni" runat="server" class="form-control" AutoPostBack="false" TabIndex="61"></asp:TextBox>
                                    </div>

                                    <div class="col-md-2">
                                        <asp:Label ID="Label50" class="control-label" runat="server" Text="Passing (%)"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtper" runat="server" class="form-control" AutoPostBack="false" TabIndex="62"></asp:TextBox>
                                    </div>

                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label51" class="control-label" runat="server" Text="Description"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtQdesc" runat="server" Style="width: 100%" class="form-control" AutoPostBack="false" TextMode="MultiLine" TabIndex="64"></asp:TextBox>
                                    </div>

                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                        <asp:Button ID="btnsaveQualification" runat="server" class="btn btn-primary" Text="ADD" Style="width: 150px" OnClientClick=" return validateQualificationDetails()" OnClick="btnsaveQualification_Click"  TabIndex="65"/>
                                    </div>
                                </div>
                            </div>
                            <div class="row col-md-4">
                                <div class="col-md-12">

                                    <div class="col-md-5">
                                        <asp:Label ID="Label48" class="control-label" runat="server" Text="Main Subject"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtsub" runat="server" class="form-control" AutoPostBack="false" TabIndex="60"></asp:TextBox>
                                    </div>

                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">

                                    <div class="col-md-5">
                                        <asp:Label ID="Label49" class="control-label" runat="server" Text="PassOut Year"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtYear" runat="server" class="form-control" AutoPostBack="false" TabIndex="63"></asp:TextBox>
                                    </div>

                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div id="val_msg_wrap_Q" class="abc">
                                            <span style="color: red; margin-left: 15px">
                                                <asp:Literal ID="lttQ" runat="server"></asp:Literal>
                                            </span>
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>
                        <br />

                        <div class="box box-primary">
                            <div class="box-header">
                                <span style="font-size: large">Qualification Details</span>
                                <br />
                                <br />
                                <div class="row  table-responsive">
                                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                                        <asp:GridView ID="grdQualification" CssClass="table table-bordered table-hover dataTable gridview" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowCancelingEdit="grdQualification_RowCancelingEdit" OnRowCommand="grdQualification_RowCommand" OnRowDataBound="grdQualification_RowDataBound" OnRowEditing="grdQualification_RowEditing" OnRowUpdating="grdQualification_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Education_id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblemployee_education_id" runat="server" Visible="false" Text='<%# Eval("employee_education_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="type" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltype" runat="server" Visible="false" Text='<%# Eval("QualificationType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qualification Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQ_type" runat="server" Text='<%# Eval("QualificationType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlQualifications" runat="server">
                                                        </asp:DropDownList>

                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qualification Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQName" runat="server" Text='<%# Eval("Qualification") %>'></asp:Label><
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtQ_name" runat="server" Text='<%# Eval("Qualification") %>'></asp:TextBox>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MainSubject">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQ_Sub" runat="server" Text='<%# Eval("MainSubject") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtQ_sub" runat="server" Text='<%# Eval("MainSubject") %>'></asp:TextBox>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="University">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQ_uni" runat="server" Text='<%# Eval("University") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtQ_uni" runat="server" Text='<%# Eval("University") %>'></asp:TextBox>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Passing(%)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQ_per" runat="server" Text='<%# Eval("PassingPercentage") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtQ_per" runat="server" Text='<%# Eval("PassingPercentage") %>'></asp:TextBox>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PassOut Year">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQ_year" runat="server" Text='<%# Eval("PassOutYear") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtQ_year" runat="server" Text='<%# Eval("PassOutYear") %>'></asp:TextBox>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQ_desc" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtQ_Desc" runat="server" Text='<%# Eval("Description") %>' TextMode="MultiLine"></asp:TextBox>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Edit" ShowHeader="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnedit" runat="server" CommandArgument='<%# Eval("employee_education_id") %>' CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="btnupdate" runat="server" CommandName="Update" Text="Update" CommandArgument='<%# Eval("employee_education_id")  %>'></asp:LinkButton>
                                                        <asp:LinkButton ID="btncancel" runat="server" CommandName="Cancel" Text="Cancel" CommandArgument='<%# Eval("employee_education_id") %>'></asp:LinkButton>
                                                    </EditItemTemplate>

                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkdelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this Qualification Details?');" CommandName="del" CommandArgument='<%#Eval("employee_education_id")%>'>Delete</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </div>

                            </div>

                        </div>

                    </div>
                </div>--%>

                <%--Experience--%>
             <%-- <div class="panel box box-default collapsed-box" id="panel_5">
					<div class="box-header with-border" data-id="5">
						<h3 class="box-title"><b>Employee Experience Details</b></h3>

						<div class="box-tools pull-right">
							<button data-id="5" type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="row col-md-8">
                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label52" class="control-label" runat="server" Text="Organization Name"></asp:Label><label style="color: red">*</label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtorgName" runat="server" class="form-control" AutoPostBack="false" TabIndex="66"></asp:TextBox>
                                    </div>

                                    <div class="col-md-2">
                                        <asp:Label ID="Label53" class="control-label" runat="server" Text="From Date"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtfrom" runat="server" class="form-control dates" AutoPostBack="false" TabIndex="67"></asp:TextBox>
                                    </div>


                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label54" class="control-label" runat="server" Text="Description"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtE_desc" runat="server" Style="width: 70%" class="form-control" AutoPostBack="false" TextMode="MultiLine" TabIndex="69"></asp:TextBox>
                                    </div>

                                    <div class="col-md-2">
                                        <asp:Label ID="Label58" class="control-label" runat="server" Text="Notes"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:TextBox ID="txtnotes" runat="server" Style="width: 100%" class="form-control" AutoPostBack="false" TextMode="MultiLine" TabIndex="70"></asp:TextBox>
                                    </div>

                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div id="val_msg_wrap_E" class="abc">
                                            <span style="color: red; margin-left: 15px">
                                                <asp:Literal ID="lttE" runat="server"></asp:Literal>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-md-2"></div>
                                    <div class="col-md-4">
                                       <%-- <asp:Button ID="BtnExperienceDetails" runat="server" class="btn btn-primary" Text="ADD" Style="width: 150px" OnClientClick=" return validateExperiencenDetails()" OnClick="BtnExperienceDetails_Click" TabIndex="73" />--%>
                                    </div>
                                </div>
                            </div>
                            <div class="row col-md-4">
                                <div class="col-md-12">

                                   <%-- <div class="col-md-5">
                                        <asp:Label ID="Label55" class="control-label" runat="server" Text="To Date"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtTo" runat="server" class="form-control dates" AutoPostBack="false" TabIndex="68"></asp:TextBox>
                                    </div>--%>

                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">

                                   <%-- <div class="col-md-5">
                                        <asp:Label ID="Label56" class="control-label" runat="server" Text="Designation"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtE_desig" runat="server" class="form-control" AutoPostBack="false" TabIndex="71"></asp:TextBox>
                                    </div>--%>

                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="col-md-12">

                                   <%-- <div class="col-md-5">
                                        <asp:Label ID="Label57" class="control-label" runat="server" Text="Last Salary"></asp:Label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:TextBox ID="txtsla" runat="server" class="form-control" AutoPostBack="false" onkeypress="return isNumberKey(event);" TabIndex="72"></asp:TextBox>
                                    </div>--%>

                                </div>
                            </div>


                        </div>

                        <br />
                    
    </section>

    <script type="text/javascript">
        function validateComlaintDetails() {
            if (document.getElementById("<%=txtname.ClientID%>").value == "") {
                alert("CustomerName can not be blank");
                document.getElementById("<%=txtname.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtresiAdd.ClientID%>").value == "") {
                alert(" Address can not be blank");
                document.getElementById("<%=txtresiAdd.ClientID%>").focus();
                return false;
            }
            <%--if (document.getElementById("<%=txtname.ClientID%>").value == "") {
                alert("Residential City  can not be blank");
                document.getElementById("<%=txtname.ClientID%>").focus();
                return false;
            }--%>
            if (document.getElementById("<%=txtcompdate.ClientID%>").value == "") {
                alert("Comlaint  Date  can not be blank");
                document.getElementById("<%=txtcompdate.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtxontactno .ClientID%>").value == "") {
                alert("Residential Country  can not be blank");
                document.getElementById("<%=txtxontactno.ClientID%>").focus();
                return false;
            }
           if (document.getElementById("<%=txtdesc.ClientID%>").value == "") {
                alert("Description  can not be blank.");
                document.getElementById("<%=txtdesc.ClientID%>").focus();
                return false;
            }

            return true;
        }
    </script>

</asp:Content>
