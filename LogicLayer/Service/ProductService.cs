using LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Domain;


namespace LogicLayer.Service
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository,
                              IBrandRepository brandRepository,
                              ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
        }

        public void CreateProduct(Product product)
        {
            _productRepository.Add(product);
        }

        public IReadOnlyList<Brand> GetBrands()
        {
            IReadOnlyList<Brand> allBrands = _brandRepository.GetAll();
            return allBrands;
        }

        public IReadOnlyList<Category> GetCategories()
        {
            IReadOnlyList<Category> allCategories = _categoryRepository.GetAll();
            return allCategories;
        }

        public Brand GetBrand(int brandId)
        {
            Brand brand = _brandRepository.GetById(brandId);
            return brand;
        }

        public Category GetCategory(int categoryId)
        {
            Category category = _categoryRepository.GetById(categoryId);
            return category;
        }
    }
}
