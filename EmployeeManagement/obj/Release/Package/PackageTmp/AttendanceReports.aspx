<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AttendanceReports.aspx.cs" Inherits="EmployeeManagement.AttendanceReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <h1 id="tit" runat="server">Attendance Reports
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>
    </section>

    <section class="content">
        <div class="box box-default">
            <div class="box-header with-border">
                <h3 class="box-title">Search Criteria</h3>

                <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                </div>
            </div>

            <div class="box-body">
                <div class="row">
                    <div class="col-md-12" style="margin: 10px;">

                        <div class="col-md-2">
                            <asp:Label ID="lblprofile" runat="server" Text="Report Title"></asp:Label>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" class="form-control pull-right" ID="txttitle" TabIndex="1" />
                        </div>

                        <div class="col-md-2">
                            <asp:Label ID="lblgrp" class="control-label" runat="server" Text="Employee Group"></asp:Label>
                        </div>
                        <div class="col-md-3">

                            <%-- <select class="form-control select2" style="width: 100%;" runat="server" id="ddlgroup" tabindex="2">
                            </select>--%>
                            <asp:DropDownList ID="ddlgroup" class="form-control select2" runat="server" Style="width: 100%;" TabIndex="2" onchange="return getProduct(this);"></asp:DropDownList>
                        </div>

                    </div>

                    <div class="col-md-12" style="display: none; margin: 10px;" id="div0" runat="server">
                        <div class="col-md-2" id="div1" runat="server">
                            <asp:Label ID="lbl1" runat="server" Text="Date range"></asp:Label>
                        </div>
                        <div class="col-md-3" id="div2" runat="server">
                            <%--<asp:TextBox runat="server" class="form-control pull-right dates" ID="fromdate" TabIndex="3" />--%>
                            <div class="input-group">

                                <button type="button" class="btn btn-default pull-right" id="daterange-btn" tabindex="4">
                                    <asp:Label runat="server" ID="lblDate" Text="Date range picker"></asp:Label>
                                    <asp:HiddenField runat="server" ID="hdnDate" />
                                    <%--  <span >
                                        <i class="fa fa-calendar"></i>Date range picker
                    </span>--%>
                                    <i class="fa fa-caret-down"></i>
                                </button>
                            </div>

                        </div>
                        <div class="col-md-1" id="div3" runat="server"></div>
                        <div class="col-md-2" id="div4" runat="server">
                            <asp:Label ID="lbl2" class="control-label" runat="server" Text="Select Date"></asp:Label>
                        </div>
                        <div class="col-md-3" id="div5" runat="server">
                            <asp:TextBox runat="server" class="form-control pull-right dates" ID="todate" TabIndex="4" />

                        </div>

                    </div>


                    <div class="col-md-12" style="display: none; margin: 10px;" id="div10" runat="server">

                        <div class="col-md-2" id="div11" runat="server">
                            <asp:Label ID="Label3" runat="server" Text="Select Month"></asp:Label>
                        </div>
                        <div class="col-md-2" id="div12" runat="server">
                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddlmonth" tabindex="5">
                            </select>
                        </div>
                        <div class="col-md-2" id="div13" runat="server">
                            <select class="form-control select2" style="width: 100%;" runat="server" id="ddlyear" tabindex="6">
                            </select>
                        </div>

                        <div class="col-md-2" id="div14" runat="server">
                            <asp:Label ID="Label4" class="control-label" runat="server" Text="Employee Name"></asp:Label>
                        </div>
                        <div class="col-md-3" id="div15" runat="server">
                            <asp:DropDownList ID="ddlemp" class="form-control select2" runat="server" Style="width: 100%;" TabIndex="7"></asp:DropDownList>
                        </div>

                    </div>

                    <div class="col-md-12" style="display: none; margin: 10px;" id="div20" runat="server">
                        <div class="col-md-2" id="div21" runat="server">
                            <asp:CheckBox ID="chlpre" class="control-label" runat="server" Text="&nbsp;&nbsp;Present Employee" TabIndex="8"></asp:CheckBox>
                        </div>
                        <div class="col-md-3" id="div22" runat="server">
                            <asp:CheckBox ID="chlab" class="control-label" runat="server" Text="&nbsp;&nbsp;Absent Employee" TabIndex="9"></asp:CheckBox>
                        </div>
                        <div class="col-md-1" id="div23" runat="server">
                        </div>
                        <div class="col-md-4" id="div24" runat="server">
                            <asp:CheckBox ID="chksal" class="control-label" runat="server" Text="&nbsp;&nbsp;Display Actual Salary" TabIndex="10"></asp:CheckBox>
                        </div>


                    </div>

                    <div class="col-md-12" style="margin: 10px;margin-bottom:0px">

                        <div class="col-md-2"></div>
                        <div class="col-md-3">
                            <asp:Button ID="BtnSearch" runat="server" class="btn btn-primary" Text="Search" OnClick="BtnSearch_Click" OnClientClick="return saveHiddenField();" TabIndex="11" Width="110px" />
                        </div>
                        <div class="col-md-4" style="width: auto"></div>
                        <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                            <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                        </span>
                    </div>
                    <asp:HiddenField ID="reportIndex" runat="server" Value="2" />
                </div>
            </div>

        </div>

         <div class="box box-primary">
            <div class="box-header">
                <span style="font-size: large">Employee Reports Data</span>
                <br />
                <br />
                <div class="row  table-responsive">
                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="true" OnRowDataBound="grd_RowDataBound">
                            <Columns>

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

        <script>
            $(function () {
                $('.dates').datepicker({
                    autoclose: true
                });
            });
        </script>

        <style>
            .date {
            }
        </style>

        <script type="text/javascript">
            function getProduct(e) {
                var id = e.options[e.selectedIndex].value;

                document.getElementById("ContentPlaceHolder1_ddlemp").innerHTML = "";
                var rpt_index = document.getElementById("ContentPlaceHolder1_reportIndex").value;
                if (rpt_index != 9 && rpt_index != 11 && rpt_index != 13 ) {
                    return;
                }
                $.ajax({
                    type: "Post",
                    url: "AttendanceReports.aspx/getEmpList",
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
                var employee = response.d.split("###");
                var dropdown = document.getElementById("ContentPlaceHolder1_ddlemp");

                for (var i = 0; i < employee.length; i++) {
                    var listoption = employee[i].split(',');
                    dropdown[dropdown.length] = new Option(listoption[1], listoption[0]);
                }
            }
        </script>

        <script>
            $(function () {
                $('#daterange-btn').daterangepicker(
                    {
                        ranges: {
                            'Today': [moment(), moment()],
                            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                            'This Month': [moment().startOf('month'), moment().endOf('month')],
                            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                        },
                        startDate: moment().subtract(29, 'days'),
                        endDate: moment()
                    },
                    function (start, end) {
                        //$('#daterange-btn span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
                        $('#daterange-btn span').html(start.format('DD/MM/YYYY') + '-' + end.format('DD/MM/YYYY'));
                    }
                );
            });

            function saveHiddenField() {
                document.getElementById("ContentPlaceHolder1_hdnDate").value = document.getElementById("ContentPlaceHolder1_lblDate").innerHTML;
                return true;
            }
        </script>

        <script type="text/javascript">
            $(document).ready(function () {
                var rpt_index = document.getElementById("ContentPlaceHolder1_reportIndex").value;
                if (rpt_index == 9) {
                    $('#ContentPlaceHolder1_grd').DataTable({
                        "fixedHeader": true,
                        "paging": true,
                        "lengthChange": true,
                        "searching": true,
                        "ordering": false,
                        "info": true,
                        "autoWidth": false
                    });
                }
                else {
                    $('#ContentPlaceHolder1_grd').DataTable({
                        "fixedHeader": true,
                        "paging": true,
                        "lengthChange": true,
                        "searching": true,
                        "ordering": true,
                        "info": true,
                        "autoWidth": false
                    });
                }
            });


        </script>

    </section>

</asp:Content>
