<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Final2.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<!--add link to .css file -->
<link href="Default.css" rel="stylesheet" type="text/css" />
    <title>Log in page</title>
</head>
<body>
	
    <form id="form1" runat="server">
        <div>
			<h1>Enter the username and password:</h1>
			<asp:Label ID="lblUsername" runat="server" Text="Username:"></asp:Label>
		<asp:TextBox ID="TextBox1" runat="server" Height="23px" Width="220px" BackColor="#99CCFF"></asp:TextBox>
		<br />
		<asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
		&nbsp;<asp:TextBox ID="TextBox2" runat="server" Width="220px" Height="23px" BackColor="#99CCFF"></asp:TextBox>
    	<p>

			<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" Width="74px" Height="30px" BackColor="#66FFFF" ForeColor="#000099" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Button ID="Button2" runat="server" Text="Reset" Width="74px" Height="30px" BackColor="#66FFFF" ForeColor="#000099" />
		</p>
		<asp:Label ID="lblError" runat="server" Visible="False"></asp:Label>
        </div>
		
    </form>
</body>
</html>
