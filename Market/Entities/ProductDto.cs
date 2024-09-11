using System.ComponentModel.DataAnnotations;

namespace Market.Entities
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be over 50 character")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Description cannot be over 200 character !!")]
        public string? Description { get; set; }
        public float Price { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

    }
    
}
