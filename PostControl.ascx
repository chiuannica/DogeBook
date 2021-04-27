<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostControl.ascx.cs" Inherits="DogeBook.PostControl" %>

<div class="card mt-2 mb-4" id="cardDiv" runat="server">
    <div class="card-header text-left" id="cardHeader">
        <asp:Image ID="imgAuthor" Height="50px" Width="50px" ImageUrl="Images/DogeBook-Logo.png" runat="server" class="img-thumbnail" />
        <asp:Label ID="lblAuthor" runat="server">Author</asp:Label>
            <button id="btnEdit" class="btn btn-sm btn-secondary text-right" runat="server" onserverclick="btnEdit_ServerClick" visible="false">
                <i class="fas fa-wrench"></i>
            </button>
    </div>
    <div class="card-body" runat="server" id="cardBody">
        <div class="input-group mb-3">
            <asp:TextBox ID="txtPostText" runat="server" class="card-text form-control border-0 bg-white" ReadOnly="True" AutoPostBack="true">Here lies the dogebook post text</asp:TextBox>
            <div class="input-group-append">
                <button id="btnUpdatePostText" class="btn btn-sm btn-secondary" runat="server" onserverclick="btnUpdatePostText_ServerClick" visible="false">
                    <i class="fas fa-edit"></i>
                </button>
            </div>
        </div>

        <br />
        <br />
        <asp:Image ImageUrl="Images/DogeBook-Logo.png" ID="imgPostImage" class="img-fluid d-block mx-auto border-5" Style="max-height: 750px;" runat="server" />

        <div class="row justify-content-center">
            <asp:Label runat="server" ID="lblLikes"><i class="fas fa-paw"></i></asp:Label>
        </div>
        <div class="row pl-3 pr-3">
            <button id="btnLike" class="btn btn-success col mr-1" runat="server" onserverclick="btnLike_ServerClick">
                <i class="fas fa-paw"></i>&nbsp Like</button>
            <button class="btn btn-primary col" runat="server" id="btnComment" onserverclick="btnComment_Click">
                <i class="fas fa-comment-alt"></i>&nbsp Comment
            </button>
        </div>

        <br />

        <asp:HiddenField runat="server" ID="hdnPostId" />
        <asp:Panel ID="pnlCommentToggle" runat="server" Visible="false">
            <div class="input-group mb-3" id="commentTextBox" runat="server">
                <asp:TextBox runat="server" ID="txtComment" type="text" class="form-control" placeholder="Wow, very comment"></asp:TextBox>
                <div class="input-group-append">
                    <button class="btn btn-outline-primary" id="btnPostComment" runat="server" onserverclick="btnPostComment_ServerClick" type="button"><i class="fas fa-comment-alt"></i></button>
                </div>
            </div>
            <asp:Panel ID="commentSection" class="card card-body" runat="server">
            </asp:Panel>
        </asp:Panel>
        <div runat="server" class="card-footer bg-white text-center" id="cardFooter">
            <asp:Label runat="server" ID="lblTimestamp">Timestamp</asp:Label>
        </div>

    </div>
</div>
