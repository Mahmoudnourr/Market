
﻿using Market.Dtos;
using Market.Validation;
using System.ComponentModel.DataAnnotations;

namespace Market.Entities
{
	public class ProductDto
	{
		[Required(ErrorMessage = "The Name is required.")]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name must contain only alphabetic characters.")]
		[StringLength(50, ErrorMessage = "Name cannot be over 50 character")]
		public string Name { get; set; }
		[Required(ErrorMessage = "The Description is required")]
		[StringLength(200, ErrorMessage = "Description cannot be over 200 character !!")]
		public string? Description { get; set; }
		[Required(ErrorMessage = "The Price is required")]
		[RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "Price must be a valid float.")]
		public float Price { get; set; }
		[Required(ErrorMessage = "Category is required.")]
		public int CategoryId { get; set; }
		[Required(ErrorMessage = "Please select a product image.")]
		[DataType(DataType.Upload)]
		[AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" }, ErrorMessage = "Only JPG, JPEG, and PNG files are allowed.")]
		[MaxFileSize(10 * 1024 * 1024, ErrorMessage = "Maximum allowed file size is 10 MB.")]
		public IFormFile img { get; set; }

		public int stock_quantity { get; set; }
	}
}