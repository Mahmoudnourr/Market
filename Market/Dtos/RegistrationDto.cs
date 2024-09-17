using System.ComponentModel.DataAnnotations;

namespace Market.Dtos
{
	public class RegistrationDto
	{
		[Required(ErrorMessage = "The LastName is Required")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "The FirstName is Required")]

		public string LastName { get; set; }
		[Required(ErrorMessage = "The Email is Required")]
		[EmailAddress]
		public string Email { get; set; }
		[Required]

		public string Password { get; set; }
		[Required(ErrorMessage = "Confirm Password is required")]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		public string ConfirmPassword { get; set; }


		[Required]
		//[RegularExpression("[a-zA-Z][a-zA-Z0-9-_]{3,32}", ErrorMessage = "The Username is Invalid")]
		public string UserName { get; set; }
	}
}
