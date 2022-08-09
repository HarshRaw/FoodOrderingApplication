using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectApplication.Data;
using ProjectApplication.Models;

namespace ProjectApplication.Areas.FOA.Contollers
{
    [Area("FOA")]
    public class DishCategoriesController : Controller
    {
        private readonly ProjectDbContext _context;

        public DishCategoriesController(ProjectDbContext context)
        {
            _context = context;
        }

        // GET: FOA/DishCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.DishCategories.ToListAsync());
        }

        // GET: FOA/DishCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishCategory = await _context.DishCategories
                .FirstOrDefaultAsync(m => m.DcId == id);
            if (dishCategory == null)
            {
                return NotFound();
            }

            return View(dishCategory);
        }

        // GET: FOA/DishCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FOA/DishCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DcId,Categories")] DishCategory dishCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dishCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dishCategory);
        }

        // GET: FOA/DishCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishCategory = await _context.DishCategories.FindAsync(id);
            if (dishCategory == null)
            {
                return NotFound();
            }
            return View(dishCategory);
        }

        // POST: FOA/DishCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DcId,Categories")] DishCategory dishCategory)
        {
            if (id != dishCategory.DcId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dishCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishCategoryExists(dishCategory.DcId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dishCategory);
        }

        // GET: FOA/DishCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishCategory = await _context.DishCategories
                .FirstOrDefaultAsync(m => m.DcId == id);
            if (dishCategory == null)
            {
                return NotFound();
            }

            return View(dishCategory);
        }

        // POST: FOA/DishCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dishCategory = await _context.DishCategories.FindAsync(id);
            _context.DishCategories.Remove(dishCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishCategoryExists(int id)
        {
            return _context.DishCategories.Any(e => e.DcId == id);
        }
    }
}
