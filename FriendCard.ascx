<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FriendCard.ascx.cs" Inherits="DogeBook.FriendCard" %>

<div class="card" >
    <img class="card-img-top" src="https://news.bitcoin.com/wp-content/uploads/2021/01/cant-keep-a-good-dog-down-meme-token-dogecoin-spiked-over-500-this-year.jpg" alt="Card image cap">
    <div class="card-body">
        <h5 class="card-title">
            <asp:Label ID="LFriendFName" runat="server" Text="First"></asp:Label>
            <asp:Label ID="LFriendLName" runat="server" Text="Last"></asp:Label>
        </h5>
        <a href="#" class="btn btn-primary">Go to profile</a>
    </div>
</div>