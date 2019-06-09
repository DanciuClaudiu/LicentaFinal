using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using LicentaFinal.Models;
using LicentaFinal.Data;
using System.Collections;
using System.Net.Mail;
using System.Net;
using NETCore.MailKit.Core;
using MimeKit;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

            return View("~/Views/Home/CartPage.cshtml", order);
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
