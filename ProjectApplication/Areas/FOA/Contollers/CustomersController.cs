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
    public class CustomersController : Controller
    {
        private readonly ProjectDbContext _context;

        public CustomersController(ProjectDbContext context)
        {
            _context = context;
        }

        // GET: FOA/Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: FOA/Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: FOA/Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FOA/Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerName,MobileNumber,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Email = customer.Email.Trim();
                bool ifduplicatefound = _context.Customers.Any(m => m.Email == customer.Email);
                bool ifduplicatefound2 = _context.Customers.Any(m => m.MobileNumber == customer.MobileNumber);
                if (ifduplicatefound2)
                {
                    ModelState.AddModelError("MobileNumber", "This Moobile Number Already exsist!");
                }
                else if(ifduplicatefound){
                    
                    ModelState.AddModelError("Email", "This Mail Already exsist!");
                }
                else
                {

                    _context.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(customer);
        }

        // GET: FOA/Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: FOA/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CustomerName,MobileNumber,Email")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                customer.Email =customer.Email.Trim();

                bool dupf= _context.Customers.Any(a=> a.Email == customer.Email && a.CustomerId!=customer.CustomerId);
                bool dupf2= _context.Customers.Any(a=> a.MobileNumber == customer.MobileNumber && a.CustomerId!=customer.CustomerId);

                if (dupf2)
                {
                    ModelState.AddModelError("MobileNumber", "Already Exsist!");
                    
                }
                else if (dupf)
                {

                    ModelState.AddModelError("Email", "Already Exsist!");
                }
                else
                {
                    try
                    {
                        _context.Update(customer);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CustomerExists(customer.CustomerId))
                        {

                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                
                
            }
            return View(customer);
        }

        // GET: FOA/Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: FOA/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
