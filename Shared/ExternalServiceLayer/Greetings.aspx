<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Greetings.aspx.cs" Inherits="ExternalServiceLayer.Greetings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">  
        <div>  
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>  
            <asp:Button ID="Button1" runat="server" Text="Greet" />  
            <br />  
            <br />  
            <asp:Label ID="Label1" runat="server" BackColor="Red"></asp:Label>  
        </div>  
    </form>  
</body>
</html>
