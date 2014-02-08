<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_1dv406_2_1_Galleriet.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Galleriet</title>
	<link href="~\Style.css" rel="stylesheet" />
</head>
<body>
    <form id="MyForm" runat="server">
    <div id="maincontainer">
		<h1>Galleriet</h1>
		<p>
			<%--Val av fil att ladda upp--%>
			<asp:FileUpload ID="MyFileUpload" runat="server" />
			<%-- Validering--%>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
				ErrorMessage="En fil måste väljas" ControlToValidate="MyFileUpload"
				Display="None"></asp:RequiredFieldValidator>
			<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
				ControlToValidate="MyFileUpload" ValidationExpression=".(?:jpg|gif|png)"
				ErrorMessage="Endast bilder av typerna gif, jpg eller png är tillåtna"
				Display="None"></asp:RegularExpressionValidator>
			<asp:Button ID="UploadButton" runat="server" Text="Ladda upp" OnClick="UploadButton_Click" />
		</p>
		<p>
			<%-- Valideringsfelmeddelanden--%>
			<asp:ValidationSummary ID="ValidationSummary1" runat="server"
				HeaderText="Fel inträffade! Korrigera felet och försök igen." />
		</p>
		<p>
			<%--Presenterar resultat--%>
			<asp:Label ID="StatusLabel" runat="server" Text="Bilden har sparats" Visible="false"></asp:Label>
		</p>
    </div>
    </form>
</body>
</html>
