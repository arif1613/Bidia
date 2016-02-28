using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bidia.Controllers
{
	public class GetPicture : Controller
	{
		DirectoryInfo Picdirectory { get; set; }
		string PicFileName { get; set; }

		public GetPicture(string name)
		{
			Picdirectory = new DirectoryInfo(Server.MapPath("~/Pictures"));
			PicFileName = name;
		}

		public string GetpicFileName()
		{
			var fileinfo = Picdirectory.EnumerateFiles();
			var itemfilename = PicFileName + ".png";
			var itempic = fileinfo.Where(r => r.Name == itemfilename).ToList();

			var p = "NoImage.png";
			if (itempic.Any())
			{
				var firstOrDefault = itempic.FirstOrDefault();
				if (firstOrDefault != null)
					p = firstOrDefault.Name;
			}

			return p;
		}

	}
}