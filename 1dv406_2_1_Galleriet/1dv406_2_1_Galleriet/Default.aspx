<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="_1dv406_2_1_Galleriet.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Galleriet</title>
	<link href="~/Style.css" rel="stylesheet" />
	<script src="http://code.jquery.com/jquery-1.10.2.min.js"></script>
</head>
<body>
	<form id="MyForm" runat="server">
		<div id="MainContainer">
			<div id="Content">
				<h1>Galleriet</h1>
				<p>
					<%--Val av fil att ladda upp--%>
					<asp:FileUpload ID="MyFileUpload" runat="server" />
					<%-- Validering--%>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
						ErrorMessage="En fil måste väljas" ControlToValidate="MyFileUpload"
						Display="None"></asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
						ControlToValidate="MyFileUpload" ValidationExpression=".*.(gif|GIF|jpg|JPG|png|PNG)"
						ErrorMessage="Endast bilder av typerna gif, jpg eller png är tillåtna"
						Display="None"></asp:RegularExpressionValidator>
					<asp:Button ID="UploadButton" runat="server" Text="Ladda upp" OnClick="UploadButton_Click" />
				</p>

				<%-- Tumnagelbilder--%>
				<asp:Panel ID="ThumbsPanel" runat="server">
					<asp:Repeater ID="ThumbsRepeater" runat="server"
						ItemType="_1dv406_2_1_Galleriet.Model.ThumbImage"
						SelectMethod="ThumbsRepeater_GetData">
						<ItemTemplate>
							<asp:HyperLink ID="ThumbsHyperLink" runat="server" NavigateUrl='<%# Item.ImgFileUrl %>'>
								<asp:Image ID="ThumbImage" runat="server" ImageUrl='<%# Item.ThumbImgUrl %>' />
							</asp:HyperLink>
						</ItemTemplate>
					</asp:Repeater>
				</asp:Panel>

				<div id="Status">
					<p>
						<%-- Valideringsfelmeddelanden--%>
						<asp:ValidationSummary ID="ValidationSummary1" runat="server"
							HeaderText="Fel inträffade! Korrigera felet och försök igen." />
					</p>
					<%--Presenterar resultat--%>
					<asp:Label ID="StatusLabel" runat="server" Text="Bilden har sparats" Visible="false"></asp:Label>
				</div>

				<p>
					<%-- Visar vald bild i större format--%>
					<asp:Panel ID="MainImagePanel" runat="server">
						<asp:Image ID="MainImage" runat="server" ImageUrl="~/Images/goj1.png" Visible="false" Width="500" Height="400" />
					</asp:Panel>
				</p>
			</div>
		</div>
	</form>

	<%-- Tonar ut statusmeddelande vid lyckad uppladdning --%>
	<script>
		$(document).ready(function () {
			var $statusText = $("#StatusLabel");
			if ($statusText.length) {
				setTimeout(function () {
					$statusText.fadeOut();
				}, 2500);
			}
		});
</script>
</body>
</html>
