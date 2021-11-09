using DapperDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperDemo.Repository.Interfaces
{
    public interface IBookRepository
    {
        Book Find(int id);
        List<Book> GetAll();
        Book Add(Book book);
        Task<Book> AddAsync(Book book);
        Book Update(Book book);

        void Remove(int id);

    }
}
