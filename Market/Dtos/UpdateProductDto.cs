﻿using Market.Validation;
using System.ComponentModel.DataAnnotations;

namespace Market.Dtos
{
	public class UpdateProductDto
	{
		[Required]
		public int Id { get; set; }
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
		public int stock_quantity { get; set; }

	}
}
