using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewLayer.Models
{
    public class AddProductViewModel
    {
        public string Title { get; set; } 
        public string Description { get; set; }
        public int Stock { get; set; }
        public string OEM { get; set; } 
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string PhotoUrl { get; set; }

        
        [ValidateNever]
        public List<SelectListItem> Brands { get; set; } 

        [ValidateNever]
        public List<SelectListItem> Categorys { get; set; } 
    }
}