using System.ComponentModel.DataAnnotations;

namespace Market.Dtos
{
	public class AddRoleModel
	{
		[Required]
		public string UserId { get; set; }

		[Required]
		public string Role { get; set; }
	}
}
