using System.Data.Entity;

namespace Bidia.Models.BidiaModels
{
	public class BidiaDB:DbContext
	{
		public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<ItemModel> Items { get; set; }
	}
}
