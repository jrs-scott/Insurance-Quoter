using insuranceQuoter.Models;
using insuranceQuoter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace insuranceQuoter.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (InsuranceQuoteEntities db = new InsuranceQuoteEntities())
            {
                var quotes = db.VehicleQuotes; // All records in the database
                var carQuoteVms = new List<CarQuoteVm>(); // Instantiate and populate list of view models for each record in the db
                foreach (var quote in quotes)
                {
                    var carQuoteVm = new CarQuoteVm
                    {
                        Id = quote.Id,
                        FirstName = quote.FirstName,
                        LastName = quote.LastName,
                        EmailAddress = quote.EmailAddress,
                        Quote = quote.Quote
                    };
                    carQuoteVms.Add(carQuoteVm);                    
                }
                return View(carQuoteVms);
            }
        }
    }
}