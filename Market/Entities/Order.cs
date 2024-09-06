using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Entities
{
	public class Order
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public string Status { get; set; }
		public float TotalAmount { get; set; }
		public string CustomerId { get; set; }
		[ForeignKey("CustomerId")]
		public ApplicationUser Customer { get; set; }
		public ICollection<OrderItem>? Items { get; set; }
		public ICollection<Payment> Payments { get; set; }
	}
}
