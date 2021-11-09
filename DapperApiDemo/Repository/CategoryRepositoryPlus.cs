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
    public class CategoryRepositoryPlus /*: ICategoryPlusRepository*/
    {
        private IDbConnection db;

        public CategoryRepositoryPlus(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public List<Category> AddCategories(List<Category> categories)
        {
            db.BulkInsert(categories);
            return categories;
        }

        public Category AddCategoryWithBooks(Category category)
        {
            db.UseBulkOptions(options => options.InsertKeepIdentity = true)
                .BulkInsert(category).ThenForEach(x => x.Books.ForEach(y => y.CategoryId = x.CategoryId))
                .ThenBulkInsert(x => x.Books);
            return category;
        }

        public List<Category> AddCategoriesWithBooks(List<Category> categories)
        {
            db.UseBulkOptions(options => options.InsertKeepIdentity = true)
                .BulkInsert(categories).ThenForEach(x => x.Books.ForEach(y => y.CategoryId = x.CategoryId))
                .ThenBulkInsert(x => x.Books);
            return categories;
        }

        public void Remove(int id)
        {
            db.Delete(new Category { CategoryId = id });
        }

        public Category Update(Category category)
        {
            db.Update(category);
            return category;
        }
    }
}
