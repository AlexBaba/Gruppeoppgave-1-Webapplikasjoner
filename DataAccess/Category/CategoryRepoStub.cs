using Nettbutikk.Model;
using System.Collections.Generic;

namespace Nettbutikk.DataAccess
{
    public class CategoryRepoStub : ICategoryRepo
    {
        public bool AddCategory(string Name)
        {
            return Name != "invalid";
        }

        public bool AddOldCategory(string Name, int adminId)
        {
            return Name != "invalid";
        }

        public bool DeleteCategory(int CategoryId)
        {
            return CategoryId != -1;
        }

        public List<Category> GetAllCategories()
        {
            var allCategories = new List<Category> {
                new Category { CategoryId = 1, Name = "test1"},
                new Category { CategoryId = 2, Name = "test2"},
                new Category { CategoryId = 3, Name = "test3"},
                new Category { CategoryId = 4, Name = "test4"}
            };

            return allCategories;
        }
        
        public Category GetCategory(int categoryId)
        {
            return categoryId == -1 ? null : new Category { CategoryId = categoryId, Name = "test"};
        }
        
        public bool UpdateCategory(int CategoryId, string Name)
        {
            return CategoryId != -1;
        }
    }
}
