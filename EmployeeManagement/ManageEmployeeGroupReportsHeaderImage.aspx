<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ManageEmployeeGroupReportsHeaderImage.aspx.cs" Inherits="EmployeeManagement.ManageEmployeeGroupReportsHeaderImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Manage Employee Report Group Header Image
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="../EmployeeGroupForReports.aspx"><i></i>Employee Report's Group List</a></li>
            <li class="active">Manage Employee Report Group Header Image</li>
        </ol>
    </section>

    <section class="content">

        <div class="box box-primary">
            <div class="box-header">
                <span style="font-size: large">Manage Report Header Image</span>

                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="Label13" class="control-label" runat="server" Text="A4 Landscape: "></asp:Label>
                                    <asp:Label ID="Label4" class="control-label" runat="server" Text="Dimension: 732*94" Style="color: red"></asp:Label>
                                </div>
                                <div class="col-md-10">
                                    <asp:Image ID="IMgA4L" runat="server" Style="height: 125px; width: 95%;" ImageUrl="~/HeaderImages.jpg" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-3">
                                    <asp:FileUpload ID="fileA4L" onchange="previewFile()" runat="server" accept=".jpg,.jpeg" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnsaveA4L" runat="server" class="btn btn-primary" Text="Save" Style="width: 150px" OnClick="btnsaveA4L_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnremoveA4L" runat="server" class="btn btn-primary" Text="Remove" Style="width: 150px" OnClick="btnremoveA4L_Click" />
                                </div>
                                <div class="col-md-3" style="width: auto"></div>
                                <span style="color: red; margin-left: 15px" id="val_msg_wrap1" class="abc">
                                    <asp:Literal ID="ltrA4L" runat="server"></asp:Literal>
                                </span>
                                <br />
                            </div>
                            <br />


                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="Label1" class="control-label" runat="server" Text="Legel Portrait: "></asp:Label>
                                    <asp:Label ID="Label3" class="control-label" runat="server" Text="Dimension:  575*94" Style="color: red"></asp:Label>
                                </div>
                                <div class="col-md-10">
                                    <asp:Image ID="imglegelP" runat="server" Style="height: 125px; width: 95%;" ImageUrl="~/HeaderImages.jpg" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-3">
                                    <asp:FileUpload ID="fileLegelP" onchange="previewFile1()" runat="server" accept=".jpg,.jpeg" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnSaveLegelP" runat="server" class="btn btn-primary" Text="Save" Style="width: 150px" OnClick="btnSaveLegelP_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnRemoveLegelP" runat="server" class="btn btn-primary" Text="Remove" Style="width: 150px" OnClick="btnRemoveLegelP_Click" />
                                </div>
                                <div class="col-md-3" style="width: auto"></div>
                                <span style="color: red; margin-left: 15px" id="val_msg_wrap2" class="abc">
                                    <asp:Literal ID="ltrLP" runat="server"></asp:Literal>
                                </span>
                                <br />
                            </div>
                            <br />


                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="Label2" class="control-label" runat="server" Text="Legel Landscape: "></asp:Label>
                                    <asp:Label ID="Label5" class="control-label" runat="server" Text="Dimension:  950*94" Style="color: red"></asp:Label>
                                </div>
                                <div class="col-md-10">
                                    <asp:Image ID="ImgLegelL" runat="server" Style="height: 125px; width: 95%;" ImageUrl="~/HeaderImages.jpg" />
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-3">
                                    <asp:FileUpload ID="fileLegelL" onchange="previewFile2()" runat="server" accept=".jpg,.jpeg" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnsaveLegelL" runat="server" class="btn btn-primary" Text="Save" Style="width: 150px" OnClick="btnsaveLegelL_Click" />
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnsaveRemoveL" runat="server" class="btn btn-primary" Text="Remove" Style="width: 150px" OnClick="btnsaveRemoveL_Click" />
                                </div>
                                <div class="col-md-3" style="width: auto"></div>
                                <span style="color: red; margin-left: 15px" id="val_msg_wrap3" class="abc">
                                    <asp:Literal ID="ltrLL" runat="server"></asp:Literal>
                                </span>
                                <br />

                            </div>

                        <asp:HiddenField ID="report_grp" runat="server" Value="0" />
                    </div>

                </div>
            </div>
        </div>
        </div>

        <script>
            function previewFile() {
                var preview = document.querySelector('#<%=IMgA4L.ClientID %>');
                var file = document.querySelector('#<%=fileA4L.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }


                var b = document.getElementById('<%=fileA4L.ClientID %>');
                if (file) {
                    reader.readAsDataURL(file);

                } else {
                    preview.ImageUrl = "~/photo.jpg";
                    reader.readAsDataURL(preview);
                }
            }

            function previewFile1() {
                var preview = document.querySelector('#<%=imglegelP.ClientID %>');
                var file = document.querySelector('#<%=fileLegelP.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }


                var b = document.getElementById('<%=fileLegelP.ClientID %>');
                if (file) {
                    reader.readAsDataURL(file);

                } else {
                    preview.ImageUrl = "~/photo.jpg";
                    reader.readAsDataURL(preview);
                }
            }

            function previewFile2() {
                var preview = document.querySelector('#<%=ImgLegelL.ClientID %>');
                var file = document.querySelector('#<%=fileLegelL.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }


                var b = document.getElementById('<%=fileLegelL.ClientID %>');
                if (file) {
                    reader.readAsDataURL(file);

                } else {
                    preview.ImageUrl = "~/photo.jpg";
                    reader.readAsDataURL(preview);
                }
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
    </section>
</asp:Content>
