using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Identity;

namespace Market.Entities
{
	public class application_user : IdentityUser
	{
		// The user's first name
		public string first_name { get; set; }
		
		// The user's last name
		public string last_name { get; set; }
		
		// The date and time when the user account was created, automatically set to the current UTC time
		public DateTime created_at { get; set; } = DateTime.UtcNow;
		
		public DateTime updated_at { get; set; }
		public List<refresh_token>?refresh_tokens { get; set; }

	}
}