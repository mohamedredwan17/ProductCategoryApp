using System.ComponentModel.DataAnnotations;

namespace ProductCategoryApp.Web.Models.Requests
{
    public class UpdateCategoryRequest
    {
        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
    }
}
