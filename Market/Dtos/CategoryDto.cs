using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Market.Dtos
{
	public class CategoryDto
	{
		[Required(ErrorMessage = "Category name is required")]
		[MaxLength(50, ErrorMessage = "Category name must be less than 50 characters")]
		public string category_name { get; set; }
		[MaxLength(250, ErrorMessage = "Category description must be less than 250 characters")]
		[Required(ErrorMessage = "Category description is required")]
		public string category_description { get; set; }
	}
}