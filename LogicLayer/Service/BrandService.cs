using LogicLayer.Domain;
using LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Service
{
    public class BrandService
    {
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public IReadOnlyList<Brand> GetBrands()
        {
            IReadOnlyList<Brand> allBrands = _brandRepository.GetAll();
            return allBrands;
        }

        public Brand GetBrand(int brandId)
        {
            Brand brand = _brandRepository.GetById(brandId);
            return brand;
        }
    }
}
