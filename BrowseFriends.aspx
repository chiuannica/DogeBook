<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrowseFriends.aspx.cs" Inherits="DogeBook.BrowseFriends" %>

<%@ Register Src="~/Navbar.ascx" TagPrefix="uc1" TagName="Navbar" %>


<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <script src="https://kit.fontawesome.com/0a596c6382.js" crossorigin="anonymous"></script>

    <title>Search for New Friends</title>
</head>
<body>
    <uc1:Navbar runat="server" ID="Navbar" />
    
            
    <div class="container">
        <h1 class="mb-4 mb-3 font-weight-normal txt-center">Browse</h1>
                
                
    </div>

    <form id="form1" runat="server">
        <div>
            <div class="container">
                
                <div class="input-group my-2">
                    <asp:TextBox ID="TBSearch" type="text" class="form-control" placeholder="Search" runat="server"></asp:TextBox>
                    <asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="BtnSearch_Click" />

                </div>
            </div>

            <div class="container mb-5">
                <h2 class="mb-4 mb-3 font-weight-normal txt-center">
                    <asp:Label ID="LSearchTitle" runat="server"  Text=""></asp:Label>
                    <asp:Label ID="LSearchEmpty" runat="server"  class="badge badge-primary" Text=""></asp:Label>
                </h2>
                <asp:Panel ID="SearchPanel" CssClass="row col-12 my-3" runat="server"></asp:Panel>
            </div>


            <div class="container mb-5">
                <h2 class="mb-4 mb-3 font-weight-normal txt-center">Friends 
                    <asp:Label ID="LFriendsEmpty" runat="server"  class="badge badge-primary" Text="Label"></asp:Label>
                </h2>

                <div class="btn-group">
                    <asp:Button ID="BtnFriendsHide" runat="server" CssClass="btn btn-outline-danger" Text="Hide Friends" OnClick="BtnFriendsHide_Click" />
                    <asp:Button ID="BtnFriends" runat="server" CssClass="btn btn-outline-success" Visible="false" Text="View Friends" OnClick="BtnFriends_Click"/>
                </div>

                <asp:Panel ID="FriendsPanel" CssClass="row col-12 my-3" runat="server"></asp:Panel>
                
            </div>



            
            <div class="container mb-5">
                <h2 class="mb-4 mb-3 font-weight-normal txt-center">Friends of Friends 
                    <asp:Label ID="LFriendOfFriendsEmpty" Visible="false" class="badge badge-primary" runat="server" Text="Label"></asp:Label>
                </h2>

                <div class="btn-group">
                    <asp:Button ID="BtnFriendOfFriendsHide" runat="server" CssClass="btn btn-outline-danger" Text="Hide Friends Of Friends" OnClick="BtnFriendOfFriendsHide_Click" />
                    <asp:Button ID="BtnFriendOfFriends" runat="server" Visible="false" CssClass="btn btn-outline-success" Text="View Friends Of Friends" OnClick="BtnFriendOfFriends_Click" />
                </div>

                <asp:Panel ID="FriendsOfFriendsPanel" CssClass="row col-12 my-3" runat="server"></asp:Panel>
            </div>


            <div class="container my-2 mb-5">
                <h2 class="mb-4 mb-3 font-weight-normal txt-center">Others 
                    <asp:Label ID="LNonFriendsEmpty" Visible="false" class="badge badge-primary"  runat="server" Text="Label"></asp:Label>
                </h2>

                <div class="btn-group">
                    <asp:Button ID="BtnAllHide" runat="server" CssClass="btn btn-outline-danger" Text="Hide Others" OnClick="BtnAllHide_Click" />
                    <asp:Button ID="BtnAll" runat="server" Visible="false" CssClass="btn btn-outline-success" Text="View Others" OnClick="BtnAll_Click" />
                </div>

                <asp:Panel ID="NonFriendPanel" CssClass="row col-12 my-3" runat="server"></asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>

<style>

</style>