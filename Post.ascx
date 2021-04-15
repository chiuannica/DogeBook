<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Post.ascx.cs" Inherits="DogeBook.Post1" %>
<asp:Image ID="imgPoster" runat="server" /><asp:Label ID="lblPoster" runat="server" Text="Name of Poster "></asp:Label>
<br />
<asp:Image ID="imgPostImage" runat="server" Visible="False" ImageAlign="Middle" />
<br />
<asp:Label ID="lblPostText" runat="server" Text="Here lies the text of the post"></asp:Label>
<asp:ImageButton ID="ImageButton1" runat="server" />
