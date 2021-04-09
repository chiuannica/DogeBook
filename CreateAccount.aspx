<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="DogeBook.CreateAccount" %>

<%@ Register Src="~/Navbar.ascx" TagPrefix="uc1" TagName="Navbar" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>

    <title>Create an Account</title>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Navbar runat="server" id="Navbar" />
        <div class="container col-6">
            <div class="form-group">
                <label >Email address</label>
                <asp:TextBox ID="TBEmail" type="email" class="form-control" placeholder="Email" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label >First Name</label>
                <asp:TextBox ID="TBFirstName" type="text" class="form-control" placeholder="First Name" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label >Last Name</label>
                <asp:TextBox ID="TBLastName" type="text" class="form-control" placeholder="Last Name" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Password</label>
                <asp:TextBox ID="TBPassword" type="password" class="form-control" placeholder="Password" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Confirm Password</label>
                <asp:TextBox ID="TBConfirmPassword" type="password" class="form-control" placeholder="Password" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Security Question 1</label>
                <div>
                    <asp:DropDownList ID="DDLSecurityQuestion1" class="dropdown btn dropdown-toggle" runat="server">
                        <asp:ListItem class="dropdown-item">What is your mother's maiden name?</asp:ListItem>
                        <asp:ListItem class="dropdown-item">Where did you go to elementary school?</asp:ListItem>
                        <asp:ListItem class="dropdown-item">Who was your favorite teacher?</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:TextBox ID="TBSecurityQuestion1" type="text" class="form-control" placeholder="Answer" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Security Question 2</label>
                <div>
                    <asp:DropDownList ID="DDLSecurityQuestion2" class="dropdown btn dropdown-toggle" runat="server">
                        <asp:ListItem class="dropdown-item">What street did you grow up on?</asp:ListItem>
                        <asp:ListItem class="dropdown-item">What was the name of your first pet?</asp:ListItem>
                        <asp:ListItem class="dropdown-item">Who was your first best friend?</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:TextBox ID="TBSecurityQuestion2" type="text" class="form-control" placeholder="Answer" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Security Question 3</label>
                <div>
                    <asp:DropDownList ID="DDLSecurityQuestion3" class="dropdown btn dropdown-toggle" runat="server">
                        <asp:ListItem class="dropdown-item">What was your grandmother's name?</asp:ListItem>
                        <asp:ListItem class="dropdown-item">Where did you go to elementary school?</asp:ListItem>
                        <asp:ListItem class="dropdown-item">Who was your favorite teacher?</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:TextBox ID="TBSecurityQuestion3" type="text" class="form-control" placeholder="Answer" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="BtnSubmit" class="btn btn-primary" runat="server" Text="Submit" OnClick="BtnSubmit_Click" />
            </div>
            <div class="containerlfluid my-3">
                <asp:Label ID="LblWarning" Visible="false" style="display:block" class="container alert alert-danger" runat="server" Text="Warning"></asp:Label>
            </div>
        </div>
        
    </form>
</body>
</html>
