<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherInfo.aspx.cs" Inherits="Final2.TeacherInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<!--add link to .css file -->
<link href="Teacher.css" rel="stylesheet" type="text/css" />
    <title>Teachers Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
			<h1>Welcome Teachers!
			</h1>
					<asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Class_ID" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">				<Columns>
					<asp:CommandField ShowSelectButton="True" />
					<asp:BoundField DataField="Class_ID" HeaderText="Class_ID" ReadOnly="True" SortExpression="Class_ID" />
					<asp:BoundField DataField="Course_ID" HeaderText="Course_ID" SortExpression="Course_ID" />
					<asp:BoundField DataField="Course_Name" HeaderText="Course_Name" SortExpression="Course_Name" />
					<asp:BoundField DataField="Teacher_ID" HeaderText="Teacher_ID" SortExpression="Teacher_ID" />
					<asp:BoundField DataField="Teacher_FullName" HeaderText="Teacher_FullName" ReadOnly="True" SortExpression="Teacher_FullName" />
					<asp:BoundField DataField="Section" HeaderText="Section" ReadOnly="True" SortExpression="Section" />
				</Columns>
</asp:GridView>
			<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"></asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
