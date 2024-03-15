using PizzeriaMK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PizzeriaMK.Controllers
{
    public class HomeController : Controller
    {
        DBContext db = new DBContext();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            OrderString();
            return View(db.Products.ToList());
        }

        public void OrderString()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                int userId = Convert.ToInt32(HttpContext.User.Identity.Name);
                using (DBContext db = new DBContext())
                {
                    OrderSummary Carrello = db.OrderSummaries.Where(c => c.UserId == userId && c.State == "NON EVASO").FirstOrDefault();
                    if (Carrello == null)
                    {
                        OrderSummary newOrder = new OrderSummary
                        {
                            UserId = userId,
                            State = "NON EVASO"
                        };
                        db.OrderSummaries.Add(newOrder);
                        db.SaveChanges();
                    }
                }
            }
        }

    }
}
