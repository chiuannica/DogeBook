<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Timeline.aspx.cs" Inherits="DogeBook.Timeline" %>

<%@ Register Src="~/PostControl.ascx" TagPrefix="uc1" TagName="PostControl" %>
<%@ Register Src="~/Navbar.ascx" TagPrefix="uc1" TagName="Navbar" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-eOJMYsd53ii+scO/bJGFsiCZc+5NDVN2yr8+0RDqr0Ql0h+rP48ckxlpbzKgwra6" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/js/bootstrap.bundle.min.js" integrity="sha384-JEW9xMcG8R+pH31jmWH6WWP0WintQrMb4s7ZOdauHnUtxwoG2vI5DkLtS3qm9Ekf" crossorigin="anonymous"></script>
    <script src="https://kit.fontawesome.com/0a596c6382.js" crossorigin="anonymous"></script>
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
                        <asp:Label ID="lblUploadStatus" runat="server" Text="Label" Visible="False"></asp:Label>
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
