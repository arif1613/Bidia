using System.ComponentModel.DataAnnotations;

namespace Bidia.Models.BidiaModels
{
	public class ProductType : IModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		[DataType(DataType.MultilineText)]
		public string ProductDescription { get; set; }

	}
}
