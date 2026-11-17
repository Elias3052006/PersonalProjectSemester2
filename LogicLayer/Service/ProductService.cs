using LogicLayer.Domain;
using LogicLayer.Interfaces;
using LogicLayer.Request;
using System.Collections.Generic;

namespace LogicLayer.Service
{
    public class ProductService
    {
        private const int ProductsPerPage = 12;

        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, IBrandRepository brandRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
        }

        public void CreateProduct(CreateProductRequest request)
        {
            Brand brand = _brandRepository.GetById(request.BrandId);
            Category category = _categoryRepository.GetById(request.CategoryId);

            Photo[] photos = { new Photo(1, request.PhotoUrl, 0) };

            Product product = new Product(0, request.Title, request.Description, request.Stock, request.OEM, request.Price, true, category, brand, photos);

            _productRepository.Add(product);
        }

        public IReadOnlyList<Product> SearchProducts(string query)
        {
            if (query == null)
            {
                query = "";
            }

            string cleanQuery = query.Trim();

            if (cleanQuery == "")
            {
                return new List<Product>();
            }

            return _productRepository.Search(cleanQuery);
        }

        public PagedResponse<Product> GetProducts(ProductFilter filter, int page)
        {
            if (filter == null)
            {
                filter = ProductFilter.Empty();
            }

            IReadOnlyList<Product> allProducts = _productRepository.GetAll();
            List<Product> filtered = new List<Product>();

            foreach (Product product in allProducts)
            {
                if (filter.Matches(product))
                {
                    filtered.Add(product);
                }
            }

            return PagedResponse<Product>.Create(filtered, page, ProductsPerPage);
        }
    }
}