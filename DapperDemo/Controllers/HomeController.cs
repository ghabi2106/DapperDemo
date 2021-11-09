using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DapperDemo.Models;
using DapperDemo.Repository.Interfaces;

namespace DapperDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBonusRepository _bonusRepository;

        public HomeController(ILogger<HomeController> logger, IBonusRepository bonusRepository)
        {
            _logger = logger;
            _bonusRepository = bonusRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _bonusRepository.GetAllCategoryWithBooks();
            return View(categories);
        }

        public IActionResult AddTestRecords()
        {

            Category category = new Category()
            {
                Name = "Test" + Guid.NewGuid().ToString(),
                Address = "test address",
                City = "test city",
                PostalCode = "test postalCode",
                State = "test state",
                Books = new List<Book>()
            };

            category.Books.Add(new Book()
            {
                Email = "test Email",
                Name = "Test Name " + Guid.NewGuid().ToString(),
                Phone = " test phone",
                Title = "Test Manager"
            });

            category.Books.Add(new Book()
            {
                Email = "test Email 2",
                Name = "Test Name 2" + Guid.NewGuid().ToString(),
                Phone = " test phone 2",
                Title = "Test Manager 2"
            });
            _bonusRepository.AddTestCategoryWithBooksWithTransaction(category);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveTestRecords()
        {
            int[] categoryIdToRemove = _bonusRepository.FilterCategoryByName("Test").Select(i => i.CategoryId).ToArray();
            _bonusRepository.RemoveRange(categoryIdToRemove);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
