using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
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
		//	foreach (var x in db.Items)
		//	{
		//		db.Items.Remove(x);
		//	}
		//	db.SaveChanges();
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

		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ItemModel itemModel = await db.Items.FindAsync(id);
			if (itemModel == null)
			{
				return HttpNotFound();
			}
			return View(itemModel);
		}

		[Authorize]

		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ItemModel itemModel = await db.Items.FindAsync(id);
			if (itemModel == null)
			{
				return HttpNotFound();
			}
			return View(itemModel);
		}

		// POST: ItemModels/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize]

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,Name,ItemDescription,ItemProductType,ItemOriantation," +
															 "ItemDatetime,ItemPrice,IsItemAvailable,DeliveryTime,IsHomePage")]
			ItemModel itemModel, HttpPostedFileBase picturefile)
		{
			if (ModelState.IsValid)
			{
				db.Entry(itemModel).State = EntityState.Modified;
				await db.SaveChangesAsync();
				if (picturefile != null && picturefile.ContentLength > 0)
				{
					string x1 = itemModel.Name;
					string newfilename = x1 + ".png";
					var filepath = Path.Combine(Server.MapPath("~/Pictures"), newfilename);
					picturefile.SaveAs(filepath);
				}
				var x =
					db.Items.Where(r => r.IsHomePage).OrderByDescending(r => r.ItemDatetime).ToList();
				return View("Index", x);
			}
			return View(itemModel);
		}

		// GET: ItemModels/Delete/5
		[Authorize]

		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ItemModel itemModel = await db.Items.FindAsync(id);
			if (itemModel == null)
			{
				return HttpNotFound();
			}
			return View(itemModel);
		}

		// POST: ItemModels/Delete/5
		[Authorize]

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			ItemModel itemModel = await db.Items.FindAsync(id);
			db.Items.Remove(itemModel);
			await db.SaveChangesAsync();
			var filepath = Path.Combine(Server.MapPath("~/Pictures"), itemModel.Name);
			var fileInfo = new FileInfo(filepath);

			if (fileInfo != null)
			{
				fileInfo.Delete();
			}

			return RedirectToAction("Index");
		}

		public void paypal_pay(decimal amount, int id)
		{
			var item = db.Items.Find(id);
			decimal d = new decimal();
			d = amount;

			string returnURL = "";
			returnURL += "https://www.paypal.com/xclick/business=KDG32QWYAZ9PY";
			returnURL += "&item_name=" + item.Name;
			returnURL += "&amount=" + d;
			returnURL += "&currency=SEK";
			returnURL += "&return=http://www.paymobd.com/Home/Thank_You";
			returnURL += "&cancel_return=http://www.paymobd.com";
			Response.Redirect(returnURL);
			//return returnURL;

		}
	}
}