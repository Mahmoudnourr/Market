using System.ComponentModel.DataAnnotations;

namespace Market.Entities
{
	public class category
	{
		[Key]
		public int category_id { get; set; }
		public string category_name { get; set; }
		public string? category_description { get; set; }
		public ICollection<product> products { get; set; }
	}
}