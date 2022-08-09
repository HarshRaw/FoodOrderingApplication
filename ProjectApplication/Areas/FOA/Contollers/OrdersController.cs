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
    public class OrdersController : Controller
    {
        private readonly ProjectDbContext _context;

        public OrdersController(ProjectDbContext context)
        {
            _context = context;
        }

        // GET: FOA/Orders
        public async Task<IActionResult> Index()
        {
            var projectDbContext = _context.Orders.Include(o => o.Dishes).Include(o => o.PaymentMethods).Include(o => o.Tables);
            return View(await projectDbContext.ToListAsync());
        }

        // GET: FOA/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Dishes)
                .Include(o => o.PaymentMethods)
                .Include(o => o.Tables)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: FOA/Orders/Create
        public IActionResult Create()
        {
            ViewData["DishId"] = new SelectList(_context.Dishes, "DishId", "DishName");
            ViewData["PaymentId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "PaymentMethodName");
            ViewData["TableId"] = new SelectList(_context.Tables, "TableId", "TableName");
            return View();
        }

        // POST: FOA/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,TableId,DishId,Quantity,PaymentId,Price,OrderPlaced")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DishId"] = new SelectList(_context.Dishes, "DishId", "DishName", order.DishId);
            ViewData["PaymentId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "PaymentMethodName", order.PaymentId);
            ViewData["TableId"] = new SelectList(_context.Tables, "TableId", "TableName", order.TableId);
            return View(order);
        }

        // GET: FOA/Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["DishId"] = new SelectList(_context.Dishes, "DishId", "DishName", order.DishId);
            ViewData["PaymentId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "PaymentMethodName", order.PaymentId);
            ViewData["TableId"] = new SelectList(_context.Tables, "TableId", "TableName", order.TableId);
            return View(order);
        }

        // POST: FOA/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,TableId,DishId,Quantity,PaymentId,Price,OrderPlaced")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["DishId"] = new SelectList(_context.Dishes, "DishId", "DishName", order.DishId);
            ViewData["PaymentId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "PaymentMethodName", order.PaymentId);
            ViewData["TableId"] = new SelectList(_context.Tables, "TableId", "TableName", order.TableId);
            return View(order);
        }

        // GET: FOA/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Dishes)
                .Include(o => o.PaymentMethods)
                .Include(o => o.Tables)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: FOA/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
