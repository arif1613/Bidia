using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Bidia.Models.BidiaModels;

namespace Bidia.Controllers
{
	public class ItemModelsController : Controller
	{
		private BidiaDB db = new BidiaDB();

		// GET: ItemModels
		[AllowAnonymous]

		public ActionResult Index(string itemtype)
		{
			var v = db.Items.Where(r => r.ItemProductType == itemtype).OrderByDescending(r => r.ItemDatetime).ToList();
			return View(v);
		}

		// GET: ItemModels/Details/5
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

		// GET: ItemModels/Create
		[Authorize]
		public ActionResult Create()
		{
			var v = db.ProductTypes.Select(r=>r.Name).ToList();
			ViewBag.pro = v;
			return View();
		}

		[Authorize]

		// POST: ItemModels/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Id,Name,ItemDescription,ItemProductType,ItemOriantation,ItemDatetime,ItemPrice,IsItemAvailable,DeliveryTime")] ItemModel itemModel)
		{
			if (ModelState.IsValid)
			{
				db.Items.Add(itemModel);
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
			}

			return View(itemModel);
		}

		// GET: ItemModels/Edit/5
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
		public async Task<ActionResult> Edit([Bind(Include = "Id,Name,ItemDescription,ItemProductType,ItemOriantation,ItemDatetime,ItemPrice,IsItemAvailable,DeliveryTime")] ItemModel itemModel)
		{
			if (ModelState.IsValid)
			{
				db.Entry(itemModel).State = EntityState.Modified;
				await db.SaveChangesAsync();
				return RedirectToAction("Index");
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
			return RedirectToAction("Index");
		}


		[AllowAnonymous]
		public PartialViewResult Itempartialview(int id)
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

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
