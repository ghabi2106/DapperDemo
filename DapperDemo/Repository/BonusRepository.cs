using Dapper;
using DapperDemo.Models;
using DapperDemo.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace DapperDemo.Repository
{
    public class BonusRepository : IBonusRepository
    {
        private IDbConnection db;

        public BonusRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public void AddTestCategoryWithBooks(Category objComp)
        {
            var sql = "INSERT INTO Categories (Name, Description) VALUES(@Name, @Description);"
                     + "SELECT CAST(SCOPE_IDENTITY() as int); ";
            var id = db.Query<int>(sql, objComp).Single();
            objComp.CategoryId = id;

            //foreach(var book in objComp.Books)
            //{
            //    book.CategoryId = objComp.CategoryId;
            //    var sql1 = "INSERT INTO Books (Title, Publisher, CategoryId) VALUES(@Title, @Publisher, @CategoryId);"
            //           + "SELECT CAST(SCOPE_IDENTITY() as int); ";
            //    db.Query<int>(sql1, book).Single();
            //}

            objComp.Books.Select(c => { c.CategoryId = id; return c; }).ToList();
              var sqlEmp = "INSERT INTO Books (Title, Publisher, CategoryId) VALUES(@Title, @Publisher, @CategoryId);"
                       + "SELECT CAST(SCOPE_IDENTITY() as int); ";

            db.Execute(sqlEmp, objComp.Books);
        }

        public void AddTestCategoryWithBooksWithTransaction(Category objComp)
        {
            using (var transaction = new TransactionScope())
            {
                try
                {
                    var sql = "INSERT INTO Categories (Title, Publisher) VALUES(@Title, @Publisher);"
                 + "SELECT CAST(SCOPE_IDENTITY() as int); ";
                    var id = db.Query<int>(sql, objComp).Single();
                    objComp.CategoryId = id;

                    objComp.Books.Select(c => { c.CategoryId = id; return c; }).ToList();
                    var sqlEmp = "INSERT INTO Books (Title, Publisher, CategoryId) VALUES(@Title, @Publisher, @CategoryId);"
                             + "SELECT CAST(SCOPE_IDENTITY() as int); ";
                    db.Execute(sqlEmp, objComp.Books);

                    transaction.Complete();
                }
                catch(Exception ex)
                {

                }
            }
        }


        public List<Category> GetAllCategoryWithBooks()
        {
            var sql = "SELECT C.*,E.* FROM Books AS E INNER JOIN Categories AS C ON E.CategoryId = C.CategoryId ";

            var categoryDic = new Dictionary<int, Category>();

            var category = db.Query<Category, Book, Category>(sql, (c, e) =>
            {
                if (!categoryDic.TryGetValue(c.CategoryId, out var currentCategory))
                {
                    currentCategory = c;
                    categoryDic.Add(currentCategory.CategoryId, currentCategory);
                }
                currentCategory.Books.Add(e);
                return currentCategory;
            }, splitOn: "BookId");

            return category.Distinct().ToList();

        }

        public Category GetCategoryWithBooks(int id)
        {
            var p = new
            {
                CategoryId = id
            };

            var sql = "SELECT * FROM Categories WHERE CategoryId = @CategoryId;"
                + " SELECT * FROM Books WHERE CategoryId = @CategoryId; ";

            Category category;

            using (var lists = db.QueryMultiple(sql, p))
            {
                category = lists.Read<Category>().ToList().FirstOrDefault();
                category.Books = lists.Read<Book>().ToList();
            }

            return category;
        }

        public List<Book> GetBookWithCategory(int id)
        {
            var sql = "SELECT E.*,C.* FROM Books AS E INNER JOIN Categories AS C ON E.CategoryId = C.CategoryId ";
            if (id != 0)
            {
                sql += " WHERE E.CategoryId = @Id ";
            }

            var book = db.Query<Book, Category, Book>(sql, (e, c) =>
            {
                e.Category = c;
                return e;
            },new { id }, splitOn: "CategoryId");

            return book.ToList();
        }

        public void RemoveRange(int[] categoryId)
        {
            db.Query("DELETE FROM Categories WHERE CategoryId IN @categoryId", new { categoryId });
        }

        public List<Category> FilterCategoryByName(string name)
        {
            return db.Query<Category>("SELECT * FROM Categories WHERE Name like '%' + @name + '%' ", new { name }).ToList();
        }
    }
}
