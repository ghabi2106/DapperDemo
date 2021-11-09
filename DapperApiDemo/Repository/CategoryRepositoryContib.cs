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

namespace DapperApiDemo.Repository
{
    public class CategoryRepositoryContib : ICategoryRepository
    {
        private IDbConnection db;

        public CategoryRepositoryContib(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public Category Add(Category category)
        {
            var id = db.Insert(category);
            category.CategoryId = (int)id;
            return category;
        }

        public Category Find(int id)
        {
            return db.Get<Category>(id);
        }

        public List<Category> GetAll()
        {
           
            return db.GetAll<Category>().ToList();
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
