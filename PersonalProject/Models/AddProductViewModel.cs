using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewLayer.Models
{
    public class AddProductViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Stock { get; set; }
        public string OEM { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string PhotoUrl { get; set; } = string.Empty;

        
        [ValidateNever]
        public List<SelectListItem> Brands { get; set; } = new();

        [ValidateNever]
        public List<SelectListItem> Categories { get; set; } = new();
    }
}