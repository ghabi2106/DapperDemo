using Dapper;
using Dapper.Contrib.Extensions;
using DapperApiDemo.Data;
using DapperApiDemo.Models;
using DapperApiDemo.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace DapperApiDemo.Repository
{
    public class CategoryRepositoryPlus : ICategoryPlusRepository
    {
        private readonly IDbConnection db;

        public CategoryRepositoryPlus(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        #region Insert
        public Category AddCategory(Category category)
        {
            DapperPlusManager.Entity<Category>().Identity(x => x.CategoryId, true);
            var t = db.BulkInsert(category);
            return category;
        }

        public List<Category> AddCategories(List<Category> categories)
        {
            DapperPlusManager.Entity<Category>().Table("Categories");
            db.BulkInsert(categories);
            return categories;
        }

        public Category AddCategoryWithBooks(Category category)
        {
            DapperPlusManager.Entity<Category>().Table("Categories").Identity(x => x.CategoryId, true);
            DapperPlusManager.Entity<Book>().Table("Books").Identity(x => x.BookId, true);
            db.BulkInsert(category).ThenForEach(x => x.Books.ForEach(y => y.CategoryId = x.CategoryId))
                .ThenBulkInsert(x => x.Books);
            return category;
        }

        public List<Category> AddCategoriesWithBooks(List<Category> categories)
        {
            DapperPlusManager.Entity<Category>().Table("Categories").Identity(x => x.CategoryId, true);
            DapperPlusManager.Entity<Book>().Table("Books").Identity(x => x.BookId, true);
            db/*.UseBulkOptions(options => options.InsertKeepIdentity = true)*/
                .BulkInsert(categories).ThenForEach(x => x.Books.ForEach(y => y.CategoryId = x.CategoryId))
                .ThenBulkInsert(x => x.Books);
            return categories;
        }
        #endregion

        #region delete
        public void RemoveCategory(int id)
        {
            DapperPlusManager.Entity<Category>().Table("Categories").Key("CategoryId");
            db.Delete(new Category { CategoryId = id });
        }

        public void RemoveCategories(List<int> id)
        {
            DapperPlusManager.Entity<Category>().Table("Categories").Key("CategoryId");
            //db.BulkDelete(db.Query<Category>("Select * FROM Categories WHERE CategoryId in @id", new { id }));

            var categories = new List<Category>();
            foreach (int item in id)
            {
                categories.Add(new Category { CategoryId = item });
            }
            db.BulkDelete(categories);
        }

        public void RemoveCategoryWithBooks(int id)
        {
            DapperPlusManager.Entity<Category>().Table("Categories").Identity(x => x.CategoryId);
            DapperPlusManager.Entity<Book>().Table("Books").Identity(x => x.BookId);
            //db.BulkDelete(db.Query<Category>("Select * FROM Categories WHERE CategoryId = @id", new { id }), category => category.Books);
            db.BulkDelete(new Category { CategoryId = id }, category => category.Books);
        }

        public void RemoveCategoriesWithBooks(List<int> id)
        {
            DapperPlusManager.Entity<Category>().Table("Categories").Identity(x => x.CategoryId);
            DapperPlusManager.Entity<Book>().Table("Books").Identity(x => x.BookId);
            //db.BulkDelete(db.Query<Category>("Select * FROM Categories WHERE CategoryId in @id", new { id }), category => category.Books);

            var categories = new List<Category>();
            foreach (int item in id)
            {
                categories.Add(new Category { CategoryId = item });
            }
            db.BulkDelete(categories, category => category.Books);
        }
        #endregion

        #region Update
        public Category UpdateCategory(Category category)
        {
            DapperPlusManager.Entity<Category>().Table("Categories");
            db.BulkUpdate(category);
            return category;
        }

        public List<Category> UpdateCategories(List<Category> categories)
        {
            DapperPlusManager.Entity<Category>().Table("Categories");
            db.BulkUpdate(categories);
            return categories;
        }
        public Category UpdateCategoryWithBooks(Category category)
        {
            DapperPlusManager.Entity<Category>().Table("Categories").Identity(x => x.CategoryId);
            DapperPlusManager.Entity<Book>().Table("Books").Identity(x => x.BookId);
            DapperPlusManager.Entity<Category>().Table("Categories");
            db.BulkUpdate(category);
            return category;
        }

        public List<Category> UpdateCategoriesWithBooks(List<Category> categories)
        {
            DapperPlusManager.Entity<Category>().Table("Categories").Identity(x => x.CategoryId);
            DapperPlusManager.Entity<Book>().Table("Books").Identity(x => x.BookId);
            DapperPlusManager.Entity<Category>().Table("Categories");
            db.BulkUpdate(categories);
            return categories;
        }
        #endregion

        #region Merge
        public Category MergeCategory(Category category)
        {
            DapperPlusManager.Entity<Category>().Table("Categories");
            db.BulkMerge(category);
            return category;
        }

        public List<Category> MergeCategories(List<Category> categories)
        {
            DapperPlusManager.Entity<Category>().Table("Categories");
            db.BulkMerge(categories);
            return categories;
        }
        public Category MergeCategoryWithBooks(Category category)
        {
            DapperPlusManager.Entity<Category>().Table("Categories").Identity(x => x.CategoryId, true);
            DapperPlusManager.Entity<Book>().Table("Books").Identity(x => x.BookId);
            db.BulkMerge(category, category => category.Books);
            return category;
        }

        public List<Category> MergeCategoriesWithBooks(List<Category> categories)
        {
            DapperPlusManager.Entity<Category>().Table("Categories").Identity(x => x.CategoryId, true);
            DapperPlusManager.Entity<Book>().Table("Books").Identity(x => x.BookId);
            db.BulkMerge(categories, category => category.Books);
            return categories;
        }
        #endregion
    }
}
