using System.IO;
using System.Linq;
using System.Web.Mvc;
using Bidia.Models.BidiaModels;
using Microsoft.Ajax.Utilities;

namespace Bidia.Controllers
{
	public class HomeController : Controller
	{
		BidiaDB db = new BidiaDB();
		public ActionResult Index()
		{
			foreach (var x in db.Items)
			{
				db.Items.Remove(x);
			}
			db.SaveChanges();
			var v = db.Items.Where(r => r.IsHomePage).OrderByDescending(r => r.ItemDatetime).ToList();
			return View(v);
		}

		public PartialViewResult HomeItempartialview(int id)
		{
			ItemModel itemModel = db.Items.Find(id);
			var Picdirectory = new DirectoryInfo(Server.MapPath("~/Pictures"));
			var fileinfo = Picdirectory.EnumerateFiles();
			var itemfilename = itemModel.Name + ".png";
			var itempic = fileinfo.Where(r => r.Name == itemfilename).ToList();

			var p = "NoImage.png";
			if (itempic.Any())
			{
				var firstOrDefault = itempic.FirstOrDefault();
				if (firstOrDefault != null)
					p = firstOrDefault.Name;
			}

			ViewBag.pic = p;

			return PartialView(itemModel);
		}
	}
}