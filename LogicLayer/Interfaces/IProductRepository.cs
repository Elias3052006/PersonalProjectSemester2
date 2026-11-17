using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Domain;

namespace LogicLayer.Interfaces
{
    public interface IProductRepository
    {
        void Add(Product product);
        IReadOnlyList<Product> GetAll();
        IReadOnlyList<Product> Search(string query);
    }
}
