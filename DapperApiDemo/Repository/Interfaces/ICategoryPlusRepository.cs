using DapperApiDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApiDemo.Repository.Interfaces
{
    public interface ICategoryPlusRepository
    {
        Category Find(int id);
        List<Category> GetAll();

        Category AddBulk(Category category);
        Category UpdateBulk(Category category);

        void RemoveBulk(int id);

    }
}
