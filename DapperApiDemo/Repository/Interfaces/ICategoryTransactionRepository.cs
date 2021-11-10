using DapperApiDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApiDemo.Repository.Interfaces
{
    public interface ICategoryTransactionRepository
    {
        #region Insert
        Category AddCategory(Category category);

        List<Category> AddCategories(List<Category> categories);
        #endregion

    }
}
