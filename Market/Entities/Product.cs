using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Entities
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public int CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		public Category Category { get; set; }
		public float Price { get; set; }
		public int StockQuantity { get; set; }
		public Discount? Discount { get; set; }
	}
}
