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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext _context;

        public CategoryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IReadOnlyList<Category> GetAll()
        {
            var categories = new List<Category>();

            using var connection = _context.CreateConnection();
            connection.Open();
            using var command = new MySqlCommand(
                "SELECT CategoryId, Name, Description FROM category ORDER BY Name;", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                categories.Add(Map(reader));
            }
            return categories;
        }

        public Category GetById(int categoryId)
        {
            using var connection = _context.CreateConnection();
            connection.Open();
            using var command = new MySqlCommand(
                "SELECT CategoryId, Name, Description FROM category WHERE CategoryId = @CategoryId;", connection);
            command.Parameters.AddWithValue("@CategoryId", categoryId);
            using var reader = command.ExecuteReader();

            if (!reader.Read())
            {
                throw new KeyNotFoundException($"Categorie met Id {categoryId} bestaat niet");
            }
            return Map(reader);
        }

        private static Category Map(MySqlDataReader reader)
        {
            return new Category(
                reader.GetString("Name"),
                reader.GetString("Description"),
                reader.GetInt32("CategoryId"));
        }
    }
}
