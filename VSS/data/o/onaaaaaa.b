<%@ Import Namespace="Etomic.ShareTransformer.Engine.Core" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TransformationList.ascx.cs" Inherits="Etomic.ShareTransformer.UI.WebControls.TransformationList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:Repeater ID="rptTransformations" Runat="server">
	<HeaderTemplate>
		<b>Saved Transformations</b><p />
	</HeaderTemplate>
	<ItemTemplate>
		<a href="Default.aspx?id=<%#((Transformation)Container.DataItem).Id%>">
			<%#((Transformation)Container.DataItem).Title%>
		</a>
		<br />
	</ItemTemplate>
</asp:Repeater>
