﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FriendCard.ascx.cs" Inherits="DogeBook.FriendCard" %>

<div class="card col-md-4 col-sm-6 mx-3" >

    <asp:Image ID="ImgFriend" class="card-img-top img-thumbnail rounded my-2" ImageUrl="https://news.bitcoin.com/wp-content/uploads/2021/01/cant-keep-a-good-dog-down-meme-token-dogecoin-spiked-over-500-this-year.jpg" runat="server" />
    <div class="card-body">
        <h5 class="card-title">
            <asp:Label ID="LFriendFName" CssClass="mr-1" runat="server" Text="First"></asp:Label>
            <asp:Label ID="LFriendLName" runat="server" Text="Last"></asp:Label>
        </h5>
        <div class="my-2">
            <asp:Button ID="BtnGoToProfile" class="btn btn-primary" runat="server" Text="Go to profile" OnClick="BtnGoToProfile_Click" />
        </div>
        <div class="my-2">
            <asp:Button ID="BtnDeleteFriend" class="btn btn-danger" runat="server" Text="Delete Friend" OnClick="BtnDeleteFriend_Click" />
        </div>
        <asp:Label ID="LblDisplay" runat="server" Visible="false"></asp:Label>

    </div>
</div>
