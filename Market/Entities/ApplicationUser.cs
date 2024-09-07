using Microsoft.AspNetCore.Identity;

namespace Market.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

	}
}
