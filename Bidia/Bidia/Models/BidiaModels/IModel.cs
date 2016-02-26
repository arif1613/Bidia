using System.ComponentModel.DataAnnotations;

namespace Bidia.Models.BidiaModels
{
	public interface IModel
	{
		[Key]
		int Id { get; set; }
		[Required]
		[DataType(DataType.Text)]
		string Name { get; set; }
	}
}
