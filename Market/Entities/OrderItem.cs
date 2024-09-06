using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Entities
{
	public class OrderItem
	{
		public int Id { get; set; }
		public float Price { get; set; }
		public int Quantity { get; set; }
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		[ForeignKey("ProductId")]
		public Product Product { get; set; }
		[ForeignKey("OrderId")]

		public Order Order { get; set; }

	}
}
