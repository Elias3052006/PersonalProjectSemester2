using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Domain
{
    public class Category
    {
        public string Name {  get; }
        public string Description { get; }
        public int CategoryId { get; private set; }
        public Category(string name, string description, int categoryId)
        {
            Name = name;
            Description = description;
            CategoryId = categoryId;
        }
    }
}
