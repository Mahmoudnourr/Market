using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Entities
{
	public class Cart
	{
		public int id { get; set; }
		public string CustomerId { get; set; }
		[ForeignKey(nameof(CustomerId))]
		public ApplicationUser Customer { get; set; }
		public ICollection<CartItem> Items { get; set; }
	}
}
