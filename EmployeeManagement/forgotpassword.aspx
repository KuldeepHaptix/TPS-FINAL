<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/forgotpassword.aspx.cs" Inherits="EmployeeManagement.forgotpassword" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Forgot Password | Forgot Password</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="../../bootstrap/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="../../dist/css/AdminLTE.min.css">
    <!-- iCheck -->
    <link rel="stylesheet" href="../../plugins/iCheck/square/blue.css">
</head>
<body class="hold-transition login-page" style="padding-top: 3%">
    <div class="login-box">

        <div class="login-logo">
            <%--<b>Reseller</b> Management--%>
 
        </div>
        <div class="login-box-body">
            <p class="login-box-msg">Enter Your Mail Id</p>

            <form runat="server">
                <div class="form-group has-feedback">
                    <input type="text" class="form-control" placeholder="Email Id" id="tb_UserName" runat="server">
                    <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                </div>
                <div class="row">
                    <!-- /.col -->
                    <div class="col-xs-12">
                        <asp:Button ID="Button1" OnClick="Button1_Click" CssClass="btn btn-primary btnLogin btnlgn"
                            runat="server" Text="Submit" Style="position: center" OnClientClick=" return validate()" />
                    </div>
                    <!-- /.col -->
                </div>
                <div class="row">
                    <div class="col-xs-12">
                        <asp:Label ID="lblmsg" ForeColor="Red" runat="server" Font-Size="14px" Visible="false"
                            Text=""></asp:Label>
                        <div id="val_msg_wrapl" class="ehide" style="display: none">
                            <div class="alert alert-danger" id="val_msg">
                            </div>
                        </div>
                    </div>
                </div>
            </form>

            <!-- /.social-auth-links -->


        </div>
        <!-- /.login-box-body -->
    </div>
    <!-- /.login-box -->

    <!-- jQuery 2.2.3 -->
    <script src="../../plugins/jQuery/jquery-2.2.3.min.js"></script>
    <!-- Bootstrap 3.3.6 -->
    <script src="../../bootstrap/js/bootstrap.min.js"></script>
    <!-- iCheck -->


    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=tb_UserName.ClientID %>").value == "") {
                alert("Email can not be blank");
                document.getElementById("<%=tb_UserName.ClientID%>").focus();
                return false;
            }

            else {
                var x = document.getElementById("<%=tb_UserName.ClientID %>").value;
                var atpos = x.indexOf("@");
                var dotpos = x.lastIndexOf(".");
                if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
                    alert("Your email address seems incorrect. Please try again.");
                    document.getElementById("<%=tb_UserName.ClientID %>").focus();
                    return false;
                }
            }
        }
    </script>
</body>
</html>
