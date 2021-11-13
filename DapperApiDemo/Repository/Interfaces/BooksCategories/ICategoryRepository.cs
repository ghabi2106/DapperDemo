﻿using DapperApiDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApiDemo.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Category Find(int id);
        List<Category> GetAll();

        Category Add(Category category);
        Category Update(Category category);

        void Remove(int id);

    }
}
