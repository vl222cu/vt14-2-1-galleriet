using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.IO;

namespace _1dv406_2_1_Galleriet.Model
{
	public class Gallery
	{
		// Fält som undersöker om en fil har tillåten
		// filändelse med hjälp av reguljärt uttryck
		private static readonly Regex ApprovedExtensions;

		// Fält som innehåller den fysiska sökvägen till
		// katalogen där uppladdade filer sparas
		private static string PhysicalUploadImagePath;

		// Fält som ser till att filnamn innehåller godkända 
		// tecken med hjälp av reguljärt uttryck
		private static readonly Regex SantizePath;

		// Konstruktor
		static Gallery()
		{
			ApprovedExtensions = new Regex("^.*.(gif|GIF|jpg|JPG|png|PNG)$");

			PhysicalUploadedImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Images");

			var invalidChars = new string(Path.GetInvalidFileNameChars());
			SantizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));
		}

		// Metod som returnerar en referens innehållande
		// bildernas filnamn sorterade i boktavsordning
		public IEnumerable<string> GetImageNames()
		{
			var sortedFiles = new DirectoryInfo(PhysicalUploadedImagesPath)
				.GetFiles()
				.OrderBy(f => f)
				.ToList();

			return sortedFiles;
		}

		// Metod som kontrollerar om en bild med angivet
		// namn finns i katalogen för uppladdade bilder
		public static bool ImageExists(string name)
		{
			return File.Exists(PhysicalApplicationPath + name);
		}

		// Metod som kontrollerar om den uppladdade filens
		// innehåll är av typen gif, jpg eller png
		public bool IsValidImage(Image image)
		{ 
			if (image.RawFormat.Guid == ImageFormat.Gif.Guid ||
				image.RawFormat.Guid == ImageFormat.Jpg.Guid ||
				image.RawFormat.Guid == ImageFormat.Png.Guid)
			{
				return true;
			}
			return false;
		}

		// Metod som verifierar, kontrollerar och sparar bild 
		// samt skapar och sparar en tumnagelbild
		public string SaveImage(Stream stream, string fileName)
		{
			if (!IsValidImage(fileName))
			{
				throw new ArgumentException("Bilden har fel MIME-typ");
			}

			if (ImageExists(fileName))
			{
				int count = 1;
				string fileNameOnly = Path.GetFileNameWithoutExtension(fileName);
				string extension = Path.GetExtension(fileName);

				while (File.Exists(fileName))
				{
					fileName = string.Format("{0}({1}){2}", fileNameOnly, count++, extension);				
				}
			}

			// Sparar bilden
			var image = System.Drawing.Image.FromStream(stream);
			image.Save(PhysicalUploadedImagesPath + fileName);

			// Skapar och sparar tumnagel
			var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);
			thumbnail.Save(PhysicalUploadedImagesPath + fileName);

			return fileName;
		}
	}
}