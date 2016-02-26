using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bidia.Models.BidiaModels
{
	public class BidiaDB:DbContext
	{
		public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<ItemModel> Items { get; set; }
	}
}
