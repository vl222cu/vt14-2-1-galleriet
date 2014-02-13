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

			PhysicalUploadImagePath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Images");

			var invalidChars = new string(Path.GetInvalidFileNameChars());
			SantizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));
		}

		// Metod som returnerar en referens innehållande
		// bildernas filnamn sorterade i boktavsordning
		public IEnumerable<ThumbImage> GetImageNames()
		{
			var sortedFiles = new DirectoryInfo(Path.Combine(PhysicalUploadImagePath, "Thumbnails"));
			return (from fi in sortedFiles.GetFiles()
					select new ThumbImage
					{
						Name = fi.Name,
						ImgFileUrl = Path.Combine("/?img=", fi.Name),
						ThumbImgUrl = Path.Combine("Images/Thumbnails/", fi.Name)
					}).OrderBy(fi => fi.Name).ToList();
		}

		// Metod som kontrollerar om en bild med angivet
		// namn finns i katalogen för uppladdade bilder
		public static bool ImageExists(string name)
		{
			//return File.Exists(PhysicalUploadImagePath + name);
			return File.Exists(Path.Combine(PhysicalUploadImagePath, name));
		}

		// Metod som kontrollerar om den uppladdade filens
		// innehåll är av typen gif, jpg eller png
		public static bool IsValidImage(Image image)
		{
			if (image.RawFormat.Guid == ImageFormat.Gif.Guid ||
				image.RawFormat.Guid == ImageFormat.Jpeg.Guid ||
				image.RawFormat.Guid == ImageFormat.Png.Guid)
			{
				return true;
			}
			return false;
		}

		// Metod som verifierar, kontrollerar och sparar bild 
		// samt skapar och sparar en tumnagelbild
		public static string SaveImage(Stream stream, string fileName)
		{
			var image = System.Drawing.Image.FromStream(stream);
			fileName = SantizePath.Replace(fileName, "");

			if (!IsValidImage(image))
			{
				throw new ArgumentException("Bilden har fel MIME-typ");
			}

			if (ImageExists(fileName))
			{
				int count = 1;
				string fileNameOnly = Path.GetFileNameWithoutExtension(fileName);
				string extension = Path.GetExtension(fileName);
				string path = Path.GetDirectoryName(fileName);

				while (ImageExists(fileName))
				{
					fileName = string.Format("{0}({1}){2}", fileNameOnly, count++, extension);
				}
			}

			// Skapar tumnagel
			var thumbnail = image.GetThumbnailImage(60, 45, null, System.IntPtr.Zero);

			// Sparar bild och tumnagel
			image.Save(Path.Combine(PhysicalUploadImagePath, fileName));
			thumbnail.Save(PhysicalUploadImagePath + "/Thumbnails/" + fileName);

			return fileName;
		}
	}
}