<%@ Register Src="~/Navbar.ascx" TagPrefix="uc1" TagName="Navbar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <script src="https://kit.fontawesome.com/0a596c6382.js" crossorigin="anonymous"></script>

    <title>Search for New Friends</title>
</head>
<body>
    <uc1:Navbar runat="server" ID="Navbar" />
    <form id="form1" runat="server">
        <div>
            <div class="container">
                <div class="form-group">
                    
                    <label >Search</label>
                    <asp:TextBox ID="TBSearch" type="text" class="form-control" placeholder="Search" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="container text-center">
                <h1 class="mb-4 mb-3 font-weight-normal">Browse</h1>
                <asp:Repeater ID="Repeater1" runat="server"></asp:Repeater>
                <asp:Panel ID="Panel1" runat="server"></asp:Panel>
            </div>
        </div>
    </form>
</body>
</html>
