namespace Market.Entities
{
	public class Discount
	{
		public int id { get; set; }
		public string name { get; set; }
		public float DiscountPercentage { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndDate { get; set; }
	}
}
