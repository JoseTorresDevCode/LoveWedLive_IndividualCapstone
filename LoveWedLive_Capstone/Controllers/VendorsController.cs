using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoveWedLive_Capstone.Data;
using LoveWedLive_Capstone.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace LoveWedLive_Capstone.Controllers
{
    public class VendorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vendors
        public ActionResult Index(string searchString)
        {

            var vendor = from s in _context.Vendors.Include(v => v.Address)
                         select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                vendor = vendor.Where(v => v.Address.City.Contains(searchString));

            }
            return View(vendor);
        }

        // GET: Vendors/Details/5
        public ActionResult Details(int? id)
        {
            {
                var getVendor = _context.Vendors.Where(c => c.Id == id).Include(v => v.Address).SingleOrDefault();
                return View(getVendor);
            }
        }

        // GET: Vendors/Create
        public IActionResult Create()
        {
            //ViewData["AddressId"] = new SelectList(_context.Addresses, "Id", "Id");
            //ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Vendors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Vendor vendor)
        {
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={vendor.Address.StreetName},+{vendor.Address.City},+{vendor.Address.State},{vendor.Address.Zipcode}&key={APIKeys.GeocodeKey}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonResult = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                JObject geoCode = JObject.Parse(jsonResult);
                vendor.Lat = (double)geoCode["results"][0]["geometry"]["location"]["lat"];
                vendor.Long = (double)geoCode["results"][0]["geometry"]["location"]["lng"];
            }
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                vendor.IdentityUserId = userId;

                _context.Add(vendor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vendor);
        }

        // GET: Vendors/Edit/5
        public ActionResult Edit(int? id)
        {
            var getVendor = _context.Vendors.Where(c => c.Id == id).Include(v => v.Address).SingleOrDefault();
            return View(getVendor);
        }
//          {
//        if (id == null)
//            {
//                return NotFound();
//    }

//    var vendor = await _context.Vendors

//        .Include(v => v.IdentityUser)
//        .FirstOrDefaultAsync(m => m.Id == id);

//            if (vendor == null)
//            {
//                return NotFound();
//}

//return View(vendor);
//          }

        // POST: Vendors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
          [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Vendors/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.Id == id);
        }
    }
}
