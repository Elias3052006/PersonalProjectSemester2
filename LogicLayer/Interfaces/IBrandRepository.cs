using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicLayer.Domain;

namespace LogicLayer.Interfaces
{
    public interface IBrandRepository
    {
        IReadOnlyList<Brand> GetAll();
        Brand GetById(int brandId);
    }
}
