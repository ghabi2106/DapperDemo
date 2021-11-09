using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DapperDemo.Data;
using DapperDemo.Models;
using DapperDemo.Repository.Interfaces;

namespace DapperDemo.Controllers
{
    public class BooksController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBonusRepository _bonusRepository;

        [BindProperty]
        public Book Book { get; set; }

        public BooksController(ICategoryRepository categoryRepository, IBookRepository bookRepository, IBonusRepository bonusRepository)
        {
            _categoryRepository = categoryRepository;
            _bonusRepository = bonusRepository;
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index(int categoryId=0)
        {
            //List<Book> books = _bookRepository.GetAll();
            //foreach(Book obj in books)
            //{
            //    obj.Category = _categoryRepository.Find(obj.CategoryId);
            //}

            List<Book> books = _bonusRepository.GetBookWithCategory(categoryId);
            return View(books);
        }

      

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> categoryList = _categoryRepository.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.CategoryId.ToString()
            });
            ViewBag.CategoryList = categoryList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePOST()
        {
            if (ModelState.IsValid)
            {
               await _bookRepository.AddAsync(Book);
                return RedirectToAction(nameof(Index));
            }
            return View(Book);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book = _bookRepository.Find(id.GetValueOrDefault());
            IEnumerable<SelectListItem> categoryList = _categoryRepository.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.CategoryId.ToString()
            });
            ViewBag.CategoryList = categoryList;
            if (Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != Book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _bookRepository.Update(Book);
                return RedirectToAction(nameof(Index));
            }
            return View(Book);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _bookRepository.Remove(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index));
        }
    }
}
