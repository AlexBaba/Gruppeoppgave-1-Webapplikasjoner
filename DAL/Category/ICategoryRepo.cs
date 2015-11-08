﻿using System.Collections.Generic;
using Nettbutikk.Model;

namespace DAL.Category
{
    public interface ICategoryRepo
    {
        bool AddCategory(string name);
        bool AddOldCategory(string Name, int adminId);
        bool DeleteCategory(int CategoryId);
        List<CategoryModel> GetAllCategories();
        CategoryModel GetCategory(int CategoryId);
        string GetCategoryName(int CategoryId);
        bool UpdateCategory(int CategoryId, string Name);
        int FirstCategoryWithProducts();

        // List<Category> GetAllCategories();
        // List<CategoryModel> GetAllCategoryModels();
        // Category GetCategory(int CategoryId);
    }
}