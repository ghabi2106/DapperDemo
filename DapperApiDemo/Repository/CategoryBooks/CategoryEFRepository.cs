﻿using DapperApiDemo.Data;
using DapperApiDemo.Models;
using DapperApiDemo.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApiDemo.Repository
{
    public class CategoryEFRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryEFRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Category Add(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return category;
        }

        public Category Find(int id)
        {
            return _db.Categories.FirstOrDefault(u=>u.CategoryId==id);
        }

        public List<Category> GetAll()
        {
            return _db.Categories.ToList();
        }

        public void Remove(int id)
        {
            Category category = _db.Categories.FirstOrDefault(u => u.CategoryId == id);
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return;
        }

        public Category Update(Category category)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
            return category;
        }
    }
}
