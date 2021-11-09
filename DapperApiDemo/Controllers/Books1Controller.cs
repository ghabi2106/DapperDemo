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
    public class Books1Controller : ControllerBase
    {
        private readonly ILogger<Books1Controller> _logger;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBonusRepository _bonusRepository;

        public Books1Controller(ILogger<Books1Controller> logger, ICategoryRepository categoryRepository, 
            IBookRepository bookRepository, IBonusRepository bonusRepository)
        {
            _categoryRepository = categoryRepository;
            _bonusRepository = bonusRepository;
            _bookRepository = bookRepository;
            _logger = logger;
        }

        [HttpGet("{categoryId:int}")]
        public List<Book> Get(int categoryId)
        {
            List<Book> books = _bonusRepository.GetBookWithCategory(categoryId);
            return books;
        }

        [HttpPost]
        public async Task<Book> CreateAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
            return book;
        }

        [HttpPut]
        public Book Update(Book book)
        {
            _bookRepository.Update(book);
            return book;
        }

        [HttpDelete]
        public int Delete(int id)
        {
            _bookRepository.Remove(id);
            return 1;
        }
    }
}
