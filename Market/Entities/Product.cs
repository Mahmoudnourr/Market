using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Entities
{
	public class product
	{
		public int id { get; set; }

		public string name { get; set; }

		public string description { get; set; }


		public int category_id { get; set; }

		[ForeignKey(nameof(category_id))]

		public category category { get; set; }

		public float price { get; set; }

		public int stock_quantity { get; set; }

		public discount? discount { get; set; }

		public byte[] image { get; set; }
	}
}