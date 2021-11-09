using DapperApiDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApiDemo.Repository.Interfaces
{
    public interface IBonusRepository
    {
        List<Book> GetBookWithCategory(int id);

        Category GetCategoryWithBooks(int id);

        List<Category> GetAllCategoryWithBooks();

        void AddTestCategoryWithBooks(Category objComp);

        void AddTestCategoryWithBooksWithTransaction(Category objComp);

        void RemoveRange(int[] categoryId);

        List<Category> FilterCategoryByName(string name);
    }
}
 