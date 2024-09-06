using Microsoft.AspNetCore.Identity;

namespace Market.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }


	}
}
