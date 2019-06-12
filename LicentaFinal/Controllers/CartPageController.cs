using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using LicentaFinal.Models;
using LicentaFinal.ViewModels;
using LicentaFinal.Data;
using System.Collections;
using System.Net.Mail;
using System.Net;
using NETCore.MailKit.Core;
using MimeKit;


namespace LicentaFinal.Controllers
{
    public class CartPageController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private readonly IEmailService _EmailService;
        //private IEmailService emailService;

        public CartPageController(ApplicationDbContext context)
        {
            _context = context;
            //_EmailService = emailService;
        }
        // GET
        public ActionResult CartPage()
        {

            var order = _context.Cart.Where(x => x.UserId.Equals(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
            int minPrice = 1000000000, maxPrice = 0, maxRecomandations = 0;


            var instrumements = _context.Instrument.Where(x => x.Id >= 0);
            var r = new Random((int)DateTime.Now.Ticks);
            var shuffledList = instrumements.Select(x => new { Number = r.Next(), Item = x }).OrderBy(x => x.Number).Select(x => x.Item);




            List<string> instrumentTypes = new List<string>();

            List<int> recomendedInstrumentIDs = new List<int>();
            List<Instrument> recomendedInstruments = new List<Instrument>();

            foreach (var instrument in order)
            {
                if (!instrumentTypes.Contains(instrument.InstrumentType))
                {
                    instrumentTypes.Add(instrument.InstrumentType);
                }
                if (instrument.InstrumentPrice < minPrice) minPrice = instrument.InstrumentPrice;
                if (instrument.InstrumentPrice > maxPrice) maxPrice = instrument.InstrumentPrice;
                
            }
            
            foreach (var instrument in shuffledList)
            {
                if(!recomendedInstrumentIDs.Contains(instrument.Id) && instrument.Price >= minPrice && instrument.Price <= maxPrice && isInCart(instrument.Id) == false && instrumentTypes.Contains(instrument.Type))
                {
                    recomendedInstrumentIDs.Add(instrument.Id);
                    maxRecomandations++;

                }
                if (maxRecomandations == 4) break;
            }

            foreach(int i in recomendedInstrumentIDs)
            {
                var instr = _context.Instrument.Where(x => x.Id.Equals(i));
                foreach(var j in instr) recomendedInstruments.Add(j);
            }

            if (recomendedInstruments.Any() == true)
            {
                CartViewModel model = new CartViewModel
                {
                    Cart = order,
                    RecomendedInstruments = recomendedInstruments
                };

                return View("~/Views/Home/CartPage.cshtml", model);
            }
            else
            {
                foreach (var instrument in shuffledList)
                {

                    recomendedInstrumentIDs.Add(instrument.Id);
                    maxRecomandations++;

                    if (maxRecomandations == 4) break;
                }

                foreach (int i in recomendedInstrumentIDs)
                {
                    var instr = _context.Instrument.Where(x => x.Id.Equals(i));
                    foreach (var j in instr) recomendedInstruments.Add(j);
                }

                CartViewModel model = new CartViewModel
                {
                    Cart = order,
                    RecomendedInstruments = recomendedInstruments
                };

                return View("~/Views/Home/CartPage.cshtml", model);
            }
                

            
        }




        private bool isInCart(int instrumentId)
        {
            var order = _context.Cart.Where(x => x.UserId.Equals(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

            foreach(var ord in order)
            {
                if (ord.InstrumentId == instrumentId) return true;
            }
            return false;
        }


        [HttpPost]
        public void CheckOut()
        {
            var orders = _context.Cart.Where(x => x.UserId.Equals(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))).ToList();
            foreach (var order in orders)
            {

                var instrument = _context.Instrument.First(s => s.Id == order.InstrumentId);
                if (instrument != null)
                {
                    if (instrument.Quantity - order.CartIntrumentQuantity >= 0)
                    {

                        instrument.Quantity -= order.CartIntrumentQuantity;


                        _context.SaveChanges();
                        _context.Cart.Remove(order);

                        Sales sale = new Sales
                        {
                            UserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                            IntrumentId = order.InstrumentId,
                            InstrumentPrice = order.InstrumentPrice,
                            Quantity = order.CartIntrumentQuantity,
                            SaleId = Guid.NewGuid(),
                            PurchaseDate = DateTime.Now
                        };

                        _context.Sales.Add(sale);


                    }
                }

                

            }
            _context.SaveChanges();


        }


        //public IActionResult Email()
        //{
        //    ViewData["Message"] = "ASP.NET Core mvc send email example";

        //    var message = new MimeMessage();

        //    message.From.Add(new MailAddress("eu", "danciuclaudiu18@gmail.com"));


        //    _EmailService.Send("claudiuvdanciu@gmail.com", "ASP.NET Core mvc send email example", "Send from asp.net core mvc action");

        //    return View();
        //}


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

        [HttpPost]
        public ActionResult ChangeQuantity (Cart param)
        {
            var order = _context.Cart.First(s => s.Id == param.Id && s.UserId.Equals(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if (order == null)
            {
                return BadRequest();
            }

            order.CartIntrumentQuantity = param.CartIntrumentQuantity;

            _context.SaveChanges();

            return Ok();
        }
    }
}
