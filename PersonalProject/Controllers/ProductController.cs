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
        private readonly CategoryService _categoryService;
        private readonly BrandService _brandService;

        public ProductController(ProductService productService, CategoryService categoryService, BrandService brandService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _brandService = brandService;
        }

        [HttpGet]
        public IActionResult Index(ProductOverviewViewModel model)
        {
            try
            {
                model.Result = _productService.GetProducts(model.ToFilter(), model.Page);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                model.Result = PagedResponse<Product>.Create(new List<Product>(), 1, 12);
            }

            FillFilterLists(model);
            return View(model);
        }

        [HttpGet]
        public IActionResult Search(SearchViewModel model)
        {
            model.Products = _productService.SearchProducts(model.Query);
            return View(model);
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
                _productService.CreateProduct(model.ToRequest());

                return RedirectToAction("Add");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                FillLists(model);
                return View(model);
            }
        }

        private void FillLists(AddProductViewModel model)
        {
            model.Brands = new List<SelectListItem>();
            foreach (var brand in _brandService.GetBrands())
            {
                model.Brands.Add(new SelectListItem(brand.Name, brand.BrandId.ToString()));
            }

            model.Categorys = new List<SelectListItem>();
            foreach (var category in _categoryService.GetCategorys())
            {
                model.Categorys.Add(new SelectListItem(category.Name, category.CategoryId.ToString()));
            }
        }

        private void FillFilterLists(ProductOverviewViewModel model)
        {
            model.Brands = new List<SelectListItem>();
            foreach (var brand in _brandService.GetBrands())
            {
                model.Brands.Add(new SelectListItem(brand.Name, brand.BrandId.ToString()));
            }

            model.Categorys = new List<SelectListItem>();
            foreach (var category in _categoryService.GetCategorys())
            {
                model.Categorys.Add(new SelectListItem(category.Name, category.CategoryId.ToString()));
            }
        }
    }
}