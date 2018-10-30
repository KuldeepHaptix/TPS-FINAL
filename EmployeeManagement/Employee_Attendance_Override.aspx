<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Employee_Attendance_Override.aspx.cs" Inherits="EmployeeManagement.Employee_Attendance_Override" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>EmployeeWise Attendance OverWrite
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
                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="lblId" class="control-label" runat="server" Text="Employee Name"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <select class=" form-control select2 " style="width: 100%;" runat="server" id="ddlemplist">
                            </select>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-2">
                            <asp:Label ID="Label1" class="control-label" runat="server" Text="Date range "></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <div class="input-group">
                                  
                                <button type="button"  class="btn btn-default pull-right" id="daterange-btn">
                                     <asp:Label runat="server" ID="lblDate" Text="Date range picker"></asp:Label>
                                       <asp:HiddenField runat="server" ID="hdnDate" />
                                  <%--  <span >
                                        <i class="fa fa-calendar"></i>Date range picker
                    </span>--%>
                                    <i class="fa fa-caret-down"></i>
                                </button>
                            </div>
                        </div>

                    </div>
                    <br />
                    <br />
                    <br />
                    <div class="col-md-12">
                        <div class="col-md-2">
                        </div>

                        <div class="col-md-2">

                            <asp:Button ID="btnseacrh" runat="server" class="btn btn-primary" Text="Search" Width="100px" OnClick="btnseacrh_Click" OnClientClick="return saveHiddenField();" />

                        </div>

                        <div class="col-md-1">
                        </div>

                        <div class="col-md-4">
                            <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                                <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                            </span>
                        </div>

                    </div>
                </div>
            </div>
        </div>

            <div class="box box-primary">
            <div class="box-header">
                <span style="font-size: large">Employee  Attendance</span>
                <br />
                <br />
                <div class="row  table-responsive">
                    <div style="width: 95%; margin-left: 2%; margin-right: 20%;">
                        <asp:GridView ID="grd" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AttType" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltype" runat="server" Visible="false" Text='<%# Eval("type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Previous Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprestatus" runat="server" Text='<%# Eval("Prev_Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="attnewType" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblnewType" runat="server" Text='<%# Eval("Update_Type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="New Status">
                                    <ItemTemplate>
                                        <select class="form-control select2" style="width: 100%" runat="server" id="ddlstatus"></select>
                                        <%-- <asp:DropDownList ID="ddltime" runat="server">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                      
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>


                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">

                        <div class="col-md-3"></div>
                        <div style="margin-top: 20px; margin-bottom: 10px; margin-bottom: 10px" class="col-md-3">
                            <asp:Button ID="btnsave" CssClass="btn btn-primary"
                                runat="server" Text="Save" style="width:150px" OnClick="btnsave_Click"/>
                        </div>
                            </div>
                    </div>
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
                 $('#ContentPlaceHolder1_grd').DataTable({
                     "fixedHeader": true,
                     "paging": false,
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
