<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Post.ascx.cs" Inherits="DogeBook.Post1" %>
<div>
    <asp:ImageButton ID="imgBtnPostImage" src="Images/DogeBook-Logo.png" runat="server" />
    <br />
    <asp:Label ID="lblPostText" runat="server" Text="The Doge gods wanna know what's up..." BackColor="#D0C196" ForeColor="White"></asp:Label><asp:Button ID="btnLike" runat="server" BackColor="#D0C196" BorderStyle="None" />
    <br />
    <asp:Label ID="lblPostTimestamp" runat="server" Text="6:22 PM 4/15/2021" BackColor="#D0C196" ForeColor="White"></asp:Label>

    <br />
    <br />

</div>

