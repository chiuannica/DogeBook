<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Timeline.aspx.cs" Inherits="DogeBook.Timeline" %>

<%@ Register Src="~/PostControl.ascx" TagPrefix="uc1" TagName="PostControl" %>
<%@ Register Src="~/Navbar.ascx" TagPrefix="uc1" TagName="Navbar" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <title>Timeline</title>
    <style>
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Navbar runat="server" ID="Navbar" />
        <div class="row justify-content-center">
            <div class="col-7">
                <div class="input-group">
                    <asp:TextBox class="form-control rounded-pill" placeholder="Such" id="txtPostText" runat="server"></asp:TextBox>
                    <div class="input-group-append">
                        <asp:FileUpload ID="fuPost" class="btn form-control" runat="server" />
                        <asp:Label ID="lblUploadStatus" runat="server" Text="" Visible="False"></asp:Label>
                    </div>
                    <asp:Button runat="server" class="btn btn-outline-secondary rounded-pill m-auto" Text="Post" ID="btnPost" OnClick="btnPost_Click"></asp:Button>
                </div>
            </div>
        </div>

        <div class="row justify-content-center">
            <div class="col-4">
                <uc1:PostControl runat="server" ID="pc1" />
            </div>
        </div>
    </form>
</body>
</html>
