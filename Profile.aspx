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
    <script src="https://kit.fontawesome.com/0a596c6382.js" crossorigin="anonymous"></script>

    <title>Profile</title>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Navbar runat="server" ID="Navbar" />
        <div class="container my-3">
            <div class="row">
                <div class="mr-3">
                    <asp:Label ID="LFirstName" runat="server" Text="First" CssClass="display-2"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="LLastName" runat="server" Text="Last" CssClass="display-2"></asp:Label>
                </div>
            </div>
        </div>
        <div class="container  border my-3 px-3 py-3">

                <div class="row col-md-4 col-sm-12">
                    <asp:Image ID="ImgProfilePic" runat="server" class="img-thumbnail rounded" src="https://www.telegraph.co.uk/content/dam/technology/2021/01/28/Screenshot-2021-01-28-at-13-20-35_trans_NvBQzQNjv4BqEGKV9LrAqQtLUTT1Z0gJNRFI0o2dlzyIcL3Nvd0Rwgc.png" />
                </div>
                

                <div class="row col-md-8 col-sm-12 my-3">
                    <asp:Label ID="LBio" runat="server" Text="Biography Biography Biography Biography Biography 
                                                                Biography Biography Biography Biography Biography
                        Biography Biography Biography Biography Biography Biography Biography Biography Biography 
                        Biography Biography Biography Biography Biography Biography Biography Biography Biography">
                    </asp:Label>
                </div>
                <div class="row col-md-8 col-sm-12 my-3">
                    <asp:Label ID="LInterests" runat="server" Text="Interests Interests Interests Interests Interests 
                        Interests Interests Interests Interests Interests Interests ">
                    </asp:Label>
                </div>
                <div class="row col-md-8 col-sm-12 my-3">
                    <asp:Label ID="LCity" runat="server" Text="DogeVile"></asp:Label>
                    <span class="mr-3">,</span>
                    <asp:Label ID="LState" runat="server" Text="Pennsylvania"></asp:Label>
                </div>

        </div>

        <div class="container my-3">
            <div class="row">
                <p class="display-2">Friends</p>
            </div>
        </div>

        <div class="container border my-2 px-3 py-3">
            <div class="row">
                <div class="col-md-4 col-sm-6"><uc1:FriendCard runat="server" id="FriendCard1" /></div>
                <div class="col-md-4 col-sm-6"><uc1:FriendCard runat="server" id="FriendCard2" /></div>
                <div class="col-md-4 col-sm-6"><uc1:FriendCard runat="server" id="FriendCard3" /></div>
            </div>

        </div>
    </form>
</body>
</html>
