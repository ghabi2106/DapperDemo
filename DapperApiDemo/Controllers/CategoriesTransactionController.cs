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
    public class CategoriesTransactionController : ControllerBase
    {
        private readonly ILogger<CategoriesTransactionController> _logger;
        private readonly ICategoryTransactionRepository _categoryTransactionRepository;

        public CategoriesTransactionController(ILogger<CategoriesTransactionController> logger, ICategoryTransactionRepository categoryTransactionRepository)
        {
            _categoryTransactionRepository = categoryTransactionRepository;
            _logger = logger;
        }

        #region Create
        [HttpPost("CreateCategory")]
        public Category CreateCategory(Category category)
        {
            return _categoryTransactionRepository.AddCategory(category);
        }

        [HttpPost("CreateCategories")]
        public List<Category> CreateCategories(List<Category> categories)
        {
            return _categoryTransactionRepository.AddCategories(categories);
        }
        #endregion
    }
}
