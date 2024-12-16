<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Untitled Page</title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Go" /><br />
			<br />
			Local Queue:<br />
			<asp:Literal ID="ltlLocal" runat="server"></asp:Literal><br />
			<br />
			Remote Queue:<br />
			<asp:Literal ID="ltlRemote" runat="server"></asp:Literal><br />
			<br />
			Feeds Queue:<br />
			<asp:Literal ID="ltlFeeds" runat="server"></asp:Literal></div>
	</form>
</body>
</html>
