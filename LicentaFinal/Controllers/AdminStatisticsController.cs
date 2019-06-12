using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LicentaFinal.Models;
using LicentaFinal.Data;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LicentaFinal.Controllers
{
    public class AdminStatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminStatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult AdminStatistics()
        {

            var instrumemnts = _context.Instrument.Where(x => x.Id >= 0);

            return View("~/Views/Home/AdminStatistics.cshtml", instrumemnts);
        }
    }
}
