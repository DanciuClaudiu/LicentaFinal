using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LicentaFinal.Models;
using LicentaFinal.Data;

namespace LicentaFinal.Controllers
{
    public class AdminPageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPageController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult AdminPage()
        {

            

            var instrumemnts = _context.Instrument.Where(x => x.Id >= 0);

            return View("~/Views/Home/AdminPage.cshtml", instrumemnts);
        }

        [HttpPost]
        public ActionResult DeleteInstrument(Instrument instrumentRequest)
        {
            var instrument = _context.Instrument.First(s => s.Id == instrumentRequest.Id);
            if (instrument == null)
            {
                return BadRequest();
            }

            _context.Remove(instrument);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public ActionResult UpdateInstrument(Instrument instrumentRequest)
        {
            var instrument = _context.Instrument.First(s => s.Id == instrumentRequest.Id);

            if (instrument == null)
            {
                return BadRequest();
            }

            if (!string.IsNullOrEmpty(instrumentRequest.Name))
                instrument.Name = instrumentRequest.Name;
            if (instrumentRequest.Price != 0)
                instrument.Price = instrumentRequest.Price;
            if (instrumentRequest.Quantity != 0)
                instrument.Quantity = instrumentRequest.Quantity;
            if (!string.IsNullOrEmpty(instrumentRequest.Type))
                instrument.Type = instrumentRequest.Type;

            _context.SaveChanges();

            return Ok();
        }
    }
}