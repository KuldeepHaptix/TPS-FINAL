<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Holiday_Setup.aspx.cs" Inherits="EmployeeManagement.Holiday_Setup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1 id="lable">Holiday Setup
        </h1>

    </section>
    <section class="content">
        <div class="box box-default">
            <div class="box-header with-border">
                <h3 class="box-title">Select Holiday Profile</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget=""><i class="fa fa-minus"></i></button>
                </div>
            </div>
            <div class="box-body">
                <div class="row">

                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="lblprofile" runat="server" Text="Select Holiday Profile"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlholiday" class="form-control select2" runat="server" Style="width: 100%;" TabIndex="2" onchange="return GetData(this);"></asp:DropDownList>
                        </div>
                        <input type="hidden" value="-1" id="holidayId" />
                        <input type="hidden" value="" id="holidaytext" />
                        <input type="hidden" runat="server" value="" id="userids" />
                        <input type="hidden" value="7" id="chkday" />
                        <div class="col-md-1">
                            <%--    <button type="submit" class="btn btn-primary" onclick="openPopup('Manage Holiday Profile');">Add</button>--%>
                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modal-default" onclick="return ongetValue()">
                                Add
                            </button>
                        </div>
                        <div class="col-md-1">
                            <button type="button" id="editbtn" class="btn btn-primary" data-toggle="modal" data-target="#modal-default" onclick="return ongetValue1()" style="display: none">
                                Edit
                            </button>
                        </div>
                        <div class="col-md-3">
                            <button type="button" id="setupApplied" class="btn btn-primary" style="display: none" onclick="return saveholidayDefaultSetup()">
                            </button>
                        </div>

                    </div>
                </div>
            </div>

        </div>


        <div class="row" id="calendars" style="display: none">

            <div class="col-md-12">
                <div class="col-md-8  animated fadeIn">
                    <div class="box box-default">
                        <div class="box-header with-border">

                            <div class="app_wrapper app_wrapper_bg">
                                <div class="app_container">

                                    <div class="widget clearfix calendar tpadding_20">
                                        <div id="calendar">
                                            <!--Dynamic Calender goes here-->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-4" id="Setholiday" style="display: none">
                    <div class="box box-default">
                        <div class="box-header with-border">
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:Label ID="Label1" runat="server" Text="Select Date"></asp:Label>
                                </div>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtdate" runat="server" class="form-control" AutoPostBack="false" ReadOnly="true"></asp:TextBox>
                                </div>
                                <br />
                                <br />
                                <br />
                            </div>
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:Label ID="Label2" runat="server" Text="Holiday Reason"></asp:Label>
                                </div>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtreason" runat="server" class="form-control" AutoPostBack="false" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                            </div>
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:Label ID="Label3" runat="server" Text="Holiday Type"></asp:Label>
                                </div>
                                <div class="col-md-7">
                                    <%-- <select class="form-control select2" style="width: 100%;" runat="server" id="ddltype">
                                    </select>--%>
                                    <asp:DropDownList ID="ddltype" class="form-control" runat="server" Style="width: 100%;"></asp:DropDownList>
                                </div>
                                <br />
                                <br />
                                <br />
                            </div>
                            <div class="row">
                                <div class="col-md-5">
                                    <asp:Label ID="Label4" runat="server" Text="Date Range"></asp:Label><br />
                                    <asp:Label ID="Label5" runat="server" Text="Note: Ex: 2-5" Style="font-weight:bolder"></asp:Label>
                                </div>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtrange" runat="server" class="form-control" AutoPostBack="false"></asp:TextBox>
                                </div>
                                <br />
                                <br />
                                <br />
                            </div>
                            <div class="row">
                                <div class="col-md-5">
                                    <%--<asp:Button ID="btnremove" runat="server" class="btn btn-primary" Text="Remove" OnClientClick="return ManageHoliday();" Width="110px" />--%>
                                    <button type="button" id="btnremove" class="btn btn-primary" onclick="return ManageHoliday(this);" style="display: none">
                                        Remove
                                    </button>
                                </div>
                                <div class="col-md-7">
                                    <%--<asp:Button ID="btnsave" runat="server" class="btn btn-primary" Text="Save" OnClientClick="return ManageHoliday();" Width="110px"  />--%>
                                    <button type="button" id="btnsave" class="btn btn-primary" onclick="return ManageHoliday(this);">
                                        Save
                                    </button>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                                        <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                                    </span>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.col -->
        </div>




        <%--Model popup--%>

        <div class="modal fade" id="modal-default" style="display: none;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span></button>
                        <h4 class="modal-title">Manage Holiday Profile</h4>
                    </div>
                    <div class="modal-body">
                        <div class="col-md-12">
                            <div class="col-md-4">
                                <asp:Label ID="lblId" class="control-label" runat="server" Text="Holiday Profile"></asp:Label>
                            </div>
                            <div class="col-md-5">
                                <asp:TextBox ID="txtholiday" runat="server" class="form-control" AutoPostBack="false" ></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <span style="color: red" id="val_msg_wrap1"></span>
                            </div>
                            <br />
                            <br />
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <asp:CheckBox ID="chkmon" class="control-label" runat="server" Text="&nbsp;&nbsp;Monday"></asp:CheckBox>
                            </div>
                            <div class="col-md-3">
                                <asp:CheckBox ID="chktue" class="control-label" runat="server" Text="&nbsp;&nbsp;Tuesday"></asp:CheckBox>
                            </div>
                            <div class="col-md-3">
                                <asp:CheckBox ID="chkwed" class="control-label" runat="server" Text="&nbsp;&nbsp;Wednesday"></asp:CheckBox>
                            </div>
                            <div class="col-md-3">
                                <asp:CheckBox ID="chkthu" class="control-label" runat="server" Text="&nbsp;&nbsp;Thursday"></asp:CheckBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <asp:CheckBox ID="chkfri" class="control-label" runat="server" Text="&nbsp;&nbsp;Friday"></asp:CheckBox>
                            </div>
                            <div class="col-md-3">
                                <asp:CheckBox ID="chksat" class="control-label" runat="server" Text="&nbsp;&nbsp;Saturday"></asp:CheckBox>
                            </div>
                            <div class="col-md-3">
                                <asp:CheckBox ID="chksun" class="control-label" runat="server" Text="&nbsp;&nbsp;Sunday"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary pull-left" data-dismiss="modal">Close</button>
                        <%--   <button type="button" class="btn btn-primary pull-left" onclick="closepopup();">Close</button>--%>
                        <button type="button" class="btn btn-primary" onclick="return GetsaveHoliday();">Save changes</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
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

        <%-- Selection change--%>
        <script type="text/javascript">
            function GetData(e) {
                var id = e.options[e.selectedIndex].value;

                //window.location.href = "http://localhost:17235/Holiday_Setup.aspx";
                document.getElementById("calendars").style.display = "block";
                document.getElementById("setupApplied").style.display = "block";
                var todaysYear = today.getFullYear();
                var todaysMonth = today.getUTCMonth() + 1;
                selectMonth = todaysMonth + "@" + todaysYear;
                if (id == -1) {
                    document.getElementById("calendars").style.display = "none";
                    document.getElementById("editbtn").style.display = "none";
                    document.getElementById("setupApplied").style.display = "none";
                }
                else {
                    document.getElementById("editbtn").style.display = "block";
                    onload = changedate('return');
                }
                document.getElementById("Setholiday").style.display = "none";
                $.ajax({
                    type: "POST",
                    url: "Holiday_Setup.aspx/getHolidayList",
                    data: '{id: "' + id + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessCategory,
                    failure: function (response) {
                        alert(response.d);
                    }
                });
                return false;
            }

            function OnSuccessCategory(response) {

                var responceresult = response.d.split("@@##");
                document.getElementById("val_msg_wrap1").innerText = "";
                if (responceresult.length == 2)
                {
                    document.getElementById("val_msg_wrap1").innerText = responceresult[1];
                }
                var responcedata = responceresult[0].split("@@@");
                if (responcedata.length == 2) {
                    var holiday = responcedata[1].split("###");
                    document.getElementById("ContentPlaceHolder1_txtdate").value = "";
                    document.getElementById("ContentPlaceHolder1_txtreason").value = "";
                    document.getElementById("ContentPlaceHolder1_txtrange").value = "";
                    var strappend = "";
                    var blank = '""';
                    for (var i = 0; i < holiday.length; i++) {
                        var holidaydetails = holiday[i].split("@");
                        if (i != holiday.length - 1) {
                            strappend += "" + blank + "@@VAL##" + holidaydetails[1] + "@@VAL##" + holidaydetails[0] + "@@VAL##" + holidaydetails[2] + "@@VAL##" + holidaydetails[3] + "@@" + holidaydetails[4] + "@@VAL##" + blank + "@@VAL##" + blank + "@@@@";
                        }
                        else {
                            strappend += "" + blank + "@@VAL##" + holidaydetails[1] + "@@VAL##" + holidaydetails[0] + "@@VAL##" + holidaydetails[2] + "@@VAL##" + holidaydetails[3] + "@@" + holidaydetails[4] + "@@VAL##" + blank + "@@VAL##" + blank + "";
                        }
                    }
                    events = strappend.split("@@@@");
                }
                if (responcedata[0] == "" || responcedata[0] == null) {
                    document.getElementById("chkday").value = "0";
                }
                else {
                    document.getElementById("chkday").value = responcedata[0];
                }

                var month_year = selectMonth.split("@");
                var todaysYear = month_year[1];
                var todaysMonth = month_year[0];
                changedate('gotomo', todaysMonth, todaysYear);
            }
            events = new Array(["", "", "", "", "", "", ""]);

            function OnAClick(e) {
                //alert(e);
                document.getElementById("Setholiday").style.display = "block";
                var text = e.href;
                var values = text.split("date=");
                var reason = values[1].split("&rea=")
 
                document.getElementById("ContentPlaceHolder1_txtdate").value = reason[0];
                if (reason.length == 2) {
                    var reasontext = reason[1].split("@@");
                    document.getElementById("ContentPlaceHolder1_txtreason").value = reasontext[0];

                    document.getElementById("ContentPlaceHolder1_ddltype").value = "1";
                    if (reasontext.length == 2) {

                        document.getElementById("ContentPlaceHolder1_ddltype").value = reasontext[1];
                    }
                    document.getElementById("ContentPlaceHolder1_txtrange").disabled = true;
                    document.getElementById("btnremove").style.display = "block";
                }
                else {
                    document.getElementById("ContentPlaceHolder1_txtreason").value = "";
                    //var skillsSelect = document.getElementById("ContentPlaceHolder1_ddltype");
                    //skillsSelect.value = "1";
                    document.getElementById("ContentPlaceHolder1_ddltype").value = "1";
                    document.getElementById("ContentPlaceHolder1_txtrange").disabled = false;
                    document.getElementById("btnremove").style.display = "none";
                }
 
            }


        </script>

        <%-- //POPUP--%>
        <script type="text/javascript">
            function GetsaveHoliday() {
                var id = document.getElementById("holidayId").value;
                var text = document.getElementById("ContentPlaceHolder1_txtholiday").value;

                document.getElementById("val_msg_wrap1").innerText = "";
                if (text == "" || text == null || text == "undefined") {
                    alert("Please Enter holiday profile name")
                    return;
                }
                document.getElementById("calendars").style.display = "none";
                document.getElementById("ContentPlaceHolder1_ddlholiday").innerHTML = "";
                var holiday_day = "";

                if (document.getElementById("ContentPlaceHolder1_chkmon").checked == true) {
                    holiday_day += "1,";
                }
                if (document.getElementById("ContentPlaceHolder1_chktue").checked == true) {
                    holiday_day += "2,";
                }
                if (document.getElementById("ContentPlaceHolder1_chkwed").checked == true) {
                    holiday_day += "3,";
                }
                if (document.getElementById("ContentPlaceHolder1_chkthu").checked == true) {
                    holiday_day += "4,";
                }
                if (document.getElementById("ContentPlaceHolder1_chkfri").checked == true) {
                    holiday_day += "5,";
                }
                if (document.getElementById("ContentPlaceHolder1_chksat").checked == true) {
                    holiday_day += "6,";
                }
                if (document.getElementById("ContentPlaceHolder1_chksun").checked == true) {
                    holiday_day += "7,";
                }

                var userid = document.getElementById("ContentPlaceHolder1_userids").value;
                $.ajax({
                    type: "POST",
                    url: "Holiday_Setup.aspx/saveHolidayProfile",
                    data: '{id: "' + id + '",Name: "' + text + '",userid:"' + userid + '" ,holiday_day:"' + holiday_day + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessSave,
                    failure: function (response) {
                        alert(response.d);
                    }
                });
                return false;
            }

            function OnSuccessSave(response) {

                var resp = response.d.split("@@@");

                if (resp.length == 2) {
                    document.getElementById("val_msg_wrap1").innerText = resp[1];
                }
                var employee = resp[0].split("###");
                var dropdown = document.getElementById("ContentPlaceHolder1_ddlholiday");

                for (var i = 0; i < employee.length; i++) {
                    var listoption = employee[i].split(',');
                    dropdown[dropdown.length] = new Option(listoption[1], listoption[0]);
                }
                document.getElementById("editbtn").style.display = "none";
                document.getElementById("setupApplied").style.display = "none";
            }


            function ongetValue() {


                var skillsSelect = document.getElementById("ContentPlaceHolder1_ddlholiday");
                var selectedText = skillsSelect.options[skillsSelect.selectedIndex].text;
                document.getElementById("holidayId").value = "-1";
                document.getElementById("holidaytext").value = "";
                //alert(selectedText);
                //alert(skillsSelect.options[skillsSelect.selectedIndex].value);
                document.getElementById("ContentPlaceHolder1_txtholiday").value = document.getElementById("holidaytext").value;
                if (skillsSelect.options[skillsSelect.selectedIndex].value == -1) {
                    document.getElementById("ContentPlaceHolder1_txtholiday").value = "";
                }
                document.getElementById("val_msg_wrap1").innerText = "";
                document.getElementById("ContentPlaceHolder1_chkmon").checked = false;
                document.getElementById("ContentPlaceHolder1_chktue").checked = false;
                document.getElementById("ContentPlaceHolder1_chkwed").checked = false;
                document.getElementById("ContentPlaceHolder1_chkthu").checked = false;
                document.getElementById("ContentPlaceHolder1_chkfri").checked = false;
                document.getElementById("ContentPlaceHolder1_chksat").checked = false;
                document.getElementById("ContentPlaceHolder1_chksun").checked = true;
            }

            function ongetValue1() {


                var skillsSelect = document.getElementById("ContentPlaceHolder1_ddlholiday");
                var selectedText = skillsSelect.options[skillsSelect.selectedIndex].text;
                document.getElementById("holidayId").value = skillsSelect.options[skillsSelect.selectedIndex].value;
                document.getElementById("holidaytext").value = selectedText
                //alert(selectedText);
                //alert(skillsSelect.options[skillsSelect.selectedIndex].value);
                document.getElementById("ContentPlaceHolder1_txtholiday").value = document.getElementById("holidaytext").value;
                if (skillsSelect.options[skillsSelect.selectedIndex].value == -1) {
                    document.getElementById("ContentPlaceHolder1_txtholiday").value = "";
                }
                document.getElementById("val_msg_wrap1").innerText = "";

                var holiday_ids = document.getElementById("chkday").value;
                document.getElementById("ContentPlaceHolder1_chksun").checked = false;
                document.getElementById("ContentPlaceHolder1_chkmon").checked = false;
                document.getElementById("ContentPlaceHolder1_chktue").checked = false;
                document.getElementById("ContentPlaceHolder1_chkwed").checked = false;
                document.getElementById("ContentPlaceHolder1_chkthu").checked = false;
                document.getElementById("ContentPlaceHolder1_chkfri").checked = false;
                document.getElementById("ContentPlaceHolder1_chksat").checked = false;
                if (holiday_ids == "" || holiday_ids == null)
                { }
                else {
                    var holidayArray = holiday_ids.split(",");

                    for (var i = 0; i < holidayArray.length; i++) {
                        if (holidayArray[i] == 1) {
                            document.getElementById("ContentPlaceHolder1_chkmon").checked = true;
                        }
                        if (holidayArray[i] == 2) {
                            document.getElementById("ContentPlaceHolder1_chktue").checked = true;
                        }
                        if (holidayArray[i] == 3) {
                            document.getElementById("ContentPlaceHolder1_chkwed").checked = true;
                        }
                        if (holidayArray[i] == 4) {
                            document.getElementById("ContentPlaceHolder1_chkthu").checked = true;
                        }
                        if (holidayArray[i] == 5) {
                            document.getElementById("ContentPlaceHolder1_chkfri").checked = true;
                        }
                        if (holidayArray[i] == 6) {
                            document.getElementById("ContentPlaceHolder1_chksat").checked = true;
                        }
                        if (holidayArray[i] == 7) {
                            document.getElementById("ContentPlaceHolder1_chksun").checked = true;
                        }
                    }
                }
            }


        </script>

        <%-- //save holiday setup--%>
        <script type="text/javascript">
            function saveholidayDefaultSetup() {
                var skillsSelect = document.getElementById("ContentPlaceHolder1_ddlholiday");
                var id = skillsSelect.options[skillsSelect.selectedIndex].value;
                var userid = document.getElementById("ContentPlaceHolder1_userids").value;
                $.ajax({
                    type: "POST",
                    url: "Holiday_Setup.aspx/saveDefaultSetup",
                    data: '{id: "' + id + '",month: "' + selectMonth + '",userid:"' + userid + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessCategory,
                    failure: function (response) {
                        alert(response.d);
                    }
                });
                return false;
            }


            function ManageHoliday(e) {
               
                var flag = "2";
                if (e.id == "btnremove")
                {
                    flag = "1";
                }
                var profile = document.getElementById("ContentPlaceHolder1_ddlholiday");
                var holidayType = document.getElementById("ContentPlaceHolder1_ddltype");
                var id = profile.options[profile.selectedIndex].value;
                var type = holidayType.options[holidayType.selectedIndex].value;
                var reason = document.getElementById("ContentPlaceHolder1_txtreason").value;
                var date = document.getElementById("ContentPlaceHolder1_txtdate").value;
                var range = document.getElementById("ContentPlaceHolder1_txtrange").value;
                if (flag == 2 && (reason == "" || reason == null || date == "" || date == null))
                {
                    alert("Holiday reason is mandatory");
                    return;
                }

                var userid = document.getElementById("ContentPlaceHolder1_userids").value;
                $.ajax({
                    type: "POST",
                    url: "Holiday_Setup.aspx/MangeHolidays",
                    data: '{id: "' + id + '",type: "' + type + '",date: "' + date + '",reason: "' + reason + '",range: "' + range + '",flag:"'+flag+'",userid:"' + userid + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccessCategory,
                    failure: function (response) {
                        alert(response.d);
                    }
                });
                return false;
            }

        </script>

        <%--calendar
        <script>
            events = new Array(["", "", "", "", "", "", ""]);
        </script>--%>
    </section>
</asp:Content>
