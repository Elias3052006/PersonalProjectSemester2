using LogicLayer.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ViewLayer.Models
{
    public class ProductOverviewViewModel
    {
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        [Range(1, 1000000, ErrorMessage = "Min price cannot be negative")]
        public decimal? MinPrice { get; set; }
        [Range(1, 1000000, ErrorMessage = "Max price cannot be negative")]
        public decimal? MaxPrice { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        [ValidateNever]
        public List<SelectListItem> Brands { get; set; }

        [ValidateNever]
        public List<SelectListItem> Categorys { get; set; }

        [ValidateNever]
        public PagedResponse<Product> Result { get; set; }

        public ProductFilter ToFilter()
        {
            return new ProductFilter(CategoryId, BrandId, MinPrice, MaxPrice);
        }
    }
}