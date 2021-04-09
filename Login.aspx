<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DogeBook.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="d-flex justify-content-center mt-5">
                <div class="card" style="width: 25rem;">
                    <div class="card-body text-center" >
                        <img src="Images/DogeBook-Logo.png" width="75" height="75" />
                        <h1 class="mb-4 mb-3 font-weight-normal">Sign In!</h1>
                        <div visible="false" id="ErrorDiv" runat="server" class="alert alert-danger">
                            <asp:Label runat="server" ID="Errors"></asp:Label>
                        </div>
                        <label for="Email">Email</label>
                        <asp:TextBox ID="TxtEmail_SignIn" runat="server" class="form-control" placeholder="You@example.com" autofocus=""></asp:TextBox>
                        <div class="invalid-feedback">Who are you?</div>
                        <label for="Password">Password</label>
                        <asp:TextBox ID="TxtPassword_SignIn" runat="server" class="form-control" TextMode="Password" placeholder="xxxxxxxxxxx"></asp:TextBox>
                        <div class="invalid-feedback">I know you're not Snowden, but cmon.</div>
                        <div class="checkbox mb-3">
                            <label>
                                <asp:CheckBox ID="RemeberChkBox" runat="server" type="checkbox" value="remember_me" />
                                Remember me
                            </label>
                        </div>
                        <asp:Button ID="Btn_SignIn" runat="server" Text="Sign In" Class="btn btn-md btn-primary btn-block" OnClick="Btn_SignIn_Click" />
                        <asp:Button ID="Btn_CreateAccount" runat="server" Text="Create Account" Class="btn btn-link" OnClick="Btn_CreateAccount_Click" />
                        <p class="mt-5 mb-3 text-muted">Stylianos Dimitriadis & Annica Chiu <br /> Copyright&copy; 2021</p>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
