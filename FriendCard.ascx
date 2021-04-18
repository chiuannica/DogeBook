<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FriendCard.ascx.cs" Inherits="DogeBook.FriendCard" %>

<div class="card col-md-3 col-sm-6 mx-3" >

    <asp:Image ID="ImgFriend" class="card-img-top img-thumbnail rounded my-2" ImageUrl="https://news.bitcoin.com/wp-content/uploads/2021/01/cant-keep-a-good-dog-down-meme-token-dogecoin-spiked-over-500-this-year.jpg" runat="server" />
    <div class="card-body">
        <h5 class="card-title">
            <asp:Label ID="LFriendFName" runat="server" Text="First"></asp:Label>
            <asp:Label ID="LFriendLName" runat="server" Text="Last"></asp:Label>
        </h5>
        <asp:Button ID="BtnGoToProfile" class="btn btn-primary" runat="server" Text="Go to profile" OnClick="BtnGoToProfile_Click" />
    </div>
</div>
