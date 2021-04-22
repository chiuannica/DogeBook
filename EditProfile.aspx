<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditProfile.aspx.cs" Inherits="DogeBook.EditProfile" %>

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

    <title>Edit Profile</title>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Navbar runat="server" ID="Navbar" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUploadProfilePicture" />
            </Triggers>
            <ContentTemplate>
                <div class="container">
                    <h1 class="mb-4 mb-3 font-weight-normal text-center">
                        <asp:Label ID="LFirstName" runat="server" Text="First" class="h2 mr-1"></asp:Label>
                        <asp:Label ID="LLastName" runat="server" Text="Last" class="h2"></asp:Label>
                    </h1>
                </div>
        
                <div class="container  border my-3 px-3 py-3">

                    <div class="row col-md-4 col-sm-12">
                        <asp:Image ID="ImgProfilePic" runat="server" class="img-thumbnail rounded" />
                    </div>


                    <div class="row col-md-6 col-sm-12">
                        <div class="container my-2">
                            <div class="row">
                                <asp:FileUpload ID="fuProfilePic" type="file" CssClass="btn btn-light"  runat="server" />
                            </div>
                            <div class="row my-2">
                                <asp:Button ID="btnUploadProfilePicture" runat="server" CssClass="btn btn-light" OnClick="btnUploadProfilePicture_Click" Text="Upload Profile Picture" />
                            </div>
                            <div class="row">
                                <asp:Label ID="lblUploadStatus" CssClass="alert alert-primary" runat="server" Text="Label" Visible="False"></asp:Label>
                            </div>
                        </div>
                    </div>

                    
                    <div class="col-md-8 col-sm-12 my-3">
                        <h4>Biography</h4>
                        <p>
                            <asp:TextBox ID="TBBio" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </p>
                    </div>

                    <div class="col-md-8 col-sm-12 my-3">
                        <h4>Interests</h4>
                        <p>
                            <asp:TextBox ID="TBInterests" runat="server"  CssClass="form-control"  TextMode="MultiLine"></asp:TextBox>
                        </p>
                    </div>
            
                    <div class="col-md-8 col-sm-12 my-3 input-group ">
                        <div class="input-group-prepend">
                            <span class="input-group-text">City</span>
                        </div>
                        <asp:TextBox ID="TBCity"  runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>

                    <div class="col-md-8 col-sm-12 my-3 input-group ">
                        <div class="input-group-prepend">
                            <span class="input-group-text">State</span>
                        </div>
                        <asp:TextBox ID="TBState" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-8 col-sm-12 my-3">
                        <asp:Button ID="BtnUpdateProfile" runat="server" CssClass="btn btn-primary" Text="Update Information" OnClick="BtnUpdateProfile_Click" />
                    </div>
                    
                    <div class="col-md-8 col-sm-12 my-3">
                        <asp:Label ID="LUpdateProfile" CssClass="alert alert-primary" runat="server" Text="" Visible="False"></asp:Label>
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
