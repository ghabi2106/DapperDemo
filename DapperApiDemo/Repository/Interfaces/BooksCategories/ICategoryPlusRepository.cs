using DapperApiDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApiDemo.Repository.Interfaces
{
    public interface ICategoryPlusRepository
    {
        #region Insert
        Category AddCategory(Category category);

        List<Category> AddCategories(List<Category> categories);

        Category AddCategoryWithBooks(Category category);

        List<Category> AddCategoriesWithBooks(List<Category> categories);
        #endregion

        #region delete
        void RemoveCategory(int id);

        void RemoveCategories(List<int> id);

        void RemoveCategoryWithBooks(int id);

        void RemoveCategoriesWithBooks(List<int> id);
        #endregion

        #region Update
        Category UpdateCategory(Category category);

        List<Category> UpdateCategories(List<Category> categories);
        Category UpdateCategoryWithBooks(Category category);

        List<Category> UpdateCategoriesWithBooks(List<Category> categories);
        #endregion

        #region Merge
        Category MergeCategory(Category category);

        List<Category> MergeCategories(List<Category> categories);
        Category MergeCategoryWithBooks(Category category);

        List<Category> MergeCategoriesWithBooks(List<Category> categories);
        #endregion

    }
}
