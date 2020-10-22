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
using Microsoft.AspNetCore.Identity;

namespace LoveWedLive_Capstone.Controllers
{
    public class QuoteRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuoteRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QuoteRequests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.QuoteRequests.Include(q => q.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: QuoteRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quoteRequest = await _context.QuoteRequests
                .Include(q => q.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quoteRequest == null)
            {
                return NotFound();
            }

            return View(quoteRequest);
        }

        // GET: QuoteRequests/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        // POST: QuoteRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HoursRequested,DayOfWedding,DateAndTimeOfRequest,VenueStreetName,VenueCity,VenueState,VenueZip,IsRequestingPhotographer,IsRequestingPhotoBooth,IsRequestingDJ,IsRequestingOfficiant,IsRequestngWeddingStylist,CustomerId")]  QuoteRequest quoteRequest)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var Customer = _context.Customers.Where(c => c.IdentityUserId == userId).FirstOrDefault();
                quoteRequest.CustomerId = Customer.Id;
                _context.Add(quoteRequest);  
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(quoteRequest);
        }

        // GET: QuoteRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quoteRequest = await _context.QuoteRequests.FindAsync(id);
            if (quoteRequest == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", quoteRequest.CustomerId);
            return View(quoteRequest);
        }

        // POST: QuoteRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HoursRequested,DayOfWedding,DateAndTimeOfRequest,VenueStreetName,VenueCity,VenueState,VenueZip,IsRequestingPhotographer,IsRequestingPhotoBooth,IsRequestingDJ,IsRequestingOfficiant,IsRequestngWeddingStylist,CustomerId")] QuoteRequest quoteRequest)
        {
            if (id != quoteRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quoteRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuoteRequestExists(quoteRequest.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", quoteRequest.CustomerId);
            return View(quoteRequest);
        }

        // GET: QuoteRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quoteRequest = await _context.QuoteRequests
                .Include(q => q.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quoteRequest == null)
            {
                return NotFound();
            }

            return View(quoteRequest);
        }

        // POST: QuoteRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quoteRequest = await _context.QuoteRequests.FindAsync(id);
            _context.QuoteRequests.Remove(quoteRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuoteRequestExists(int id)
        {
            return _context.QuoteRequests.Any(e => e.Id == id);
        }
    }
}
