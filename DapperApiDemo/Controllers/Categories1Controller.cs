using DapperApiDemo.Models;
using DapperApiDemo.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperApiDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Categories1Controller : ControllerBase
    {
        private readonly ILogger<Categories1Controller> _logger;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBonusRepository _bonusRepository;

        public Categories1Controller(ILogger<Categories1Controller> logger, ICategoryRepository categoryRepository,
            IBookRepository bookRepository, IBonusRepository bonusRepository)
        {
            _categoryRepository = categoryRepository;
            _bonusRepository = bonusRepository;
            _bookRepository = bookRepository;
            _logger = logger;
        }

        [HttpGet]
        public List<Category> Get()
        {
            var categories = _categoryRepository.GetAll();
            return categories;
        }

        [HttpGet("{id:int}")]
        public Category Get(int id)
        {
            var category = _bonusRepository.GetCategoryWithBooks(id);
            return category;
        }

        [HttpPost]
        public Category Create(Category category)
        {
            _categoryRepository.Add(category);
            return category;
        }

        [HttpPut]
        public Category Update(Category category)
        {
            _categoryRepository.Update(category);
            return category;
        }

        [HttpDelete]
        public int Delete(int id)
        {
            _categoryRepository.Remove(id);
            return 1;
        }
    }
}
