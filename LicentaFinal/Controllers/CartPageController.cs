using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using LicentaFinal.Models;
using LicentaFinal.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LicentaFinal.Controllers
{
    public class CartPageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartPageController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET
        public ActionResult CartPage()
        {

            var order = _context.Cart.Where(x => x.UserId.Equals(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

            return View("~/Views/Home/CartPage.cshtml", order);
        }
        [HttpPost]
        public void CheckOut()
        {
            var orders = _context.Cart.Where(x => x.UserId.Equals(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))).ToList();
            foreach (var order in orders)
            {
                _context.Cart.Remove(order);

            }
            _context.SaveChanges();
        }

        [HttpPost]
        public ActionResult DeleteInstrument(Instrument instrumentRequest)
        {
            var order = _context.Cart.First(s => s.Id == instrumentRequest.Id && s.UserId.Equals(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if (order == null)
            {
                return BadRequest();
            }

            _context.Cart.Remove(order);
            _context.SaveChanges();

            return Ok();
        }
    }
}
