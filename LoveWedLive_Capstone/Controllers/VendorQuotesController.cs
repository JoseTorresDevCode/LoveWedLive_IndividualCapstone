using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoveWedLive_Capstone.Data;
using LoveWedLive_Capstone.Models;
using System.Security.Claims;

namespace LoveWedLive_Capstone.Controllers
{
    public class VendorQuotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorQuotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VendorQuotes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VendorQuotes.Include(v => v.Vendor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: VendorQuotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorQuote = await _context.VendorQuotes
                .Include(v => v.Vendor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorQuote == null)
            {
                return NotFound();
            }

            return View(vendorQuote);
        }

        // GET: VendorQuotes/Create
        public IActionResult Create()
        {
            ViewData["VendorId"] = new SelectList(_context.Vendors, "Id", "Id");
            return View();
        }

        // POST: VendorQuotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PriceQuote,QuotedHours,VendorId")] VendorQuote vendorQuote)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier); 
                var Vendor = _context.Vendors.Where(c => c.IdentityUserId == userId).FirstOrDefault();
                vendorQuote.VendorId = Vendor.Id;
                _context.Add(vendorQuote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(vendorQuote);
        }

        // GET: VendorQuotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorQuote = await _context.VendorQuotes.FindAsync(id);
            if (vendorQuote == null)
            {
                return NotFound();
            }
            ViewData["VendorId"] = new SelectList(_context.Vendors, "Id", "Id", vendorQuote.VendorId);
            return View(vendorQuote);
        }

        // POST: VendorQuotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PriceQuote,QuotedHours,VendorId")] VendorQuote vendorQuote)
        {
            if (id != vendorQuote.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorQuote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorQuoteExists(vendorQuote.Id))
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
            ViewData["VendorId"] = new SelectList(_context.Vendors, "Id", "Id", vendorQuote.VendorId);
            return View(vendorQuote);
        }

        // GET: VendorQuotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorQuote = await _context.VendorQuotes
                .Include(v => v.Vendor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorQuote == null)
            {
                return NotFound();
            }

            return View(vendorQuote);
        }

        // POST: VendorQuotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorQuote = await _context.VendorQuotes.FindAsync(id);
            _context.VendorQuotes.Remove(vendorQuote);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorQuoteExists(int id)
        {
            return _context.VendorQuotes.Any(e => e.Id == id);
        }
    }
}
