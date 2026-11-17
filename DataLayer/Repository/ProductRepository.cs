using System;
using System.Collections.Generic;
using DataLayer.DataContext;
using LogicLayer.Domain;
using LogicLayer.Interfaces;
using MySqlConnector;

namespace DataLayer.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _context;
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductRepository(DatabaseContext context,
                                 IBrandRepository brandRepository,
                                 ICategoryRepository categoryRepository)
        {
            _context = context;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
        }
        public void Add(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);

            using var connection = _context.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                const string insertProduct = @"
                    INSERT INTO product (Title, Description, Stock, OEM, Price, IsActive, BrandId, CategoryId)
                    VALUES (@Title, @Description, @Stock, @OEM, @Price, @IsActive, @BrandId, @CategoryId);";

                long productId;
                using (var command = new MySqlCommand(insertProduct, connection, transaction))
                {
                    command.Parameters.AddWithValue("@Title", product.Title);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@Stock", product.Stock);
                    command.Parameters.AddWithValue("@OEM", product.OEM);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@IsActive", product.IsActive);
                    command.Parameters.AddWithValue("@BrandId", product.Brand.BrandId);
                    command.Parameters.AddWithValue("@CategoryId", product.Category.CategoryId);
                    command.ExecuteNonQuery();
                    productId = command.LastInsertedId;
                }

                const string insertPhoto = @"
                    INSERT INTO photo (PhotoNumber, Url, ProductId)
                    VALUES (@PhotoNumber, @Url, @ProductId);";

                foreach (Photo photo in product.Photos)
                {
                    using var photoCommand = new MySqlCommand(insertPhoto, connection, transaction);
                    photoCommand.Parameters.AddWithValue("@PhotoNumber", photo.PhotoNumber);
                    photoCommand.Parameters.AddWithValue("@Url", photo.URL);
                    photoCommand.Parameters.AddWithValue("@ProductId", productId);
                    photoCommand.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public IReadOnlyList<Product> GetAll()
        {
            using var connection = _context.CreateConnection();
            connection.Open();

            string sql = "SELECT ProductId, Title, Description, Stock, OEM, Price, IsActive, BrandId, CategoryId " +
                         "FROM product " +
                         "WHERE IsActive = 1;";

            using var command = new MySqlCommand(sql, connection);
            return ReadProducts(command);
        }
        public IReadOnlyList<Product> Search(string query)
        {
            using var connection = _context.CreateConnection();
            connection.Open();

            string sql = "SELECT p.ProductId, p.Title, p.Description, p.Stock, p.OEM, p.Price, p.IsActive, p.BrandId, p.CategoryId " +
                         "FROM product p " +
                         "JOIN brand b ON b.BrandId = p.BrandId " +
                         "JOIN category c ON c.CategoryId = p.CategoryId " +
                         "WHERE p.IsActive = 1 " +
                         "AND (p.Title LIKE @Search OR p.OEM LIKE @Search OR b.Name LIKE @Search OR c.Name LIKE @Search);";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Search", "%" + query + "%");
            return ReadProducts(command);
        }
        private List<Product> ReadProducts(MySqlCommand command)
        {
            List<Product> products = new List<Product>();

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int productId = reader.GetInt32("ProductId");
                int brandId = reader.GetInt32("BrandId");
                int categoryId = reader.GetInt32("CategoryId");

                Brand brand = _brandRepository.GetById(brandId);
                Category category = _categoryRepository.GetById(categoryId);
                Photo[] photos = GetPhotos(productId);

                Product product = new Product(
                    productId,
                    reader.GetString("Title"),
                    reader.GetString("Description"),
                    reader.GetInt32("Stock"),
                    reader.GetString("OEM"),
                    reader.GetDecimal("Price"),
                    reader.GetBoolean("IsActive"),
                    category,
                    brand,
                    photos);

                products.Add(product);
            }

            return products;
        }

        private Photo[] GetPhotos(int productId)
        {
            List<Photo> photos = new List<Photo>();

            using var connection = _context.CreateConnection();
            connection.Open();

            string sql = "SELECT PhotoId, PhotoNumber, Url FROM photo WHERE ProductId = @ProductId ORDER BY PhotoNumber;";
            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ProductId", productId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Photo photo = new Photo(
                    reader.GetInt32("PhotoNumber"),
                    reader.GetString("Url"),
                    reader.GetInt32("PhotoId"));
                photos.Add(photo);
            }

            return photos.ToArray();
        }
    }
}