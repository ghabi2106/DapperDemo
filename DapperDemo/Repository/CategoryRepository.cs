using Dapper;
using DapperDemo.Data;
using DapperDemo.Models;
using DapperDemo.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private IDbConnection db;

        public CategoryRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public Category Add(Category category)
        {
            var sql = "INSERT INTO Categories (Name, Address, City, State, PostalCode) VALUES(@Name, @Address, @City, @State, @PostalCode);"
                        + "SELECT CAST(SCOPE_IDENTITY() as int); ";
            var id = db.Query<int>(sql,category).Single();
            category.CategoryId = id;
            return category;

        }

        public Category Find(int id)
        {
            var sql = "SELECT * FROM Categories WHERE CategoryId = @CategoryId";
            return db.Query<Category>(sql, new { @CategoryId = id }).Single();
        }

        public List<Category> GetAll()
        {
            var sql = "SELECT * FROM Categories";
            return db.Query<Category>(sql).ToList();
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM Categories WHERE CategoryId = @Id";
            db.Execute(sql, new { id });
        }

        public Category Update(Category category)
        {
            var sql = "UPDATE Categories SET Name = @Name, Address = @Address, City = @City, " +
                "State = @State, PostalCode = @PostalCode WHERE CategoryId = @CategoryId";
            db.Execute(sql, category);
            return category;
        }
    }
}
