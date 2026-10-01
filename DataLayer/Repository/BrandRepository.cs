using DataLayer.DataContext;
using LogicLayer.Domain;
using LogicLayer.Interfaces;
using MySqlConnector;

namespace DataLayer.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly DatabaseContext _connectieString;

        public BrandRepository(DatabaseContext context)
        {
            _connectieString = context;
        }

        public IReadOnlyList<Brand> GetAll()
        {
            var brands = new List<Brand>();

            using var connection = _connectieString.CreateConnection();
            connection.Open();
            using var command = new MySqlCommand(
                "SELECT BrandId, Name, Description FROM brand ORDER BY Name;", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                brands.Add(Map(reader));
            }
            return brands;
        }

        public Brand GetById(int brandId)
        {
            using var connection = _connectieString.CreateConnection();
            connection.Open();
            using var command = new MySqlCommand(
                "SELECT BrandId, Name, Description FROM brand WHERE BrandId = @BrandId;", connection);
            command.Parameters.AddWithValue("@BrandId", brandId);
            using var reader = command.ExecuteReader();

            if (!reader.Read())
            {
                throw new KeyNotFoundException($"Merk met Id {brandId} bestaat niet");
            }
            return Map(reader);
        }

        private static Brand Map(MySqlDataReader reader)
        {
            return new Brand(
                reader.GetString("Name"),
                reader.GetString("Description"),
                reader.GetInt32("BrandId"));
        }
    }
}
