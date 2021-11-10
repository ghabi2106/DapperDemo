using DapperApiDemo.Models;
using DapperApiDemo.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Categories1Controller : ControllerBase
    {
        private readonly ILogger<Categories1Controller> _logger;
        private readonly ICategoryPlusRepository _categoryPlusRepository;

        public Categories1Controller(ILogger<Categories1Controller> logger, ICategoryPlusRepository categoryPlusRepository)
        {
            _categoryPlusRepository = categoryPlusRepository;
            _logger = logger;
        }

        #region Create
        [HttpPost("CreateCategory")]
        public Category CreateCategory(Category category)
        {
            return _categoryPlusRepository.AddCategory(category);
        }

        [HttpPost("CreateCategories")]
        public List<Category> CreateCategories(List<Category> categories)
        {
            return _categoryPlusRepository.AddCategories(categories);
        }

        [HttpPost("CreateCategoryWithBooks")]
        public Category CreateCategoryWithBooks(Category category)
        {
            return _categoryPlusRepository.AddCategoryWithBooks(category);
        }

        [HttpPost("CreateCategoriesWithBooks")]
        public List<Category> CreateCategoriesWithBooks(List<Category> categories)
        {
            return _categoryPlusRepository.AddCategoriesWithBooks(categories);
        }
        #endregion

        #region Update
        [HttpPut("UpdateCategory")]
        public Category UpdateCategory(Category category)
        {
            return _categoryPlusRepository.UpdateCategory(category);
        }

        [HttpPut("UpdateCategories")]
        public List<Category> UpdateCategories(List<Category> categories)
        {
            return _categoryPlusRepository.UpdateCategories(categories);
        }

        [HttpPut("UpdateCategoryWithBooks")]
        public Category UpdateCategoryWithBooks(Category category)
        {
            return _categoryPlusRepository.UpdateCategoryWithBooks(category);
        }

        [HttpPut("UpdateCategoriesWithBooks")]
        public List<Category> UpdateCategoriesWithBooks(List<Category> categories)
        {
            return _categoryPlusRepository.UpdateCategoriesWithBooks(categories);
        }
        #endregion

        #region Delete
        [HttpDelete("DeleteCategory/{id:int}")]
        public bool DeleteCategory(int id)
        {
            _categoryPlusRepository.RemoveCategory(id);
            return true;
        }

        [HttpDelete("DeleteCategories")]
        public bool DeleteCategories(List<int> id)
        {
            _categoryPlusRepository.RemoveCategories(id);
            return true;
        }

        [HttpDelete("DeleteCategoryWithBooks/{id:int}")]
        public bool DeleteCategoryWithBooks(int id)
        {
            _categoryPlusRepository.RemoveCategoryWithBooks(id);
            return true;
        }

        [HttpDelete("DeleteCategoriesWithBooks")]
        public bool DeleteCategoriesWithBooks(List<int> id)
        {
            _categoryPlusRepository.RemoveCategoriesWithBooks(id);
            return true;
        }
        #endregion

        #region Merge
        [HttpPost("MergeCategory")]
        public Category MergeCategory(Category category)
        {
            return _categoryPlusRepository.MergeCategory(category);
        }

        [HttpPost("MergeCategories")]
        public List<Category> MergeCategories(List<Category> categories)
        {
            return _categoryPlusRepository.MergeCategories(categories);
        }

        [HttpPost("MergeCategoryWithBooks")]
        public Category MergeCategoryWithBooks(Category category)
        {
            return _categoryPlusRepository.MergeCategoryWithBooks(category);
        }

        [HttpPost("MergeCategoriesWithBooks")]
        public List<Category> MergeCategoriesWithBooks(List<Category> categories)
        {
            return _categoryPlusRepository.MergeCategoriesWithBooks(categories);
        }
        #endregion
    }
}
