using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ViewLayer.Models
{
    public class SearchViewModel
    {
        [Required(ErrorMessage = "Enter a search term")]
        public string Query { get; set; }

        [ValidateNever]
        public IReadOnlyList<Product> Products { get; set; }
    }
}
