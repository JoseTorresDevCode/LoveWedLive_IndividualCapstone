using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoveWedLive_Capstone.Data;
using LoveWedLive_Capstone.Models;

namespace LoveWedLive_Capstone.Controllers
{
    public class AdminsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admins
        public ActionResult AdminVendorView(string searchString)
        {
            var vendor = from s in _context.Vendors.Include(v => v.Address)
                         select s;
            

            if (!string.IsNullOrEmpty(searchString))
            {
                vendor = vendor.Where(v => v.CompanyName.Contains(searchString));
                

            } 
            return View(vendor);  
        }

        public ActionResult AdminCustomerView(string searchString)
        {
            var customer = from c in _context.Customers.Include(v => v.Address)
                         select c;


            if (!string.IsNullOrEmpty(searchString))
            {
                customer = customer.Where(v => v.LastName.Contains(searchString));


            }
            return View(customer);
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .Include(a => a.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AdminName,IdentityUserId")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", admin.IdentityUserId);
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> AdminEditCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(v => v.Address)
                .Include(v => v.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminEditCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               // return RedirectToAction(nameof(Index));
            }
            //ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", customer.AddressId);
            //ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(customer);
        }

        public async Task<IActionResult> AdminEditVendor(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .Include(v => v.Address)
                .Include(v => v.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminEditVendor(int id, Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(vendor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(HomeController));
            }
            //ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id", customer.AddressId);
            //ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", customer.IdentityUserId);
            return View(vendor);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> AdminDeleteCustomer(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(v => v.Address)
                .Include(v => v.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("AdminDeleteCustomer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminDeleteCustomerConfirmed(int id) 
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        

        public async Task<IActionResult> AdminDeleteVendor(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendors = await _context.Vendors
                .Include(v => v.Address)
                .Include(v => v.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendors == null)
            {
                return NotFound();
            }

            return View(vendors);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("AdminDeleteVendor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminDeleteVendorConfirmed(int id) 
        {
            var vendor = await _context.Vendors.FindAsync(id);
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.Id == id);
        }
    }
}
