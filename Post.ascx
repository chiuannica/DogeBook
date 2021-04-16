<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Post.ascx.cs" Inherits="DogeBook.Post1" %>

<div class="card text-center text-white bg-dark mt-1 mb-1">
    <div class="card-header text-left ">
        <asp:Image ID="imgAuthor" height="50px" width="50px" ImageUrl="Images/DogeBook-Logo.png" runat="server" class="img-thumbnail"/> Author
    </div>
    <div class="card-body">
        <img src="Images/DogeBook-Logo.png" ID="imgPostImage" class="img-fluid" alt="..." runat="server">
        <p id="txtPostText" runat="server" class="card-text">Here lies the dogebook post text</p>
        <button id="btnLike" type="button" class="btn btn-success px-3"><i class="far fa-thumbs-up" aria-hidden="true"></i></button>
        <a href="#" class="btn btn-primary" id="btnComment">Comment</a>
    </div>
    <div id="txtTimestamp" runat="server" class="card-footer text-muted">
        2 days ago
    </div>
</div>

