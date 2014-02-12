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
		public Gallery Gallery 
		{ 
			get 
			{ 
				return _gallery ?? (_gallery = new Gallery()); 
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void UploadButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				if (MyFileUpload.HasFile)
				{
					Gallery.SaveImage(MyFileUpload.FileContent, MyFileUpload.FileName);
					StatusLabel.Visible = true;
				}
			}
		}

		public IEnumerable<_1dv406_2_1_Galleriet.Model.ThumbImage> ThumbsRepeater_GetData()
		{
			return Gallery.GetImageNames();
		}
	}
}