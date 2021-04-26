<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NonFriendCard.ascx.cs" Inherits="DogeBook.NonFriendCard" %>
<div class="col-md-4 col-sm-6" >
    <div class="card" >
        <asp:Image ID="ImgFriend" class="card-img-top" ImageUrl="https://news.bitcoin.com/wp-content/uploads/2021/01/cant-keep-a-good-dog-down-meme-token-dogecoin-spiked-over-500-this-year.jpg" runat="server" Height="200px" ImageAlign="Middle" />
        <div class="card-body">

            <h5 class="card-title">
                <asp:Label ID="LFriendFName" CssClass="mr-1" runat="server" Text="First"></asp:Label>
                <asp:Label ID="LFriendLName" runat="server" Text="Last"></asp:Label>
            </h5>
            
            


            <div class="btn-group-vertical my-2">
                <asp:Button ID="BtnGoToProfile" class="btn btn-primary" runat="server" Text="Go to profile" OnClick="BtnGoToProfile_Click"  />
                <asp:Button ID="BtnAddFriend" class="btn btn-success" runat="server" Text="Add Friend" OnClick="BtnAddFriend_Click" />
            </div>

            
        </div>
        <div class="card-footer">
            <div class="card-text">
                <asp:Label ID="LblDisplay" CssClass="badge badge-secondary" runat="server" Visible="false"></asp:Label>
            </div>
            <div class="card-text">
                <asp:Label ID="LDescription" CssClass=""  runat="server"  Text=""></asp:Label>
            </div>
        </div>

    </div>
</div>