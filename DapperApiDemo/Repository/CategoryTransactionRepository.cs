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
    public class CategoryTransactionRepository : ICategoryTransactionRepository
    {
        private readonly IDbConnection db;

        public CategoryTransactionRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        #region Insert
        public Category AddCategory(Category category)
        {
            db.Open();
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    DapperPlusManager.Entity<Category>().Identity(x => x.CategoryId, true);
                    var t = transaction.BulkInsert(category);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // roll the transaction back
                    transaction.Rollback();

                    // handle the error however you need to.
                    throw;
                }

                return category;
            }
        }

        public List<Category> AddCategories(List<Category> categories)
        {
            db.Open();
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    DapperPlusManager.Entity<Category>().Table("Categories");
                    transaction.BulkInsert(categories);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // roll the transaction back
                    transaction.Rollback();

                    // handle the error however you need to.
                    throw;
                }

                return categories;
            }
        }
        #endregion
    }
}
