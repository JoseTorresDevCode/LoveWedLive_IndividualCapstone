using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LoveWedLive_Capstone.Models;
using System.Configuration;
using System.Data.SqlClient;


namespace LoveWedLive_Capstone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            //string markers = "[";
            //string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            //SqlCommand cmd = new SqlCommand("SELECT * FROM Sp_GeoLoc");
            //using (SqlConnection con = new SqlConnection(conString))
            //{
            //    cmd.Connection = con;
            //    con.Open();
            //    using (SqlDataReader sdr = cmd.ExecuteReader())
            //    {
            //        while (sdr.Read())
            //        {
            //            markers += "{";
            //            markers += string.Format("'title': '{0}',", sdr["CompanyName"]);
            //            markers += string.Format("'lat': '{0}',", sdr["Latitude"]);
            //            markers += string.Format("'lng': '{0}',", sdr["Longitude"]);
            //            markers += string.Format("'description': '{0}'", sdr["VendorType"]);
            //            markers += "},";
            //        }
            //    }
            //    con.Close();
            //}

            //markers += "];";
            //ViewBag.Markers = markers;
            return View();
        }
    
    public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
