using Nettbutikk.Model;
using System;
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
        
        public string GetCategoryName(int CategoryId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCategory(int CategoryId, string Name)
        {
            return CategoryId != -1;
        }

        public List<Category> GetAllCategories()
        {
            var allCategories = new List<Category> {
                new Category { CategoryId = 1, Name = "test name 1"},
                new Category { CategoryId = 2, Name = "test name 2"},
                new Category { CategoryId = 3, Name = "test name 3"},
                new Category { CategoryId = 4, Name = "test name 4"}
            };

            return allCategories;
        }

        public Category GetCategory(int CategoryId)
        {
            return CategoryId == -1 ? null : new Category { CategoryId = CategoryId, Name = "test name" };
        }

        public int FirstCategoryWithProducts()
        {
            throw new NotImplementedException();
        }
    }
}
