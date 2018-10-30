<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="EmployeeManagement.Registration" %>

<!DOCTYPE html>
<html lang="en" >

<head>
  <meta charset="UTF-8">
  <title>Register</title>
  <meta name="viewport" content="width=device-width, initial-scale=1"><link href='https://fonts.googleapis.com/css?family=Roboto:400,700' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/normalize/5.0.0/normalize.min.css">

  
      <link rel="stylesheet" href="login/css/style.css">

  
</head>

<body>

  <div class="user">
    <header class="user__header">
        <img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/3219/logo.svg" alt="" />
        <h1 class="user__title">Register</h1>
    </header>
    
    <form class="form" runat="server" autocomplete="off">
        <div class="form__group">
            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="white" Class="form__input_error"></asp:Label>
        </div>
        <div class="form__group">
            <asp:textbox runat="server" ID="txtname" class="form__input" AutoCompleteType="Disabled" placeholder="Username #" ></asp:textbox>
        </div>

        <div class="form__group">
            <asp:TextBox ID="txtemail" runat="server" class="form__input" placeholder="Email" AutoCompleteType="Disabled"  ></asp:TextBox>
        </div>
        <div class="form__group">
        <asp:TextBox ID="txtcnno" onkeypress="return isNumberKey(event)"  MaxLength="10" runat="server" class="form__input" TextMode="Phone" placeholder="Contact #" ></asp:TextBox>
        </div>
        <div class="form__group">
            <asp:Label ID="Label6" class="btn_redio" runat="server" Text="Gender"></asp:Label>
            <asp:RadioButton ID="rdbmale" runat="server" class="btn_redio" Text="&nbsp;Male" Checked="true" GroupName="gender" />
            <asp:RadioButton ID="rdbFemale" runat="server" class="btn_redio" Text="&nbsp;Female" GroupName="gender" />
        </div>
        <div class="form__group">
            <asp:TextBox ID="txtpwd" runat="server" TextMode="Password" class="form__input" placeholder="Password"></asp:TextBox>
        </div>
        <div class="form__group">
            <asp:TextBox ID="txtconfpwd" runat="server" TextMode="Password" class="form__input" placeholder="Confirm Password" ></asp:TextBox>
        </div>
        <asp:Button ID="btnSave" class="btn" type="button"
                    runat="server" Text="Save"  OnClientClick="return validateRegistrationDetails()" OnClick="btnSave_Click" />
        
    </form>
</div>
  
  

    <script  src="login/js/index.js"></script>




</body>

<script type="text/javascript">
function validateEmail(emailField){
        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

        if (reg.test(emailField.value) == false) 
        {
            alert('Invalid Email Address');
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
<%--<script type="text/javascript">
    function validateRegistrationDetails() {
        if (document.getElementById("<%=txtname.ClientID%>").value == "") {
                alert("User Name can not be blank");
                document.getElementById("<%=txtname.ClientID%>").focus();
                return false;
        }
        if (document.getElementById("<%=txtemail.ClientID%>").value == "") {
            alert("Email can not be blank");
            document.getElementById("<%=txtemail.ClientID%>").focus();
            return false;
        }
            if (document.getElementById("<%=txtpwd.ClientID%>").value == "") {
                alert("Password  can not be blank");
                document.getElementById("<%=txtpwd.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtconfpwd.ClientID%>").value == "") {
                alert("Confirm Password can not be blank");
                document.getElementById("<%=txtconfpwd.ClientID%>").focus();
                return false;
            }
        var pass1 = document.getElementById("<%=txtpwd.ClientID %>").value;
        var pass2 = document.getElementById("<%=txtconfpwd.ClientID %>").value;
        if (pass1 != pass2) {
            document.getElementById("<%=lblmsg.ClientID %>").innerHTML = "Passwords Don't Match";
            return false;
        }
         
        else {
            document.getElementById("<%=lblmsg.ClientID %>").innerHTML = "";
            //empty string means no validation error
        }

            return true;
        }
    </script>--%>


</html>