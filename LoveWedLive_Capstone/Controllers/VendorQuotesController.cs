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
using System.Security.Cryptography.X509Certificates;

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
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.VendorQuotes.Include(v => v.Vendor);
            var customerss = _context.Customers.Where(c => c.IdentityUserId == userId);
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
        public async Task<IActionResult> Create(VendorQuote vendorQuote,Customer customer)
        {
            
            if (ModelState.IsValid)
            {
                
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var Vendor = _context.Vendors.Where(c => c.IdentityUserId == userId).FirstOrDefault();
                var userId2 = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var Customer = _context.Customers.Where(c => c.IdentityUserId == userId).FirstOrDefault(); 
                vendorQuote.VendorId = Vendor.Id;
                _context.Add(vendorQuote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vendorQuote);
        }

        // get: vendorquotes/edit/5
        //public async task<iactionresult> edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return notfound();
        //    }

        //    var vendorquote = await _context.vendorquotes.findasync(id);
        //    if (vendorquote == null)
        //    {
        //        return notfound();
        //    }
        //    viewdata["vendorid"] = new selectlist(_context.vendors, "id", "id", vendorquote.vendorid);
        //    return view(vendorquote);
        //}

        // post: vendorquotes/edit/5
        // to protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?linkid=317598.
        //[httppost]
        //[validateantiforgerytoken]
        //public async task<iactionresult> edit(int id,vendorquote vendorquote)
        //{
        //    if (id != vendorquote.id)
        //    {
        //        return notfound();
        //    }

        //    if (modelstate.isvalid)
        //    {
        //        try
        //        {
        //            _context.update(vendorquote);
        //            await _context.savechangesasync();
        //        }
        //        catch (dbupdateconcurrencyexception)
        //        {
        //            if (!vendorquoteexists(vendorquote.id))
        //            {
        //                return notfound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return redirecttoaction(nameof(index));
        //    }
        //    viewdata["vendorid"] = new selectlist(_context.vendors, "id", "id", vendorquote.vendorid);
        //    return view(vendorquote);
        //}

        // get: vendorquotes/delete/5
        //public async task<iactionresult> delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return notfound();
        //    }

        //    var vendorquote = await _context.vendorquotes
        //        .include(v => v.vendor)
        //        .firstordefaultasync(m => m.id == id);
        //    if (vendorquote == null)
        //    {
        //        return notfound();
        //    }

        //    return view(vendorquote);
        //}

        //    // post: vendorquotes/delete/5
        //    [httppost, actionname("delete")]
        //    [validateantiforgerytoken]
        //    public async task<iactionresult> deleteconfirmed(int id)
        //    {
        //        var vendorquote = await _context.vendorquotes.findasync(id);
        //        _context.vendorquotes.remove(vendorquote);
        //        await _context.savechangesasync();
        //        return redirecttoaction(nameof(index));
        //    }

        private bool VendorQuoteExists(int id)
        {
            return _context.VendorQuotes.Any(e => e.Id == id);
        }
    }
}
