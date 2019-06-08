using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LicentaFinal.Data;
using LicentaFinal.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LicentaFinal.Controllers
{
    public class UserPageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserPageController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET
        public ActionResult UserPage()
        {
            var instrumemnts = _context.Instrument.Where(x => x.Id >= 0);

            return View("~/Views/Home/UserPage.cshtml", instrumemnts);
        }
        [HttpPost]
        public async Task<ActionResult> AddToCart(int id)
        {
            var instrument = _context.Instrument.FirstOrDefault(x => x.Id == id);
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            Cart myCart = new Cart
            {
                InstrumentName = instrument.Name,
                InstrumentPrice = instrument.Price,
                InstrumentQuantity = instrument.Quantity,
                InstrumentType = instrument.Type,
                UserId = userId
            };


            _context.Cart.Add(myCart);
            await _context.SaveChangesAsync();

            return View("~/Views/Home/UserPage.cshtml");
        }
    }
}
