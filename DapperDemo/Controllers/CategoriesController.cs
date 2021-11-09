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
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBonusRepository _bonusRepository;
        private readonly IDapperSprocRepo _dapperRepository;

        public CategoriesController(ICategoryRepository categoryRepository, IBookRepository bookRepository, IBonusRepository bonusRepository, IDapperSprocRepo dapperRepository)
        {
            _categoryRepository = categoryRepository;
            _dapperRepository = dapperRepository;
            _bookRepository = bookRepository;
            _bonusRepository = bonusRepository;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(_categoryRepository.GetAll());
            //return View(_dapperRepository.List<Category>("usp_GetALLCategory"));
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _bonusRepository.GetCategoryWithBooks(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name,Address,City,State,PostalCode")] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           // var category = _dapperRepository.Single<Category>("usp_GetCategory", new  { CategoryId = id.GetValueOrDefault() });
            var category = _categoryRepository.Find(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name,Address,City,State,PostalCode")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _categoryRepository.Remove(id.GetValueOrDefault());
            return RedirectToAction(nameof(Index));
        }
    }
}
