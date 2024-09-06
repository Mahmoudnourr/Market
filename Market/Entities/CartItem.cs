using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Entities
{
	public class CartItem
	{

		public int Id { get; set; }
		public int ProductId { get; set; }
		[ForeignKey(nameof(ProductId))]
		public Product Product { get; set; }

	}
}
