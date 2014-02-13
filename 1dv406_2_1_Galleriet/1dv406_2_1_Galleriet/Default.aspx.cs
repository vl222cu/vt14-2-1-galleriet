using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using _1dv406_2_1_Galleriet.Model;




namespace _1dv406_2_1_Galleriet
{
	public partial class Default : System.Web.UI.Page
	{
		// Fält
		private Gallery _gallery;

		// Egenskap 
		private Gallery Gallery 
		{ 
			get 
			{ 
				return _gallery ?? (_gallery = new Gallery()); 
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			var fileName = Request.QueryString["img"];

			// Visar bild i större format 
			if (fileName != null)
			{
				Gallery.ImageExists(fileName);
				MainImage.Visible = true;
				MainImage.ImageUrl = "~/Images/" + fileName;
			}

			// Vid lyckad uppladdning visas den uppladdade bilden
			if (Request.QueryString["uploaded"] == "success")
			{
				StatusLabel.Visible = true;
				StatusLabel.Text = String.Format("Bilden '{0}' har sparats.", fileName);
			}

			// Vid misslyckad uppladdning visas ett felmeddelande
			if (Request.QueryString["uploaded"] == "failed")
			{
				StatusLabel.Visible = true;
				StatusLabel.Text = String.Format("Ett fel inträffade då bilden '{0}' skulle överföras.", fileName);
			}
		}

		protected void UploadButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				if (MyFileUpload.HasFile)
				{
					try
					{
						Gallery.SaveImage(MyFileUpload.FileContent, MyFileUpload.FileName);
						Response.Redirect("?img=" + MyFileUpload.FileName + "&uploaded=success");
					}
					catch (Exception)
					{
						Response.Redirect("?img=" + MyFileUpload.FileName + "&uploaded=failed");
					}
				}
			}
		}

		public IEnumerable<_1dv406_2_1_Galleriet.Model.ThumbImage> ThumbsRepeater_GetData()
		{
			return Gallery.GetImageNames();
		}
	}
}