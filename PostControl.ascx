<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostControl.ascx.cs" Inherits="DogeBook.PostControl" %>

<div class="card text-center text-white bg-dark mt-1 mb-1">
    <div class="card-header text-left">
        <asp:Image ID="imgAuthor" Height="50px" Width="50px" ImageUrl="Images/DogeBook-Logo.png" runat="server" class="img-thumbnail" />
        <asp:Label ID="lblAuthor" runat="server">Author</asp:Label>
    </div>
    <div class="card-body" runat="server" id="postBody">
        <asp:Image ImageUrl="Images/DogeBook-Logo.png" id="imgPostImage" class="img-fluid" runat="server"/> <br />
        <asp:Label id="lblPostText" runat="server" class="card-text">Here lies the dogebook post text</asp:Label><br />
        <asp:LinkButton id="btnLike" runat="server" CssClass="btn btn-primary form-control" Width="50px" OnClick="btnLike_Click"><i class="far fa-thumbs-up" aria-hidden="true"></i></asp:LinkButton>
        <asp:Button class="btn btn-primary" ID="btnComment" runat="server" Text="Comment" OnClick="btnComment_Click" />
    </div>
    <div runat="server" class="card-footer text-muted">
        <asp:Label runat="server" ID="lblTimestamp">Timestamp</asp:Label>
    </div>
</div>

