using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataContext;
using LogicLayer.Domain;
using LogicLayer.Interfaces;
using MySqlConnector;

namespace DataLayer.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _context;

        public ProductRepository(DatabaseContext context)
        {
            _context = context;
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
    }
}
