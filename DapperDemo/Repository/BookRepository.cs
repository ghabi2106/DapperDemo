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
    public class BookRepository : IBookRepository
    {
        private IDbConnection db;

        public BookRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public Book Add(Book book)
        {
            var sql = "INSERT INTO Books (Title, Publisher, CategoryId) VALUES(@Title, @Publisher, @CategoryId);"
                        +"SELECT CAST(SCOPE_IDENTITY() as int); ";
            var id = db.Query<int>(sql, book).Single();
            book.BookId = id;
            return book;
        }

        public async Task<Book> AddAsync(Book book)
        {
            var sql = "INSERT INTO Books (Title, Publisher, CategoryId) VALUES(@Title, @Publisher, @CategoryId);"
                        + "SELECT CAST(SCOPE_IDENTITY() as int); ";
            var id = await db.QueryAsync<int>(sql, book);
            book.BookId = id.Single();
            return book;
        }

        public Book Find(int id)
        {
            var sql = "SELECT * FROM Books WHERE BookId = @Id";
            return db.Query<Book>(sql, new { @Id = id }).Single();
        }

        public List<Book> GetAll()
        {
            var sql = "SELECT * FROM Books";
            return db.Query<Book>(sql).ToList();
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM Books WHERE BookId = @Id";
            db.Execute(sql, new { id });
        }

        public Book Update(Book book)
        {
            var sql = "UPDATE Books SET Title = @Title, Publisher = @Publisher, CategoryId = @CategoryId WHERE BookId = @BookId";
            db.Execute(sql, book);
            return book;
        }
    }
}
