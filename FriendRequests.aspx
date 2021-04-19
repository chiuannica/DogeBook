<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FriendRequests.aspx.cs" Inherits="DogeBook.FriendRequests" %>

<%@ Register Src="~/Navbar.ascx" TagPrefix="uc1" TagName="Navbar" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <script src="https://kit.fontawesome.com/0a596c6382.js" crossorigin="anonymous"></script>

    <title>Friend Requests</title>
</head>
<body>
    <uc1:Navbar runat="server" ID="Navbar" />

    <form id="form1" runat="server">
        
        <div class="container text-center">
            <h1 class="mb-4 mb-3 font-weight-normal">Friend Requests</h1>
        </div>
        

        <div class="container">

            
            <table class="table">
            <asp:Repeater  ID="RFriendRequests" runat="server"  OnItemCommand="RFriendRequests_ItemCommand">
                    <ItemTemplate>
                        <tr class="form-group">
                            <td>
                                <asp:Label ID="LFirstName" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "FirstName") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LLastName" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "LastName") %>'></asp:Label>
                            </td>
                            
                            <td>
                                <asp:Label ID="LUserId" runat="server" Visible="false" 
                                        Text='<%# DataBinder.Eval(Container.DataItem, "UserId") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Button ID="BtnAccept" runat="server" CommandName="Accept" CssClass="btn btn-success" Text="Accept" />

                                <asp:Button ID="BtnDeny" runat="server" CommandName="Deny" CssClass="btn btn-danger" Text="Deny" />

                            </td>

                        <tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="container">
                <asp:Label ID="LMessage" Visible="false" runat="server" Text="Label"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
