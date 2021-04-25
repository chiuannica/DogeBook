<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostControl.ascx.cs" Inherits="DogeBook.PostControl" %>

<div class="card mt-2 mb-4" id="cardDiv" runat="server">
    <div class="card-header text-left" id="cardHeader">
        <asp:Image ID="imgAuthor" Height="50px" Width="50px" ImageUrl="Images/DogeBook-Logo.png" runat="server" class="img-thumbnail" />
        <asp:Label ID="lblAuthor" runat="server">Author</asp:Label>
        <asp:HiddenField runat="server" ID="hdnPostId" />
    </div>
    <div class="card-body" runat="server" id="cardBody">
        <asp:TextBox ID="txtPostText" runat="server" class="card-text form-control border-0 disabled bg-white" ReadOnly="True">Here lies the dogebook post text</asp:TextBox>
        <br />
        <br />
        <asp:Image ImageUrl="Images/DogeBook-Logo.png" ID="imgPostImage" class="img-fluid d-block mx-auto border-5" Style="max-height: 750px;" runat="server" />
        <br />
        <div class="row pl-3 pr-3">
           <%-- <asp:Button ID="btnLike" runat="server" class="btn btn-success col mr-1" OnClick="btnLike_Click" Text="Like"></asp:Button>--%>
            <button runat="server" id="btnLike" class="btn btn-success col mr-1"  OnClick="btnLike_Click" title="Like">
                <i class="far fa-thumbs-up"></i></i>Like
            </button>
            <asp:Button class="btn btn-primary col" ID="btnComment" runat="server" Text="Comment" OnClick="btnComment_Click1" />
        </div>
        <asp:TextBox runat="server" class="form-control" Visible="false"></asp:TextBox>
        <asp:Table runat="server" ID="commentTable" Visible="false">
            <asp:TableHeaderRow>
                <asp:TableCell ID="cmtAuthor">Author</asp:TableCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell ID="comment">Comment</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <div runat="server" class="card-footer bg-white text-center" id="cardFooter">
            <asp:Label runat="server" ID="lblTimestamp">Timestamp</asp:Label>
        </div>

    </div>
</div>
