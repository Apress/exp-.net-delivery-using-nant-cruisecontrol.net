<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="false" Inherits="Etomic.Transformer.Web.UI.DefaultCodeBehind" ValidateRequest="False" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>Transformer</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <link rel="stylesheet" href="CSS/style.css" type="text/css" />
  </head>
  <body MS_POSITIONING="GridLayout">
	<form id="Form1" method="post" runat="server">
		<table width="100%" cellpadding="2" cellspacing="2">
			<tr>
				<td><asp:Label ID="lblMessages" Runat="server" ForeColor="Red" /></td>
			</tr>
			<tr>
				<td><b>XML:</b></td>
			</tr>
			<tr>
				<td><asp:TextBox ID="txtXml" Runat="server" TextMode="MultiLine" Width="100%" Rows="6" /></td>
			</tr>
			<tr>
				<td><b>XSLT:</b></td>
			</tr>
			<tr>
				<td><asp:TextBox ID="txtXslt" Runat="server" TextMode="MultiLine" Width="100%" Rows="6" /></td>
			</tr>
			<tr>
				<td><asp:Button ID="btnTransform" Runat="server" Text="Transform" OnClick="btnTransform_Click" /></td>
			</tr>
			<tr>
				<td><hr></td>
			</tr>			
			<tr>
				<td><b>Output:</b></td>
			</tr>
			<tr>
				<td><asp:TextBox ID="txtOutput" Runat="server" TextMode="MultiLine" Width="100%" Rows="6" /></td>
			</tr>
		</table>
	</form>
  </body>
</html>
