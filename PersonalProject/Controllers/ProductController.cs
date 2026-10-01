using Microsoft.AspNetCore.Mvc;
using ViewLayer.Models;
using LogicLayer.Domain;
using LogicLayer.Service;

namespace ViewLayer.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpGet] public IActionResult Add() 
        {
            return View(new AddProductViewModel());
        }
        [HttpPost] public IActionResult Add(AddProductViewModel model) 
        { 
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            Product NewProduct = new Product(model.Titel, model.Description, model.OEM, model.Stock);

            _productService.CreateProduct(NewProduct);


            return RedirectToAction("Add");
            


        }
    }
}
