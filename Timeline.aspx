<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Timeline.aspx.cs" Inherits="DogeBook.Timeline" %>

<%@ Register Src="~/Navbar.ascx" TagPrefix="uc1" TagName="Navbar" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <script src="https://kit.fontawesome.com/0a596c6382.js" crossorigin="anonymous"></script>
    <title>Timeline</title>
    <style>
        body {
            /*background: url("Images/dogeTimeline.jpg") no-repeat center center fixed;
            background-size: cover;*/
            background-color: #ffffff;
        }

        input[type=file]::-webkit-file-upload-button {
            color: #ffffff;
            border: none;
            padding-top: 5px;
            background-image: linear-gradient(to right, #051937, #004d7a, #008793, #00bf72, #a8eb12);
            border-radius: 4px;
            height: 35px;
            width: 200px
        }
    </style>



</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1"
            runat="server" />
        <uc1:Navbar runat="server" ID="Navbar" />
        <div class="row justify-content-center mt-2">
            <div class="col-8">
                <div class="input-group">
                    <asp:TextBox class="form-control border-0 " placeholder="Tell Doge what happened" ID="txtPostText" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <div class="input-group-append">
                        <asp:FileUpload ID="fuPost" class="btn form-control border-0 " runat="server" />
                        <asp:Label ID="lblUploadStatus" runat="server" Text="" Visible="False"></asp:Label>
                    </div>
                </div>
            </div>

        </div>
        <div class="row justify-content-center mt-2">
            <div class="col-12 text-center">
                <asp:Button runat="server" class="btn btn-outline-primary col-8" Text="Post" ID="btnPost" OnClick="btnPost_Click"></asp:Button>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row justify-content-center">
                    <div class="col-5 ml-5 mt-5" runat="server" id="timeline"></div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>



    </form>
    <script type="text/javascript">

        $(document).ready(function () {
            var i;
            for (i = 0; i < ($('input:hidden').length); i++) {
                if ($('input:hidden').eq(i).attr('id').includes("hdnPostId")) {
                    //console.log($('input:hidden').eq(i).parent().next().children().attr('id'));
                    //console.log($('input:hidden').eq(i).next().attr('id'));
                    const userAction = async () => {
                        const response = await fetch('https://localhost:44305/api/Timeline/GetComments/' + $('input:hidden').eq(i).val());
                        const myJson = await response.json(); //extract JSON from the http response
                        //const obj = JSON.parse(myJson);
                        console.log(JSON.stringify(myJson, null, 4));
                        //$('input:hidden').eq(i).next().append(obj.Text);

                    }
                    userAction();
                }
            }

            //var x;
            //for (x = 0; x < postIds.length; x++) {
            //    var xhttp = new XMLHttpRequest();
            //    xhttp.onreadystatechange = function () {
            //        if (this.readyState == 4 && this.status == 200) {
            //            alert(this.responseText);
            //        }
            //    };
            //    xhttp.open("GET", "https://localhost:44305/api/Timeline/GetComments/" + postIds[x], true);
            //    xhttp.setRequestHeader("Content-type", "application/json");
            //}


        });


    </script>
</body>
</html>
