using System.Text.Json.Serialization;

namespace Market.Dtos
{
	public class CategoryResponseDto
	{
		public int category_id { get; set; }
		public string category_name { get; set; }
		public string category_description { get; set; }
		[JsonIgnore]
		public bool Is_success { get; set; }
		[JsonIgnore]
		public string Message { get; set; }
	}
}