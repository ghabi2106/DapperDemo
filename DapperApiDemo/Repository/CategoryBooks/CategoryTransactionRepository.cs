using Dapper;
using Dapper.Contrib.Extensions;
using Dapper.Transaction;
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

        #region Insert Dapper Plus
        public Category AddCategory(Category category)
        {
            db.Open();
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    DapperPlusManager.Entity<Category>().Identity(x => x.CategoryId, true);
                    transaction.BulkInsert(category);
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

        #region Insert Dapper
        public Category AddCategoryUsingDapper(Category category)
        {
            string sql = "INSERT INTO Categories (Name, Description) Values (@Name, @Description);";
            db.Open();
            using (var transaction = db.BeginTransaction())
            {
                try
                {
                    #region 2 Methods
                    #region First Method
                    // Dapper
                    var affectedRows = db.Execute(sql, category, transaction: transaction);
                    #endregion

                    #region Second Method
                    // Dapper Transaction with nuget package dapper.transaction
                    //var affectedRows2 = transaction.Execute(sql, category);
                    #endregion
                    #endregion
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
        #endregion
    }
}
