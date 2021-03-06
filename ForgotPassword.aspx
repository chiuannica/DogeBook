<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="DogeBook.ForgotPassword" %>

<%@ Register Src="~/NavbarNotLoggedIn.ascx" TagPrefix="uc1" TagName="NavbarNotLoggedIn" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <script src="https://kit.fontawesome.com/0a596c6382.js" crossorigin="anonymous"></script>


    <title>Forgot Password</title>
</head>

<body>
    <form id="form1" runat="server">
        <uc1:NavbarNotLoggedIn runat="server" ID="NavbarNotLoggedIn" />

        <div class="container text-center">
            <h1 class="mb-4 mb-3 font-weight-normal">Forgot Password</h1>
         </div>
        
        <div class="container col-md-6 col-sm-12">
            <div class="form-group">
                <label >Email address</label>
                <asp:TextBox ID="TBEmail" type="email" class="form-control" placeholder="Email" runat="server"></asp:TextBox>
            </div>

            <div class="my-3">
                <asp:Button ID="BtnGetSecurityQuestions" class="btn btn-primary" runat="server" Text="Answer Security Questions" OnClick="BtnGetSecurityQuestions_Click"  />
            </div>

            <table class="table">
            <asp:Repeater  ID="RSecurityQuestions" runat="server">
                    <ItemTemplate>
                        <tr class="form-group">
                            <td>
                                <asp:Label ID="LQuestion" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "SecurityQuestion") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LAnswer" runat="server" Visible="false"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Answer") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TBAnswer" runat="server"></asp:TextBox>
                            </td>

                        <tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>

            <div class="my-3">
                <asp:Button ID="BtnSubmitAns" Visible="false" class="btn btn-primary" runat="server" Text="Submit Answers" OnClick="BtnSubmitAns_Click" />
            </div>

            <div class="form-group">
                <asp:Label ID="LPassword" Visible="false" runat="server" Text="Enter New Password"></asp:Label>
                <asp:TextBox ID="TBPassword" Visible="false" type="password" class="form-control" placeholder="Password" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="LConfirmPassword" Visible="false" runat="server" Text="Confirm New Password"></asp:Label>
                <asp:TextBox ID="TBConfirmPassword" Visible="false" type="password" class="form-control" placeholder="Password" runat="server"></asp:TextBox>
            </div>

            <div class="my-3">
                <asp:Button ID="BtnCreateNewPass" Visible="false" class="btn btn-primary" runat="server" Text="Create New Password" OnClick="BtnCreateNewPass_Click" />
            </div>



            <div class="container my-3">
                <asp:Label ID="LblWarning" style="display:block" Visible="false" class="alert alert-danger" runat="server" Text="Warning"></asp:Label>
            </div>

            <div class="container my-3">
                <asp:Label ID="LblSuccess" style="display:block" Visible="false" class="alert alert-success" runat="server" Text="Success"></asp:Label>
            </div>

        </div>
    </form>
</body>
</html>
