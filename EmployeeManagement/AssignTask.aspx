<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignTask.aspx.cs" Inherits="EmployeeManagement.AssignTask" MasterPageFile="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Assign Task
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>

    </section>
    <section class="content">
        <div class="box box-primary">
            <div class="box-header">

                <div class="col-md-12">
                    <div class="col-md-2">
                        <asp:Label ID="Label9" class="control-label" runat="server" Text="Engeenier"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <select class="form-control select2" runat="server" style="width: 100%" id="ddleng" tabindex="11">
                        </select>
                    </div>
                </div>
            </div>
        </div>

        <div class="box box-primary">
            <div class="box-header">

                <h3>Detail
                </h3>
            </div>
        </div>
    </section>
</asp:Content>
