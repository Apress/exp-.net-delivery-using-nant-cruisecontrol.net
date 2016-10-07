<%@ Control Language="c#" AutoEventWireup="false" Codebehind="TransformerControl.ascx.cs" Inherits="Etomic.ShareTransformer.UI.WebControls.TransformerControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="100%" cellpadding="2" cellspacing="2">
	<tr>
		<td><asp:Label ID="lblMessages" Runat="server" ForeColor="Red" /></td>
	</tr>
	<tr>
		<td><b>Title:</b><asp:RequiredFieldValidator ID="vldTitle" Runat="server" ErrorMessage="*" ControlToValidate="txtTitle" /></td>
	</tr>
	<tr>
		<td><asp:TextBox ID="txtTitle" Runat="server" Width="100%" MaxLength="150" /></td>
	</tr>
	<tr>
		<td><b>XML:</b><asp:RequiredFieldValidator ID="vldXml" Runat="server" ErrorMessage="*" ControlToValidate="txtXml" /></td>
	</tr>
	<tr>
		<td><asp:TextBox ID="txtXml" Runat="server" TextMode="MultiLine" Width="100%" Rows="6" /></td>
	</tr>
	<tr>
		<td><b>XSLT:</b><asp:RequiredFieldValidator ID="vldXslt" Runat="server" ErrorMessage="*" ControlToValidate="txtXslt" /></td>
	</tr>
	<tr>
		<td><asp:TextBox ID="txtXslt" Runat="server" TextMode="MultiLine" Width="100%" Rows="6" /></td>
	</tr>
	<tr>
		<td><b>Output:</b><asp:RequiredFieldValidator ID="vldOutput" Runat="server" ErrorMessage="*" ControlToValidate="txtOutput" /></td>
	</tr>
	<tr>
		<td><asp:TextBox ID="txtOutput" Runat="server" TextMode="MultiLine" Width="100%" Rows="6" Enabled="False" /></td>
	</tr>
	<tr>
		<td><hr>
		</td>
	</tr>
	<tr>
		<td>
			<asp:Button ID="btnTransform" Runat="server" Text="Transform" OnClick="btnTransform_Click" CausesValidation="False" />&nbsp;
			<asp:Button ID="btnSaveOutput" Runat="server" Text="Save Transformation" OnClick="btnSaveOutput_Click" CausesValidation="True" />
		</td>
	</tr>
</table>
