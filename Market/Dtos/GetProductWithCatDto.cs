using Market.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Dtos
{
	public class GetProductWithCatDto
	{
		public int id { get; set; }

		public string name { get; set; }

		public string description { get; set; }


		public int category_id { get; set; }
		public float price { get; set; }

		public int stock_quantity { get; set; }

		public byte[] image { get; set; }

	}
}
