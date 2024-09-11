namespace Market.Dtos
{
	public class CategoryProductsDto
	{
		public int id { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public float price { get; set; }
		public int stock_quantity { get; set; }
		public byte[] image { get; set; }
	}
}