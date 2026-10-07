using LogicLayer.Domain;
using LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Service
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IReadOnlyList<Category> GetCategorys()
        {
            IReadOnlyList<Category> allCategories = _categoryRepository.GetAll();
            return allCategories;
        }

        public Category GetCategory(int categoryId)
        {
            Category category = _categoryRepository.GetById(categoryId);
            return category;
        }
    }
}
