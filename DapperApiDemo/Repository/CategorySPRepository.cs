using Dapper;
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
    public class CategorySPRepository : ICategoryRepository
    {
        private IDbConnection db;

        public CategorySPRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public Category Add(Category category)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryId", 0, DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@Name", category.Name);
            parameters.Add("@Description", category.Description);
            this.db.Execute("usp_AddCategory", parameters, commandType: CommandType.StoredProcedure);
            category.CategoryId = parameters.Get<int>("CategoryId");

            return category;
        }

        public Category Find(int id)
        {
            return db.Query<Category>("usp_GetCategory", new { CategoryId = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
        }

        public List<Category> GetAll()
        {
           
            return db.Query<Category>("usp_GetALLCategory", commandType: CommandType.StoredProcedure).ToList();
        }

        public void Remove(int id)
        {
            db.Execute("usp_RemoveCategory", new { CategoryId = id }, commandType: CommandType.StoredProcedure);
        }

        public Category Update(Category category)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryId", category.CategoryId, DbType.Int32);
            parameters.Add("@Name", category.Name);
            parameters.Add("@Description", category.Description);
            this.db.Execute("usp_UpdateCategory", parameters, commandType: CommandType.StoredProcedure);
           
            return category;
        }
    }
}
