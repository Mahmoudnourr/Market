﻿using System.ComponentModel.DataAnnotations;

namespace Market.Validation
{
	public class AllowedExtensionsAttribute : ValidationAttribute
	{
		private readonly string[] _extensions;

		public AllowedExtensionsAttribute(string[] extensions)
		{
			_extensions = extensions;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is IFormFile file)
			{
				var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
				if (!_extensions.Contains(extension))
				{
					return new ValidationResult(GetErrorMessage());
				}
			}

			return ValidationResult.Success;
		}

		public string GetErrorMessage()
		{
			return $"Allowed file extensions are {string.Join(", ", _extensions)}";
		}
	}
}
