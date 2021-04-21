<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostControl.ascx.cs" Inherits="DogeBook.PostControl" %>

<div class="card text-white bg-dark mt-1 mb-1" id="cardDiv">
    <div class="card-header text-left" id="cardHeader">
        <asp:Image ID="imgAuthor" Height="50px" Width="50px" ImageUrl="Images/DogeBook-Logo.png" runat="server" class="img-thumbnail" />
        <asp:Label ID="lblAuthor" runat="server">Author</asp:Label>
    </div>
    <div class="card-body text-center" runat="server" id="cardBody">
        <asp:Image ImageUrl="Images/DogeBook-Logo.png" ID="imgPostImage" class="img-fluid" runat="server" />
        <br />
        <asp:Label ID="lblPostText" runat="server" class="card-text">Here lies the dogebook post text</asp:Label><br />
        <button id="btnLike" runat="server" class="btn btn-success" onclick="btnLike_Click"><i class="far fa-thumbs-up" runat="server" id="likeIcon"></i></button>

        <asp:Button class="btn btn-primary" data-bs-toggle="collapse" data-bs-target="#collapseCommentStandard" ID="btnComment" runat="server" OnClientClick="return false;" Text="Comment" />
        <div class="collapse" id="collapseCommentStandard">
            <div class="border-bottom mb-3">
                What would you like to comment?<span class="required">*</span>
            </div>
            <asp:TextBox CssClass="w-100 form-control" ID="TextBox1" Height="100" TextMode="MultiLine" runat="server"></asp:TextBox>
            <br />
            <div class="text-end">
                <button type="button" class="btn btn-success" runat="server">Create Comment</button>
            </div>
            <div class="accordion" id="accordionCommentStandards">
                <div class="accordion-item">
                    <h2 class="accordion-header" id="standardsCommentHeader">
                        <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#standardsCommentCollapse" aria-expanded="true" aria-controls="standardsCommentCollapse">
                            Comments
                        </button>
                    </h2>
                    <div id="standardsCommentCollapse" class="accordion-collapse collapse show" aria-labelledby="standardsCommentHeader" data-bs-parent="#accordionCommentStandards">
                        <div class="accordion-body comment-holder">
                            <div class="comments bg-white">
                                <div class="comments-header">
                                    <span class="red-text userName-Email">Tester Will</span>
                                </div>
                                <div class="comments-body">
                                    Aye, it does be looking good
                                </div>
                                <div class="comments-footer red text-light">
                                    4/12/2021
                                </div>
                            </div>
                            <div class="comments bg-white">
                                <div class="comments-header">
                                    <span class="red-text userName-Email">Nkem Ohanenye</span>
                                </div>
                                <div class="comments-body">
                                    I like what you are doing here 
                                </div>
                                <div class="comments-footer red text-light">
                                    4/12/2021
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div runat="server" class="card-footer text-muted text-center" id="cardFooter">
        <asp:Label runat="server" ID="lblTimestamp">Timestamp</asp:Label>
    </div>
</div>

