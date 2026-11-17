using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Request
{
    public class CreateProductRequest
    {
        public string Title { get; }
        public string Description { get; }
        public int Stock { get; }
        public string OEM { get; }
        public decimal Price { get; }
        public int BrandId { get; }
        public int CategoryId { get; }
        public string PhotoUrl { get; }

        public CreateProductRequest(string title, string description, int stock, string oEM, decimal price, int brandId, int categoryId, string photoUrl)
        {
            Title = title;
            Description = description;
            Stock = stock;
            OEM = oEM;
            Price = price;
            BrandId = brandId;
            CategoryId = categoryId;
            PhotoUrl = photoUrl;
        }
    }
}
