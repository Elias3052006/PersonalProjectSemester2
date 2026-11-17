using LogicLayer.Request;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ViewLayer.Models
{
    public class AddProductViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Stock is required")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "The OEM number is required")]
        public string OEM { get; set; }
        [Range(1, 1000000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Choose a brand")]
        public int BrandId { get; set; }
        [Required(ErrorMessage = "Choose a Category")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "A PhotoUrl is required")]
        [Url(ErrorMessage = "Enter a valid URL")]
        public string PhotoUrl { get; set; }

        [ValidateNever]
        public List<SelectListItem> Brands { get; set; }

        [ValidateNever]
        public List<SelectListItem> Categorys { get; set; }

        public CreateProductRequest ToRequest()
        {
            return new CreateProductRequest(Title, Description, Stock, OEM, Price, BrandId, CategoryId, PhotoUrl);
        }
    }
}