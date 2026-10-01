using LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Domain;

using LogicLayer.Domain;
using LogicLayer.Interfaces;

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

        public IReadOnlyList<Brand> GetBrands() => _brandRepository.GetAll();

        public IReadOnlyList<Category> GetCategories() => _categoryRepository.GetAll();

        public Brand GetBrand(int brandId) => _brandRepository.GetById(brandId);

        public Category GetCategory(int categoryId) => _categoryRepository.GetById(categoryId);
    }
}
