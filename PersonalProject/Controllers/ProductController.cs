using LogicLayer.Domain;
using LogicLayer.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ViewLayer.Models;

namespace ViewLayer.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddProductViewModel();
            FillLists(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                FillLists(model);
                return View(model);
            }

            try
            {
                Brand brand = _productService.GetBrand(model.BrandId);
                Category category = _productService.GetCategory(model.CategoryId);
                Photo[] photos = { new Photo(1, model.PhotoUrl, 0) };

                Product newProduct = new Product(model.Title, model.Description, model.Stock,
                                                 model.OEM, model.Price, category, brand, photos);
                _productService.CreateProduct(newProduct);

                return RedirectToAction("Add");
            }
            catch (Exception ex) when (ex is ArgumentException || ex is KeyNotFoundException)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                FillLists(model);
                return View(model);
            }
        }

        private void FillLists(AddProductViewModel model)
        {
            model.Brands = _productService.GetBrands()
                .Select(b => new SelectListItem(b.Name, b.BrandId.ToString()))
                .ToList();

            model.Categories = _productService.GetCategories()
                .Select(c => new SelectListItem(c.Name, c.CategoryId.ToString()))
                .ToList();
        }
    }
}