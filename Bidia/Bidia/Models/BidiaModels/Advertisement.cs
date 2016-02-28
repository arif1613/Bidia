using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bidia.Models.BidiaModels
{
	public class Advertisement:IModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}