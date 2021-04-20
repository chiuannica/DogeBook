<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Timeline.aspx.cs" Inherits="DogeBook.Timeline" %>

<%@ Register Src="~/PostControl.ascx" TagPrefix="uc1" TagName="PostControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-eOJMYsd53ii+scO/bJGFsiCZc+5NDVN2yr8+0RDqr0Ql0h+rP48ckxlpbzKgwra6" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0-beta3/dist/js/bootstrap.bundle.min.js" integrity="sha384-JEW9xMcG8R+pH31jmWH6WWP0WintQrMb4s7ZOdauHnUtxwoG2vI5DkLtS3qm9Ekf" crossorigin="anonymous"></script>
    <script src="https://kit.fontawesome.com/0a596c6382.js" crossorigin="anonymous"></script>
    <title>Timeline</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="input-group">
            <input class="form-control" type="text" placeholder="Such " aria-label="default input example" />
            <div class="input-group-btn">
                <input class="form-control" type="file" id="formFile" />
            </div>
        </div>

        <div class="row justify-content-center">
            <div class="col-4">
                <uc1:PostControl runat="server" ID="pc1" />
            </div>
        </div>

    </form>
</body>
</html>
