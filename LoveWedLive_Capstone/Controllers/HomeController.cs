using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LoveWedLive_Capstone.Models;
using Stripe.BillingPortal;
using Stripe;
using Stripe.Checkout;

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
            // Set your secret key. Remember to switch to your live secret key in production!
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.ApiKey = "sk_test_51HbotsJGLwxagEOLsjFtu0nSwsAXA5H3i0BUBw8Q0v105uH8hAdnsjnL57NjhzesPuVugD3FrwV3evw8lQoaLn1e007HeTvTZs";

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                BillingAddressCollection = "auto",
                PaymentMethodTypes = new List<string>
                {
                     "card",
                },
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = new List<string>
                    {
                         "US",
                         "CA",
                    },
                },
                LineItems = new List<SessionLineItemOptions>
                {
                      // products
                      new SessionLineItemOptions
                      {
                          Name = "Subscription",
                          Description = "Monthly Subscription to Love Wed Live",
                          Amount = 200,
                          Currency = "usd",
                          Quantity = 1,
                      },
                },
                Mode = "payment",
                SuccessUrl = "https://example.com/success", // Website, Stripe will redirect to this URL
                CancelUrl = "https://example.com/cancel", // If User cancels, Stripe will redirect to this URL
                // Configurations
                PaymentIntentData = new SessionPaymentIntentDataOptions
                {
                    // Maybe is Order ID, Customer ID, Descriptions,...
                    Metadata = new Dictionary<string, string>
                    {
                        //For example: Order ID, Description
                        //{ "Order_ID", "123456"}, //Order Id in Database
                       // { "Description", "Monthly Subscription to Love Wed Live" }
                    }
                }
            };

            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);
            
            return View(session);
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
