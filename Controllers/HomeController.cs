using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using insuranceQuoter.Models;
using insuranceQuoter.ViewModels;

namespace insuranceQuoter.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult QuoteRequest(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, int carYear, string carMake, string carModel, bool dui, string speedingTickets, string coverageType)
        {
            using(InsuranceQuoteEntities db = new InsuranceQuoteEntities())
            {
                // Map user input to model properties
                var quote = new VehicleQuote()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    EmailAddress = emailAddress,
                    DOB = Convert.ToDateTime(dateOfBirth),
                    CarYear = carYear,
                    CarMake = carMake,
                    CarModel = carModel,
                    DUI = dui,
                    SpeedingTickets = Convert.ToInt32(speedingTickets),
                    CoverageType = coverageType,
                };

                // Calculates insurance quote based on parameters
                decimal quoteAmount = 50;
                int age = DateTime.Now.Year - quote.DOB.Year;
                if (age > 100) quoteAmount += 25;
                if (age < 25) quoteAmount += 25;
                if (age < 18) quoteAmount += 75; // This adds to the user being under 25, for a total of $100
                if (quote.CarYear < 2000 || quote.CarYear > 2015) quoteAmount += 25;
                if (quote.CarMake.ToLower() == "porsche") quoteAmount += 25;
                if (quote.CarMake.ToLower() == "porsche" && quote.CarModel.ToLower() == "911 carrera") quoteAmount += 25;
                if (quote.SpeedingTickets > 0) quoteAmount += quote.SpeedingTickets * 10;
                if (quote.DUI) quoteAmount += quoteAmount * 25 / 100; // Add 25% to rate if they have a DUI
                if (quote.CoverageType == "full") quoteAmount += quoteAmount * 50 / 100;
                quote.Quote = quoteAmount;

                db.VehicleQuotes.Add(quote);
                db.SaveChanges();

                ViewBag.endQuote = quoteAmount.ToString("C"); // Display quote amount to user upon form submission
                return View("Quote");
            }            
        }

        public ActionResult Quote()
        {
            return View();
        }
    }
}