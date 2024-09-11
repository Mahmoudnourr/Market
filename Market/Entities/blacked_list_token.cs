using System.ComponentModel.DataAnnotations;

namespace Market.Entities
{
	public class blacked_list_token
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string token { get; set; }

		[Required]
		public DateTime expiry_date { get; set; }
	}
}
