<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewlyAddedArticles.ascx.cs" Inherits="AllTheTalk.Web.Controls.NewlyAddedArticles" %>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
	<Columns>
		<asp:TemplateField HeaderText="Recently Added Articles">
			<AlternatingItemTemplate>
				<asp:HyperLink ID="hl0" runat="server">[hl0]</asp:HyperLink><br />
				<asp:Label ID="lbl0" runat="server"></asp:Label>
			</AlternatingItemTemplate>
			<ItemTemplate>
				<asp:HyperLink ID="hl0" runat="server">[hl0]</asp:HyperLink><br />
				<asp:Label ID="lbl0" runat="server"></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
	</Columns>
</asp:GridView>
