using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Identity;

namespace Market.Entities
{
	public class application_user : IdentityUser
	{
		public string first_name { get; set; }
		public string last_name { get; set; }
		public DateTime created_at { get; set; }=DateTime.UtcNow;
		public DateTime updated_at { get; set; }

	}
}