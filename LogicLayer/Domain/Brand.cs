using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Domain
{
    public class Brand
    {
        public string Name { get; }
        public string Description { get; }
        public int BrandId { get; private set; }
        public Brand(string name, string description, int brandId)
        {
            Name = name;
            Description = description;
            BrandId = brandId;
        }
    }
}
