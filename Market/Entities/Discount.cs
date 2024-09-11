namespace Market.Entities
{
	public class discount
	{
		public int id { get; set; }
		public string name { get; set; }
		public float discount_percentage { get; set; }
		public DateTime start_time { get; set; }
		public DateTime end_date { get; set; }
	}
}