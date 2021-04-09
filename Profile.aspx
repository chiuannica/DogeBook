<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="DogeBook.Profile" %>

<%@ Register Src="~/Navbar.ascx" TagPrefix="uc1" TagName="Navbar" %>
<%@ Register Src="~/FriendCard.ascx" TagPrefix="uc1" TagName="FriendCard" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>


    <title>Profile</title>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Navbar runat="server" ID="Navbar" />
        <div class="container">
            <div class="row">
                <asp:Label ID="LFirstName" runat="server" Text="First" CssClass="h2"></asp:Label>
                <span> </span>
                <asp:Label ID="LLastName" runat="server" Text="Last" CssClass="h2"></asp:Label>
            </div>
            <div class="row my-2">
                <asp:Label ID="LBio" runat="server" Text="Biography Biography Biography Biography Biography 
                                                            Biography Biography Biography Biography Biography
                    Biography Biography Biography Biography Biography Biography Biography Biography Biography 
                    Biography Biography Biography Biography Biography Biography Biography Biography Biography">
                </asp:Label>
            </div>
            <div class="row my-2">
                <asp:Label ID="LInterests" runat="server" Text="Interests Interests Interests Interests Interests 
                    Interests Interests Interests Interests Interests Interests ">
                </asp:Label>
            </div>
            <div class="row my-2">
                <asp:Label ID="LCity" runat="server" Text="DogeVile"></asp:Label>
                <span>, </span>
                <asp:Label ID="LState" runat="server" Text="Pennsylvania"></asp:Label>
            </div>
        </div>
        <div class="container my-2">
            <div class="row">
                <div class="col-4"><uc1:FriendCard runat="server" id="FriendCard1" /></div>
                <div class="col-4"><uc1:FriendCard runat="server" id="FriendCard2" /></div>
                <div class="col-4"><uc1:FriendCard runat="server" id="FriendCard3" /></div>
            </div>
            
        </div>
    </form>
</body>
</html>
