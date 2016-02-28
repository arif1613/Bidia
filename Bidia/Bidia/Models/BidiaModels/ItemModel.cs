using System;
using System.ComponentModel.DataAnnotations;

namespace Bidia.Models.BidiaModels
{
	public class ItemModel:IModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		[DataType(DataType.MultilineText)]
		public string ItemDescription { get; set; }
		[DataType(DataType.Text)]
		public string ItemProductType { get; set; }
		[DataType(DataType.Text)]
		public string ItemOriantation { get; set; }
		[DataType(DataType.DateTime)]
		public DateTime ItemDatetime { get; set; }
		[Required]
		public double ItemPrice { get; set; }
		public bool IsHomePage { get; set; }
		public bool IsItemAvailable { get; set; }
		public string DeliveryTime { get; set; }
	}
}
