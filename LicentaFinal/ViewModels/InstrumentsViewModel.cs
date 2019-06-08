using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicentaFinal.Models;

namespace LicentaFinal.ViewModels
{
    public class InstrumentsViewModel
    {
        //public User User { get; set; }
        public IEnumerable<Instrument> Instruments { get; set; }
    }
}
