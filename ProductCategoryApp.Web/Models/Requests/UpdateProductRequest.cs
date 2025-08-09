using System.ComponentModel.DataAnnotations;

namespace ProductCategoryApp.Web.Models.Requests
{
    public class UpdateProductRequest
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Currency { get; set; } = string.Empty;

        [Required]
        public Guid CategoryId { get; set; }
    }
}
