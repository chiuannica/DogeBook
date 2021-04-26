<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherProfile.aspx.cs" Inherits="DogeBook.OtherProfile" %>

<%@ Register Src="~/Navbar.ascx" TagPrefix="uc1" TagName="Navbar" %>


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

        <div class="container">
            <h1 class="mb-4 mb-3 font-weight-normal text-center">
                <asp:Label ID="LFirstName" runat="server" Text="First" class="h2 mr-1"></asp:Label>
                <asp:Label ID="LLastName" runat="server" Text="Last" class="h2"></asp:Label>
            </h1>
        </div>
        

        <div class="container col-md-8 col-sm-12 border my-3 px-3 py-3">

                <div class="row col-md-4 col-sm-12">
                    <asp:Image ID="ImgProfilePic" runat="server" class="img-thumbnail rounded" />
                </div>


                <div class="col-md-8 col-sm-12 my-3">
                    <h4>Biography <i class="fas fa-dog"></i></h4>
                    <p>

                        <asp:Label ID="LBio" runat="server" Text="Biography Biography Biography Biography Biography 
                                                                    Biography Biography Biography Biography Biography
                            Biography Biography Biography Biography Biography Biography Biography Biography Biography 
                            Biography Biography Biography Biography Biography Biography Biography Biography Biography">
                        </asp:Label>
                    </p>

                </div>
                <div class="col-md-8 col-sm-12 my-3">
                    <h4>Interests <i class="fas fa-baseball-ball"></i></h4>
                    <p>
                        <asp:Label ID="LInterests" runat="server" Text="Interests Interests Interests Interests Interests 
                            Interests Interests Interests Interests Interests Interests ">
                        </asp:Label>
                    </p>
                
                </div>
                <div class="col-md-8 col-sm-12 my-3">
                    <h4>Location <i class="fas fa-search-location"></i></h4>
                    <p>
                        <asp:Label ID="LCity" runat="server" Text="DogeVile"></asp:Label>
                        <span class="mr-1"></span>
                        <asp:Label ID="LState" runat="server" Text="Pennsylvania"></asp:Label>
                    </p>
                </div>
            </div>
            


            <div class="container">
                <h2 class="mb-4 mb-3 font-weight-normal  text-center">Friends
                    <asp:Label ID="LFriendsNumber" runat="server"  class="badge badge-primary" Text=""></asp:Label>
                </h2>
                    <asp:Panel ID="FriendPanel" CssClass="row " runat="server"></asp:Panel>
            </div>
            


            <div class="container">
                
                <div class="input-group my-2">
                    <asp:TextBox ID="TBSearch" type="text" class="form-control" placeholder="Search Friends" runat="server"></asp:TextBox>
                    <asp:Button ID="BtnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="BtnSearch_Click" />
                </div>
            </div>

            <div class="container mb-5">
                <p class="mb-4 mb-3 font-weight-normal txt-center">
                    <asp:Label ID="LSearchTitle" runat="server"  Text=""></asp:Label>
                    <asp:Label ID="LSearchEmpty" runat="server"  class="badge badge-primary" Text=""></asp:Label>
                </p>
                <asp:Panel ID="SearchPanel" CssClass="row col-12 my-3" runat="server"></asp:Panel>
            </div>

    </form>
</body>
</html>
