using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Entities
{
	public class Payment
	{
		public int Id { get; set; }
		public float Amount { get; set; }
		public string Method { get; set; }
		public DateTime Date { get; set; }
		public int OrderId { get; set; }
		[ForeignKey("OrderId")]

		public Order Order { get; set; }
	}
}
